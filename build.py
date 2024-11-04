#!/usr/bin/env python3


import argparse
import os
import re
import shutil
import subprocess
import sys
import tarfile
import urllib.request
from pathlib import Path

from tools.autd3_build_utils.autd3_build_utils import (
    BaseConfig,
    err,
    fetch_submodule,
    info,
    remove,
    rremove,
    run_command,
    working_dir,
)


class Config(BaseConfig):
    release: bool
    no_examples: bool

    def __init__(self, args) -> None:  # noqa: ANN001
        super().__init__()

        self.release = getattr(args, "release", False)
        self.no_examples = getattr(args, "no_examples", False)


def should_update_dll(config: Config, version: str) -> bool:
    if config.is_windows():
        if not Path("src/native/windows/x64/autd3capi.dll").is_file() or not Path("tests/autd3capi.dll").is_file():
            return True
    elif config.is_macos():
        if not Path("src/native/osx/universal/libautd3capi.dylib").is_file() or not Path("tests/libautd3capi.dylib").is_file():
            return True
    elif config.is_linux():  # noqa: SIM102
        if not Path("src/native/linux/x64/libautd3capi.so").is_file() or not Path("tests/libautd3capi.so").is_file():
            return True

    if not Path("VERSION").is_file():
        return True

    with Path("VERSION").open("r") as f:
        old_version = f.read().strip()

    return old_version != version


def download_and_extract(url: str, *dest_dirs: str) -> None:
    tmp_file = Path("tmp.zip" if url.endswith(".zip") else "tmp.tar.gz")
    urllib.request.urlretrieve(url, tmp_file)
    if tmp_file.suffix == ".zip":
        shutil.unpack_archive(tmp_file, ".")
    else:
        with tarfile.open(tmp_file, "r:gz") as tar:
            tar.extractall(filter="fully_trusted")
    tmp_file.unlink()

    for dest_dir in dest_dirs:
        Path(dest_dir).mkdir(parents=True, exist_ok=True)

    for dll in Path("bin").glob("*.dll"):
        for dest_dir in dest_dirs:
            shutil.copy(dll, dest_dir)
    for dylib in Path("bin").glob("*.dylib"):
        for dest_dir in dest_dirs:
            shutil.copy(dylib, dest_dir)
    for so in Path("bin").glob("*.so"):
        for dest_dir in dest_dirs:
            shutil.copy(so, dest_dir)
    remove("bin")


def copy_dll(config: Config) -> None:
    with Path("src/AUTD3Sharp.csproj").open() as f:
        content = f.read()
        version = re.search(r"<Version>(.*)</Version>", content).group(1).split(".")
        version = ".".join(version[:4]) if version[2].endswith("rc") else ".".join(version[:3])

    if not should_update_dll(config, version):
        return

    base_url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}"
    download_and_extract(f"{base_url}/autd3-v{version}-win-x64-shared.zip", "src/native/windows/x64", "tests")
    download_and_extract(f"{base_url}/autd3-v{version}-win-aarch64-shared.zip", "src/native/windows/arm")
    download_and_extract(f"{base_url}/autd3-v{version}-macos-aarch64-shared.tar.gz", "src/native/osx/aarch64", "tests")
    download_and_extract(f"{base_url}/autd3-v{version}-linux-x64-shared.tar.gz", "src/native/linux/x64", "tests")

    shutil.copyfile("LICENSE", "src/LICENSE.txt")
    with Path("src/LICENSE.txt").open("a") as f:
        f.write("\n=========================================================\n")
        f.write(Path("ThirdPartyNotice.txt").read_text())

    remove("lib")

    Path("VERSION").write_text(version)


def rm_tmp_source() -> None:
    _ = subprocess.run(
        ["dotnet", "nuget", "remove", "source", "autd3sharp_local_derive"],
        check=False,
        capture_output=True,
    )
    _ = subprocess.run(
        ["dotnet", "nuget", "remove", "source", "autd3sharp_local"],
        check=False,
        capture_output=True,
    )
    _ = subprocess.run(
        ["dotnet", "nuget", "locals", "all", "-c"],
        check=False,
        capture_output=True,
    )


def cs_build(args) -> None:  # noqa: ANN001
    config = Config(args)

    copy_dll(config)

    rm_tmp_source()

    with working_dir("derive"):
        command = ["dotnet", "build"]
        if config.release:
            command.append("-c:Release")
        run_command(command)

        bin_dir = "Release" if config.release else "Debug"
        run_command(
            [
                "dotnet",
                "nuget",
                "add",
                "source",
                f"{Path.cwd()}/bin/{bin_dir}",
                "-n",
                "autd3sharp_local_derive",
            ],
        )

    with working_dir("src"):
        command = ["dotnet", "build"]
        if config.release:
            command.append("-c:Release")
        run_command(command)

        bin_dir = "Release" if config.release else "Debug"
        run_command(
            [
                "dotnet",
                "nuget",
                "add",
                "source",
                f"{Path.cwd()}/bin/{bin_dir}",
                "-n",
                "autd3sharp_local",
            ],
        )

    if not config.no_examples:
        info("Building examples...")
        with working_dir("example"):
            command = ["dotnet", "build"]
            if config.release:
                command.append("-c:Release")
            run_command(command)


def cs_test(args) -> None:  # noqa: ANN001
    args.no_examples = True

    cs_build(args)

    with working_dir("tests"):
        run_command(["dotnet", "test"])


def check_if_all_native_methods_called() -> None:
    defined_methods = set()
    pattern = re.compile("\\s*public static extern .* (AUTD.*?)\\(.*")
    for file in Path("src/NativeMethods").rglob("*.cs"):
        with file.open() as f:
            for line in f.readlines():
                result = pattern.match(line)
                if result:
                    defined_methods.add(result.group(1))
    defined_methods = set(filter(lambda x: not x.endswith("T4010A1"), defined_methods))
    defined_methods = set(filter(lambda x: x != "AUTDSamplingConfigDivision", defined_methods))

    used_methods = set()
    pattern = re.compile("NativeMethods.*?\\.(AUTD.*?)\\(")

    paths: set[Path] = set()
    paths |= set(Path("src").rglob("*.cs"))
    paths -= set(Path("src/NativeMethods").rglob("*.cs"))
    paths |= set(Path("tests").rglob("*.cs"))
    paths.add(Path("src/NativeMethods/DriverExt.cs"))

    for file in paths:
        with file.open(encoding="utf-8") as f:
            for line in f.readlines():
                result = pattern.findall(line)
                if result:
                    for group in result:
                        used_methods.add(group)
    unused_methods = defined_methods.difference(used_methods)
    if len(unused_methods) > 0:
        err("Following native methods are defined but not used.")
        for method in unused_methods:
            print(f"\t{method}")
        sys.exit(-1)


def cs_cov(args) -> None:  # noqa: ANN001
    cs_build(args)

    check_if_all_native_methods_called()

    with working_dir("tests"):
        run_command(
            [
                "dotnet",
                "test",
                '--collect:"XPlat Code Coverage"',
                "--settings",
                "coverlet.runsettings",
            ],
        )

        if args.html:
            cov_res = sorted(
                Path("TestResults").rglob("coverage.cobertura.xml"),
                key=os.path.getmtime,
                reverse=True,
            )[0]
            command = [
                "reportgenerator",
                f"-reports:{cov_res}",
                "-targetdir:html",
                "-reporttypes:Html",
            ]
            run_command(command)


def cs_run(args) -> None:  # noqa: ANN001
    args.no_examples = False
    cs_build(args)

    with working_dir("example"):
        command = ["dotnet", "run"]
        command.append("--project")
        command.append(args.target)
        if args.release:
            command.append("-c:Release")
        run_command(command)


def cs_clear(_) -> None:  # noqa: ANN001
    remove("derive/bin")
    remove("derive/obj")

    remove("src/bin")
    remove("src/obj")

    remove("tests/bin")
    rremove("tests/*.dll")
    rremove("tests/*.dylib")
    rremove("tests/*.so")
    remove("tests/obj")

    rremove("example/**/bin")
    rremove("example/**/obj")


def should_update_dll_unity(config: Config, version: str) -> bool:
    if config.is_windows():
        if not Path("unity/Assets/Plugins/x86_64/autd3capi.dll").is_file():
            return True
    elif config.is_macos():
        if not Path("unity/Assets/Plugins/x86_64/libautd3capi.dylib").is_file():
            return True
    elif config.is_linux():  # noqa: SIM102
        if not Path("unity/Assets/Plugins/x86_64/libautd3capi.so").is_file():
            return True

    if not Path("UNITY_VERSION").is_file():
        return True

    with Path("UNITY_VERSION").open("r") as f:
        old_version = f.read().strip()

    return old_version != version


def copy_dll_unity(config: Config) -> None:
    with Path("unity/Assets/package.json").open() as f:
        content = f.read()
        version = re.search(r'"version": "(.*)"', content).group(1).split(".")
        version = ".".join(version[:4]) if version[2].endswith("rc") else ".".join(version[:3])

    if not should_update_dll_unity(config, version):
        return

    base_url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}"
    download_and_extract(f"{base_url}/autd3-v{version}-win-x64-unity.zip", "unity/Assets/Plugins/x86_64")
    download_and_extract(f"{base_url}/autd3-v{version}-win-aarch64-unity.zip", "unity/Assets/Plugins/ARM64")
    download_and_extract(f"{base_url}/autd3-v{version}-macos-aarch64-unity.tar.gz", "unity/Assets/Plugins/aarch64")
    download_and_extract(f"{base_url}/autd3-v{version}-linux-x64-unity.tar.gz", "unity/Assets/Plugins/x86_64")

    shutil.copy("LICENSE", "unity/Assets/LICENSE.md")
    with Path("unity/Assets/LICENSE.md").open("a") as f:
        f.write("\n=========================================================\n")
        f.write(Path("ThirdPartyNotice.txt").read_text())
    shutil.copy("CHANGELOG.md", "unity/Assets/CHANGELOG.md")

    remove("lib")

    Path("UNITY_VERSION").write_text(version)


def unity_build(args) -> None:  # noqa: ANN001
    cs_build(args)

    config = Config(args)
    copy_dll_unity(config)

    ignore = shutil.ignore_patterns("NativeMethods", ".vs", "bin", "obj")
    shutil.copytree(
        "src",
        "unity/Assets/Scripts",
        dirs_exist_ok=True,
        ignore=ignore,
    )
    remove("unity/Assets/Scripts/AUTD3Sharp.csproj")
    remove("unity/Assets/Scripts/AUTD3Sharp.nuspec")
    remove("unity/Assets/Scripts/LICENSE.txt")
    remove("unity/Assets/Scripts/.gitignore")
    remove("unity/Assets/Scripts/.vs")
    remove("unity/Assets/Scripts/obj")
    remove("unity/Assets/Scripts/bin")
    remove("unity/Assets/Scripts/native")
    shutil.copy(
        "src/NativeMethods/DriverExt.cs",
        "unity/Assets/Scripts/NativeMethods/DriverExt.cs",
    )
    shutil.copy(
        "derive/GainAttribute.cs",
        "unity/Assets/Scripts/Derive/",
    )
    shutil.copy(
        "derive/ModulationAttribute.cs",
        "unity/Assets/Scripts/Derive/",
    )
    shutil.copy(
        "derive/PropertyAttribute.cs",
        "unity/Assets/Scripts/Derive/",
    )
    config_dir = config.release and "Release" or "Debug"
    for derive in Path(f"src/obj/{config_dir}/net8.0/generated/AUTD3Sharp.Derive").rglob("*.cs"):
        shutil.copy(derive, "unity/Assets/Scripts/Derive/")


def unity_clear(_) -> None:  # noqa: ANN001
    with working_dir("unity"):
        remove(".vs")
        remove("Library")
        remove("Logs")
        remove("obj")
        remove("Packages")
        remove("ProjectSettings")
        remove("UserSettings")
        rremove(
            "Assets/Scripts/**/*.cs",
            exclude="Assets/Scripts/NativeMethods/*.cs",
        )
        rremove("Assets/Plugins/x86_64/*.dll")
        rremove("Assets/Plugins/aarch64/*.dylib")
        rremove("Assets/Plugins/x86_64/*.so")


def util_update_ver(args) -> None:  # noqa: ANN001
    version = args.version

    for proj in Path("example").rglob("*.csproj"):
        content = proj.read_text()
        content = re.sub(
            r'"AUTD3Sharp" Version="(.*)"',
            f'"AUTD3Sharp" Version="{version}"',
            content,
            flags=re.MULTILINE,
        )
        proj.write_text(content)

    src_proj = Path("src/AUTD3Sharp.csproj")
    content = src_proj.read_text()
    content = re.sub(
        r"<Version>(.*)</Version>",
        f"<Version>{version}</Version>",
        content,
        flags=re.MULTILINE,
    )
    src_proj.write_text(content)

    derive_proj = Path("derive/AUTD3Sharp.Derive.csproj")
    content = derive_proj.read_text(encoding="UTF-8")
    content = re.sub(
        r"<Version>(.*)</Version>",
        f"<Version>{version}</Version>",
        content,
        flags=re.MULTILINE,
    )
    derive_proj.write_text(content, encoding="UTF-8")

    nuspec = Path("src/AUTD3Sharp.nuspec")
    content = nuspec.read_text()
    content = re.sub(
        r'"AUTD3Sharp\.Derive" version="(.*)"',
        f'"AUTD3Sharp.Derive" version="{version}"',
        content,
        flags=re.MULTILINE,
    )
    nuspec.write_text(content)

    with working_dir("unity"):
        package_json = Path("Assets/package.json")
        content = package_json.read_text()
        content = re.sub(
            r'"version": "(.*)"',
            f'"version": "{version}"',
            content,
            flags=re.MULTILINE,
        )
        package_json.write_text(content)


def util_gen_wrapper(_) -> None:  # noqa: ANN001
    fetch_submodule()

    if shutil.which("cargo") is not None:
        with working_dir("tools/wrapper-generator"):
            run_command(["cargo", "run"])
    else:
        err("cargo is not installed. Skip generating wrapper.")


def command_help(args) -> None:  # noqa: ANN001
    print(parser.parse_args([args.command, "--help"]))


if __name__ == "__main__":
    with working_dir(Path(__file__).parent):
        parser = argparse.ArgumentParser(description="autd3 library build script")
        subparsers = parser.add_subparsers()

        # cs
        parser_cs = subparsers.add_parser("cs", help="see `cs -h`")
        subparsers_cs = parser_cs.add_subparsers()

        # cs build
        parser_cs_build = subparsers_cs.add_parser("build", help="see `cs build -h`")
        parser_cs_build.add_argument("--release", action="store_true", help="release build")
        parser_cs_build.add_argument("--no-examples", action="store_true", help="skip building examples")
        parser_cs_build.set_defaults(handler=cs_build)

        # cs test
        parser_cs_test = subparsers_cs.add_parser("test", help="see `cs test -h`")
        parser_cs_test.add_argument("--release", action="store_true", help="release build")
        parser_cs_test.set_defaults(handler=cs_test)

        # cs cov
        parser_cs_cov = subparsers_cs.add_parser("cov", help="see `cs cov -h`")
        parser_cs_cov.add_argument("--release", action="store_true", help="release build")
        parser_cs_cov.add_argument("--html", action="store_true", help="generate html report", default=False)
        parser_cs_cov.set_defaults(handler=cs_cov)

        # cs run
        parser_cs_run = subparsers_cs.add_parser("run", help="see `cs run -h`")
        parser_cs_run.add_argument("target", help="binary target")
        parser_cs_run.add_argument("--release", action="store_true", help="release build")
        parser_cs_run.set_defaults(handler=cs_run)

        # cs clear
        parser_cs_clear = subparsers_cs.add_parser("clear", help="see `cs clear -h`")
        parser_cs_clear.set_defaults(handler=cs_clear)

        # unity
        parser_unity = subparsers.add_parser("unity", help="see `unity -h`")
        subparsers_unity = parser_unity.add_subparsers()

        # unity build
        parser_unity_build = subparsers_unity.add_parser("build", help="see `unity build -h`")
        parser_unity_build.add_argument("--release", action="store_true", help="release build")
        parser_unity_build.set_defaults(handler=unity_build)

        # unity clear
        parser_unity_clear = subparsers_unity.add_parser("clear", help="see `unity clear -h`")
        parser_unity_clear.set_defaults(handler=unity_clear)

        # util
        parser_util = subparsers.add_parser("util", help="see `util -h`")
        subparsers_util = parser_util.add_subparsers()

        # util update version
        parser_util_upver = subparsers_util.add_parser("upver", help="see `util upver -h`")
        parser_util_upver.add_argument("version", help="version")
        parser_util_upver.set_defaults(handler=util_update_ver)

        # util update version
        parser_util_gen_wrap = subparsers_util.add_parser("gen_wrap", help="see `util gen_wrap -h`")
        parser_util_gen_wrap.set_defaults(handler=util_gen_wrapper)

        # help
        parser_help = subparsers.add_parser("help", help="see `help -h`")
        parser_help.add_argument("command", help="command name which help is shown")
        parser_help.set_defaults(handler=command_help)

        args = parser.parse_args()
        if hasattr(args, "handler"):
            args.handler(args)
        else:
            parser.print_help()

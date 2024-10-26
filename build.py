#!/usr/bin/env python3


import argparse
import contextlib
import glob
import os
import platform
import re
import shutil
import subprocess
import sys
import tarfile
import urllib.request


def fetch_submodule():
    if shutil.which("git") is not None:
        with working_dir(os.path.dirname(os.path.abspath(__file__))):
            subprocess.run(["git", "submodule", "update", "--init"]).check_returncode()
    else:
        err("git is not installed. Skip fetching submodules.")


def generate_wrapper():
    if shutil.which("cargo") is not None:
        with working_dir(os.path.dirname(os.path.abspath(__file__))):
            with working_dir("tools/wrapper-generator"):
                subprocess.run(
                    [
                        "cargo",
                        "run",
                    ]
                ).check_returncode()
    else:
        err("cargo is not installed. Skip generating wrapper.")


def err(msg: str):
    print("\033[91mERR \033[0m: " + msg)


def warn(msg: str):
    print("\033[93mWARN\033[0m: " + msg)


def info(msg: str):
    print("\033[92mINFO\033[0m: " + msg)


def rm_f(path):
    try:
        os.remove(path)
    except FileNotFoundError:
        pass


def onexc(func, path, exeption):
    import stat

    if not os.access(path, os.W_OK):
        os.chmod(path, stat.S_IWUSR)
        func(path)
    else:
        raise


def rmtree_f(path):
    try:
        shutil.rmtree(path, onerror=onexc)
    except FileNotFoundError:
        pass


def glob_norm(path, recursive):
    return list(
        map(lambda p: os.path.normpath(p), glob.glob(path, recursive=recursive))
    )


def rm_glob_f(path, exclude=None, recursive=True):
    if exclude is not None:
        for f in list(
            set(glob_norm(path, recursive=recursive))
            - set(glob_norm(exclude, recursive=recursive))
        ):
            rm_f(f)
    else:
        for f in glob.glob(path, recursive=recursive):
            rm_f(f)


def rmtree_glob_f(path):
    for f in glob.glob(path):
        rmtree_f(f)


@contextlib.contextmanager
def working_dir(path):
    cwd = os.getcwd()
    os.chdir(path)
    try:
        yield
    finally:
        os.chdir(cwd)


class Config:
    _platform: str
    release: bool
    no_examples: bool

    def __init__(self, args):
        self._platform = platform.system()

        if not self.is_windows() and not self.is_macos() and not self.is_linux():
            err(f'platform "{platform.system()}" is not supported.')
            sys.exit(-1)

        self.release = hasattr(args, "release") and args.release
        self.no_examples = hasattr(args, "no_examples") and args.no_examples

    def is_windows(self):
        return self._platform == "Windows"

    def is_macos(self):
        return self._platform == "Darwin"

    def is_linux(self):
        return self._platform == "Linux"

    def exe_ext(self):
        return ".exe" if self.is_windows() else ""

    def is_pcap_available(self):
        if not self.is_windows():
            return True
        wpcap_exists = os.path.isfile(
            "C:\\Windows\\System32\\wpcap.dll"
        ) and os.path.isfile("C:\\Windows\\System32\\Npcap\\wpcap.dll")
        packet_exists = os.path.isfile(
            "C:\\Windows\\System32\\Packet.dll"
        ) and os.path.isfile("C:\\Windows\\System32\\Npcap\\Packet.dll")

        return wpcap_exists and packet_exists


def should_update_dll(config: Config, version: str) -> bool:
    if config.is_windows():
        if not os.path.isfile(
            "src/native/windows/x64/autd3capi.dll"
        ) or not os.path.isfile("tests/autd3capi.dll"):
            return True
    elif config.is_macos():
        if not os.path.isfile(
            "src/native/osx/universal/libautd3capi.dylib"
        ) or not os.path.isfile("tests/libautd3capi.dylib"):
            return True
    elif config.is_linux():
        if not os.path.isfile(
            "src/native/linux/x64/libautd3capi.so"
        ) or not os.path.isfile("tests/libautd3capi.so"):
            return True

    if not os.path.isfile("VERSION"):
        return True

    with open("VERSION", "r") as f:
        old_version = f.read().strip()

    return old_version != version


def copy_dll(config: Config):
    with open("src/AUTD3Sharp.csproj", "r") as f:
        content = f.read()
        version = re.search(r"<Version>(.*)</Version>", content).group(1).split(".")
        version = (
            ".".join(version) if version[2].endswith("rc") else ".".join(version[:3])
        )

    if not should_update_dll(config, version):
        return

    url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}/autd3-v{version}-win-x64-shared.zip"
    urllib.request.urlretrieve(url, "tmp.zip")
    shutil.unpack_archive("tmp.zip", ".")
    rm_f("tmp.zip")
    for dll in glob.glob("bin/*.dll"):
        shutil.copy(dll, "src/native/windows/x64")
        shutil.copy(dll, "tests")
    rmtree_f("bin")

    url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}/autd3-v{version}-win-aarch64-shared.zip"
    urllib.request.urlretrieve(url, "tmp.zip")
    shutil.unpack_archive("tmp.zip", ".")
    rm_f("tmp.zip")
    for dll in glob.glob("bin/*.dll"):
        shutil.copy(dll, "src/native/windows/arm")
    rmtree_f("bin")

    url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}/autd3-v{version}-macos-aarch64-shared.tar.gz"
    urllib.request.urlretrieve(url, "tmp.tar.gz")
    with tarfile.open("tmp.tar.gz", "r:gz") as tar:
        tar.extractall()
    rm_f("tmp.tar.gz")
    for dll in glob.glob("bin/*.dylib"):
        shutil.copy(dll, "src/native/osx/aarch64")
        shutil.copy(dll, "tests")
    rmtree_f("bin")

    url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}/autd3-v{version}-linux-x64-shared.tar.gz"
    urllib.request.urlretrieve(url, "tmp.tar.gz")
    with tarfile.open("tmp.tar.gz", "r:gz") as tar:
        tar.extractall()
    rm_f("tmp.tar.gz")
    for dll in glob.glob("bin/*.so"):
        shutil.copy(dll, "src/native/linux/x64")
        shutil.copy(dll, "tests")
    rmtree_f("bin")

    shutil.copyfile("LICENSE", "src/LICENSE.txt")
    with open("ThirdPartyNotice.txt", "r") as notice:
        with open("src/LICENSE.txt", "a") as f:
            f.write("\n=========================================================\n")
            f.write(notice.read())
    rmtree_f("bin")

    rmtree_f("lib")

    with open("VERSION", mode="w") as f:
        f.write(version)


def cs_build(args):
    config = Config(args)

    copy_dll(config)

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

    with working_dir("derive"):
        command = ["dotnet", "build"]
        if config.release:
            command.append("-c:Release")
        subprocess.run(command).check_returncode()

        bin_dir = "Release" if config.release else "Debug"
        subprocess.run(
            [
                "dotnet",
                "nuget",
                "add",
                "source",
                f"{os.getcwd()}/bin/{bin_dir}",
                "-n",
                "autd3sharp_local_derive",
            ]
        )

    with working_dir("src"):
        command = ["dotnet", "build"]
        if config.release:
            command.append("-c:Release")
        subprocess.run(command).check_returncode()

        bin_dir = "Release" if config.release else "Debug"
        subprocess.run(
            [
                "dotnet",
                "nuget",
                "add",
                "source",
                f"{os.getcwd()}/bin/{bin_dir}",
                "-n",
                "autd3sharp_local",
            ]
        )

    if not config.no_examples:
        info("Building examples...")
        with working_dir("example"):
            command = ["dotnet", "build"]
            if config.release:
                command.append("-c:Release")
            subprocess.run(command).check_returncode()


def cs_test(args):
    config = Config(args)

    copy_dll(config)

    with working_dir("src"):
        command = ["dotnet", "build"]
        command.append("-c:Release")
        subprocess.run(command).check_returncode()

    with working_dir("tests"):
        command = ["dotnet", "test"]
        if not config.is_pcap_available():
            command.append("--filter")
            command.append("require!=soem")
        subprocess.run(command).check_returncode()


def check_if_all_native_methods_called():
    defined_methods = set()
    pattern = re.compile("\\s*public static extern .* (AUTD.*?)\\(.*")
    for file in glob.glob("src/NativeMethods/*.cs"):
        with open(file, "r") as f:
            for line in f.readlines():
                result = pattern.match(line)
                if result:
                    defined_methods.add(result.group(1))
    defined_methods = set(filter(lambda x: not x.endswith("T4010A1"), defined_methods))
    defined_methods = set(
        filter(lambda x: not x == "AUTDSamplingConfigDivision", defined_methods)
    )

    used_methods = set()
    pattern = re.compile("NativeMethods.*?\\.(AUTD.*?)\\(")
    for file in (
        list(
            set(glob_norm("src/**/*.cs", recursive=True))
            - set(glob_norm("src/NativeMethods/*.cs", recursive=True))
        )
        + list(set(glob_norm("tests/**/*.cs", recursive=True)))
        + ["src/NativeMethods/DriverExt.cs"]
    ):
        with open(file, encoding="utf-8", mode="r") as f:
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


def cs_cov(args):
    config = Config(args)

    copy_dll(config)

    with working_dir("src"):
        command = ["dotnet", "build"]
        command.append("-c:Release")
        subprocess.run(command).check_returncode()

    check_if_all_native_methods_called()

    with working_dir("tests"):
        command = [
            "dotnet",
            "test",
            '--collect:"XPlat Code Coverage"',
            "--settings",
            "coverlet.runsettings",
        ]
        if not config.is_pcap_available():
            command.append("--filter")
            command.append("require!=soem")
        subprocess.run(command).check_returncode()

        if args.html:
            cov_res = sorted(
                glob.glob("./TestResults/*/coverage.cobertura.xml"),
                key=os.path.getmtime,
                reverse=True,
            )[0]
            command = [
                "reportgenerator",
                f"-reports:{cov_res}",
                "-targetdir:html",
                "-reporttypes:Html",
            ]
            subprocess.run(command).check_returncode()


def cs_run(args):
    args.no_examples = False
    cs_build(args)

    with working_dir("example"):
        command = ["dotnet", "run"]
        command.append("--project")
        command.append(args.target)
        if args.release:
            command.append("-c:Release")
        subprocess.run(command).check_returncode()


def cs_clear(_):
    with working_dir("."):
        rmtree_f("src/bin")
        rmtree_f("src/obj")

        rmtree_f("tests/bin")
        rm_glob_f("tests/*.dll")
        rm_glob_f("tests/*.dylib")
        rm_glob_f("tests/*.so")
        rmtree_f("tests/obj")

        rmtree_glob_f("example/**/bin")
        rmtree_glob_f("example/**/obj")


def should_update_dll_unity(config: Config, version: str) -> bool:
    if config.is_windows():
        if not os.path.isfile("unity/Assets/Plugins/x86_64/autd3capi.dll"):
            return True
    elif config.is_macos():
        if not os.path.isfile("unity/Assets/Plugins/x86_64/libautd3capi.dylib"):
            return True
    elif config.is_linux():
        if not os.path.isfile("unity/Assets/Plugins/x86_64/libautd3capi.so"):
            return True

    if not os.path.isfile("UNITY_VERSION"):
        return True

    with open("UNITY_VERSION", "r") as f:
        old_version = f.read().strip()

    return old_version != version


def copy_dll_unity(config: Config):
    with open("unity/Assets/package.json", "r") as f:
        content = f.read()
        version = re.search(r'"version": "(.*)"', content).group(1)
        version_tokens = version.split(".")
        version: str
        if version_tokens[2].endswith("rc"):
            version = ".".join(version_tokens)
        elif "-" in version_tokens[2]:
            version = (
                ".".join(version_tokens[:2]) + "." + version_tokens[2].split("-")[0]
            )
        else:
            ".".join(version_tokens[:3])

    if not should_update_dll_unity(config, version):
        return

    url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}/autd3-v{version}-win-x64-unity.zip"
    urllib.request.urlretrieve(url, "tmp.zip")
    shutil.unpack_archive("tmp.zip", ".")
    rm_f("tmp.zip")
    for dll in glob.glob("bin/*.dll"):
        shutil.copy(dll, "unity/Assets/Plugins/x86_64")
    rmtree_f("bin")

    url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}/autd3-v{version}-macos-aarch64-unity.tar.gz"
    urllib.request.urlretrieve(url, "tmp.tar.gz")
    with tarfile.open("tmp.tar.gz", "r:gz") as tar:
        tar.extractall()
    rm_f("tmp.tar.gz")
    for dll in glob.glob("bin/*.dylib"):
        shutil.copy(dll, "unity/Assets/Plugins/aarch64")
    rmtree_f("bin")

    url = f"https://github.com/shinolab/autd3-capi/releases/download/v{version}/autd3-v{version}-linux-x64-unity.tar.gz"
    urllib.request.urlretrieve(url, "tmp.tar.gz")
    with tarfile.open("tmp.tar.gz", "r:gz") as tar:
        tar.extractall()
    rm_f("tmp.tar.gz")
    for dll in glob.glob("bin/*.so"):
        shutil.copy(dll, "unity/Assets/Plugins/x86_64")
    rmtree_f("bin")

    shutil.copy("LICENSE", "unity/Assets/LICENSE.md")
    with open("ThirdPartyNotice.txt", "r") as notice:
        with open("unity/Assets/LICENSE.md", "a") as f:
            f.write("\n=========================================================\n")
            f.write(notice.read())
    shutil.copy("CHANGELOG.md", "unity/Assets/CHANGELOG.md")

    rmtree_f("lib")

    with open("UNITY_VERSION", mode="w") as f:
        f.write(version)


def unity_build(args):
    cs_build(args)

    config = Config(args)

    ignore = shutil.ignore_patterns("NativeMethods", ".vs", "bin", "obj")
    shutil.copytree(
        "src",
        "unity/Assets/Scripts",
        dirs_exist_ok=True,
        ignore=ignore,
    )
    rm_f("unity/Assets/Scripts/AUTD3Sharp.csproj")
    rm_f("unity/Assets/Scripts/AUTD3Sharp.nuspec")
    rm_f("unity/Assets/Scripts/LICENSE.txt")
    rm_f("unity/Assets/Scripts/.gitignore")
    rmtree_f("unity/Assets/Scripts/.vs")
    rmtree_f("unity/Assets/Scripts/obj")
    rmtree_f("unity/Assets/Scripts/bin")
    rmtree_f("unity/Assets/Scripts/native")
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
    configDir = config.release and "Release" or "Debug"
    for derive in glob.glob(
        f"src/obj/{configDir}/net8.0/generated/AUTD3Sharp.Derive/**/*.cs",
        recursive=True,
    ):
        shutil.copy(derive, "unity/Assets/Scripts/Derive/")

    copy_dll_unity(config)


def unity_clear(_):
    with working_dir("."):
        with working_dir("unity"):
            rmtree_f(".vs")
            rmtree_f("Library")
            rmtree_f("Logs")
            rmtree_f("obj")
            rmtree_f("Packages")
            rmtree_f("ProjectSettings")
            rmtree_f("UserSettings")
            rm_glob_f(
                "Assets/Scripts/**/*.cs",
                exclude="Assets/Scripts/NativeMethods/*.cs",
            )

        rm_glob_f("unity/Assets/Plugins/x86_64/*.dll")
        rm_glob_f("unity/Assets/Plugins/aarch64/*.dylib")
        rm_glob_f("unity/Assets/Plugins/x86_64/*.dylib")
        rm_glob_f("unity/Assets/Plugins/x86_64/*.so")


def util_update_ver(args):
    version = args.version

    with working_dir("."):
        for proj in glob.glob("example/**/*.csproj", recursive=True):
            with open(proj, "r") as f:
                content = f.read()
                content = re.sub(
                    r'"AUTD3Sharp" Version="(.*)"',
                    f'"AUTD3Sharp" Version="{version}"',
                    content,
                    flags=re.MULTILINE,
                )
            with open(proj, "w") as f:
                f.write(content)

        with open("src/AUTD3Sharp.csproj", "r") as f:
            content = f.read()
            content = re.sub(
                r"<Version>(.*)</Version>",
                f"<Version>{version}</Version>",
                content,
                flags=re.MULTILINE,
            )
        with open("src/AUTD3Sharp.csproj", "w") as f:
            f.write(content)

        with open("derive/AUTD3Sharp.Derive.csproj", encoding="UTF-8", mode="r") as f:
            content = f.read()
            content = re.sub(
                r"<Version>(.*)</Version>",
                f"<Version>{version}</Version>",
                content,
                flags=re.MULTILINE,
            )
        with open("derive/AUTD3Sharp.Derive.csproj", encoding="UTF-8", mode="w") as f:
            f.write(content)

        with open("src/AUTD3Sharp.nuspec", "r") as f:
            content = f.read()
            content = re.sub(
                r'"AUTD3Sharp\.Derive" version="(.*)"',
                f'"AUTD3Sharp.Derive" version="{version}"',
                content,
                flags=re.MULTILINE,
            )
            print(content)
        with open("src/AUTD3Sharp.nuspec", "w") as f:
            f.write(content)

        with working_dir("unity"):
            with open("Assets/package.json", "r") as f:
                content = f.read()
                content = re.sub(
                    r'"version": "(.*)"',
                    f'"version": "{version}"',
                    content,
                    flags=re.MULTILINE,
                )
            with open("Assets/package.json", "w") as f:
                f.write(content)


def util_gen_wrapper(_):
    fetch_submodule()
    generate_wrapper()


def command_help(args):
    print(parser.parse_args([args.command, "--help"]))


if __name__ == "__main__":
    with working_dir(os.path.dirname(os.path.abspath(__file__))):
        parser = argparse.ArgumentParser(description="autd3 library build script")
        subparsers = parser.add_subparsers()

        # cs
        parser_cs = subparsers.add_parser("cs", help="see `cs -h`")
        subparsers_cs = parser_cs.add_subparsers()

        # cs build
        parser_cs_build = subparsers_cs.add_parser("build", help="see `cs build -h`")
        parser_cs_build.add_argument(
            "--release", action="store_true", help="release build"
        )
        parser_cs_build.add_argument(
            "--no-examples", action="store_true", help="skip building examples"
        )
        parser_cs_build.set_defaults(handler=cs_build)

        # cs test
        parser_cs_test = subparsers_cs.add_parser("test", help="see `cs test -h`")
        parser_cs_test.add_argument(
            "--release", action="store_true", help="release build"
        )
        parser_cs_test.set_defaults(handler=cs_test)

        # cs cov
        parser_cs_cov = subparsers_cs.add_parser("cov", help="see `cs cov -h`")
        parser_cs_cov.add_argument(
            "--release", action="store_true", help="release build"
        )
        parser_cs_cov.add_argument(
            "--html", action="store_true", help="generate html report", default=False
        )
        parser_cs_cov.set_defaults(handler=cs_cov)

        # cs run
        parser_cs_run = subparsers_cs.add_parser("run", help="see `cs run -h`")
        parser_cs_run.add_argument("target", help="binary target")
        parser_cs_run.add_argument(
            "--release", action="store_true", help="release build"
        )
        parser_cs_run.set_defaults(handler=cs_run)

        # cs clear
        parser_cs_clear = subparsers_cs.add_parser("clear", help="see `cs clear -h`")
        parser_cs_clear.set_defaults(handler=cs_clear)

        # unity
        parser_unity = subparsers.add_parser("unity", help="see `unity -h`")
        subparsers_unity = parser_unity.add_subparsers()

        # unity build
        parser_unity_build = subparsers_unity.add_parser(
            "build", help="see `unity build -h`"
        )
        parser_unity_build.add_argument(
            "--release", action="store_true", help="release build"
        )
        parser_unity_build.set_defaults(handler=unity_build)

        # unity clear
        parser_unity_clear = subparsers_unity.add_parser(
            "clear", help="see `unity clear -h`"
        )
        parser_unity_clear.set_defaults(handler=unity_clear)

        # util
        parser_util = subparsers.add_parser("util", help="see `util -h`")
        subparsers_util = parser_util.add_subparsers()

        # util update version
        parser_util_upver = subparsers_util.add_parser(
            "upver", help="see `util upver -h`"
        )
        parser_util_upver.add_argument("version", help="version")
        parser_util_upver.set_defaults(handler=util_update_ver)

        # util update version
        parser_util_gen_wrap = subparsers_util.add_parser(
            "gen_wrap", help="see `util gen_wrap -h`"
        )
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

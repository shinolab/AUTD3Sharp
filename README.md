# autd3sharp


![build](https://github.com/shinolab/AUTD3Sharp/workflows/build/badge.svg)
[![codecov](https://codecov.io/gh/shinolab/AUTD3Sharp/graph/badge.svg)](https://codecov.io/gh/shinolab/AUTD3Sharp)
[![NuGet stable version](https://img.shields.io/nuget/v/autd3sharp)](https://nuget.org/packages/AUTD3Sharp)
[![autd3-unity](https://img.shields.io/npm/v/com.shinolab.autd3?label=autd3-unity)](https://www.npmjs.com/package/com.shinolab.autd3)

[autd3](https://github.com/shinolab/autd3-rs) wrapper for .Net Standard 2.1

## Install

* Please install using NuGet
    https://www.nuget.org/packages/autd3sharp

## Example

* Please refer to [C# example](./example/cs) or [F# example](./example/fs).

* If you are using Linux/macOS, you may need to run as root (e.g. `sudo dotnet run`).
    * On Ubuntu 20.04, you may need to specify runtime (e.g. `sudo dotnet run -r ubuntu-x64`).

## Unity

* Please install via Unity Package Manager

    1. From the menu bar, select `Edit > Project settings > Package Manager`.
    1. Add the following `Scoped registory`.
        - Name : shinolab
        - URL : https://registry.npmjs.com
        - Scope(s): com.shinolab
    1. From the menu bar, select `Window > Package Manager`.
    1. Select `My Registries` from the drop-down menu in the upper left corner.
    1. Install `autd3-unity` package.

## LICENSE

See [LICENSE](./LICENSE) and [ThirdPartyNotice](./ThirdPartyNotice.txt).

# Author

Shun Suzuki 2022-2024

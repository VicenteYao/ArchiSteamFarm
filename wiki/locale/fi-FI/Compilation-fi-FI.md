# Suorittaminen

Compilation is the process of creating executable file. This is what you want to do if you want to add your own changes to ASF, or if you for whatever reason don't trust executable files provided in official **[releases](https://github.com/JustArchiNET/ArchiSteamFarm/releases)**. If you're user and not a developer, most likely you want to use already precompiled binaries, but if you'd like to use your own ones, or learn something new, continue reading.

ASF can be compiled on any currently supported platform, as long as you have all needed tools to do so.

* * *

## .NET Core SDK

Regardless of platform, you need full .NET Core SDK (not just runtime) in order to compile ASF. Installation instructions can be found on **[.NET Core installation page](https://dotnet.microsoft.com/download)**. You need to install appropriate .NET Core SDK version for your OS. After successful installation, `dotnet` command should be working and operative. You can verify if it works with `dotnet --info`. Also ensure that your .NET Core SDK matches ASF **[runtime requirements](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements)**.

* * *

## Suorittaminen

Assuming you have .NET Core SDK operative and in appropriate version, simply navigate to source ASF directory (cloned or downloaded and unpacked ASF repo) and execute:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/generic"
```

If you're using Linux/OS X, you can instead use `cc.sh` script which will do the same, in a bit more complex manner.

If compilation ended successfully, you can find your ASF in `source` flavour in `out/generic` directory. This is the same as official `generic` ASF build, but it has forced `UpdateChannel` and `UpdatePeriod` of `0`, which is appropriate for self-builds.

### OS-specific

You can also generate OS-specific .NET Core package if you have a specific need. In general you shouldn't do that because you've just compiled `generic` flavour that you can run with your already-installed .NET Core runtime that you've used for the compilation in the first place, but just in case you want to:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/linux-x64" -r "linux-x64"
```

Of course, replace `linux-x64` with OS-architecture that you want to target, such as `win-x64`. This build will also have updates disabled.

### .NET Framework

In a very rare case when you'd want to build `generic-netf` package, you can change target framework from `netcoreapp3.1` to `net48`. Keep in mind that you'll need appropriate **[.NET Framework](https://dotnet.microsoft.com/download/visual-studio-sdks)** developer pack for compiling `netf` variant, in addition to .NET Core SDK, so the below will work only on Windows:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "net48" -o "out/generic-netf"
```

In case of being unable to install .NET Framework or even .NET Core SDK itself (e.g. because of building on `linux-x86` with `mono`), you can call `msbuild` directly. You'll also need to specify `ASFNetFramework` manually, as ASF by default disables netf build on non-Windows platforms:

```shell
msbuild /m /r /t:Publish /p:Configuration=Release /p:TargetFramework=net48 /p:PublishDir=out/generic-netf /p:ASFNetFramework=true ArchiSteamFarm
```

* * *

## Development

If you'd like to edit ASF code, you can use any .NET Core compatible IDE for that purpose, although even that is optional, since you can as well edit with a notepad and compile with `dotnet` command described above. Still, for Windows we recommend **[latest Visual Studio](https://visualstudio.microsoft.com/downloads)** (free community version is more than enough). We also suggest to use it together with **[ReSharper](https://www.jetbrains.com/resharper)** (optionally), although it's not a free product.

If you'd like to work with ASF code on Linux/OS X instead, we recommend **[latest Visual Studio Code](https://code.visualstudio.com/download)**. It's not as rich as classic Visual Studio, but it's good enough.

Of course all suggestions above are only recommendations, you can use whatever you want to, it comes down to `dotnet build` command anyway. We use Visual Studio + ReSharper for ASF development, with a small part of third-party `tools` that you can find in the repo.

* * *

## Tags

`master` branch is not guaranteed to be in a state that allows successful compilation or flawless ASF execution in the first place, since it's development branch just like stated in our **[release cycle](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle)**. If you want to compile or reference ASF from source, then you should use appropriate **[tag](https://github.com/JustArchiNET/ArchiSteamFarm/tags)** for that purpose, which guarantees at least successful compilation, and very likely also flawless execution (if build was marked as stable release). In order to check the current "health" of the tree, you can use our CIs - **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** or **[Travis](https://travis-ci.com/JustArchiNET/ArchiSteamFarm)**.

* * *

## Official releases

Official ASF releases are compiled by **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** on Windows, with latest .NET Core SDK that matches ASF **[runtime requirements](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements)**. After passing tests, all packages are deployed on GitHub. This also guarantees transparency, since AppVeyor always uses official public source for all builds, and you can compare checksums of AppVeyor artifacts with GitHub assets. ASF developers do not compile or publish builds themselves, except for private development process and debugging.
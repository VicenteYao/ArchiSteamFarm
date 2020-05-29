# Khả năng tương thích

ASF is a C# application that is running on .NET Core platform. This means that ASF is not compiled directly into **[machine code](https://en.wikipedia.org/wiki/Machine_code)** that is running on your CPU, but into **[CIL](https://en.wikipedia.org/wiki/Common_Intermediate_Language)** that requires a CIL-compatible runtime for executing it.

This approach has gigantic amount of advantages, as CIL is platform-independent, which is why ASF can run natively on many available OSes, especially Windows, Linux and OS X. There is not only no emulation needed, but also support for all platform-related and hardware-related optimizations, such as CPU SSE instructions. Thanks to that, ASF can achieve superior performance and optimization, while still offering a perfect compatibility and reliability.

This also means that ASF has **no specific OS requirement**, because it requires working **runtime** on that OS and not OS itself. As long as that runtime is executing ASF code properly, it does not matter whether underlying OS is Windows, Linux, OS X, BSD, Sony Playstation 4, Nintendo Wii or your toaster - as long as there is **[.NET Core for it](https://github.com/dotnet/core-setup#daily-builds)**, there is **[ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** for it.

However, regardless of where you run ASF, you must ensure that your target platform has **[.NET Core prerequisites](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)** installed. Those are low-level libraries required for proper runtime functionality and absolutely core for ASF to work in the first place. Very likely you can have some of them (or even all) already installed.

* * *

## Multiple instances

ASF is compatible with running multiple instances of the process on the same machine. The instances can be completely standalone or derived from the same binary location (in which case, you want to run them with different `--path` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**).

When running multiple instances from the same binary, keep in mind that you should typically disable auto-updates in all of their configs, as there is no synchronization between them in regards to auto-updates. If you'd like to keep having auto-updates enabled, we recommend standalone instances, but you can still make updates work, as long as you can ensure that all other ASF instances are closed.

ASF will do its best to maintain a minimum amount of OS-wide, cross-process communication with other ASF instances. This includes ASF checking its configuration directory against other instances, as well as sharing core process-wide limiters configured with `*LimiterDelay` **[global config properties](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config)**, ensuring that running multiple ASF instances will not cause a possibility to run into a rate-limiting issue. In regards to technical aspects, Windows platforms use native OS-wide named semaphores for this purpose, Unix platforms use fallback mechanism of custom ASF file-based locks created in `/tmp/ASF` directory.

It's not required for running ASF instances to share the same `*LimiterDelay` properties, they can use different values, as each ASF will add its own configured delay to the release time after acquiring the lock. If the configured `*LimiterDelay` is set to `0`, ASF instance will entirely skip waiting for the lock of given resource that is shared with other instances (that could potentially still maintain a shared lock with each other). When set to any other value, ASF will properly synchronize with other ASF instances and wait for its turn, then release the lock after configured delay, allowing other instances to continue.

ASF takes into account `WebProxy` setting when deciding about shared scope, which means that two ASF instances using different `WebProxy` configurations will not share their limiters with each other. This is implemented in order to allow `WebProxy` setups to operate without excessive delays, as expected from different network interfaces. This should be good enough for majority of use cases, however, if you have a specific custom setup in which you're e.g. routing requests yourself in a different way, you can specify network group yourself through `--network-group` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**, which will allow you to declare ASF group that will be synchronized with this instance. Keep in mind that custom network groups are used exclusively, which means that ASF will no longer use `WebProxy` for determining the right group, as you're in charge of grouping in this case.

* * *

## ASF packaging

ASF comes in 2 main flavours - generic package and OS-specific. Functionality-wise both packages are exactly the same, they're both also capable of automatically updating themselves. The only difference between them is whether or not ASF **generic** package also comes with **OS-specific** runtime to power it.

* * *

### Generic

Generic package is platform-agnostic build that doesn't include any machine-specific code. This setup requires from you to have .NET Core runtime already installed on your OS **in appropriate version**. We all know how troublesome it is to keep dependencies up-to-date, therefore this package is here mainly for people that **already use** .NET Core and don't want to duplicate their runtime solely for ASF if they can make use of what they have installed already. Generic package also allows you to run ASF **anywhere where you can obtain working implementation of .NET Core runtime**, regardless if there exists OS-specific ASF build for it, or not.

It's not recommended to use generic flavour if you're casual or even advanced user that just wants to make ASF work and not dig into .NET Core technical details. In other words - if you know what this is, you can use it, otherwise it's much better to use OS-specific package explained below.

#### .NET Framework package

In addition to generic package mentioned above, there is also `generic-netf` package which is built on top of .NET Framework (and not .NET Core). This package is a legacy variant that provides missing compatibility known from ASF V2 times, and can be run e.g. with **[Mono](https://www.mono-project.com)**, while .NET Core `generic` package can't as of today.

In general you should **avoid this package as much as possible**, as majority of operating systems and setups are perfectly (and much better) supported with `generic` package mentioned above. In fact, this package makes sense to be used only on platforms that lack working .NET Core runtime, while having working Mono implementation. Examples of such platforms include `linux-x86` (32-bit i386/i686 linux), as well as `linux-armel` (ARMv6 boards found e.g. in Raspberry Pi 0 & 1), all of which do not have official working .NET Core runtime as of today.

As the time goes on with more platforms being supported by .NET Core and less compatibility between .NET Framework and .NET Core, `generic-netf` package will be entirely replaced with `generic` one in the future. Please refrain from using it if you can use any .NET Core package instead, as `generic-netf` is missing a lot of functionality and compatibility compared to .NET Core versions, and it'll be only less functional as the time goes on. We offer support for this package **only** on machines that can't use `generic` variant above (e.g. `linux-x86`), and only with up-to-date runtime (e.g. latest Mono).

* * *

### OS-specific

OS-specific package, apart from managed code included in generic package, also includes native code for given platform. In other words, OS-specific package **already includes proper .NET Core runtime inside**, which allows you to entirely skip the whole installation mess and just launch ASF directly. OS-specific package, as you can guess from the name, is OS-specific and every OS requires its own version - for example Windows requires PE32+ `ArchiSteamFarm.exe` binary while Linux works with Unix ELF `ArchiSteamFarm` binary. As you may know, those two types are not compatible with each other.

ASF is currently available in following OS-specific variants:

- `win-x64` works on 64-bit Windows OSes. This includes Windows 7 (SP1+), 8.1, 10, Server 2008 R2 (SP1+), 2012, 2012 R2, 2016, as well as future versions.
- `linux-arm` works on 32-bit ARM-based (ARMv7+) GNU/Linux OSes. This includes platforms such as Raspberry Pi 2 (and newer) with all GNU/Linux OSes available for them (such as Raspbian), in current and future versions. This variant will not work with older ARM architectures, such as ARMv6 found in Raspberry Pi 0 & 1, it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-arm64` works on 64-bit ARM-based (ARMv8+) GNU/Linux OSes. This includes platforms such as Raspberry Pi 3 (and newer) with all AArch64 GNU/Linux OSes available for them (such as Debian), in current and future versions. This variant will not work with 32-bit OSes that do not have required 64-bit libraries available (such as Raspbian), it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-x64` works on 64-bit GNU/Linux OSes. This includes Alpine, CentOS/Fedora/RHEL, Debian/Ubuntu/Linux Mint, OpenSUSE/SLES and many other ones, including their derivatives, in current and future versions.
- `osx-x64` works on 64-bit OS X OSes. This includes 10.13, as well as future versions.

Of course, even if you don't have OS-specific package available for your OS-architecture combination, you can always install appropriate .NET Core runtime yourself and run generic ASF flavour, which is also the main reason why it exists in the first place. Generic ASF build is platform-agnostic and will run on any platform that has a working .NET Core runtime. This is important to note - ASF requires .NET Core runtime, not some specific OS or architecture. For example, if you're running 32-bit Windows then despite of no dedicated `win-x86` ASF version, you can still install .NET Core SDK in `win-x86` version and run generic ASF just fine. We simply can't target every OS-architecture combination that exists and is used by somebody, so we have to draw a line somewhere. x86 is a good example of that line, as it's obsolete architecture since at least 2004.

For a complete list of all supported platforms and OSes by .NET Core 3.1, visit **[release notes](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md)**.

* * *

## Runtime requirements

If you're using OS-specific package then you don't need to worry about runtime requirements, because ASF always ships with required and up-to-date runtime that will work properly as long as you have **[.NET Core prerequisites](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)** installed and up-to-date. In other words, **you don't need to install .NET Core runtime or SDK**, as OS-specific builds require only native OS dependencies (prerequisites) and nothing else.

However, if you're trying to run **generic** ASF package then you must ensure that your .NET Core runtime supports platform required by ASF.

ASF as a program is targeting **.NET Core 3.1** (`netcoreapp3.1`) right now, but it may target newer platform in the future. `netcoreapp3.1` is supported since 3.1.100 SDK (3.1.0 runtime), although ASF is configured to target **latest runtime at the moment of compilation**, so you should ensure that you have **[latest SDK](https://dotnet.microsoft.com/download)** (or at least runtime) available for your machine. Generic ASF variant may refuse to launch if your runtime is older than the minimum (target) one known during compilation.

If in doubt, check what our **[continuous integration uses](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** for compiling and deploying ASF releases on GitHub. You can find `dotnet --info` output on top of each build.

* * *

## Các vấn đề

### Various lock-related issues running ASF on Linux VPS with OpenVZ virtualization

OpenVZ kernel is usually based on a very old Linux kernel version (2.6) which seems incompatible with latest .NET Core runtime. If you're trying to run ASF in such environment, you may encounter various lock-related issues, usually in form of process freeze. See related CoreCLR issue: https://github.com/dotnet/coreclr/issues/26873

Our recommendation is to ditch OpenVZ in favour of much better virtualization solutions, such as KVM. The issue described here is indeed a .NET Core runtime bug that is supposed to be **[fixed](https://github.com/dotnet/coreclr/pull/26912)** in the next .NET Core runtime upgrade (3.1 service patch), but there is no actual timeframe for it yet. If you're unable to move to better virtualization solution, you can consider running `generic-netf` ASF variant with `mono`, at least until new runtime version is released.

For more advanced users that are not afraid of their Linux OS, there is a **[much better workaround](https://github.com/dotnet/coreclr/issues/26873#issuecomment-559854433)** available which makes it possible to run latest .NET Core ASF without running into this issue.
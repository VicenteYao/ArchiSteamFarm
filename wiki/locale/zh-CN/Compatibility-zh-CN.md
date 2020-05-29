# 兼容性

ASF 是一个用 C# 语言编写并运行在 .NET Core 平台上的应用程序。 这意味着 ASF 并非直接被编译为可供 CPU 执行的&#8203;**[机器码](https://en.wikipedia.org/wiki/Machine_code)**，而是被编译为 **[CIL](https://en.wikipedia.org/wiki/Common_Intermediate_Language)**，一种需要相应的运行环境才能执行的语言。

这种方法能够带来巨大的方便。由于 CIL 是跨平台的，这使得 ASF 天然能够运行在许多可供使用的操作系统上，特别是 Windows、Linux 和 macOS 这三个系统。ASF 不仅不需要通过模拟运行，同时所有对于系统及其相关硬件的优化也对其有效。 基于此，ASF 在表现出卓越的性能以及优化的同时，仍然能提供完美的兼容性和可靠性。

这也意味着运行 ASF **没有特定的操作系统要求**，因为它需要的只是运行于操作系统上的**运行环境**而非操作系统本身。 只要运行环境能够正确地执行 ASF 的代码，底层系统是 Windows、Linux、macOS 还是 BSD，是运行在 Sony Playstation 4、Nintendo Wii 还是您的烤面包机上都无所谓。有供它运行的 **[.NET Core](https://github.com/dotnet/core-setup#daily-builds)** 就有能正常运行的 **[ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**。

但是，无论您想要在哪个平台上运行 ASF，您必须确保该平台安装了 **[.NET Core 的依赖项](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)**。 这些都是确保运行环境功能正常的底层库，也是确保 ASF 能够第一时间工作的绝对核心。 通常情况下，部分库（甚至全部）很有可能已经安装在系统内。

* * *

## 多实例

ASF 兼容在同一台机器上运行多个进程实例。 实例可以是完全独立的，或是派生于同一个二进制文件（如果您需要它们在不同路径下运行，可以使用 `--path` **[命令行参数](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**）。

请注意，在通常情况下，当使用同一份二进制文件运行不同实例时应该在所有实例的配置文件内禁用自动更新，因为它们之间不会同步与自动更新有关的信息。 如果您仍希望启用自动更新，我们建议您使用独立的实例，但只要您能确保所有其他 ASF 实例已关闭，自动更新就仍然可以正常工作。

ASF 会尽全力减少操作系统层面的与其他 ASF 实例的跨进程通信。 这包括 ASF 会读取其他实例的配置文件目录，并且共享由 `*LimiterDelay` **[全局配置属性](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config)**&#8203;配置的进程级限制，确保运行多个 ASF 实例不会导致频率限制问题。 从技术角度来看，Windows 平台会为此使用原生的操作系统级信号量，Unix 平台会回退到 ASF 自定义的基于文件的锁机制，这些锁文件会被放在 `/tmp/ASF` 目录下。

运行多个 ASF 实例不需要它们共享相同的 `*LimiterDelay` 属性，它们可以设置不同的值，因为每个 ASF 都会在获得锁之后将自己配置的延迟添加到释放时间。 如果配置的 `*LimiterDelay` 设置为 `0`，ASF 实例将完全跳过等待其他实例释放资源的锁（它们之间可能仍然会维护一个共享锁）。 当设置为其他任何值时，ASF 将会与其他实例同步此值，并等待自己获得锁，然后会在预先配置的延迟时间之后释放锁，使其他实例继续工作。

ASF 在决定共享范围时会考虑到 `WebProxy` 设置，即使用不同 `WebProxy` 的 ASF 实例之间不会采用同一个限制。 实现此功能是为了让 `WebProxy` 设置不会导致过大的操作延迟，符合使用不同网络接口的预期。 对于大多数情况，这应该足够了，然而，如果您的方案使用自定义的机制，例如使用其他方式手动路由请求，您可以通过 `--network-group` **[命令行参数](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-zh-CN)**&#8203;指定网络组，使您指定 ASF 需要与同一个组内的实例同步。 请注意，设置自定义网络组选项后，ASF 就不再根据 `WebProxy` 判断所需的组，因为此时由您自己管理分组。

* * *

## ASF 打包

ASF 有两种主要的打包方式——Generic 包以及 OS-specific 包（操作系统包）。 从功能上来讲，这两种包是完全一样的，都能够自动进行更新。 唯一的区别就是 **Generic** 包中不包含 **OS-specific** 包内所具有的能使 ASF 运行的环境。

* * *

### Generic

Generic 包是一个与平台无关的版本，所以它不包含特定于计算机的代码。 所以使用这个包需要您的操作系统中已经安装有**合适版本**的 .NET Core 运行时环境。 我们都知道保证依赖始终是最新的这件事是十分令人烦恼的，所以这个包主要面向那些**已经在使用** .NET Core，不想为了 ASF 对已有环境做单独复制的人。 Generic 包还允许您将 ASF 运行在**任何可以获得正常工作的 .NET Core 环境的机器上**，不需要担心是否存在相应的 OS-specific 包。

对于一般用户甚至是高级用户，如果只是想要运行 ASF 而不想要深入了解 . NET core 的技术细节，不推荐使用 Generic 包。 也就是说，如果您明白以上所讲的内容，那您就可以使用它，不然下面所介绍的 OS-specific 包才是最合适的。

#### .NET 框架包

除了上面提到的 Generic 包，我们也提供 `Generic-netf` 包，它基于 .NET 框架（而非 .NET Core）。 该包是一种旧式包，它补全了从 ASF V2 时代即已知的兼容性缺失，并且可以使用 **[Mono](https://www.mono-project.com)** 运行，而来自 .NET Core 的 `Generic` 包则无法用于 Mono。

通常，您应该**尽量避免使用此程序包**，因为大多数操作系统都完全（并且更好地）支持上面提到的 `Generic` 包。 事实上，这个软件包只适用于缺乏 .NET Core 运行时环境，但能够运行 Mono 的平台。 此类平台包括 `linux-x86`（32 位 i386/i686 Linux），以及 `linux-armel`（Raspberry Pi 0 & 1 等开发板所用的 ARMv6），目前官方尚未提供这些平台可用的 .NET Core 运行时环境。

随着时间的推移，.NET Core 会支持更多平台，而 .NET Framework 和 .NET Core 之间会更加不兼容，`Generic-netf` 包将会在未来完全被 `Generic` 包取代。 如果您可以使用任何 .NET Core 软件包，就不要使用框架包，因为 `Generic-netf` 与 .NET Core 版本相比缺少许多功能和兼容性，并且功能会随着时间的推移变少。 我们**仅**对无法使用 `Generic` 包的平台提供此版本的支持（例如 `linux-x86`），并且也仅基于最新版本的运行时环境（例如最新版 Mono）提供支持。

* * *

### OS-specific（特定操作系统）

除了 Generic 包中包含的托管代码之外，OS-specific 包还包括指定平台的本机代码。 换句话说，OS-specific 包**内部已经包含了可用的 .NET Core 运行时环境**，使您可以跳过麻烦的安装过程，直接启动 ASF。 OS-specific 包，顾名思义，是针对不同操作系统的，每种操作系统都需要其特定的版本——例如 Windows 需要 PE32+ `ArchiSteamFarm.exe` 二进制文件，而 Linux 则需要 Unix ELF `ArchiSteamFarm` 二进制文件。 您可能知道，这两种类型之间是完全不兼容的。

ASF 目前提供以下几种 OS-specific 包：

- `win-x64`，支持 64 位 Windows 操作系统。 包括 Windows 7（SP1+）、8.1、10、Server 2008 R2（SP1+）、2012、2012 R2、2016，和未来的版本。
- `linux-arm`，支持 32 位基于 ARM（ARMv7+）的 GNU/Linux 操作系统。 包括所有支持当前和未来版本 GNU/Linux 操作系统（例如 Raspbian）的平台，例如 Raspberry Pi 2（或更新版本）。 此包不支持更早的 ARM 架构，例如 Raspberry Pi 0 & 1 使用的 ARMv6，也不支持未实现所需 GNU/Linux 环境的操作系统（例如 Android）。
- `linux-arm64`，支持 64 位基于 ARM（ARMv8+）的 GNU/Linux 操作系统。 包括所有支持当前和未来版本 AArch64 GNU/Linux 操作系统（例如 Debian）的平台，例如 Raspberry Pi 3（或更新版本）。 此包不支持 32 位操作系统（例如 Raspbian），因为它们缺少所需的 64 位库，也不支持未实现所需 GNU/Linux 环境的操作系统（例如 Android）。
- `linux-x64` 支持 64 位 GNU/Linux 操作系统。 包括 Alpine、CentOS/Fedora/RHEL、Debian/Ubuntu/Linux Mint、OpenSUSE/SLES 等很多操作系统以及它们的衍生版的当前和未来版本。
- `osx-x64` 支持 64 位 macOS 操作系统。 包括 10.13 及更新版本。

当然，即使没有适合您操作系统及架构的 OS-specific 包，您也可以手动安装适当的 .NET Core 运行时环境并运行 Generic ASF 包，这也是这个包存在的主要原因。 Generic ASF 包与平台无关，可在任何具有可用 .NET Core 运行时环境的平台上运行。 需要注意——ASF 需要的是 .NET Core 运行时环境，而不是特定的操作系统或架构。 例如，如果您使用的是 32 位 Windows，但 ASF 没有 `win-x86` 版本，您仍然可以安装 `win-x86` 版本的 .NET Core SDK，然后运行 Generic 版本的 ASF。 我们无法为所有操作系统和架构组合都生成一份可执行文件，所以我们为此画下一道分隔线。 x86 就是这条线之一，因为这种架构自 2004 年开始就过时了。

您可以访问&#8203;**[发行说明](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md)**&#8203;查看完整的 .NET Core 3.1 支持的平台与操作系统列表。

* * *

## 运行时环境需求

如果您正在使用 OS-specific 包，那么您不必担心运行时需求，因为 ASF 总会将所需的最新运行时环境打包在一起，只要您已安装并更新 **[.NET Core 依赖项](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)**，就能够正常运行。 换句话说，**您不需要安装 .NET Core 运行时环境或 SDK**，因为 OS-specific 版本只需要本机已安装对应操作系统的依赖项，而不需要其他项目。

但如果您使用 **Generic** 包，则必须保证已安装 ASF 所需的对应平台的 .NET Core 运行时环境。

ASF 目前指向的构建目标是 **.NET Core 3.1**（`netcoreapp3.1`），但在未来可能会指向更高版本。 `netcoreapp3.1` 自 3.1.100 SDK（3.1.0 运行时环境）以来就受到支持，但 ASF 以**编译时最新版本的运行时环境**为构建目标，所以您应该确保您的机器上有&#8203;**[最新版 SDK](https://dotnet.microsoft.com/download)**（或至少有运行时环境）。 如果您的运行时环境版本低于编译时的已知最低目标版本，Generic ASF 包将会拒绝启动。

如有疑问，您可以访问我们用于编译并在 GitHub 上部署新版本的 **[CI](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)**。 您可以在每个构建的顶端看到 `dotnet --info` 的输出。

* * *

## 异常情况

### 在 OpenVZ 虚拟化的 Linux VPS 上运行 ASF 出现的各种与锁相关的问题

OpenVZ 内核通常基于非常旧的 Linux 内核版本（2.6），此版本似乎不兼容最新的 .NET Core 运行时环境。 如果您尝试在这种环境下运行 ASF，就可能会遇到各种与锁相关的问题，并通常表现为程序卡死。 参见相关的 CoreCLR Issue：https://github.com/dotnet/coreclr/issues/26873

我们的建议是放弃 OpenVZ 架构，转向更好的虚拟化方案，例如 KVM。 这里提到的问题的确是 .NET Core 运行时环境的一个漏洞，并且应当在下个版本 .NET Core 运行时环境（3.1 服务补丁）中被&#8203;**[修复](https://github.com/dotnet/coreclr/pull/26912)**，但暂无明确的时间。 如果无法迁移到更好的虚拟化方案，则在此问题被修复之前，您可以考虑使用 `mono` 运行 ASF 的 `generic-netf` 包。

对于能够掌控 Linux 操作系统的高级用户，这里有一个&#8203;**[更好的解决方案](https://github.com/dotnet/coreclr/issues/26873#issuecomment-559854433)**，能够运行最新的 .NET Core ASF 而不会发生此问题。
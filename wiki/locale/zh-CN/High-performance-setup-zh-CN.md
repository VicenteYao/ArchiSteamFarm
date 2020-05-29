# 高性能方案

这篇文档与&#8203;**[低内存方案](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-zh-CN)**&#8203;完全相反，如果您愿意增加内存开销和一些潜在的成本，以增强 ASF 的性能（CPU 速度方面），请阅读以下内容。

* * *

在一般情况的平衡调整中，ASF 已经尝试优先考虑性能部分，因此您没有很多优化 ASF 性能的余地，尽管仍然有一些选项可以做适当的调整。 但是，请记住，我们没有默认启用这些选项，这意味着它们并不能在大多数情况下保证平衡，因此您应该自己决定是否可以接受它们带来的内存开销。

* * *

## 运行时环境调优（高级）

以下技巧**会造成严重的内存消耗**，应谨慎使用。

.NET Core 运行时环境允许您以多种方式&#8203;**[调整垃圾回收](https://docs.microsoft.com/zh-cn/dotnet/core/run-time-config/garbage-collector)**，根据需要高效地优化 GC 过程。

应用这些设置的推荐方式是设置 `COMPlus_` 环境变量。 当然，您也可以使用其他方法，例如 `runtimeconfig.json`，但这种方法无法调整某些选项，并且 ASF 还会在每次更新时替换掉您的自定义 `runtimeconfig.json`，因此我们推荐使用环境变量，这样您就可以在运行程序之前轻松设置。

请阅读文档了解所有您能使用的属性，我们也会在此说明（在我们眼中）最重要的几个：

### `gcServer`

> 配置应用程序是使用工作站垃圾回收还是服务器垃圾回收。

您可以在&#8203;**[垃圾回收的基本知识](https://docs.microsoft.com/zh-cn/dotnet/standard/garbage-collection/fundamentals)**&#8203;了解服务器 GC 的详情。

ASF 默认使用工作站 GC。 这主要是因为其在内存消耗和性能之间的良好平衡，这对于运行少数机器人来说已经足够了，因为通常单个并发后台 GC 线程足以快速地处理所有由 ASF 分配的内存。

但是，现在我们通常有很多 CPU 核心，ASF 也因此受益，在每个 CPU vCore 中都有一个专用的 GC 线程。 这可以极大地提高 ASF 执行繁重任务时的性能，例如解析徽章或库存页面，因为每个 CPU vCore 都可以提供帮助，而不是仅仅 2 个线程（主线程和 GC 线程）。 对于具有 3 个或更多 CPU vCore 的计算机，建议使用服务器 GC，如果您的计算机只有 1 个 CPU vCore，则会自动强制使用工作站 GC ，如果您只有 2 个，那么您可以考虑尝试两者（结果可能会有所不同）。

仅仅启用服务器 GC 本身不会导致增加很大的内存开销，但它具有更大的代数，因此会更少将内存返回给操作系统。 您可能会发现自己处于一个尴尬的情况，服务器 GC 显著提高了性能，并且您希望继续使用它，但同时您无法承受使用它带来的巨大内存消耗。 幸运的是，将 **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-zh-CN#gclatencylevel)** 属性设置为 `0` 是一个两者兼顾的设置，服务器 GC 仍然启用，但会限制代数并且更关注内存消耗。 或者，您也许可以尝试另一个属性，**[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-zh-CN#gcheaphardlimitpercent)**，或者同时启用这两个选项。

但是，如果内存对您来说不是问题（因为 GC 仍会根据您的可用内存自行调整），那么最好完全不更改这些属性，从而达到最佳性能。

* * *

您可以通过 `COMPlus_` 环境变量启用所有 GC 选项。 例如，在 Linux 上（Shell）：

```shell
export COMPlus_gcServer=1

./ArchiSteamFarm # 针对操作系统包
```

或者在 Windows 上（Powershell）：

```powershell
$Env:COMPlus_gcServer=1

.\ArchiSteamFarm.exe # 针对操作系统包
```

* * *

## 建议的优化

- 确保您的 `OptimizationMode` 属性设置为默认值 `MaxPerformance`。 这是最重要的设置，因为使用 `MinMemoryUsage` 值会对性能产生巨大影响。
- 启用服务器 GC。 与工作站 GC 相比，您可以通过瞬间增加的显著内存消耗确认服务器 GC 被启用。
- 如果您无法负担如此高的内存开销，可以考虑调整 **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-zh-CN#gclatencylevel)** 和/或 **[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-zh-CN#gcheaphardlimitpercent)** 以求两全。 但是，如果您可以负担得起这样的内存消耗，那么最好将其保持默认状态——服务器 GC 在运行时已经进行了自我优化，并且在您的操作系统真正需要时使用更少的内存。

如果您已启用服务器 GC 并保留其他各个属性的默认设置，那么即使您启用成百上千个机器人，ASF 仍会有出色的性能。 CPU 不会再成为瓶颈，因为 ASF 能够在需要时发挥您的 CPU 的全部能力，从而缩短操作所需时间。 若要进一步优化就只能升级 CPU 和内存了。
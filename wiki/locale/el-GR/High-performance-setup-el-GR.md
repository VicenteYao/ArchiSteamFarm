# High-performance setup

This is exact opposite of **[low-memory setup](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup)** and typically you want to follow those tips if you want to further increase ASF performance (in terms of CPU speed), for potential cost of increased memory usage.

* * *

ASF already tries to prefer performance when it comes to general balanced tuning, therefore there is not a lot you can do to further increase its performance, although you're not completely out of options either. However, keep in mind that those options are not enabled by default, which means that they're not good enough to consider them balanced for majority of usages, therefore you should decide yourself if memory increase brought by them is acceptable for you.

* * *

## Runtime tuning (advanced)

Below tricks **involve serious memory increase** and should be used with caution.

.NET Core runtime allows you to **[tweak garbage collector](https://docs.microsoft.com/dotnet/core/run-time-config/garbage-collector)** in a lot of ways, effectively fine-tuning the GC process according to your needs.

The recommended way of applying those settings is through `COMPlus_` environment properties. Of course, you could also use other methods, e.g. `runtimeconfig.json`, but some settings are impossible to be set this way, and on top of that ASF will replace your custom `runtimeconfig.json` with its own on the next update, therefore we recommend environment properties that you can set easily prior to launching the process.

Refer to the documentation for all the properties that you can use, we'll mention the most important ones (in our opinion) below:

### `gcServer`

> Configures whether the application uses workstation garbage collection or server garbage collection.

You can read the exact specific of the server GC at **[fundamentals of garbage collection](https://docs.microsoft.com/dotnet/standard/garbage-collection/fundamentals)**.

ASF is using workstation garbage collection by default. This is mainly because of a good balance between memory usage and performance, which is more than enough for just a few bots, as usually a single concurrent background GC thread is fast enough to handle entire memory allocated by ASF.

However, today we have a lot of CPU cores that ASF can greatly benefit from, by having a dedicated GC thread per each CPU vCore that is available. This can greatly improve the performance during heavy ASF tasks such as parsing badge pages or the inventory, since every CPU vCore can help, as opposed to just 2 (main and GC). Server GC is recommended for machines with 3 CPU vCores and more, workstation GC is automatically forced if your machine has just 1 CPU vCore, and if you have exactly 2 then you can consider trying both (results may vary).

Server GC itself does not result in a very huge memory increase by just being active, but it has much bigger generation sizes, and therefore is far more lazy when it comes to giving memory back to OS. You may find yourself in a sweet spot where server GC increases performance significantly and you'd like to keep using it, but at the same time you can't afford that huge memory increase that comes out of using it. Luckily for you, there is a "best of both worlds" setting, by using server GC with **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gclatencylevel)** configuration property set to `0`, which will still enable server GC, but limit generation sizes and focus more on memory. Alternatively, you might also experiment with another property, **[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gcheaphardlimitpercent)**, or even both of them at the same time.

However, if memory is not a problem for you (as GC still takes into account your available memory and tweaks itself), it's a much better idea to not change those properties at all, achieving superior performance in result.

* * *

You can enable all GC properties by setting appropriate `COMPlus_` environment variables. For example, on Linux (shell):

```shell
export COMPlus_gcServer=1

./ArchiSteamFarm # For OS-specific build
```

Or on Windows (powershell):

```powershell
$Env:COMPlus_gcServer=1

.\ArchiSteamFarm.exe # For OS-specific build
```

* * *

## Recommended optimization

- Ensure that you're using default value of `OptimizationMode` which is `MaxPerformance`. This is by far the most important setting, as using `MinMemoryUsage` value has dramatic effects on performance.
- Enable server GC. Server GC can be immediately seen as being active by significant memory increase compared to workstation GC.
- If you can't afford that much memory increase, considering tweaking **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gclatencylevel)** and/or **[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gcheaphardlimitpercent)** to achieve "the best of both worlds". However, if your memory can afford it, then it's better to keep it at default - server GC already tweaks itself during runtime and is smart enough to use less memory when your OS will truly need it.

If you've enabled server GC and kept other configuration properties at their default values, then you have superior ASF performance that should be blazing fast even with hundreds or thousands of enabled bots. CPU should not be a bottleneck anymore, as ASF is able to use your entire CPU power when needed, cutting required time to bare minimum. The next step would be CPU and RAM upgrades.
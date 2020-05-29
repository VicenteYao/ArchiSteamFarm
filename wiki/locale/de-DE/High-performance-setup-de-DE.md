# Hochleistungs-Setup

Dies ist genau das Gegenteil von **[Speichereffizientes Setup](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-de-DE)** und normalerweise möchtest du diesen Tipps folgen, wenn du die ASF-Leistung (in Bezug auf die CPU-Geschwindigkeit) weiter erhöhen willst, für potenzielle Kosten einer erhöhten Speichernutzung.

* * *

ASF versucht bereits die Performance zu bevorzugen, wenn es um die allgemeine ausgewogene Abstimmung geht. Daher gibt es nicht viel was man tun kann um die Performance weiter zu steigern, obwohl man auch nicht völlig ohne Möglichkeiten da steht. Beachte jedoch, dass diese Optionen standardmäßig nicht aktiviert sind, was bedeutet, dass sie nicht gut genug sind, um sie für die Mehrheit der Anwendungen als ausgewogen zu betrachten. Deshalb solltest du selbst entscheiden, ob diese Optionen zur Speichererweiterung für dich akzeptabel sind.

* * *

## Laufzeitoptimierung (Erweitert)

Die folgenden Tricks **beanspruchen eine ernsthafte Speicherzunahme** und sollten mit Vorsicht verwendet werden.

.NET Core runtime allows you to **[tweak garbage collector](https://docs.microsoft.com/dotnet/core/run-time-config/garbage-collector)** in a lot of ways, effectively fine-tuning the GC process according to your needs.

The recommended way of applying those settings is through `COMPlus_` environment properties. Of course, you could also use other methods, e.g. `runtimeconfig.json`, but some settings are impossible to be set this way, and on top of that ASF will replace your custom `runtimeconfig.json` with its own on the next update, therefore we recommend environment properties that you can set easily prior to launching the process.

Refer to the documentation for all the properties that you can use, we'll mention the most important ones (in our opinion) below:

### `gcServer`

> Configures whether the application uses workstation garbage collection or server garbage collection.

You can read the exact specific of the server GC at **[fundamentals of garbage collection](https://docs.microsoft.com/dotnet/standard/garbage-collection/fundamentals)**.

ASF verwendet standardmäßig die Garbage Collection der Workstation. Dies liegt vor allem an einem ausgewogenen Verhältnis zwischen Speichernutzung und Performance, das für wenige Bots mehr als ausreichend ist, da normalerweise ein einzelner gleichzeitiger Hintergrund-GC-Thread schnell genug ist um den gesamten von ASF zugewiesenen Speicher zu bewältigen.

Heutzutage haben wir jedoch eine Menge an CPU-Kernen, von denen ASF sehr profitieren kann, indem wir für jede verfügbare CPU vCore einen eigenen GC-Thread bereitstellen. Dies kann die Leistung bei komplexen ASF-Aufgaben wie dem Parsen von Abzeichen-Seiten oder dem Inventar erheblich verbessern, da jede CPU vCore helfen kann, im Gegensatz zu nur 2 (Haupt und GC). Server GC wird für Maschinen mit 3 CPU vCores und mehr empfohlen, Workstation GC wird automatisch erzwungen, wenn deine Maschine nur 1 CPU vCore hat, und wenn du genau 2 hast, dann kannst du beide ausprobieren (die Ergebnisse können variieren).

Server GC selbst führt nicht zu einer sehr großen Speicherzunahme, wenn er einfach nur aktiv ist, aber er hat viel größere Generationsgrößen und ist daher viel fauler, wenn es darum geht, dem Betriebssystem Speicher zurückzugeben. Du befindest dich möglicherweise an einem Sweet Spot, an dem Server GC die Leistung signifikant erhöht und du sie weiterhin nutzen möchtest, aber gleichzeitig kannst du dir nicht leisten, dass der enorme Speicherzuwachs, der durch die Verwendung entsteht, zunimmt. Luckily for you, there is a "best of both worlds" setting, by using server GC with **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gclatencylevel)** configuration property set to `0`, which will still enable server GC, but limit generation sizes and focus more on memory. Alternatively, you might also experiment with another property, **[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gcheaphardlimitpercent)**, or even both of them at the same time.

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

## Empfohlene Optimierung

- Vergewissere dich, dass du den Standardwert `MaxPerformance` für `OptimizationMode` verwendest. Dies ist bei weitem die wichtigste Einstellung, da die Verwendung des Wertes `MinMemoryUsage` dramatische Auswirkungen auf die Performance hat.
- Enable server GC. Server GC can be immediately seen as being active by significant memory increase compared to workstation GC.
- If you can't afford that much memory increase, considering tweaking **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gclatencylevel)** and/or **[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup#gcheaphardlimitpercent)** to achieve "the best of both worlds". Wenn dein Speicher es sich jedoch leisten kann, dann ist es besser, es bei der Standardeinstellung zu belassen - Server GC optimiert sich bereits während der Laufzeit und ist intelligent genug um weniger Speicher zu verbrauchen, wenn dein Betriebssystem es wirklich benötigt.

If you've enabled server GC and kept other configuration properties at their default values, then you have superior ASF performance that should be blazing fast even with hundreds or thousands of enabled bots. Die CPU sollte kein Engpass mehr sein, da ASF in der Lage ist, bei Bedarf die gesamte CPU-Leistung zu nutzen, was die benötigte Zeit auf ein Minimum reduziert. Der nächste Schritt wäre CPU und RAM Upgrades.
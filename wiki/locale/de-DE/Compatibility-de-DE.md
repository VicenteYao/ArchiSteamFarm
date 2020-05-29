# Kompatibilität

ASF ist eine, auf .NET Core basierende, C#-Anwendung. Das bedeutet, dass ASF nicht direkt in **[Maschinencode](https://en.wikipedia.org/wiki/Machine_code)** kompiliert wird, der auf deiner CPU läuft, sondern in **[CIL](https://en.wikipedia.org/wiki/Common_Intermediate_Language)**, der eine CIL-kompatible Runtime für seine Ausführung benötigt.

Dieser Lösungsansatz hat enorme Vorteile, da CIL plattformunabhängig ist, weshalb ASF nativ auf vielen verfügbaren Betriebssystemen, insbesondere Windows, Linux und OS X, laufen kann. Es ist nicht nur keine Emulation erforderlich, sondern es werden auch alle plattform- und hardwarebezogenen Optimierungen, wie z.B. CPU SSE-Anweisungen unterstützt. Dank dessen kann ASF eine überlegene Leistung und Optimierung erreichen, während es gleichzeitig eine perfekte Kompatibilität und Zuverlässigkeit bietet.

Das bedeutet auch, dass ASF **keine spezifische Betriebssystem-Anforderung** hat, weil es die funktionierende **Runtime** auf diesem Betriebssystem benötigt und nicht das Betriebssystem selbst. Solange diese Runtime ASF-Code korrekt ausführt, spielt es keine Rolle ob das zugrunde liegende Betriebssystem Windows, Linux, OS X, BSD, Sony Playstation 4, Nintendo Wii oder dein Toaster ist - solange es **[.NET Core dafür gibt](https://github.com/dotnet/core-setup#daily-builds)**, gibt es **[ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** für dieses System.

Aber je nachdem auf welcher Plattform du ASF ausführst, musst du sicherstellen, dass auf deiner Zielplattform die **[.NET Core Prerequisites](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)** installiert sind. Das sind Low-Level-Bibliotheken, die für eine einwandfreie Laufzeitfunktionalität erforderlich sind und absolut notwendig für die Funktionsfähigkeit von ASF. Sehr wahrscheinlich hast du einige von ihnen (oder sogar alle) bereits installiert.

* * *

## Multiple instances

ASF is compatible with running multiple instances of the process on the same machine. The instances can be completely standalone or derived from the same binary location (in which case, you want to run them with different `--path` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**).

When running multiple instances from the same binary, keep in mind that you should typically disable auto-updates in all of their configs, as there is no synchronization between them in regards to auto-updates. If you'd like to keep having auto-updates enabled, we recommend standalone instances, but you can still make updates work, as long as you can ensure that all other ASF instances are closed.

ASF will do its best to maintain a minimum amount of OS-wide, cross-process communication with other ASF instances. This includes ASF checking its configuration directory against other instances, as well as sharing core process-wide limiters configured with `*LimiterDelay` **[global config properties](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config)**, ensuring that running multiple ASF instances will not cause a possibility to run into a rate-limiting issue. In regards to technical aspects, Windows platforms use native OS-wide named semaphores for this purpose, Unix platforms use fallback mechanism of custom ASF file-based locks created in `/tmp/ASF` directory.

It's not required for running ASF instances to share the same `*LimiterDelay` properties, they can use different values, as each ASF will add its own configured delay to the release time after acquiring the lock. If the configured `*LimiterDelay` is set to `0`, ASF instance will entirely skip waiting for the lock of given resource that is shared with other instances (that could potentially still maintain a shared lock with each other). When set to any other value, ASF will properly synchronize with other ASF instances and wait for its turn, then release the lock after configured delay, allowing other instances to continue.

ASF takes into account `WebProxy` setting when deciding about shared scope, which means that two ASF instances using different `WebProxy` configurations will not share their limiters with each other. This is implemented in order to allow `WebProxy` setups to operate without excessive delays, as expected from different network interfaces. This should be good enough for majority of use cases, however, if you have a specific custom setup in which you're e.g. routing requests yourself in a different way, you can specify network group yourself through `--network-group` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**, which will allow you to declare ASF group that will be synchronized with this instance. Keep in mind that custom network groups are used exclusively, which means that ASF will no longer use `WebProxy` for determining the right group, as you're in charge of grouping in this case.

* * *

## ASF-Pakete

ASF gibt es in 2 Hauptformen - generisches Paket und betriebssystemspezifisch. Aus funktionaler Sicht sind beide Pakete genau gleich. Beide sind auch in der Lage, sich selbst automatisch zu aktualisieren. Der einzige Unterschied zwischen ihnen ist, ob zusätzlich zum **generischen** Paket von ASF auch eine **betriebssystemspezifische** Runtime enthalten ist oder nicht.

* * *

### Generisch

Das generische Paket ist ein plattformunabhängiger Build, der keinen maschinenspezifischen Code enthält. Dieses Setup erfordert von dir, dass du die .NET Core Runtime bereits auf deinem Betriebssystem ** in der entsprechenden Version** installiert hast. Wir alle wissen, wie lästig es ist, Abhängigkeiten auf dem neuesten Stand zu halten, deshalb ist dieses Paket hauptsächlich für Leute gedacht, die .NET Core **bereits** verwenden und diese Runtime nicht explizit für ASF zusätzlich pflegen wollen, wenn sie das nutzen können, was sie bereits installiert haben. Das generische Paket ermöglicht es dir auch, ASF ** überall dort auszuführen, wo du eine funktionierende Implementierung der .NET Core Runtime zur Verfügung hast**, unabhängig davon, ob es einen betriebssystemspezifischen ASF Build dafür gibt oder nicht.

Es wird nicht empfohlen, den generischen Build zu verwenden, wenn du ein Gelegenheits- oder sogar fortgeschrittener Benutzer bist, der nur ASF zum Laufen bringen will und nicht in technische Details von .NET Core einsteigen möchte. Mit anderen Worten - wenn du weißt, was das ist, kannst du es benutzen, sonst ist es viel besser, ein betriebssystemspezifisches Paket zu verwenden, das unten erklärt wird.

#### .NET Framework Paket

Zusätzlich zu dem oben genannten generischen Paket gibt es auch das Paket `generic-netf`, das auf dem .NET Framework (und nicht dem .NET Core) basiert. Bei diesem Paket handelt es sich um eine veraltete Variante, die eine, bereits aus ASF V2 bekannte, fehlende Kompatibilität bietet und z.B. mit **[Mono](https://www.mono-project.com)** ausgeführt werden kann, während das mit dem `generic` .NET Core Paket derzeit nicht möglich ist.

Im Allgemeinen solltest du **dieses Paket so weit wie möglich vermeiden**, da die meisten Betriebssysteme und Setups perfekt (und viel besser) mit dem oben erwähnten ` generic ` Paket unterstützt werden. Tatsächlich ist es sinnvoll, dieses Paket nur auf Plattformen zu verwenden, auf denen es keine funktionierende .NET Core Runtime gibt, aber eine Mono-Implementierung bieten. Examples of such platforms include `linux-x86` (32-bit i386/i686 linux), as well as `linux-armel` (ARMv6 boards found e.g. in Raspberry Pi 0 & 1), all of which do not have official working .NET Core runtime as of today.

Im Laufe der Zeit werden mehr Plattformen von .NET Core unterstützt und die Kompatibilität zwischen .NET Framework und .NET Core wird sich verringern. In der Zukunft wird das `generic-netf` Paket vollständig durch das `generic` Paket ersetzt werden. Bitte verzichte darauf es zu verwenden, wenn du stattdessen ein anderes .NET Core Paket verwenden kannst, da `generic-netf` im Vergleich zu .NET Core Versionen viel Funktionalität und Kompatibilität fehlt und es in Zukunft immer weniger funktional sein wird. We offer support for this package **only** on machines that can't use `generic` variant above (e.g. `linux-x86`), and only with up-to-date runtime (e.g. latest Mono).

* * *

### Betriebssystemspezifisch

Das betriebssystemspezifische Paket beinhaltet neben dem verwalteten Code, der im generischen Paket enthalten ist, auch nativen Code für die jeweilige Plattform. Mit anderen Worten, das betriebssystemspezifische Paket **beinhaltet bereits die richtige .NET Core Runtime**, was es dir ermöglicht, den gesamten Installationsprozess komplett zu überspringen und ASF einfach direkt zu starten. Das betriebssystemspezifische Paket, wie man dem Namen entnehmen kann, ist betriebssystemspezifisch und jedes Betriebssystem benötigt eine eigene Version - zum Beispiel Windows benötigt PE32+ `ArchiSteamFarm.exe` Binärdatei während Linux mit Unix ELF `ArchiSteamFarm` Binärdatei arbeitet. Wie du vielleicht weißt, sind diese beiden Typen nicht miteinander kompatibel.

ASF ist derzeit in folgenden betriebsystemspezifischen Varianten verfügbar:

- `win-x64` ist kompatibel mit 64-Bit-Windows-Betriebssystemen. Dazu gehören Windows 7 (SP1+), 8.1, 10, Server 2008 R2 (SP1+), 2012, 2012 R2, 2016 sowie zukünftige Versionen.
- `linux-arm` ist kompatibel mit 32-Bit-ARM-basierten (ARMv7+) GNU/Linux-Betriebssystemen. This includes platforms such as Raspberry Pi 2 (and newer) with all GNU/Linux OSes available for them (such as Raspbian), in current and future versions. This variant will not work with older ARM architectures, such as ARMv6 found in Raspberry Pi 0 & 1, it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-arm64` works on 64-bit ARM-based (ARMv8+) GNU/Linux OSes. This includes platforms such as Raspberry Pi 3 (and newer) with all AArch64 GNU/Linux OSes available for them (such as Debian), in current and future versions. This variant will not work with 32-bit OSes that do not have required 64-bit libraries available (such as Raspbian), it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-x64` ist kompatibel mit 64-Bit GNU/Linux-Betriebssystemen. Dazu gehören Alpine, CentOS/Fedora/RHEL, Debian/Ubuntu/Linux Mint, OpenSUSE/SLES und viele andere, einschließlich ihrer Derivate, in aktuellen und zukünftigen Versionen.
- `osx-x64` ist kompatibel mit 64-Bit OS X Betriebssystemen. Dazu gehört die Version 10.13 sowie zukünftige Versionen.

Selbst wenn du kein betriebssystemspezifisches Paket für deine Betriebssystem-Architektur-Kombination zur Auswahl hast, kannst du natürlich jederzeit selbst die entsprechende .NET Core Runtime installieren und die generische ASF-Version ausführen, was auch der Hauptgrund dafür ist, dass diese überhaupt existiert. Der generische ASF-Build ist plattformunabhängig und läuft auf jeder Plattform, die eine funktionierende .NET Core Runtime hat. Wichtig ist: ASF benötigt die .NET Core Runtime und nicht ein bestimmtes Betriebssystem oder eine bestimmte Architektur. Wenn du zum Beispiel ein 32-Bit-Windows benutzt, dann kannst du trotz der fehlenden dedizierten `win-x86` ASF-Version immer noch das .NET Core SDK in der `win-x86`-Version installieren und die generische ASF-Version problemlos ausführen. Wir können nicht jede Kombination aus Betriebssystem und Architektur ansprechen, die existiert und von jemandem verwendet wird, also müssen wir irgendwo eine Grenze ziehen. Ein gutes Beispiel für diese Grenze ist x86, da es sich um eine veraltete Architektur seit mindestens 2004 handelt.

Für eine vollständige Liste aller unterstützten Plattformen und Betriebssystemen von .NET Core 3.1 besuche die **[Versionshinweise](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md)**.

* * *

## Runtime-Anforderungen

Wenn du ein betriebssystemspezifisches Paket verwendest, musst du dir keine Sorgen um die Runtime-Anforderungen machen, denn ASF wird immer mit der erforderlichen und aktuellen Runtime ausgeliefert, die einwandfrei funktioniert, solange du die **[.NET Core Prerequisites](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)** installiert und auf dem neuesten Stand hast. Mit anderen Worten, **du musst die .NET Core Runtime oder SDK** nicht installieren, da betriebssystemspezifische Builds nur native Betriebssystemabhängigkeiten (Prerequisites) und nichts anderes erfordern.

Wenn du jedoch versuchst, das **generische** ASF-Paket auszuführen, dann musst du sicherstellen, dass deine .NET Core Runtime die von ASF benötigte Plattform unterstützt.

ASF as a program is targeting **.NET Core 3.1** (`netcoreapp3.1`) right now, but it may target newer platform in the future. `netcoreapp3.1` is supported since 3.1.100 SDK (3.1.0 runtime), although ASF is configured to target **latest runtime at the moment of compilation**, so you should ensure that you have **[latest SDK](https://dotnet.microsoft.com/download)** (or at least runtime) available for your machine. Die generische ASF-Variante kann den Start verweigern, wenn deine Runtime älter ist als die minimale (Ziel-) Runtime, die während der Kompilierung bekannt ist.

Im Zweifelsfall solltest du überprüfen, was unsere **[kontinuierliche Integration](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** für die Kompilierung und Bereitstellung von ASF-Versionen auf GitHub verwendet. Dort findest du die `dotnet --info` Ausgabe oben in jedem Build.

* * *

## Probleme

### Various lock-related issues running ASF on Linux VPS with OpenVZ virtualization

OpenVZ kernel is usually based on a very old Linux kernel version (2.6) which seems incompatible with latest .NET Core runtime. If you're trying to run ASF in such environment, you may encounter various lock-related issues, usually in form of process freeze. See related CoreCLR issue: https://github.com/dotnet/coreclr/issues/26873

Our recommendation is to ditch OpenVZ in favour of much better virtualization solutions, such as KVM. The issue described here is indeed a .NET Core runtime bug that is supposed to be **[fixed](https://github.com/dotnet/coreclr/pull/26912)** in the next .NET Core runtime upgrade (3.1 service patch), but there is no actual timeframe for it yet. If you're unable to move to better virtualization solution, you can consider running `generic-netf` ASF variant with `mono`, at least until new runtime version is released.

For more advanced users that are not afraid of their Linux OS, there is a **[much better workaround](https://github.com/dotnet/coreclr/issues/26873#issuecomment-559854433)** available which makes it possible to run latest .NET Core ASF without running into this issue.
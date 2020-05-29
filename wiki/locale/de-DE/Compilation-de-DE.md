# Kompilierung

Kompilierung ist der Prozess zur Erstellung von ausführbaren Dateien. Dies solltest du tun, wenn du deine eigenen &Auml;nderungen zu ASF hinzufügen willst oder wenn du aus irgendeinem Grund den ausf&uuml;hrbaren Dateien der offiziell bereitgestellten **[Versionen](https://github.com/JustArchiNET/ArchiSteamFarm/releases)** nicht vertraust. Wenn du ein Benutzer und kein Entwickler bist, möchtest du höchstwahrscheinlich bereits vorkompilierte Binärdateien verwenden. Wenn du aber deine eigenen verwenden oder etwas Neues lernen möchtest, lies weiter.

ASF kann auf allen momentan unterstützten Plattformen kompiliert werden so lange du alle benötigten Programme hierzu hast.

* * *

## .NET Core SDK

Unabhängig von der Plattform benötigst du die vollständige .NET Core SDK (nicht nur Runtime) um ASF zu kompilieren. Eine Installationsanleitung findest du auf der **[.NET Core Installationsseite](https://dotnet.microsoft.com/download)**. Du musst die passende .NET Core SDK-Version für dein Betriebssystem installieren. Nach erfolgreicher Installation sollte der Befehl `dotnet` funktionieren und betriebsbereit sein. Du kannst mit `dotnet --info` überprüfen ob es funktioniert. Achte auch darauf, dass dein .NET Core SDK mit den ASF **[Runtime-Anforderungen](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-de-DE#runtime-anforderungen)** übereinstimmt.

* * *

## Kompilierung

Unter der Annahme, dass du die .NET Core SDK funktionsfähig und in der entsprechenden Version hast, navigiere einfach zum Quell-ASF-Verzeichnis (geklont oder heruntergeladen und entpacktes ASF-Repository) und führe folgendes aus:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/generic"
```

Wenn du Linux/OS X verwendest, kannst du stattdessen das Skript `cc.sh` verwenden, was dasselbe in etwas komplexerer Weise tut.

If compilation ended successfully, you can find your ASF in `source` flavour in `out/generic` directory. This is the same as official `generic` ASF build, but it has forced `UpdateChannel` and `UpdatePeriod` of `0`, which is appropriate for self-builds.

### Betriebssystemspezifisch

Du kannst auch das betriebssystemspezifische .NET Core Paket erstellen, falls du einen bestimmten Grund dazu hast. In general you shouldn't do that because you've just compiled `generic` flavour that you can run with your already-installed .NET Core runtime that you've used for the compilation in the first place, but just in case you want to:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/linux-x64" -r "linux-x64"
```

Natürlich solltest du `linux-x64` durch eine Betriebssystemarchitektur ersetzen die du anpeilen möchtest, wie beispielsweise `win-x64`. Auch in diesem Build werden Aktualisierungen deaktiviert sein.

### .NET Framework

Im sehr seltenen Fall, dass du das `generic-netf` Paket erstellen möchtest, kannst du das Zielframework von `netcoreapp3.1` auf `net48` ändern. Beachte, dass du neben der .NET Core SDK auch ein entsprechendes **[.NET Framework](https://dotnet.microsoft.com/download/visual-studio-sdks)** Entwicklerpaket für die Kompilierung der `netf` Variante benötigst, so dass das untenstehende nur unter Windows funktioniert:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "net48" -o "out/generic-netf"
```

Falls du das .NET Framework oder sogar die .NET Core SDK selbst nicht installieren kannst (z.B. weil du auf `linux-x86` mit `mono` kompiliert hast), kannst du `msbuild` direkt aufrufen. Du musst auch `ASFNetFramework` manuell angeben, da ASF standardmäßig netf build auf Nicht-Windows-Plattformen deaktiviert:

```shell
msbuild /m /r /t:Publish /p:Configuration=Release /p:TargetFramework=net48 /p:PublishDir=out/generic-netf /p:ASFNetFramework=true ArchiSteamFarm
```

* * *

## Entwicklung

Wenn du ASF-Quelltext bearbeiten möchtest, kannst du zu diesem Zweck jede .NET Core kompatible IDE verwenden, obwohl selbst das optional ist. Du kannst auch mit einem Notepad arbeiten und mit dem oben beschriebenen `dotnet` Befehl kompilieren. Dennoch empfehlen wir für Windows ein **[aktuelles Visual Studio](https://visualstudio.microsoft.com/downloads)** (kostenlose Community-Version reicht vollkommen). Wir empfehlen auch, Visual Studio zusammen mit **[ReSharper](https://www.jetbrains.com/resharper)** zu verwenden (optional), auch wenn dies kein kostenloses Produkt ist.

Wenn du stattdessen den ASF-Quelltext unter Linux/OS X bearbeiten möchtest, empfehlen wir eine **[aktuelle Visual Studio Code Version](https://code.visualstudio.com/download)**. Diese Version ist nicht so umfangreich wie das klassische Visual Studio, aber reicht vollkommen aus.

Natürlich sind alle obigen Vorschläge nur Empfehlungen, du kannst verwenden was immer du willst. Am Ende wird sowieso immer `dotnet build` ausgeführt. Wir verwenden Visual Studio + ReSharper für die ASF-Entwicklung, sowie ein paar der Drittanbieter `Programme`, die du im Repository finden kannst.

* * *

## Tags

Der `master` Zweig ist nicht unbedingt in einem Zustand, der eine erfolgreiche Kompilierung oder eine fehlerfreie ASF-Ausführung überhaupt erst ermöglicht, da es sich um einen Entwicklungszweig handelt, wie in unserem **[Veröffentlichungszyklus](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle-de-DE)** beschrieben. If you want to compile or reference ASF from source, then you should use appropriate **[tag](https://github.com/JustArchiNET/ArchiSteamFarm/tags)** for that purpose, which guarantees at least successful compilation, and very likely also flawless execution (if build was marked as stable release). Um den aktuellen "Gesundheitszustand" des Baumes zu überprüfen, kannst du unsere CIs verwenden - **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** oder **[Travis](https://travis-ci.com/JustArchiNET/ArchiSteamFarm)**.

* * *

## Offizielle Veröffentlichungen

Die offiziellen ASF-Veröffentlichungen werden von **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** unter Windows kompiliert, mit der neuesten .NET Core SDK, welche den ASF **[Runtime-Anforderungen](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-de-DE#runtime-anforderungen)** entspricht. Nach dem Bestehen der Tests werden alle Pakete auf GitHub bereitgestellt. Dies garantiert auch Transparenz, da AppVeyor für alle Builds immer den offiziellen und öffentlichen Quelltext verwendet und man die Prüfsummen von AppVeyor-Artefakten mit GitHub Assets vergleichen kann. Die ASF-Entwickler kompilieren oder veröffentlichen selbst keine Builds, außer für den privaten Entwicklungsprozess und Debugging.
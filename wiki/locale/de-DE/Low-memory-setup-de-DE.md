# Speichereffizientes Setup

Dies ist genau das Gegenteil von **[Hochleistungs-Setup](https://github. com/JustArchiNET/ArchiSteamFarm/wiki/High-performance-setup-de-DE)** und normalerweise möchtest du diesen Tipps folgen, wenn du den Speicherverbrauch von ASF verringern willst, um die Gesamtbelastung zu senken.

* * *

ASF ist per Definition extrem ressourcenschonend und je nach Nutzung ist sogar ein 128 MB VPS mit Linux in der Lage, es auszuführen, obwohl es nicht empfohlen wird so niedrig zu gehen da dies zu verschiedenen Problemen führen kann. Obwohl ASF leicht ist, hat es keine Angst davor, das Betriebssystem nach mehr Speicher zu fragen, wenn Speicher benötigt wird, damit ASF mit optimaler Geschwindigkeit arbeiten kann.

ASF als Anwendung versucht, so optimiert und effizient wie möglich zu sein, wobei auch die während der Ausführung verwendeten Ressourcen berücksichtigt werden. Wenn es um Speicher geht, zieht ASF die Performance dem Speicherverbrauch vor, was zu temporären Speicherspitzen führen kann, was z.B. bei Konten mit mehr als 3 Abzeichen-Seiten auffällt, da ASF die erste Seite abruft und analysiert, daraus die Gesamtzahl der Seiten liest und dann für jede zusätzliche Seite die Abrufaufgabe startet, was zum gleichzeitigen Abrufen und Parsen der verbleibenden Seiten führt. Dieser "zusätzliche" Speicherverbrauch (im Vergleich zu einem absoluten Minimum für den Betrieb) kann die Ausführung und die Gesamtleistung drastisch beschleunigen, und zwar auf Kosten des erhöhten Speicherverbrauchs, der erforderlich ist um all diese Dinge parallel auszuführen. Ähnliches geschieht mit allen anderen allgemeinen ASF-Aufgaben, die parallel ausgeführt werden können, z.B. mit dem Parsen aktiver Handelsangebote, ASF kann sie alle auf einmal analysieren, da sie alle unabhängig voneinander agieren. Darüber hinaus wird ASF (C# Runtime) ungenutzten Speicher **nicht** sofort danach an das Betriebssystem zurückgeben, was man in Form eines ASF-Prozesses schnell bemerken kann, der nur mehr und mehr Speicher benötigt, aber diesen Speicher nie an das Betriebssystem zurückgibt. Einige Leute mögen es bereits fragwürdig finden, vielleicht sogar ein Memory-Leak vermuten, aber keine Sorge, all das ist zu erwarten.

ASF ist extrem gut optimiert und nutzt die vorhandenen Ressourcen so gut wie möglich. Eine hohe Speicherauslastung von ASF bedeutet nicht, dass ASF aktiv diesen Speicher **verwendet** und ** ihn benötigt**. Sehr oft behält ASF zugewiesenen Speicher als "Raum" für zukünftige Aktionen, da wir die Leistung drastisch verbessern können, wenn wir nicht für jeden Speicherabschnitt, den wir verwenden wollen, das Betriebssystem fragen müssen. Die Runtime sollte automatisch ungenutzten ASF-Speicher an das Betriebssystem zurückgeben, wenn das Betriebssystem ihn **wirklich** benötigt. **[Ungenutzter Speicher ist verschwendeter Speicher](https://www.howtogeek.com/128130/htg-explains-why-its-good-that-your-computers-ram-is-full)**. Du stößt auf Probleme, wenn der Speicher, den du **benötigst** höher ist als der Speicher, der für dich verfügbar ist, nicht wenn ASF einige zusätzliche zugewiesene Ressourcen behält, mit dem Ziel, Funktionen zu beschleunigen, die in einem Moment ausgeführt werden. Du stößt auf Probleme, wenn dein Linux-Kernel den ASF-Prozess aufgrund von OOM (out of memory) beendet, nicht wenn du den ASF-Prozess als Top-Speicherverbraucher in `htop` siehst.

Der Garbage Collector, der in ASF verwendet wird, ist ein sehr komplexer Mechanismus, intelligent genug, um nicht nur ASF selbst, sondern auch dein Betriebssystem und andere Prozesse zu berücksichtigen. Wenn du viel freien Speicher hast, wird ASF um alles bitten, was nötig ist, um die Leistung zu verbessern. Dies kann sogar bis zu 1 GB (mit Server GC) betragen. Wenn der Arbeitsspeicher deines Betriebssystems fast erschöpft ist, gibt ASF automatisch einen Teil davon an das Betriebssystem zurück, um die Dinge zu erleichtern, was dazu führen kann, dass der Gesamtverbrauch an ASF-Speicher nur noch 50 MB beträgt. Der Unterschied zwischen 50 MB und 1 GB ist enorm, aber das gilt auch für den Unterschied zwischen einem kleinen 512 MB VPS und einem großen dedizierten Server mit 32 GB. Wenn ASF garantieren kann, dass dieser Speicher sinnvoll ist und gleichzeitig nichts anderes ihn gerade jetzt benötigt, zieht er es vor, ihn zu behalten und sich automatisch auf der Grundlage von Routinen zu optimieren, die in der Vergangenheit ausgeführt wurden. Der in ASF verwendete GC ist selbstanpassend und erzielt umso bessere Ergebnisse, je länger der Prozess läuft.

Dies ist auch der Grund, warum der ASF-Prozessspeicher von Setup zu Setup variiert, da ASF sein Bestes tut, um die verfügbaren Ressourcen **so effizient wie möglich** zu nutzen und nicht auf eine feste Art und Weise, wie es zu Windows XP-Zeiten der Fall war. Der tatsächliche (reale) Speicherverbrauch, den ASF verwendet, kann mit dem `stats` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** überprüft werden und liegt normalerweise bei etwa 4 MB für nur wenige Bots, bis zu 30 MB, wenn du Dinge wie **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC)** und andere erweiterte Features verwendest. Bedenke, dass der von dem Befehl `stats` zurückgegebene Speicher auch freien Speicher beinhaltet, der noch nicht vom Garbage Collector zurückgefordert wurde. Alles andere ist gemeinsamer Laufzeitspeicher (ca. 40-50 MB) und Ausführungsspielraum (variiert). Aus diesem Grund kann der gleiche ASF auch nur 50 MB in einer speicherarmen VPS-Umgebung verwenden, während er sogar bis zu 1 GB auf deinem Desktop verwendet. ASF passt sich aktiv an die jeweilige Umgebung an und wird versuchen, ein optimales Gleichgewicht zu finden, um dein Betriebssystem weder unter Druck zu setzen noch seine eigene Leistung einzuschränken, wenn du viel ungenutzten Speicher hast, der in Gebrauch genommen werden könnte.

* * *

Natürlich gibt es viele Möglichkeiten, wie du ASF helfen kannst die richtige Richtung in Bezug auf den Speicher den du verwenden möchtest zu lenken. Im Allgemeinen ist es am besten, den Garbage-Collector in Ruhe arbeiten zu lassen und das zu tun, was er für das Beste hält. Aber das ist nicht immer möglich, zum Beispiel, wenn dein Linux-Server auch mehrere Websites, MySQL-Datenbank und PHP-Worker hostet, dann kannst du es dir nicht wirklich leisten, dass ASF sich selbst reduziert, wenn du in der Nähe von OOM liegst, da es normalerweise zu spät ist und der Leistungsabfall früher kommt. Dies ist in der Regel der Fall, wenn du an einer weiteren Optimierung interessiert bist und deshalb diese Seite liest.

Die folgenden Vorschläge sind in einige Kategorien unterteilt, mit unterschiedlichem Schwierigkeitsgrad.

* * *

## ASF Setup (Einfach)

Die folgenden Tricks **beeinträchtigen die Performance nicht negativ** und können sicher auf allen Setups angewendet werden.

- Führe niemals mehr als eine ASF-Instanz aus. ASF ist dazu gedacht, eine unbegrenzte Anzahl von Bots auf einmal zu verarbeiten, und wenn du nicht jede ASF-Instanz an eine andere Interface/IP-Adresse bindest, solltest du genau **einen** ASF-Prozess haben, mit mehreren Bots (falls erforderlich).
- Nutze `ShutdownOnFarmingFinished`. Ein aktiver Bot benötigt mehr Ressourcen als ein deaktivierter. Es ist keine nennenswerte Einsparung, da der Zustand des Bot noch beibehalten werden muss, aber du sparst eine gewisse Menge an Ressourcen, insbesondere alle mit dem Netzwerk verbundenen Ressourcen, wie TCP-Sockets. Du brauchst nur einen aktiven Bot um die ASF-Instanz am Laufen zu halten und du kannst bei Bedarf jederzeit andere Bots aufrufen.
- Halte die Anzahl deiner Bots niedrig. Nicht `Enabled` Bot-Instanz benötigen weniger Ressourcen, da ASF sich nicht die Mühe macht, sie zu starten. Bedenke auch, dass ASF für jede deiner Konfigurationen einen Bot erstellen muss. Wenn du also den gegebenen Bot nicht `start` musst und du etwas mehr Speicherplatz sparen willst, kannst du `Bot.json` vorübergehend umbenennen, z.B. in `Bot.json.bak`, um zu vermeiden, dass der Status für deine deaktivierte Bot-Instanz in ASF erzeugt wird. Auf diese Weise kannst du es nicht `start`, ohne es umzubenennen, aber ASF wird sich auch nicht die Mühe machen, den Zustand dieses Bot im Speicher zu halten und Platz für andere Dinge zu lassen (sehr kleine Ersparnis, in 99,9% der Fälle solltest du dich nicht damit beschäftigen, behalte einfach deine Bots mit `Enabled` von `false`).
- Optimiere deine Konfigurationen. Insbesondere die globale ASF-Konfiguration hat viele Variablen, die angepasst werden müssen, z.B. durch die Erhöhung von `LoginLimiterDelay` werden deine Bots langsamer, was es bereits gestarteten Instanzen ermöglicht, Abzeichen in der Zwischenzeit zu holen, anstatt deine Bots schneller aufzurufen, was mehr Ressourcen beansprucht, da mehr Bots gleichzeitig größere Aufgaben erledigen (z.B. Parsen von Abzeichen). Je weniger Arbeit zur gleichen Zeit erledigt werden muss - desto weniger Speicherplatz wird verbraucht.

Das sind einige Dinge, die du im Hinterkopf behalten kannst, wenn es um die Speichernutzung geht. Diese Dinge haben jedoch keine "entscheidende" Bedeutung für die Speichernutzung, da die Speichernutzung hauptsächlich von Dingen kommt, mit denen ASF es zu tun hat, und nicht von internen Strukturen, die für das Sammeln von Karten verwendet werden.

Die ressourcenintensivsten Funktionen sind:

- Das Parsen der Abzeichen-Seite
- Das Parsen des Inventars

Das bedeutet, dass der Speicher am meisten ansteigt, wenn es sich bei ASF um das Lesen von Abzeichen-Seiten handelt, und wenn es sich um sein Inventar handelt (z.B. das Senden von Handelsangeboten oder das Arbeiten mit STM). Denn ASF hat es mit einer wirklich großen Datenmenge zu tun - der Speicherverbrauch deines Lieblingsbrowsers, der diese beiden Seiten startet, wird nicht niedriger sein. Tut mir leid, so funktioniert es - verringere die Anzahl deiner Abzeichen-Seiten und halte die Anzahl deiner Inventar-Gegenstände niedrig, das kann sicher helfen.

* * *

## Laufzeitoptimierung (Erweitert)

Die folgenden Tricks **beeinträchtigen die Performance-Minderung** und sollten mit Vorsicht angewendet werden.

.NET Core runtime allows you to **[tweak garbage collector](https://docs.microsoft.com/dotnet/core/run-time-config/garbage-collector)** in a lot of ways, effectively fine-tuning the GC process according to your needs.

The recommended way of applying those settings is through `COMPlus_` environment properties. Of course, you could also use other methods, e.g. `runtimeconfig.json`, but some settings are impossible to be set this way, and on top of that ASF will replace your custom `runtimeconfig.json` with its own on the next update, therefore we recommend environment properties that you can set easily prior to launching the process.

Refer to the documentation for all the properties that you can use, we'll mention the most important ones (in our opinion) below:

### `GCHeapHardLimitPercent`

> Specifies the GC heap usage as a percentage of the total memory.

The "hard" memory limit for ASF process, this setting tunes GC to use only a subset of total memory and not all of it. It may become especially useful in various server-like situations where you can dedicate a fixed percentage of your server's memory for ASF, but never more than that. Be advised that limiting memory for ASF to use will not magically make all of those required memory allocations go away, therefore setting this value too low might result in running into out of memory scenarios, where ASF process will be forced to terminate.

On the other hand, setting this value high enough is a perfect way to ensure that ASF will never use more memory than you can realistically afford, giving your machine some breathing room even under heavy load, while still allowing the program to do its job efficiently when possible.

### `GCLatencyLevel`

> Gibt die GC-Latenzstufe an, für die du optimieren möchtest.

This is undocumented property that turned out to work exceptionally well for ASF, by limiting size of GC generations and in result make GC purge them more frequently and more aggressively. Die standardmäßige (symmetrische) Latenzstufe ist `1`, wir werden `0` verwenden wollen, was sich nach der Speichernutzung richtet.

### `gcTrimCommitOnLowMemory`

> Wenn wir die Einstellung vorgenommen haben, trimmen wir den engagierten Raum aggressiver für das ephemere Segment. Dies wird verwendet, um viele Instanzen von Serverprozessen auszuführen, bei denen sie so wenig Speicher wie möglich gebunden halten wollen.

This offers little improvement, but may make GC even more aggressive when system will be low on memory, especially for ASF which makes use of threadpool tasks heavily.

* * *

You can enable all GC properties by setting appropriate `COMPlus_` environment variables. For example, on Linux (shell):

```shell
# Don't forget to tune this one if you're going to use it
export COMPlus_GCHeapHardLimitPercent=75

export COMPlus_GCLatencyLevel=0
export COMPlus_gcTrimCommitOnLowMemory=1

./ArchiSteamFarm # For OS-specific build
```

Or on Windows (powershell):

```powershell
# Don't forget to tune this one if you're going to use it
$Env:COMPlus_GCHeapHardLimitPercent=75

$Env:COMPlus_GCLatencyLevel=0
$Env:COMPlus_gcTrimCommitOnLowMemory=1

.\ArchiSteamFarm.exe # For OS-specific build
```

Insbesondere `GCLatencyLevel` wird sehr nützlich sein, da wir überprüft haben, dass die Laufzeit tatsächlich Programmcode für den Speicher optimiert und somit den durchschnittlichen Speicherverbrauch signifikant reduziert, selbst bei Server-GC. Es ist einer der besten Tricks, die du anwenden kannst, wenn du den ASF-Speicherverbrauch deutlich senken und gleichzeitig die Leistung mit `OptimizationMode` nicht zu stark beeinträchtigen willst.

* * *

## ASF-Abstimmung (Mittelmäßig)

Die folgenden Tricks **führen zu einer ernsthaften Leistungsabnahme** und sollten mit Vorsicht angewendet werden.

- Als letzten Ausweg kannst du ASF für `MinMemoryUsage` durch `OptimizationMode` in der **[globalen Konfigurationseigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config)** einstellen. Lies sorgfältig seinen Zweck, da dies eine ernsthafte Leistungseinbuße ist und fast keine Vorteile für den Speicher hat. Dies ist normalerweise **das Letzte, was du tun willst**, lange nachdem du die **[Laufzeitoptimierung](#runtime-tuning-advanced)** durchgeführt hast, um sicherzustellen, dass du keine andere Wahl hast.

* * *

## Empfohlene Optimierung

- Beginne mit den einfachen ASF-Setup-Tricks, vielleicht benutzt du deinen ASF einfach falsch, wie z.B. den Prozess für alle deine Bots mehrmals zu starten oder alle aktiv zu halten, wenn du nur ein oder zwei zum Autostart brauchst.
- If it's still not enough, enable all configuration properties listed above by setting appropriate `COMPlus_` environment variables. Insbesondere `GCLatencyLevel` bietet signifikante Laufzeitverbesserungen bei geringen Leistungskosten.
- Wenn auch das nicht geholfen hat, aktiviere als letztes Mittel `MinMemoryUsage` `OptimizationMode`. Dies zwingt ASF, fast alles in synchroner Angelegenheit auszuführen, was es viel langsamer macht, aber auch nicht auf Thread-Pool angewiesen ist, um die Dinge auszugleichen, wenn es um die parallele Ausführung geht.

Es ist physisch unmöglich, den Speicher noch weiter zu verringern, dein ASF ist bereits in Bezug auf die Leistung stark beeinträchtigt und du hast alle deine Möglichkeiten ausgeschöpft, sowohl code- als auch laufzeitbezogen. Überlege dir, etwas zusätzlichen Speicher für ASF hinzuzufügen, selbst 128 MB würden einen großen Unterschied machen.
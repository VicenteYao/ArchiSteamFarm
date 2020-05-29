# Befehlszeilenargumente

ASF unterstützt mehrere Befehlszeilenargumente, die die Programm-Runtime beeinflussen können. Diese können von fortgeschrittenen Benutzern verwendet werden, um festzulegen, wie das Programm arbeiten soll. Im Vergleich zum gängigen Standardweg über die Konfigurationsdatei `ASF.json` werden Befehlszeilenargumente für die Kerninitialisierung (z.B. `--path`), plattformspezifische Einstellungen (z.B. `--system-required`) oder sensible Daten (z.B. `--cryptkey`) verwendet.

* * *

## Nutzung

Die Nutzung ist abhängig vom verwendeten Betriebssystem und der ASF Variante.

Generisch:

```shell
dotnet ArchiSteamFarm.dll --argument --anderesArgument
```

Windows:

```powershell
.\ArchiSteamFarm.exe --argument --anderesArgument
```

Linux/OS X:

```shell
./ArchiSteamFarm --argument --anderesArgument
```

Die Befehlszeilenargumente werden auch in allgemeinen Hilfsskripten wie zum Beispiel `ArchiSteamFarm.cmd` oder `ArchiSteamFarm.sh` unterstützt. Darüber hinaus kannst du bei Verwendung eines Hilfsskripts auch die Umgebungsvariable `ASF_ARGS` verwenden, wie es im Abschnitt **[Docker](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker-de-DE#befehlszeilenargumente)** beschrieben ist.

Wenn dein Argument Leerzeichen enthält, vergiss nicht, es in Anführungszeichen zu setzen. Diese zwei sind falsch:

```shell
./ArchiSteamFarm --path /home/archi/Meine Downloads/ASF # Falsch!
./ArchiSteamFarm --path=/home/archi/Meine Downloads/ASF # Falsch!
```

Aber diese beiden sind völlig in Ordnung:

```shell
./ArchiSteamFarm --path "/home/archi/Meine Downloads/ASF" # OK
./ArchiSteamFarm "--path=/home/archi/Meine Downloads/ASF" # OK
```

## Argumente

`--cryptkey <key>` oder `--cryptkey=<key>` - ASF startet mit dem benutzerdefinierten kryptographischen Schlüssel `<key>`. Diese Option wirkt sich auf die **[Sicherheit](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Security-de-DE)** aus und veranlasst ASF, den von dir bereitgestellten `<key>` Schlüssel anstelle des standardmäßig fest in die ausführbare Datei einprogrammierten Schlüssels zu verwenden. Beachte, dass Passwörter, welche mit diesem Schlüssel verschlüsselt sind, diesen bei jedem Start von ASF übergeben bekommen müssen.

Aufgrund der Natur dieser Eigenschaft ist es auch möglich, einen cryptkey zu setzen, indem man die Umgebungsvariable `ASF_CRYPTKEY` deklariert, was für Personen, die sensible Details in den Prozessargumenten vermeiden wollen, besser geeignet sein kann.

* * *

`--network-group <group>` or `--network-group=<group>` - will cause ASF to init its limiters with additional custom network group having `<group>` value. This option affects running ASF in **[multiple instances](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** by signalizing that given instance is dependent only on instances sharing the same network group, and independent of the rest. Typically you want to use this property only if you're routing ASF requests through custom mechanism (e.g. different IP addresses) and you want to set networking groups yourself, without relying on ASF to do it automatically (which currently includes taking into account `WebProxy` only). Keep in mind that when using a custom network group, this is unique identifier within the local machine, and ASF will not take into account any other details, such as `WebProxy` value, allowing you to e.g. start two instances with different `WebProxy` values which are still dependent on each other.

Due to the nature of this property, it's also possible to set the value by declaring `ASF_NETWORK_GROUP` environment variable, which may be more appropriate for people that would want to avoid sensitive details in the process arguments.

* * *

`--no-restart` - Diese Option wird hauptsächlich in unseren **[Docker](https://github.com/JustArchi/ArchiSteamFarm/wiki/Docker-de-DE)**-Containern genutzt und setzt `AutoRestart` auf `false`. Wenn du keinen besonderen Bedarf hast, solltest du stattdessen die Eigenschaft `AutoRestart` direkt in deiner Konfigurationsdatei setzen. Diese Option existiert nur damit unser Docker-Skript deine globale Konfiguration nicht modifizieren muss, um sie an die eigene Umgebung anzupassen. Natürlich, wenn Sie ASF in einem Skript ausführen, können Sie auch diesen Schalter verwenden (ansonsten sind Sie mit der globalen Konfigurationseigenschaft besser dran).

* * *

`--path <path>` oder `--path=<path>` - ASF wechselt beim Start immer in sein eigenes Verzeichnis. Wird dieser Parameter angegeben so wird ASF nach der Initialisierung zu dem gegebenem Programmverzeichnis navigieren, dies macht es Ihnen möglich andere Verzeichnisse wie z.B. `config`, `plugins` oder `www` (sowie auch die `NLog.config` Datei) für unterschiedliche Teile der Applikation zu nutzen. Das Kopieren der Binärdateien an diese Stellen ist dadurch nicht mehr nötig. Es kann besonders nützlich sein, wenn Sie die Binärdatei von der eigentlichen Konfiguration trennen möchten, wie es in einer Linux-ähnlichen Verpackung geschieht - so können Sie eine (aktuelle) Binärdatei mit mehreren verschiedenen Konfigurationen verwenden. Der Pfad kann entweder relativ zum aktuellen Ort der ASF-Binärdatei oder absolut sein. Keep in mind that this command points to new "ASF home" - the directory that has the same structure as original ASF, with config directory inside, see below example for explanation.

Aufgrund der Natur dieser Eigenschaft ist es auch möglich, den erwarteten Pfad zu setzen, indem man die Umgebungsvariable `ASF_PATH` deklariert, was für Personen, die sensible Details in den Prozessargumenten vermeiden wollen, besser geeignet sein kann.

If you're considering using this command-line argument for running multiple instances of ASF, we recommend reading our **[compatibility page](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** on this manner.

Beispiele:

```shell
dotnet /opt/ASF/ArchiSteamFarm.dll --path /opt/TargetDirectory # Absolute path
dotnet /opt/ASF/ArchiSteamFarm.dll --path ../TargetDirectory # Relative path works as well
ASF_PATH=/opt/TargetDirectory dotnet /opt/ASF/ArchiSteamFarm.dll # Same as env variable
```

    ├── /opt
    │     ├── ASF
    │     │     ├── ArchiSteamFarm.dll
    │     │     └── ...
    │     └── Ziel-Verzeichnis
    │           ├── config
    │           ├── logs (generiert)
    │           ├── plugins (optional)
    │           ├── www (optional)
    │           ├── log.txt (generiert)
    │           └── NLog.config (optional)
    └── ...
    

* * *

`--process-required` - Durch die Verwendung dieser Option wird das standardmäßige Herunterfahren von ASF deaktiviert, wenn keine Bots laufen. Dies ist besonders in Kombination mit der **[IPC](https://github.com/JustArchi/ArchiSteamFarm/wiki/IPC-de-DE)**-API nützlich, da die Mehrheit der Benutzer erwarten würden, dass ihr Webservice unabhängig von der Anzahl der aktivierten Bots ausgeführt wird. Wenn du die Option IPC verwendest oder anderweitig einen ASF-Prozess benötigst, welcher die ganze Zeit läuft, bis du ihn selbst beendest, ist dies die richtige Option.

Wenn du nicht vorhast IPC auszuführen, wird diese Option für dich ziemlich nutzlos sein, da du den Prozess bei Bedarf einfach wieder starten kannst (im Gegensatz zum Webserver von ASF, wo du ihn die ganze Zeit lauschen lassen musst um Befehle zu senden).

* * *

`--system-required` - Die Verwendung dieser Option veranlasst ASF, dem Betriebssystem zu signalisieren, dass das System für die gesamte Lebensdauer des Prozesses betriebsbereit sein muss. Derzeit hat diese Option nur auf Windows-Rechnern Auswirkungen, wo sie verhindert, dass dein System in den Ruhezustand wechselt, solange der Prozess läuft. Dies kann besonders nützlich sein, wenn du nachts auf deinem PC oder Laptop am Sammeln bist. ASF ist damit in der Lage dein System während des Sammelns wach zu halten und sich nach Beendigung des Sammelns wie gewohnt abzuschalten. So kann dein System wieder in den Ruhemodus wechseln und somit sofort nach Beendigung des Sammelns Strom sparen.

Bedenke, dass du für ein ordnungsgemäßes automatisches Herunterfahren von ASF weitere Einstellungen benötigst - vor allem `--process-required` sollte vermieden werden und alle deine Bots sollten `ShutdownOnFarmingFinished` aktiv haben. Natürlich ist das automatische Herunterfahren nur eine Möglichkeit für dieses Feature und keine Anforderung, da du dieses Attribut auch zusammen mit z.B. `--process-required` verwenden kannst, was dein System nach dem Start von ASF unbegrenzt wach hält.
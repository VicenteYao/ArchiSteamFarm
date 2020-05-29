# Protokollierung

ASF ermöglicht es dir, dein eigenes benutzerdefiniertes Protokollierungsmodul zu konfigurieren, das während der Laufzeit verwendet wird. Dies funktioniert, indem du eine spezielle Datei mit dem Namen `NLog.config` in dem Programmverzeichnis ablegst. Du kannst die gesamte Dokumentation über NLog im **[NLog Wiki](https://github.com/NLog/NLog/wiki/Configuration-file)** nachlesen, zusätzlich wirst du auch hier nützliche Beispiele dazu finden.

* * *

## Standard Protokollierung

Standardmäßig protokolliert ASF in `ColoredConsole` (Standardausgabe) und `File`. `File` Protokollierung beinhaltetet die `log.txt` Datei im Programmverzeichnis und das `logs` Verzeichnis für Archivierungszwecke.

Using custom NLog config automatically disables default ASF config, your config overrides **completely** default ASF logging, which means that if you want to keep e.g. our `ColoredConsole` target, then you must define it **yourself**. Dies erlaubt dir nicht nur **extra** Protokollierungsziele zu erstellen, sondern auch die **Standardziele** zu verändern oder deaktivieren.

Wenn du die standard ASF-Protokollierung ohne irgendwelche Veränderung verwenden möchtest, musst du nichts tun - auch brauchst du dies nicht in der `NLog.config` definieren. Verwende die `NLog.config` nicht, wenn du die standard ASF-Protokollierung nicht verändern möchtest. Zum Vergleich: Das Äquivalent zur fest definierten standard ASF-Protokollierung wäre:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="ColoredConsole" name="ColoredConsole" layout="${date:format=yyyy-MM-dd HH\:mm\:ss}|${processname}-${processid}|${level:uppercase=true}|${logger}|${message}${onexception:inner= ${exception:format=toString,Data}}" />
    <target xsi:type="File" name="File" archiveFileName="${currentdir}/logs/log.{#}.txt" archiveNumbering="Rolling" archiveOldFileOnStartup="true" cleanupFileName="false" concurrentWrites="false" deleteOldFileOnStartup="true" fileName="${currentdir}/log.txt" layout="${date:format=yyyy-MM-dd HH\:mm\:ss}|${processname}-${processid}|${level:uppercase=true}|${logger}|${message}${onexception:inner= ${exception:format=toString,Data}}" maxArchiveFiles="10" />
    <!-- Below becomes active when ASF's IPC interface is started -->
    <!-- <target type="History" name="History" layout="${date:format=yyyy-MM-dd HH\:mm\:ss}|${processname}-${processid}|${level:uppercase=true}|${logger}|${message}${onexception:inner= ${exception:format=toString,Data}}" maxCount="20" /> -->
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="ColoredConsole" />
    <logger name="*" minlevel="Debug" writeTo="File" />
    <!-- Below becomes active when ASF's IPC interface is started -->
    <!-- <logger name="*" minlevel="Debug" writeTo="History" /> -->
  </rules>
</nlog>
```

* * *

## ASF Integration

ASF enthält einige nette Quellcode-Tricks, die die Integration mit NLog verbessern und es dir ermöglichen bestimmte Nachrichten leichter zu erfassen.

Die NLog-spezifische `${logger}` Variable wird immer die Quelle der Nachricht anzeigen - es wird entweder `BotName` von einem deiner Bots sein, oder `ASF` wenn die Nachricht direkt vom ASF-Prozess kommt. Auf diese Weise kannst du Nachrichten leicht abfangen, die bestimmte Bot(s) oder ASF-Prozesse (nur) berücksichtigen, anstatt sie alle, basierend auf dem Namen des Loggers.

ASF versucht, Meldungen entsprechend den von NLog bereitgestellten Warnstufen zu kennzeichnen, was es dir ermöglicht, nur bestimmte Meldungen von bestimmten Protokollebenen statt von allen zu erhalten. Natürlich kann die Protokollierungsstufe für eine bestimmte Nachricht nicht angepasst werden, da es sich um eine ASF-Festlegung handelt, wie ernst die gegebene Nachricht ist, aber du kannst ASF definitiv weniger/mehr leise machen, wie du es für richtig hältst.

ASF protokolliert zusätzliche Informationen, wie z.B. Benutzer-/Chat-Nachrichten auf der `Trace` Protokollierungsebene. Die standardmäßige ASF-Protokollierung protokolliert nur die `Debug` Ebene und darüber, was diese zusätzlichen Informationen verbirgt, da sie für die Mehrheit der Benutzer nicht benötigt werden, sowie die Ausgabe mit potenziell wichtigeren Nachrichten zu mült. Du kannst diese Informationen jedoch nutzen, indem du `Trace` Logging-Ebene wieder aktivierst, insbesondere in Kombination mit dem Logging nur eines bestimmten Bots deiner Wahl, mit einem bestimmten Event, an dem du interessiert bist.

Im Allgemeinen versucht ASF, es dir so einfach und bequem wie möglich zu machen, nur die Nachrichten zu protokollieren, die du willst, anstatt dich zu zwingen, sie manuell durch Drittanbieterprogramme wie `grep` und ähnliche zu filtern. Konfiguriere NLog einfach wie unten beschrieben und du solltest in der Lage sein, auch sehr komplexe Protokollierungsregeln mit benutzerdefinierten Zielen, wie beispielsweise ganze Datenbanken, zu spezifizieren.

In Bezug auf die Versionierung - ASF versucht immer die aktuellste Version von NLog zu liefern, die zum Zeitpunkt der ASF-Version unter **[NuGet](https://www.nuget.org/packages/NLog)** verfügbar ist. Es ist sehr oft eine Version, die neuer als die neueste stabile ist, daher sollte es kein Problem sein, alle Funktionen zu verwenden, die du im NLog-Wiki in ASF finden kannst, sogar Funktionen, die sich in einem sehr aktiven Entwicklungs- und WIP-Status befinden - stelle einfach sicher, dass du auch aktuelles ASF verwendest.

Im Rahmen der ASF-Integration bietet ASF auch Unterstützung für zusätzliche ASF NLog-Protokollierungsziele, die im Folgenden erläutert werden.

* * *

## Beispiele

Lass uns mit etwas einfachem anfangen. Wir werden nur **[ColoredConsole](https://github.com/nlog/nlog/wiki/ColoredConsole-target)** target verwenden. Unsere initiale `NLog.config` wird so aussehen:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="ColoredConsole" name="ColoredConsole" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="ColoredConsole" />
  </rules>
</nlog>
```

Die Erklärung der obigen Konfiguration ist ziemlich einfach - wir definieren ein **logging target**, das `ColoredConsole` ist, dann leiten wir **all loggers** (`*`) der Ebene `Debug` und höher zu `ColoredConsole` target um, das wir zuvor definiert haben. Das ist alles.

Wenn du ASF jetzt mit obiger `NLog.config` startest, wird nur `ColoredConsole` target aktiv sein, und ASF wird nicht in `File` schreiben, unabhängig von der fest programmierten ASF NLog Konfiguration.

Nehmen wir an, wir mögen das Standardformat `${longdate}|${level:uppercase=true}|${logger}|${message}` nicht und wir wollen nur die Meldung protokollieren. Wir können dies tun, indem wir **[Layout](https://github.com/nlog/nlog/wiki/Layouts)** unseres targets ändern.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="ColoredConsole" name="ColoredConsole" layout="${message}" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="ColoredConsole" />
  </rules>
</nlog>
```

Wenn du ASF jetzt startest, wirst du feststellen, dass Datum, Level und Logger-Name verschwunden sind - und du nur noch ASF-Nachrichten im Format `Function() Message` hast.

Wir können die Konfiguration auch so ändern, dass sie bei mehr als einem Ziel protokolliert. Lasst uns gleichzeitig `ColoredConsole` und **[File](https://github.com/nlog/nlog/wiki/File-target)** protokollieren.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="ColoredConsole" name="ColoredConsole" />
    <target xsi:type="File" name="File" fileName="NLog.txt" deleteOldFileOnStartup="true" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="ColoredConsole" />
    <logger name="*" minlevel="Debug" writeTo="File" />
  </rules>
</nlog>
```

Und fertig, wir werden jetzt alles über `ColoredConsole` und `File` protokollieren. Hast du bemerkt, dass du auch benutzerdefinierte `fileName` und zusätzliche Optionen angeben kannst?

Schließlich verwendet ASF verschiedene Protokollebenen, um es dir leichter zu machen, zu verstehen, was vor sich geht. Wir können diese Informationen verwenden, um die Schweregrad-Protokollierung zu ändern. Nehmen wir an, wir wollen alles (`Trace`) bis `File` protokollieren, aber nur `Warning` und darüber **[log level](https://github.com/NLog/NLog/wiki/Configuration-file#log-levels)** zur `ColoredConsole`. Wir können das erreichen, indem wir unsere `rules` modifizieren:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="ColoredConsole" name="ColoredConsole" />
    <target xsi:type="File" name="File" fileName="NLog.txt" deleteOldFileOnStartup="true" />
  </targets>

  <rules>
    <logger name="*" minlevel="Warn" writeTo="ColoredConsole" />
    <logger name="*" minlevel="Trace" writeTo="File" />
  </rules>
</nlog>
```

Das war's, jetzt zeigt unsere `ColoredConsole` nur noch Warnungen und darüber, während sie immer noch alles in `File` protokolliert. Du kannst es weiter optimieren, um z.B. nur `Info` und darunter zu protokollieren, und so weiter.

Zuletzt, lasst uns etwas weiter fortgeschrittenes tun und alle Nachrichten in einer Datei protokollieren, aber nur von einem Bot namens `LogBot`.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="ColoredConsole" name="ColoredConsole" />
    <target xsi:type="File" name="LogBotFile" fileName="LogBot.txt" deleteOldFileOnStartup="true" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="ColoredConsole" />
    <logger name="LogBot" minlevel="Trace" writeTo="LogBotFile" />
  </rules>
</nlog>
```

Du kannst sehen, wie wir die ASF-Integration oben verwendet haben und leicht zu unterscheidende Quelle der Nachricht basierend auf der `${logger}` Eigenschaft.

* * *

## Erweiterte Nutzung

Die obigen Beispiele sind ziemlich einfach und sollen dir zeigen, wie einfach es ist, eigene Protokollierungsregeln zu definieren, die mit ASF verwendet werden können. Du kannst NLog für verschiedene Dinge verwenden, einschließlich komplexer Ziele (wie das Führen von Protokollen in `Datenbank`), Protokollrotation (wie das Entfernen alter `File` Protokolle), Verwendung benutzerdefinierter `Layout`s, Deklaration eigener `<when>` Protokollierungsfilter und vieles mehr. Ich empfehle dir, die gesamte **[NLog-Dokumentation](https://github.com/nlog/nlog/wiki/Configuration-file)** durchzulesen, um mehr über jede Option zu erfahren, die dir zur Verfügung steht, damit du das ASF-Logging-Modul so anpassen kannst, wie du willst. Es ist ein wirklich leistungsstarkes Programm und die Anpassung der ASF-Protokollierung war nie einfacher.

* * *

## Einschränkungen

ASF deaktiviert vorübergehend **alle** Regeln, die `ColoredConsole` oder `Console` Targets beinhalten, wenn Benutzereingaben erwartet werden. Wenn du also die Protokollierung für andere Ziele beibehalten möchtest, auch wenn ASF Benutzereingaben erwartet werden, solltest du diese Ziele mit ihren eigenen Regeln definieren, wie in den obigen Beispielen gezeigt, anstatt viele Ziele in `writeTo` der gleichen Regel zu setzen (es sei denn, dies ist dein gewünschtes Verhalten). Die vorübergehende Deaktivierung von Konsole-Zielen wird durchgeführt, um die Konsole sauber zu halten, wenn auf Benutzereingaben gewartet wird.

* * *

## Chat-Protokollierung

ASF bietet erweiterte Unterstützung für das Chat-Logging, indem es nicht nur alle empfangene/gesendete Nachrichten auf `Trace` Logging-Ebene aufzeichnet, sondern auch zusätzliche Informationen zu ihnen in **[Ereigniss-Eigenschaften](https://github.com/NLog/NLog/wiki/EventProperties-Layout-Renderer)** anzeigt. Dies liegt daran, dass wir Chat-Nachrichten ohnehin als Befehle behandeln müssen, so dass es uns nichts kostet, diese Ereignisse zu protokollieren, um es dir zu ermöglichen, zusätzliche Logik hinzuzufügen (z.B. ASF zu deinem persönlichen Steam-Chat-Archiv zu machen).

### Ereignis-Eigenschaften

| Name        | Beschreibung                                                                                                                                                                                                                                 |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Echo        | `bool` Typ. Dies wird auf `true` gesetzt, wenn die Nachricht von uns an den Empfänger gesendet wird, und andernfalls auf `false`.                                                                                                            |
| Message     | `string` Typ. Dies ist die eigentliche gesendete/empfangene Nachricht.                                                                                                                                                                       |
| ChatGroupID | `ulong` Typ. Dies ist die ID des Gruppen-Chats für gesendete/empfangene Nachrichten. Wird `0` sein, wenn kein Gruppen-Chat für die Übertragung dieser Nachricht verwendet wird.                                                              |
| ChatID      | `ulong` Typ. Dies ist die ID des `ChatGroupID` Kanals für gesendete/empfangene Nachrichten. Wird `0` sein, wenn kein Gruppen-Chat für die Übertragung dieser Nachricht verwendet wird.                                                       |
| SteamID     | `ulong` Typ. Dies ist die ID des Steam-Benutzers für gesendete/empfangene Nachrichten. Kann `0` sein, wenn kein bestimmter Benutzer an der Nachrichtenübertragung beteiligt ist (z.B. wenn wir eine Nachricht an einen Gruppen-Chat senden). |

### Beispiel

Dieses Beispiel basiert auf unserem `ColoredConsole` Basis-Beispiel oben. Bevor du versuchst, es zu verstehen, empfehle ich dir dringend, einen Blick auf **[oben](#beispiele)** zu werfen, um zunächst die Grundlagen der NLog-Protokollierung zu erfahren.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target xsi:type="ColoredConsole" name="ColoredConsole" />
    <target xsi:type="File" name="ChatLogFile" fileName="${event-properties:item=ChatGroupID}-${event-properties:item=ChatID}${when:when='${event-properties:item=ChatGroupID}' == 0:inner=-${event-properties:item=SteamID}}.txt" layout="${date:format=yyyy-MM-dd HH\:mm\:ss} ${event-properties:item=Message} ${when:when='${event-properties:item=Echo}' == true:inner=-&gt;:else=&lt;-} ${event-properties:item=SteamID}" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="ColoredConsole" />
    <logger name="MainAccount" level="Trace" writeTo="ChatLogFile">
      <filters>
        <when condition="not starts-with('${message}','OnIncoming') and not starts-with('${message}','SendMessage')" action="Ignore" />
      </filters>
    </logger>
  </rules>
</nlog>
```

Wir haben mit unserem einfachen Beispiel `ColoredConsole` begonnen und es weiter ausgebaut. In erster Linie haben wir eine permanente Chat-Logdatei für jeden Gruppenkanal und Steam-Benutzer erstellt - dies ist möglich dank zusätzlicher Eigenschaften, die ASF uns auf ausgefallene Weise zur Verfügung stellt. Wir haben uns auch für ein benutzerdefiniertes Layout entschieden, das nur das aktuelle Datum, die Nachricht, die gesendete/empfangene Info und den Steam-Benutzer selbst schreibt. Schließlich haben wir unsere Chat-Protokollierungsregel nur für `Trace` Ebene aktiviert, nur für unseren `MainAccount` Bot und nur für Funktionen im Zusammenhang mit der Chat-Protokollierung (`OnIncoming*`, der für den Empfang von Nachrichten und Echos verwendet wird, und `SendMessage*` für das Senden von ASF-Nachrichten).

Das obige Beispiel erzeugt `0-0-76561198069026042.txt` Datei, wenn man mit **[ArchiBoT](https://steamcommunity.com/profiles/76561198069026042)** spricht:

    2018-07-26 01:38:38 how are you doing? -> 76561198069026042
    2018-07-26 01:38:38 /me I'm doing great, how about you? <- 76561198069026042
    

Natürlich ist dies nur ein funktionierendes Beispiel mit ein paar schönen Layout-Tricks, die in praktischer Hinsicht gezeigt werden. Du kannst diese Idee weiter auf deine eigenen Bedürfnisse ausdehnen, wie z.B. zusätzliche Filterung, benutzerdefinierte Reihenfolge, persönliches Layout, Aufnahme nur empfangener Nachrichten und so weiter.

* * *

## ASF-Ziele

Zusätzlich zu den standardmäßigen NLog-Protokollierungszielen (wie z.B. `ColoredConsole` und `File` siehe oben) kannst du auch benutzerdefinierte ASF-Protokollierungsziele verwenden.

Um eine maximale Vollständigkeit zu gewährleisten, erfolgt die Definition der ASF-Ziele nach der NLog-Dokumentationskonvention.

* * *

### SteamTarget

Wie du erraten kannst, verwendet dieses Ziel Steam-Chat-Nachrichten zum Protokollieren von ASF-Nachrichten. Du kannst es so konfigurieren, dass es entweder einen Gruppen-Chat oder einen privaten Chat verwendet. Zusätzlich zur Angabe eines Steam-Ziels für deine Nachrichten kannst du auch `botName` des Bots angeben, der diese senden soll.

Wird in allen von ASF verwendeten Umgebungen unterstützt.

* * *

#### Konfigurations-Syntax

```xml
<targets>
  <target type="Steam"
          name="String"
          layout="Layout"
          chatGroupID="Ulong"
          steamID="Ulong"
          botName="String" />
</targets>
```

Lies mehr über die Verwendung der [Konfigurationsdatei](https://github.com/NLog/NLog/wiki/Configuration-file).

* * *

#### Parameter

##### Allgemeine Optionen

*name* - Name des Ziels.

* * *

##### Layout Optionen

*layout* - Der zu rendernde Text. [Layout](https://github.com/NLog/NLog/wiki/Layouts) Erforderlich. Standard: `${level:uppercase=true}|${logger}|${message}`

* * *

##### SteamTarget Optionen

*chatGroupID* - ID des Gruppen-Chats, der als 64-Bit lange unsignierte Ganzzahl deklariert wurde. Nicht erforderlich. Standardmäßig ist `0` voreingestellt, was die Gruppen-Chat-Funktion deaktiviert und stattdessen privaten Chat verwendet. Wenn aktiviert (auf einen Nicht-Nullwert gesetzt), fungiert die folgende Eigenschaft `steamID` als `chatID` und gibt die ID des Kanals in diesem `chatGroupID` an, an den der Bot Nachrichten senden soll.

*steamID* - SteamID deklariert als 64-Bit lange unsignierte ganze Zahl des Ziel-Steam-Benutzers (wie `SteamOwnerID`), oder Ziel `chatID` (wenn `chatGroupID` eingestellt ist). Erforderlich. Standardwert ist 0, wodurch das Protokollierungsziel vollständig deaktiviert wird.

*botName* - Name des Bots (wie er von ASF erkannt wird, Groß-/Kleinschreibung beachten) des Ziel-Bots, der Nachrichten an `steamID` senden wird, die oben deklariert wurden. Nicht erforderlich. Standardmäßig ist `null` voreingestellt, was automatisch **jeden** aktuell verbundenen Bot auswählt. Es wird empfohlen, diesen Wert entsprechend einzustellen, da `SteamTarget` nicht viele Steam-Einschränkungen berücksichtigt, wie z.B. die Tatsache, dass du `steamID` des Ziels auf deiner Freundeliste haben musst.

* * *

#### SteamTarget Beispiele

Um alle Nachrichten von `Debug` Ebene und darüber, von dem Bot namens `MyBot` zu steamID von `76561198006963719` zu schreiben, solltest du `NLog.config` ähnlich wie unten verwenden:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="https://nlog-project.org/schemas/NLog.xsd" xmlns:xsi="https://www.w3.org/2001/XMLSchema-instance">
  <targets>
    <target type="Steam" name="Steam" steamID="76561198006963719" botName="MyBot" />
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="Steam" />
  </rules>
</nlog>
```

**Hinweis:** Unser `SteamTarget` ist ein benutzerdefiniertes Target, also solltest du sicherstellen, dass du es als `type="Steam"`, NICHT `xsi:type="Steam"` deklarierst, da xsi für offizielle, von NLog unterstützte Ziele reserviert ist.

Wenn du ASF mit `NLog.config` ähnlich wie oben gestartet hast, beginnt `MyBot` dem Steam-Benutzer `76561198006963719` alle üblichen ASF-Protokollmeldungen zu senden. Bedenke, dass `MyBot` verbunden sein muss, um Nachrichten zu senden, damit alle anfänglichen ASF-Nachrichten, die stattfanden, bevor unser Bot sich mit dem Steam-Netzwerk verbinden konnte, nicht weitergeleitet werden.

Natürlich verfügt `SteamTarget` über alle typischen Funktionen, die du von generischem `TargetWithLayout` erwarten kannst, so dass du es in Verbindung mit z.B. benutzerdefinierten Layouts, Namen oder erweiterten Protokollierungsregeln verwenden kannst. Das obige Beispiel ist lediglich ein grundlegendes Beispiel.

* * *

#### Screenshots

![Screenshot](https://i.imgur.com/5juKHMt.png)

* * *

### HistoryTarget

Dieses Target wird intern von ASF verwendet, um eine Protokollhistorie mit fester Größe in `/Api/NLog` Endpunkt von **[ASF API](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC#asf-api)** bereitzustellen, die anschließend von ASF-ui und anderen Programmen verwendet werden kann. Im Allgemeinen solltest du dieses Target nur dann definieren, wenn du bereits eine benutzerdefinierte NLog-Konfiguration für andere Anpassungen verwendest und das Protokoll auch in der ASF-API angezeigt werden soll, z.B. für ASF-ui. Es kann auch angegeben werden, wenn du das Standardlayout oder `maxCount` der gespeicherten Nachrichten ändern möchtest.

Wird in allen von ASF verwendeten Umgebungen unterstützt.

* * *

#### Konfigurations-Syntax

```xml
<targets>
  <target type="History"
          name="String"
          layout="Layout"
          maxCount="Byte" />
</targets>
```

Lies mehr über die Verwendung der [Konfigurationsdatei](https://github.com/NLog/NLog/wiki/Configuration-file).

* * *

#### Parameter

##### Allgemeine Optionen

*name* - Name des Ziels.

* * *

##### Layout Optionen

*layout* - Der zu rendernde Text. [Layout](https://github.com/NLog/NLog/wiki/Layouts) Erforderlich. Standard: `${date:format=yyyy-MM-dd HH\:mm\:ss}|${processname}-${processid}|${level:uppercase=true}|${logger}|${message}${onexception:inner= ${exception:format=toString,Data}}`

* * *

##### HistoryTarget Optionen

*maxCount* - Maximale Anzahl der gespeicherten Protokolle für die Abrufhistorie. Nicht erforderlich. Die Standardeinstellung ist `20`, was eine gute Balance für die Bereitstellung der Anfangshistorie ist, während die Speichernutzung, die sich aus den Speicheranforderungen ergibt, immer noch im Auge behalten wird. Muss größer als `0` sein.

* * *

## Vorbehalt

Achte darauf, wenn du dich entscheidest, `Debug` Logging-Ebene oder darunter in deinem `SteamTarget` mit `steamID` zu kombinieren, das am ASF-Prozess teilnimmt. Dies kann zu einer möglichen `StackOverflowException` führen, da du eine Endlosschleife erzeugst, in der ASF eine gegebene Nachricht empfängt, sie dann durch Steam protokolliert, was zu einer weiteren Nachricht führt, die protokolliert werden muss. Derzeit ist die einzige Möglichkeit dafür, `Trace` Ebene (wo ASF seine eigenen Chat-Nachrichten aufzeichnet), oder `Debug` Ebene zu protokollieren, während ASF auch im `Debug` Modus ausgeführt wird (wo ASF alle Steam-Pakete aufzeichnet).

Kurz gesagt, wenn deine `steamID` am gleichen ASF-Prozess teilnimmt, dann sollte die `minlevel` Logging-Ebene deines `SteamTarget` `Info` (oder `Debug` sein, wenn du auch nicht ASF im `Debug` Modus) und darüber. Alternativ kannst du deine eigenen `<when>` Logging-Filter definieren, um eine unendliche Logging-Schleife zu vermeiden, wenn die Änderung des Levels nicht für deinen Fall geeignet ist. Dies gilt auch für Gruppen-Chats.
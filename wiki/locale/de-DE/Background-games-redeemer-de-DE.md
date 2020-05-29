# Hintergrundproduktschlüsselaktivierer

Der Hintergrundproduktschlüsselaktivierer ist ein speziell eingebautes ASF-Feature, mit dem du einen bestimmten Satz von Steam-Produktschlüsseln (zusammen mit ihren Namen) importieren kannst. Diese können dann im Hintergrund aktiviert werden. Das ist besonders nützlich, wenn du eine große Menge an Produktschlüsseln aktivieren möchtest und du sicherlich den `RateLimited` **[Status](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-de-DE#was-bedeutet-status-beim-einlösen-eines-produktschlüssels?)** erreichst, bevor du mit deiner gesamten Charge fertig bist.

Der Hintergrundproduktschlüsselaktivierer bezieht sich nur auf eine einzelne Bot-Instanz, was bedeutet, dass `RedeemingPreferences` nicht berücksichtigt wird. Dieses Feature kann bei Bedarf zusammen mit dem (oder anstelle vom) `redeem` **[Befehl](https://github. com/JustArchi/ArchiSteamFarm/wiki/Commands-de-DE)** benutzt werden.

* * *

## Import

Der Importvorgang kann auf zwei Arten durchgeführt werden - entweder über eine Datei oder über IPC.

### Datei

ASF erkennt in seinem `config`-Verzeichnis eine Datei mit dem Namen `BotName.keys`, wobei `BotName` der Name deines Bots ist. Diese Datei hat eine erwartete und feste Struktur, bestehend aus dem Namen des Spiels und dessen Produktschlüssel, getrennt von einander durch ein Tab-Zeichen und endend mit einem Zeilenumbruch zur Kennzeichnung des nächsten Eintrags. Wenn mehrere Tabulatoren verwendet werden, dann gilt der erste Eintrag als Spielname, der letzte Eintrag als Produktschlüssel und alles dazwischen wird ignoriert. Zum Beispiel:

    POSTAL 2    ABCDE-EFGHJ-IJKLM
    Domino Craft VR 12345-67890-ZXCVB
    A Week of Circus Terror POIUY-KJHGD-QWERT
    Terraria    DasWirdIgnoriert   DasWirdAuchIgnoriert    ZXCVB-ASDFG-QWERT
    

Alternativ kannst du auch nur Produktschlüssel als Format verwenden (immer noch mit einem Zeilenumbruch zwischen jedem Eintrag). ASF verwendet in diesem Fall die Antwort von Steam (wenn möglich), um den richtigen Namen zu finden. Für jede Art von Produktschlüsselkennzeichnung empfehlen wir dir, deine Produktschlüssel selbst zu benennen, da Pakete, die bei Steam eingelöst werden, nicht der Logik der Spiele folgen müssen, die sie aktivieren, so dass du je nachdem, was der Entwickler angegeben hat, korrekte Spielnamen, benutzerdefinierte Paketnamen (z.B. Humble Indie Bundle 18) oder völlig falsche und möglicherweise sogar gefährliche (z.B. Half-Life 4) sehen kannst.

    ABCDE-EFGHJ-IJKLM
    12345-67890-ZXCVB
    POIUY-KJHGD-QWERT
    ZXCVB-ASDFG-QWERT
    

Egal für welches Format du dich entschieden hast, ASF importiert deine `Produktschlüssel`-Datei entweder beim Start des Bots oder später während der Ausführung. Nach dem erfolgreichen Parsen deiner Datei und dem eventuellen Weglassen ungültiger Einträge werden alle ordnungsgemäß erkannten Spiele der Hintergrundwarteschlange hinzugefügt und die Datei `BotName.keys` selbst wird aus dem Verzeichnis `config` entfernt.

### IPC

Zusätzlich zur Verwendung der oben genannten Produktschlüsseldateien stellt ASF den `GamesToRedeemInBackground` **[ASF API-Endpunkt](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-de-DE#asf-api)** bereit, welcher von jedem IPC-Programm, einschließlich unserem ASF-ui, verwendet werden kann. Die Verwendung von IPC könnte mächtiger sein, da du selbst eine geeignete Syntexanalyse durchführen kannst. Zum Beispiel kannst du ein benutzerdefiniertes Trennzeichen verwenden und bist damit nicht an das Tabulatorzeichen gebunden oder du kannst sogar komplett individuelle Produktschlüssel-Strukturen benutzen.

* * *

## Warteschlange

Sobald die Spiele erfolgreich importiert wurden, werden sie der Warteschlange hinzugefügt. ASF arbeitet sich automatisch durch seine Hintergrundwarteschlange, solange der Bot mit dem Steam-Netzwerk verbunden ist und die Warteschlange nicht leer ist. Ein bei dem Aktivierungsvorgang verwendeter Produktschlüssel, welcher nicht zu einem `RateLimited` führte, wird aus der Warteschlange entfernt und mit seinem Ergebnis ordnungsgemäß in eine Datei im `config` Ordner geschrieben - entweder in `BotName.keys.used`, wenn der Produktschlüssel im Prozess benutzt wurde (z.B. `NoDetail`, `BadActivationCode`, `DuplicateActivationCode`) oder andernfalls `BotName.keys.unused`. ASF verwendet absichtlich den von dir angegebenen Spielenamen, da der Produktschlüssel nicht zwingend einen aussagekräftigen Namen vom Steam-Netzwerk erhält - auf diese Weise kannst du deine Produktschlüssel sogar mit benutzerdefinierten Namen versehen, falls erforderlich/gewünscht.

Wenn während dieses Prozesses unser Konto den Status `RateLimited` erhält, wird die Warteschlange vorübergehend für eine volle Stunde unterbrochen, um die Abklingzeit abzuwarten. Danach wird der Prozess dort fortgesetzt, wo er unterbrochen wurde, und endet, wenn die gesamte Warteschlange leer ist.

* * *

## Beispiel

Nehmen wir an, du hast eine Liste mit 100 Produktschlüsseln. Zuerst solltest du eine neue `BotName.keys.new` Datei im ASF Verzeichnis `config` erstellen. Wir fügen die Erweiterung `.new` hinzu, um ASF wissen zu lassen, dass es diese Datei nicht sofort beim Erstellen einlesen soll (da es sich um eine neue leere Datei handelt, die noch nicht für den Import bereit ist).

Nun kannst du unsere neue Datei öffnen und dort unsere Liste der 100 Produktschlüssel entsprechend der oben beschriebenen Struktur einfügen und bei Bedarf das Format korrigieren. Nach gegebenenfalls nötigen Korrekturen umfasst unsere Datei `BotName.keys.new` genau 100 Zeilen (oder 101, wenn man die letzte Zeile hinzuzählt), wobei jede Zeile als `Spielname\tProduktschlüssel\n` strukturiert ist (wobei `\t` ein Tabulatorzeichen und `\n` ein Zeilenumbruch darstellt).

Nun kannst du diese Datei von `BotName.keys.new` in `BotName.keys` umbenennen, um ASF mitzuteilen, dass sie bereit ist, eingelesen zu werden. In diesem Augenblick importiert ASF die Datei automatisch (ohne Neustart) und löscht sie anschließend, um zu bestätigen, dass alle unsere Spiele eingelesen und der Warteschlange hinzugefügt wurden.

Anstatt die Datei `BotName.keys` zu verwenden, kannst du auch den IPC API-Endpunkt verwenden, oder, wenn du es möchtest, sogar beides kombinieren.

Nach einiger Zeit werden die Dateien `BotName.keys.used` und `BotName.keys.unused` generiert. Diese Dateien enthalten die Ergebnisse unseres Einlöseprozesses. Zum Beispiel kannst du die Datei `BotName.keys.unused` in `BotName2.keys` umbenennen und somit unsere unbenutzten Produktschlüssel an einen anderen Bot weiterreichen, da der vorherige Bot diese Produktschlüssel nicht selbst verwendet hat. Oder du kannst einfach unbenutzte Produktschlüssel in eine andere Datei kopieren und für eine spätere Verwendung aufbewahren. Beachte bitte, dass ASF beim Abarbeiten der Warteschlange neue Einträge in die Dateien `used` und `unused` hinzufügt, wodurch es empfehlenswert ist zu warten, bis die Warteschlange vollständig geleert ist, bevor du diese Dateien verwendest. Falls du doch mal unbedingt auf diese Dateien zugreifen musst, bevor die Warteschlange vollständig geleert ist, solltest du zunächst die Ausgabedatei, auf welche du zugreifen möchtest, in ein anderen Dateiordner **verschieben**, und **dann** parsen. Dies liegt daran, dass ASF einige neue Einträge anhängen kann, während du darauf zugreifst und dies könnte zum Verlust einiger Produktschlüssel führen. Hier ein Beispiel: Du siehst dir eine Datei an, die aktuell 3 Produktschlüssel enthält und löscht sie dann ohne zu merken, dass ASF zwischenzeitlich 4 weitere Produktschlüssel in diese Datei geschrieben hat. Wenn du auf diese Dateien zugreifen möchtest, stelle bitte sicher, dass du sie aus dem ASF Verzeichnis `config` entfernst, bevor du sie liest, zum Beispiel durch Umbenennen.

Es ist auch möglich, zusätzliche Spiele zu importieren, während sich einige Spiele bereits in der Warteschlange befinden, indem du alle oben genannten Schritte wiederholst. ASF wird zusätzliche Einträge ordnungsgemäß in die bereits laufende Warteschlange aufnehmen und sich um sie kümmern.

* * *

## Anmerkungen

Der Hintergrundproduktschlüsselaktivierer verwendet `OrderedDictionary` unter der Haube, was bedeutet, dass deine Produktschlüssel die Reihenfolge beibehalten werden, wie sie in der Datei (oder dem IPC-API-Aufruf) angegeben wurden. Das bedeutet, dass du eine Liste erstellen kannst (und solltest), in der der angegebene Produktschlüssel nur direkte Abhängigkeiten von den weiter oben aufgeführten Produktschlüsseln haben kann, aber nicht von denen weiter unten. Zum Beispiel bedeutet dies, dass, wenn du DLC `D` hast, das erfordert, dass das Spiel `G` zuerst aktiviert wird, dann sollte der Produktschlüssel für das Spiel `G` **immer** vor dem Produktschlüssel für DLC `D` enthalten sein. Dies gilt ebenfalls wenn das DLC `D` abhängig ist von `A`, `B` und `C`, dann sollten alle 3 vorher enthalten sein (in beliebiger Reihenfolge, es sei denn, es gäbe wiederum Abhängigkeiten untereinander).

Wenn du dem obigen Schema nicht folgst, wird dein DLC nicht aktiviert und du erhältst `DoesNotOwnRequiredApp` als Rückmeldung, selbst wenn dein Konto nach dem Durchlaufen der gesamten Warteschlange für die Aktivierung berechtigt wäre. Um dies zu vermeiden musst du sicherstellen, dass dein DLC immer nach dem Basis-Spiel in deiner Warteschlange steht.
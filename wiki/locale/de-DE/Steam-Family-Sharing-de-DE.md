# Steam-Familienbibliothek

ASF unterstützt die Steam-Familienbibliothek seit Version 2.1.5.5.5+. Um zu verstehen wie ASF damit umgeht, solltest du zunächst lesen wie die **[Steam-Familienbibliothek funktioniert](https://store.steampowered.com/promotion/familysharing)**, welches im Steam-Shop verfügbar ist.

* * *

## ASF

Die Unterstützung für das Steam-Familienbibliothek-Feature in ASF ist transparent, d.h. es werden keine neuen Bot/Prozesskonfigurationseigenschaften eingeführt - es funktioniert sofort als zusätzliche integrierte Funktionalität.

ASF beinhaltet eine entsprechende Logik, um zu erkennen, dass die Bibliothek von Familienmitgliedern gesperrt ist, so dass du nicht durch den Start eines Spiels aus der Spielsitzung geworfen wirst. ASF verhält sich genau so wie mit dem primären Konto das die Sperre hält. Daher wird ASF, wenn diese Sperre entweder von deinem Steam-Client oder von einem der Benutzer deiner Familie gehalten wird nicht versuchen, einen Sammelprozess zu starten, sondern darauf warten, dass die Sperre freigegeben wird.

Zusätzlich zu den oben genannten Einstellungen greift ASF nach der Anmeldung auf deine **[Games Sharing-Einstellungen](https://store.steampowered.com/account/managedevices)** zu, aus denen es bis zu 5 `steamIDs` extrahiert, die deine Bibliothek verwenden dürfen. Diese Benutzer erhalten `FamilySharing` Berechtigung zur Verwendung von **[Befehlen](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)**, insbesondere zur Verwendung des `pause~` Befehls auf einem Bot-Account, der Spiele mit diesen teilt, was es ermöglicht, das automatische Karten-Sammel-Modul anzuhalten, um ein Spiel zu starten, das gemeinsam genutzt werden kann.

Die Verbindung beider oben beschriebenen Funktionalitäten ermöglicht es deinen Freunden, den `pause~` Befehl für deinen Karten-Sammelprozess zu starten, ein Spiel zu starten, so lange zu spielen wie sie wollen, dann, nachdem sie fertig gespielt haben, wird der Karten-Sammelprozess automatisch von ASF wieder aufgenommen. Natürlich ist das Senden von `pause~` nicht erforderlich, wenn ASF derzeit keinen aktiven Sammelprozess betreibt, da deine Freunde das Spiel sofort starten können und die oben beschriebene Logik stellt sicher, dass sie nicht aus der Sitzung geworfen werden.

* * *

## Einschränkungen

Das Steam-Netzwerk neigt dazu ASF falsche Statusupdates zu übermitteln. Dies lässt ASF unter Umständen glauben, der Account sei nicht in Nutzung. Die Folge: Der Freund wird frühzeitig aus seiner Spielsitzung geworfen. Dies ist das gleiche Problem, welches von uns bereits in **[diesem FAQ Artikel](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ#asf-is-kicking-my-steam-client-session-while-im-playing--this-account-is-logged-on-another-pc)** beschrieben wurde. Wir empfehlen die gleiche Lösung. Befördern sie ihren Freund zum `Operator` (oder höher) und informieren sie diesen über die Nutzung der `pause` und `resume` Befehle.
# Befehle

ASF unterstützt eine Vielzahl von Befehlen, mit denen das Verhalten der Prozess- und Bot-Instanzen gesteuert werden kann.

Die folgenden Befehle können auf verschiedene Weise an den Bot gesendet werden:

- Durch die interaktive ASF-Konsole
- Durch den privaten Steam-Chat / Gruppen-Chat
- Durch unsere **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-de-DE)** Schnittstelle

Bedenke, dass die ASF-Interaktion von dir verlangt, dass du für den Befehl gemäß den ASF-Berechtigungen berechtigt bist. Weitere Informationen findest du in den Konfigurationseigenschaften `SteamUserPermissions` und `SteamOwnerID`.

Befehle die über den Steam-Chat ausgeführt werden, sind von der **[globalen Konfigurationseigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#commandprefix)** `CommandPrefix` betroffen, welche standardmäßig `!` ist. Das bedeutet, dass du beispielsweise für die Ausführung von dem Befehl `status` tatsächlich `!status` (oder einen benutzerdefinierten `CommandPrefix` deiner Wahl) verwenden musst. `CommandPrefix` ist bei Verwendung von IPC oder der Konsole nicht zwingend erforderlich und kann weggelassen werden.

* * *

### Interaktive Konsole

Beginnend mit V4.0.0.0.9 unterstützt ASF eine interaktive Konsole, die durch die Einrichtung der [**`SteamOwnerID`**](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#steamownerid) Eigenschaft aktiviert werden kann. Anschließend drückst du einfach die `c` Taste um den Befehlsmodus zu aktivieren, gibst deinen Befehl ein und bestätigst mit der Eingabetaste.

![Screenshot](https://i.imgur.com/bH5Gtjq.png)

Die interaktive Konsole ist nicht im [**`Headless`**](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#headless) Modus verfügbar.

* * *

### Steam-Chat

Du kannst den Befehl zum angegebenen ASF-Bot auch über den Steam-Chat ausführen. Natürlich kannst du nicht direkt mit dir selbst schreiben, deshalb brauchst du mindestens ein weiteres Bot-Konto, wenn du Befehle ausführen willst, die auf dein Haupt-Konto abzielen.

![Screenshot](https://i.imgur.com/IvFRJ5S.png)

In ähnlicher Weise kannst du auch den Gruppen-Chat einer bestimmten Steam-Gruppe verwenden. Beachte, dass diese Option die korrekt eingestellte Eigenschaft `SteamMasterClanID` erfordert. In diesem Fall wird der Bot auch im Gruppen-Chat auf Befehle warten (und sich bei Bedarf diesem anschließen). Dies kann auch für "Selbstgespräche" genutzt werden, da diese Variante kein dediziertes Bot-Konto erfordert, im Gegensatz zum privaten Chat. Du kannst einfach die `SteamMasterClanID`-Eigenschaft auf deine neu erstellte Gruppe setzen und dir dann entweder über `SteamOwnerID` oder `SteamUserPermissions` Zugriff auf deinen eigenen Bot gewähren. Auf diese Weise tritt der ASF-Bot (Du) der Gruppe bei und chattet mit der ausgewählten Gruppe und hört auf die Befehle von deinem eigenen Konto. Du kannst dem gleichen Gruppen-Chatroom beitreten, um dir selbst Befehle zu erteilen (Achtung: Wenn eine ASF-Instanz diesem Chatroom beigetreten ist wird diese ebenfalls sämtliche Befehle empfangen, die an diesen Chat gesendet werden, selbst wenn dieser anzeigt, dass nur dein Konto im Chat ist).

Bitte bedenke, dass das Senden eines Befehls an den Gruppen-Chat wie ein Relais funktioniert. Wenn du `redeem X` zu drei deiner Bots sendest, die zusammen mit dir im Gruppen-Chat sind, wird es das gleiche Ergebnis haben, wie wenn du `redeem X` an jeden einzelnen von ihnen privat senden würdest. In den meisten Fällen ist **dies nicht das, was du willst**, und stattdessen solltest du den Befehl `given bot` verwenden, der an **einen einzelnen Bot im privaten Fenster** gesendet wird. ASF unterstützt den Gruppen-Chat, da es in vielen Fällen eine nützliche Quelle für die Kommunikation mit deinem einzigen Bot sein kann, aber du solltest fast nie einen Befehl im Gruppen-Chat ausführen, wenn dort zwei oder mehr ASF-Bots sitzen, es sei denn, du verstehst das hier beschriebene ASF-Verhalten vollständig und du willst tatsächlich den gleichen Befehl an jeden einzelnen Bot weitergeben, der auf dich hört.

*And even in this case you should use private chat with `[Bots]` syntax instead.*

* * *

### IPC

Die fortschrittlichste und flexibelste Art der Befehlsausführung. Perfekt geeignet für die Benutzerinteraktion (ASF-ui), für Drittanbieter-Programme oder Skripte (ASF-API). Diese Variante erfordert, dass ASF im `IPC` Modus ausgeführt wird und einen Client der Befehle über die **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-de-DE)** Schnittstelle ausführt.

![Screenshot](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/commands.png)

* * *

## Befehle

| Befehl                                                               | Zugriff         | Beschreibung                                                                                                                                                                                                                                                                                                                                                             |
| -------------------------------------------------------------------- | --------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `2fa [Bots]`                                                         | `Master`        | Generiert temporäre **[2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE)**-Codes der angegebenen Bot-Instanzen.                                                                                                                                                                                                                  |
| `2fano [Bots]`                                                       | `Master`        | Lehnt alle ausstehenden **[2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE)**-Bestätigungen der angegebenen Bot-Instanzen ab.                                                                                                                                                                                                   |
| `2faok [Bots]`                                                       | `Master`        | Akzeptiert alle ausstehenden **[2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE)**-Bestätigungen der angegebenen Bot-Instanzen.                                                                                                                                                                                                 |
| `addlicense [Bots] <Licenses>`                                 | `Operator`      | Aktiviert angegebene `Lizenzen`, wie **[unten](#addlicense-licenses)** erklärt, auf den angegebenen Bot-Instanzen (nur kostenlose Spiele).                                                                                                                                                                                                                               |
| `balance [Bots]`                                                     | `Master`        | Zeigt das Guthaben der angegebenen Bot-Instanzen an.                                                                                                                                                                                                                                                                                                                     |
| `bgr [Bots]`                                                         | `Master`        | Gibt Informationen über **[BGR](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer)** Warteschlange der angegebenen Bot-Instanzen aus.                                                                                                                                                                                                        |
| `bl [Bots]`                                                          | `Master`        | Listet Benutzer aus dem Handelsmodul der gegebenen Bot-Instanzen auf, die auf der schwarzen Liste stehen.                                                                                                                                                                                                                                                                |
| `bladd [Bots] <SteamIDs64>`                                    | `Master`        | Setzt gegebene `steamIDs` auf die Schwarze Liste des Handelsmodul der gegebenen Bot-Instanzen.                                                                                                                                                                                                                                                                           |
| `blrm [Bots] <SteamIDs64>`                                     | `Master`        | Entfernt gegebene `steamIDs` von der schwarzen Liste des Handelsmoduls der gegebenen Bot-Instanzen.                                                                                                                                                                                                                                                                      |
| `exit`                                                               | `Besitzer`      | Stoppt den kompletten ASF-Prozess.                                                                                                                                                                                                                                                                                                                                       |
| `farm [Bots]`                                                        | `Master`        | Startet das Karten-Sammel-Modul für gegebene Bot-Instanzen neu.                                                                                                                                                                                                                                                                                                          |
| `help`                                                               | `FamilySharing` | Zeigt Hilfe an (Link zu dieser Seite).                                                                                                                                                                                                                                                                                                                                   |
| `input [Bots] <Type> <Value>`                            | `Master`        | Setzt den gegebenen Eingabetyp auf den gegebenen Wert für gegebene Bot-Instanzen. Funktioniert nur im `Headless`-Modus, genauer erklärt **[unten](#input-befehl)**.                                                                                                                                                                                                      |
| `ib [Bots]`                                                          | `Master`        | Listet Anwendungen auf, die vom automatischen Sammeln von gegebenen Bot-Instanzen ausgeschlossen sind.                                                                                                                                                                                                                                                                   |
| `ibadd [Bots] <AppIDs>`                                        | `Master`        | Fügt gegebene `appIDs` zur schwarzen Liste der gegebenen Bot-Instanzen hinzu, die vom automatischen Sammeln ausgeschlossen sind.                                                                                                                                                                                                                                         |
| `ibrm [Bots] <AppIDs>`                                         | `Master`        | Entfernt gegebene `appIDs` von der schwarzen Liste der gegebenen Bot-Instanzen, die vom automatischen Sammeln ausgeschlossen sind.                                                                                                                                                                                                                                       |
| `iq [Bots]`                                                          | `Master`        | Listet die Priorität der Sammel-Warteschlange der gegebenen Bot-Instanzen auf.                                                                                                                                                                                                                                                                                           |
| `iqadd [Bots] <AppIDs>`                                        | `Master`        | Fügt gegebene `appIDs` zur Priorität Sammel-Warteschlange der gegebenen Bot-Instanzen hinzu.                                                                                                                                                                                                                                                                             |
| `iqrm [Bots] <AppIDs>`                                         | `Master`        | Entfernt gegebene `appIDs` aus der Priorität Sammel-Warteschlange der gegebenen Bot-Instanzen.                                                                                                                                                                                                                                                                           |
| `level [Bots]`                                                       | `Master`        | Gibt das Steam-Level der angegebenen Bot-Instanzen an.                                                                                                                                                                                                                                                                                                                   |
| `loot [Bots]`                                                        | `Master`        | Sendet alle `LootableTypes` Steam-Community-Gegenstände gegebener Bot-Instanzen an `Master` welcher in den `SteamUserPermissions` (mit niedrigster steamID, wenn mehr als eine) definiert ist.                                                                                                                                                                           |
| `loot@ [Bots] <RealAppIDs>`                                    | `Master`        | Sendet alle `LootableTypes` Steam-Community-Gegenstände, die mit den angegebenen `RealAppIDs` der angegebenen Bot-Instanzen übereinstimmen, an `Master` welcher in den `SteamUserPermissions` (mit der niedrigsten SteamID, wenn mehr als eine) definiert ist. Dies ist das Gegenteil von `loot%`.                                                                       |
| `loot% [Bots] <RealAppIDs>`                                    | `Master`        | Sendet alle `LootableTypes` Steam-Community-Gegenstände, abgesehen von den angegebenen `RealAppIDs` der bestimmten Bot-Instanzen, an dem vom Nutzer definiertem `Master` in den `SteamUserPermissions` (der mit der niedrigsten SteamID, wenn mehr als eine definiert is). Dies ist das Gegenteil von `loot@`.                                                           |
| `loot^ [Bots] <AppID> <ContextID>`                       | `Master`        | Sendet alle Steam-Gegenstände von gegebenem `AppID` in `ContextID` der angegebenen Bot-Instanzen an `Master` welcher in den `SteamUserPermissions` (mit niedrigster SteamID wenn mehr als eine) definiert ist.                                                                                                                                                           |
| `nickname [Bots] <Nickname>`                                   | `Master`        | Ändert den Steam-Namen gegebener Bot-Instanzen auf gegeben `nickname`.                                                                                                                                                                                                                                                                                                   |
| `owns [Bots] <Games>`                                          | `Operator`      | Prüft, ob die Bot-Instanzen bereits im Besitz von `games`, wie **[unten](#owns-games)** erklärt.                                                                                                                                                                                                                                                                         |
| `password [Bots]`                                                    | `Master`        | Gibt das verschlüsselte Passwort der gegebenen Bot-Instanzen an (in Verwendung mit `PasswordFormat`).                                                                                                                                                                                                                                                                    |
| `pause [Bots]`                                                       | `Operator`      | Pausiert permanent das automatische Karten-Sammel-Modul der gegebenen Bot-Instanzen. ASF wird nicht versuchen, das aktuelle Konto in dieser Sitzung zu verwalten, es sei denn, du setzt `resume` manuell ein oder startest den Prozess neu.                                                                                                                              |
| `pause~ [Bots]`                                                      | `FamilySharing` | Pausiert vorübergehend das automatische Karten-Sammel-Modul der gegebenen Bot-Instanzen. Das Sammeln wird beim nächsten Wiedergabe-Ereignis oder beim Trennen der Bot-Verbindung automatisch wieder fortgesetzt. Du kannst `resume` verwenden, um es zu deaktivieren.                                                                                                    |
| `pause& [Bots] <Seconds>`                                  | `Operator`      | Pausiert vorübergehend das automatische Karten-Sammel-Modul gegebener Bot-Instanzen für eine bestimmte Anzahl von `seconds`. Nach einer Verzögerung wird das Karten-Sammel-Modul automatisch wieder aktiviert.                                                                                                                                                           |
| `play [Bots] <AppIDs,GameName>`                                | `Master`        | Wechselt zu manuellem Sammeln - startet gegebene `AppIDs` auf gegebenen Bot-Instanzen, optional auch mit benutzerdefiniertem `GameName`. Damit diese Funktion richtig funktioniert, **muss** dein Steam-Konto eine gültige Lizenz für alle `AppIDs` besitzen, die du hier angibst, dies schließt auch F2P-Spiele ein. Benutze `reset` oder `resume` um zurück zu kehren. |
| `privacy [Bots] <Settings>`                                    | `Master`        | Ändert die **[Steam Privatsphäre-Einstellungen](https://steamcommunity.com/my/edit/settings)** der gegebenen Bot-Instanzen, zu entsprechend ausgewählten Optionen erklärt **[unten](#privacy-einstellungen)**.                                                                                                                                                           |
| `redeem [Bots] <Keys>`                                         | `Operator`      | Löst die angegebenen Produktschlüssel oder Guthaben-Codes auf den angegebenen Bot-Instanzen ein.                                                                                                                                                                                                                                                                         |
| `redeem^ [Bots] <Modes> <Keys>`                          | `Operator`      | Löst gegebene `Produktschlüssel oder Guthaben-Codes` auf gegebenen Bot-Instanzen ein, indem er gegebene `Modi` verwendet, die **[unten](#redeem-modi)** erklärt werden.                                                                                                                                                                                                  |
| `reset [Bots]`                                                       | `Master`        | Setzt den Spielstatus auf normal zurück, wird beim manuellen Farmen via `play` Befehl verwendet.                                                                                                                                                                                                                                                                         |
| `restart`                                                            | `Owner`         | Startet den ASF-Prozess neu.                                                                                                                                                                                                                                                                                                                                             |
| `resume [Bots]`                                                      | `FamilySharing` | Setzt das automatische Sammeln der gegebenen Bot-Instanzen fort. Siehe auch `pause`, `play`.                                                                                                                                                                                                                                                                             |
| `start [Bots]`                                                       | `Master`        | Startet gegebene Bot-Instanzen.                                                                                                                                                                                                                                                                                                                                          |
| `stats`                                                              | `Owner`         | Gibt Prozessstatistiken an, wie z.B. die Nutzung des verwalteten Speichers.                                                                                                                                                                                                                                                                                              |
| `status [Bots]`                                                      | `FamilySharing` | Gibt den Status der gegebenen Bot-Instanzen an.                                                                                                                                                                                                                                                                                                                          |
| `stop [Bots]`                                                        | `Master`        | Stoppt gegebene Bot-Instanzen.                                                                                                                                                                                                                                                                                                                                           |
| `transfer [Bots] <TargetBot>`                                  | `Master`        | Sendet alle `TransferableTypes` Steam Community-Gegenstände von gegebenen Bot-Instanzen an den Ziel-Bot-Instanz.                                                                                                                                                                                                                                                         |
| `transfer@ [Bots] <RealAppIDs> <TargetBot>`              | `Master`        | Sendet alle `TransferableTypes` Steam-Community-Gegenstände, die mit den gegebenen `RealAppIDs` übereinstimmen, von gegebenen Bot-Instanzen an den Ziel-Bot-Instanz. Dies ist das Gegenteil von `transfer%`.                                                                                                                                                             |
| `transfer% [Bots] <RealAppIDs> <TargetBot>`              | `Master`        | Sendet alle `TransferableTypes` Steam-Community-Gegenstände, abgesehen von den angegebenen `RealAppIDs` von bestimmten Bot-Instanzen zu der Ziel-Bot-Instanz. Dies ist das Gegenteil von `transfer@`.                                                                                                                                                                    |
| `transfer^ [Bots] <AppID> <ContextID> <TargetBot>` | `Master`        | Sendet alle Steam-Gegenstände von gegebenem `AppID` in `ContextID` der gegebenen Bot-Instanzen an die Ziel-Bot-Instanz.                                                                                                                                                                                                                                                  |
| `unpack [Bots]`                                                      | `Master`        | Entpackt alle Booster Packs, die im Inventar der angegebenen Bot-Instanzen vorhanden sind.                                                                                                                                                                                                                                                                               |
| `update`                                                             | `Owner`         | Überprüft GitHub auf ASF-Aktualisierungen (dies geschieht automatisch alle `UpdatePeriod`).                                                                                                                                                                                                                                                                              |
| `version`                                                            | `FamilySharing` | Zeigt die ASF-Version an.                                                                                                                                                                                                                                                                                                                                                |

* * *

### Anmerkungen

Bei Befehlen selbst ist die Groß- und Kleinschreibung egal, aber bei deren Argumenten (wie zum Beispiel Bot-Namen) muss die Groß- und Kleinschreibung beachtet werden.

`[Bots]` argument is optional in all commands. Wenn verwendet, wird der Befehl auf den angegebenen Bots ausgeführt. Ohne diese Angabe wird der Befehl auf dem Bot ausgeführt, der den Befehl erhält. Mit anderen Worten: Wenn `status A` an Bot `B` gesendet wird ist es dasselbe wie wenn `status` an Bot `A` gesendet wird. Bot `B` dient in diesem Fall nur als Proxy. Dies kann auch zum Senden von Befehlen an Bots verwendet werden, die andernfalls nicht verfügbar sind. Zum Beispiel das Starten angehaltener Bots oder die Ausführung von Aktionen auf Ihrem Hauptkonto (welches Sie für die Ausführung der Befehle verwenden).

**Zugriff** eines Befehls definiert die **minimale** `EPermission` von `SteamUserPermissions`, die für die Verwendung des Befehls erforderlich ist - mit Ausnahme von `Owner` der als `SteamOwnerID` in der globalen Konfigurationsdatei (und damit höchste verfügbare Berechtigung) definiert ist.

Plural arguments, such as `[Bots]`, `<Keys>` or `<AppIDs>` mean that command supports multiple arguments of given type, separated by a comma. For example, `status [Bots]` can be used as `status MyBot,MyOtherBot,Primary`. Dies führt dazu, dass der angegebene Befehl auf **alle Ziel-Bots** auf die gleiche Weise ausgeführt wird, wie wenn du `status` an jeden Bot in einem separaten Chat-Fenster senden würdest. Bitte beachte, dass nach `,` kein Leerzeichen steht.

ASF verwendet alle Whitespace-Zeichen als mögliche Trennzeichen für einen Befehl, wie Leerzeichen und Zeilenumbrüche. Das bedeutet, dass du zur Begrenzungen deiner Argumente keine Leerzeichen verwenden musst, du kannst auch jedes andere Whitespace-Zeichen (z.B. Tabulator oder Zeilenumbruch) verwenden.

ASF wird zusätzliche "Out-of-Range"-Argumente mit dem Plural-Typ des letzten "In-Range"-Arguments "verbinden". This means that `redeem bot key1 key2 key3` for `redeem [Bots] <Keys>` will work exactly the same as `redeem bot key1,key2,key3`. Da ein Zeilenumbruch als Trennzeichen verwendet werden kann ermöglicht dir dies, `redeem bot` zu schreiben und dann eine Liste von Produktschlüsseln einzufügen, die durch ein beliebiges zulässiges Trennzeichen (z.B. Zeilenumbruch) oder dem Standard `,` ASF-Trennzeichen getrennt sind. Keep in mind that this trick can be used only for command variant that uses the most amount of arguments (so specifying `[Bots]` is mandatory in this case).

Wie du oben gelesen hast, wird ein Leerzeichen als Trennzeichen für einen Befehl verwendet, daher kann es nicht in Argumenten verwendet werden. Allerdings kann ASF auch, wie oben erwähnt, "Out-of-Range"-Argumente verbinden, was bedeutet, dass du tatsächlich ein Leerzeichen in Argumenten verwenden kannst, das als letztes für den angegebenen Befehl definiert ist. Zum Beispiel, `nickname bob Great Bob` wird den Benutzernamen des Bots `bob` richtig auf "Great Bob" setzen. Auf die gleiche Weise kannst du Namen, die Leerzeichen enthalten, im Befehl `owns` überprüfen.

* * *

Für einige Befehle sind auch Aliase verfügbar, um dir Zeit beim tippen zu sparen:

| Befehl       | Alias |
| ------------ | ----- |
| `owns ASF`   | `oa`  |
| `status ASF` | `sa`  |
| `redeem`     | `r`   |
| `redeem^`    | `r^`  |

* * *

### `[Bots]` argument

`[Bots]` argument is a special variant of plural argument, as in addition to accepting multiple values it also offers extra functionality.

Es gibt zunächst ein spezielles Schlüsselwort `ASF`, das als "alle Bots im Prozess" fungiert, so dass der Befehl `status ASF` gleich `status all,your,bots,listed,here` ist. Dies kann auch verwendet werden, um die Bots zu identifizieren, auf die du Zugriff hast, da das `ASF` Schlüsselwort, trotz der Ausrichtung auf alle Bots, nur von den Bots eine Antwort generiert, an die du den Befehl tatsächlich senden kannst.

`[Bots]` argument supports special "range" syntax, which allows you to choose a range of bots more easily. The general syntax for `[Bots]` in this case is `firstBot..lastBot`. Wenn du zum Beispiel Bots mit den Namen `A, B, C, D, E, F` hast, kannst du `status B..E` ausführen, was in diesem Fall gleich `status B,C,D,E` ist. Bei Verwendung dieser Syntax verwendet ASF die alphabetische Sortierung, um festzustellen, welche Bots sich in dem von dir angegebenen Bereich befinden. Sowohl `ersterBot` als auch `letzterBot` müssen gültige Bot-Namen sein, die von ASF erkannt werden, da sonst die Bereichs-Syntax vollständig übersprungen wird.

In addition to range syntax above, `[Bots]` argument also supports **[regex](https://en.wikipedia.org/wiki/Regular_expression)** matching. Du kannst Regex Muster aktivieren, indem du `r!<pattern>` als Bot-Namen verwendest, wobei `r!` der ASF-Aktivator für den Regex Abgleich ist, und `<pattern>` dein Regex-Muster ist. Ein Beispiel für einen regex-basierten Bot-Befehl wäre `status r!\d{3}`, der den Befehl `status` an Bots sendet, die einen aus 3 Ziffern bestehenden Namen haben (z.B. `123` und `981`). Zögere nicht einen Blick auf die **[Dokumentation](https://docs.microsoft.com/dotnet/standard/base-types/regular-expression-language-quick-reference)** zu werfen, um weitere Erklärungen und Beispiele für verfügbare Regex-Muster zu erhalten.

* * *

## `privacy` Einstellungen

`<Settings>` Argument akzeptiert **bis zu 7** verschiedene Optionen, getrennt wie üblich durch das standard ASF-Trennzeichen, das Komma. Diese sind, in der Reihenfolge:

| Argument | Name           | Child von  |
| -------- | -------------- | ---------- |
| 1        | Profile        |            |
| 2        | OwnedGames     | Profile    |
| 3        | Playtime       | OwnedGames |
| 4        | FriendsList    | Profile    |
| 5        | Inventory      | Profile    |
| 6        | InventoryGifts | Inventory  |
| 7        | Comments       | Profile    |

Für eine Beschreibung der oben genannten Felder besuche bitte die **[Steam Privatsphäre-Einstellungen](https://steamcommunity.com/my/edit/settings)**.

Für alle Argumente sind folgende Werte gültig:

| Wert | Name          |
| ---- | ------------- |
| 1    | `Private`     |
| 2    | `FriendsOnly` |
| 3    | `Public`      |

Du kannst entweder den case-insensitiven Namen oder den numerischen Wert verwenden. Argumente, die weggelassen wurden, werden standardmäßig auf `Private` gesetzt. Es ist wichtig, die Beziehung zwischen Kind und Eltern von oben genannten Argumenten zu beachten, da das Kind nie mehr offene Berechtigung haben kann als sein Elternteil. Zum Beispiel kannst du Spiele im Besitz **nicht** auf `Public` haben, während dein Profil auf `Private` steht.

### Beispiel

Wenn du **alle** Privatsphäre-Einstellungen deines Bots namens `Main` auf `Private` setzen möchtest, kannst du jede der folgenden Optionen verwenden:

    privacy Main 1
    privacy Main Private
    

Dies liegt daran, dass ASF automatisch alle anderen Einstellungen als `Private` ansieht, so dass es nicht notwendig ist, sie anzugeben. Wenn du dagegen alle Privatsphäre-Einstellungen auf `Public` setzen möchtest, dann musst du eine der folgenden Optionen verwenden:

    privacy Main 3,3,3,3,3,3,3
    privacy Main Public,Public,Public,Public,Public,Public,Public
    

Auf diese Weise kannst du auch einzelne Einstellungen so vornehmen, wie du willst:

    privacy Main Public,FriendsOnly,Private,Public,Public,Private,Public
    

Das oben genannte wird das Profil auf öffentlich setzen, eigene Spiele auf nur für Freunde, Spielzeit auf privat, Freundesliste auf öffentlich, Inventar auf öffentlich, Inventar Geschenke auf privat und Profil-Kommentare auf öffentlich. Das Gleiche kann man mit numerischen Werten erreichen, wenn man dies möchte.

Bedenke, dass ein Kind nie mehr offene Berechtigungen haben kann als sein Elternteil. Siehe hierzu die Argumentationsbeziehung für die verfügbaren Optionen.

* * *

## `addlicense` Lizenzen

`addlicense` Befehl unterstützt zwei verschiedene Lizenztypen, diese sind:

| Typ   | Alias | Beispiel     | Beschreibung                                                                      |
| ----- | ----- | ------------ | --------------------------------------------------------------------------------- |
| `app` | `a`   | `app/292030` | Spiel bestimmt durch seine einzigartige `appID`.                                  |
| `sub` | `s`   | `sub/47807`  | Paket mit einem oder mehreren Spielen, bestimmt durch seine einzigartige `subID`. |

Die Differenzierung ist wichtig, da ASF die Steam Netzwerk Aktivierung für Apps und die Steam Shop Aktivierung für Pakete verwenden wird. Those two are not compatible with each other, typically you'll use apps for free weekends and permanently F2P games, and packages otherwise.

We recommend to explicitly define the type of each entry in order to avoid ambiguous results, but for the backwards compatibility, if you supply invalid type or omit it entirely, ASF will assume that you ask for `sub` in this case. You can also query one or more of the licenses at the same time, using standard ASF `,` delimiter.

Beispiel für einen vollständigen Befehl:

    addlicense ASF app/292030,sub/47807
    

* * *

## `owns` Spiele

`owns` command supports several different game types for `<games>` argument that can be used, those are:

| Typ     | Alias | Beispiel         | Beschreibung                                                                                                                                                                                                                                                            |
| ------- | ----- | ---------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `app`   | `a`   | `app/292030`     | Spiel bestimmt durch seine einzigartige `appID`.                                                                                                                                                                                                                        |
| `sub`   | `s`   | `sub/47807`      | Paket mit einem oder mehreren Spielen, bestimmt durch seine einzigartige `subID`.                                                                                                                                                                                       |
| `regex` | `r`   | `regex/^\d{4}:` | **[Regex](https://en.wikipedia.org/wiki/Regular_expression)** applying to the game's name, case-sensitive. See the **[docs](https://docs.microsoft.com/dotnet/standard/base-types/regular-expression-language-quick-reference)** for complete syntax and more examples. |
| `name`  | `n`   | `name/Witcher`   | Part of the game's name, case-insensitive.                                                                                                                                                                                                                              |

We recommend to explicitly define the type of each entry in order to avoid ambiguous results, but for the backwards compatibility, if you supply invalid type or omit it entirely, ASF will assume that you ask for `app` if your input is a number, and `name` otherwise. You can also query one or more of the games at the same time, using standard ASF `,` delimiter.

Beispiel für einen vollständigen Befehl:

    owns ASF app/292030,name/Witcher
    

* * *

## `redeem^` Modi

Der Befehl `redeem^` ermöglicht es dir, die Modi zu optimieren, die für ein einzelnes Einlöse-Szenario verwendet werden. Dazu wird temporär die `RedeemingPreferences` **[Bot-Konfigurationseigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#redeemingpreferences)** überschrieben.

`<Modes>` Argument akzeptiert mehrere Modus-Werte, die, wie üblich, durch ein Komma getrennt werden. Die verfügbaren Modus-Werte sind im Folgenden aufgeführt:

| Wert | Name                  | Beschreibung                                                                                                  |
| ---- | --------------------- | ------------------------------------------------------------------------------------------------------------- |
| FAWK | ForceAssumeWalletKey  | Erzwingt `AssumeWalletKeyOnBadActivationCode` Einlöseeinstellungen zu aktivieren                              |
| FD   | ForceDistributing     | Erzwingt die Aktivierung der `Distributing` Einlöse-Präferenz                                                 |
| FF   | ForceForwarding       | Erzwingt die Aktivierung der `Forwarding` Einlöse-Präferenz                                                   |
| FKMG | ForceKeepMissingGames | Erzwingt die Aktivierung der `KeepMissingGames` Einlöse-Präferenz                                             |
| SAWK | SkipAssumeWalletKey   | Erzwingt `AssumeWalletKeyOnBadActivationCode` Einlöseeinstellungen zu deaktivieren                            |
| SD   | SkipDistributing      | Erzwingt die Deaktivierung der `Distributing` Einlöse-Präferenz                                               |
| SF   | SkipForwarding        | Erzwingt die Deaktivierung der `Forwarding` Einlöse-Präferenz                                                 |
| SI   | SkipInitial           | Überspringt die Produktschlüssel-Aktivierung beim ersten Bot                                                  |
| SKMG | SkipKeepMissingGames  | Erzwingt die Deaktivierung der `KeepMissingGames` Einlöse-Präferenz                                           |
| V    | Validate              | Überprüft die Produktschlüssel auf das richtige Format und überspringt automatisch ungültige Produktschlüssel |

Zum Beispiel möchten wir drei Produktschlüssel auf einem unserer Bots einlösen, der noch keine Spiele besitzt, aber nicht auf unserem `primary` Bot. Um das zu erreichen, können wir Folgendes nutzen:

`redeem^ primary FF,SI key1,key2,key3`

Es ist wichtig zu beachten, dass fortgeschrittenes Einlösen nur die `RedeemingPreferences` überschreibt, die du **im Befehl angeben hast**. Wenn du zum Beispiel `Distributing` in deinen `RedeemingPreferences` aktiviert hast, dann macht es keinen Unterschied, ob du den `FD` Modus verwendest oder nicht, weil die Verteilung trotzdem bereits aktiv ist, aufgrund der `RedeemingPreferences` die du verwendest. Deshalb hat jede zwangsweise aktivierte Überschreibung auch eine zwangsweise deaktivierte. Du kannst selbst entscheiden, ob du es vorziehst deaktivierte mit aktiviert zu überschreiben oder umgekehrt.

* * *

## `input` Befehl

Der Input-Befehl kann nur im `Headless`-Modus verwendet werden, um die angegebenen Daten über **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-de-DE)** oder Steam-Chat einzugeben, wenn ASF ohne Unterstützung für Benutzerinteraktion läuft.

General syntax is `input [Bots] <Type> <Value>`.

`<Type>` ist Groß-/Kleinschreibung unabhängig und definiert einen von ASF unterstützten Eingabetyp. Derzeit unterstützt ASF folgende Typen:

| Typ                     | Beschreibung                                                                              |
| ----------------------- | ----------------------------------------------------------------------------------------- |
| DeviceID                | 2FA Geräte-Identifikation, wenn sie in `.maFile` fehlt.                                   |
| Login                   | `SteamLogin` Bot-Konfigurationseigenschaft, wenn sie in der Konfiguration fehlt.          |
| Password                | `SteamPassword` Bot-Konfigurationseigenschaft, wenn sie in der Konfiguration fehlt.       |
| SteamGuard              | Auth-Code, der an deine E-Mail gesendet wird, wenn du 2FA nicht benutzt.                  |
| SteamParentalCode       | ` SteamParentalCode ` Bot-Konfigurationseigenschaft, wenn sie in der Konfiguration fehlt. |
| TwoFactorAuthentication | 2FA-Code, der von deinem Handy generiert wurde, wenn du 2FA benutzt, aber nicht ASF 2FA.  |

`<Value>` ist der Wert, der für einen angegebenen Typ gesetzt werden soll. Derzeit sind alle Werte Zeichenketten.

### Beispiel

Lass uns annehmen, dass wir einen Bot haben, der durch SteamGuard (nicht im Zwei-Faktor-Modus) geschützt wird. Wir wollen diesen Bot starten während das Konfigurationsfeld `Headless` auf wahr gesetzt ist.

Um das zu tun müssen wir folgende Befehle ausführen:

`start MeinSteamGuardBot` -> Der Bot wird versuchen zu starten, was allerdings fehlschlagen wird, weil ein Authentifizierungscode benötigt wird. Dann wird er sich selbst stoppen, weil ASF im `Headless`-Modus läuft. Wir brauchen dies, damit das Steam-Netzwerk uns den Authentisierungscode an unsere E-Mail sendet - wenn es keinen Bedarf dafür gibt, würden wir diesen Schritt komplett überspringen.

`input MeinSteamGuardBot SteamGuard ABCDE` -> Wir setzen den `SteamGuard`-Input von `MeinSteamGuardBot` auf `ABCDE`. Natürlich sollte `ABCDE` der Code sein, den du in deiner E-Mail erhalten hast.

`start MeinSteamGuardBot` -> wir starten unseren (gestoppten) Bot wieder. Diesmal wird der Code benutzt, den wir im vorherigen Befehl gesetzt haben und der Bot loggt sich ein und löscht den Code aus seinem internen Speicher.

Auf dem selben Weg können wir auf Bots, die durch Zwei-Faktor-Authentifizierung geschützt sind (und nicht die 2FA von ASF verwenden), zugreifen und andere Dinge während der Laufzeit tun.
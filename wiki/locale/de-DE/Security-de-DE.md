# Sicherheit

## SteamPassword

ASF unterstützt derzeit 4 Arten von Passwörtern - `PlainText`, `AES`, `ProtectedDataForCurrentUser` und Keins (`null` / `""`).

Um ein verschlüsseltes Passwort zu verwenden, solltest du dich zunächst wie gewohnt mit `PlainText` bei Steam anmelden und dann verschlüsselte Passwörter mit dem `password` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** generieren. Wähle die gewünschte Verschlüsselungsmethode aus, gib dann das verschlüsselte Passwort das du erhalten hast als `SteamPassword` Bot-Konfigurationseigenschaft ein und vergiss schließlich nicht `PasswordFormat` auf denjenigen Wert zu ändern, die deiner gewählten Verschlüsselungsmethode entspricht.

* * *

### PlainText

Dies ist die einfachste und zugleich unsicherste Methode das Passwort zu speichern, definiert als `PasswordFormat` mit einem Wert von `0`. ASF erwartet, dass die Eigenschaft `SteamPassword` ein einfacher Text ist - das Passwort wird verwendet um sich bei Steam in seiner direkten Form anzumelden. Es ist das am einfachsten zu bedienende und 100% kompatible mit allen Systemen, daher ist es der Standard.

* * *

### AES

Nach heutigen Standards als sicher angesehen, wird die Methode **[AES](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard)** zum Speichern des Passworts in Form von `PasswordFormat` mit einem Wert von `1` definiert. ASF erwartet, dass `SteamPassword` eine **[base64-kodierte](https://en.wikipedia.org/wiki/Base64)** Zeichenfolge ist, die nach der Übersetzung zu einem AES-verschlüsselten Byte-Array führt, das dann mit dem enthaltenen **[Initialisierungsvektor](https://en.wikipedia.org/wiki/Initialization_vector)** und dem ASF-Verschlüsselungsschlüssel entschlüsselt werden sollte.

Die obige Methode garantiert Sicherheit, solange der Angreifer den eingebauten ASF-Verschlüsselungsschlüssel nicht kennt, der sowohl zur Entschlüsselung als auch zur Verschlüsselung von Passwörtern verwendet wird. ASF erlaubt es dir, den Schlüssel mit dem **[Befehlszeilenargument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments-de-DE)** `--cryptkey` anzugeben, den du für maximale Sicherheit verwenden solltest. Wenn du dich dazu entscheidest es wegzulassen, verwendet ASF seinen eigenen Schlüssel, der **bekannt** und fest in der Anwendung programmiert ist, was bedeutet, dass jeder die ASF-Verschlüsselung umkehren und ein entschlüsseltes Passwort erhalten kann. Es erfordert immer noch etwas Aufwand und ist nicht so einfach zu bewerkstelligen, aber möglich, deshalb solltest du fast immer `AES` Verschlüsselung mit deinem eigenen `--cryptkey` verwenden, den du geheim halten solltest. Die in ASF verwendete AES-Methode bietet eine zufriedenstellende Sicherheit und es ist ein Kompromiss zwischen der Einfachheit von `PlainText` und der Komplexität von `ProtectedDataForCurrentUser`, aber es wird dringend empfohlen, sie mit benutzerdefinierten `--cryptkey` zu verwenden.

* * *

### ProtectedDataForCurrentUser

Die derzeit sicherste Art, die von ASF angeboten wird, das Passwort zu speichern und viel sicherer als die oben beschriebene Methode `AES`, ist definiert als `PasswordFormat` mit einem Wert von `2`. Der Hauptvorteil dieser Methode ist gleichzeitig der größte Nachteil - anstelle der Verwendung eines Verschlüsselungsschlüssels (wie in `AES`) werden die Daten mit den Anmeldeinformationen des aktuell angemeldeten Benutzers verschlüsselt, was bedeutet, dass es möglich ist die Daten **nur** auf dem Computer zu entschlüsseln, auf dem sie verschlüsselt wurden, und darüber hinaus **nur** durch den Benutzer der die Verschlüsselung ausgestellt hat. Dadurch wird sichergestellt, dass selbst wenn du deine komplette `Bot.json` Datei an jemand anderen schickst, er das Passwort nicht ohne Zugriff auf deinen PC entschlüsseln kann. Dies ist eine ausgezeichnete Sicherheitsmaßnahme, hat aber gleichzeitig den großen Nachteil, am wenigsten kompatibel zu sein, da das mit dieser Methode verschlüsselte Passwort mit jedem anderen Benutzer und Rechner - einschließlich **deinem eigenen** - inkompatibel sein wird, wenn du dich z.B. für eine Neuinstallation des Betriebssystems entscheidest. Dennoch ist es eine der besten Methoden Passwörter zu speichern und wenn du dir Sorgen um die Sicherheit von `PlainText` machst und nicht jedes Mal ein Passwort mit der Option `Keins` setzen willst, dann ist dies deine beste Wahl, solange du nicht von einem anderen Computer als deinem eigenen auf deine Konfiguration zugreifen musst.

**Bitte beachte, dass diese Option nur auf Windows Betriebssystemen zur Verfügung steht.**

* * *

### Keins

Der einzige Weg, der 100% Sicherheit garantiert und sicherstellt, dass niemand dein Steam-Passwort stehlen kann. Um diese Option zu nutzen, setzt du einfach dein `SteamPassword` auf einen leeren String (`""`) oder `null` Wert. ASF fragt dich nach deinem Steam-Passwort, wenn es benötigt wird, und speichert es nirgendwo, außer im Speicher des aktuell laufenden Prozesses, bis du ihn schließt. Es ist zwar die sicherste Methode im Umgang mit Passwörtern, aber auch die lästigste, da du dein Passwort bei jedem ASF-Lauf manuell eingeben musst (wenn es erforderlich ist). Wenn das kein Problem für dich ist ist dies die beste Wahl in Bezug auf Sicherheit.

* * *

## Empfehlung

Wenn die Kompatibilität für dich kein Problem ist und du mit der Funktionsweise der `ProtectedDataForCurrentUser` Methode einverstanden bist, ist diese Option **empfohlen** um das Passwort in ASF zu speichern, da sie die beste Sicherheit bietet. Die Methode `AES` ist eine gute Wahl für Leute, die ihre Konfigurationen immer noch auf jedem beliebigen Computer verwenden wollen, während `PlainText` die einfachste Art ist das Passwort zu speichern, wenn es dir nichts ausmacht, dass jeder danach in der JSON-Konfigurationsdatei suchen kann.

Bitte bedenke, dass diese 3 Methoden als **unsicher** betrachtet werden, wenn ein Angreifer Zugriff auf deinen PC hat. ASF muss in der Lage sein das verschlüsselte Passwort zu entschlüsseln und wenn das auf deiner Maschine laufende Programm dazu in der Lage ist, dann kann auch jedes andere auf derselben Maschine laufende Programm dies tun. `ProtectedDataForCurrentUser` ist die sicherste Variante, da **auch andere Benutzer, die den gleichen PC verwenden, ihn nicht entschlüsseln können**, aber es ist trotzdem möglich die Daten zu entschlüsseln, wenn jemand in der Lage ist deine Login-Informationen und Maschineninformationen zusätzlich zur ASF-Konfigurationsdatei zu stehlen.

Für Personen die ASF selten starten oder die sich nicht die Mühe machen das Passwort bei jedem ASF-Start einzugeben, ist `Keins` die sicherste, da das Steam-Passwort nirgendwo gespeichert ist.

* * *

# Entschlüsselung

ASF unterstützt keine Möglichkeit, bereits verschlüsselte Passwörter zu entschlüsseln, da Entschlüsselungsmethoden nur intern für den Zugriff auf die Daten innerhalb des Prozesses verwendet werden. Wenn du die Verschlüsselungsprozedur rückgängig machen möchtest, z.B. um ASF auf einen anderen Rechner zu verschieben, wenn du `ProtectedDataForCurrentUser` verwendest, dann schalte einfach dein `PasswordFormat` zurück auf `0` (PlainText) und fülle `SteamPassword` entsprechend aus. Du kannst dann ASF wie gewohnt starten und den Vorgang von Anfang an wiederholen.
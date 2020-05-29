# Zwei-Faktor-Authentifizierung

Vor einiger Zeit hat Valve ein System namens "Treuhand" eingeführt, das einen zusätzlichen Authentifikator für verschiedene kontobezogene Aktivitäten erfordert. Du kannst **[hier](https://support.steampowered.com/kb_article.php?ref=1284-WTKB-4729)** und **[hier](https://support.steampowered.com/kb_article.php?ref=8078-TPHC-6195)** mehr darüber lesen. Es ist wichtig, das 2FA-System zuerst zu verstehen, bevor man versucht, die Logik hinter ASF 2FA zu verstehen.

Wie du sehen kannst, werden alle Handelsangebote für bis zu 15 Tage zurückgehalten, was kein großes Problem für unsere ASF ist, aber dennoch ärgerlich sein kann, besonders für diejenigen, die eine vollständige Automatisierung wünschen. Glücklicherweise enthält ASF eine Lösung für dieses Problem, genannt ASF 2FA.

* * *

# ASF-Logik

Unabhängig davon, ob du das unten erklärte ASF 2FA verwenden wirst oder nicht, enthält ASF die passende Logik und ist sich bewusst, dass die Konten durch das Standard 2FA geschützt sind. Es fragt dich nach den erforderlichen Details, wenn sie benötigt werden (z.B. beim Anmelden). Wenn du ASF 2FA verwendest, kann das Programm diese Anfragen überspringen und automatisch benötigte Codes generieren, was dir Ärger erspart und zusätzliche Funktionen ermöglicht (siehe unten).

* * *

# ASF 2FA

ASF 2FA ist ein eingebautes Modul, das für die Bereitstellung von 2FA-Funktionen für den ASF-Prozess verantwortlich ist, wie z.B. das Erzeugen von Codes und das Annehmen von Bestätigungen. Es dupliziert deinen vorhandenen Authentifikator, so dass es nicht notwendig ist, ASF 2FA ausschließlich zu verwenden.

Du kannst überprüfen, ob dein Bot-Konto bereits ASF 2FA verwendet, indem du den `2fa` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** ausführst. Wenn du deinen Authentifikator nicht bereits als ASF 2FA importiert hast, sind alle `2fa`-Befehle nicht funktionsfähig, was bedeutet, dass dein Konto nicht ASF 2FA verwendet, weshalb es auch nicht für erweiterte ASF-Funktionen geeignet ist, die den Betrieb des Moduls erfordern.

Um ASF 2FA zu aktivieren benötigst du:

- Funktionierenden Steam-Authentifikator in deinem Android-Gerät
- oder funktionierenden Steam-Authentifikator in deinem iOS-Gerät
- oder funktionierenden Steam-Authentifikator in **[SteamDesktopAuthenticator](https://github.com/Jessecar96/SteamDesktopAuthenticator)**
- oder funktionierenden Steam-Authentifikator in **[WinAuth](https://winauth.github.io/winauth)**
- oder jede andere funktionierende Implementierung eines Steam-Authentifikators mit Zugriff auf ein gemeinsames/identitäres Geheimnis und die Geräte-ID

* * *

## Import

Um die unten beschriebenen Schritte abzuschließen, solltest du bereits einen verknüpften und funktionsfähigen Authentifikator haben, der von ASF unterstützt wird. ASF unterstützt derzeit einige verschiedene Quellen von 2FA - Android, iOS, SteamDesktopAuthenticator und WinAuth. Wenn du noch keinen Authentifikator hast musst du einen davon auswählen und ihn zuerst einrichten. Wenn du nicht genau weißt welchen du auswählen sollst, empfehlen wir WinAuth, aber alle der oben genannten Möglichkeiten funktionieren gut, wenn du die Anweisungen befolgst.

Alle folgenden Anleitungen erfordern von dir, dass du bereits einen **funktionierenden und betriebsbereiten** Authentifikator hast, der mit dem gegebenen Programm/Anwendung verwendet wird. ASF 2FA funktioniert nicht ordnungsgemäß, wenn du ungültige Daten importierst. Stelle daher sicher, dass dein Authentifikator ordnungsgemäß funktioniert bevor du versuchst ihn zu importieren. Dies beinhaltet das Testen und Überprüfen, ob die folgenden Authentifikator-Funktionen ordnungsgemäß funktionieren:

- Du kannst Codes generieren und diese Codes werden vom Steam-Netzwerk akzeptiert
- Du kannst Bestätigungen abrufen und sie kommen auf deinem mobilen Authentifikator an
- Du kannst diese Bestätigungen akzeptieren und sie werden vom Steam-Netzwerk ordnungsgemäß als bestätigt/abgelehnt erkannt

Vergewissere dich, dass dein Authentifikator funktioniert indem du überprüfst, ob die obigen Aktionen funktionieren - wenn nicht, dann werden sie auch nicht in ASF funktionieren und du verschwendest nur Zeit und verursachst dir selbst Probleme.

* * *

### Android-Handy

Im Allgemeinen benötigst du für den Import des Authentifikator von deinem Android-Handy **[root](https://en.wikipedia.org/wiki/Rooting_(Android_OS))**-Zugriff. Das Rooting variiert von Gerät zu Gerät, daher werde ich dir nicht sagen wie du dein Gerät rooten kannst. Auf der Webseite **[XDA](https://www.xda-developers.com/root)** findest du exzellente Anleitungen wie du das machen kannst, sowie allgemeine Informationen zum rooten im Allgemeinen. Wenn du dein Gerät oder die Anleitung dazu nicht finden kannst, versuche es bei Google zu finden.

Zumindest ist es offiziell nicht möglich, auf geschützte Steam-Dateien ohne Root zuzugreifen. Die einzige offizielle nicht-root Methode zum Extrahieren von Steam-Dateien ist das Erstellen von unverschlüsselten `/data` Backups auf die eine oder andere Weise und das manuelle Abrufen entsprechender Dateien auf dem PC, aber da so etwas stark von deinem Handyhersteller abhängt und **nicht** im Android-Standard ist, werden wir es hier nicht weiter erörtern. Wenn du das Glück hast eine solche Funktionalität zu haben, kannst du sie nutzen, aber die Mehrheit der Benutzer hat so etwas nicht.

Inoffiziell ist es möglich die benötigten Dateien ohne Root-Zugriff zu extrahieren, indem man die Steam-App mit der Version 2.1 (oder früher) installiert oder heruntergestuft, einen mobilen Authentifikator aufsetzt und dann einen Snapshot der App (zusammen mit den von uns benötigten `data` Dateien) über `adb backup` erstellt. Da es sich jedoch um eine schwerwiegende Sicherheitsverletzung und eine völlig nicht unterstützte Methode zum Extrahieren der Dateien handelt, werden wir nicht weiter darauf eingehen. Valve hat diese Sicherheitslücke in neueren Versionen aus einem bestimmten Grund deaktiviert und wir erwähnen sie nur als Möglichkeit.

Unter der Annahme, dass du dein Handy erfolgreich gerootet hast, solltest du danach einen beliebigen auf dem Markt erhältlichen Root-Explorer herunterladen, wie z.B. **[dieser ](https://play.google.com/store/apps/details?id=com.jrummy.root.browserfree)** (oder einen anderen deiner Wahl). Du kannst auch über ADB (Android Debug Bridge) oder eine andere für dich verfügbare Methode auf die geschützten Dateien zugreifen, wir machen es über den Explorer, da es definitiv der benutzerfreundlichste Weg ist.

Nachdem du deinen Root Explorer geöffnet hast, navigiere zum Ordner `/data/data`. Beachte, dass das Verzeichnis `/data/data` geschützt ist und du ohne root-Zugriff nicht darauf zugreifen kannst. Dort angekommen, findest du den Ordner `com.valvesoftware.android.steam.community` und kopierst ihn auf deine `/sdcard`, die auf deinen integrierten internen Speicher zeigt. Danach solltest du dein Handy an deinen PC anschließen und den Ordner wie gewohnt von deinem internen Speicher kopieren können. Wenn der Ordner zufällig nicht sichtbar ist, obwohl du dir sicher bist, dass du ihn an die richtige Stelle kopiert hast, versuche zuerst dein Handy neu zu starten.

Nun kannst du wählen, ob du deinen Authentifikator zuerst in WinAuth und dann in ASF oder sofort in ASF importieren möchtest. Die erste Option ist benutzerfreundlicher und ermöglicht es dir deinen Authentifikator auch auf deinem PC zu duplizieren, so dass du Bestätigungen vornehmen und Codes an 3 verschiedenen Stellen generieren kannst - deinem Handy, deinem PC und ASF. Wenn du das tun möchtest, öffne einfach WinAuth, füge einen neuen Steam-Authentifikator hinzu und wähle die Option "Import aus Android", und folge dann den Anweisungen, indem du auf die Dateien zugreifst, die du oben erhalten hast. Danach kannst du diesen Authentifikator von WinAuth nach ASF importieren, was im speziellen WinAuth-Abschnitt weiter unten erläutert wird.

Wenn du dich nicht mit WinAuth befassen willst, dann kopiere einfach die Datei `files/Steamguard-SteamID` aus unserem geschützten Verzeichnis, wobei `SteamID` deine 64-Bit Steam-Identifikation des Kontos ist, das du hinzufügen möchtest (wenn mehr als eine, denn wenn du nur ein Konto hast, dann ist dies die einzige Datei). Du musst diese Datei in das ASF-Verzeichnis `config` kopieren. Sobald du das getan hast, benenne die Datei um in `BotName.maFile`, wobei `BotName` der Name deines Bot ist, dem du ASF 2FA hinzufügst. Nach diesem Schritt starte ASF - es sollte die `.maFile` erkennen und importieren.

    [*] INFO: ImportAuthenticator() <1> Konvertiere .maFile in ASF-Format...
    <1> Bitte trage die Gerätekennung deines mobilen Authentifikators ein (einschließlich "android:"):
    

Du musst nur noch einen weiteren Schritt ausführen - finde deine `DeviceID` Eigenschaft in `shared_prefs/steam.uuid.xml`. Es wird in XML-Tags enthalten sein und mit `android:` beginnen. Kopiere das (oder schreibe es auf) und gib es in ASF ein, wenn gefragt. Wenn du alles richtig gemacht hast sollte der Import abgeschlossen sein.

    [*] INFO: ImportAuthenticator() <1> Import vom mobilen Authentifikator erfolgreich abgeschlossen!
    

Bitte verifiziere, dass die Annahme von Bestätigungen tatsächlich funktioniert. Wenn du einen Fehler bei der Eingabe deiner `DeviceID` gemacht hast, dann hast du einen halb kaputten Authentifikator - Codes funktionieren aber Bestätigungen akzeptieren nicht. Du kannst `Bot.db` jederzeit entfernen und bei Bedarf neu beginnen.

* * *

### iOS

Für iOS kannst du **[ios-steamguard-extractor](https://github.com/CaitSith2/ios-steamguard-extractor)** verwenden. Dies ist möglich, da du entschlüsselte Backups erstellen, auf deinem PC installieren und das Programm verwenden kannst, um Steam-Daten zu extrahieren, die sonst unmöglich zu bekommen sind (zumindest ohne Jailbreak, aufgrund der iOS-Verschlüsselung).

Gehe zu der **[neuesten Version](https://github.com/CaitSith2/ios-steamguard-extractor/releases/latest)** um das Programm herunterzuladen. Sobald du die Daten extrahiert hast kannst du sie z.B. in WinAuth, dann von WinAuth nach ASF kopieren (obwohl du auch einfach generiertes json kopieren kannst, beginnend mit `{` endend mit `}` in `BotName.maFile` und wie üblich vorgehen). Wenn du mich fragst, empfehle ich dir dringend zuerst nach WinAuth zu importieren und dann sicherzustellen, dass sowohl das Erzeugen von Codes als auch das Akzeptieren von Bestätigungen richtig funktioniert, damit du sicher sein kannst, dass alles in Ordnung ist. Wenn deine Anmeldeinformationen ungültig sind wird ASF 2FA nicht ordnungsgemäß funktionieren, daher ist es viel besser den ASF-Importschritt als letzten Schritt durchzuführen.

Für Fragen/Probleme besuche bitte **[issues](https://github.com/CaitSith2/ios-steamguard-extractor/issues)**.

*Beachte, dass das obige Programm inoffiziell ist - du benutzt es auf eigene Gefahr. Wir bieten keine technische Unterstützung wenn es nicht ordnungsgemäß funktioniert - wir haben ein paar Hinweise erhalten, dass es sich um den Export ungültiger 2FA-Anmeldeinformationen handelt - überprüfe, ob Bestätigungen in einem Authentifikator wie WinAuth funktionieren, bevor du diese Daten in ASF importierst!*

* * *

### SteamDesktopAuthenticator

Wenn du deinen Authentifikator bereits in SDA laufen hast, solltest du beachten, dass es eine `steamID.maFile` Datei im Ordner `maFiles` gibt. Kopiere diese Datei in das Verzeichnis `config` von ASF. Vergewissere dich, dass `.maFile` in unverschlüsselter Form vorliegt, da ASF diese SDA-Dateien nicht entschlüsseln kann - unverschlüsselte Dateiinhalte sollten mit `{` Zeichen beginnen.

Du solltest nun `steamID.maFile` in `BotName.maFile` im ASF-Konfigurationsverzeichnis umbenennen, wobei `BotName` der Name deines Bot ist, dem du ASF 2FA hinzufügst. Alternativ kannst du es so lassen wie es ist, ASF wird es dann nach dem Einloggen automatisch auswählen. ASF zu helfen macht es möglich, ASF 2FA vor dem Einloggen zu verwenden, wenn du ASF nicht helfen willst, dann kann die Datei erst ausgewählt werden, wenn ASF sich erfolgreich einloggt hat (da ASF `steamID` deines Kontos nicht kennt, bevor du dich tatsächlich einloggst).

Wenn du alles richtig gemacht hast, starte ASF und du solltest folgendes sehen:

    [*] INFO: ImportAuthenticator() <1> Konvertiere .maFile in ASF-Format...
    [*] INFO: ImportAuthenticator() <1> Import vom mobilen Authentifikator erfolgreich abgeschlossen!
    

Von nun an sollte dein ASF 2FA für dieses Konto einsatzbereit sein.

* * *

### WinAuth

Erstelle zunächst eine neue leere `BotName.maFile` Datei im ASF-Konfigurationsverzeichnis, wobei `BotName` der Name deines Bot ist, dem du ASF 2FA hinzufügst. Bedenke, dass es `BotName.maFile` und NICHT `BotName.maFile.txt` sein sollte, Windows mag es bekannte Erweiterungen standardmäßig zu verstecken. Wenn du einen falschen Namen angibst wird er nicht von ASF ausgewählt.

Nun starte WinAuth wie gewohnt. Nach einem Rechtsklick auf das Steam-Symbol wähle "Show SteamGuard and Recovery Code". Dann setze einen Haken bei "Allow copy". Du solltest bemerken, dass dir die JSON-Struktur am unteren Rand des Fensters vertraut ist, beginnend mit `{`. Kopiere den gesamten Text in die `BotName.maFile` Datei, die du im vorherigen Schritt erstellt hast.

Wenn du alles richtig gemacht hast, starte ASF und du solltest folgendes sehen:

    [*] INFO: ImportAuthenticator() <1> Konvertiere .maFile in ASF-Format...
    <1> Bitte trage die Gerätekennung deines mobilen Authentifikators ein (einschließlich "android:"):
    

Hier kommt der knifflige Teil ins Spiel. WinAuth fehlt die deviceID-Eigenschaft, die von ASF benötigt wird, so dass du noch eine weitere Sache tun musst.

Gehe zurück zu WinAuths "Show SteamGuard and Recovery Code" und du solltest die Eigenschaft "Device ID" über dem JSON-Code bemerken, den du vor nicht allzu langer Zeit kopiert hast. Kopiere die gesamte Android-Geräte-ID, einschließlich dem `android:` Teil in ASF.

Wenn du das auch richtig gemacht hast, bist du jetzt fertig!

    [*] INFO: ImportAuthenticator() <1> Import vom mobilen Authentifikator erfolgreich abgeschlossen!
    

Bitte verifiziere, dass die Annahme von Bestätigungen tatsächlich funktioniert. Wenn du einen Fehler bei der Eingabe deiner `DeviceID` gemacht hast, dann hast du einen halb kaputten Authentifikator - Codes funktionieren aber Bestätigungen akzeptieren nicht. Du kannst `Bot.db` jederzeit entfernen und bei Bedarf neu beginnen.

* * *

## Fertig

Von diesem Moment an funktionieren alle `2fa` Befehle so, wie sie auf deinem herkömmlichen 2FA-Gerät ausgeführt werden. Du kannst sowohl ASF 2FA als auch den Authentifikator deiner Wahl (Android, iOS, SDA oder WinAuth) verwenden, um Codes zu generieren und Bestätigungen zu akzeptieren.

Wenn du einen Authentifikator auf deinem Handy hast, kannst du optional SteamDesktopAuthenticator und/oder WinAuth entfernen, da wir ihn nicht mehr benötigen. Allerdings schlage ich vor es für alle Fälle aufzubewahren, ganz zu schweigen davon, dass es praktischer ist als ein normaler Steam-Authentifikator. Bedenke lediglich, dass ASF 2FA **KEIN** Universal-Authentifikator ist und es sollte **nie** der einzige sein, den du verwendest, da es nicht einmal alle Daten enthält die der Authentifikator haben sollte. Es ist nicht möglich ASF 2FA wieder in den Original-Authentifikator umzuwandeln, deshalb ist es wichtig, dass du den Universal-Authentifikator an anderer Stelle, z.B. in WinAuth/SDA, oder auf deinem Handy hast.

* * *

## FAQ (oft gestellte Fragen)

### Wie nutzt ASF das 2FA-Modul?

Wenn ASF 2FA verfügbar ist wird ASF es für die automatische Bestätigung von Handelsangeboten verwenden, die von ASF gesendet bzw. akzeptiert werden. Es wird auch in der Lage sein bei Bedarf automatisch 2FA-Codes zu generieren, z.B. um sich anzumelden. Darüber hinaus ermöglicht ASF 2FA auch die Verwendung von `2fa`-Befehlen. Das sollte vorerst alles sein, falls ich nichts vergessen habe - im Grunde genommen verwendet ASF das 2FA-Modul bei Bedarf.

* * *

### Was ist wenn ich einen 2FA-Code benötige?

Du benötigst einen 2FA-Code, um auf das 2FA-geschützte Konto zuzugreifen, das auch jedes Konto mit ASF 2FA einbezieht. Du solltest Codes im Authentifikator generieren, den du für den Import benutzt hast, aber du kannst auch temporäre Codes über den Befehl `2fa` erzeugen, der über den Chat an den angegebenen Bot gesendet wird. Du kannst auch den Befehl `2fa <BotNames>` verwenden, um temporäre Codes für bestimmte Bot-Instanzen zu erzeugen. Das sollte ausreichen, damit du z.B. über den Browser auf Bot-Konten zugreifen kannst, aber wie oben erwähnt, solltest du stattdessen deinen benutzerfreundlichen Authentifikator (Android, iOS, SDA oder WinAuth) verwenden.

* * *

### Kann ich nach dem Import zu ASF 2FA meinen Original-Authentifikator verwenden?

Ja, dein Original-Authentifikator bleibt funktionsfähig und du kannst ihn zusammen mit ASF 2FA verwenden. Das ist der Sinn des Verfahrens - wir importieren deine Authentifikationsinformationen in ASF damit ASF sie nutzen und ausgewählte Bestätigungen in deinem Namen akzeptieren kann.

* * *

### Wo wird der ASF Mobile Authenticator gespeichert?

ASF Mobile Authenticator wird in der Datei `BotName.db` in deinem Konfigurationsverzeichnis gespeichert, zusammen mit einigen anderen wichtigen Daten, die sich auf das angegebene Konto beziehen. Wenn du ASF 2FA entfernen möchtest, lies das untenstehende.

* * *

### Wie entferne ich ASF 2FA?

Stoppe einfach ASF und entferne die zugehörige `BotName.db` Datei des Bots mit der verlinkten ASF 2FA, die du entfernen möchtest. Diese Option entfernt die zugehörige importierte 2FA mit ASF, entfernt aber NICHT deinen Authentifikator. Wenn du stattdessen deinen Authentifikator entfernen möchtest, solltest du ihn nicht nur von ASF entfernen (zuerst), sondern auch in einem Authentifikator deiner Wahl (Android, iOS, SDA oder WinAuth) entfernen, oder - wenn du aus irgendeinem Grund nicht kannst, einen Widerrufscode verwenden, den du während der Verknüpfung dieses Authentifikators auf der Steam-Webseite erhalten hast. Es ist nicht möglich, deinen Authentifikator über ASF zu entfernen, dafür sollte der bereits vorhandene Standard-Authentifikator verwendet werden.

* * *

### Ich habe den Authentifikator in SDA/WinAuth verlinkt und dann in ASF importiert. Kann ich ihn jetzt entfernen und auf meinem Handy wieder verlinken?

**Nein**. ASF **importiert** deine Authentifikatordaten um sie zu verwenden. Wenn du deinen Authentifikator entfernst, dann wirst du damit auch bewirken, dass ASF 2FA nicht mehr funktioniert, egal ob du ihn zuerst entfernst, wie in der obigen Frage angegeben ist oder nicht. Wenn du deinen Authentifikator sowohl auf deinem Handy als auch auf ASF (plus optional in SDA/WinAuth) verwenden möchtest, dann musst du deinen Authentifikator von deinem Handy **importieren** und keinen neuen in SDA/WinAuth erstellen. Du kannst nur **einen** verknüpften Authentifikator haben, deshalb **importiert** ASF den Authentifikator und seine Daten, um ihn als ASF 2FA zu verwenden - es ist **derselbe** Authentifikator der nur an zwei Stellen existiert. Wenn du dich dazu entscheidest deine mobilen Authentifizierungsdaten zu entfernen - unabhängig davon, in welcher Weise, wird ASF 2FA die Funktionalität einstellen, da die zuvor kopierten mobilen Authentifizierungsdaten nicht mehr gültig sind. Um ASF 2FA zusammen mit dem Authentifikator auf deinem Handy verwenden zu können musst du es aus Android/iOS importieren, was oben beschrieben ist.

* * *

### Ist die Verwendung von ASF 2FA besser als die Verwendung von WinAuth/SDA/Anderer Authentifikator um alle Bestätigungen zu akzeptieren?

**Ja**, auf verschiedene Arten. Die erste und wichtigste - die Verwendung von ASF 2FA erhöht **erheblich** deine Sicherheit, da das ASF 2FA-Modul sicherstellt, dass ASF nur automatisch seine eigenen Bestätigungen akzeptiert, so dass selbst wenn der Angreifer ein schädliches Handelsangebot sendet, ASF 2FA dieses Handelsangebot **nicht** akzeptiert, da er nicht von ASF generiert wurde. Zusätzlich zum Sicherheitsteil bringt die Verwendung von ASF 2FA auch Performance-/Optimierungsvorteile, da ASF 2FA Bestätigungen unmittelbar nach der Generierung abruft und akzeptiert und nur dann, im Gegensatz zur ineffizienten Abfrage der Bestätigungen alle X Minuten, die z.B. von SDA oder WinAuth durchgeführt wird. Kurz gesagt, es gibt keinen Grund den Authentifikator eines Drittanbieters dem von ASF 2FA vorzuziehen, wenn du vorhast von ASF generierte Bestätigungen zu automatisieren - genau dafür ist ASF 2FA gedacht und die Verwendung steht nicht im Konflikt mit der Bestätigung von allem anderen in einem Authentifikator deiner Wahl. Wir empfehlen nachdrücklich ASF 2FA für die gesamte ASF-Aktivität zu verwenden - dies ist viel sicherer als jede andere Lösung.

* * *

## Erweiterte Einstellungen

Wenn du ein fortgeschrittener Benutzer bist, kannst du die maFile-Datei auch manuell generieren. Sie sollte eine **[gültige JSON-Struktur](https://jsonlint.com)** aufweisen:

```json
{
  "shared_secret": "STRING",
  "identity_secret": "STRING",
  "device_id": "STRING"
}
```

`device_id` ist optional beim Import, aber obligatorisch für den ASF-Betrieb - Falls du es weglässt fragt ASF beim Import danach. Natürlich musst du `"STRING"` durch einen gültigen Inhalt in jedem Feld ersetzen.

Standard-Authentifikatordaten haben mehr Felder - sie werden von ASF beim Import völlig ignoriert, da sie nicht benötigt werden. Du musst sie auch nicht entfernen - ASF benötigt nur gültiges JSON mit den 2 oben beschriebenen Pflichtfeldern und optional auch `device_id`.
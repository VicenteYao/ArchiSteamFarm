# Installation

Wenn du hier zum ersten Mal angekommen bist, herzlich willkommen! Wir freuen uns sehr, einen weiteren Reisenden zu sehen, der sich für unser Projekt interessiert, obwohl wir berücksichtigen müssen, dass mit großer Macht eine große Verantwortung einhergeht - ASF ist in der Lage, viele verschiedene Dinge rund um Steam zu tun, aber nur, solange man sich **genug kümmert, um zu lernen, wie man ihn benutzt**. Es gibt hier eine steile Lernkurve, und wir erwarten von dir, dass du das Wiki in dieser Hinsicht liest, das im Detail erklärt, wie alles funktioniert.

Wenn du immer noch hier bist, heißt das, dass du den Text oben überstanden hast, was toll ist. Außer du hast ihn übersprungen, was heißt, dass du bald **[schlechte Zeiten](https://www.youtube.com/watch?v=WJgt6m6njVw)** vor dir hast... Wie auch immer: ASF ist eine Konsolenanwendung, was bedeutet, dass das Programm selbst keine freundliche Benutzeroberfläche hat, wie du es eventuell gewohnt bist. ASF sollte hauptsächlich auf Servern laufen, so dass es als Dienst (Daemon) und nicht als Desktop-App fungiert.

Das bedeutet jedoch nicht, dass du es nicht auf deinem PC benutzen kannst oder dass die Benutzung etwas komplizierter ist als sonst, nichts dergleichen. ASF ist ein eigenständiges Programm, das keine Installation benötigt und sofort einsatzbereit ist. Allerdings wird eine Konfiguration erfordert, bevor es nützlich wird. Die Konfiguration sagt ASF, was es nach dem Start tatsächlich tun soll. Wenn du ASF ohne Konfiguration startest, dann wird es nichts tun. Ganz einfach.

* * *

## Betriebssystemspezifisches Setup

Folgendes werden wir in den nächsten paar Minuten machen:

- Installiere **[.NET Core prerequisites](#net-core-prerequisites)**.
- Die **[neueste ASF Version](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** in der entsprechenden betriebssystemspezifischen Variante herunterladen.
- Das Archiv in einen neuen Ordner entpacken (und mit `chmod +x ArchiSteamFarm` ausführbar machen, wenn du dich auf Linux oder OS X befindest).
- **[ASF konfigurieren](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)**.
- ASF starten und der Magie ihren Lauf lassen.

Hört sich einfach an, richtig? Dann lass uns anfangen.

* * *

### .NET Core Prerequisites

Der erste Schritt ist sicherzustellen, dass dein Betriebssystem ASF überhaupt richtig ausführen kann. ASF ist in C# programmiert, basierend auf .NET Core und benötigt möglicherweise native Bibliotheken, die auf deiner Plattform noch nicht verfügbar sind. Abhängig davon, ob du Windows, Linux oder OS X verwendest, wirst du unterschiedliche Voraussetzungen haben, welche allerdings alle im **[.NET Core Abhängigkeiten](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)**-Dokument, dem du folgen solltest, aufgelistet sind. Dieses ist Referenzmaterial, das verwendet werden sollte, allerdings haben wir im Sinne der Einfachheit auch alle benötigten Pakete unten aufgelistet, damit du nicht das gesamte Dokument lesen musst.

Es ist völlig normal, dass manche (oder sogar alle) Abhängigkeiten bereits in deinem System existieren, weil sie mit der Software Dritter, welche du verwendest, mitinstalliert wurden. Trotzdem solltest du sicherstellen, dass dies wirklich der Fall ist indem du das entsprechende Installationsprogramm für dein Betriebssytem ausführst - Ohne diese Abhängigkeiten wird ASF nicht einmal starten.

Behalte im Hinterkopf, dass du für betriebssystemspezifische ASF-Versionen nichts weiteres tun must. Insbesondere betrifft dies die Installation des .NET Core SDKs oder der Runtimes, da die betriebssytemspezifische Versionen das alles bereits beinhalten. Du benötigst nur die .NET Core Prerequisites (Abhängigkeiten) um die .NET Core Runtime, die bereits in ASF enthalten ist, auszuführen.

#### **[Windows](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-windows)**:

- **[Microsoft Visual C++ 2015 Redistributable Update](https://www.microsoft.com/en-us/download/details.aspx?id=53587)** (x64 for 64-bit Windows, x86 for 32-bit Windows)
- Wir dringend empfohlen sicherzustellen, dass alle Windows Aktualisierungen installiert sind. Du benötigst mindestens **[KB2533623](https://support.microsoft.com/en-us/help/2533623/microsoft-security-advisory-insecure-library-loading-could-allow-remot)** und **[KB2999226](https://support.microsoft.com/en-us/help/2999226/update-for-universal-c-runtime-in-windows)**, aber es könnten weiteren Aktualisierungen benötigt werden. Wenn dein Windows aktuell ist, sind diese bereits alle installiert. Versichere dich, dass du diese Voraussetzungen erfüllst, bevor du das Visual C++ Paket installierst.

#### **[Linux](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-linux)**:

Paketnamen hängen von der Linux-Distribution, die du verwendest, ab. Wir listen nur die Gebräuchlichsten auf. Du kannst alle über den nativen Paketmanager für dein Betriebssystem (wie zum Beispiel `apt` unter Debian oder `yum` unter CentOS) installieren.

- `libcurl` (`libcurl4`, `libcurl3`)
- `libicu` (neueste Version für deine Distribution, zum Beispiel `libicu60`)
- `libkrb5-3` (`krb5-libs`)
- `liblttng-ust0` (`lttng-ust`)
- `libssl` (`libssl1.1`, `openssl-libs`, neueste 1.1.X Version für deine Distribution)
- `zlib1g` (`zlib`)

Wenigstens einige davon sollten bereits nativ auf deinem System verfügbar sein (wie z. B. `zlib1g` das heutzutage in fast allen Linux-Distributionen benötigt wird).

#### **[OS X](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-macos)**:

- Keine vorerst, aber du solltest die neueste Version von OS X installiert haben, mindestens 10.13+.

* * *

### Herunterladen

Da wir nun alle benötigten Abhängigkeiten installiert haben, ist der nächste Schritt das Herunterladen der **[neuesten ASF-Version](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**. ASF ist in mehreren Varianten verfügbar, aber du bist nur am Paket interessiert, das deinem Betriebssystem und der Architektur deines PCs entspricht. Zum Beispiel, wenn du `64`-Bit `Win`dows verwendest, dann willst du die `ASF-win-x64`-Variante. Für mehr Information über die verfügbaren Varianten, besuche bitte den Abschnitt **[ Kompatibilität](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-de-DE)**. ASF kann auch unter Betriebssystemen ausgeführt werden, für die wir kein betriebssystemspezifisches Paket zur Verfügung stellen. Ein Beispiel hierfür ist **32-Bit Windows**. Dafür gehe bitte zum **[generischen Setup](#generisches-setup)**.

![Assets](https://i.imgur.com/Ym2xPE5.png)

Beginne nach dem Download damit, die Zip-Datei in einen eigenen Ordner zu entpacken. Wir empfehlen die Verwendung von **[7-zip](https://www.7-zip.org)** oder die Standard-Dienstprogramme wie `unzip` unter Linux/OS X sollten ebenfalls problemlos funktionieren. Danach wirst du ein riesiges Durcheinander an Ordnern und Dateien vorfinden. Keine Angst, wir werden das gleich aufräumen.

Wenn du Linux oder OS X verwendest, vergiss nicht mit `chmod +x ArchiSteamFarm`, die Berechtigungen zu setzen, da diese in einem zip-Archiv nicht gesetzt sind. Dies muss nur nach dem initialen Entpacken gemacht werden.

Stelle bitte sicher, dass du ASF in **einen eigenen Ordner** entpackst und nicht in einen bereits existenten, der für etwas anderes verwendet wird - ASFs automatische Aktualisierungen werden alle alten Dateien in diesem Ordner löschen, was möglicherweise dazu führen könnte, dass du Dateien verlierst, die nichts mit ASF zu tun haben aber im selben Ordner sind. Solltest du zusätzliche Skripte oder Dateien haben, die du mit ASF verwenden willst, solltest du sie in den Ordner darüber tun.

Eine Beispiel-Struktur würde wie folgt aussehen:

    C:\ASF (hier tust du deine eigenen Dateien hin)
        ├── ASF Verknüpfung.lnk (optional)
        ├── Config Verknüpfung.lnk (optional)
        ├── Befehle.txt (optional)
        ├── MeinZusätzlichesSkript.bat (optional)
        ├── ... (Alle anderen Dateien deiner Wahl, optional)
        └── Core (nur ASF, hier extrahierst du das Archiv)
             ├── ArchiSteamFarm.dll
             ├── config
             └── (...)
    

Dies ist auch eine Struktur, die wir empfehlen würden, so dass du nicht durch eine große Anzahl von Dateien und Ordnern gehen musst, die in ASF enthalten sind, da du für die Verwendung nur eine Verknüpfung zu Konfigurationsordner und Hauptbinärdatei benötigst.

Let's prepare ASF structure for usage. If you want to, you can now skip to the next step, since cleaning up ASF structure is not required (especially if you're using OS-specific builds that are already bundled), but it can make your life a bit easier.

You can open ASF folder and find core executable file, this will be `ArchiSteamFarm.exe` on Windows, and `ArchiSteamFarm` on Linux/OS X. Right click it and select "copy". Navigiere nun zu dem Ort an dem du die ASF-Verknüpfung haben möchtest (z.B. auf deinem Desktop), klicke mit der rechten Maustaste und wähle "Verknüpfung hier einfügen" aus. Wenn du möchtest kannst du deine Verknüpfung umbenennen, z.B. indem du ihr den Namen "ASF" gibst. Jetzt machst du dasselbe mit dem Verzeichnis `config`, das du an der gleichen Stelle wie die ASF-Binärdatei finden kannst.

Nach einer kleinen Säuberung hast du nun eine sehr komfortable Struktur, ähnlich der untenstehenden:

![Structure](https://i.imgur.com/k85csaZ.png)

Auf diese Weise kannst du problemlos und ohne großen Aufwand auf ASF-Binär- und Konfigurationsdateien zugreifen. In meinem Fall habe ich mich für die oben genannte Struktur entschieden, so dass sich meine ASF-Dateien im Verzeichnis "Core" befinden. Du kannst diese Struktur nach Belieben anpassen, wie z.B. ASF + config Verknüpfungen auf dem Desktop kopieren und das ASF-Verzeichnis z.B. in `C:\ASF` legen. Das liegt ganz bei dir.

Linux/OS X-Benutzern wird empfohlen, dasselbe zu tun, du kannst einen ausgezeichneten Mechanismus für symbolische Links verwenden, der über `ln -s` verfügbar ist.

* * *

### Konfiguration

Wir sind nun bereit, den allerletzten Schritt, die Konfiguration, durchzuführen. Dies ist bei weitem der komplizierteste Schritt, da es sich um eine Menge neuer Informationen handelt, die du noch nicht kennst, also werden wir versuchen, hier einige leicht verständliche Beispiele und vereinfachte Erklärungen zu geben.

In erster Linie gibt es die Seite **[Installation](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)**, die **alles** erklärt, was sich auf die Konfiguration bezieht, aber es ist eine riesige Menge an neuen Informationen, von denen wir im Moment nicht alle benötigen werden. Stattdessen zeigen wir dir, wie du die Informationen bekommst, die du tatsächlich suchst.

Die ASF-Konfiguration kann auf zwei Arten erfolgen - entweder mit Hilfe unseres Web-Konfigurationsgenerators oder manuell. Dies wird ausführlich auf der Seite **[Installation](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)** erläutert, also beziehe dich darauf, wenn du detailliertere Informationen benötigst. Wir werden den Web-Konfigurationsgenerator verwenden, da er viel einfacher ist.

Besuche unsere **[Web-Konfigurationsgenerator](https://justarchinet.github.io/ASF-WebConfigGenerator)** Seite mit deinem Lieblingsbrowser (du musst JavaScript aktiviert haben, falls du es manuell deaktiviert hast). Wir empfehlen Chrome oder Firefox, aber es sollte mit allen gängigen Browsern funktionieren.

Nach dem Öffnen der Seite klicke auf "Bot". Du solltest nun eine ähnliche Seite wie die unten sehen:

![Bot tab](https://i.imgur.com/aF3k8Rg.png)

Wenn die Version von ASF, die du gerade heruntergeladen hast, älter ist als der Konfigurationsgenerator, der standardmäßig verwendet wird, wähle einfach deine ASF-Version aus dem Dropdown-Menü. Dies kann passieren, da der Konfigurationsgenerator zum Erzeugen von Konfigurationen für neuere (Vorabversionen) ASF-Versionen verwendet werden kann, die noch nicht als stabil markiert wurden. Du hast die neueste stabile Version von ASF heruntergeladen, die auf ihre zuverlässige Funktion überprüft wurde.

Beginne damit, den Namen deines Bot in das rot markierte Feld zu schreiben. Dies kann jeder beliebige Name sein, den du verwenden möchtest, wie z.B. dein Spitzname, Kontoname, eine Nummer oder etwas anderes. Es gibt nur ein Wort, das du nicht verwenden kannst, nämlich `ASF`, da dieses Schlüsselwort für die globale Konfigurationsdatei reserviert ist. Außerdem kann dein Bot-Name nicht mit einem Punkt beginnen (ASF ignoriert diese Dateien absichtlich). Wir empfehlen dir auch keine Leerzeichen zu verwenden. Du solltest bei Bedarf `_` als Trennzeichen verwenden.

Nachdem du dich für einen Namen entschieden hast, aktiviere den `Enabled` Schalter. Hiermit wird festgelegt, ob dein Bot von ASF automatisch nach dem Start (des Programms) gestartet werden soll.

Jetzt kannst du dich für zwei Dinge entscheiden:

- Du kannst deinen Login in das Feld `SteamLogin` und dein Passwort in das `SteamPassword` Feld eintragen
- Oder du kannst sie leer lassen

Wenn du das erste tust, kann ASF deine Konto-Anmeldeinformationen während des Startvorgangs automatisch verwenden, so dass du sie nicht jedes Mal manuell eingeben musst, wenn ASF sie benötigt. Du kannst sie jedoch auch weglassen. In diesem Fall werden sie nicht gespeichert, so dass ASF nicht ohne deine Hilfe automatisch starten kann und du sie während der Laufzeit eingeben musst.

ASF benötigt deine Anmeldeinformationen, da es seine eigene Implementierung des Steam-Clients beinhaltet und die gleichen Details benötigt, die du selbst benutzt um dich anzumelden. Deine Anmeldeinformationen werden nirgends außer auf deinem PC im `config` ASF-Verzeichnis gespeichert. Unser Web-Konfigurationsgenerator ist client-basiert, was bedeutet, dass der Programmcode lokal in deinem Browser ausgeführt wird, um gültige ASF-Konfigurationen zu generieren, ohne dass du Details eingibst, die deinen PC überhaupt erst verlassen, so dass du dir keine Sorgen über einen möglichen Verlust vertraulicher Daten machen musst. Dennoch, wenn du aus irgendeinem Grund deine Zugangsdaten dort nicht eingeben möchtest, verstehen wir das, und du kannst sie später manuell in generierte Dateien einfügen, oder sie ganz weglassen und sie nur in die ASF-Befehlszeile eingeben. Mehr zu Sicherheitsfragen findest du im Abschnitt **[Konfiguration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)**.

Du kannst auch nur ein Feld leer lassen, wie z.B. `SteamPassword`. ASF kann dann dein Login automatisch verwenden, fragt aber trotzdem nach dem Passwort (ähnlich wie beim Steam Client). Wenn du die Steam-Familienansicht benutzt um das Konto freizuschalten musst du es in das Feld `SteamParentalCode` eingeben.

Nach der Entscheidung und den optionalen Details wird deine Webseite nun ähnlich wie die untenstehende aussehen:

![Bot tab 2](https://i.imgur.com/yf54Ouc.png)

Du kannst jetzt auf den "Download"-Button klicken und unser Web-Konfigurationsgenerator erzeugt eine neue `json` Datei basierend auf deinem gewählten Namen. Speichere diese Datei in das `config` Verzeichnis von ASF. Du kannst die zuvor erstellte `config` Verknüpfung verwenden oder das `config` Verzeichnis manuell direkt in der ASF-Dateistruktur finden.

Dein `config` Verzeichnis sieht nun wie folgt aus:

![Structure 2](https://i.imgur.com/crWdjcp.png)

Glückwunsch! Du hast gerade die sehr einfache ASF-Bot-Konfiguration abgeschlossen. Wir werden dies in Kürze erweitern, denn jetzt ist dies alles, was du brauchst.

* * *

### Ausführen von ASF

Du bist nun bereit, das Programm zum ersten Mal zu starten. Simply double-click ASF shortcut, or `ArchiSteamFarm` binary in ASF directory.

Nach diesem Schritt, vorausgesetzt, du hast alle erforderlichen Abhängigkeiten im ersten Schritt installiert, sollte ASF richtig starten, deinen ersten Bot bemerken (wenn du nicht vergessen hast, die generierte Konfiguration in das Verzeichnis `config` zu legen) und versuchen, dich anzumelden:

![ASF](https://i.imgur.com/u5hrSFz.png)

Wenn du `SteamLogin` und `SteamPassword` für ASF angegeben hast, wirst du nur nach deinem SteamGuard-Code gefragt (E-Mail, 2FA oder keine, abhängig von deinen Steam-Einstellungen). Wenn du es nicht getan hast, wirst du auch nach deinem Steam-Login und Passwort gefragt.

Jetzt ist ein guter Zeitpunkt, um unseren Abschnitt zur **[Datenschutzerklärung](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-de-DE#aktuelle-datenschutzerkl%C3%A4rung)** zu lesen, wenn du dir Sorgen darüber machst, was als nächstes passieren wird, wie von ASF selbst angegeben.

Nachdem du das anfängliche Anmeldeportal passiert hast, (davon ausgegangen, dass deine Daten korrekt sind) wirst du erfolgreich angemeldet und ASF beginnt mit den Standardeinstellungen, die du bisher nicht geändert hast, zu sammeln:

![ASF 2](https://i.imgur.com/Cb7DBl4.png)

Dies beweist, dass ASF nun erfolgreich seine Arbeit auf deinem Konto erledigt, so dass du nun das Programm minimieren und etwas anderes tun kannst. Nach einiger Zeit (je nach **[Performance](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE)**) wirst du sehen, wie Steam-Karten langsam gesammelt werden. Natürlich musst du dafür zulässige Spiele zum Sammeln haben, die auf deiner **[Abzeichen-Seite](https://steamcommunity.com/my/badges)** folgendes zeigen: "Du kannst noch X weitere Kartendrops vom Spielen dieses Spiels bekommen". Wenn es keine Spiele zum Sammeln gibt, dann wird ASF feststellen, dass es nichts zu tun gibt, wie in unserem **[FAQ](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ#what-is-asf)** beschrieben.

Dies schließt unseren sehr einfachen Einrichtungsleitfaden ab. Du kannst nun entscheiden, ob du ASF weiter konfigurieren möchtest oder ob du es in den Standardeinstellungen seine Arbeit tun lassen möchtest. Wir werden noch ein paar grundlegende Details besprechen und dir dann das gesamte Wiki zur Verfügung stellen.

* * *

### Erweiterte Konfiguration

#### Sammeln auf mehreren Konten gleichzeitig

ASF unterstützt das Karten-Sammeln von mehr als einem Konto auf einmal, was seine Hauptfunktion ist. Du kannst weitere Konten zu ASF hinzufügen, indem du mehr Bot-Konfigurationsdateien erzeugst, genau so wie du deine erste vor wenigen Minuten generiert hast. Du musst nur zwei Dinge sicherstellen:

- Eindeutiger Bot-Name, wenn du bereits deinen ersten Bot "HauptKonto" genannt hast, kannst du keinen weiteren so nennen.
- Gültige Anmeldedaten, wie z.B. `SteamLogin`, `SteamPassword` und `SteamParentalCode` (bei Verwendung der Steam Parental Einstellungen)

Mit anderen Worten, gehe einfach erneut zur Konfiguration und mache genau das Gleiche, nur für dein zweites oder drittes Konto. Vergiss nicht für alle deine Bots eindeutige Namen zu verwenden.

* * *

#### Einstellungen ändern

Du kannst bestehende Einstellungen auf die gleiche Weise ändern, indem du eine neue Konfigurationsdatei erzeugst. Wenn du unseren Web-Konfigurationsgenerator noch nicht geschlossen hast, klicke auf "Zu den erweiterten Einstellungen umschalten" und sieh dir an, was du entdecken kannst. Für diese Anleitung werden wir die Einstellung `CustomGamePlayedWhileFarming` ändern, mit der du einstellen kannst, dass der benutzerdefinierte Name angezeigt wird, wenn ASF am Sammeln ist, anstatt das aktuelle Spiel anzuzeigen.

Also lass uns das tun, wenn du ASF ausführst und das Sammeln beginnst, wirst du in den Standardeinstellungen sehen, dass dein Steam-Konto jetzt im Spiel ist:

![Steam](https://i.imgur.com/1VCDrGC.png)

Lass uns das jetzt ändern. Aktiviere die erweiterten Einstellungen im Webkonfigurationsgenerator und suche `CustomGamePlayedWhileFarming`. Sobald du das getan hast, füge dort deinen eigenen benutzerdefinierten Text ein, den du anzeigen möchtest, wie zum Beispiel "Idling cards":

![Bot tab 3](https://i.imgur.com/gHqdEqb.png)

Nun kannst du die neue Konfigurationsdatei auf genau die gleiche Weise herunterladen, dann **überschreibe** deine alte Konfigurationsdatei mit der neuen. Du kannst natürlich auch deine alte Konfigurationsdatei löschen und an ihre Stelle die neue einfügen.

Sobald du das getan hast und ASF erneut startest, wirst du feststellen, dass ASF nun deinen benutzerdefinierten Text an der vorherigen Stelle anzeigt:

![Steam 2](https://i.imgur.com/vZg0G8P.png)

Dies bestätigt, dass du deine Konfiguration erfolgreich bearbeitet hast. Auf genau die gleiche Weise kannst du globale ASF-Eigenschaften ändern, indem du vom Bot-Tab zum "ASF"-Tab wechselst, die generierte `ASF.json` Konfigurationsdatei herunterlädst und sie in dein `config` Verzeichnis legst.

* * *

#### Verwendung von ASF-ui

ASF ist eine Konsolenanwendung und beinhaltet keine grafische Benutzeroberfläche. Wir arbeiten jedoch aktiv an unserem IPC-Schnittstellen Frontend **[ASF-ui](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC#asf-ui)**, was eine sehr gute und benutzerfreundliche Möglichkeit sein kann, auf verschiedene ASF-Funktionen zuzugreifen.

Um ASF-ui verwenden zu können, solltest du sicherstellen, dass du die globale Konfigurationseigenschaften `IPC` und `SteamOwnerID` (Registerkarte ASF) einrichtest.

Für `SteamOwnerID` musst du einen eindeutigen Steam-Identifikator in 64-Bit-Form deines Kontos eingeben. Du kannst sie auf verschiedene Arten einsehen, wir werden dafür **[SteamRep](https://steamrep.com)** verwenden. Rufe die Website auf, finde die "Anmeldung über Steam"-Schaltfläche oben rechts in der Ecke und melde dich dann an. Anschließend klicke an gleicher Stelle auf deinen Avatar und schaue im Feld `steamID64` in deinem Profil nach.

![SteamRep](https://i.imgur.com/RUuJ63i.png)

Für mein Konto ist dies die Nummer `76561198006963719`. Du wirst eine ähnliche haben, auch beginnend mit `7656`. Kopiere sie.

Navigiere nun noch einmal zu unserem Web-Konfigurationsgenerator und trage diese Nummer als SteamOwnerID ein.

![SteamOwnerID](https://i.imgur.com/V6jslfQ.png)

Du musst nur noch eine weitere Sache erledigen und zwar die erweiterten Einstellungen umschalten und die Option `IPC` finden und aktivieren.

![IPC](https://i.imgur.com/NhujZCN.png)

Jetzt kannst du deine ASF-Konfiguration herunterladen und wie gewohnt in dein `config` Verzeichnis legen. Starte danach ASF erneut, und du solltest bestätigen können, dass es die IPC-Schnittstelle ordnungsgemäß gestartet hat:

![IPC 2](https://i.imgur.com/ZmkO8pk.png)

Wenn du alles richtig gemacht hast, kannst du nun unter **[diesem](http://localhost:1242)** Link auf die IPC-Schnittstelle von ASF zugreifen, solange ASF läuft. Du kannst ASF-ui für verschiedene Zwecke verwenden, z.B. um **[Befehle](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** zu senden. Wirf einfach einen Blick darauf, um alle Funktionalitäten von ASF-ui kennenzulernen.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

Bitte bedenke, dass sich ASF-ui derzeit im Preview-Zustand befindet und noch nicht alles verfügbar/funktionsfähig ist, aber es ist mehr als genug für eine simple ASF-Nutzung.

* * *

### Zusammenfassung

Du hast ASF bereits erfolgreich so eingestellt, dass es deine Steam-Konten nutzt, und du hast es bereits ein wenig nach deinen Wünschen angepasst. Wenn du unseren gesamten Leitfaden befolgt hast, dann hast du es sogar geschafft, einen einfachen Befehl über unsere ASF-ui-Schnittstelle zu senden. Jetzt ist ein guter Zeitpunkt, unseren gesamten Abschnitt **[Konfiguration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)** zu lesen, um zu erfahren, was all die verschiedenen Einstellungen, die du auf der Registerkarte Erweitert gesehen hast, tatsächlich bewirken und was ASF bieten kann. Wenn du über ein Problem gestolpert bist oder eine allgemeine Frage hast, lies stattdessen das **[FAQ](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-de-DE)**, was alle oder zumindest die meisten Fragen abdecken sollte, die du haben könntest. Wenn du alles über ASF erfahren möchtest und wie es dein Leben einfacher machen kann, dann schaue dir den Rest von **[unserem Wiki](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home-de-DE)** an. Viel Spaß!

* * *

## Generisches Setup

Dieses Setup ist für fortgeschrittene Benutzer, die ASF so einrichten möchten, dass es in der Variante **[generic](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#generic)** läuft. Es wird nicht für Personen empfohlen, die das **[betriebssystemspezifische Setup](#os-specific-setup)** verwenden können.

Du willst die generische Variante hauptsächlich in drei Situationen verwenden (aber natürlich kannst du sie trotzdem verwenden):

- Wenn du ein Betriebssystem verwendest, für das wir kein betriebssystemspezifisches Paket erstellen (z.B. 32-Bit Windows)
- Wenn du bereits die .NET Core Runtime/SDK hast, oder eine installieren und verwenden möchtest
- Wenn du die ASF-Strukturgröße minimieren willst, indem du die Laufzeitanforderungen selbst handhabst

Bedenke jedoch, dass du in diesem Fall für die .NET Core Runtime verantwortlich bist. Das bedeutet, dass, wenn deine .NET Core SDK (Runtime) nicht verfügbar, veraltet oder defekt ist, ASF nicht funktioniert. Aus diesem Grund empfehlen wir dieses Setup nicht für Gelegenheitsnutzer, da du nun sicherstellen musst, dass dein .NET Core SDK (Runtime) den ASF-Anforderungen entspricht und ASF ausführen kann, anstatt dass **wir** sicherstellen, dass unsere mit ASF gebündelte .NET Core Runtime dies tun kann.

Für das generische Pakete kannst du dem gesamten betriebssystemspezifischen Leitfaden oben folgen, mit zwei kleinen Abweichungen. Zusätzlich zur Installation der .NET Core Prerequisites möchtest du auch das .NET Core SDK installieren, und anstatt eine betriebssystemspezifische `ArchiSteamFarm(.exe)` ausführbare Datei zu haben, hast du nun eine generische `ArchiSteamFarm.dll` Binärdatei. Alles andere ist genau gleich.

Mit zusätzlichen Schritten:

- Installiere **[.NET Core prerequisites](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)**.
- Installiere **[.NET Core SDK](https://www.microsoft.com/net/download)** (oder zumindest die Runtime), die für dein Betriebssystem geeignet ist. Du möchtest höchstwahrscheinlich ein Installationsprogramm verwenden. Ließ die **[Runtime-Anforderungen](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-de-DE#runtime-anforderungen)**, wenn du nicht sicher bist, welche Version du installieren sollst.
- Lade die **[aktuellste ASF-Version](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** in generischer Variante herunter.
- Entpack das Archiv an einen neuen Ort (und `chmod +x ArchiSteamFarm.sh` wenn du unter Linux/OS X bist).
- **[Konfiguriere ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)**.
- Starte ASF entweder mit einem Hilfs-Skript oder führe `dotnet /pfad/zu/ArchiSteamFarm.dll` aus deiner Lieblings-Shell heraus manuell aus.

Hilfs-Skripte (z.B. `ArchiSteamFarm.cmd` für Windows und `ArchiSteamFarm.sh` für Linux/OS X) befinden sich neben der `ArchiSteamFarm.dll` Binärdatei - diese sind nur in der generischen Variante enthalten. Du kannst sie verwenden, wenn du den Befehl `dotnet` nicht manuell ausführen willst. Du kannst auch eine Verknüpfung zu diesen Skripten erstellen, wie oben gezeigt, da sie Binär-Ersatz auf Skript-Basis bereitstellen sollen. Offensichtlich funktionieren Hilfs-Skripte nicht, wenn du .NET Core SDK nicht installiert hast und du keine `dotnet` ausführbare Datei in deinem `PATH` hast. Hilfs-Skripte sind völlig optional zu verwenden, du kannst jederzeit `dotnet /pfad/zu/ArchiSteamFarm.dll` manuell eingeben.
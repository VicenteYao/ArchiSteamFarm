# FAQ

Unser FAQ umfasst Standardfragen und Antworten, die du vielleicht hast. Für weniger häufig gestellte Fragen besuche bitte stattdessen unser **[erweitertes FAQ](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Extended-FAQ-de-DE)**.

# Inhaltsverzeichnis

- [Allgemein](#allgemein)
- [Vergleich mit ähnlichen Programmen](#vergleich-mit-ähnlichen-programmen)
- [Sicherheit / Datenschutz / VAC / Sperren / Nutzungsbedingungen](#sicherheit--datenschutz--vac--sperren--nutzungsbedingungen)
- [Sonstiges](#sonstiges)
- [Probleme](#probleme)

* * *

## Allgemein

### Was ist ASF?

### Warum behauptet das Programm, dass es auf meinem Konto nichts zum Farmen gibt?

### Warum ist mein Konto begrenzt?

Bevor du versuchst zu verstehen, was ASF ist, solltest du sicherstellen, dass du verstehst, was Steam Sammelkarten sind und wie man sie erhält, was in der offiziellen FAQ **[hier](https://steamcommunity.com/tradingcards/faq)** gut beschrieben ist.

Kurz gesagt, Steam-Sammelkarten sind sammelbare Gegenstände, für die du berechtigt bist, wenn du ein bestimmtes Spiel besitzt, und können für die Herstellung von Abzeichen, den Verkauf auf dem Steam-Markt oder für jeden anderen Zweck deiner Wahl verwendet werden.

Hier werden noch einmal die Kernpunkte genannt, weil Leute im Allgemeinen nicht mit ihnen einverstanden sind und so tun als ob diese nicht existieren würden:

- **Du musst das Spiel auf deinem Steam Account besitzen, um für die dazu gehörigen Kartenfunde berechtigt zu sein. Spiele die über die Steam-Familienbibliothek geteilt werden zählen nicht.**
- **Du kannst nicht unendlich lange sammeln, jedes Spiel hat eine feste Anzahl an Kartenfunde. Sobald du alle Karten gesammelt hast (ungefähr die Hälfte eines vollständigen Satzes), ist das Spiel kein Idling-Kandidat mehr. It doesn't matter whether you've sold, crafted or forgot what happened to those cards you've obtained, once you run out of card drops, the game is finished.**
- **You can't drop cards from F2P games without spending any money in them. Dies beinhaltet dauerhafte F2P Spiele so wie Team Fortress 2 oder Dota 2. Owning F2P games does not grant you with card drops.**
- **You can't drop cards on [limited accounts](https://support.steampowered.com/kb_article.php?ref=3330-iagk-7663), regardless of owned games. Es war in der Vergangenheit möglich, aber dies ist nicht mehr der Fall.**
- **Paid games that you've obtained for free during a promotion can't be idled for card drops, regardless of what is displayed on the store page. Es war in der Vergangenheit möglich, aber dies ist nicht mehr der Fall.**

So as you can see, Steam cards are awarded to you for playing a game that you bought, or F2P game that you've put money into. If you play such game long enough, all cards for that game will eventually drop to your inventory, making it possible for you to complete a badge (after obtaining the remaining half of the set), sell them, or do whatever else you want.

Nun, da wir die Grundlagen von Steam erklärt haben, können wir ASF erklären. The program itself is quite complex to understand fully, so instead of digging into all the technical details, we'll offer a very simplified explanation below.

ASF meldet sich über unseren integrierten mini Steam-Client mit den von dir angegebenen Anmeldeinformationen bei deinem Steam-Konto an. Nach erfolgreicher Anmeldung analysiert es deine **[Abzeichen](https://steamcommunity.com/my/badges)** um Spiele zu finden, die für das Sammeln verfügbar sind (Du kannst X weitere Karten erhalten, wenn du dieses Spiel spielst). Nachdem alle Seiten analysiert und eine endgültige Liste der verfügbaren Spiele erstellt wurde, wählt ASF den effizientesten Sammel-Algorithmus aus und startet den Prozess. Der Prozess hängt von dem gewählten **[Karten-Sammel-Algorithmus](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE)** ab, aber normalerweise besteht er aus dem Spielen des geeigneten Spiels und der periodischen (plus bei jedem Kartendrop) Überprüfung, ob das Spiel bereits vollständig gesammelt wurde - wenn ja, kann ASF mit dem nächsten Spiel fortfahren, mit dem gleichen Verfahren, bis alle Spiele vollständig im gesammelt wurden.

Beachte, dass die obige Erklärung vereinfacht ist und nicht Dutzende von zusätzlichen Features und Funktionen beschreibt die ASF anbietet. Besuche den Rest von **[unserem Wiki](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home-de-DE)**, wenn du jedes Detail von ASF wissen willst. Ich habe versucht es einfach zu halten, damit es für alle verständlich ist, ohne technische Details einzubringen - fortgeschrittene Benutzer werden ermutigt, tiefer zu graben.

Als Programm bietet ASF etwas Magie an. Erstens, es muss keine deiner Spieldateien herunterladen, es kann sofort Spiele spielen. Zweitens ist es völlig unabhängig von deinem normalen Steam-Client - du musst den Steam-Client nicht laufen lassen oder gar installiert haben. Drittens ist es eine automatisierte Lösung - was bedeutet, dass ASF automatisch alles hinter deinem Rücken erledigt, ohne dass du ihm sagen musst, was er tun soll - was dir Ärger und Zeit erspart. Letztendlich muss es das Steam-Netzwerk nicht durch Prozessemulation (die z.B. Idle Master verwendet) austricksen, da es direkt mit ihm kommunizieren kann. Es ist auch superschnell und leicht und ist eine erstaunliche Lösung für alle, die Karten ohne großen Aufwand bekommen wollen - es ist besonders nützlich, wenn man es im Hintergrund laufen lässt, während man etwas anderes macht oder sogar im Offline-Modus spielt.

All das oben Gesagte ist schön, aber ASF hat auch einige technische Einschränkungen, die von Steam erzwungen werden - wir können keine Spiele sammeln, die du nicht besitzt, wir können keine Spiele für immer sammeln um zusätzliche Karten über das erzwungene Limit hinaus zu bekommen, und wir können keine Spiele sammeln, während du spielst. All das sollte "logisch" sein, wenn man bedenkt wie ASF funktioniert. Aber man sollte beachten, dass ASF keine Superkräfte hat und nichts tun wird, was physisch unmöglich ist, also denk daran - es ist im Grunde genommen dasselbe, als ob du jemandem gesagt hättest, dass er sich von einem anderen PC aus in dein Konto einloggen und diese Spiele für dich sammeln soll.

Zusammenfassend lässt sich sagen: ASF ist ein Programm, das dir hilft, die Karten, für die du berechtigt bist, ohne großen Aufwand sammeln zu lassen. Es bietet auch einige andere Funktionen, aber lasst uns vorerst bei dieser bleiben.

* * *

### Muss ich meine Zugangsdaten angeben?

**Ja**. ASF benötigt deine Konto-Anmeldeinformationen auf die gleiche Weise wie der offizielle Steam-Client, da er die gleiche Methode für die Steam-Netzwerkinteraktion verwendet. Dies bedeutet jedoch nicht, dass du deine Zugangsdaten in ASF-Konfigurationen eingeben musst, du kannst ASF weiterhin mit `null`/leerem `SteamLogin` und/oder `SteamPassword` verwenden und bei Bedarf diese Daten für jeden ASF-Lauf eingeben (sowie mehrere andere Zugangsdaten, siehe Konfiguration). Auf diese Weise werden deine Daten nirgendwo gespeichert, aber natürlich kann ASF ohne deine Hilfe nicht automatisch starten. ASF bietet auch viele andere Möglichkeiten deine **[Sicherheit](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Security-de-DE)** zu erhöhen. Wenn du ein fortgeschrittener Benutzer bist solltest du nicht zögern und diesen Teil des Wikis lesen. Wenn du das nicht bist und deine Konto-Anmeldeinformationen nicht in die ASF-Konfigurationen einfügen möchtest, dann lass es einfach und gebe sie stattdessen erst bei Bedarf ein, z.B. wenn ASF nach ihnen fragt.

Bedenke, dass ASF für deinen persönlichen Gebrauch bestimmt ist und deine Zugangsdaten werden deinen Computer niemals verlassen. Du teilst sie auch mit niemandem, was Steam-Nutzungsbedingungen erfüllt - eine sehr wichtige Sache, die viele Leute vergessen. Du schickst deine Daten nicht an unsere Server oder einen Dritten, sondern nur direkt an Steam-Server, die von Valve betrieben werden. Wir kennen deine Zugangsdaten nicht und können sie auch nicht für dich wiederherstellen, unabhängig davon, ob du sie in deine Konfigurationen eingefügt hast oder nicht.

* * *

### Wie lange muss ich warten bis ich Karten erhalte?

**So lange wie es eben dauert** - ernsthaft. Jedes Spiel hat unterschiedliche Sammel-Schwierigkeiten, die vom Entwickler/Herausgeber festgelegt werden, und es liegt ganz an ihnen, wie schnell die Karten gesammelt werden. Die Mehrheit der Spiele folgt 1 Karte pro 30 Minuten Spielzeit, aber es gibt auch Spiele die von dir verlangen sogar mehrere Stunden zu spielen, bevor du eine Karte bekommst. Darüber hinaus kann es sein, dass dein Konto daran gehindert wird, Karten von Spielen zu erhalten, die du noch nicht lange genug gespielt hast, wie im Abschnitt **[Performance](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE)** beschrieben. Versuche nicht zu erraten, wie lange ASF den gegebenen Titel sammeln soll - es liegt weder an dir, noch an ASF dies zu entscheiden. Es gibt nichts was du tun kannst um es zu beschleunigen und es gibt keinen "Bug", der damit zusammenhängt, dass Karten nicht pünktlich gesammelt werden - du kontrollierst den Karten-Sammel-Prozess nicht, ebenso wenig wie ASF. Im besten Fall erhältst du durchschnittlich 1 Karte pro 30 Minuten. Im schlimmsten Fall erhältst du auch 4 Stunden nach dem Start von ASF keine Karte. Beide dieser Situationen sind normal und werden in unserem Abschnitt **[Performance](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE)** beschrieben.

* * *

### Das Sammeln dauert so lange - Kann ich es irgendwie beschleunigen?

Das Einzige, was die Geschwindigkeit des Sammelns stark beeinflusst ist der gewählte **[Sammel-Algorithmus](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE)** für die jeweilige Bot-Instanz. Alles andere hat einen unwesentlichen Effekt und beschleunigt das Sammeln nicht, wobei manche Dinge, wie zum Beispiel das mehrfache Starten von ASF es sogar **schlechter** machen. Wenn du wirklich den Drang hast, jede einzelne Sekunde des Sammel-Prozesses zu machen, dann erlaubt ASF dir einige grundlegende Sammel-Variablen wie `FarmingDelay` zu optimieren - alle von ihnen sind in der **[Konfiguration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)** erklärt. Nichtsdestotrotz ist dieser Effekt unwesentlich und den entsprechenden Sammel-Algorithmus für das jeweilige Konto zu wählen ist die einzige ausschlaggebende Wahl, die die Geschwindigkeit schwer beeinflussen kann, während alles andere rein kosmetisch ist. Anstatt dich wegen der Geschwindigkeit zu sorgen, solltest du einfach ASF starten und es seinen Job machen lassen - Ich kann dir versichern, dass es das auf die effektivste Art macht, die wir uns ausdenken konnten. Je weniger du dich sorgst, desto zufriedener wirst du sein.

* * *

### Aber ASF sagte, dass das Sammeln etwa X Zeit in Anspruch nehmen wird!

ASF gibt dir eine grobe Schätzung basierend auf der Anzahl der Karten, die du sammeln musst, und dem gewählten Algorithmus - dies ist bei weitem nicht annähernd die tatsächliche Zeit, die du für das Sammeln benötigen wirst, die in der Regel länger ist als diese, da ASF nur den besten Fall annimmt, und ignoriert alle Steam-Netzwerk-Eigenarten, Internet-Trennungen, Überlastung der Steam-Server und ähnliches. Es ist nur als allgemeiner Indikator zu verstehen, wie lange man mit dem Sammeln von ASF rechnen kann, im besten Fall sehr oft, da die tatsächliche Zeit variiert, in einigen Fällen sogar deutlich. Wie oben erwähnt, versuche nicht zu erraten wie lange das Spiel gesammelt wird, es liegt nicht an dir und auch nicht an ASF dies zu entscheiden.

* * *

### Kann ASF auf meinem Android/Smartphone funktionieren?

ASF ist ein C#-Programm, das eine funktionierende Implementierung von .NET Core erfordert. Derzeit gibt es keinen nativen .NET Core Build für Android selbst, aber es gibt ordentliche und funktionierende Builds für Linux auf ARM-Architektur, so dass es durchaus möglich ist, so etwas wie **[Linux Deploy](https://play.google.com/store/apps/details?id=ru.meefik.linuxdeploy)** für die Installation von Linux zu verwenden, dann ASF in einem solchen Linux Chroot wie gewohnt zu verwenden.

Es ist sehr wahrscheinlich, dass wir in Zukunft mit einem funktionierenden .NET Core für Android rechnen können.

* * *

### Kann ASF Gegenstände aus Steam-Spielen wie CS:GO oder Unturned sammeln?

**Nein**, dies verstößt gegen Steam-Nutzungsbedingungen und Valve hat dies mit der letzten Welle von Community-Sperren für das Sammeln von TF2-Gegenständen deutlich gemacht. ASF ist ein Steam-Karten-Sammel-Programm, kein Spiele-Gegenstände-Sammel-Programm - es hat keine Möglichkeit Spiele-Gegenstände zu sammeln und es ist nicht geplant ein solches Feature in der Zukunft hinzuzufügen, hauptsächlich wegen der Verletzung der Steam-Nutzungsbedingungen. Bitte stell keine Fragen dazu - das Allerbeste was du davon hast ist ein Report von einem salzigen Benutzer und du hast Probleme. Das Gleiche gilt für alle anderen Arten des Sammelns, wie z.B. Kisten von CS:GO Übertragungen. ASF konzentriert sich ausschließlich auf Steam-Handelskarten.

* * *

### Kann ich mir aussuchen welche Spiele gesammelt werden sollen?

**Ja**, auf verschiedene Arten. Wenn du die Standardreihenfolge der Sammel-Warteschlange ändern möchtest, dann ist das das, wofür `FarmingOrders` **[Bot Konfigurations-Eigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#bot-konfiguration)** verwendet werden kann. Wenn du bestimmte Spiele manuell auf die schwarze Liste setzen möchtest, damit sie nicht automatisch gesammelt werden, kannst du die schwarze Liste verwenden, die mit `ib` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** verfügbar ist. Wenn du alles sammeln möchtest, aber einigen Spielen Vorrang vor allem anderen geben möchtest, dann ist das das, wofür die Sammel-Prioritäts-Warteschlange, die mit `iq` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** verfügbar ist, verwendet werden kann. Und schließlich, wenn du nur bestimmte Spiele deiner Wahl sammeln möchtest, dann kannst du `IdlePriorityQueueOnly` **[Bot Konfigurations-Eigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#bot-konfiguration)** verwenden, um dies zu erreichen, zusammen mit dem Hinzufügen deiner ausgewählten Spiele zur Sammel-Prioritäts-Warteschlange.

Zusätzlich zur Verwaltung des oben beschriebenen Moduls für das automatische Karten-Sammeln kannst du ASF auch mit dem `play` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** in den manuellen Sammel-Modus umschalten oder einige andere externe Einstellungen wie `GamesPlayedWhileIdle` **[Bot Konfigurations-Eigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#bot-konfiguration)** verwenden.

* * *

### I'm not interested in card drops, I'd like to idle hours played instead, is that possible?

Ja, ASF erlaubt es Ihnen, dies auf mehrere Arten zu tun.

The most optimal way to achieve that is to make use of **[`GamesPlayedWhileIdle`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#gamesplayedwhileidle)** configuration property, which will idle your chosen appIDs when ASF has no cards to idle. If you'd like to idle your games all the time, even if you do have card drops from other games, then you can combine it with **[`IdlePriorityQueueOnly`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#idlepriorityqueueonly)**, so ASF will idle only those games for card drops that you explicitly set, or **[`Paused`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#paused)**, which will cause cards farming module to be paused until you unpause it yourself.

Alternatively, you can make use of the **[`play`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands#commands-1)** command, which will cause ASF to play your selected games. However, keep in mind that `play` should be used only for games you want to idle temporarily, as it's not a persistent state, causing ASF to revert back to default state e.g. upon disconnection from Steam network. Therefore, we recommend you to use `GamesPlayedWhileIdle`, unless you indeed want to start your selected games just for a short time period, and then revert back to general flow.

* * *

### Ich bin ein Linux / OS X Benutzer - Kann ASF Spiele sammeln die für mein Betriebssystem nicht verfügbar sind? Wird ASF 64-Bit-Spiele sammeln, wenn ich es auf einem 32-Bit-Betriebssystem ausführe?

Ja, ASF kümmert sich nicht einmal um das Herunterladen aktueller Spieldateien, so dass es mit all deinen Lizenzen funktioniert, die an dein Steam-Konto gebunden sind, unabhängig von Plattform oder technischen Anforderungen. Es sollte auch für Spiele funktionieren, die an eine bestimmte Region gebunden sind (region-locked Spiele), auch wenn du nicht in der passenden Region bist, obwohl wir dies nicht getestet haben.

* * *

## Vergleich mit ähnlichen Programmen

* * *

### Ist ASF ähnlich wie Idle Master?

Die einzige Ähnlichkeit ist der allgemeine Zweck beider Programme, nämlich das Spielen von Steam-Spielen, um Karten zu erhalten. Alles andere, einschließlich der eigentlichen Sammel-Methode, der verwendeten Algorithmen, der Programmstruktur, der Funktionalität, der Kompatibilität, die mit dem Quellcode selbst endet, ist völlig unterschiedlich und diese beiden Programme haben nichts gemeinsam, auch nicht die Kerngrundlage (IM läuft auf .NET Framework, ASF auf .NET Core). ASF wurde geschaffen, um IM-Probleme zu lösen, die mit einer einfachen Programmcode-Editierung nicht zu lösen waren - deshalb wurde ASF von Grund auf neu geschrieben, ohne eine einzige Programmzeile oder gar eine allgemeine Idee des IM zu verwenden, weil dieser Code und diese Ideen von Anfang an völlig fehlerhaft waren. IM und ASF sind wie Windows und Linux - beide sind Betriebssysteme und beide können auf deinem PC installiert werden, aber sie haben fast nichts miteinander zu tun, abgesehen davon, dass sie einem ähnlichen Zweck dienen.

Aus diesem Grund solltest du ASF nicht mit IM vergleichen, basierend auf den Erwartungen von IM. Du solltest ASF und IM als völlig unabhängige Programme mit eigenen exklusiven Funktionalitäten betrachten. Einige von ihnen überschneiden sich tatsächlich und man kann in beiden ein bestimmtes Feature finden, aber sehr selten, da ASF seinen Zweck mit einem völlig anderen Ansatz als IM erfüllt.

* * *

### Lohnt es sich ASF zu verwenden, wenn ich gerade Idle Master verwende und es für mich gut funktioniert?

**Ja**. ASF ist viel zuverlässiger und beinhaltet viele eingebaute Funktionen, die **entscheidend** sind, unabhängig davon, wie du sammelst, den IM einfach nicht anbietet.

ASF hat die richtige Logik für **unveröffentlichte Spiele** - IM wird versuchen, Spiele zu sammeln, die bereits Karten hinzugefügt haben, auch wenn sie noch nicht veröffentlicht wurden. Natürlich ist es nicht möglich, diese Spiele bis zum Veröffentlichungsdatum zu sammeln, so dass dein Sammel-Prozess stecken bleibt. Dazu musst du es entweder zur Blacklist hinzufügen, auf die Freigabe warten oder manuell überspringen. Keine dieser Lösungen ist gut, und alle erfordern deine Aufmerksamkeit - ASF überspringt automatisch das Sammeln unveröffentlichter Spiele (vorübergehend) und kehrt später zu ihnen zurück, wenn sie es sind, um das Problem vollständig zu vermeiden und effizient damit umzugehen.

ASF hat auch die richtige Logik für **Serien** Videos. Es gibt viele Videos auf Steam, die Karten haben, aber mit falscher `appID` auf der Abzeichen-Seite angegeben sind, wie z.B. **[Double Fine Adventure](https://store.steampowered.com/app/402590)** - IM wird die falsche `appID` sammeln, was keine Karten ergibt und der Prozess stecken bleibt. Wieder einmal musst du es entweder auf die schwarze Liste setzen oder manuell überspringen, wobei beide deine Aufmerksamkeit erfordern. ASF entdeckt automatisch die richtige `appID` für das Sammeln von Karten.

Darüber hinaus ist ASF **viel stabiler und zuverlässiger**, wenn es um Netzwerkprobleme und Steam-Macken geht - es funktioniert meistens und erfordert deine Aufmerksamkeit überhaupt nicht, wenn es einmal konfiguriert ist, während IM oft für viele Leute abbricht, zusätzliche Korrekturen erfordert oder trotzdem einfach nicht funktioniert. Es ist auch völlig abhängig von deinem Steam-Client, was bedeutet, dass es nicht funktionieren kann, wenn dein Steam-Client ernste Probleme hat. ASF funktioniert ordnungsgemäß, solange es mit dem Steam-Netzwerk verbunden werden kann, und erfordert nicht, dass der Steam-Client ausgeführt oder sogar installiert wird.

Dies sind 3 **sehr wichtige** Punkte, warum du ASF verwenden solltest, da sie jeden der Steam-Karten sammelt direkt betreffen und es keine Möglichkeit gibt zu sagen "das betrifft mich nicht", da Steam-Wartungen und -Pannen jedem passieren. Es gibt Dutzende von zusätzlichen, unwichtigeren und wichtigeren Gründen, die du im Rest des FAQs erfahren kannst. Also kurz gesagt, **ja**, du solltest ASF verwenden, auch wenn du keine zusätzliche ASF-Funktion brauchst, die im Vergleich zu IM verfügbar ist.

Darüber hinaus wird IM offiziell aufgegeben und könnte in Zukunft komplett kaputt gehen, ohne dass sich jemand die Mühe macht, es zu reparieren, wenn man bedenkt, dass es viel leistungsfähigere Lösungen gibt (nicht nur ASF). IM funktioniert bereits für viele Leute nicht mehr, und diese Zahl nimmt nur zu, nicht ab. Du solltest es vermeiden, veraltete Software zu verwenden, nicht nur IM, sondern auch alle anderen veralteten Programme. Kein aktiver Verwalter bedeutet, dass sich niemand darum kümmert, ob es funktioniert oder nicht, niemand verifiziert, ob es funktioniert, und **niemand ist für seine Funktionalität** verantwortlich, was für die Sicherheit entscheidend ist.

* * *

### Welche interessanten Features bietet ASF, die Idle Master nicht bietet?

Es hängt davon ab, was für dich "interessant" ist. Wenn du vorhast, mehr als ein Konto zu nutzen, dann ist die Antwort bereits offensichtlich, da ASF es dir erlaubt, auf allen zu Sammeln mit einer übergeordneten Gesamtlösung, Ressourcen zu sparen, Probleme mit Ärger und Kompatibilität zu lösen. Wenn du allerdings diese Frage stellst, dann hast du höchstwahrscheinlich nicht diesen speziellen Bedarf, also lass uns andere Vorteile bewerten, die für ein einzelnes Konto gelten, das in ASF verwendet wird.

In erster Linie hast du einige eingebaute Funktionen, die **[oben](#lohnt-es-sich-asf-zu-verwenden-wenn-ich-gerade-idle-master-verwende-und-es-für-mich-gut-funktioniert)** erwähnt werden, die der Kern für das Sammeln sind, unabhängig von deinem Endziel, und sehr oft reicht das allein schon aus, um ASF zu verwenden. Aber das weißt du bereits, also laßt uns zu einigen weiteren interessanten Features kommen:

- **Du kannst offline sammeln** (`OnlineStatus` von `Offline` Feature). Das Offline-Sammeln ermöglicht es dir, deinen Steam-In-Game-Status komplett zu überspringen, was es dir ermöglicht, mit ASF zu sammeln, während du gleichzeitig "Online" auf Steam bist, ohne dass deine Freunde überhaupt merken, dass ASF in deinem Namen ein Spiel spielt. Dies ist eine überlegene Funktion, da es dir erlaubt, online in deinem Steam-Client zu bleiben, ohne deine Freunde mit ständigen Spieländerungen zu belästigen oder sie in die Irre zu führen, dass du ein Spiel spielst, während du es in Wirklichkeit nicht bist. Allein dieser Punkt macht es sinnvoll, ASF zu verwenden, wenn man seine eigenen Freunde respektiert, aber es ist nur der Anfang. Es ist auch schön zu beachten, dass diese Funktion nichts mit den Privatsphäre-Einstellungen von Steam zu tun hat - wenn du das Spiel selbst startest, dann wirst du deinen Freunden richtig als In-Game angezeigt, so dass nur der ASF-Teil unsichtbar wird und dein Konto überhaupt nicht beeinträchtigt wird.

- **Du kannst erstattungsfähige Spiele überspringen** (`IdleRefundableGames` Feature). ASF hat eine eingebaute Logik für rückerstattungsfähige Spiele und du kannst ASF so konfigurieren, dass es keine rückerstattungsfähige Spiele automatisch sammelt. Auf diese Weise kannst du selbst beurteilen, ob dein neu gekauftes Spiel aus dem Steam-Shop dein Geld wert war, ohne dass ASF versucht, so schnell wie möglich Karten zu sammeln. Wenn du es für 2+ Stunden spielst, oder 2 Wochen seit deinem Kauf vergangen sind, dann wird ASF mit diesem Spiel fortfahren, da es nicht mehr rückerstattungsfähig ist. Bis dahin hast du die volle Kontrolle, ob du es magst oder nicht, und du kannst es bei Bedarf leicht zurückerstatten, ohne dass du dieses Spiel manuell auf die schwarze Liste setzen oder ASF für die gesamte Dauer nicht verwenden musst.

- **Du kannst neue Gegenstände automatisch als erhalten markieren** (`BotBehaviour` von `DismissInventoryNotifications` Feature). Das Sammeln mit ASF führt dazu, dass dein Konto neue Karten erhält. Du weißt bereits, dass dies geschehen wird, also lass ASF diese unnötige Benachrichtigung für dich lesen und sicherstellen, dass nur wichtige Dinge deine Aufmerksamkeit gewinnen. Natürlich nur wenn du willst.

- **Du kannst automatisch Karten von Steam Events** erhalten (`AutoSteamSaleEvent` Feature). ASF ermöglicht es dir, die Entdeckungsliste während des Steam-Sales zu automatisieren, aber natürlich nur, wenn du das auch möchtest. Dies spart jeden Tag enorm viel Zeit, während der Steam-Sale stattfindet, und stellt sicher, dass du deine täglichen Karten nie wieder verpassen wirst.

- **Du kannst die bevorzugte Sammel-Reihenfolge mit mehr verfügbaren Optionen anpassen** (`FarmingOrders` Feature). Vielleicht möchtest du zuerst deine neu gekauften Spiele sammeln? Oder deine ältesten? Nach der Anzahl der Karten? Abzeichen-Level, die du bereits hergestellt hast? Gespielte Stunden? Alphabetisch? Nach den AppIDs? Oder komplett zufällig? Das liegt ganz bei dir.

- **ASF kann dir helfen, deine Sets zu vervollständigen** (`TradingPreferences` mit `SteamTradeMatcher` Feature). Mit etwas fortgeschrittenerem Tüfteln kannst du deinen ASF in einen vollwertigen Benutzer-Bot umwandeln, der automatisch **[STM](https://www.steamtradematcher.com)** Handelsangebote akzeptiert und dir jeden Tag hilft, deine Sets ohne jegliche Benutzerinteraktion zu ergänzen. ASF enthält sogar ein eigenes ASF 2FA-Modul, mit dem du deinen mobilen Steam-Authentifikator importieren und den gesamten Prozess vollständig automatisieren kannst, auch bei der Annahme von Bestätigungen. Oder möchtest du vielleicht manuell akzeptieren und ASF nur diese Handelsangebote für dich vorbereiten lassen? Das liegt wieder einmal ganz bei dir.

- **ASF kann Produktschlüssel im Hintergrund für dich einlösen** (**[Hintergrundproduktschlüsselaktivierer](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer-de-DE)** Feature). Vielleicht hast du hundert Produktschlüssel aus verschiedenen Bundles wo du zu faul bist alle einzulösen, da du bei jedem durch ein paar Fenster gehen und den Steam-Bedingungen immer wieder zustimmen musst. Warum nicht einfach die Liste der Produktschlüssel zu ASF kopieren und es seine Arbeit machen lassen? ASF löst automatisch alle diese Produktschlüssel im Hintergrund ein und stellt dir entsprechende Ausgaben zur Verfügung, damit du weißt, wie sich jeder Einlösungsversuch ausgewirkt hat. Außerdem, wenn du hunderte von Produktschlüssel hast, wirst du garantiert früher oder später von Steam rate-limited, und ASF weiß auch davon, es wird geduldig warten, bis das Rate-Limit weggeht, und dort weitermachen, wo es aufgehört hat.

Wir könnten nun mit dem gesamten **[ASF-Wiki](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home-de-DE)** fortfahren und auf jedes einzelne Feature des Programms hinweisen, aber wir müssen irgendwo eine Grenze ziehen. Das ist es, dies ist eine Liste von Features, die du als ASF-Benutzer genießen kannst, wo nur eine davon leicht als ein Hauptgrund angesehen werden könnte, nie zurückzuschauen, und wir haben tatsächlich **eine Menge** von ihnen aufgelistet, mit noch mehr, über die du auf dem Rest unseres Wikis mehr erfahren kannst. Ah ja, und wir sind nicht einmal ins Detail gegangen, mit Dingen wie ASFs API, die es dir ermöglicht, deine eigenen Befehle zu skripten, oder glorreiches Bots-Management, da wir es einfach halten wollten.

* * *

### Ist ASF schneller als Idle Master?

**Ja**, obwohl die Erklärung ziemlich kompliziert ist.

Bei jedem neuen Prozess, der in deinem System gestartet und beendet wird, sendet der Steam-Client automatisch eine Anfrage mit allen deinen Spielen, die du gerade spielst - so kann das Steam-Netzwerk Stunden berechnen und Karten ausgeben. Das Steam-Netzwerk zählt jedoch deine Spielzeit in Intervallen von 1 Sekunde, und das Senden einer neuen Anfrage setzt den aktuellen Status zurück. Mit anderen Worten, wenn du alle 0,5 Sekunden einen neuen Prozess starten/abbrechen würdest, würdest du nie eine Karte sammeln, weil jeder 0,5-Sekunden-Steam-Client eine neue Anfrage senden würde und das Steam-Netzwerk nie auch nur 1 Sekunde Spielzeit zählen würde. Außerdem ist es aufgrund der Funktionsweise des Betriebssystems durchaus üblich, neue Prozesse zu erkennen, die ohne dass du überhaupt etwas tust, also selbst wenn du nichts auf deinem PC tust - es gibt viele Prozesse, die immer noch im Hintergrund arbeiten und andere Prozesse die ganze Zeit über treiben/beenden. Idle Master basiert auf dem Steam Client, so dass dieser Mechanismus dich beeinflusst, wenn du ihn benutzt.

ASF basiert nicht auf dem Steam-Client, sondern hat eine eigene Steam-Client-Implementierung. Dank dessen ist das, was ASF tut, nicht das Auslösen eines Prozesses, sondern das Senden einer echten Anfrage an das Steam-Netzwerk, dass wir mit dem Spielen eines Spiels begonnen haben. Dies ist die gleiche Anfrage, die vom Steam-Client gesendet wird, aber da wir die tatsächliche Kontrolle über den ASF-Steam-Client haben, brauchen wir keine neuen Prozesse zu starten, und wir imitieren den Steam-Client nicht bezüglich der Sendeanforderung bei jeder Prozessänderung, so dass der oben beschriebene Mechanismus uns nicht betrifft. Dank dessen unterbrechen wir nie das 1-Sekunden-Intervall auf Steam, was unsere Sammel-Geschwindigkeit erhöht.

* * *

### Aber ist der Unterschied wirklich spürbar?

Nein. Die Unterbrechungen, die mit einem normalen Steam-Client und dem Idle Master auftreten, haben nur einen geringen Einfluss auf die Kartendrops, so dass es kein spürbarer Unterschied ist, der ASF überlegen machen würde.

Jedoch **gibt es** einen Unterschied, und du kannst deutlich feststellen, dass, je nachdem, wie beschäftigt dein Betriebssystem ist, Karten **werden** schneller gesammelt, von ein paar Sekunden auf sogar ein paar Minuten, wenn du extrem unglücklich bist. Obwohl ich nicht in Betracht ziehen würde, ASF nur zu verwenden, weil es Karten schneller sammelt, da sowohl ASF als auch Idle Master von der Funktionsweise des Steam-Netzes beeinflusst werden, interagiert ASF einfach effektiver mit dem Steam-Netz, während Idle Master nicht kontrollieren kann, was der Steam-Client tatsächlich tut (also ist es nicht die Schuld von Idle Master, sondern die des Steam-Clients selbst).

* * *

### Kann ASF mehrere Spiele auf einmal sammeln?

**Ja**, obwohl ASF besser weiß, wann dieses Feature zu verwenden ist, basierend auf dem ausgewählten **[Karten-Sammel-Algorithmus](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE)**. Die Kartendrops-Rate beim Sammeln mehrerer Spiele ist nahe Null, deshalb verwendet ASF mehrere Spiele, die ausschließlich für Stunden sammeln, um `HoursUntilCardDrops` schneller zu überwinden, für bis zu `32` Spiele auf einmal. Deshalb solltest du dich auch auf den Teil der Konfiguration von ASF konzentrieren und den Algorithmus entscheiden lassen, was der optimale Weg ist, um das Ziel zu erreichen - was du für richtig hältst, ist in Wirklichkeit nicht unbedingt richtig, denn das Sammeln mehrerer Spiele auf einmal wird dir keine Kartendrops bescheren.

* * *

### Kann ASF schnell durch die Spiele springen?

**Nein**, ASF unterstützt nicht und fördert auch nicht die Verwendung von **[Steam-Pannen](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE#steam-pannen)**.

* * *

### Kann ASF jedes Spiel automatisch für X Stunden spielen, bevor Karten gesammelt werden?

**Nein**, der Sinn der Systemumstellung von Steam-Karten war es, mit falschen Statistiken und Geisterspielern zu kämpfen. ASF wird nicht mehr als nötig dazu beitragen, denn das Hinzufügen eines solchen Features ist nicht geplant und wird nicht geschehen. Wenn dein Spiel auf übliche Weise Kartendrops erhält, wird ASF diese so schnell wie möglich sammeln.

* * *

### Kann ich ein Spiel spielen während ASF am Sammeln ist?

**Nein**. ASF hat im Gegensatz zu IM einen unabhängigen Steam-Client integriert, und das Steam-Netzwerk erlaubt es nur **einem Steam-Client auf einmal**, ein Spiel zu spielen. Du kannst die ASF-Verbindung jedoch jederzeit trennen, indem du ein Spiel startest (und auf "OK" klickst, wenn du gefragt wirst, ob das Steam-Netzwerk einen anderen Client trennen soll) - ASF wird dann geduldig warten, bis du fertig bist, und den Prozess danach fortsetzen. Alternativ kannst du immer noch im Offline-Modus spielen, wann immer du willst, wenn das für dich befriedigend ist.

Bedenke, dass die Drop-Rate der Karten, wenn du mehrere Spiele spielst, ohnehin nahe bei 0 liegt, daher gibt es keine direkten Vorteile, wenn du das mit IM machen kannst, während es starke Vorteile gibt, wenn du dich nicht in andere Spiele einmischst, die mit ASF gestartet wurden, was z.B. für VAC entscheidend ist.

* * *

## Sicherheit / Datenschutz / VAC / Sperren / Nutzungsbedingungen

* * *

### Kann ich eine VAC-Sperre bekommen, wenn ich ASF nutze?

Nein, das ist nicht möglich, da ASF (im Gegensatz zu Idle Master oder SAM) in keiner Weise den Steam-Client oder seine Prozesse stört. Es ist physisch unmöglich, eine VAC-Sperre für die Verwendung von ASF zu erhalten, selbst wenn man auf gesicherten Servern spielt, während ASF läuft - das liegt daran, dass **ASF nicht einmal verlangt, dass der Steam-Client überhaupt installiert ist**, um ordnungsgemäß zu funktionieren. ASF ist das einzige Sammel-Programm, das derzeit die VAC-Freiheit garantieren kann.

* * *

### Kann die Verwendung von ASF verhindern, dass ich auf VAC-geschützten Servern spiele kann, wie **[hier](https://support.steampowered.com/kb_article.php?ref=2117-ilzv-2837)** angegeben?

ASF benötigt keinen Steam-Client, der ausgeführt oder gar installiert wird. Nach diesem Konzept sollte es **keine** VAC-bezogenen Probleme verursachen, da ASF garantiert, dass der Steam-Client und alle seine Prozesse nicht gestört werden - das ist der wichtigste Punkt, wenn es um die VAC-freie Garantie geht, die ASF bietet.

Nach Angaben von Benutzern und nach bestem Wissen und Gewissen ist dies im Moment der Fall, da niemand irgendwelche Probleme wie im obigen Link während der Verwendung von ASF gemeldet hat. Wir konnten das obige Problem nicht mit ASF reproduzieren, während wir es mit Idle Master klar reproduzieren konnten.

Bedenke jedoch, dass Valve irgendwann immer noch ASF zur schwarzen Liste hinzufügen könnte, aber es ist ein kompletter Unsinn, denn selbst wenn sie das tun, könntest du immer noch VAC-gesicherte Spiele von deinem PC aus spielen und ASF gleichzeitig verwenden, z.B. auf deinem Server, also bin ich ziemlich sicher, dass sie sehr gut wissen, dass ASF kein verdächtiger VAC-technischer Verdächtiger sein sollte, und sie werden uns das Leben nicht schwerer machen, indem sie ASF ohne Grund auf die schwarze Liste setzen. Im schlimmsten Fall wirst du jedoch nicht spielen können, wie oben erwähnt, weil die VAC-freie Garantie von ASF immer noch da ist, unabhängig davon, ob Steam die ASF-Binärdatei auf die schwarze Liste setzt oder nicht (und du kannst ASF immer noch auf jeder anderen Maschine starten, ohne dass ein Steam-Client überhaupt installiert ist). Im Moment gibt es keinen Grund, irgendetwas davon zu tun, und wir hoffen, dass es so bleibt.

* * *

### Ist es sicher?

Wenn du fragst, ob ASF als Software sicher ist, was bedeutet, dass es deinem Computer keinen Schaden zufügt, deine privaten Daten nicht stiehlt, Viren oder ähnliches installiert - ist es sicher. ASF ist frei von Malware, Datendiebstahl, Kryptowährungsminern und jedem (und allen) anderen zweifelhaften Verhalten, das vom Benutzer als bösartig oder unerwünscht angesehen werden kann. Darüber hinaus haben wir einen speziellen Abschnitt **[Statistik](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics)**, der unsere Datenschutzerklärung und das ASF-Verhalten behandelt, das über das hinausgeht, was Sie mit dem Programm selbst konfiguriert haben.

Unser Quelltext ist Open-Source, und verteilte Binärdateien werden immer aus **[öffentlich verfügbaren Quellen](https://en.wikipedia.org/wiki/Open-source_software)** von **[automatisierten und vertrauenswürdigen kontinuierlichen Integrationssystemen](https://en.wikipedia.org/wiki/Build_automation)** und nicht einmal von Entwicklern selbst erstellt. Jeder Build ist reproduzierbar, indem er unserem Build-Skript folgt, und wird genau dasselbe ergeben, **[deterministisch](https://en.wikipedia.org/wiki/Deterministic_system)** IL (binärer) Code. Wenn du aus irgendeinem Grund unseren Builds nicht vertraust, kannst du ASF jederzeit aus dem Quelltext kompilieren und verwenden, einschließlich aller Bibliotheken, die ASF verwendet (wie SteamKit2), die ebenfalls Open-Source sind.

Am Ende ist es jedoch immer eine Frage des Vertrauens zu den Entwicklern deiner Anwendung, also solltest du selbst entscheiden, ob du ASF für sicher hältst oder nicht und deine Entscheidung möglicherweise mit den oben genannten technischen Argumenten unterstützen. Glaube nicht blind an etwas, nur weil ich es gesagt habe - überprüfe es selbst, denn das ist der einzige Weg, um sicher zu gehen.

* * *

### Kann ich dafür gesperrt werden?

Um diese Frage zu beantworten, sollten wir einen genaueren Blick auf die **[Steam-Nutzungsbedingungen](https://store.steampowered.com/subscriber_agreement)** werfen. Steam verbietet nicht die Verwendung mehrerer Konten, vielmehr **[erlauben sie es](https://support.steampowered.com/kb_article.php?ref=8625-WRAH-9030#share)**, da du denselben mobilen Authentifikator für mehr als ein Konto verwenden kannst. Was es jedoch nicht erlaubt, ist die gemeinsame Nutzung von Konten mit anderen Personen, aber das tun wir hier nicht.

Der einzige wirkliche Punkt, der ASF betrifft, ist der folgende:

> You may not use Cheats, automation software (bots), mods, hacks, or any other unauthorized third-party software, to modify or automate any Subscription Marketplace process.

Die Frage ist, was ist eigentlich der Subscription Marketplace-Prozess. Wie wir lesen können:

> An example of a Subscription Marketplace is the Steam Community Market

Wir ändern oder automatisieren den Prozess des Abonnenten-Marktplatzes nicht, wenn wir unter Abonnenten-Marktplatz den Markt der Steam-Community oder den Steam-Shop verstehen. Allerdings...

> Valve may cancel your Account or any particular Subscription(s) at any time in the event that (a) Valve ceases providing such Subscriptions to similarly situated Subscribers generally, or (b) you breach any terms of this Agreement (including any Subscription Terms or Rules of Use).

Daher ist ASF, wie bei jeder Steam-Software, nicht von Valve autorisiert und ich kann nicht garantieren, dass du nicht gesperrt wirst, wenn Valve plötzlich entscheidet, dass sie Konten mit ASF jetzt sperren. Dies ist angesichts der Tatsache, dass ASF auf mehr als einer halben Million Steam-Konten verwendet wird, äußerst unwahrscheinlich, aber dennoch eine Möglichkeit, unabhängig von der tatsächlichen Wahrscheinlichkeit.

Besonders weil:

> In regard to all Subscriptions, Contents and Services that are not authored by Valve, Valve does not screen such third party content available on Steam or through other sources. Valve assumes no responsibility or liability for such third party content. Some third party application software is capable of being used by businesses for business purposes - however, you may only acquire such software via Steam for private personal use.

Allerdings erkennt Valve klar an, dass es "Steam-Sammler" gibt, wie **[hier](https://support.steampowered.com/kb_article.php?ref=2117-ilzv-2837)** angegeben, also wenn du mich fragst, bin ich mir ziemlich sicher, dass sie, wenn sie nicht einverstanden mit ihnen wären, bereits etwas tun würden, anstatt darauf hinzuweisen, dass sie VAC-technisch Probleme verursachen könnten. Das Schlüsselwort hier ist **Steam** Sammler, zum Beispiel ASF, und nicht **Spiel** Sammler.

Bitte bedenke, dass oben nur unsere Interpretation der Steam Nutzungsbedingungen und verschiedenen Punkten steht - ASF ist unter der Apache 2.0 Lizenz lizenziert, was klar und deutlich sagt:

> Unless required by applicable law or agreed to in writing, ASF is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

Du benutzt diese Software auf eigene Gefahr. Es ist sehr unwahrscheinlich, dass man dafür gesperrt wird, aber wenn man es wird, kann man nur sich selbst die Schuld dafür geben.

* * *

### Wurde jemand dafür gesperrt?

**Ja**, wir hatten bereits ein paar Vorfälle, die zu einer Art Steam-Sperre führten. Ob ASF selbst die Ursache war oder nicht, ist eine ganz andere Geschichte, die wir wahrscheinlich nie erfahren werden.

Ein Fall war von einem Typen mit über 1000+ Bots, der vom Handel ausgeschlossen wurde (zusammen mit allen Bots), höchstwahrscheinlich aufgrund der übermäßigen Verwendung von `loot ASF`, das auf allen Bots gleichzeitig ausgeführt wurde, oder einer anderen verdächtigen einseitigen Menge von Handelsangeboten in sehr kurzer Zeit.

> Hello XXX, Thank you for contacting Steam Support. It looks like this account was used to manage a network of bot accounts. Botting is a violation of the Steam Subscriber Agreement.

Bitte, benutze etwas gesunden Menschenverstand und gehe nicht davon aus, dass du solche verrückten Dinge nur tun kannst, weil ASF dir das erlaubt. Das Ausführen von `loot ASF` auf über 1k Bots kann leicht als **[DDoS](https://en.wikipedia.org/wiki/DDoS)** Angriff angesehen werden, und ich persönlich bin nicht schockiert, dass jemand für so eine Sache gesperrt wurde. Bedenke und benutze etwas gesunden Menschenverstand und ein Minimum an fairem Gebrauch in Bezug auf den Steam Service, und * höchstwahrscheinlich* wird es dir gut gehen.

Ein weiterer Fall war ein Typ mit 170+ Bots, der während dem Steam Winter Sale gesperrt wurde.

> Your account was blocked for violation of the agreement of the subscriber Steam. Judging by the exchanges and other factors, this account was used to illegally collect collectible cards on Steam, as well as related and not only commercial activities. The account has been permanently blocked and Steam Support can not provide additional support on this issue.

Dieser Fall ist wieder einmal sehr schwer zu analysieren, da die Reaktion des Steam-Supports, der kaum Details bietet, vage ist. Basierend auf meinen persönlichen Ansichten hat dieser Benutzer wahrscheinlich Steam-Karten gegen irgendeine Art von Geld eingetauscht (Level-up-Bot?) oder auf andere Weise versucht, bei Steam Geld auszuzahlen. Falls du es nicht wusstest, ist dies nach Steam-Nutzungsbedingungen auch verboten.

Letzter Fall, bei dem ein Benutzer mit 120+ Bots wegen Verletzung von **[Steam-Online-Verhalten](https://store.steampowered.com/online_conduct?l=english)** gesperrt wurde.

> Hello XXX, Thank you for contacting Steam Support. This and other accounts were used for flooding our network infrastructure, which is a violation of Steam online conduct. The account has been permanently blocked and Steam Support can not provide additional support on this issue.

Dieser Fall ist aufgrund der zusätzlichen Angaben des Benutzers etwas einfacher zu analysieren. Anscheinend verwendete der Benutzer **eine sehr veraltete ASF-Version**, die einen Fehler enthielt, der dazu führte, dass ASF eine übermäßige Anzahl von Anfragen an Steam-Server sendete. Der Fehler selbst existierte zunächst nicht, wurde aber aufgrund einer Änderung von Steam aktiviert, die in einer späteren Version behoben wurde. **ASF wird nur in der **[aktuellen stabilen Version](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** unterstützt, die auf GitHub** veröffentlicht wurde. Software wird von Menschen geschrieben, und Menschen neigen dazu, Fehler zu machen. Wenn der Fehler einen globalen Umfang hat, wird er schnell behoben und als Bugfix für alle Benutzer veröffentlicht. Valve wird nicht plötzlich eine halbe Million ASF-Benutzer aufgrund meines Fehlers sperren, aus offensichtlichen Gründen. Wenn du jedoch absichtlich auf die Verwendung einer aktuellen Version von ASF verzichtest, dann bist du per Definition in einer sehr kleinen Minderheit von Benutzern, die **Vorfällen wie diesen** aufgrund von **keiner technischen Unterstützung** ausgesetzt sind, da niemand auf deine veraltete Version von ASF aufpasst, niemand sie repariert und niemand sicherstellt, dass du nicht einfach durch den Start von ASF völlig gesperrt wirst. **Bitte verwende die aktuellste Version**, nicht nur ASF, sondern auch alle anderen Anwendungen.

* * *

Alle oben genannten Vorfälle haben eines gemeinsam - ASF ist nur ein Programm und es ist **deine** Entscheidung, wie du es nutzen willst. Du bekommst keine Sperre für die direkte Verwendung von ASF, aber es kommt darauf an **wie** du ASF benutzt. Es kann ein Hilfsprogramm sein, das nur ein einziges Konto sammelt, oder ein riesiges Sammel-Netzwerk aus Tausenden von Bots. In jedem dieser Fälle biete ich keine rechtliche Beratung an, und du solltest dich überhaupt erst selbst über deine ASF-Nutzung entscheiden. Ich verheimliche keine Informationen, die dir helfen könnten, z.B. die Tatsache, dass ASF einige Leute gesperrt hat, da ich keinen Grund dazu habe - es ist deine Entscheidung, was du mit diesen Informationen machen willst. If you ask me - use some common sense, avoid owning more bots than our recommendation, do not send hundreds of trades at the same time, always use up-to-date ASF version and you *should* be fine. Every single incident of this nature for **some reason** always happened to people that have disregarded our recommendation and decided that they know better than us how many bots they can run. Ob es nur ein Zufall oder ein tatsächlicher Faktor ist, das liegt bei dir zu entscheiden. Ich biete keine rechtliche Beratung an, sondern gebe dir nur meine Gedanken, die du nützlich finden kannst, oder ignoriere sie ganz und gar und arbeite nur mit den oben genannten Fakten.

* * *

### Welche Datenschutzinformationen gibt ASF weiter?

Eine detaillierte Erklärung findest du im Abschnitt **[Statistiken](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-de-DE)**. Du solltest es lesen, wenn du dich um deine Privatsphäre sorgst, z.B. wenn du dich fragst, warum Konten, die in ASF verwendet werden, unserer Steam-Gruppe beitreten. ASF sammelt keine sensiblen Informationen und gibt sie nicht an Dritte weiter.

* * *

## Sonstiges

* * *

### Ich verwende ein nicht unterstütztes Betriebssystem z.B. 32-Bit-Windows, kann ich trotzdem die neueste ASF Version verwenden?

Ja, und diese Version wird auch unterstützt, nur nicht offiziell erstellt. Schaue dir hierfür die generische Varianten im Abschnitt **[Kompatibilität](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-de-DE)** an. ASF hat keine ausgeprägte Abhängigkeit vom Betriebssystem und es kann überall dort funktionieren, wo du eine funktionierende .NET Core Runtime, die auch 32-Bit Windows enthält, zum Laufen bekommen kannst, auch wenn es kein OS-spezifisches Paket für `win-x86` von uns gibt.

* * *

### ASF ist großartig! Kann ich spenden?

Ja, und wir freuen uns sehr zu hören, dass dir unser Projekt gefällt! Unter jeder **[Veröffentlichung](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** und auch **[auf der Hauptseite](https://github.com/JustArchiNET/ArchiSteamFarm)** findest du verschiedene Möglichkeiten eine Spende zu tätigen. Es ist gut zu wissen, dass wir neben allgemeinen Geldspenden auch Steam-Gegenstände akzeptieren, so dass dich nichts davon abhält, Skins, Keys oder einen kleinen Teil der Karten, die du mit ASF gesammelt hast, zu spenden. Natürlich nur wenn du möchtest. Vielen Dank im Voraus für deine Großzügigkeit!

* * *

### Ich verwende den Steam Parental Code um mein Konto zu schützen, muss ich sie irgendwo eingeben?

Ja, du musst ihn in der `SteamParentalCode` Bot-Konfigurationseigenschaft setzen. Dies liegt hauptsächlich daran, dass ASF auf viele geschützte Teile deines Steam-Kontos zugreift und es für ASF unmöglich ist ohne ihn zu arbeiten.

* * *

### Ich möchte nicht, dass ASF standardmäßig irgendwelche Spiele sammelt, aber ich möchte zusätzliche ASF-Funktionen verwenden. Ist das möglich?

Ja, wenn Sie ASF nur mit dem pausierten Karten-Farming-Modul starten wollen, können Sie die Bot-Konfigurationseigenschaft `Paused` auf `true` setzen, um dies zu erreichen. Dies ermöglicht es Ihnen, es während der Laufzeit `fortzusetzen`.

Wenn Sie das Karten-Farming-Modul vollständig deaktivieren und sicherstellen möchten, dass es nie ausgeführt wird, ohne dass Sie es explizit anders angeben, dann empfehlen wir, `IdlePriorityQueueOnly` auf `true` zu setzen, was den Leerlauf komplett deaktiviert, anstatt es nur anzuhalten, bis Sie die Spiele selbst in die Warteschlange für ungenutzte Prioritäten aufnehmen.

Wenn das Karten-Farming-Modul angehalten/deaktiviert ist, können Sie zusätzliche ASF-Funktionen nutzen, wie z. B. `GamesPlayedWhileIdle`.

* * *

### Kann ich ASF in die Taskleiste minimieren?

ASF ist eine Konsolenanwendung, es gibt kein zu minimierendes Fenster, da das Fenster von deinem Betriebssystem für dich erstellt wird. Du kannst jedoch ein beliebiges Drittanbieterprogramm verwenden wie z.B. **[RBTray](http://rbtray.sourceforge.net)** für Windows oder **[screen](https://linux.die.net/man/1/screen)** für Linux/OS X. Das sind nur Beispiele, es gibt viele andere Programme mit ähnlicher Funktionalität.

* * *

### Stellt die Verwendung von ASF die Berechtigung zum Erhalt von Booster Packs sicher?

**Ja**. ASF verwendet die gleiche Methode, um sich im Steam-Netzwerk wie der offizielle Client anzumelden, daher behält es auch die Möglichkeit, Booster Packs für die verwendeten Konten zu erhalten. Es ist jedoch nicht bestätigt, ob das Anmelden bei der Steam-Community tatsächlich erforderlich ist oder nicht, deshalb schlage ich vor, `OnlineStatus` auf `Online` zu belassen, zumindest bis jemand bestätigt, dass die Verwendung des Steam-Netzwerkes allein ausreichend ist. Es sollte so sein, aber ich bin mir nicht sicher.

* * *

### Gibt es eine Möglichkeit mit ASF zu kommunizieren?

Ja, auf verschiedene Arten. Weitere Informationen findest du im Abschnitt **[Befehle](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)**.

* * *

### Ich möchte bei der Übersetzung von ASF helfen, was muss ich tun?

Vielen Dank für dein Intresse! Alle Details hierzu kannst du in unserem Abschnitt **[Lokalisierung](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Localization-de-DE)** finden.

* * *

### Ich habe nur ein (Haupt-)Konto zu ASF hinzugefügt, kann ich trotzdem Befehle per Steam-Chat senden?

**Ja**, es wird im Abschnitt **[Befehle](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands#notes)** erklärt. Du kannst dies über den Steam-Gruppen-Chat tun, obwohl die Verwendung von **[ASF-ui](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-de-DE#asf-ui)** für dich einfacher sein könnte.

* * *

### ASF scheint zu funktionieren, aber ich bekomme keine Karten!

Die Sammelrate der Karten ist von Spiel zu Spiel unterschiedlich, wie du in **[Performance](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-de-DE)** nachlesen kannst. Es dauert eine Weile, normalerweise **mehrere Stunden pro Spiel** und du solltest nicht erwarten, dass Karten in ein paar Minuten nach dem Start eines Programmes gesammelt werden. Wenn du sehen kannst, dass ASF den Kartenstatus aktiv überprüft und das Spiel wechselt, nachdem das aktuelle vollständig gesammelt wurde, dann funktioniert das Ganze einwandfrei. Es ist möglich, dass du eine Option wie `DismissInventoryNotifications` von `BotBehaviour` aktiviert hast, die Inventarbenachrichtigungen automatisch ausblendet. Siehe **[Konfiguration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)** für Details.

* * *

### Wie kann ich den ASF-Prozess für mein Konto vollständig stoppen?

Beende einfach den ASF-Prozess, z.B. durch Anklicken von [X] unter Windows. Wenn du stattdessen einen bestimmten Bot deiner Wahl stoppen, aber andere am Laufen halten möchtest, dann schaue dir die `Enabled` **[Bot-Konfigurationseigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#bot-konfiguration)** an, oder den `stop` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)**. Wenn du stattdessen den automatischen Sammel-Prozess stoppen willst, aber ASF für dein Konto am Laufen halten möchtest, dann ist das das, wofür `Paused` **[Bot-Konfigurationseigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#bot-konfiguration)** und der `pause` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** ist.

* * *

### Wie viele Bots kann ich mit ASF verwenden?

ASF als Programm hat keine unmittelbare Obergrenze für Bot-Instanzen, so dass du so viele Bot-Instanzen ausführen kannst, wie du Speicher auf deinem Computer hast, aber du bist immer noch durch das Steam-Netzwerk und andere Steam-Dienste limitiert. Derzeit kannst du bis zu 100-200 Bots mit einer einzigen IP und einer einzelnen ASF-Instanz laufen lassen. Es ist möglich, mehr Bots mit mehr IPs und mehr ASF-Instanzen auszuführen, indem man die IP-Beschränkungen umgeht. Bedenke, dass du, wenn du diese große Anzahl von Bots verwendest, deren Anzahl du selbst kontrollieren solltest, z.B. sicherstellen, dass sich alle tatsächlich anmelden und zur gleichen Zeit arbeiten. ASF wurde nicht für diese große Anzahl von Bots optimiert und die allgemeine Regel gilt, dass **je mehr Bots du hast, desto mehr Probleme wirst du bekommen**. Außerdem ist zu beachten, dass das obige Limit im Allgemeinen von vielen internen Faktoren abhängt - es ist eher eine Annäherung als ein strenges Limit - Du wirst höchstwahrscheinlich in der Lage sein, mehr/weniger Bots als oben beschrieben auszuführen.

Das ASF-Team empfiehlt, bis zu **10 Bots insgesamt** laufen zu lassen (und zu **besitzen**), alles darüber wird nicht unterstützt und auf eigenes Risiko durchgeführt, gegen unseren Vorschlag, der hier gemacht wurde. Diese Empfehlung basiert auf internen Valve-Richtlinien sowie unseren eigenen Empfehlungen. Ob du diese Regel einhältst oder nicht, ist deine Entscheidung, ASF als Werkzeug wird nicht gegen deinen eigenen Willen handeln, auch wenn es dazu führen sollte, dass deine Steam-Konten dafür gesperrt werden. Daher wird ASF dir eine Warnung anzeigen, wenn du über das hinausgehst, was wir empfehlen, aber trotzdem alles, was du willst, auf eigenes Risiko und ohne unseren Support ausführen kannst.

* * *

### Kann ich dann mehr ASF-Instanzen ausführen?

Du kannst so viele ASF-Instanzen auf einem Computer ausführen, wie du willst, vorausgesetzt, jede Instanz hat ihr eigenes Verzeichnis und ihre eigenen Konfigurationen, und das in einer Instanz verwendete Konto wird in einer anderen nicht verwendet. Allerdings solltest du dich fragen, warum du das tun willst. ASF is optimized to handle a dozen, even a hundred of accounts at the same time, and launching those dozen of bots in their own ASF instances degrades performance, takes more OS resources (such as CPU and memory), and causes a potential synchronization issues between different ASF instances, as ASF is forced to share its limiters with other instances.

Deshalb ist es mein **Ratschlag**, immer maximal eine ASF-Instanz pro IP/Schnittstelle auszuführen. If you have more IPs/interfaces, by all means you can run more ASF instances, with every instance using its own IP/interface or unique `WebProxy` setting. If you don't, launching more ASF instances is totally pointless, as you won't gain anything from launching more than 1 instance per a single IP/interface. Steam wird es dir auf magische Weise nicht erlauben, mehr Bots auszuführen, nur weil du sie in einer anderen ASF-Instanz gestartet hast, und ASF beschränkt dich von Anfang an nicht.

Of course, there are still valid use cases for multiple ASF instances on the same network interface, such as hosting ASF service for your friends with each friend having its own unique ASF instance in order to guarantee isolation between bots and even the ASF processes themselves, however, you're not circumventing any Steam limitations this way, that's entirely different purpose.

* * *

### Was bedeutet Status beim Einlösen eines Produktschlüssels?

Der Status zeigt an, wie sich der gegebene Einlösungsversuch ausgewirkt hat. Es gibt viele verschiedene Stati, darunter die gängigsten:

| Status                  | Beschreibung                                                                                                                                                                                                                                                     |
| ----------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| NoDetail                | Der Status "OK" zeigt Erfolg an - der Produktschlüssel wurde erfolgreich eingelöst.                                                                                                                                                                              |
| Timeout                 | Das Steam-Netzwerk hat nicht in einer bestimmten Zeit reagiert, wir wissen nicht, ob der Produktschlüssel eingelöst wurde oder nicht (höchstwahrscheinlich wurde er das, aber du kannst es noch einmal versuchen).                                               |
| BadActivationCode       | Der angegebene Produktschlüssel ist ungültig (nicht als gültiger Produktschlüssel im Steam-Netzwerk erkannt).                                                                                                                                                    |
| DuplicateActivationCode | Der angegebene Produktschlüssel wurde bereits von einem anderen Konto eingelöst oder vom Entwickler/Verleger widerrufen.                                                                                                                                         |
| AlreadyPurchased        | Dein Konto besitzt bereits `packageID`, das mit diesem Produktschlüssel verbunden ist. Bedenke, dass dies nicht anzeigt, ob der Produktschlüssel `DuplicateActivationCode` ist oder nicht - nur dass er gültig ist und bei diesem Versuch nicht verwendet wurde. |
| RestrictedCountry       | Dies ist ein Produktschlüssel mit Regionssperre und dein Konto befindet sich nicht in der gültigen Region, in der du ihn einlösen darfst.                                                                                                                        |
| DoesNotOwnRequiredApp   | Du kannst diesen Produktschlüssel nicht einlösen, da dir ein anderes Spiel fehlt - hauptsächlich das Basis-Spiel, wenn du versuchst, das DLC-Paket einzulösen.                                                                                                   |
| RateLimited             | Du hast zu viele Einlösungsversuche durchgeführt und dein Konto wurde vorübergehend gesperrt. Versuche es in einer Stunde erneut.                                                                                                                                |

* * *

### Gehörst du zu einem Karten-Sammel-Dienst?

**Nein**. ASF ist mit keinem Dienst verknüpft und alle diese Behauptungen sind falsch. Dein Steam-Konto ist dein Eigentum und du kannst dein Konto auf jede erdenkliche Weise nutzen, aber Valve hat es klar in den **[offiziellen Nutzungsbedingungen](https://store.steampowered.com/subscriber_agreement/english)** erklärt:

> You are responsible for the confidentiality of your login and password and for the security of your computer system. Valve is not responsible for the use of your password and Account or for all of the communication and activity on Steam that results from use of your login name and password by you, by any person to whom you may have intentionally or by negligence disclosed your login and/or password in violation of this confidentiality provision.

ASF ist unter der freien Apache 2.0 Lizenz lizenziert, die es anderen Entwicklern ermöglicht, ASF weiter in ihre eigenen Projekte und Dienste zu integrieren. Es wird jedoch nicht garantiert, dass solche Drittprojekte, die ASF verwenden, sicher, überprüft, angemessen oder legal gemäß **[Steam-Nutzungsbedingungen](https://store.steampowered.com/subscriber_agreement/english)** sind. Wenn du unsere Meinung hören möchtest, **wir empfehlen dir dringend, KEINE deiner Kontoinformationen mit Drittanbietern zu teilen**. Wenn sich ein solcher Dienst als **typischer Betrug** herausstellt, wirst du mit dem Problem allein gelassen, höchstwahrscheinlich ohne dein Steam-Konto und ASF übernimmt keine Verantwortung für Dienstleistungen von Drittanbietern, die behaupten, sicher und geschützt zu sein, weil das ASF-Team weder genehmigt noch eine davon überprüft hat. Mit anderen Worten, **du benutzt sie auf eigene Gefahr, entgegen unseres oben genannten Vorschlags**.

Darüber hinaus wird dies in den offiziellen Steam-Nutzungsbedingungen deutlich gemacht:

> You may not reveal, share or otherwise allow others to use your password or Account except as otherwise specifically authorized by Valve.

Es ist dein Konto und deine Entscheidung. Sag nur nicht, dass dich niemand gewarnt hat. ASF als Programm erfüllt alle oben genannten Bedingungen, da du deine Kontodaten an niemanden weitergibst und du das Programm für deinen eigenen persönlichen Gebrauch verwendest, aber jeder andere "Karten-Sammel-Dienst" verlangt von dir deine Zugangsdaten, so dass es auch gegen die oben genannte Regel verstößt (eigentlich mehrere von ihnen). Wie bei der Auswertung der Steam-Nutzungsbedingungen bieten wir keine rechtliche Beratung an und du solltest selbst entscheiden, ob du diese Dienste nutzen möchtest oder nicht - laut uns **verletzt es direkt die Steam-Nutzungsbedingungen** und könnte zu einer Sperre führen, wenn Valve dies erfährt. Wie bereits erwähnt, **wir empfehlen dringend, KEINE dieser Dienste zu nutzen**.

* * *

## Probleme

* * *

### One of my games is being farmed for more than 10 hours now, but I still didn't get any cards from it!

The reason for that could be related to known issue of Steam, which happens when you have two licenses for the same game, one of which has card drops limited. This usually happens when you activate game for free during a mass giveaway on Steam, and then activate a key for the same game (but without limitations), e.g. from a paid bundle. If such situation happens, Steam reports on badge page that game still has cards to drop, but no matter how much you play the game - cards will never drop due to free license on your account. Since it's not an ASF issue, but a Steam one, we can't somehow circumvent it on ASF's side, and you need to solve it yourself.

There are two ways to solve the issue. Firstly, you can blacklist this game in ASF, either with `ibadd` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** or with `Blacklist` **[configuration property](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**. This will prevent ASF from trying to farm cards from this game, but will not solve the underlying issue which prevents you from obtaining card drops from the affected game. Secondly, you can use Steam support self-service tool to remove free license from your account, leaving only full license that includes the card drops. In order to do so, firstly visit your **[licenses and product key activations](https://store.steampowered.com/account/licenses)** page and locate both free and paid license for the affected game. Usually it's fairly easy - both have similar name, but free one has "limited free promotional package" or other "promo" in the license name, plus "complimentary" in "acquisition method" field. Sometimes it might be more tricky, for example if free package was in some bundle and has a different name. If you have found two licenses like that - then it's indeed the issue described here, and you can safely remove free license without losing the game.

In order to remove the free license from your account, visit **[Steam support page](https://help.steampowered.com/wizard/HelpWithGame)** and put the affected game name into the search field, the game should be available in "products" section, click on it. Alternatively, you can just use `https://help.steampowered.com/wizard/HelpWithGame?appid=<appID>` link and replace `<appID>` with appID of the game that causes troubles. Afterwards, click on "I want to permanently remove this game from my account" and then select the faulty free license that you've found above, usually the one with "limited free promotional package" in the name (or similar). After removal of the free license, ASF should be able to drop cards from the affected game without issues, you should restart the idling operation after the removal just to be sure that Steam picks up the right license this time.

* * *

### ASF erkennt Spiel `X` nicht als zum Sammeln verfügbar, aber ich weiß, dass es Steam-Karten enthält!

Hierfür gibt es zwei Hauptgründe. Der erste und offensichtlichste Grund ist die Tatsache, dass du dich auf den **Steam-Shop** beziehst, wo ein gegebenes Spiel mit Karten angekündigt wird. Diese Annahme ist **falsch**, da es lediglich besagt, dass das Spiel Karten **enthält**, aber nicht unbedingt ob diese Funktion für dieses Spiel auch sofort **aktiviert** ist. Mehr dazu kannst du in den **[offiziellen Ankündigungen](https://steamcommunity.com/games/593110/announcements/detail/1954971077935370845)** lesen.

Kurz gesagt, das Karten-Symbol im Steam-Shop bedeutet nichts, prüfe deine **[Abzeichen-Seiten](https://steamcommunity.com/my/badges)** zur Bestätigung, ob ein Spiel Karten aktiviert hat oder nicht - das ist auch das, was ASF tut. Wenn dein Spiel nicht auf der Liste als ein Spiel mit Karten erscheint, dann ist es **nicht** möglich diese Spiel zu sammeln, unabhängig vom Grund.

Das zweite Problem ist weniger offensichtlich, und es ist die Situation, wenn du sehen kannst, dass dein Spiel tatsächlich mit Karten auf deiner Abzeichen-Seite verfügbar ist, aber es wird nicht sofort von ASF ignoriert. Es sei denn, du triffst einen anderen Fehler, wie z.B. ASF, das nicht in der Lage ist, Abzeichen-Seiten zu überprüfen (siehe unten), es ist einfach ein Cache-Effekt und auf der ASF-Seite berichtet Steam immer noch über veraltete Abzeichen-Seiten. Dieses Problem sollte sich früher oder später lösen, wenn der Cache entwertet wird. Es gibt auch keine Möglichkeit, dies auf unserer Seite zu beheben.

Natürlich geht das alles davon aus, dass du ASF mit standardmäßig unberührten Einstellungen verwendest, da du dieses Spiel auch zur schwarzen Liste des Sammel-Moduls hinzufügen könntest, benutze `IdlePriorityQueueOnly` von `true`, benutze `IdleRefundableGames` von `false` und so weiter.

* * *

### Warum erhöht sich die Spielzeit von Spielen, die über ASF gespielt werden, nicht?

Das tut es, aber **nicht in Echtzeit**. Steam zeichnet deine Spielzeit in festen Intervallen auf und aktualisiert die Zeitpläne dafür, aber es ist nicht garantiert, dass sie sofort nach Beendigung der Sitzung aktualisiert wird, geschweige denn während dieser Zeit. Wenn es möglich wäre, die Spielzeit beim Sammeln zu überspringen, dann kannst du sicher sein, dass wir es schon vor langer Zeit in ASF implementiert hätten und es tatsächlich in den Standardeinstellungen verwenden würden. Aber das tun wir nicht, und das tun wir nicht gerade, weil es nicht möglich ist - nur weil die Spielzeit nicht in Echtzeit aktualisiert wird, bedeutet das nicht, dass sie nicht aufgenommen wird.

* * *

### Worin besteht der Unterschied zwischen einer Warnung und einem Fehler im Protokoll?

ASF schreibt eine Reihe von Informationen über verschiedene Logging-Ebenen in sein Protokoll. Unser Ziel ist es, **präzise** zu erklären, was ASF tut, einschließlich welcher Steam-Probleme es zu bewältigen hat, oder anderer zu überwindender Probleme. Meistens ist nicht alles relevant, deshalb haben wir in ASF zwei große Stufen, die in Bezug auf Probleme verwendet werden - eine Warnstufe und eine Fehlerstufe.

Die allgemeine ASF-Regel ist, dass Warnungen **keine** Fehler sind, daher sollten sie **nicht** gemeldet werden. Eine Warnung ist ein Indikator für dich, dass etwas möglicherweise Unerwünschtes passiert. Egal ob es Steam war das nicht reagierte, die API Fehler geworfen hat oder deine Netzwerkverbindung Probleme hatte - es ist eine Warnung und das bedeutet, dass wir erwarten das dies passiert, also belästige die ASF-Entwicklung nicht damit. Natürlich steht es dir frei, nach ihnen zu fragen oder Hilfe zu erhalten, indem du unsere technische Unterstützung in Anspruch nimmst, aber du solltest nicht davon ausgehen, dass es sich um meldenswerte ASF-Fehler handelt (sofern wir nichts anderes bestätigen).

Fehler hingegen deuten auf eine Situation hin, die nicht eintreten sollte, daher sind sie es wert, gemeldet zu werden, solange man sich vergewissert hat, dass man es nicht selbst ist, der sie verursacht. Wenn es sich um eine allgemeine Situation handelt, die wir erwarten, dann wird sie stattdessen in eine Warnung umgewandelt. Andernfalls handelt es sich möglicherweise um einen Fehler, der korrigiert werden sollte, nicht stillschweigend ignoriert werden sollte, vorausgesetzt, er ist nicht das Ergebnis deines eigenen technischen Problems. Wenn du zum Beispiel ungültigen Inhalt in die Datei `ASF.json` einträgst, wird ein Fehler auftreten, da ASF ihn nicht analysieren kann, aber du warst es, der ihn dort platziert hat, also solltest du diesen Fehler nicht an uns melden (es sei denn, du hast bestätigt, dass ASF falsch ist und deine Struktur tatsächlich absolut korrekt ist).

In einem Satz: Fehler melden und Warnungen nicht melden. Du kannst nach wie vor zu Warnungen Fragen stellen und in unseren Unterstützungsbereichen Hilfe erhalten.

* * *

### ASF startet nicht, das Programmfenster schließt sich sofort!

Unter normalen Bedingungen erzeugt jeder ASF-Crash oder -Ausgang eine `log.txt` im Verzeichnis des Programms, die Sie sich ansehen können, um die Ursache dafür zu finden. In addition to that, a few last log files are also archived in `logs` directory, since the main `log.txt` file is overwritten with each ASF run.

Wenn jedoch selbst die.NET Core Runtime nicht in der Lage ist, auf Ihrer Maschine zu booten, dann wird `log.txt` nicht erstellt. Wenn Ihnen das passiert, haben Sie höchstwahrscheinlich vergessen, die Voraussetzungen für die Installation von.NET Core zu erfüllen, wie in der Anleitung **[Einrichtung](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Setting-up#os-specific-setup)** beschrieben. Andere häufige Probleme können der Versuch sein, eine falsche ASF-Variante für dein Betriebssystem zu starten, oder auf andere Weise fehlende native .NET Core Laufzeitabhängigkeiten. Wenn sich das Konsolenfenster zu früh schließt, als dass du die Nachricht lesen könntest, dann öffne die unabhängige Konsole und starte von dort aus die ASF-Binärdatei. Öffnen Sie beispielsweise unter Windows das ASF-Verzeichnis, halten Sie `Shift` gedrückt, klicken Sie mit der rechten Maustaste in den Ordner und wählen Sie "Befehlsfenster hier öffnen" (oder powershell), geben Sie dann in die Konsole `.\ArchiSteamFarm.exe` ein und bestätigen Sie mit Enter. Auf diese Weise erhältst du eine genaue Meldung, warum ASF nicht richtig startet.

* * *

### ASF wirft mich aus meiner Steam-Client-Sitzung während ich spiele! / `This account is logged on another PC`

Dies wird als Meldung im Steam-Overlay angezeigt, dass das Konto während des Spiels woanders verwendet wird. Dieses Problem kann zwei verschiedene Gründe haben.

Ein Grund dafür sind defekte Pakete (Spiele), die speziell eine Spielsperre nicht richtig halten, aber erwarten, dass diese vom Client besessen wird. Ein Beispiel für ein solches Paket wäre Skyrim SE. Dein Steam-Client startet das Spiel richtig, aber dieses Spiel registriert sich selbst nicht als in Benutzung. Aus diesem Grund sieht ASF, dass es frei ist den Prozess fortzusetzen, was es tut, und das wirft dich aus dem Steam-Netzwerk, da Steam plötzlich erkennt, dass das Konto an einem anderen Ort verwendet wird.

Ein zweiter Grund kann auftreten, wenn du auf deinem PC spielst, während ASF wartet (besonders auf einem anderen Computer) und du deine Netzwerkverbindung verlierst. In diesem Fall markiert dich das Steam-Netzwerk als offline und gibt die Spielsperre (wie oben) frei, was ASF (z.B. auf einer anderen Maschine) dazu veranlasst, das Sammeln wieder aufzunehmen. Wenn dein PC wieder online ist, kann Steam keine Spielsperre mehr erfassen (die jetzt von ASF gehalten wird, ebenfalls ähnlich wie oben) und zeigt die gleiche Meldung an.

Die beiden Ursachen auf der ASF-Seite sind eigentlich sehr schwer zu beheben, da ASF das Sammeln einfach wieder aufnimmt, sobald das Steam-Netzwerk es darüber informiert, dass das Konto wieder frei ist verwendet werden kann. Das ist es, was normalerweise passiert, wenn du das Spiel schließt, aber bei defekten Paketen kann dies sofort passieren, auch wenn dein Spiel noch läuft. ASF hat keine Möglichkeit zu wissen, ob du die Verbindung getrennt hast, aufgehört hast ein Spiel zu spielen oder ob du immer noch ein Spiel spielst, das die Sperre nicht ordnungsgemäß hält.

Die einzig richtige Lösung für dieses Problem ist, deinen Bot mit `pause` manuell zu pausieren und ihn mit `resume` wieder zu starten, sobald du fertig bist. Alternativ kannst du das Problem einfach ignorieren und dich genauso verhalten, als ob du mit dem Offline-Steam-Client spielen würdest.

* * *

### `Von Steam getrennt!` - Ich kann keine Verbindung zu den Steam-Servern herstellen.

ASF kann nur **versuchen** eine Verbindung mit den Steam-Servern herzustellen, und es kann aus vielen Gründen fehlschlagen, einschließlich fehlender Internetverbindung, Steam ist ausgefallen, deine Firewall blockiert die Verbindung, Programme von Drittanbietern, falsch konfigurierte Routen oder temporäre Ausfälle. Du kannst den `Debug`-Modus aktivieren, um ein ausführlicheres Protokoll mit genauen Fehlerursachen zu erhalten, obwohl es normalerweise einfach durch deine eigenen Aktionen verursacht wird, wie z.B. die Verwendung von "CS:GO MM Server Picker", das viele Steam-IPs auf eine schwarze Liste setzt, was es für dich sehr schwierig macht, tatsächlich das Steam-Netzwerk zu erreichen.

ASF wird sein Bestes tun, um eine Verbindung herzustellen, was nicht nur die Abfrage nach der aktualisierten Liste der Server beinhaltet, sondern auch den Versuch einer anderen IP, wenn die letzte fehlschlägt. Wenn es also wirklich ein temporäres Problem mit einem bestimmten Server oder einer bestimmten Route ist, wird ASF früher oder später eine Verbindung herstellen. Wenn du dich jedoch hinter der Firewall befindest oder auf andere Weise nicht in der Lage bist, die Steam-Server zu erreichen, dann musst du es natürlich selbst reparieren, mit Hilfe des `Debug`-Modus.

Es ist auch möglich, dass deine Maschine nicht in der Lage ist, eine Verbindung mit den Steam-Servern über das Standardprotokoll in ASF herzustellen. Du kannst Protokolle, die ASF verwenden darf, ändern, indem du `SteamProtocols` globale Konfigurationseigenschaft änderst. Wenn du zum Beispiel Probleme hast, Steam mit dem `UDP` Protokoll zu erreichen (z.B. Aufgrund Firewalls), dann kannst du `TCP` oder `WebSocket` versuchen.

In einer sehr unwahrscheinlichen Situation, in der falsche Server zwischengespeichert werden, z.B. weil ASF `config` Ordner von einer Maschine auf eine andere Maschine in einem völlig anderen Land verschoben wurde, könnte das Löschen von `ASF.db` helfen, um die Steam-Server beim nächsten Start zu aktualisieren. Sehr oft ist es nicht notwendig und muss auch nicht getan werden, da diese Liste beim ersten Start und beim Verbindungsaufbau automatisch aktualisiert wird - wir erwähnen sie nur als eine Möglichkeit, um alles zu bereinigen, was mit der Liste der Steam-Server zusammenhängt, die von ASF im Zwischenspeicher gehalten werden.

* * *

### `Konnte Abzeicheninformation nicht abfragen, wir versuchen es später erneut!`

Normalerweise bedeutet das, dass du die Steam Parental PIN für den Zugriff auf dein Konto verwendest, aber du hast vergessen, sie in die ASF-Konfiguration einzugeben. Du musst eine gültige PIN in `SteamParentalCode` Bot-Konfigurationseigenschaft eingeben, sonst kann ASF nicht auf die meisten Webinhalte zugreifen und kann daher nicht richtig funktionieren. Schaue unter **[Konfiguration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)** vorbei, um mehr über `SteamParentalCode` zu erfahren.

Andere Gründe können temporäre Steam-Probleme, Netzwerkprobleme oder ähnliches sein. Wenn sich das Problem nach mehreren Stunden nicht beheben lässt und du sicher bist, dass du ASF richtig konfiguriert hast, kannst du uns gerne darüber informieren.

* * *

### ASF schlägt fehl mit `Request failed after 5 tries` Fehler!

Normalerweise bedeutet das, dass du die Steam Parental PIN für den Zugriff auf dein Konto verwendest, aber du hast vergessen, sie in die ASF-Konfiguration einzugeben. Du musst eine gültige PIN in `SteamParentalCode` Bot-Konfigurationseigenschaft eingeben, sonst kann ASF nicht auf die meisten Webinhalte zugreifen und kann daher nicht richtig funktionieren. Schaue unter **[Konfiguration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE)** vorbei, um mehr über `SteamParentalCode` zu erfahren.

Wenn die Eltern-PIN nicht der Grund dafür ist, dann ist dies ein häufiger Fehler, und du solltest dich daran gewöhnen, bedeutet das einfach, dass ASF eine Anfrage an das Steam-Netzwerk gesendet hat und keine gültige Antwort erhalten hat, 5 mal in Folge. Normalerweise bedeutet es, dass Steam entweder außer Betrieb ist oder einige Schwierigkeiten oder Wartungsarbeiten hat - ASF ist sich solcher Probleme bewusst und du solltest dir keine Sorgen um sie machen, es sei denn, sie passieren ständig für mehr als mehrere Stunden, und andere Benutzer haben keine derartigen Probleme.

Wie kann man überprüfen, ob Steam außer Betrieb ist? **[Steam Status](https://steamstat.us)** ist eine ausgezeichnete Quelle, um zu überprüfen, **ob** Steam in Betrieb sein sollte, wenn du Fehler bemerkst, insbesondere im Zusammenhang mit der Community oder der Web-API, dann hat Steam Schwierigkeiten. Entweder ASF in Ruhe lassen und es nach einer kurzen Zeit wieder versuchen, oder selbst warten.

Das ist jedoch nicht immer der Fall, da in einigen Situationen Steam-Probleme möglicherweise nicht durch Steam Status erkannt werden, z.B. passierte ein solcher Fall, als Valve die HTTPS-Unterstützung für Steam Community am 7. Juni 2016 unterbrach - der Zugriff auf **[SteamCommunity](https://steamcommunity.com)** durch HTTPS warf einen Fehler. Verlasse dich daher auch nicht blind auf den Steam Status, am besten überprüfe selbst, ob alles so funktioniert, wie es sollte.

Darüber hinaus beinhaltet Steam verschiedene Maßnahmen zur Ratenbegrenzung, die deine IP vorübergehend sperren, wenn du übermäßig viele Anfragen auf einmal stellst. ASF ist sich dessen bewusst und bietet dir in der Konfiguration mehrere verschiedene Begrenzer an, die du nutzen solltest. Die Standardeinstellungen wurden auf Basis von **einer gesunden** Anzahl der Bots angepasst. Wenn du so viele verwendest, dass sogar Steam dir sagt, du sollst weggehen, dann optimierst du sie entweder, bis sie es dir nicht mehr sagen, oder du tust, was man dir sagt. Ich nehme an, dass der zweite Weg keine Option für dich ist, also lies weiter zu diesem Thema und achte besonders auf `WebLimiterDelay`, der ein allgemeiner Begrenzer ist, der für alle Web-Anfragen gilt.

Es gibt keine "goldene Regel", die für jeden funktioniert, denn Sperren werden stark von Faktoren Dritter beeinflusst, deshalb muss man selbst experimentieren und einen Wert finden, der für einen funktioniert. Du kannst das auch ignorieren und einen Wert wie `10000` verwenden, was garantiert korrekt funktioniert, aber dann beschwere dich nicht, dass dein ASF auf alles innerhalb von 10 Sekunden reagiert und wie das Parsen von Abzeichen 5 Minuten dauert. Darüber hinaus ist es durchaus möglich, dass kein Begrenzer etwas bewirkt, weil du so viele Bots hast, dass du die **[Obergrenze](#wie-viele-bots-kann-ich-mit-asf-verwenden)** ereicht hast, wie oben erwähnt. Ja, es ist durchaus möglich, dass du dich ohne Probleme im Steam-Netzwerk anmelden kannst, aber Steam Web wird sich weigern, dir zuzuhören, wenn du 100 Sitzungen gleichzeitig eingerichtet hast. ASF verlangt, dass sowohl das Steam-Netzwerk als auch das Steam-Web kooperativ sind. Wenn nur eines von beiden nicht funktioniert kann es zu Problemen kommen von denen du dich nicht erholen wirst.

Wenn nichts hilft und du keine Ahnung hast was kaputt ist, kannst du immer den `Debug`-Modus aktivieren und dir im ASF-Log selbst ansehen warum genau die Anfragen fehlschlagen. Zum Beispiel:

    InternalRequest() HEAD https://steamcommunity.com/my/edit/settings
    InternalRequest() Forbidden <- HEAD https://steamcommunity.com/my/edit/settings
    

Siehst du das `Forbidden`? Das bedeutet, dass du vorübergehend für übermäßig viele Anfragen gesperrt wurdest, weil du `WebLimiterDelay` noch nicht richtig angepasst hast (vorausgesetzt, du bekommst den gleichen Fehlercode auch für alle anderen Anfragen). Es kann noch weitere Gründe geben, wie z.B. `InternalServerError`, `ServiceUnavailable` und Timeouts, die auf Steam Wartungsarbeiten und Probleme hinweisen. Du kannst immer versuchen, den von ASF erwähnten Link selbst zu besuchen und zu überprüfen, ob er funktioniert - wenn nicht, dann weißt du, warum ASF auch nicht darauf zugreifen kann. Wenn dies der Fall ist und der gleiche Fehler nach ein oder zwei Tagen nicht verschwindet, könnte es sich lohnen, ihn zu untersuchen und zu melden.

Bevor du das tust, **solltest du dich vergewissern, dass der Fehler überhaupt einen Bericht wert ist**. Wenn es in diesem FAQ erwähnt wird, wie z.B. handelsbedingte Probleme, dann ist das nicht der Fall. Wenn es sich um ein temporäres Problem handelt, das ein- oder zweimal auftrat, insbesondere wenn dein Netzwerk instabil war oder Steam ausgefallen ist - dann ist das nicht der Fall. Wenn du jedoch in der Lage warst, dein Problem mehrmals hintereinander, über 2 Tage, zu reproduzieren, ASF sowie deine Maschine im Prozess neu zu starten und sicherzustellen, dass es hier keinen FAQ-Eintrag gibt, der dir hilft, es zu lösen, dann könnte es sich lohnen, nach ihm zu fragen.

* * *

### ASF scheint einzufrieren und gibt nichts auf der Konsole aus, bis ich eine Taste drücke!

Du benutzt höchstwahrscheinlich Windows und deine Konsole hat den QuickEdit-Modus aktiviert. Siehe **[diese](https://stackoverflow.com/questions/30418886/how-and-why-does-quickedit-mode-in-command-prompt-freeze-applications)** Frage zu StackOverflow für eine technische Erklärung. Du solltest den QuickEdit-Modus deaktivieren, indem du mit der rechten Maustaste auf das ASF-Konsolenfenster klickst, die Eigenschaften öffnest und das entsprechende Kontrollkästchen deaktivierst.

* * *

### ASF kann keine Handelsangebote akzeptieren oder versenden!

Offensichtliche Sache zuerst - neue Konten beginnen als begrenzt. Bis du das Konto freischaltest, indem du sein Guthaben lädst oder 5€ im Shop ausgibst, kann ASF weder Handelsangebote akzeptieren noch über dieses Konto versenden. In diesem Fall gibt ASF an, dass das Inventar leer ist, da jede Karte, die sich darin befindet, nicht handelbar ist. Es wird auch nicht möglich sein, ein Handelsangebot zu erhalten, da dieser Teil erfordert, dass ASF in der Lage ist, den API-Schlüssel zu holen, und die API-Schlüsselfunktionalität für begrenzte Konten deaktiviert ist. Kurz gesagt - der Handel ist für alle begrenzten Konten deaktiviert, keine Ausnahmen.

Als nächstes, wenn du **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE)** nicht verwendest, ist es möglich, dass ASF tatsächlich das Handelsangebot akzeptiert/sendet, aber du musst es per E-Mail bestätigen. Ebenso musst du, wenn du die klassische 2FA verwendest, das Handelsangebot über deinen Authentifikator bestätigen. Bestätigungen sind jetzt **obligatorisch**, also wenn du sie nicht selbst akzeptieren willst, erwäge deinen Authentifikator in ASF 2FA zu importieren.

Beachte auch, dass du nur mit deinen Freunden und Personen mit bekanntem Handelslink handeln kannst. Wenn du versuchst, Bot->Master-Handelsangebot zu veranlassen, wie z.B. `loot`, dann musst du entweder deinen Bot auf deiner Freundesliste haben oder deinen `SteamTradeToken` in der Bot-Konfiguration angegeben haben. Stelle sicher, dass der Code gültig ist - sonst kannst du keine Handelsangebote versenden.

Abschließend solltest du daran denken, dass neue Geräte eine 7-Tage-Handels-Sperre haben. Wenn du also gerade dein Konto zu ASF hinzugefügt hast, warte mindestens 7 Tage - alles sollte nach diesem Zeitraum funktionieren. Diese Einschränkung beinhaltet **sowohl** die Annahme von Handelsangeboten als **auch** das Versenden von Handelsangeboten. Es wird nicht immer ausgelöst, und es gibt Leute, die sofort Handelsangebote senden und annehmen können. Die Mehrheit der Personen sind jedoch betroffen, und die Verriegelung **wird** passieren, auch wenn du Handelsangebote über deinen Steam-Client auf der gleichen Maschine senden und annehmen kannst. Warte einfach geduldig, es gibt nichts, was du tun kannst, um es zu beschleunigen. Ebenso kann es sein, dass du eine ähnliche Sperre für das Entfernen/Ändern verschiedener sicherheitsrelevanter Einstellungen von Steam erhältst, wie z.B. 2FA, SteamGuard, Passwort, E-Mail und ähnliches. Im Allgemeinen solltest du überprüfen, ob du ein Handelsangebot von diesem Konto aus selbst versenden kannst. Wenn ja, dann ist es sehr wahrscheinlich, dass es sich um eine klassische 7-Tage-Sperre von einem neuen Gerät handelt.

Und schließlich, bedenke, dass ein Konto nur 5 ausstehende Handelsangebote zu einem anderen haben kann, so dass ASF keine Handelsangebote senden kann, wenn du bereits 5 (oder mehr) ausstehende Handelsangebote von diesem einen Bot hast. Dies ist selten ein Problem, aber es ist auch erwähnenswert, besonders wenn du ASF auf automatisches Senden von Handelsangeboten einstellst, aber du benutzt ASF 2FA nicht und hast vergessen, sie tatsächlich zu bestätigen.

Wenn nichts geholfen hat, kannst du jederzeit den `Debug` Modus aktivieren und selbst überprüfen, warum Anfragen fehlschlagen. Bitte bedenke, dass Steam die meiste Zeit Unsinn redet, und vorausgesetzt, dass der Grund keinen logischen Sinn ergibt oder sogar völlig falsch ist - wenn du dich entscheidest, diesen Grund zu interpretieren, stelle sicher, dass du ein angemessenes Wissen über Steam und seine Besonderheiten hast. Es ist auch durchaus üblich, dieses Problem ohne logischen Grund zu sehen, und die einzige vorgeschlagene Lösung in diesem Fall ist, das Konto erneut zu ASF hinzuzufügen (und wieder 7 Tage zu warten). Manchmal behebt sich dieses Problem auf *magische* Weise, genauso wie es auch kaputt geht. Allerdings ist es in der Regel nur entweder 7-Tage Handels-Sperre, temporäres Steam-Problem oder beides. Es ist am besten, ihm ein paar Tage vor der manuellen Überprüfung zu geben, was falsch ist, es sei denn, du hast den Drang, die wahre Ursache zu finden (und normalerweise wirst du gezwungen sein, trotzdem zu warten, weil Fehlermeldungen keinen Sinn ergeben und dir auch nicht im Geringsten helfen).

In jedem Fall kann ASF nur **versuchen**, eine ordnungsgemäße Anfrage an Steam zu senden, um das Handelsangebot anzunehmen/senden. Ob Steam diese Anfrage annimmt oder nicht, liegt außerhalb des Anwendungsbereichs von ASF, und ASF wird es nicht auf magische Weise zum Funktionieren bringen. Es gibt keinen Fehler im Zusammenhang mit dieser Funktion, und es gibt auch nichts zu verbessern, da die Logik außerhalb von ASF stattfindet. Deshalb, bitte nicht um die Behebung von Dingen, die nicht kaputt sind, und frage auch nicht, warum ASF keine Handelsangebote annehmen oder senden kann - **Ich weiß es nicht, und ASF weiß es auch nicht**. Entweder findest du dich damit ab oder du reparierst es selbst.

* * *

### Warum muss ich bei der Anmeldung jedesmal den 2FA/SteamGuard-Code eingeben? / `Abgelaufenen Anmelde-Schlüssel entfernt`

ASF verwendet Anmelde-Schlüssel (wenn du `UseLoginKeys` aktiviert hast), um die Anmeldeinformationen gültig zu halten, den gleichen Mechanismus, den Steam verwendet - 2FA/SteamGuard-Code ist nur einmal erforderlich. Aufgrund von Steam-Netzwerkproblemen und -schwierigkeiten ist es jedoch durchaus möglich, dass der Anmelde-Schlüssel nicht im Netzwerk gespeichert ist. Ich habe solche Probleme bereits nicht nur mit ASF, sondern auch mit dem normalen Steam-Client gesehen (eine Notwendigkeit, bei jedem Durchlauf Login + Passwort einzugeben, unabhängig von der Option "Remember me").

Du könntest `BotName.db` (+ `BotName.bin`, falls vorhanden) des betroffenen Kontos entfernen und versuchen, ASF wieder mit deinem Konto zu verbinden, aber das muss nicht zum Erfolg führen. Die eigentliche ASF-basierte Lösung besteht darin, deinen Authentifikator als **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE)** zu importieren - so kann ASF Codes automatisch generieren, wenn sie benötigt werden, und du musst sie nicht manuell eingeben. In der Regel löst sich das Problem nach einiger Zeit von selbst, so dass man es einfach abwarten kann. Of course you can also ask Valve for solution, because I can't force Steam network to accept our login keys.

Als Randbemerkung kannst du auch die Anmelde-Schlüssel mit `UseLoginKeys` Konfigurationseigenschaft auf `false` deaktivieren, aber das wird das Problem nicht lösen, sondern nur den Fehler des ersten Anmelde-Schlüssels überspringen. ASF ist sich des hier beschriebenen Problems bereits bewusst und wird versuchen, keine Anmelde-Schlüssel zu verwenden, wenn es sich selbst alle Anmeldeinformationen zuschicken kann, so dass es nicht notwendig ist, `UseLoginKeys` manuell zu optimieren, wenn du alle Zugangsdaten zusammen mit ASF 2FA angeben kannst.

* * *

### Ich erhalte den Fehler: `Die Anmeldung bei Steam ist nicht möglich: InvalidPassword or RateLimitExceeded`

Dieser Fehler kann vieles bedeuten, unter anderem auch:

- Ungültige Login/Passwort-Kombination (offensichtlich)
- Abgelaufener Anmelde-Schlüssel, der von ASF für die Anmeldung verwendet wird
- Zu viele fehlgeschlagene Anmeldeversuche in kurzer Zeit (Anti-Bruteforce)
- Zu viele Anmeldeversuche in kurzer Zeit (Rate-Limiting)
- Erfordernis eines Captcha zum Anmelden (sehr wahrscheinlich durch die beiden obigen Gründe verursacht)
- Alle anderen Gründe, die das Steam-Netzwerk daran hindern könnten, sich anzumelden.

Im Falle von Anti-Bruteforce und Rate-Limiting wird das Problem nach einiger Zeit verschwinden, also warte einfach und versuche dich in der Zwischenzeit nicht anzumelden. Wenn du dieses Problem häufiger hast, ist es vielleicht ratsam, `LoginLimiterDelay` Konfigurationseigenschaft von ASF zu erhöhen. Übermäßige Programmneustarts und andere absichtliche/nicht absichtliche Anmeldeanforderungen werden bei diesem Problem definitiv nicht helfen, also vermeide es, wenn möglich.

Im Falle eines abgelaufenen Anmelde-Schlüssels wird ASF den alten entfernen und beim nächsten Anmelden nach einem neuen fragen (was erfordert, dass du einen 2FA-Code setzt, wenn dein Konto 2FA-geschützt ist). Wenn dein Konto ASF 2FA verwendet, wird ein Code generiert und automatisch verwendet. This can naturally happen over time, but if you get this issue on each login, it's possible that Steam for some reason decided to ignore our login key save requests, as mentioned in the issue **[above](#why-do-i-have-to-put-2fasteamguard-code-on-each-login--removed-expired-login-key)**. You can of course disable `UseLoginKeys` entirely, but that won't solve the issue, only avoid a need of removing expired login keys each time. The real solution, as per the issue above, is to use ASF 2FA.

Und schlussendlich, wenn du eine falsche Kombination aus Login und Passwort verwendet hast, musst du dies natürlich korrigieren oder den Bot deaktivieren, der versucht, sich mit diesen Zugangsdaten zu verbinden. ASF kann nicht von selbst erraten, ob `InvalidPassword` ungültige Anmeldeinformationen bedeutet, oder einen der oben genannten Gründe, daher wird es weiter versuchen, bis es erfolgreich ist.

Bedenke, dass ASF über ein eigenes eingebautes System verfügt, um entsprechend auf Steam-Schrullen zu reagieren, irgendwann wird es sich verbinden und seine Arbeit wieder aufnehmen, daher ist es nicht erforderlich, etwas zu tun, wenn das Problem vorübergehend ist. Ein Neustart von ASF, um Probleme magisch zu beheben, wird die Situation nur verschlimmern (da der neue ASF den vorherigen ASF-Zustand nicht kennt, dass er sich nicht anmelden kann, und versucht, eine Verbindung herzustellen, anstatt zu warten), also solltest du das nicht tun, es sei denn, du weißt, was du tust.

Abschließend kann sich ASF, wie bei jeder Steam-Anfrage, nur **versuchen** mit den von dir angegebenen Zugangsdaten anmelden. Ob diese Anforderung erfolgreich ist oder nicht, liegt außerhalb des Umfangs und der Logik von ASF - es gibt keinen Fehler, und in dieser Hinsicht kann nichts behoben oder verbessert werden.

* * *

### `System.IO.IOException: Input/output error`

Wenn dieser Fehler während der ASF-Eingabe aufgetreten ist (z.B. wenn du `Console.ReadLine()` im Stacktrace sehen kannst), dann wird er durch deine Umgebung verursacht, die es ASF verbietet, die Standardeingabe deiner Konsole zu lesen. Das kann aus vielen Gründen passieren, aber der häufigste ist, dass du ASF in der falschen Umgebung verwendest (z.B. in `nohup` oder `&` Hintergrund anstelle von `screen` unter Linux). Wenn ASF nicht auf die Standardeingabe zugreifen kann, wird dieser Fehler protokolliert und ASF kann deine Daten während der Ausführung nicht verwenden.

Wenn du **erwartest**, dass dies geschieht, also du **willst** ASF in einer eingabefreien Umgebung laufen lassen, dann solltest du ASF ausdrücklich sagen, dass es der Fall ist, indem du den **[`Headless`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#headless)** Modus entsprechend einstellst.

* * *

### `System.Net.Http.WinHttpException: A security error occurred`

Dieser Fehler tritt auf, wenn ASF keine sichere Verbindung mit dem angegebenen Server herstellen kann, fast ausschließlich wegen des Misstrauens gegen ein SSL-Zertifikat.

In fast allen Fällen wird dieser Fehler durch ein **falsches Datum/Uhrzeit auf deiner Maschine** verursacht. Jedes SSL-Zertifikat hat ein Ausstellungsdatum und ein Verfallsdatum. Wenn dein Datum ungültig ist und außerhalb dieser beiden Grenzen liegt, kann dem Zertifikat nicht vertraut werden, da es als potentieller MITM-Angriff ausgenutzt werden kann und ASF weigert sich, eine Verbindung herzustellen.

Eine offensichtliche Lösung ist das Datum auf deiner Maschine entsprechend einzustellen. Es wird dringend empfohlen, eine automatische Datumssynchronisation zu verwenden, wie z.B. die unter Windows verfügbare native Synchronisation oder `ntpd` unter Linux.

Wenn du sichergestellt hast, dass das Datum auf deiner Maschine korrekt ist und der Fehler nicht verschwinden will, dann, angenommen, es ist kein temporäres Problem das bald verschwindet, könnten SSL-Zertifikate, denen dein System vertraut, veraltet oder ungültig sein. In diesem Fall solltest du sicherstellen, dass deine Maschine sichere Verbindungen herstellen kann, z.B. indem du über einen beliebigen Browser oder mit einem CLI-Tool wie `curl` überprüfst, ob du auf `https://github.com` zugreifen kannst. Wenn du bestätigt hast, dass dies ordnungsgemäß funktioniert, kannst du das Problem gerne in unserer Steam-Gruppe melden.

* * *

### `System.Threading.Tasks.TaskCanceledException: A task was canceled.`

Diese Warnung bedeutet, dass Steam nicht innerhalb einer bestimmten Zeit auf die ASF-Anfrage geantwortet hat. Normalerweise wird dies durch Steam-Netzwerkproblemen verursacht und hat keine Auswirkung auf ASF. In manchen Fällen ist es das gleiche wie wenn die Anfrage nach 5 Versuchen fehlschlägt. Die Meldung dieses Problems macht meistens keinen Sinn, da wir Steam nicht zwingen können, auf unsere Anfragen zu reagieren.

* * *

### `The type initializer for 'System.Security.Cryptography.CngKeyLite' threw an exception`

This problem is almost exclusively caused by disabled/stopped `CNG Key Isolation` windows service, which provides core cryptography functionality for ASF, without which the program isn't able to run. You can fix this issue by launching `services.msc` and ensuring that `CNG Key Isolation` windows service doesn't have disabled startup and is currently running.

* * *

### ASF wird von meinem AntiVirus als Schadsoftware erkannt! Was ist hier los?

**Stelle sicher, dass du ASF von einer vertrauenswürdigen Quelle heruntergeladen hast**. Die einzige offizielle und vertrauenswürdige Quelle ist **[ASF-Veröffentlichungen](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** Seite auf GitHub (und das ist auch die Quelle für automatische ASF-Aktualisierungen) - **jede andere Quelle ist per Definition nicht vertrauenswürdig und könnte Schadsoftware enthalten, die von anderen Personen hinzugefügt wurde** - Du solltest keiner anderen Quelle per Definition vertrauen und sicherstellen, dass dein ASF immer von uns kommt.

Wenn du überprüft hast, dass ASF von einer vertrauenswürdigen Quelle heruntergeladen wurde, dann ist es höchstwahrscheinlich nur ein Fehlalarm. Dies **passierte in der Vergangenheit**, **passiert gerade jetzt**, und **wird in der Zukunft passieren**. Wenn du dir um die tatsächliche Sicherheit bei der Verwendung von ASF Sorgen machst, dann schlage ich vor, ASF mit vielen verschiedenen AVs nach der tatsächlichen Erkennungsrate zu scannen, zum Beispiel durch **[VirusTotal](https://www.virustotal.com)** (oder einem anderen Webdienst deiner Wahl).

Wenn die von dir verwendete AV-Software ASF fälschlicherweise als Schadsoftware erkennt, dann **ist es eine gute Idee, dieses Dateibeispiel an die Entwickler deiner AV-Software zu senden, damit sie es analysieren und ihre Erkennungsmaschine verbessern können**, da sie offensichtlich nicht so gut funktioniert, wie du denkst. Es gibt kein Sicherheitsproblem im ASF-Code, und es gibt auch nichts zu beheben, da wir keine Schadsoftware verteilen, daher macht es auch keinen Sinn uns diese Fehlalarme zu melden. Wir empfehlen dringend, ASF-Beispiele für weitere Analysen wie oben beschrieben zu versenden, aber wenn du dich nicht darum kümmern möchtest, dann kannst du ASF immer zu den AV-Ausnahmen hinzufügen, deine AV-Software deaktivieren oder einfach ein andere verwenden. Leider sind wir es gewohnt, dass AV-Softwares dumm sind, da ab und zu einige AV-Softwares ASF als Virus erkennen, was normalerweise sehr kurz andauert und schnell von den Entwicklern ausgebessert wird, aber wie wir oben erwähnt haben - **es passierte**, **passiert** und **wird immer passieren**. ASF enthält keinen schädlichen Code, du kannst ASF-Code überprüfen und sogar selbst den Quellcode kompilieren. Wir sind keine Hacker die den ASF-Code verschleiern, um sich vor AV-Heuristiken und Fehlalarmen zu verstecken, also erwarte nicht von uns, dass wir reparieren, was nicht kaputt ist - es gibt keinen "Virus", den wir beseitigen können.
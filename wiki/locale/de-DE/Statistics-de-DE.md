# Statistiken

Die ASF-Entwicklung wird durch 3 wichtige Dinge unterstützt: Spenden, Benutzer-Rückmeldungen und Statistiken. Spenden beeinflussen direkt unsere Bereitschaft an dem Projekt zu arbeiten, die Rückmeldungen der Benutzer sind immer schön zu lesen (besonders positive), und Statistiken liefern uns die Informationen, wie und von wie vielen Personen unsere Software verwendet wird - auf diese Weise können wir erfahren, was wir verbessern, was wir beheben und worauf wir uns konzentrieren können.

ASF hat in den Standardeinstellungen `Statistics` globale Konfigurationseigenschaft aktiviert. Wenn du neue Versionen, Fehlerbehebungen und die Implementierung neuer Funktionen sehen möchtest, solltest du in Betracht ziehen, diese Einstellung beizubehalten, damit wir diese Daten nutzen können, um dir eine bessere Software (aber nicht nur) zu bieten. Dies ist besonders wichtig da ASF dir **kostenlos** zur Verfügung gestellt wird, und das ist das Mindeste, was du tun kannst um Danke zu sagen - **teil uns mit, dass du ASF benutzt**, da dies das ist, was unsere aktuellen Statistiken hauptsächlich tun. ASF sammelt oder verwendet keine Daten, die als privat angesehen und/oder gegen dich verwendet werden könnten. Wir reduzieren die Verwendung von Statistiken auf ein Minimum und jede einzelne gesammelte Information erfordert von uns, dass wir sie hier genau angeben, zusammen mit einer praxisnahen Erklärung. So erhältst du einen vollständigen Überblick darüber, was wir sammeln, zu welchem Zweck und wie es helfen soll. Alle diese Informationen findest du weiter unten.

* * *

## Aktuelle Datenschutzerklärung

Wenn `Statistics` aktiv ist passiert Folgendes:

1. Das in ASF verwendete Konto wird unserer **[Steam-Gruppe](https://steamcommunity.com/gid/103582791440160998)** beitreten. Dies geschieht aus drei Gründen:

* Es bietet **dir** Gruppenankündigungen, insbesondere bei neuen Versionen, kritischen Problemen, Steam-Problemen und andere Dingen, die wichtig sind, um die Community auf dem Laufenden zu halten (kein Spam, der nichts mit ASF zu tun hat)
* Es ermöglicht **dir**, unsere technische Unterstützung in Anspruch zu nehmen, indem du Fragen stellst, Probleme löst, Probleme meldest oder Verbesserungsvorschläge machst
* Es ermöglicht uns zu sehen wie viele Steam-Konten tatsächliche von ASF verwendet werden

Wir betrachten die Steam Group als einen entscheidenden Teil der ASF-Community. Dies ist unser wichtigster Kommunikationskanal, den wir für alle wichtigen Angelegenheiten im Zusammenhang mit ASF nutzen, insbesondere um Sie über die Entwicklung, mögliche Probleme, eventuelle Warnungen und alle anderen Angelegenheiten auf dem Laufenden zu halten, auf die Sie als Benutzer Zugriff haben sollten. Wir profitieren in keiner Weise von der Pflege dieser Gruppe, es ist der Ort, der ASF-Anwendern gewidmet ist, und wir betrachten Sie als Teil unserer Community. Da die Mitgliedschaft in der Gruppe Sie in keiner Weise als ASF-Benutzer identifiziert, betrachten wir dies nicht als ein Problem in Bezug auf die Privatsphäre.

2. Wenn dein Konto **[uneingeschränkt](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663)** ist, unter Verwendung von **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE#asf-2fa)**, dein **[öffentliches Inventar](https://steamcommunity.com/my/edit/settings)** mit mindestens 100 `MatchableTypes` Elementen darin hat und du absichtlich `SteamTradeMatcher` in deinen `TradingPreferences` aktiviert hast, dann wird ASF regelmäßig mit unserem **[Server](https://asf.justarchi.net)** kommunizieren um die aktivierte Funktionalität zu erfüllen. Tatsächliche Daten bestehen aus einer eindeutigen ASF-ID (die von ASF generiert wird) und folgenden kontobezogenen Informationen:

* Deine Steam-ID (in 64-Bit-Form, zur Generierung von Links)
* Dein Nickname (zu Anzeigezwecken)
* Dein Avatar (hash, zu Anzeigezwecken)
* Dein **[Handels-Code](https://steamcommunity.com/my/tradeoffers/privacy)** (damit Leute außerhalb deiner Freundesliste dir Handelsangebote schicken können)
* Deine `MatchableTypes` (zum Anzeigen und Abgleichen)
* Wert von `MatchEverything` in deinen `TradingPreferences` (für Anzeigezwecke und Zusammenführung)
* Gesamtzahl der `MatchableTypes` Steam-Gegenständen in deinem Inventar (für Anzeigezwecke und Zusammenführung)
* Gesamtzahl der einzigartigen Spiele, die über `MatchableTypes` Steam Gegenstände bestehen (für Anzeigezwecke und Zusammenführung)

ASF wird **keine** weiteren Daten (außer die aufgezählten) ohne vorherige wichtige Mitteilung im Änderungsprotokoll und aus einem sehr guten praktischen Grund sammeln. Wir erachten das Obige nicht als eine ernste Angelegenheit, und wir erwähnen es, um dich wissen zu lassen, was genau ASF neben dem, was du selbst konfiguriert hast, macht, damit die Leute unseren Standpunkt besser verstehen können.

* * *

# Nutzung der Daten

Alle im zweiten Punkt angegebenen Werte werden für unser **öffentliches ASF STM Inserat** verwendet, das im Folgenden erläutert wird. Wir verwenden keine weiteren Daten für andere Zwecke.

* * *

## Öffentliches ASF STM Inserat

Unsere öffentliche ASF STM-Liste befindet sich auf **[unserer Website](https://asf.justarchi.net/STM)** und wird als öffentlicher Dienst sowohl für ASF-Benutzer, die `MatchActively` nutzen, als auch für ASF- und Nicht-ASF-Benutzer für manuelles Matching verwendet.

### Wie es genau funktioniert

ASF sendet nach der Anmeldung einmalig erste Daten, die alle Eigenschaften enthalten, die von der öffentlichen Auflistung verwendet werden. Dann sendet ASF alle 10 Minuten eine sehr kleine "Heartbeat"-Anfrage, die unseren Server darüber informiert, dass der Bot noch aktiv ist. Wenn der "Heartbeat" aus irgendeinem Grund nicht ankam, z.B. aufgrund von Netzwerkproblemen, wird ASF versuchen, ihn jede Minute erneut zu senden bis der Server ihn registriert.

Auf diese Weise kann unsere Webseite erkennen, welche Konten für den Abgleich verwendet werden können und ob sie noch aktiv sind. Dank dieser Tatsache kann unsere Webseite alle ASF 2FA+STM Konten anzeigen die in den **letzten 15 Minuten** aktiv waren.

Die Benutzer werden nach ihren Beständen (in absteigender Reihenfolge) sortiert - `MatchEverything` Bots mit `Any` Banner, die alle 1:1 Trades akzeptieren, dann `MatchableTypes` unique games count, und schließlich `MatchableTypes` items count.

Bitte beachte, dass du **nicht** auf der Webseite angezeigt wirst, wenn du nicht alle Anforderungen erfüllst. ASF wird sich in diesem Fall nicht einmal die Mühe machen, mit unserem Server zu kommunizieren, so dass der zweite Punkt für Sie komplett übersprungen wird, wenn Sie `SteamTradeMatcher` nicht absichtlich aktiviert haben, um sich selbst zu helfen, Duplikate zu finden. Auch die öffentliche Auflistung ist nur mit der neuesten stabilen Version von ASF kompatibel und kann sich weigern, veraltete Bots anzuzeigen, insbesondere wenn ihnen Kernfunktionen fehlen, die nur in neueren Versionen zu finden sind.

### API

ASF STM Inserate akzeptieren vorerst nur ASF-Bots. Es gibt vorerst keine Möglichkeit Drittanbieter-Bots in unsere Auflistung einzutragen (da wir ihren Quellcode nicht einfach überprüfen können und sicherstellen müssen, dass sie unserer gesamten Handelslogik entsprechen).

Wenn du nach einem einfachen Weg suchst, um programmgesteuert auf unser Angebot zuzugreifen, haben wir einen sehr einfachen **[/Api/Bots](https://asf.justarchi.net/Api/Bots)** Endpunkt, den du verwenden kannst. Dies ist auch der Endpunkt, den ASF intern für `MatchActively` Benutzer verwendet.

### Bemerkung

*Das gesamte Konzept, zusammen mit der Webseitenintegration und dem ASF-Reporting befindet sich noch in der Beta-Phase - es kann im Laufe der Zeit verbessert/geändert werden - auch dann, wenn wir das Gefühl haben, dass es nicht genügend Interesse an diesem Feature gibt.*

* * *

## Zustimmung verweigern

Die Teilnahme an Statistiken ist **nicht obligatorisch**, obwohl sie für die Zukunft des Programms sehr empfohlen wird. Wir verurteilen dich nicht und wenn du den inneren Drang verspürst, die Tatsache zu verbergen, dass du ASF-Benutzer bist, dann kannst du die Statistik **vollständig** deaktivieren, indem du die globale Konfigurationseigenschaft `Statistics` auf `false` stellst. Deaktivierte Statistiken machen das gesamte Modul unbrauchbar und führen keine der in unserer Datenschutzerklärung oben genannten Aktionen aus.

Das Deaktivieren von Statistiken könnte **unseren technischen Support, ASF-Funktionalität und andere Dinge beeinflussen, die Ihnen kostenlos angeboten werden**. Zum Beispiel können Sie `MatchActively` nicht verwenden, ohne `Statistik` aktiviert zu haben (da Sie sich weigern, mit unserem Server zu kommunizieren, damit die Funktionalität funktioniert).
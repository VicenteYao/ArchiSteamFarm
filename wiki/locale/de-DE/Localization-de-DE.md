# Lokalisierung

ASF wird von dem Dienst Crowdin unterstützt, wodurch es jedem ermöglicht wird ASF in alle weltweit gesprochenen Sprachen zu übersetzen. Für eine detailliertere Erklärung wie Crowdin funktioniert lies dir bitte die **[Einführung in Crowdin](https://support.crowdin.com/crowdin-intro)** durch.

Wenn du daran interessiert bist, was zur Zeit vor sich geht, kannst du die **[aktuellen ASF Crowdin Aktivitäten](https://crowdin.com/project/archisteamfarm/activity_stream)** ansehen.

* * *

## Umfang

Unsere Plattform unterstützt die Lokalisierung unseres ASF-Hauptprogramms sowie aller lokalisierbaren Inhalte, die wir zusammen mit diesem anbieten. Dies beinhaltet insbesondere unseren ASF-WebConfigGenerator, ASF-ui sowie auch das Wiki selbst. All das kann man bequem durch das Crowdin-Interface übersetzen.

* * *

## Anmeldung

Wenn du bei der Arbeit an ASF helfen möchtest, entweder durch Übersetzen, Überprüfen oder Genehmigen von Übersetzungen, melde dich bitte auf unserer **[Crowdin-Projektseite](https://crowdin.com/project/archisteamfarm)** an. Die Registrierung ist einfach und absolut kostenlos! Nach dem Einloggen kannst du Sprachen auswählen, denen du gern zugewiesen werden möchtest und anschließend zu den ASF-Zeichenketten gehen und dem Rest der Community helfen, ASF in alle gängigen Sprachen zu übersetzen!

* * *

### Übersetzen

Wenn der Sprache deiner Wahl noch einige Zeichenketten fehlen, kannst du sie dir schnappen und mit der Übersetzung beginnen. Wir haben versucht, unser Bestes in Bezug auf die Flexibilität der Übersetzungen zu geben, daher enthalten viele Zeichenketten zusätzliche Variablen, die ASF während der Laufzeit bereitstellen wird - diese sind in Klammern mit einer Zahl eingeschlossen, wie z.B. `{0}`. Dies ermöglicht es dir, das Standard-ASF-Format der Zeichenkette zu ändern, z.B. indem du die von ASF bereitgestellte Variable an einen Ort verschiebst, der deiner Sprache und deiner Übersetzung entspricht, anstatt in einen strengen Zusammenhang und Format gezwungen zu werden. Das ist besonders wichtig in Sprachen wie Hebräisch, die von rechts nach links gelesen werden.

Zum Beispiel könntest du folgende Zeichenkette haben:

> Wir haben {0} Spiele zum Sammeln.

Aber basierend auf deiner Sprache könnte folgender Satz mehr Sinn machen:

> Die Zahl der verbleibenden Spiele zum Sammeln ist {0}.

Oder:

> {0} ist die Zahl der verbleibenden Spiele zum Sammeln.

Diese Flexibilität wird speziell für dich bereitgestellt, so dass du den ASF-Satz leicht umformulieren kannst, um ihn besser an deine Sprache anzupassen und die von ASF bereitgestellte Nummer oder andere Informationen an einen Ort zu verschieben, der zu deiner Übersetzung passt (anstatt jeden Teil unabhängig zu übersetzen). Das Verbessert die allgemeine Übersetzungsqualität.

* * *

### Überprüfung

Wenn deine Zeichenkette bereits von jemand anderem übersetzt wurde, kannst du dafür stimmen. Stimmen macht es möglich die beste Variante einer Übersetzung auszusuchen, anstatt an einem ursprünglichen Vorschlag festzuhalten - Das Verbessert die Qualität der Übersetzungen weiter. Du kannst für bereits verfügbare Vorschläge stimmen oder deinen eigenen Vorschlag der Übersetzung hinzufügen, der durch den selben Prozess gehen wird. Schlussendlich wird ein endgültiger Wert gewählt, basierend auf dem Vorschlag mit den meisten Stimmen oder als Wahl eines Korrekturlesers, der für diese Sprache ausgewählt wurde, der die gegebene Übersetzung persönlich genehmigt (unter Anderem basierend auf deinen Stimmen).

**Du brauchst keine Genehmigung um deine Übersetzungen in ASF zu sehen**. Genehmigung heißt einfach, dass jemand dem wir vertrauen deinen Beitrag angesehen und für gut befunden hat. Es ist in Ordnung, nicht genehmigte Übersetzungen, die von der Community gemacht wurden zu haben, wo du für die Beste Version stimmst. So lange es übersetzt ist, ist es in Ordnung! Wenn du denkst, dass die aktuelle Übersetzung schlecht ist, kannst du gerne jederzeit für eine bessere stimmen oder selbst eine vorschlagen.

* * *

### Korrekturlesen

Es ist eine gute Idee eine konsistente Übersetzung zu haben, selbst, wenn es möglicherweise die Freiheiten der oben erklärten Community-Rezensierung oder des Stimmprozesses einschränken könnte. Das ist hauptsächlich weil falsche Übersetzungen, die nicht notwendigerweise schlecht sind, so viele Stimmen bekommen könnten, dass es nicht möglich ist eine bessere Übersetzung vorzuschlagen, selbst, wenn diese jemand hat.

Wenn du bereits in der Vergangenheit Beiträge auf Crowdin oder einer anderen Übersetzungsplattform übersetzt hast, die wir verifizieren und vertrauensvoll nennen können, würden wir uns freuen, dir Korrekturleser-Zugriff zu der Sprache zu geben, die du übersetzt, damit es dir möglich ist gegebene Übersetzungen zu genehmigen oder konsistenter zu gestalten. Korrekturlesen ist keine einfache Aufgabe, besonders, weil ASF von Zeit zu Zeit sehr "technisch" und schwer zu übersetzen sein kann, aber wir verstehen, dass es für eine perfekte Übersetzung oft nötig ist. Wenn du uns helfen kannst eine Sprache Korrektur zu lesen, bitten wir dich: **[Gib uns Bescheid](https://crowdin.com/messages/create/13177432/240376)**, bedenke aber, dass du deine Anfrage mit vergangenen Beiträgen, die wir verifizieren können (etwa bei ASF oder anderen Projekten auf Crowdin zu helfen), stützen musst. Wir könnten auch fortgeschrittenen Benutzern das initiale Korrekturlesen erlauben, wenn wir sie persönlich kennen und sie mit dem Rest der Community kooperieren können um ASF in der gegebenen Sprache bestmöglich zu übersetzen.

Die generellen Regeln gelten auch für das Korrekturlesen - nimm dir Zeit, hör auf die Benutzer, arbeite als Projektmanager, löse Probleme und stell sicher, dass du die Dinge besser machst und nicht schlimmer.

* * *

### Probleme

Wenn du ein Problem mit einer bestimmten Übersetzung hast, weil du zum Beispiel nicht weißt, wie sie zu übersetzen ist, eine genehmigte Übersetzung falsch ist, du mehr Kontext benötigst oder Ähnliches, dann Poste bitte einen Kommentar unter dem betroffenen String und markiere ihn mit [X] als ein Problem.

**Bitte vermeide das [x] wenn du keine technische Erklärung oder Aktion eines Admins benötigst**. Es steht dir frei Kommentare für Diskussionen zur Übersetzung einer Zeichenkette zu verwenden, aber der "Issue-Tag" sollte nur verwendet werden, wenn eine weitergehende technische Erklärung oder Lösung durch einen Admin benötigt wird und wird typischerweise jemanden betreffen, der deine Sprache nicht spricht, weshalb du Problem-Kommentare möglichst auf Englisch schreiben solltest (damit wir verstehen, was das Problem ist).

Es gibt 4 Typen von Problemen, die wir unterstützen:

* generelle Frage - Für alles, was keinen der anderen Typen betrifft. Dieser Typ **sollte vermieden** werden, weil es dann höchstwahrscheinlich **kein** Übersetzungsproblem ist. Trotzdem ist diese Möglichkeit für alle anderen Fälle offen.
* Die aktuelle Übersetzung ist falsch - Dies sollte **nur** verwendet werden, wenn die Übersetzung von einem Korrekturleser genehmigt wurde und du glaubst, dass sie falsch ist, weil sie etwa einen Tippfehler beinhaltet oder du einen guten Vorschlag hast sie zu verbessern. Dieser Typ sollte nie bei Übersetzungen verwendet werden, die durch Community-Stimmen gewählt wurden, in welchem Fall du den Benutzer der die Übersetzung geschrieben hat kontaktieren und ihn um eine Korrektur bitten oder einfach für eine bessere Übersetzung stimmen solltest.
* Fehlender Kontext - Dies ist, was du verwenden solltest, wenn du nicht sicher bist, welchen Teil von ASF du übersetzt und was der Kontext der gegebenen Zeichenkette oder sein Sinn ist. Dieser Typ sollte nur für die Entwicklung von ASF verwendet werden, da er bedeutet, dass du technische Hilfe benötigst, weil du nicht sicher bist, wie die Zeichenkette zu übersetzen ist.
* Fehler in der Quell-Zeichenkette - Dies sollte nur verwendet werden, wenn du denkst, dass das Original (Englisch) falsch ist. Sehr selten, aber Englisch ist nicht meine Muttersprache von daher zögere nicht diesen zu verwenden, wenn du eine generelle Idee hast, wie man es verbessern könnte.

* * *

### Übersetzungsfortschritt

Jede Sprache hat zwei Zustände des Fortschritts - Übersetzung und Korrekturlesen.

Eine Sprache gilt als **übersetzt** wenn ihr Übersetzungsfortschritt 100% erreicht. Zu diesem Zeitpunkt hat jede übersetzbare Zeichenkette die von ASF verwendet wird eine entsprechende Bedeutung, was wunderbar ist. Allerdings heißt das nicht, dass das kein Raum für Verbesserungen lässt - das weitere Stimmen ist durchgehend aktiviert und du kannst immer noch Vorschläge für bessere Übersetzungen einbringen und für existierende Stimmen. Bitte bedenke, dass bereits fertig übersetzte Sprachen immer noch unter 100% fallen können, wenn wir während der Entwicklung existierende Zeichenketten ändern oder neue hinzufügen. Du kannst entsprechende Crowdin-Benachrichtigungen aktivieren, wenn du in einem solchen Fall eine E-Mail erhalten willst.

Gewählte Sprachen können entsprechende Korrekturleser haben, die Übersetzungen validieren und endgültige Versionen auswählen. Dies ist der letzte Schritt nach der Übersetzung und erlaubt weitere Verbesserungen.

ASF wird die Sprache **so bald wie möglich** beinhalten, was bedeutet, dass sie nicht genehmigt oder zu 100% übersetzt werden muss. Die tatsächlich verwendeten Zeichenketten sind immer die am beliebtesten im Bezug auf Stimmen, außer der gewählte Korrekturleser hat anders entschieden (sehr selten). Deshalb kannst du deine Bemühungen direkt in der nächsten Version von ASF sehen, sobald die Übersetzung auf Crowdin ist. Typischerweise aktualisieren wir Übersetzungen im selben Moment in dem wir auch die neue ASF-Version herausgeben werden.

* * *

## Fehlende Sprachen

Standardmäßig verfügt das ASF-Projekt nur Übersetzungen für die 30 meistgesprochenen Sprachen der Welt. Wenn du eine weitere (oder einen lokalen Dialekt zu einer existierenden) hinzufügen willst, **[lass es uns bitte wissen](https://crowdin.com/messages/create/13177432/240376)** und wir werden sie schnellstmöglichst hinzufügen. Wir wollen nicht mehrere hundert verschiedene Sprachen, wenn sie keiner übersetzt. Deshalb haben wir sie auf diese Anzahl reduziert. Bitte zögere nicht uns zu kontaktieren, wenn du eine nicht gelistete Sprache übersetzen willst. Für uns ist es sehr einfach eine weitere hinzuzufügen. Achte einfach darauf, dass du den tatsächlichen Willen und die Entschlossenheit hast ASF in deine Sprache zu übersetzen bevor du dich entscheidest mit uns Kontakt aufzunehmen.

Für eine komplette Liste der verfügbaren Sprachen, in die ASF übersetzt werden kann, **[klicke hier](https://support.crowdin.com/api/language-codes)**.

* * *

## Pluralisierung

Jede Sprache hat ihre eigenen Regeln in Bezug auf die Pluralisierung. Diese Regeln findest du unter **[CLDR](https://www.unicode.org/cldr/charts/latest/supplemental/language_plural_rules.html)**, welche ihre Anzahl und genauen Sprachbedingungen angibt.

Wir tun unser Bestes, um dir eine flexible Lokalisierung anzubieten, und so lange wie möglich, wird dies auch Regeln für Pluralisierung beinhalten. Als Beispiel werden wir heute folgende Zeichenkette ins Polnische übersetzen:

> Released {PLURAL:n|{n} month|{n} months} ago

`PLURAL` Schlüsselwort wird hier besonders behandelt, da es dir erlaubt, alle Pluralformen einzubeziehen, die deine Sprache unterstützt. Wenn du einen Blick auf CLDR wirfst, wirst du sehen, dass es im Englischen nur 2 kardinale Formen gibt - "eine" und "andere". Und wie du oben sehen kannst, haben wir beide definiert - `{n} month` und `{n} months`.

Unsere polnische Sprache umfasst jedoch 4 von ihnen - "eine", "wenige", "viele" und "andere". Das bedeutet, dass wir alle für die vollständige Umsetzung definieren sollten. Unsere Übersetzungsprogramme sind bereits intelligent genug, um eine geeignete Pluralform basierend auf Sprachregeln auszuwählen, so dass du nur alle diese in der Übersetzung definieren musst:

> Wydany {PLURAL:n|{n} miesiąc|{n} miesiące|{n} miesięcy|{n} miesiąca} temu

Auf diese Weise haben wir alle 4 Pluralformen für unsere polnische Sprache definiert, und da unsere Lokalisierungsbibliothek bereits die genauen Regeln kennt, wird sie die richtige Form für die angegebene `{n}` Nummer korrekt verwenden.

Es ist nicht zwingend erforderlich alle von deiner Sprache verwendeten Pluralformen zu definieren. Wenn sie fehlt, verwendet unsere Lokalisierungsbibliothek die zuletzt definierte Form an ihrer Stelle. Es ist eine gute Idee, alle von deiner Sprache verwendeten Pluralformen zu definieren, aber in einigen Fällen können die verbleibenden Pluralformen die gleichen sein wie die letzten, in diesem Fall ist es nicht notwendig, sie zu wiederholen. In unserem obigen Beispiel war es zwingend erforderlich, da die "andere" Form im Polnischen für Monate "miesiąca" ist und nicht "miesięcy" wie in "vielen".

* * *

## Wiki

Unsere Crowdin-Plattform ermöglicht es dir sogar selbst das Wiki zu lokalisieren. Dies ist ein sehr mächtiges Programm, da es dir ermöglicht, eine komplette ASF-Dokumentation in deiner Muttersprache zu erstellen und so das allerletzte Problem bei der ASF-Lokalisierung effektiv zu lösen. Zusammen mit der Übersetzung des Programms und alle seiner Teile macht dies die Lokalisierung komplett.

Das Wiki ist in dieser Hinsicht etwas Besonderes, da es eine Online-Hilfe ist, bei der man sich nicht zu sehr an den ursprünglichen Satz halten muss. Das bedeutet, dass du mit deiner Sprache so natürlich wie möglich umgehen und eine originelle Bedeutung und Hilfe liefern solltest - nicht unbedingt mit der ursprünglichen Zeichenkette, den verwendeten Wörtern und der tatsächlichen Interpunktion. Scheu dich nicht die Zeichenkette in etwas viel natürlicheres für deine Sprache umzuschreiben, solange du die allgemeine Richtung und die im Satz enthaltene Hilfe einhältst.

* * *

### Globale Links

Unsere Crowdin-Plattform ermöglicht es dir auch, den Originaltext so anzupassen, dass er auf neue (lokalisierte) Standorte verweist.

ASF enthält Links auf fast jeder Seite zur leichteren Navigation sowie eine Seitenleiste auf der rechten Seite. Die fantastische Tatsache ist, dass du all das bearbeiten kannst, indem du Links "fixierst", um auf richtige lokalisierte Seiten für deine Sprache zu verweisen. Es erfordert ein wenig Vorsicht, wenn du das tust, aber es ist möglich.

Zum Beispiel enthält die **[Startseite](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home-de-DE)** von ASF folgenden Text:

> Wenn du ein neuer Benutzer bist, empfehlen wir dir mit dem Leitfaden zur **[Installation](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Setting-up-de-DE)** anzufangen.

Der eigentlich so geschrieben wird:

```markdown
Wenn du ein neuer Benutzer bist, empfehlen wir dir mit dem Leitfaden zur **[Installation](https://github.com/JustArchi/ArchiSteamFarm/wiki/Setting-up-de-DE)** anzufangen.
```

Auf Crowdin solltest du zuerst zu deinen Editor-Einstellungen gehen und sicherstellen, dass HTML-Tags für dich auf "Show" gesetzt sind. Dies ist sehr wichtig, wenn du dich dazu entscheidest das Wiki zu übersetzen.

* * *

![Crowdin](https://i.imgur.com/YqAxiZ4.png)

* * *

Nun, während der Übersetzung auf Crowdin, je nach Formatierung, siehst du ASF-Links im Text entweder als:

* Zu übersetzende Zeichenkette zusammen mit HTML-Tags (Mehrheit der Zeichenketten, wobei nur ein Teil des Satzes ein Link ist)
* Einzelne zu übersetzende Zeichenkette, mit Link in `Hidden texts` -> `Link addresses` (selten, wo die gesamte Zeichenkette ein Link ist, am häufigsten in der Seitenleiste)

In unserem obigen Beispiel ist dies der erste Fall (da nur "Installation" ein Link ist), so dass wir es in Crowdin folgendermaßen sehen werden:

* * *

![Crowdin 2](https://i.imgur.com/Li5RzS3.png)

* * *

Unabhängig vom Fall solltest du zuerst den Quelltext kopieren und wie gewohnt übersetzen, wobei du das gesamte HTML (falls vorhanden) intakt lässt. Dies wäre ein Beispiel für eine Übersetzung in die polnische Sprache:

* * *

![Crowdin 3](https://i.imgur.com/NpKwfka.png)

* * *

Wenn es sich bei dem Link um einen generischen Link handelt, der außerhalb des Wikis liegt (z.B. auf die neueste ASF-Version), kannst du ihn so lassen wie er ist, da du ihn in diesem Fall nicht bearbeiten solltest. Du kannst ihn speichern und weitermachen.

Wenn der Link jedoch weiter **innerhalb** des Wikis zeigt, wie der oben genannte, kannst du ihn tatsächlich korrigieren, um auf einen neuen (lokalisierten) Pfad zu verweisen. Du erreichst dies, indem du `-locale` sorgfältig an die Ziel-URL im `<a>`-Tag anhängst, wie unten beschrieben:

* * *

![Crowdin 4](https://i.imgur.com/TL8uwmb.png)

* * *

Achte darauf, dass deine URL tatsächlich existiert, denn wenn du einen Fehler machst wird dieser Link nicht mehr funktionieren. Wenn du erfolgreich warst, hast du jetzt eine voll funktionsfähige Übersetzung mit einem Link, der auf die übersetzte (in unserem Fall `Setting-up-pl-PL`) Seite zeigt.

Wenn du die obigen Schritte ausführst, wird unser HTML-Code korrekt zurück in Markdown übersetzt:

```markdown
Jeśli jesteś nowym użytkownikiem, zalecamy rozpoczęcie od korzystania z **[przewodnika po konfiguracji](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Setting-up-pl-PL)**.
```

Und schließlich in den Wiki-Text:

> Jeśli jesteś nowym użytkownikiem, zalecamy rozpoczęcie od korzystania z **[przewodnika po konfiguracji](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Setting-up-pl-PL)**.

Wenn kein HTML vorhanden ist (zweiter Fall), ist das noch einfacher, da du einfach zu `Hidden texts` -> `Link addresses` gehen kannst.

* * *

![Crowdin 5](https://i.imgur.com/ZmgavCM.png)

* * *

Von dort aus kannst du den Link zum Verweis auf eine neue Position leicht korrigieren ohne dich überhaupt um HTML kümmern zu müssen:

* * *

![Crowdin 6](https://i.imgur.com/maG7kSm.png)

* * *

### Lokale Links

Im gesamten Wiki findest du auch lokale Links die auf einen bestimmten Abschnitt des Dokuments verweisen. Diese Links beinhalten ein `#` Zeichen die dem Webbrowser anzeigen, dass er sich in Richtung dieses Abschnitts des Dokuments bewegen soll.

Dies sind nun Sonderfälle, da diese Links auf den Namen der Abschnitte des aktuellen Dokuments basieren. Während wir für URLs die allgemeine Konvention haben der URL die `-locale` hinzuzufügen (dies funktioniert überall), werden Abschnittsnamen von dir und anderen Leuten übersetzt, also musst du sicherstellen, dass sie auf den richtigen Ort zeigen.

Beispielsweise findest du den Link `#introduction` in unserem Abschnitt **[configuration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#introduction)**:

* * *

![Crowdin 7](https://i.imgur.com/EEHSPtK.png)

* * *

Da wir das Wort "Introduction" in "Wprowadzenie" für unsere polnische Sprache übersetzen werden, müssen wir diesen Link korrigieren, da er sonst nicht mehr funktioniert.

* * *

![Crowdin 8](https://i.imgur.com/JMJegO7.png)

* * *

Auf diese Weise funktioniert unser lokaler Link weiterhin, da er nun auf den Namen des Bereichs zeigt den wir verwenden. Du kannst Links innerhalb von HTML-Tags auf die gleiche Weise korrigieren.

* * *

### Codeblöcke

Sei äußerst sorgfältig, wenn du Sätze mit `<code></code>` Blöcken übersetzt. Der Codeblock zeigt feste ASF-Codenamen oder Begriffe an die nicht übersetzt werden sollten. Zum Beispiel:

> Das ist besonders nützlich, wenn du eine große Menge an Produktschlüsseln aktivieren möchtest und du sicherlich den `RateLimited` Status erreichst, bevor du mit deiner gesamten Charge fertig bist.

Wie du sehen kannst, befindet sich das Wort `RateLimited` hier in einem Codeblock und zeigt den internen ASF-Code-Status an, der nicht übersetzt werden sollte. Ebenso solltest du keine anderen Codeblöcke übersetzen, wie z.B. Namen von Konfigurationseigenschaften (z.B. `TradingPreferences`), Enum-Mitglieder (z.B. `Stable` und `Experimental` Optionen von `UpdateChannel`) und ähnliches.

Nur weil diese Wörter nicht übersetzt werden sollten, bedeutet das nicht, dass du ihnen keine entsprechende Übersetzung hinzufügen kannst, zum Beispiel in Klammern.

> Das ist besonders nützlich, wenn du eine große Menge an Produktschlüsseln aktivieren möchtest und du sicherlich den `RateLimited` (zu häufiges Aktivieren) Status erreichst, bevor du mit deiner gesamten Charge fertig bist.

Wie du oben sehen kannst, haben wir neben `RateLimited` "zu häufiges Aktivieren" hinzugefügt, um diesen Status benutzerfreundlich zu übersetzen, während gleichzeitig das ursprüngliche ASF-Wort beibehalten wird, dass der Benutzer während der Nutzung des Programms eventuell sieht. Auf die gleiche Weise kannst du auch andere ähnliche Fälle von verschiedenen Wörtern und Sätzen übersetzen/erklären.

Wenn du glaubst, dass etwas Unangemessenes in einem Codeblock enthalten ist oder es einen Text gibt der sich nicht in einem Codeblock befindet, sich aber in einem Codeblock befinden sollte, kannst du gerne hier auf Crowdin fragen, indem du ein entsprechendes **[Problem](#Probleme)** erstellst. Dies dient auch als praktisches Beispiel für die Nutzung eines lokalen Links.

* * *

## Ruhmeshalle

Wir möchten unseren ewigen Dank den Menschen zeigen, die viel ihrer Zeit und Bereitschaft gegeben haben die ASF Lokalisierung zu verbessern. Diese Mitwirkenden haben bislang mindestens über **20 Tausend Wörter** übersetzt, das entspricht ungefähr der Hälfte des gesamten Projektes. Ihre Mühe ist unglaublich, und Sie können komplette Übersetzungen, einschließlich des Wikis, vor allem dank ihnen genießen.

| Mitwirkender                                               | Sprachen             |
| ---------------------------------------------------------- | -------------------- |
| **[Astaroth](https://crowdin.com/profile/ismacanto)**      | Spanisch             |
| **[Dead_Sam](https://crowdin.com/profile/Dead_Sam)**       | Portugiesisch (BR)   |
| **[deluxghost](https://crowdin.com/profile/deluxghost)**   | Chinesisch (CN)      |
| **[Ryzhehvost](https://crowdin.com/profile/Ryzhehvost)**   | Russisch, Ukrainisch |
| **[SKANKHUNTER](https://crowdin.com/profile/MrBurrBurr)**  | Deutsch              |
| **[XinxingChen](https://crowdin.com/profile/XinxingChen)** | Chinesisch (HK)      |

Vielen Dank, an euch alle, für die Verbesserung unserer ASF-Lokalisierungsqualität!
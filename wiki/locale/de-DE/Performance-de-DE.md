# Performance

Das Hauptziel von ASF ist es, so effektiv wie möglich zu sammeln, basierend auf zwei Arten von Daten, mit denen es arbeiten kann - einem kleinen Satz von benutzerdefinierten Daten, die für ASF unmöglich sind selbst zu erraten/kontrollieren, und einem größeren Satz von Daten, der von ASF automatisch überprüft werden kann.

Im Automatikmodus erlaubt ASF dir nicht, die Spiele auszuwählen, die gesammelt werden sollen, noch erlaubt es dir, den Algorithmus für das Karten sammeln zu ändern. **ASF weiß besser als du, was es tun sollte und welche Entscheidungen es treffen sollte, um so schnell wie möglich zu sammeln**. Dein Ziel ist es, die Konfigurationseigenschaften richtig einzustellen, da ASF sie nicht alleine erraten kann, alles andere ist abgedeckt.

* * *

Vor einiger Zeit hat Valve den Algorithmus um Karten zu erhalten geändert. Von diesem Zeitpunkt an können wir Steam-Konten nach zwei Kategorien kategorisieren: die **mit eingeschränkten** Karten drops und die **ohne**. Der einzige Unterschied zwischen diesen beiden Typen ist die Tatsache, dass Konten mit eingeschränkten Karten drops keine Karten aus dem gegebenen Spiel erhalten können, bis sie das gegebene Spiel für mindestens `X` Stunden spielen. Es hat den Anschein, dass ältere Konten, die nie um Rückerstattung gebeten haben, **unbeschränkte Karten drops** haben, während neue Konten und diejenigen, die um Rückerstattung gebeten haben, **beschränkte Karten drops** haben. Dies ist jedoch nur eine Theorie und sollte nicht als Tatsache betrachtet werden. Deshalb gibt es **keine offensichtliche Antwort** und ASF verlässt sich darauf, dass **du** ihm sagst, welcher Fall für dein Konto geeignet ist.

* * *

ASF beinhaltet derzeit zwei Sammel-Algorithmen:

**Einfacher** Algorithmus funktioniert am besten bei Konten, die über unbegrenzte Karten drops verfügen. Dies ist der primäre Algorithmus, der von ASF verwendet wird. Der Bot findet Spiele für das Sammeln und sammelt sie einzeln, bis alle Karten gesammelt wurden. Dies liegt daran, dass die Karten-Sammel-Rate beim Sammeln von mehr als einem Spiel gegen Null tendiert und völlig ineffektiv ist.

**Komplex** ist ein neuer Algorithmus der implementiert wurde, um auch eingeschränkten Konten zu helfen, ihre Gewinne zu maximieren. ASF wird zunächst den Standard **Einfacher** Algorithmus für alle Spiele verwenden, die `HoursUntilCardDrops` Stunden Spielzeit überschritten haben, dann, wenn keine Spiele mit >= `HoursUntilCardDrops` Stunden übrig sind, wird es alle Spiele (mit einem Limit von `32`) mit < `HoursUntilCardDrops` Stunden gleichzeitig sammeln, bis einer von ihnen `HoursUntilCardDrops` Stunden Marke erreicht hat, dann wird ASF die Schleife von Anfang an fortsetzen (verwende **Einfach** auf diesem Spiel, kehre zur gleichzeitigen auf < `HoursUntilCardDrops` und so weiter zurück). In diesem Fall können wir "Mehrere-Spiele-Sammeln" verwenden, um die Stunden der Spiele, die wir Sammeln müssen, auf einen angemessenen Wert zu bringen. Bedenke, dass ASF während des Stunden-Sammelns **keine** Karten sammelt. Daher wird es auch nicht prüfen ob während dieses Zeitraums Karten gesammelt wurden (aus den oben genannten Gründen).

Derzeit wählt ASF den Karten-Sammel-Algorithmus, der ausschließlich auf der Konfigurationseigenschaft `HoursUntilCardDrops` basiert (die **du** festlegst). Wenn `HoursUntilCardDrops` auf `0` gesetzt ist, wird **Einfacher** Algorithmus verwendet, ansonsten wird stattdessen **Komplex** Algorithmus verwendet.

* * *

### **Es gibt keine eindeutige Antwort, welcher Algorithmus für dich besser ist**.

Dies ist einer der Gründe, warum du keinen Karten-Sammel-Algorithmus wählst, sondern ASF mitteilst, ob das Konto eingeschränkte Karten Drops hat oder nicht. Wenn das Konto nicht eingeschränkte Karten Drops hat, wird auf diesem Konto **Einfacher** Algorithmus **besser funktionieren**, da wir keine Zeit damit verschwenden werden, alle Spiele auf `X` Stunden zu bringen - die Drops Rate der Karten liegt bei mehreren Spielen nahe bei 0%. Auf der anderen Seite, wenn dein Konto eingeschränkt Karten Drops hat, wird der **Komplex** Algorithmus für dich besser sein, da es keinen Sinn macht, alleine zu arbeiten, wenn das Spiel `HoursUntilCardDrops` Stunden noch nicht erreicht hat - also werden wir zuerst die **Spieldauer** und **dann** die Karten im Solomodus sammeln.

`HoursUntilCardDrops` sollte nicht blindlings gesetzt werden, nur weil dir jemand gesagt hat du sollst es tun. Du solltest Tests durchführen, Ergebnisse vergleichen und basierend auf den Daten, die du bekommst, entscheiden, welche Option für dich besser sein sollte. Wenn du minimalen Aufwand dafür treibst, wirst du sicherstellen, dass ASF mit größtmöglicher Effizienz für dein Konto arbeitet, was wahrscheinlich das ist, was du willst, wenn man bedenkt, dass du diese Wiki-Seite gerade liest. Wenn es eine Lösung gäbe, die für alle funktioniert, hättest du keine Wahl - ASF würde selbst entscheiden.

* * *

### Was ist der beste Weg, um herauszufinden, ob dein Konto eingeschränkt ist?

Stelle sicher, dass du einige Spiele zu Sammeln hast, vorzugsweise 5+, und führe ASF mit `HoursUntilCardDrops` von `0` aus. Es wäre eine gute Idee, wenn du während des Sammelns nichts spielen würdest, um genauere Ergebnisse zu erzielen (am besten ASF nachts laufen lassen). Lass ASF diese 5 Spiele sammeln und schaue dir danach das Protokoll an, um die Ergebnisse zu sehen.

ASF zeigt deutlich an, wann eine Karte für ein bestimmtes Spiel gesammelt wurde. Du möchtest den schnellsten Karten Drop den ASF erreicht hat. Wenn dein Konto beispielsweise uneingeschränkt ist, sollte ein erster Karten Drop nach etwa 30 Minuten seit Beginn des Sammelns erfolgen. Wenn du **mindestens ein** Spiel entdeckst, bei dem die Karte in den ersten 30 Minuten gesammelt wurde, dann ist dies ein Indikator dafür, dass dein Konto **nicht** eingeschränkt ist und `HoursUntilCardDrops` von `0` verwendet werden sollte.

Auf der anderen Seite, wenn du bemerkst, dass **jedes** Spiel mindestens `X` Stunden braucht, bevor die ersten Karten gesammelt werden, dann ist dies ein Indikator für dich, `HoursUntilCardDrops` anzupassen. Die Mehrheit (wenn nicht gar alle) der eingeschränkten Benutzer benötigen mindestens `3` Stunden Spielzeit, damit die Karten gesammelt werden und dies ist auch der Standardwert für die Einstellung `HoursUntilCardDrops`.

Bedenke, dass Spiele unterschiedliche Drop-Raten haben können. Deshalb solltest du testen, ob deine Theorie mit **mindestens** 3 Spielen stimmt, vorzugsweise 5+, um sicherzustellen, dass du nicht durch einen Zufall auf falsche Ergebnisse stößt. Ein Karten Drop von einem Spiel in weniger als einer Stunde ist eine Bestätigung, dass dein Konto **nicht** eingeschränkt ist und `HoursUntilCardDrops` von `0` verwendet werden kann, aber um zu bestätigen, dass dein Konto **eingeschränkt** ist, brauchst du mindestens mehrere Spiele, bei denen keine Karten gesammelt wurden, bis du eine feste Marke erreicht hast.

Es ist wichtig zu beachten, dass in der Vergangenheit `HoursUntilCardDrops` nur `0` oder `2` war und deshalb hatte ASF eine einzige `CardDropsRestricted` Eigenschaft, die es erlaubte, zwischen diesen beiden Werten zu wechseln. Mit den jüngsten Änderungen stellten wir fest, dass jetzt nicht nur die Mehrheit der Benutzer `3` Stunden anstelle der vorherigen `2` benötigt, sondern auch, dass `HoursUntilCardDrops` jetzt dynamisch ist und jeden Wert pro Konto treffen kann.

Am Ende liegt die Entscheidung natürlich bei dir.

Und um es noch schlimmer zu machen - ich habe Fälle erlebt in denen Leute von einem eingeschränkten in einen uneingeschränkten Zustand und umgekehrt gewechselt haben - entweder wegen eines Steam-Bugs (oh ja, es gibt viele davon) oder wegen einiger Änderungen an der Logik seitens Valve. Selbst wenn du also bestätigt hast, dass dein Konto eingeschränkt ist (oder nicht), glaube nicht, dass es so bleiben wird - um von uneingeschränkt zu eingeschränkt zu wechseln genügt es eine Rückerstattung zu verlangen. Wenn du das Gefühl hast, dass ein zuvor eingestellter Wert nicht mehr angemessen ist, kannst du jederzeit einen erneuten Test durchführen und ihn entsprechend aktualisieren.

* * *

Standardmäßig geht ASF davon aus, dass `HoursUntilCardDrops` `3` ist, da der negative Effekt der Einstellung auf `3`, wenn er kleiner sein sollte, kleiner ist als umgekehrt. Dies liegt daran, dass wir im schlimmsten Fall `3` Stunden Sammeln pro `32` Spiele verschwenden, verglichen mit dem Verschwenden von `3` Stunden Sammeln pro Spiel, wenn `HoursUntilCardDrops` standardmäßig auf `0` eingestellt wurde. Du solltest diese Variable jedoch immer noch so einstellen, dass sie mit deinem Konto übereinstimmt, um maximale Effizienz zu erreichen, da dies nur eine blinde Vermutung ist, die auf potenziellen Nachteilen und der Mehrheit der Benutzer basiert (daher versuchen wir, standardmäßig das "kleineres Übel" zu wählen).

Im Moment reichen zwei der oben genannten Algorithmen für alle derzeit möglichen Kontoszenarien aus, um so effektiv wie möglich zu wirtschaften, daher ist es nicht geplant, weitere hinzuzufügen.

Es ist schön zu beachten, dass ASF auch `Manuell` Sammel-Modus beinhaltet, der durch den Befehl `play` aktiviert werden kann. Du kannst mehr darüber lesen in **[Befehle](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**.

* * *

## Steam Pannen

Der Algorithmus zum Sammeln von Karten funktioniert nicht immer so, wie er sollte, und es ist durchaus möglich, dass verschiedene Steam-Pannen auftreten, wie z.B. Karten, die auf eingeschränkten Konten gesammelt werden, Karten, die beim Schließen/Schalten des Spiels gesammelt werden, Karten, die beim Spielen überhaupt nicht gesammelt werden, und ebenso.

Dieser Abschnitt ist hauptsächlich für Leute, die sich fragen, warum ASF <stark>X</stark> nicht macht, wie z.B. die Schnellumschaltung von Spielen um Karten schneller zu sammeln.

Was ist eine **Steam-Panne** - eine spezifische Aktion, die **undefiniertes** Verhalten auslöst, das **nicht beabsichtigt, undokumentiert und als logischer Fehler betrachtet wird**. Es ist **unzuverlässig per Definition**, was bedeutet, dass es nicht zuverlässig mit einer sauberen Testumgebung reproduziert werden kann, und daher ohne den Einsatz von Hacks programmiert ist, die erraten sollen, wann eine Störung auftritt und wie man damit kämpfen / sie missbrauchen kann. Normalerweise ist es temporär, bis die Entwickler den Logikfehler beheben, obwohl einige Fehlfunktionen für einen sehr langen Zeitraum unbemerkt bleiben können.

Ein gutes Beispiel für das, was als **Steam-Panne** gilt, ist nicht die ungewöhnliche Situation, eine Karte zu sammeln, wenn das Spiel geschlossen wird, was bis zu einem gewissen Grad mit der Spiel-Sprung-Funktion des untätigen Meisters missbraucht werden kann.

- **Undefiniertes Verhalten** - Du kannst nicht sagen, ob 0 oder 1 Karten gesammelt werden, wenn du die Panne auslöst.
- **Nicht beabsichtigt** - basierend auf früheren Erfahrungen und Verhaltensweisen des Steam-Netzwerkes, die nicht zu demselben Verhalten beim Senden einer einzelnen Anfrage führen.
- **Undokumentiert** - es ist klar dokumentiert auf der Steam-Website, wie Karten erhalten werden, und **an jedem einzelnen Ort** es wird klar gesagt, dass es durch **Spielen** erhalten wird, NICHT durch das Schließen von Spielen, das Erhalten von Erfolgen, das Wechseln oder das gleichzeitige Starten von 32 Spielen.
- **Betrachtet als logischer Fehler** - das Schließen von Spielen oder das Vertauschen von Spielen sollte kein Ergebnis haben, wenn Karten fallen gelassen werden, die eindeutig als durch **gewonnene Spielzeit** erhalten werden.
- **Unzuverlässig per Definition, kann nicht zuverlässig reproduziert werden** - es funktioniert nicht für alle, und selbst wenn es einmal für dich funktioniert hat, funktioniert es vielleicht nicht mehr zum zweiten Mal.

Jetzt, nachdem wir erkannt haben, was eine Steam-Panne ist, und die Tatsache, dass Karten, die fallen gelassen werden, wenn das Spiel geschlossen wird eine **ist**, können wir zum zweiten Punkt übergehen - **ASF missbraucht das Steam-Netzwerk in keiner Weise per Definition, und es tut sein Bestes, um Steam ToS, seine Protokolle und das, was allgemein akzeptiert wird** zu erfüllen. Das Spamming des Steam-Netzwerkes mit ständigen Anfragen zum Öffnen/Schließen des Spiels kann als **[DoS-Angriff](https://en.wikipedia.org/wiki/Denial-of-service_attack)** betrachtet werden und **verletzt direkt [Steam-Online-Verhalten](https://store.steampowered.com/online_conduct/?l=english)**.

> As a Steam subscriber you agree to abide by the following conduct rules.
> 
> You will not:
> 
> Institute attacks upon a Steam server or otherwise disrupt Steam.

Es spielt keine Rolle, ob du in der Lage bist, Steam-Pannen mit anderen Programmen (wie IM) auszulösen, und es spielt auch keine Rolle, ob du mit uns einverstanden bist und ein solches Verhalten als DoS-Angriff betrachtest oder nicht - es liegt an Valve, dies zu beurteilen, aber wenn wir es als Ausnutzen/Missbrauch nicht beabsichtigten Verhaltens durch übermäßige Steam-Netzwerkanfragen betrachten, dann kannst du ziemlich sicher sein, dass Valve eine ähnliche Ansicht dazu hat.

ASF wird **nie** Steam-Exploits, Missbräuche, Hacks oder jede mögliche andere Aktivität ausnutzen, die wir als **illegal oder unerwünscht** entsprechend Steam-ToS, Steam-Online-Verhalten oder irgendeiner anderen vertrauenswürdigen Quelle sehen, die anzeigen könnte, dass ASF-Aktivität durch Steam-Netzwerk unerwünscht ist, wie im Abschnitt **[Beitragen](https://github.com/JustArchiNET/ArchiSteamFarm/blob/master/.github/CONTRIBUTING.md)** angegeben.

Wenn du um jeden Preis dein Steam-Konto riskieren willst, um ein paar Cent Karten schneller als sonst zu Sammeln, dann wird ASF leider nie so etwas im Automatikmodus anbieten, obwohl du immer noch `play` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** hast, das als Werkzeug benutzt werden kann, um zu tun, was immer du willst, in Bezug auf die Steam-Netzwerkinteraktion. Wir empfehlen nicht, die Vorteile von Steam-Pannen zu nutzen und sie für den eigenen Gewinn zu nutzen - nicht nur mit ASF, sondern auch mit jedem anderen Programm. Am Ende ist es jedoch dein Konto und deine Wahl, was du damit machen willst - denk einfach daran, dass wir dich gewarnt haben.
# Handel

ASF beinhaltet Unterstützung für Steam nicht-interaktive (Offline-) Handelsangebote. Sowohl das Empfangen (Akzeptieren/Ablehnen) als auch das Senden von Handelsangeboten ist sofort verfügbar und erfordert keine spezielle Konfiguration, außer natürlich ein uneingeschränktes Steam-Konto (eins das bereits 5€ im Shop ausgegeben hat). Das Handelsmodul ist für eingeschränkte Konten nicht verfügbar.

* * *

## Logik

ASF akzeptiert immer alle Handelsangebote, unabhängig von Gegenständen, die vom Benutzer mit `Master` (oder höherem) Zugriff auf den Bot gesendet werden. Dies ermöglicht nicht nur das einfache Plündern von Steam-Karten, die von der Bot-Instanz gesammelt werden, sondern auch das einfache Verwalten von Steam-Gegenständen, die im Inventar aufbewahrt werden.

ASF lehnt Handelsangebote, unabhängig vom Inhalt, von jedem (Nicht-Master) Benutzer ab, der auf der schwarzen Liste des Handelsmoduls steht. Die schwarze Liste ist in der Standard-Datenbank `BotName.db` gespeichert und kann über die **[Befehle](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** `bl`, `bladd` und `blrm` verwaltet werden. Dies sollte als Alternative zu dem standardmäßig von Steam angebotenen Blocken eines Benutzers funktionieren - Verwendung mit Bedacht.

ASF wird alle, an Bots gesendete, `loot`-ähnlichen Handelsangebote akzeptieren, es sei denn, `DontAcceptBotTrades` ist in `TradingPreferences` angegeben. Kurz gesagt, der Standardwert `None` in `TradingPreferences` bewirkt, dass ASF automatisch Handelsangebote von Benutzern mit `Master` Zugriff auf den Bot (siehe oben) akzeptiert, sowie alle Spenden-Handelsangebote von anderen Bots die am ASF-Prozess teilnehmen. Wenn du Spenden-Handelsangebote von anderen Bots deaktivieren möchtest, dann solltest du `DontAcceptBotTrades` in deinen `TradingPreferences` verwenden.

Wenn du `AcceptDonations` in deinen `TradingPreferences` aktivierst, akzeptiert ASF auch jedes Spenden-Handelsangebot bei dem das Bot-Konto keine Gegenstände verliert. Diese Eigenschaft betrifft nur Nicht-Bot-Konten, da Bot-Konten von `DontAcceptBotTrades` betroffen sind. `AcceptDonations` ermöglicht es dir problemlos Spenden von anderen Personen anzunehmen, aber auch von Bots die nicht am ASF-Prozess teilnehmen.

Es ist gut zu erwähnen, dass `AcceptDonations` kein **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE)** erfordert, da es keine Bestätigung braucht wenn wir keine Gegenstände verlieren.

Du kannst die ASF-Handelsmöglichkeiten auch weiter anpassen, indem du die `TradingPreferences` entsprechend anpasst. Eine der Hauptfunktionen von `TradingPreferences` ist die Option `SteamTradeMatcher`, die ASF dazu veranlasst eine eingebaute Logik für die Annahme von Handelsangeboten zu verwenden. Diese hilft dir dabei fehlende Abzeichen zu vervollständigen, was besonders nützlich in Zusammenarbeit mit dem öffentlichen Inserieren von **[SteamTradeMatcher](https://www.steamtradematcher.com)** ist, aber auch ohne diese arbeiten kann. Es wird im Folgenden näher beschrieben.

* * *

## `SteamTradeMatcher`

Wenn `SteamTradeMatcher` aktiv ist, wird ASF einen ziemlich komplexen Algorithmus verwenden, um zu überprüfen, ob das Handelsangebot die STM-Regeln erfüllt und zumindest neutral gegenüber steht. Die eigentliche Logik ist die folgende:

- Lehne das Handelsangebot ab, wenn wir etwas anderes verlieren als die in unseren `MatchableTypes` angegebenen Objekttypen.
- Lehne das Handelsangebot ab, wenn wir nicht mindestens die gleiche Anzahl an Gegenständen pro Spiel und pro Typ erhalten.
- Lehne das Handelsangebot ab, wenn der Benutzer nach speziellen Steam Sommer/Winter-Verkaufskarten fragt und eine Handelssperre hat.
- Lehne das Handelsangebot ab, wenn die Dauer der Handelssperre die globale Konfigurationseigenschaft `MaxTradeHoldDuration` überschreitet.
- Lehne das Handelsangebot ab, wenn wir nicht `MatchEverything` gesetzt haben und es für uns schlimmer als neutral ist.
- Akzeptiere das Handelsangebot, wenn wir ihn nicht durch einen der oben genannten Punkte abgelehnt haben.

Es ist nett zu erwähnen, dass ASF auch Überzahlungen unterstützt - die Logik wird richtig funktionieren, wenn der Benutzer dem Handelsangebot etwas mehr hinzufügt, solange alle oben genannten Bedingungen erfüllt sind.

Die ersten 4 Ablehnungsprädikate sollten für jeden offensichtlich sein. Das letzte beinhaltet die Logik der tatsächlichen Duplikate, die den aktuellen Zustand unseres Inventars überprüft und entscheidet was der Status des Handelsangebotes ist.

- Das Handelsangebot ist **gut**, wenn unser Fortschritt in Richtung Fertigstellung voranschreitet. A A (vorher) <-> A B (nachher)
- Das Handelsangebot ist **neutral**, wenn unser Fortschritt bei der Fertigstellung intakt bleibt. A B (vorher) <-> A C (nachher)
- Das Handelsangebot ist **schlecht**, wenn unser Fortschritt in Richtung Fertigstellung zurückgeht. A C (vorher) <-> A A (nachher)

STM arbeitet nur mit guten Handelsangeboten, was bedeutet, dass Benutzer die STM für den Duplikatabgleich verwenden uns immer nur gute Handelsangebote vorschlagen sollten. ASF ist jedoch liberal und akzeptiert auch neutrale Handelsangebote, denn in diesen Handelsangeboten verlieren wir nicht wirklich etwas, so dass es keinen wirklichen Grund gibt sie abzulehnen. Dies ist besonders nützlich für deine Freunde, da sie deine überschüssigen Karten ohne STM tauschen können solange du keinen Set-Fortschritt verlierst.

Standardmäßig lehnt ASF schlechte Handelsangebote ab - das ist fast immer das, was du als Benutzer willst. Du kannst jedoch optional `MatchEverything` in deinen `TradingPreferences` aktivieren, um ASF dazu zu bringen alle Duplikat-Handelsangebote zu akzeptieren, einschließlich **schlechter Handelsangebote**. Dies ist nur dann nützlich, wenn du einen 1:1-Handels-Bot unter deinem Account betreiben möchtest, da du verstehst, dass **ASF dir nicht mehr helfen wird, Fortschritte bei der Vervollständigung von Abzeichen zu erzielen und dich anfällig für den Verlust des gesamten fertigen Sets für N Duplikate derselben Karte macht**. Wenn du nicht absichtlich einen Handels-Bot ausführen möchtest, der **nie** ein Set vervollständigt, willst du diese Option nicht aktivieren.

Unabhängig von den von dir gewählten `TradingPreferences` bedeutet ein von ASF abgelehntes Handelsangebot nicht, dass du es nicht selbst akzeptieren kannst. Wenn du den Standardwert von `BotBehaviour` beibehalten hast, der `RejectInvalidTrades` nicht enthält, ignoriert ASF diese Handelsangebote einfach - so kannst du selbst entscheiden, ob du an ihnen interessiert bist oder nicht. Gleiches gilt für Handelsangebote mit Gegenständen außerhalb von `MatchableTypes`, sowie für alles andere - das Modul soll dir helfen STM-Handelsangebote zu automatisieren, nicht zu entscheiden, was ein gutes Handelsangebot ist und was nicht. Die einzige Ausnahme von dieser Regel ist, wenn es um Benutzer geht die du vom Handelsmodul aus mit dem Befehl `bladd` auf die schwarze Liste gesetzt hast - Handelsangebote von diesen Benutzern werden unabhängig von den Einstellungen von `BotBehaviour` sofort abgelehnt.

Es wird dringend empfohlen, **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-de-DE)** zu verwenden, wenn du diese Option aktivierst, da diese Funktion ihr ganzes Potenzial verliert, wenn du dich dazu entscheidest jedes Handelsangebot manuell zu bestätigen. `SteamTradeMatcher` funktioniert auch ohne die Möglichkeit, Handelsangebote zu bestätigen, aber es könnte einen Rückstand an Bestätigungen erzeugen, wenn du sie nicht rechtzeitig akzeptierst.

* * *

### `MatchActively`

Die `MatchActively` Einstellung ist eine erweiterte Version von `SteamTradeMatcher`, die zusätzlich zum passiven Abgleich die von dieser Option angeboten wird, auch das aktive Abgleichen beinhaltet, bei dem der Bot Handelsangebote an andere Personen sendet.

Um von dieser Option Gebrauch zu machen musst du eine Reihe von Anforderungen erfüllen. Zuerst musst du `SteamTradeMatcher` aktivieren (da dieses Feature eine Erweiterung davon ist) und sicherstellen, dass du `MatchEverything` **deaktiviert** hast (da Handels-Bots nie aktiv abgleichen). Afterwards, you have to be eligible for our **[ASF STM listing](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics#current-privacy-policy)**, with a bit relaxed requirements. At the minimum you must have `Statistics` enabled, **[unrestricted](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663)** account, **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication#asf-2fa)** active and at least one valid type in `MatchableTypes`, such as trading cards.

If you meet all of the requirements above, ASF will periodically communicate with our **[public ASF STM listing](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics#public-asf-stm-listing)** in order to actively match bots that are currently available.

- Each matching is composed of "rounds", with up to `10` being a maximum in a single matching session.
- In jeder Runde holt ASF unser Inventar und das Inventar der ausgewählten Bots, die aufgelistet sind, um `MatchableTypes` Gegenstände zu finden, die zugeordnet werden können. Wenn die passende Übereinstimmung gefunden wird, sendet und bestätigt ASF das Handelsangebot automatisch.
- Each set (composition of appID, type and rarity of the item) can be matched in a single round only once. Dies ist implementiert, um "nicht mehr verfügbare Artikel" zu minimieren und zu vermeiden, dass man warten muss, bis jeder Bot reagiert bevor man alle Handelsangebote versendet. It's also the primary reason why matching is composed of rounds and not one ongoing process.
- ASF sendet nicht mehr als `255` Gegenstände in einem einzigen Handelsangebot, und nicht mehr als `5` Handelsangebote an einen einzelnen Benutzer in einer einzigen Runde. Dies wird durch Begrenzungen von Steam sowie durch unsere eigene Load-Balancing-Funktion vorgegeben.
- Matching round ends the moment we try to match a total of `40` bots, if not cancelled before due to running out of sets to match or excessive amount of empty matches.
- If last matching round resulted in at least a single trade being sent, next round starts within `5` minutes since the last one (to add some cooldown and allow all bots to react to our trades), otherwise matching session ends and repeats itself in `8` hours.

Dieses Modul soll transparent sein. Der Abgleich beginnt etwa `1` Stunde nach ASF-Start und wird jede `8` Stunden wiederholt (falls erforderlich). Die Funktion `MatchActively` ist als langfristige, periodische Maßnahme gedacht, um sicherzustellen, dass wir aktiv auf dem Weg zur Fertigstellung von Sets sind, aber ohne einen kurzfristigen Zeit- und Ressourcendruck, der auftreten würde, wenn dies als Befehl angeboten werden würde. The target users of this module are primary accounts and "stash" alt accounts, although it can be used by any bot that is not set to `MatchEverything`.

ASF wird sein Bestes tun, um die Anzahl der Anfragen und den Druck, die durch die Verwendung dieser Option erzeugt werden, zu minimieren und gleichzeitig die Effizienz des Abgleichens zu maximieren. The exact algorithm of choosing bots to match is ASF's implementation detail, but right now ASF will tend to favor bots with better diversity of games that their items are from, with `Any` bots further preferred.

`MatchActively` berücksichtigt Bots, die du vom Handel über den `bladd` **[Befehl](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-de-DE)** auf die schwarze Liste gesetzt hast und wird nicht versuchen, sie aktiv zu vergleichen. Dies kann verwendet werden, um ASF mitzuteilen, welche Bots nie verglichen werden sollen, auch wenn sie potenzielle Duplikate haben die wir verwenden können.
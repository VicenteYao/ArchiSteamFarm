# Échange

ASF inclut la prise en charge des échanges non interactifs (hors ligne) de Steam. La réception (acceptation / refus) ainsi que l’envoi de transactions sont disponibles immédiatement et ne nécessitent pas de configuration particulière, mais nécessitent évidemment un compte Steam sans restriction (Dépensé 5 $ dans le magasin). Le système d'échange n'est pas disponible pour les comptes restreints.

* * *

## Logique

ASF acceptera toujours tous les échanges, quels que soient les éléments, envoyés par les utilisateurs ayant un accès ` Master</ 0> (ou supérieur) au bot. Cela permet non seulement de loot facilement les cartes Steam acquises par le bot, mais également de gérer facilement les objets Steam stockés dans l'inventaire.</p>

<p>ASF rejettera l'offre d'échange, quel que soit le contenu, de tout utilisateur (non master) inscrit sur la liste noire à partir du système d'échange. La liste noire est stockée dans la base de données standard <code> BotName.db </ 0> et peut être gérée via les <1 > commandes</ 1> : <code> bl </ 0>, <code> bladd </ 0> et <code> blrm </ 0> . Cela devrait servir comme une alternative au blocage utilisateur standard offert par Steam - à utiliser avec prudence.</p>

<p>ASF acceptera toutes les transactions de type <code>loot</ 0> envoyées par des bots sauf si <code> DontAcceptBotTrades </ 0> est spécifié dans <code> TradingPreferences </ 0>.  En bref, <code> TradingPreferences</ 0> par défaut de <code>None </ 0> obligera ASF à accepter automatiquement les transactions de l'utilisateur disposant d'un accès <code> Master </ 0> au bot (expliqué ci-dessus). comme tous les dons se négocient à partir 
 de d'autres  bots qui participent au processus ASF. Si vous souhaitez désactiver les échanges de dons provenant d’autres bots, c’est à cela que sert <code> DontAcceptBotTrades </ 0> dans vos <code> TradingPreferences </ 0>.</p>

<p>Lorsque vous activez <code> AcceptDonations </ 0> dans vos <code> TradingPreferences </ 0>, ASF acceptera également toute donations, le compte bot ne perd aucun éléments. Cette propriété affecte uniquement les comptes sans bot, car les comptes bot sont affectés par <code> DontAcceptBotTrades </ 0>. <code> AcceptDonations </ 0> vous permet d’accepter facilement les dons d’autres personnes, ainsi que des bots ne participant pas au processus ASF.</p>

<p>Il est  appréciable de noter que <code> AcceptDonations </ 0> ne nécessite pas de <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication"> ASF 2FA </ 1>, car aucune confirmation n'est nécessaire si nous ne perdons aucun élément.</p>

<p>Vous pouvez également personnaliser davantage les capacités d'échange ASF en modifiant <code>TradingPreferences</ 0> en conséquence. L’une des principales caractéristiques de <code> TradingPreferences </ 0> est <code> SteamTradeMatcher </ 0>, qui obligera ASF à utiliser la logique intégrée pour accepter les transactions qui vous aident à compléter les badges non complet, ce qui est particulièrement utile en coopération avec la liste publique de <strong><a href="https://www.steamtradematcher.com"> SteamTradeMatcher </ 1>, mais peut également fonctionner sans ce dernier. Il est décrit plus en détail ci-dessous.</p>

<hr />

<h2><code>SteamTradeMatcher`</h2> 

Lorsque ` SteamTradeMatcher </ 0> est actif, ASF utilisera un algorithme assez complexe pour vérifier si le commerce respecte les règles du STM et est au moins neutre envers nous. La logique actuelle est la suivante:</p>

<ul>
<li>Rejetez la transaction si nous perdons autre chose que les types d’articles spécifiés dans notre <code>MatchableTypes`. </li> 

- Rejetez la transaction si nous ne recevons pas au moins le même nombre d’objets par jeu et par type.
- Rejetez la transaction si l'utilisateur demande des cartes spéciales Soldes été / hiver de Steam et si cette transaction est suspendue.
- Rejetez la transaction si la durée de la suspension dépasse ` la propriété de configuration globale <0>MaxTradeHoldDuration </ 0>.</li>
<li>Rejetez la transaction si nous n'avons pas <code> MatchEverything </ 0>, et c'est pire que neutre pour nous.</li>
<li>Acceptez le commerce si nous ne le rejetons pas par l’un des points ci-dessus.</li>
</ul>

<p>Il est intéressant de noter qu'ASF prend également en charge les supplément - la logique fonctionnera correctement lorsque l'utilisateur ajoutera quelque chose de plus au commerce, à condition que toutes les conditions ci-dessus soient remplies.</p>

<p>Les 4 premiers motifs de rejet devraient être évidents pour tout le monde. Le dernier comprend la logique des  doubles qui vérifie l’état actuel de nos stocks et décide de l’état du commerce.</p>

<ul>
<li>Le commerce est <strong> bon </ 0> si nos progrès vers l’achèvement fixé progressent. A A (avant)  <-> A B (après)</li>
<li>Le commerce est <strong> bon </ 0> si nos progrès vers l’achèvement fixé progressent. A B (avant)  <->  A C (après)</li>
<li>Le commerce est <strong> mauvais </ 0> si nos progrès vers l’achèvement des objectifs fixés diminuent. A C (avant)  <->  A A(après)</li>
</ul>

<p>STM ne fonctionne que sur des offres correct, ce qui signifie que l'utilisateur qui utilise STM pour les doubles devrait toujours suggérer que des offres correct nous. Cependant, ASF est  indulgent et accepte également les transactions neutres, car dans ces transactions, nous ne perdons rien, il n’y a donc aucune raison de les refuser. Ceci est particulièrement utile pour vos amis, car ils peuvent échanger vos cartes en trop sans utiliser la technologie STM, tant que vous ne perdez pas la progression définie.</p>

<p>Par défaut, ASF rejettera les mauvaises transactions - c’est généralement ce que vous recherchez en tant qu’utilisateur. Cependant, vous pouvez éventuellement activer <code> MatchEverything </ 0> dans vos <code> TradingPreferences </ 0> afin de permettre à ASF d'accepter tous les échanges doubles, y compris <strong> les mauvais </ 1>. Cela n’est utile que si vous souhaitez exécuter un bot commercial 1: 1 sous votre compte, car vous comprenez que <strong> ASF ne vous aidera plus à progresser vers la réalisation de vos badges et cela rend susceptible la possibilité de perdre toute votre set fini pour un certains nombres de doubles de la même carte </ 0>. À moins que vous ne vouliez délibérément exécuter un bot commercial qui est <strong> jamais </ 0> censé terminer un ensemble, vous ne souhaitez pas activer cette option.</p>

<p>Quels que soient les <code> TradingPreferences que vous avez choisies </ 0>, une transaction rejetée par ASF ne signifie pas que vous ne pouvez l’accepter vous-même. If you kept default value of <code>BotBehaviour`, which doesn't include `RejectInvalidTrades`, ASF will just ignore those trades - allowing you to decide yourself if you're interested in them or not. Il en va de même pour les transactions avec des éléments en dehors de ` MatchableTypes </ 0>, ainsi que pour tout le reste - le module est censé vous aider à automatiser les transactions STM, sans décider de ce qui est une bonne transaction ou non. La seule exception à cette règle concerne les utilisateurs que vous avez inscrits sur la liste noire du module d'échange à l'aide de la commande <code> bladd </ 0>. Les transactions de ces utilisateurs sont immédiatement rejetées, quels que soient les paramètres <code> BotBehaviour </ 0>.</p>

<p>Il est vivement recommandé d'utiliser <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication"> ASF 2FA </ 0> lorsque vous activez cette option, car cette fonction perd tout son potentiel si vous décidez de confirmer manuellement chaque transaction. <code>SteamTradeMatcher` will work properly even without ability to confirm trades, but it can generate backlog of confirmations if you're not accepting them in time.</p> 
    * * *
    
    ### `MatchActively`
    
    `MatchActively` setting is extended version of `SteamTradeMatcher` which in addition to passive matching offered by that option, also includes active matching in which the bot will send trades to other people.
    
    In order to make use of that option, you have a set of requirements to meet. Firstly, you need to enable `SteamTradeMatcher` (as this feature is extension of that), and ensure that you have `MatchEverything` **disabled** (as trading bots never match actively). Afterwards, you have to be eligible for our **[ASF STM listing](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics#current-privacy-policy)**, with a bit relaxed requirements. At the minimum you must have `Statistics` enabled, **[unrestricted](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663)** account, **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication#asf-2fa)** active and at least one valid type in `MatchableTypes`, such as trading cards.
    
    If you meet all of the requirements above, ASF will periodically communicate with our **[public ASF STM listing](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics#public-asf-stm-listing)** in order to actively match bots that are currently available.
    
    - Each matching is composed of "rounds", with up to `10` being a maximum in a single matching session.
    - In each round ASF will fetch our inventory and inventory of selected bots that are listed in order to find `MatchableTypes` items that can be matched. If match is found, ASF will send and confirm trade offer automatically.
    - Each set (composition of appID, type and rarity of the item) can be matched in a single round only once. This is implemented in order to minimize "items no longer available" and avoid a need to wait for each bot to react before sending all the trades. It's also the primary reason why matching is composed of rounds and not one ongoing process.
    - ASF will send no more than `255` items in a single trade, and no more than `5` trades to a single user in a single round. This is imposed by Steam limits, as well as our own load-balancing.
    - Matching round ends the moment we try to match a total of `40` bots, if not cancelled before due to running out of sets to match or excessive amount of empty matches.
    - If last matching round resulted in at least a single trade being sent, next round starts within `5` minutes since the last one (to add some cooldown and allow all bots to react to our trades), otherwise matching session ends and repeats itself in `8` hours.
    
    This module is supposed to be transparent. Matching will start in approximately `1` hour since ASF start, and will repeat each `8` hours (if needed). `MatchActively` feature is aimed to be used as a long-run, periodical measure to ensure that we're actively heading towards sets completion, but without a short-term time and resources pressure that would happen if this was offered as a command. The target users of this module are primary accounts and "stash" alt accounts, although it can be used by any bot that is not set to `MatchEverything`.
    
    ASF will do its best to minimize the amount of requests and pressure generated by using this option, while at the same time maximizing efficiency of matching to the upper limit. The exact algorithm of choosing bots to match is ASF's implementation detail, but right now ASF will tend to favor bots with better diversity of games that their items are from, with `Any` bots further preferred.
    
    `MatchActively` takes into account bots that you blacklisted from trading through `bladd` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** and will not attempt to actively match them. This can be used for telling ASF which bots it should never match, even if they'd have potential dupes for us to use.
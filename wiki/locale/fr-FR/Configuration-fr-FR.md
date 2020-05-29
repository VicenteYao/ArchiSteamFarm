# Configuration

Cette page est dédiée à la configuration ASF. Cette documentation complète est pour le répertoire `config`. Vous pouvez ainsi adapter ASF à vos besoins.

- **[Introduction](#introduction)**
- **[Panel Web de configuration](#web-based-configgenerator)**
- **[ Configuration Manuelle](#manual-configuration)**
- **[Configuration globale](#global-config)**
- **[Configuration Bot](#bot-config)**
- **[Structures des répertoires](#file-structure)**
- **[JSON mapping](#json-mapping)**
- **[Mode de compatibilité (mapping)](#compatibility-mapping)**
- **[Compatibilité des configurations](#configs-compatibility)**
- **[Auto-rechargement ](#auto-reload)**

* * *

## Introduction

La configuration ASF est séparé en deux parties principales : la configuration globale (processus) et la configuration de chaque bot. Chaque bot dispose de son propre fichier de configuration nommé `BotName.json ` ( `BotName ` est le nom du bot). La que la configuration globale ASF (processus) est un fichier unique nommé `ASF.json `.

Un bot est un compte unique qui participe au processus ASF. Pour fonctionner correctement, ASF a besoin d’au moins **une** instance de bot définie. Il n'y a pas de limite d'instances de bot imposée par le processus, vous pouvez donc utiliser autant de bots (comptes steam) que vous le souhaitez.

ASF utilise le format **[JSON](https://en.wikipedia.org/wiki/JSON)** pour stocker ses fichiers de configuration. C'est un format convivial, lisible et très universel dans lequel vous pouvez configurer le programme. Ne vous inquiétez pas, vous n'avez pas besoin de connaître JSON pour configurer ASF. Je viens de le mentionner au cas où vous voudriez déjà créer en masse des configurations ASF avec une sorte de script bash.

La configuration peut être effectuée manuellement - en créant les configurations JSON appropriées ou en utilisant notre **[Panel Web de configuration](https://justarchinet.github.io/ASF-WebConfigGenerator)**, ce qui devrait être beaucoup plus simple et pratique. À moins que vous ne soyez un utilisateur expérimenté, je suggère d'utiliser le générateur de configuration, qui sera décrit ci-dessous.

* * *

## Panel Web de configuration

Le but de **[web-based ConfigGenerator](https://justarchinet.github.io/ASF-WebConfigGenerator)** est de vous fournir une interface conviviale qui est utilisée pour générer des fichiers de configuration ASF. Le Panel Web de configuration est 100% basé sur le client, ce qui signifie que les détails que vous saisissez ne sont envoyés nulle part, mais traités localement uniquement. Cela garantit la sécurité et la fiabilité, car cela peut même fonctionner **[hors connexion](https://github.com/JustArchiNET/ASF-WebConfigGenerator/tree/master/docs)** si vous souhaitez télécharger tous les fichiers et exécuter `index.html` dans votre navigateur préféré.

Le Panel Web de configuration est vérifié pour fonctionner correctement sur Chrome et Firefox, mais il devrait fonctionner correctement avec la plupart des navigateurs les plus populaires comptabile avec javascript.

L’utilisation est assez simple - indiquez si vous souhaitez générer la configuration `ASF` ou `Bot` en basculant sur l’onglet approprié, assurez-vous que la version choisie du fichier de configuration correspond à votre version ASF, puis saisissez tous les détails et cliquez sur le bouton "télécharger". Déplacez ce fichier dans le répertoire ASF `config`, en écrasant les fichiers existants si nécessaire. Répétez cette procédure pour toutes les modifications ultérieures éventuelles et reportez-vous au reste de cette section pour obtenir une explication de toutes les options disponibles pour la configuration.

* * *

## Configuration Manuelle

Je recommande fortement d'utiliser ConfigGenerator sur le Web, mais si, pour une raison quelconque, vous ne le souhaitez pas, vous pouvez également créer les configurations appropriées vous-même. Check JSON examples below for a good start in proper structure, you can copy the content into a file and use it as a base for your config. Since you're not using our frontend, ensure that your config is **[valid](https://jsonlint.com)**, as ASF will refuse to load it if it can't be parsed. Pour connaître la structure JSON appropriée avec tous les champs disponibles, reportez-vous à la section **[Mappage JSON](#json-mapping)** et à la documentation ci-dessous.

* * *

## Configuration globale

La configuration globale se trouve dans le fichier ASF.json et se présente dans la structure suivante:

```json
{
    "AutoRestart": true,
    "Blacklist": [],
    "CommandPrefix": "!",
    "ConfirmationsLimiterDelay": 10,
    "ConnectionTimeout": 90,
    "CurrentCulture": null,
    "Debug": false,
    "FarmingDelay": 15,
    "GiftsLimiterDelay": 1,
    "Headless": false,
    "IdleFarmingPeriod": 8,
    "InventoryLimiterDelay": 3,
    "IPC": false,
    "IPCPassword": null,
    "LoginLimiterDelay": 10,
    "MaxFarmingTime": 10,
    "MaxTradeHoldDuration": 15,
    "OptimizationMode": 0,
    "Statistics": true,
    "SteamMessagePrefix": "/me ",
    "SteamOwnerID": 0,
    "SteamProtocols": 7,
    "UpdateChannel": 1,
    "UpdatePeriod": 24,
    "WebLimiterDelay": 300,
    "WebProxy": null,
    "WebProxyPassword": null,
    "WebProxyUsername": null
}
```

* * *

Toutes les options sont expliquées ci-dessous :

### `AutoRestart`

`bool` avec la valeur par défaut `true</ 0>. Cette fonctionnalité définit si ASF est autorisé à effectuer un redémarrage automatique en cas de besoin. Quelques événements nécessiteront un redémarrage automatique de la part d'ASF, tels que la mise à jour d'ASF (effectuée avec la commande <code>UpdatePeriod` ou `update`), ainsi que la `ASF. json` config edit, `restart` et de même. En règle générale, le redémarrage comprend deux parties: créer un nouveau processus et terminer le processus en cours. La plupart des utilisateurs devraient accepter cette option et conserver cette fonction avec la valeur par défaut `true`. Toutefois, si vous exécutez ASF avec votre propre script et / ou avec `dotnet`, vous voudrez peut-être avoir le contrôle total sur le démarrage du processus et éviter une situation telle que l'exécution d'un nouveau processus ASF (redémarré) quelque part en silence en arrière-plan et non au premier plan du script, qui s'est terminé avec l'ancien processus ASF. Ceci est particulièrement important compte tenu du fait que le nouveau processus ne sera plus votre fonction direct, ce qui vous rendrait incapable, par exemple d'utiliser une entrée de console standard pour cela.

Si tel est le cas, cette fonction est spécialement conçue pour vous et vous pouvez la définir sur `false</ 0>. Cependant, gardez à l'esprit que dans ce cas, <strong>vous</strong> êtes responsable du redémarrage du processus. C’est important car ASF se ferme uniquement au lieu de créer un nouveau processus (par exemple, après la mise à jour). Ainsi, si aucune logique n’est ajoutée par vous, il cessera tout simplement de fonctionner jusqu’à ce que vous le redémarriez. ASF se ferme toujours avec le code d'erreur correct indiquant succès (zéro) ou non succès (différent de zéro). Vous pouvez ainsi ajouter une logique appropriée dans votre script, ce qui devrait éviter le redémarrage automatique d'ASF en cas d'échec, ou du moins créez une copie locale de <code>log.txt` pour une analyse plus approfondie. Notez également que la commande `restart` redémarre toujours ASF, quel que soit le mode de définition de cette fonction, car cette fonction définit le comportement par défaut, tandis que la commande `restart` redémarre toujours le processus. Sauf si vous avez une raison de désactiver cette fonctionnalité, vous devez la laisser activée.

* * *

### `Liste noire`

`ImmutableHashSet<uint>` avec la valeur par défaut étant vide. Comme son nom l'indique, cette propriété de configuration globale définit des appID (jeux) qui seront entièrement ignorés par le processus automatique de farming ASF. Malheureusement, Steam adore désigner les badges de soldes d’été / hiver comme étant "disponibles pour des cartes à obtenir", ce qui induit en erreur le processus ASF en lui faisant croire que c’est un jeu valable qui mérite d’être cultivé. S'il n'y avait aucune sorte de liste noire, ASF finirait par "bloquer" le farming d'un jeu qui n'est en fait pas un jeu, et attendrait infiniment que les cartes tombent. La liste noire ASF a pour but de marquer ces badges comme étant non disponibles pour le farming. Nous pouvons donc les ignorer en silence lorsque nous décidons d’agir, et ne pas tomber dans le piège.

ASF inclut deux listes noires par défaut - `GlobalBlacklist`, qui est codé en dur dans le code de ASF et pas possible de l'éditer et une `liste noire` normal, qui est défini ici. `GlobalBlacklist` est mis à jour avec la version de ASF et comprend généralement tous les « mauvais » AppID au moment de sa sortie, si vous utilisez ASF à jour vous n’avez pas besoin de maintenir votre propre `liste noire` défini ici. Le but principal de cette propriété est de vous permettre de mettre en liste noire de nouveaux identificateurs d’application non connus au moment de la publication d'une nouvelle version ASF, qui ne doivent pas être mis en farming. Codé en dur `GlobalBlacklist` est mis à jour aussi vite que possible, donc vous n’êtes pas tenu de mettre à jour votre propre `liste noire` si vous utilisez la dernière version de l’ASF, mais sans `liste noire` vous seraient obligés de mettre à jour ASF afin de « continuer à fonctionner » lorsque Steam libère des nouveaux badges - je ne veux pas vous forcer à utiliser le dernier code ASF, par conséquent, cette propriété est ici pour vous permettent de « fixer » ASF vous-même si pour une raison quelconque vous ne voulez pas ou ne pouvez pas, mettre à jour à nouveau en `GlobalBlacklist` dur dans la nouvelle version de ASF , mais vous voulez garder votre ancienne version d’ASF. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

Si vous recherchez plutôt une liste noire basée sur les bots, jetez un coup d'œil à `ib`, `ibadd` et `ibrm`</a></strong> .

* * *

### `CommandPrefix`

`chaîne` avec la valeur par défaut `!`. Cette fonction spécifie **le préfixe** sensible à la case utilisé pour les **[commandes](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** ASF. En d’autres termes, c’est ce que vous devez ajouter à chaque commande ASF pour que ASF vous écoute. Il est possible de définir cette valeur sur `null` ou sur une valeur vide pour que ASF n’utilise aucun préfixe de commande. Dans ce cas, vous devez entrer des commandes avec leurs identificateurs simples. Cela risque toutefois de réduire les performances d'ASF, car ASF est optimisé pour ne plus analyser le message s'il ne commence pas par `CommandPrefix` - si vous décidez intentionnellement de ne pas l'utiliser, ASF sera forcé de tout lire. messages et y répondre, même s’ils ne sont pas des commandes ASF. Par conséquent, il est recommandé de continuer à utiliser des `CommandPrefix`, tels que `/` si vous n'aimez pas la valeur par défaut de `!`. Par souci de cohérence, `CommandPrefix` affecte l’ensemble du processus ASF. Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `ConfirmationsLimiterDelay`

`byte` avec la valeur par défaut `10`. ASF will ensure that there will be at least `ConfirmationsLimiterDelay` seconds in between of two consecutive 2FA confirmations fetching requests to avoid triggering rate-limit - those are being used by **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)** during e.g. `2faok` command, as well as on as-needed basis when performing various trading-related operations. La valeur par défaut a été définie en fonction de nos tests et ne doit pas être réduite si vous ne voulez pas rencontrer de problèmes. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `ConnectionTimeout`

`byte` avec la valeur par défaut `90`. Cette propriété définit les délais d'attente pour diverses actions réseau effectuées par ASF, en secondes. En particulier, `ConnectionTimeout` définit le délai d'expiration en secondes pour les demandes HTTP et IPC, `ConnectionTimeout / 10 ` définit le nombre maximal de pulsations manquées, tandis que `ConnectionTimeout / 30 ` définit le nombre de minutes que nous autorisons pour la demande initiale de connexion au réseau Steam. La valeur par défaut de `90` devrait convenir à la majorité des gens. Toutefois, si vous avez une connexion réseau ou un PC plutôt lent, vous souhaiterez peut-être augmenter ce nombre (par exemple, `120</0 >). Gardez à l'esprit que de plus grandes valeurs ne résoudront pas comme par magie des serveurs Steam lents ou même inaccessibles. Nous ne devrions donc pas attendre indéfiniment quelque chose qui ne se produira pas et simplement réessayer ultérieurement. Si vous définissez cette valeur sur une valeur trop élevée, cela entraînera un retard excessif dans la résolution des problèmes de réseau, ainsi que dans une diminution des performances globales. Si vous définissez cette valeur sur une valeur trop basse, la stabilité et les performances globales seront également réduites, car ASF abandonnera les requêtes valides en cours d'analyse. Par conséquent, définir cette valeur sur une valeur inférieure à celle par défaut ne présente généralement aucun avantage, car les serveurs Steam ont tendance à être lents de temps en temps et peuvent nécessiter plus de temps pour l'analyse des demandes ASF. La valeur par défaut est un équilibre entre croire que notre connexion réseau est stable et douter du réseau Steam pour traiter notre demande dans un délai indiqué. Si vous souhaitez détecter les problèmes plus rapidement et permettre à ASF de se reconnecter / répondre plus rapidement, la valeur par défaut convient (ou très légèrement en dessous, par exemple <code>60`, rendant ASF moins patient). Si vous remarquez au contraire qu'ASF a des problèmes de réseau, tels que des requêtes en échec, la perte de pulsations ou la connexion à Steam interrompue, il peut être judicieux d'augmenter cette valeur si vous êtes sûr que cela n'est **pas** via votre réseau, mais par Steam lui-même, à mesure que les délais dépassent, ASF devient plus "patient" et ne décide pas de se reconnecter immédiatement.

An example situation that may require increase of this property is letting ASF to deal with a very huge trade offers that can take good 2+ minutes to be fully accepted and handled by Steam. By increasing default timeout, ASF will be more patient and wait longer before deciding that the trade is not going through and abandon the initial request.

Another situation could be caused by very slow machine or internet connection that requires more time to handle the data being transmitted. This is pretty rare condition, as the CPU/network bandwidth is almost never a bottleneck, but still a possibility worth mentioning.

En bref, la valeur par défaut devrait être convenable dans la plupart des cas, mais vous pouvez l’augmenter si nécessaire. Néanmoins, aller au-delà de la valeur par défaut n’a pas de sens, car des délais plus longs ne résoudront pas comme par magie des serveurs Steam inaccessibles. Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `CurrentCulture`

`chaîne` avec la valeur par défaut `null`. Par défaut, ASF tente d'utiliser la langue de votre système d'exploitation et préférera utiliser les chaînes traduites dans cette langue si elles sont disponibles. Ceci est possible grâce à notre communauté qui tente de **[traduire](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Localization)** ASF dans toutes les langues les plus populaires. Si, pour une raison quelconque, vous ne souhaitez pas utiliser la langue native de votre système d'exploitation, vous pouvez utiliser cette propriété de configuration pour sélectionner une langue valide que vous souhaitez utiliser. Pour obtenir une liste de toutes les langues disponibles, visitez **[MSDN](https://msdn.microsoft.com/en-us/library/cc233982.aspx)** et recherchez `Langage tag`. Il est intéressant de noter qu'ASF accepte les deux langues spécifiques, telles que `en-GB`, mais également les langues neutres, telles que `en`. La spécification de la langue actuelle peut également affecter d'autres comportements spécifiques à la langues, tels que le format de date/heure et autres. Notez que vous aurez peut-être besoin de packs de polices / langues supplémentaires pour afficher correctement les caractères spécifiques à une langue, si vous avez sélectionné une langue non native qui les utilise. Généralement, vous souhaitez utiliser cette fonction de configuration si vous préférez ASF en anglais plutôt que dans votre langue maternelle.

* * *

### `Debug`

`bool` avec la valeur par défaut `true</ 0>. Cette propriété définit si le processus doit être exécuté en mode debug. En mode débogage, ASF crée un répertoire spécial <code>debug` à côté de la `config`, qui assure le suivi de l'ensemble de la communication entre les serveurs ASF et Steam. Les informations de debug peuvent aider à détecter des problèmes épineux liés à la mise en réseau et au flux de travail ASF général. En outre, certaines routines de programme seront beaucoup plus détaillées, telles que `WebBrowser` indiquant la raison exacte pour laquelle certaines demandes échouent: ces entrées sont écrites dans le journal ASF normal. **Vous ne devez pas exécuter ASF en mode debug, à moins que le développeur ne vous le demande**. L'exécution d'ASF en mode debug **diminue les performances**, **affecte négativement la stabilité** et est **beaucoup plus détaillé à divers endroits**. Il doit donc être utilisé **uniquement** intentionnellement, à court terme, pour debug un problème particulier, reproduire le problème ou obtenir plus d’informations sur une demande en échec, mais de la même manière, mais **pas** pour l’exécution normale du programme. Vous verrez **beaucoup** de nouvelles erreurs, problèmes et exceptions - assurez-vous d'avoir une bonne connaissance d'ASF, de Steam et de ses bizarreries si vous décidez d'analyser tout cela vous-même.

**AVERTISSEMENT:** l'activation de ce mode inclut la journalisation d'informations **potentiellement sensibles** telles que les identifiants de connexion et les mots de passe que vous utilisez pour vous connecter à Steam (en raison de la journalisation réseau). Ces données sont écrites dans le répertoire `debug`, ainsi que dans le fichier standard `log.txt` (qui est maintenant beaucoup plus détaillé pour consigner ces informations). Vous ne devez pas publier de contenu de debug généré par ASF dans un emplacement public, le développeur ASF doit toujours vous rappeler de l'envoyer à son adresse e-mail ou à un autre emplacement sécurisé. Nous ne stockons pas, ni n'utilisons ces détails sensibles, ils sont écrits dans le cadre de routines de debug, car leur présence peut être pertinente pour le problème qui vous concerne. Nous préférerions ne pas modifier la journalisation ASF de quelque manière que ce soit, mais si vous êtes inquiet, vous êtes libre de rédiger ces informations sensibles de manière appropriée.

> La rédaction implique le remplacement de détails sensibles, par exemple par des étoiles. Vous devez vous abstenir de supprimer entièrement les lignes sensibles, car leur existence pure pourrait être pertinente et devrait être préservée.

* * *

### `FarmingDelay`

`byte` avec la valeur par défaut `15`. Pour que ASF fonctionne, il vérifiera la partie actuellement exploitée toutes les `FarmingDelay`, s'il a peut-être déjà obtenus toutes les cartes. Si vous définissez cette fonction sur une valeur trop basse, le nombre de demandes de Steam envoyées sera excessif. Si vous la définissez trop haut, ASF continuera à "farmer" le titre en cours jusqu'à `FarmingDelay` minutes après la fin de son exploitation. La valeur par défaut devrait être excellente pour la plupart des utilisateurs, mais si vous avez beaucoup de bots en cours d'exécution, vous pouvez envisager de l'augmenter à quelque chose comme ` 30 ` minutes afin de limiter les demandes d'envoi envoyées. Il est agréable de noter que ASF utilise un mécanisme basé sur les événements et vérifie la page de badge du jeu sur chaque objet Steam obtenu. Par conséquent, en général **nous n’avons même pas besoin de le vérifier à des intervalles de temps fixes**. Ne faites pas entièrement confiance au réseau Steam - de toute façon, nous vérifions la page des badges de jeu. Si nous ne l’avons pas vérifiée au cours du dernier `FarmingDelay` (dans le cas où le réseau Steam ne nous informerait pas de l’élément obtenu ou des trucs comme ça). En supposant que le réseau Steam fonctionne correctement, diminuer cette valeur **n'améliorera en aucun cas l'efficacité du farming**, tandis que **augmentera considérablement la surcharge du réseau** - il est recommandé de l'augmenter uniquement (si nécessaire). par défaut de `15` minutes. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `GiftsLimiterDelay`

`byte` avec la valeur par défaut `1`. ASF veillera à ce qu'il y ait au moins `GiftsLimiterDelay` deux secondes entre deux demandes consécutives de traitement cadeau / clé / licence (utilisation) afin d'éviter de déclencher une limite de débit. En plus de cela, il sera également utilisé comme limiteur global pour les demandes de liste de jeux, tel que celui émis par la **[commande](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** `owns<0>. À moins que vous n'ayez une raison <strong>forte</strong> de modifier cette fonction, vous devez la conserver par défaut.</p>

<hr />

<h3><code>Headless`</h3> 

`bool` avec la valeur par défaut `true</ 0>. Cette propriété définit si le processus doit être exécuté en mode headless. When in headless mode, ASF assumes that it's running on a server or in other non-interactive environment, therefore it will not attempt to read any information through console input. This includes on-demand details (account credentials such as 2FA code, SteamGuard code, password or any other variable required for ASF to operate) as well as all other console inputs (such as interactive command console). In other words, <code>Headless` mode is equal to making ASF console read-only. This setting is useful mainly for users running ASF on their servers, as instead of asking e.g. for 2FA code, ASF will silently abort the operation by stopping an account. Unless you're running ASF on a server, and you previously confirmed that ASF is able to operate in non-headless mode, you should keep this property disabled. Any user interaction will be denied when in headless mode, and your accounts will not run if they require **any** console input during starting. This is useful for servers, as ASF can abort trying to log onto the account when asked for credentials, instead of waiting (infinitely) for user to provide those. Enabling this mode will also allow you to use `input` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** which acts as a replacement for standard console input. Si vous n'êtes pas sur du réglage de cette propriété, laissez-la à la valeur par défaut de `false`.

If you're running ASF on the server, you probably want to use this option together with `--process-required` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**.

* * *

### `IdleFarmingPeriod`

`byte` avec la valeur par défaut `8`. When ASF has nothing to farm, it will periodically check every `IdleFarmingPeriod` hours if perhaps account got some new games to farm. This feature is not needed when talking about new games we're getting, as ASF is smart enough to automatically check badge pages in this case. `IdleFarmingPeriod` is mainly for situations such as old games we already have having trading cards added. In this case there is no event, so ASF has to periodically check badge pages if we want to have this covered. Value of `0` disables this feature. Also check: `ShutdownOnFarmingFinished`.

* * *

### `InventoryLimiterDelay`

`byte` avec la valeur par défaut `3`. ASF will ensure that there will be at least `InventoryLimiterDelay` seconds in between of two consecutive inventory requests to avoid triggering rate-limit - those are being used for fetching Steam inventories, especially during your own commands such as `transfer`, as well as in features like `MatchActively`. Default value of `3` was set based on fetching inventories of over 100 consecutive bot instances, and should satisfy most (if not all) of the users. You may however want to decrease it, or even change to `0` if you have very low amount of bots, so ASF will ignore the delay and loot steam inventories much faster. Be warned though, as setting it too low **will** result in Steam temporarily banning your IP, and that will prevent you from fetching your inventory at all. You also may need to increase this value if you're running a lot of bots with a lot of inventory requests, although in this case you should probably try to limit number of those requests instead. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `IPC`

`bool` avec la valeur par défaut `true</ 0>. Cette fonction définit si le serveur <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC">IPC</a></strong> d'ASF doit démarrer en même temps que le processus. IPC permet une communication inter-processus en démarrant un serveur HTTP local. Si vous n'allez pas utiliser le serveur IPC d'ASF, vous n'avez alors aucune raison d'activer cette option.</p>

<hr />

<h3><code>IPCPassword`</h3> 

`chaîne` avec la valeur par défaut `null`. This property defines mandatory password for every API call done via IPC and serves as an extra security measure. When set to non-empty value, all IPC requests will require extra `password` property set to the password specified here. Default value of `null` will skip a need of the password, making ASF respect all queries. In addition to that, enabling this option also enables built-in IPC anti-bruteforce mechanism which will temporarily ban given `IPAddress` after sending too many unauthorized requests in a very short time. Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `LoginLimiterDelay`

`byte` avec la valeur par défaut `10`. ASF veillera à ce qu'il y ait au moins `LoginLimiterDelay` deux secondes entre deux demandes successives de tentatives de connexion, afin d'éviter de déclencher une limite de débit. Default value of `10` was set based on connecting over 100 bot instances, and should satisfy most (if not all) of the users. You may however want to increase/decrease it, or even change to `0` if you have very low amount of bots, so ASF will ignore the delay and connect to Steam much faster. Be warned though, as setting it too low while having too many bots **will** result in Steam temporarily banning your IP, and that will prevent you from logging in **at all**, with `InvalidPassword/RateLimitExceeded` error - and that also includes your normal Steam client, not only ASF. Likewise, if you're running excessive number of bots, especially together with other Steam clients/tools using the same IP address, most likely you'll need to increase this value in order to spread logins across longer period of time.

As a side note, this value is also used as load-balancing buffer in all ASF-scheduled actions, such as trades in `SendTradePeriod`. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `MaxFarmingTime`

`byte` avec la valeur par défaut `10`. As you should know, Steam is not always working properly, sometimes weird situations can happen such as steam not being recording our playtime, despite of in fact playing a game. ASF will allow farming a single game in solo mode for maximum of `MaxFarmingTime` hours, and consider it fully farmed after that period. This is required to not freeze farming process in case of weird situations happening, but also if for some reason Steam released a new badge that would stop ASF from progressing further (see: `Blacklist`). Default value of `10` hours should be enough for dropping all steam cards from one game. Setting this property too low can result in valid games being skipped (and yes, there are valid games taking even up to 9 hours to farm), while setting it too high can result in farming process being frozen. Please note that this property affects only a single game in a single farming session (so after going through entire queue ASF will return to that title), also it's not based on total playtime but internal ASF farming time, so ASF will also return to that title after a restart. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `MaxTradeHoldDuration`

`byte` avec la valeur par défaut `15`. This property defines maximum duration of trade hold in days that we're willing to accept - ASF will reject trades that are being held for more than `MaxTradeHoldDuration` days, as defined in **[trading](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading)** section. This option makes sense only for bots with `TradingPreferences` of `SteamTradeMatcher`, as it doesn't affect `Master`/`SteamOwnerID` trades, neither donations. Trade holds are annoying for everyone, and nobody really wants to deal with them. ASF is supposed to work on liberal rules and help everyone, regardless if on trade hold or not - that's why this option is set to `15` by default. However, if you'd instead prefer to reject all trades affected by trade holds, you can specify `0` here. Please consider the fact that cards with short lifespan are not affected by this option and automatically rejected for people with trade holds, as described in **[trading](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading)** section, so there is no need to globally reject everybody only because of that. Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `OptimizationMode`

`byte` avec la valeur par défaut `0`. This property defines optimization mode which ASF will prefer during runtime. Currently ASF supports two modes - `0` which is called `MaxPerformance`, and `1` which is called `MinMemoryUsage`. By default ASF prefers to run as many things in parallel (concurrently) as possible, which enhances performance by load-balancing work across all CPU cores, multiple CPU threads, multiple sockets and multiple threadpool tasks. For example, ASF will ask for your first badge page when checking for games to idle, and then once request arrived, ASF will read from it how many badge pages you actually have, then request each other one concurrently. This is what you should want **almost always**, as the overhead in most cases is minimal and benefits from asynchronous ASF code can be seen even on the oldest hardware with a single CPU core and heavily limited power. However, with many tasks being processed in parallel, ASF runtime is responsible for their maintenance, e.g. keeping sockets open, threads alive and tasks being processed, which can result in increased memory usage from time to time, and if you're extremely constrained by available memory, you may want to switch this property to `1` (`MinMemoryUsage`) in order to force ASF into using as little tasks as possible, and typically running possible-to-parallel asynchronous code in a synchronous manner. You should consider switching this property only if you previously read **[low-memory setup](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup)** and you intentionally want to sacrifice gigantic performance boost, for a very small memory overhead decrease. Usually this option is **much worse** than what you can achieve with other possible ways, such as by limiting your ASF usage or tuning runtime's garbage collector, as explained in **[low-memory setup](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup)**. Therefore, you should use `MinMemoryUsage` as a **last resort**, right before runtime recompilation, if you couldn't achieve satisfying results with other (much better) options. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `Statistiques`

`bool` avec la valeur par défaut `true</ 0>. This property defines if ASF should have statistics enabled. Vous trouverez des explications détaillées dans la section <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics">statistiques</a></strong>. Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.</p>

<hr />

<h3><code>SteamMessagePrefix`</h3> 

`chaîne` avec la valeur par défaut `/me`. This property defines a prefix that will be prepended to all Steam messages being sent by ASF. By default ASF uses `"/me "` prefix in order to distinguish bot messages more easily by showing them in different color on Steam chat. Another worthy mention is `"/pre "` prefix which achieves similar result, but uses different formatting. You can also set this property to empty string or `null` in order to disable using prefix entirely and output all ASF messages in a traditional way. It's nice to note that this property affects Steam messages only - responses returned through other channels (such as IPC) are not affected. Unless you want to customize standard ASF behaviour, it's a good idea to leave it at default.

* * *

### `SteamOwnerID
`

`ulong` avec la valeur par défaut `0`. This property defines Steam ID in 64-bit form of ASF process owner, and is very similar to `Master` permission of given bot instance (but global instead). You almost always want to set this property to ID of your own main Steam account. `Master` permission includes full control over his bot instance, but global commands such as `exit`, `restart` or `update` are reserved for `SteamOwnerID` only. This is convenient, as you may want to run bots for your friends, while not allowing them to control ASF process, such as exiting it via `exit` command. Default value of `0` specifies that there is no owner of ASF process, which means that nobody will be able to issue global ASF commands. Keep in mind that **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC)** commands work with `SteamOwnerID`, so if you want to use them, then you must provide a valid value here.

* * *

### `SteamProtocols`

`byte flags` avec la valeur par défaut `7`. Cette fonction définit les protocoles Steam utilisés par ASF lors de la connexion aux serveurs Steam, définis comme suit:

| Valeur  | Nom        | Description                                                                                              |
| ------- | ---------- | -------------------------------------------------------------------------------------------------------- |
| 0       | None       | Pas de protocole                                                                                         |
| 1       | TCP        | **[Protocole de contrôle de transmission](https://en.wikipedia.org/wiki/Transmission_Control_Protocol)** |
| 2       | UDP        | **[Protocole utilisateur Datagram ](https://en.wikipedia.org/wiki/User_Datagram_Protocol)**              |
| 4       | WebSockets | **[WebSockets](https://en.wikipedia.org/wiki/WebSocket)**                                                |

Veuillez noter que cette fonction est le champ `flags`, il est donc possible de choisir n’importe quelle combinaison de valeurs disponibles. Consultez **[le mapping des drapeaux](#json-mapping)** si vous souhaitez en savoir plus. Si aucun indicateur n’est activé, l’option `None` est validée et cette option n’est pas valide par elle-même.

By default ASF will use all available Steam protocols as a measure for fighting with downtimes and other similar Steam issues. Généralement, vous souhaitez modifier cette fonction si vous souhaitez limiter ASF à l’utilisation d’un ou deux protocoles spécifiques au lieu de tous les protocoles disponibles. Une telle mesure pourrait être nécessaire si, par exemple, en activant uniquement le trafic TCP sur votre pare-feu, vous ne voulez pas qu'ASF tente de se connecter via UDP. Cependant, sauf si vous êtes en train de debug un problème ou un problème particulier, vous voulez presque toujours vous assurer qu'ASF est libre d'utiliser tout protocole actuellement pris en charge, et non un ou deux. À moins que vous n'ayez une raison **forte** de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `UpdateChannel`

`byte` avec la valeur par défaut `1`. This property defines update channel which is being used, either for auto-updates (if `UpdatePeriod` is greater than `0`), or update notifications (otherwise). Currently ASF supports three update channels - `0` which is called `None`, `1`, which is called `Stable`, and `2`, which is called `Experimental`. `Stable` channel is the default release channel, which should be used by majority of users. `Experimental` channel in addition to `Stable` releases, also includes **pre-releases** dedicated for advanced users and other developers in order to test new features, confirm bugfixes or give feedback about planned enhancements. **Experimental versions often contain unpatched bugs, work-in-progress features or rewritten implementations**. If you don't consider yourself advanced user, please stay with default `1` (Stable) update channel. `Experimental` channel is dedicated to users who know how to report bugs, deal with issues and give feedback - no technical support will be given. Check out ASF **[release cycle](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle)** if you'd like to learn more. You can also set `UpdateChannel` to `0` (`None`), if you want to completely remove all version checks. Setting `UpdateChannel` to `0` will entirely disable entire functionality related to updates, including `update` command. Using `None` channel is **strongly discouraged** due to exposing yourself to all sort of problems (mentioned in `UpdatePeriod` description below).

**Unless you know what you're doing**, we **strongly** recommend to keep it at default.

* * *

### `UpdatePeriod`

`byte` avec la valeur par défaut `24`. This property defines how often ASF should check for auto-updates. Updates are crucial not only to receive new features and critical security patches, but also to receive bugfixes, performance enhancements, stability improvements and more. When a value greater than `0` is set, ASF will automatically download, replace, and restart itself (if `AutoRestart` permits) when new update is available. In order to achieve this, ASF will check every `UpdatePeriod` hours if new update is available on our GitHub repo. A value of `0` disables auto-updates, but still allows you to execute `update` command manually. You may also be interested in setting appropriate `UpdateChannel` that `UpdatePeriod` should follow.

Update process of ASF involves update of entire folder structure that ASF is using, but without touching your own configs or databases located in `config` directory - this means that any extra files unrelated to ASF in its directory can be lost during update. Default value of `24` is a good balance between unnecessary checks, and ASF that is fresh enough.

Unless you have a **strong** reason to disable this feature, you should keep auto-updates enabled within reasonable `UpdatePeriod` **for your own good**. This is not only because we don't support anything but latest stable ASF release, but also because **we give our security guarantee only for latest version**. If you're using outdated ASF version then you're intentionally exposing yourself to all kind of issues, from small bugs, through broken functionality, ending with **[permanent Steam account suspensions](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ#did-somebody-get-banned-for-it)**, so we **strongly recommend**, for your own good, to always ensure that your ASF version is up to date. Auto-updates allow us to react quickly to all kind of issues by disabling or patching problematic code before it can escalate - if you opt out of it, you lose all of our security guarantees and risk consequences from running code that could be potentially harmful, not only to Steam network, but also (by definition) to your own Steam account.

* * *

### `WebLimiterDelay`

`string` avec la valeur par défaut `300`. This property defines, in miliseconds, the minimum amount of delay between sending two consecutive requests to the same Steam web-service. Such delay is required as **[AkamaiGhost](https://www.akamai.com)** service that Steam uses internally includes rate-limiting based on global number of requests sent across given time period. In normal circumstances akamai block is rather hard to achieve, but under very heavy workloads with a huge ongoing queue of requests, it's possible to trigger it if we keep sending too many requests across too short time period.

Default value was set based on assumption that ASF is the only tool accessing Steam web-services, in particular `steamcommunity.com`, `api.steampowered.com` and `store.steampowered.com`. If you're using other tools sending requests to the same web-services then you should make sure that your tool includes similar functionality of `WebLimiterDelay` and set both to double of default value, which would be `600`. This guarantees that under no circumstance you'll exceed sending more than 1 request per `300` ms.

In general, lowering `WebLimiterDelay` under default value is **strongly discouraged** as it could lead to various IP-related blocks, some of which are possible to be permanent. Default value is good enough for running a single ASF instance on the server, as well as using ASF in normal scenario along with original Steam client. It should be correct for majority of usages, and you should only increase it (never lower it), if - apart from using ASF, you're also using another tool that may send excessive number of requests to the same web-services that ASF is making use of. In short, global number of all requests sent from a single IP to a single Steam domain should never exceed more than 1 request per `300` ms.

Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `WebProxy`

`chaîne` avec la valeur par défaut `null`. This property defines a web proxy address that will be used for all internal http and https requests sent by ASF's `HttpClient`, especially to services such as `github.com`, `steamcommunity.com` and `store.steampowered.com`. Proxying ASF requests in general has no advantages, but it's exceptionally useful for bypassing various kinds of firewalls, especially the great firewall in China.

Cette propriété est définie comme une chaîne uri :

> A URI string is composed of a scheme (http or https), a host, and an optional port. An example of a complete uri string is `"http://contoso.com:8080"`.

If your proxy requires user authentication, you will also need to set up `WebProxyUsername` and/or `WebProxyPassword`. If there is no such need, setting up this property alone is sufficient.

Right now ASF uses web proxy only for `http` and `https` requests, which **do not** include internal Steam network communication done within ASF's internal Steam client. There are currently no plans for supporting that, mainly due to missing **[SK2](https://github.com/SteamRE/SteamKit/issues/587#issuecomment-413271550)** functionality. If you need/want it to happen, I'd suggest starting from there.

Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `WebProxyPassword`

`chaîne` avec la valeur par défaut `null`. This property defines password field used in basic, digest, NTLM, and Kerberos authentication that is supported by a target `WebProxy` machine providing proxy functionality. If your proxy doesn't require user credentials, there is no need for you to input anything here. Using this option makes sense only if `WebProxy` is used as well, as it has no effect otherwise.

Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

### `WebProxyUsername`

`chaîne` avec la valeur par défaut `null`. This property defines username field used in basic, digest, NTLM, and Kerberos authentication that is supported by a target `WebProxy` machine providing proxy functionality. If your proxy doesn't require user credentials, there is no need for you to input anything here. Using this option makes sense only if `WebProxy` is used as well, as it has no effect otherwise.

Sauf si vous avez une raison de modifier cette fonction, vous devez la conserver par défaut.

* * *

## Configuration Bot

As you should know already, every bot should have its own config based on example JSON structure below. Start from deciding how you want to name your bot (e.g. `1.json`, `main.json`, `primary.json` or `AnythingElse.json`) and head over to configuration.

**Notice:** Bot can't be named `ASF` (as that keyword is reserved for global config), ASF will also ignore all configuration files starting with a dot.

La configuration du bot a la structure suivante :

```json
{
    "AcceptGifts": false,
    "AutoSteamSaleEvent": false,
    "BotBehaviour": 0,
    "CustomGamePlayedWhileFarming": null,
    "CustomGamePlayedWhileIdle": null,
    "Enabled": false,
    "FarmingOrders": [],
    "GamesPlayedWhileIdle": [],
    "HoursUntilCardDrops": 3,
    "IdlePriorityQueueOnly": false,
    "IdleRefundableGames": true,
    "LootableTypes": [1, 3, 5],
    "MatchableTypes": [5],
    "OnlineStatus": 1,
    "PasswordFormat": 0,
    "Paused": false,
    "RedeemingPreferences": 0,
    "SendOnFarmingFinished": false,
    "SendTradePeriod": 0,
    "ShutdownOnFarmingFinished": false,
    "SteamLogin": null,
    "SteamMasterClanID": 0,
    "SteamParentalCode": null,
    "SteamPassword": null,
    "SteamTradeToken": null,
    "SteamUserPermissions": {},
    "TradingPreferences": 0,
    "TransferableTypes": [1, 3, 5],
    "UseLoginKeys": true
}
```

* * *

Toutes les options sont expliquées ci-dessous :

### `AcceptGifts`

`bool` avec la valeur par défaut `true</ 0>. When enabled, ASF will automatically accept and redeem all steam gifts (including wallet gift cards) sent to the bot. This includes also gifts sent from users other than those defined in <code>SteamUserPermissions`. Keep in mind that gifts sent to e-mail address are not directly forwarded to the client, so ASF won't accept those without your help.

This option is recommended only for alt accounts, as it's very likely that you don't want to automatically redeem all gifts sent to your primary account. Si vous ne savez pas si cette fonctionnalité doit être activée ou non, conservez-la avec la valeur par défaut `false`.

* * *

### `AutoSteamSaleEvent`

`bool` avec la valeur par défaut `false`. During Steam summer/winter sale events Steam is known for providing you extra cards for browsing discovery queue each day, as well as through other event-specific activities. When this option is enabled, ASF will automatically check Steam discovery queue each `8` hours (starting in one hour since program start), and clear it if needed. Cette option n'est pas recommandée si vous souhaitez effectuer cette action vous-même et ne doit normalement avoir de sens que sur des comptes bot. De plus, vous devez vous assurer que votre compte a au moins le niveau `8` si vous vous attendez à recevoir ces cartes en premier lieu, car c'est une pré-requis exigé par Steam. Si vous ne savez pas si cette fonctionnalité doit être activée ou non, conservez-la avec la valeur par défaut `false`.

Veuillez noter qu'en raison de problèmes, modifications et problèmes constants liés à Steam, **nous ne donnons aucune garantie sur le bon fonctionnement de cette fonction**. Il est donc tout à fait possible que cette option **ne fonctionne pas du tout** . Nous n'acceptons pas **les** rapports de bugs, ni les demandes d'assistance pour cette option. Il est offert avec aucune garantie, vous l'utilisez à vos risques et périls.

* * *

### `BotBehaviour`

`chaîne` avec la valeur par défaut `0`. Cette fonction définit le comportement de type bot ASF lors de divers événements. Elle est définie comme suit:

| Valeur  | Nom                           | Description                                                                                              |
| ------- | ----------------------------- | -------------------------------------------------------------------------------------------------------- |
| 0       | None                          | No special bot behaviour, the least invasive mode, default                                               |
| 1       | RejectInvalidFriendInvites    | Forcera ASF à rejeter (au lieu d'ignorer) des offres d'amitié non valides                                |
| 2       | RejectInvalidTrades           | Forcera ASF à rejeter (au lieu d'ignorer) des offres d'échange non valides                               |
| 4       | RejectInvalidGroupInvites     | Forcera ASF à rejeter (au lieu d'ignorer) des offres d'inclusion à un groupe non valides                 |
| 8       | DismissInventoryNotifications | Will cause ASF to automatically dismiss all inventory notifications                                      |
| 16      | MarkReceivedMessagesAsRead    | Will cause ASF to automatically mark all received messages as read                                       |
| 32      | MarkBotMessagesAsRead         | Will cause ASF to automatically mark messages from other ASF bots (running in the same instance) as read |

Veuillez noter que cette fonction est le champ `flags`, il est donc possible de choisir n’importe quelle combinaison de valeurs disponibles. Consultez **[le mapping des drapeaux](#json-mapping)** si vous souhaitez en savoir plus. Si aucun indicateur n’est activé, l’option `None` est activée.

En général, vous souhaitez modifier cette fonction si vous attendez d'ASF une certaine automatisation liée à son activité, comme ce serait le cas d'un compte bot, mais pas d'un compte principal utilisé dans ASF. Par conséquent, la modification de cette fonction est particulièrement utile pour tout les comptes, bien que vous puissiez également utiliser les options sélectionnées pour les comptes principaux.

Le comportement ASF normal (`None`) consiste à n’automatiser que les éléments souhaités par l’utilisateur (par exemple, le farming des cartes ou les offres `SteamTradeMatcher `, si elles sont définies dans `TradingPreferences`. Il s'agit du mode le moins invasif. Il est avantageux pour la majorité des utilisateurs, car vous gardez le contrôle total de votre compte et vous pouvez décider vous-même d'autoriser ou non certaines interactions hors de la portée.

Une invitation d'amis non valide est une invitation qui ne provient pas d'un utilisateur disposant de l'autorisation `FamilySharing` (défini dans `SteamUserPermissions`) ou supérieure. ASF en mode normal ignore ces invitations, comme vous vous en doutez, vous laissant le choix de les accepter ou non. `RejectInvalidFriendInvites` entraînera le rejet automatique de ces invitations, ce qui désactivera pratiquement l'option permettant à d'autres personnes de vous ajouter à leur liste d'amis (ASF refusant toutes ces demandes, à l'exception des personnes définies dans `SteamUserPermissions`). Si vous ne souhaitez pas refuser toutes les invitations d'amis, vous ne devez pas activer cette option.

Une offre commerciale non valide est une offre qui n'est pas acceptée via le module ASF intégré. Pour plus d'informations à ce sujet, reportez-vous à la section **[trading](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading)**, qui définit explicitement les types de commerce que ASF est disposé à accepter automatiquement. Les transactions valides sont également définies par d'autres paramètres, notamment `TradingPreferences`. `RejectInvalidTrades` entraînera le rejet de toutes les offres commerciales non valides, au lieu de les ignorer. Si vous ne souhaitez pas refuser toutes les offres commerciales qui ne sont pas automatiquement acceptées par ASF, vous ne devez pas activer cette option.

Une invitation de groupe non valide est une invitation qui ne provient pas du groupe `SteamMasterClanID`. ASF en mode normal ignore ces invitations à un groupe, comme vous le souhaitez, vous permettant de décider vous-même si vous souhaitez rejoindre un groupe Steam particulier ou non. `RejectInvalidGroupInvites` entraînera le rejet automatique de toutes les invitations à un groupe, ce qui rendra impossible toute invitation à un autre groupe que `SteamMasterClanID`. Si vous ne souhaitez pas refuser toutes les invitations à un groupe, vous ne devez pas activer cette option.

`DismissInventoryNotifications` is extremely useful when you start getting annoyed by contact Steam notification about receiving new items. ASF can't get rid of the notification itself, as that's built-in into your Steam client, but it's able to automatically clear the notification after receiving it, which will no longer leave "new items in inventory" notification hanging around. If you expect to evaluate yourself all received items (especially cards idled with ASF), then naturally you shouldn't enable this option. When you start going crazy, remember this is offered as an option.

`MarkReceivedMessagesAsRead` will automatically mark **all** messages being received by the account on which ASF is running, both private and group, as read. This typically should be used by alt accounts only in order to clear "new message" notification coming e.g. from you during executing ASF commands. We do not recommend this option for primary accounts, unless you want to cut yourself from any kind of new messages notifications, **including** those that happened while you were offline, assuming that ASF was still left open dismissing it.

`MarkBotMessagesAsRead` works in a similar manner by marking only bot messages as read. However, keep in mind that when using that option on group chats with your bots and other people, Steam implementation of acknowledging chat message **also** acknowledges all messages that happened **before** the one you decided to mark, so if by any chance you don't want to miss unrelated message that happened in-between, you typically want to avoid using this feature. Obviously, it's also risky when you have multiple primary accounts (e.g. from different users) running in the same ASF instance, as you can also miss their normal out-of-ASF messages.

Si vous ne savez pas comment configurer cette option, il est préférable de la laisser par défaut.

* * *

### `CustomGamePlayedWhileFarming`

`chaîne` avec la valeur par défaut `null`. When ASF is farming, it can display itself as "Playing non-steam game: `CustomGamePlayedWhileFarming`" instead of currently farmed game. This can be useful if you would like to let your friends know that you're farming, yet you don't want to use `OnlineStatus` of `Offline`. Please note that ASF cannot guarantee the actual display order of Steam network, therefore this is only a suggestion that may, or may not, display properly. Default value of `null` disables this feature.

* * *

### `CustomGamePlayedWhileIdle`

`chaîne` avec la valeur par défaut `null`. Similar to `CustomGamePlayedWhileFarming`, but for the situation when ASF has nothing to do (as account is fully farmed). Default value of `null` disables this feature.

* * *

### `Enabled`

`bool` avec la valeur par défaut `true</ 0>. Cette propriété définit si le bot est activé. Enabled bot instance (<code>true`) will automatically start on ASF run, while disabled bot instance (`false`) will need to be started manually. By default every bot is disabled, so you probably want to switch this property to `true` for all of your bots that should be started automatically.

* * *

### `FarmingOrders`

`ImmutableHashSet<byte>` avec la valeur par défaut étant vide. This property defines the **preferred** farming order used by ASF for given bot account. Currently there are following farming orders available:

| Valeur  | Nom                       | Description                                                                      |
| ------- | ------------------------- | -------------------------------------------------------------------------------- |
| 0       | Unordered                 | No sorting, slightly improving CPU performance                                   |
| 1       | AppIDsAscending           | Try to farm games with lowest `appIDs` first                                     |
| 2       | AppIDsDescending          | Try to farm games with highest `appIDs` first                                    |
| 3       | CardDropsAscending        | Try to farm games with lowest number of card drops remaining first               |
| 4       | CardDropsDescending       | Try to farm games with highest number of card drops remaining first              |
| 5       | HoursAscending            | Try to farm games with lowest number of hours played first                       |
| 6       | HoursDescending           | Try to farm games with highest number of hours played first                      |
| 7       | NamesAscending            | Try to farm games in alphabetical order, starting from A                         |
| 8       | NamesDescending           | Try to farm games in reverse alphabetical order, starting from Z                 |
| 9       | Random                    | Try to farm games in totally random order (different on each run of the program) |
| 10      | BadgeLevelsAscending      | Try to farm games with lowest badge levels first                                 |
| 11      | BadgeLevelsDescending     | Try to farm games with highest badge levels first                                |
| 12      | RedeemDateTimesAscending  | Try to farm oldest games on our account first                                    |
| 13      | RedeemDateTimesDescending | Try to farm newest games on our account first                                    |
| 14      | MarketableAscending       | Try to farm games with unmarketable card drops first                             |
| 15      | MarketableDescending      | Try to farm games with marketable card drops first                               |

Since this property is an array, it allows you to use several different settings in your fixed order. For example, you can include values of `15`, `11` and `7` in order to sort by marketable games first, then by those with highest badge level, and finally alphabetically. As you can guess, the order actually matters, as reverse one (`7`, `11` and `15`) achieves something entirely different. Majority of people will probably use just one order out of all of them, but in case you want to, you can also sort further by extra parameters.

Also notice the word "try" in all above descriptions - the actual ASF order is heavily affected by selected **[cards farming algorithm](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance)** and sorting will affect only results that ASF considers same performance-wise. For example, in `Simple` algorithm, selected `FarmingOrders` should be entirely respected in current farming session (as every game has the same performance value), while in `Complex` algorithm actual order is affected by hours first, and then sorted according to chosen `FarmingOrders`. This will lead to different results, as games with existing playtime will have a priority over others, so effectively ASF will prefer games that already passed required `HoursUntilCardDrops` firstly, and only then sorting those games further by your chosen `FarmingOrders`. Likewise, once ASF runs out of already-bumped games, it'll sort remaining queue by hours first (as that will decrease time required for bumping any of remaining titles to `HoursUntilCardDrops`). Therefore, this config property is only a **suggestion** that ASF will try to respect, as long as it doesn't affect performance negatively (in this case, ASF will always prefer idling performance over `FarmingOrders`).

There is also idling priority queue that is accessible through `iq` **[commands](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. If it's used, actual idling order is sorted firstly by performance, then by idling queue, and finally by your `FarmingOrders`.

* * *

### `GamesPlayedWhileIdle`

`ImmutableHashSet<uint>` avec la valeur par défaut étant vide. If ASF has nothing to farm it can play your specified steam games (`appIDs`) instead. Playing games in such manner increases your "hours played" of those games, but nothing else apart of it. In order for this feature to work properly, your Steam account **must** own a valid license to all the `appIDs` that you specify here, this includes F2P games as well. This feature can be enabled at the same time with `CustomGamePlayedWhileIdle` in order to play your selected games while showing custom status in Steam network, but in this case, like in `CustomGamePlayedWhileFarming` case, the actual display order is not guaranteed. Please note that Steam allows ASF to play only up to `32` `appIDs` total, therefore you can put only as many games in this property.

* * *

### `HoursUntilCardDrops`

`byte` avec la valeur par défaut `3`. This property defines if account has card drops restricted, and if yes, for how many initial hours. Restricted card drops means that account is not receiving any card drops from given game until the game is played for at least `HoursUntilCardDrops` hours. Unfortunately there is no magical way to detect that, so ASF relies on you. This property affects **[cards farming algorithm](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance)** that will be used. Setting this property properly will maximize profits and minimize time required for cards to be farmed. Remember that there is no obvious answer whether you should use one or another value, since it fully depends on your account. It seems that older accounts which never asked for refund have unrestricted card drops, so they should use a value of `0`, while new accounts and those who did ask for refund have restricted card drops with a value of `3`. This is however only theory, and should not be taken as a rule. The default value for this property was set based on "lesser evil" and majority of use cases.

* * *

### `IdlePriorityQueueOnly`

`bool` avec la valeur par défaut `true</ 0>. This property defines if ASF should consider for automatic idling only apps that you added yourself to priority idling queue available with <code>iq` **[commands](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. When this option is enabled, ASF will skip all `appIDs` that are missing on the list, effectively allowing you to cherry-pick games for automatic ASF idling. Keep in mind that if you didn't add any games to the queue then ASF will act as if there is nothing to idle on your account. Si vous ne savez pas si cette fonctionnalité doit être activée ou non, conservez-la avec la valeur par défaut `false`.

* * *

### `IdleRefundableGames`

`bool` avec la valeur par défaut `true</ 0>. This property defines if ASF is permitted to idle games that are still refundable. A refundable game is a game that you bought in last 2 weeks through Steam Store and didn't play for longer than 2 hours yet, as stated on <strong><a href="https://store.steampowered.com/steam_refunds">Steam refunds</a></strong> page. By default when this option is set to <code>true`, ASF ignores Steam refunds policy entirely and idles everything, as most people would expect. However, you can change this option to `false` if you want to ensure that ASF won't idle any of your refundable games too soon, allowing you to evaluate those games yourself and refund if needed without worrying about ASF affecting playtime negatively. Please note that if you disable this option then games you purchased from Steam Store won't be idled by ASF for up to 14 days since redeem date, which will show as nothing to idle if your account doesn't own anything else. Si vous ne savez pas si cette fonctionnalité doit être activée ou non, conservez-la avec la valeur par défaut `true`.

* * *

### `LootableTypes`

`ImmutableHashSet<byte>` type with default value of `1, 3, 5` steam item types. This property defines ASF behaviour when looting - both manual, using a **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**, as well as automatic one, through one or more configuration properties. ASF will ensure that only items from `LootableTypes` will be included in a trade offer, therefore this property allows you to choose what you want to receive in a trade offer that is being sent to you.

| Valeur  | Nom                   | Description                                                                            |
| ------- | --------------------- | -------------------------------------------------------------------------------------- |
| 0       | Unknown               | Tous les types qui ne correspondent à aucun des éléments ci-dessous                    |
| 1       | BoosterPack           | Paquet de cartes contenant 3 cartes aléatoires d'un jeu                                |
| 2       | Emoticon              | Emoji à utiliser dans le chat Steam                                                    |
| 3       | FoilTradingCard       | Variante brillante de `TradingCard`                                                    |
| 4       | ProfileBackground     | Fond d'écran à utiliser sur votre profil Steam                                         |
| 5       | Carte à échanger      | Carte Steam à échanger, utilisée dans la fabrication de badges (non-brillants)         |
| 6       | SteamGems             | Gemmes Steam utilisés dans la fabrication des paquets de cartes, sacs de gemmes inclus |
| 7       | SaleItem              | Articles spéciaux attribués lors des soldes Steam                                      |
| 8       | Consumable            | Articles consommables spéciaux qui disparaissent après avoir été utilisés              |
| 9       | ProfileModifier       | Articles spéciaux qui peuvent modifier l'apparence du profil Steam                     |
| 10      | Sticker               | Special items that can be used on Steam chat                                           |
| 11      | ChatEffect            | Special items that can be used on Steam chat                                           |
| 12      | MiniProfileBackground | Special background for Steam profile                                                   |

Please note that regardless of the settings above, ASF will only ask for Steam (`appID` of 753) community (`contextID` of 6) items, so all game items, gifts and likewise, are excluded from the trade offer by definition.

Default ASF setting is based on the most common usage of the bot, with looting only booster packs, and trading cards (including foils). The property defined here allows you to alter that behaviour in whatever way that satisfies you. Please keep in mind that all types not defined above will show as `Unknown` type, which is especially important when Valve releases some new Steam item, that will be marked as `Unknown` by ASF as well, until it's added here (in the future release). That's why in general it's not recommended to include `Unknown` type in your `LootableTypes`, unless you know what you're doing, and you also understand that ASF will send your entire inventory in a trade offer if Steam Network gets broken again and reports all your items as `Unknown`. My strong suggestion is to not include `Unknown` type in the `LootableTypes`, even if you expect to loot everything (else).

* * *

### `MatchableTypes`

`ImmutableHashSet<byte>` type with default value of `5` Steam item types. This property defines which Steam item types are permitted to be matched when `SteamTradeMatcher` option in `TradingPreferences` is enabled. Types are defined as below:

| Valeur  | Nom                   | Description                                                                            |
| ------- | --------------------- | -------------------------------------------------------------------------------------- |
| 0       | Unknown               | Tous les types qui ne correspondent à aucun des éléments ci-dessous                    |
| 1       | BoosterPack           | Paquet de cartes contenant 3 cartes aléatoires d'un jeu                                |
| 2       | Emoticon              | Emoji à utiliser dans le chat Steam                                                    |
| 3       | FoilTradingCard       | Variante brillante de `TradingCard`                                                    |
| 4       | ProfileBackground     | Fond d'écran à utiliser sur votre profil Steam                                         |
| 5       | Carte à échanger      | Carte Steam à échanger, utilisée dans la fabrication de badges (non-brillants)         |
| 6       | SteamGems             | Gemmes Steam utilisés dans la fabrication des paquets de cartes, sacs de gemmes inclus |
| 7       | SaleItem              | Articles spéciaux attribués lors des soldes Steam                                      |
| 8       | Consumable            | Articles consommables spéciaux qui disparaissent après avoir été utilisés              |
| 9       | ProfileModifier       | Articles spéciaux qui peuvent modifier l'apparence du profil Steam                     |
| 10      | Sticker               | Special items that can be used on Steam chat                                           |
| 11      | ChatEffect            | Special items that can be used on Steam chat                                           |
| 12      | MiniProfileBackground | Special background for Steam profile                                                   |

Of course, types that you should use for this property typically include only `2`, `3`, `4` and `5`, as only those types are supported by STM. ASF includes proper logic for discovering rarity of the items, therefore it's also safe to match emoticons or backgrounds, as ASF will properly consider fair only those items from the same game and type, that also share the same rarity.

Please note that **ASF is not a trading bot** and **will NOT care about the market price**. If you don't consider items of the same rarity from the same set to be the same price-wise, then this option is NOT for you. Please evaluate twice if you understand and agree with this statement before you decide to change this setting.

Unless you know what you're doing, you should keep it with default value of `5`.

* * *

### `OnlineStatus`

`byte` avec la valeur par défaut `1`. This property specifies Steam community status that the bot will be announced with after logging in to Steam network. Currently you can choose one of below statuses:

| Valeur  | Nom            |
| ------- | -------------- |
| 0       | Hors ligne     |
| 1       | En ligne       |
| 2       | Busy           |
| 3       | Away           |
| 4       | Snooze         |
| 5       | LookingToTrade |
| 6       | LookingToPlay  |
| 7       | Invisible      |

`Offline` status is extremely useful for primary accounts. As you should know, farming a game actually shows your steam status as "Playing game: XXX", which can be misleading to your friends, confusing them that you're playing a game while actually you're only farming it. Using `Offline` status solves that issue - your account will never be shown as "in-game" when you're farming steam cards with ASF. This is possible thanks to the fact that ASF does not have to sign in into Steam Community in order to work properly, so we're in fact playing those games, connected to Steam network, but without announcing our online presence at all. Keep in mind that played games using offline status will still count towards your playtime, and show as "recently played" on your profile.

In addition to that, this feature is also important if you want to receive notifications and unread messages when ASF is running, while not keeping Steam client open at the same time. This is because ASF acts as a Steam client itself, and whether ASF would like it or not, Steam broadcasts all those messages and other events to it. This is not a problem if you have both ASF and your own Steam client running, as both clients receive exactly the same events. However, if just ASF is running, Steam network could mark certain events and messages as "delivered", despite of your traditional Steam client not receiving it due to not being present. Offline status also solves this problem, as ASF is never considered for any community events in this case, so all unread messages and other events will be properly marked as unread when you come back.

It's important to note that ASF running on `Offline` mode will **not** be able to receive commands in usual Steam chat way, as the chat, as well as entire community presence is in fact, entirely offline. A solution to this issue is using `Invisible` mode instead which works in a similar way (not exposing status), but keeps the ability to receive and respond to messages (so also a potential to dismiss notifications and unread messages as stated above). `Invisible` mode makes the most sense on alt accounts that you don't want to expose (status-wise), but still be able to send commands to.

However, there is one catch with `Invisible` mode - it doesn't go well with primary accounts. This is because **any** Steam session that is currently online **exposes** the status, even if ASF itself does not. This is caused by the current limitation/bug of the Steam network that isn't possible to be fixed on ASF side, so if you want to use `Invisible` mode you will also need to ensure that **all** other sessions to the same account use `Invisible` mode as well. This will be the case on alt accounts where ASF is hopefully the only active session, but on primary accounts you'll almost always prefer to show as `Online` to your friends, hiding only ASF activity, and in this case `Invisible` mode will be entirely useless for you (we recommend to use `Offline` mode instead). Hopefully this limitation/bug will be eventually solved in the future by Valve, but I wouldn't expect that to happen anytime soon...

If you're unsure how to set up this property, it's recommended to use a value of `0` (`Offline`) for primary accounts, and default `1` (`Online`) otherwise.

* * *

### `PasswordFormat`

`byte` avec la valeur par défaut `0`. This property defines the format of `SteamPassword` property, and currently supports - `0` for `PlainText`, `1` for `AES` and `2` for `ProtectedDataForCurrentUser`. Please refer to **[Security](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Security)** section if you want to learn more, as you'll need to ensure that `SteamPassword` property indeed includes password in matching `PasswordFormat`. In other words, when you change `PasswordFormat` then your `SteamPassword` should be **already** in that format, not just aiming to be. Unless you know what you're doing, you should keep it with default value of `0`.

* * *

### `Paused`

`bool` avec la valeur par défaut `true</ 0>. This property defines initial state of <code>CardsFarmer` module. With default value of `false`, bot will automatically start farming when it's started, either because of `Enabled` or `start` command. Switching this property to `true` should be done only if you want to manually `resume` automatic farming process, for example because you want to use `play` all the time and never use automatic `CardsFarmer` module - this works exactly the same as `pause` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. Si vous ne savez pas si cette fonctionnalité doit être activée ou non, conservez-la avec la valeur par défaut `false`.

* * *

### `RedeemingPreferences`

`chaîne` avec la valeur par défaut `0`. This property defines ASF behaviour when redeeming cd-keys, and is defined as below:

| Valeur  | Nom                                | Description                                                                                                                     |
| ------- | ---------------------------------- | ------------------------------------------------------------------------------------------------------------------------------- |
| 0       | None                               | No special redeeming preferences, default                                                                                       |
| 1       | Forwarding                         | Forward keys unavailable to redeem to other bots                                                                                |
| 2       | Distributing                       | Distribute all keys among itself and other bots                                                                                 |
| 4       | KeepMissingGames                   | Keep keys for (potentially) missing games when forwarding, leaving them unused                                                  |
| 8       | AssumeWalletKeyOnBadActivationCode | Assume that `BadActivationCode` keys are equal to `CannotRedeemCodeFromClient`, and therefore try to redeem them as wallet keys |

Veuillez noter que cette fonction est le champ `flags`, il est donc possible de choisir n’importe quelle combinaison de valeurs disponibles. Consultez **[le mapping des drapeaux](#json-mapping)** si vous souhaitez en savoir plus. Si aucun indicateur n’est activé, l’option `None` est activée.

`Forwarding` will cause bot to forward a key that is not possible to redeem, to another connected and logged on bot that is missing that particular game (if possible to check). The most common situation is forwarding `AlreadyPurchased` game to another bot that is missing that particular game, but this option also covers other scenarios, such as `DoesNotOwnRequiredApp`, `RateLimited` or `RestrictedCountry`.

`Distributing` will cause bot to distribute all received keys among itself and other bots. This means that every bot will get a single key from the batch. Typically this is used only when you're redeeming many keys for the same game, and you want to evenly distribute them among your bots, as opposed to redeeming keys for various different games. This feature makes no sense if you're redeeming only one key in a single `redeem` action (as there are no extra keys to be distributed).

`KeepMissingGames` will cause bot to skip `Forwarding` when we can't be sure if key being redeemed is in fact owned by our bot, or not. This basically means that `Forwarding` will apply **only** to `AlreadyPurchased` keys, instead of covering also other cases such as `DoesNotOwnRequiredApp`, `RateLimited` or `RestrictedCountry`. Typically you want to use this option on primary account, to ensure that keys being redeemed on it won't be forwarded further if your bot for example becomes temporarily `RateLimited`. As you can guess from the description, this field has absolutely no effect if `Forwarding` is not enabled.

`AssumeWalletKeyOnBadActivationCode` will cause `BadActivationCode` keys to be treated as `CannotRedeemCodeFromClient`, and therefore result in ASF trying to redeem them as wallet keys. This is needed because Steam might announce wallet keys as `BadActivationCode` (and not `CannotRedeemCodeFromClient` as it used to), resulting in ASF never attempting to redeem them. However, we recommend **against** using this preference, as it'll result in ASF trying to redeem every invalid key as a wallet code, resulting in excessive amount of (potentially invalid) requests sent to the Steam service, with all the potential consequences. Instead, we recommend to use `ForceAssumeWalletKey` **[`redeem^`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands#redeem-modes)** mode while knowingly redeeming wallet keys, which will enable the needed workaround only when it's required, on as-needed basis.

Enabling both `Forwarding` and `Distributing` will add distributing feature on top of forwarding one, which makes ASF trying to redeem one key on all bots firstly (forwarding) before moving to the next one (distributing). Typically you want to use this option only when you want `Forwarding`, but with altered behaviour of switching the bot on key being used, instead of always going in-order with every key (which would be `Forwarding` alone). This behaviour can be beneficial if you know that majority or even all of your keys are tied to the same game, because in this situation `Forwarding` alone would try to redeem everything on one bot firstly (which makes sense if your keys are for unique games), and `Forwarding` + `Distributing` will switch the bot on the next key, "distributing" the task of redeeming new key onto another bot than the initial one (which makes sense if keys are for the same game, skipping one pointless attempt per key).

The actual bots order for all of the redeeming scenarios is alphabetical, excluding bots that are unavailable (not connected, stopped or likewise). Please keep in mind that there is per-IP and per-account hourly limit of redeeming tries, and every redeem try that didn't end with `OK` contributes to failed tries. ASF will do its best to minimize number of `AlreadyPurchased` failures, e.g. by trying to avoid forwarding a key to another bot that already owns that particular game, but it's not always guaranteed to work due to how Steam is handling licenses. Using redeeming flags such as `Forwarding` or `Distributing` will always increase your likelyhood to hit `RateLimited`.

Also keep in mind that you can't forward or distribute keys to bots that you do not have access to. This should be obvious, but ensure that you're at least `Operator` of all the bots you want to include in your redeeming process, for example with `status ASF` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**.

* * *

### `SendOnFarmingFinished`

`bool` avec la valeur par défaut `true</ 0>. When ASF is done with farming given account, it can automatically send steam trade containing everything farmed up to this point to user with <code>Master` permission, which is very convenient if you don't want to bother with trades yourself. This option works the same as `loot` command, therefore keep in mind that it requires user with `Master` permission set, you may also need a valid `SteamTradeToken`, as well as using an account that is eligible for trading in the first place. In addition to initiating `loot` after finishing farming, ASF will also initiate `loot` on each new items notification (when not farming), and after completing each trade that results in new items (always) when this option is active. This is especially useful for "forwarding" items received from other people to our account.

Typically you'll want to use **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)** together with this feature, although it's not a requirement if you intend to confirm manually in timely fashion. Si vous n'êtes pas sur du réglage de cette propriété, laissez-la à la valeur par défaut de `false`.

* * *

### `SendTradePeriod`

`byte` avec la valeur par défaut `0`. This property works very similar to `SendOnFarmingFinished` property, with one difference - instead of sending trade when farming is done, we can also send it every `SendTradePeriod` hours, regardless of how much we have to farm left. This is useful if you want to `loot` your alt accounts on usual basis instead of waiting for it to finish farming. Default value of `0` disables this feature, if you want your bot to send you trade e.g. every day, you should put `24` here.

Typically you'll want to use **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)** together with this feature, although it's not a requirement if you intend to confirm manually in timely fashion. Si vous n'êtes pas sur du réglage de cette propriété, laissez-la à la valeur par défaut de `0`.

* * *

### `ShutdownOnFarmingFinished`

`bool` avec la valeur par défaut `true</ 0>. ASF is "occupying" an account for the whole time of process being active. When given account is done with farming, ASF periodically checks it (every <code>IdleFarmingPeriod` hours), if perhaps some new games with steam cards were added in the meantime, so it can resume farming of that account without a need to restart the process. This is useful for majority of people, as ASF can automatically resume farming when needed. However, you may actually want to stop the process when given account is fully farmed, you can achieve that by setting this property to `true`. When enabled, ASF will proceed with logging off when account is fully farmed, which means that it won't be periodically checked or occupied anymore. You should decide yourself if you prefer ASF to work on given bot instance for the whole time, or if perhaps ASF should stop it when farming process is done. When all accounts are stopped and process is not running in `--process-required` **[mode](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments)**, ASF will shutdown as well, putting your machine at rest and allowing you to schedule other actions, such as sleep or shutdown at the same moment of last card dropping.

Si vous n'êtes pas sur du réglage de cette propriété, laissez-la à la valeur par défaut de `false`.

* * *

### `SteamLogin`

`chaîne` avec la valeur par défaut `null`. This property defines your steam login - the one you use for logging in to steam. In addition to defining steam login here, you may also keep default value of `null` if you want to enter your steam login on each ASF startup instead of putting it in the config. This may be useful for you if you don't want to save sensitive data in config file.

* * *

### `SteamMasterClanID`

`ulong` avec la valeur par défaut `0`. This property defines the steamID of the steam group that bot should automatically join, including its group chat. You can check steamID of your group by navigating to its **[page](https://steamcommunity.com/groups/archiasf)**, then adding `/memberslistxml?xml=1` to the end of the link, so the link will look like **[this](https://steamcommunity.com/groups/archiasf/memberslistxml?xml=1)**. Then you can get steamID of your group from the result, it's in `<groupID64>` tag. In above example it would be `103582791440160998`. In addition to trying to join given group at startup, the bot will also automatically accept group invites to this group, which makes it possible for you to invite your bot manually if your group has private membership. If you don't have any group dedicated for your bots, you should keep this property with default value of `0`.

* * *

### `SteamParentalCode`

`chaîne` avec la valeur par défaut `null`. This property defines your steam parental PIN. ASF requires an access to resources protected by steam parental, therefore if you use that feature, you should provide ASF with parental unlock PIN, so it can operate normally. Default value of `null` means that there is no steam parental PIN required to unlock this account, and this is probably what you want if you don't use steam parental functionality. In addition to defining steam parental PIN here, you may also use value of `0` if you want to enter your steam parental PIN on each ASF startup, when needed, instead of putting it in the config. This may be useful for you if you don't want to save sensitive data in config file.

In limited circumstances, ASF is also able to generate a valid Steam parental code itself, although that requires excessive amount of OS resources and additional time to complete, not to mention that it's not guaranteed to succeed, therefore we recommend to not rely on that feature and instead put valid `SteamParentalCode` in the config for ASF to use.

* * *

### `SteamPassword`

`chaîne` avec la valeur par défaut `null`. This property defines your steam password - the one you use for logging in to steam. In addition to defining steam password here, you may also keep default value of `null` if you want to enter your steam password on each ASF startup instead of putting it in the config. This may be useful for you if you don't want to save sensitive data in config file.

* * *

### `SteamTradeToken`

`chaîne` avec la valeur par défaut `null`. When you have your bot on your friend list, then bot can send a trade to you right away without worrying about trade token, therefore you can leave this property at default value of `null`. If you however decide to NOT have your bot on your friend list, then you will need to generate and fill a trade token as the user that this bot is expecting to send trades to. In other words, this property should be filled with trade token of the account that is defined with `Master` permission in `SteamUserPermissions` of **this** bot instance.

In order to find your token, as logged in user with `Master` permission, navigate **[here](https://steamcommunity.com/my/tradeoffers/privacy)** and take a look at your trade URL. The token we're looking for is made out of 8 characters after `&token=` part in your trade URL. You should copy and put those 8 characters here, as `SteamTradeToken`. Do not include whole trading URL, neither `&token=` part, only the token itself (8 characters).

* * *

### `SteamUserPermissions`

`ImmutableDictionary<ulong, byte>` type with default value of being empty. This property is a dictionary property which maps given Steam user identified by his 64-bit steam ID, to `byte` number that specifies his permission in ASF instance. Currently available bot permissions in ASF are defined as:

| Valeur  | Nom             | Description                                                                                                                                                                                        |
| ------- | --------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 0       | None            | No special permission, this is mainly a reference value that is assigned to steam IDs missing in this dictionary - there is no need to define anybody with this permission                         |
| 1       | PartageFamilial | Provides minimum access for family sharing users. Once again, this is mainly a reference value since ASF is capable of automatically discovering steam IDs that we permitted for using our library |
| 2       | Opérateur       | Provides basic access to given bot instances, mainly adding licenses and redeeming keys                                                                                                            |
| 3       | Maître          | Provides full access to given bot instance                                                                                                                                                         |

In short, this property allows you to handle permissions for given users. Permissions are important mainly for access to ASF **[commands](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**, but also for enabling many ASF features, such as accepting trades. For example you may want to set your own account as `Master`, and give `Operator` access to 2-3 of your friends so they can easily redeem keys for your bot with ASF, while **not** being eligible e.g. for stopping it. Thanks to that you can easily assign permissions to given users and let them use your bot to some specified by you degree.

We recommend to set exactly one user as `Master`, and any amount you wish as `Operators` and below. While it's technically possible to set multiple `Masters` and ASF will work correctly with them, for example by accepting all of their trades sent to the bot, ASF will use only one of them (with lowest steam ID) for every action that requires a single target, for example a `loot` request, so also properties like `SendOnFarmingFinished` or `SendTradePeriod`. If you perfectly understand those limitations, especially the fact that `loot` request will always send items to the `Master` with lowest steam ID, regardless of the `Master` that actually executed the command, then you can define multiple users with `Master` permission here, but we still recommend a single master scheme - multiple masters scheme is discouraged setup that is not supported.

It's nice to note that there is one more extra `Owner` permission, which is declared as `SteamOwnerID` global config property. You can't assign `Owner` permission to anybody here, as `SteamUserPermissions` property defines only permissions that are related to the bot instance, and not ASF as a process. For bot-related tasks, `SteamOwnerID` is treated the same as `Master`, so defining your `SteamOwnerID` here is not necessary.

* * *

### `TradingPreferences`

`chaîne` avec la valeur par défaut `0`. This property defines ASF behaviour when in trading, and is defined as below:

| Valeur  | Nom                 | Description                                                                                                                                                                                                     |
| ------- | ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 0       | None                | No special trading preferences, default                                                                                                                                                                         |
| 1       | AcceptDonations     | Accepts trades in which we're not losing anything                                                                                                                                                               |
| 2       | SteamTradeMatcher   | Passively participates in **[STM](https://www.steamtradematcher.com)**-like trades. Consultez **[trading](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading#steamtradematcher)** pour plus de détails |
| 4       | MatchEverything     | Requires `SteamTradeMatcher` to be set, and in combination with it - also accepts bad trades in addition to good and neutral ones                                                                               |
| 8       | DontAcceptBotTrades | Doesn't automatically accept `loot` trades from other bot instances                                                                                                                                             |
| 16      | MatchActively       | Actively participates in **[STM](https://www.steamtradematcher.com)**-like trades. Consultez **[trading](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading#matchactively)** pour plus de détails      |

Veuillez noter que cette fonction est le champ `flags`, il est donc possible de choisir n’importe quelle combinaison de valeurs disponibles. Consultez **[le mapping des drapeaux](#json-mapping)** si vous souhaitez en savoir plus. Si aucun indicateur n’est activé, l’option `None` est activée.

For further explanation of ASF trading logic, and description of every available flag, please visit **[trading](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading)** section.

* * *

### `TransferableTypes`

`ImmutableHashSet<byte>` type with default value of `1, 3, 5` steam item types. This property defines which Steam item types will be considered for transferring between bots, during `transfer` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. ASF will ensure that only items from `TransferableTypes` will be included in a trade offer, therefore this property allows you to choose what you want to receive in a trade offer that is being sent to one of your bots.

| Valeur  | Nom                   | Description                                                                            |
| ------- | --------------------- | -------------------------------------------------------------------------------------- |
| 0       | Unknown               | Tous les types qui ne correspondent à aucun des éléments ci-dessous                    |
| 1       | BoosterPack           | Paquet de cartes contenant 3 cartes aléatoires d'un jeu                                |
| 2       | Emoticon              | Emoji à utiliser dans le chat Steam                                                    |
| 3       | FoilTradingCard       | Variante Foil de `TradingCard`                                                         |
| 4       | ProfileBackground     | Fond d'écran à utiliser sur votre profil Steam                                         |
| 5       | Carte à échanger      | Carte Steam à échanger, utilisée dans la fabrication de badges (non-brillants)         |
| 6       | SteamGems             | Gemmes Steam utilisés dans la fabrication des paquets de cartes, sacs de gemmes inclus |
| 7       | SaleItem              | Articles spéciaux attribués lors des soldes Steam                                      |
| 8       | Consumable            | Articles consommables spéciaux qui disparaissent après avoir été utilisés              |
| 9       | ProfileModifier       | Articles spéciaux qui peuvent modifier l'apparence du profil Steam                     |
| 10      | Sticker               | Special items that can be used on Steam chat                                           |
| 11      | ChatEffect            | Special items that can be used on Steam chat                                           |
| 12      | MiniProfileBackground | Special background for Steam profile                                                   |

Please note that regardless of the settings above, ASF will only ask for Steam (`appID` of 753) community (`contextID` of 6) items, so all game items, gifts and likewise, are excluded from the trade offer by definition.

Default ASF setting is based on the most common usage of the bot, with transfering only booster packs, and trading cards (including foils). The property defined here allows you to alter that behaviour in whatever way that satisfies you. Please keep in mind that all types not defined above will show as `Unknown` type, which is especially important when Valve releases some new Steam item, that will be marked as `Unknown` by ASF as well, until it's added here (in the future release). That's why in general it's not recommended to include `Unknown` type in your `TransferableTypes`, unless you know what you're doing, and you also understand that ASF will send your entire inventory in a trade offer if Steam Network gets broken again and reports all your items as `Unknown`. My strong suggestion is to not include `Unknown` type in the `TransferableTypes`, even if you expect to transfer everything.

* * *

### `UseLoginKeys`

`bool` avec la valeur par défaut `true</ 0>. This property defines if ASF should use login keys mechanism for this Steam account. Login keys mechanism works very similar to official Steam client's "remember me" option, which makes it possible for ASF to store and use temporary one-time use login key for next logon attempt, effectively skipping a need of providing password, Steam Guard or 2FA code as long as our login key is valid. Login key is stored in <code>BotName.db` file and updated automatically. This is why you don't need to provide password/SteamGuard/2FA code after logging in successfully with ASF just once.

Login keys are used by default for your convenience, so you don't need to input `SteamPassword`, SteamGuard or 2FA code (when not using **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)**) on each login. It's also superior alternative since login key can be used only for a single time and does not reveal your original password in any way. Exactly the same method is being used by your original Steam client, which saves your account name and login key for your next logon attempt, effectively being the same as using `SteamLogin` with `UseLoginKeys` and empty `SteamPassword` in ASF.

However, some people could be concerned even about this little detail, therefore this option is available here for you if you'd like to ensure that ASF won't store any kind of token that would allow resuming previous session after being closed, which will result in full authentication being mandatory on each login attempt. Disabling this option will work exactly the same as not checking "remember me" in official Steam client. Unless you know what you're doing, you should keep it with default value of `true`.

* * *

## Structures des répertoires

ASF utilise une structure de fichier assez simple.

    ├── config
    │     ├── ASF.json
    │     ├── ASF.db
    │     ├── Bot1.json
    │     ├── Bot1.db
    │     ├── Bot1.bin
    │     ├── Bot2.json
    │     ├── Bot2.db
    │     ├── Bot2.bin
    │     └── ...
    ├── ArchiSteamFarm.dll
    ├── log.txt
    └── ...
    

In order to move ASF to new location, for example another PC, it's enough to move/copy `config` directory alone, and that's the recommended way of doing any form of "ASF backups", since you can always download the remaining (program) part from the GitHub, while not risking corrupting internal ASF files, e.g. through a faulty backup.

Le fichier ` log.txt </ 0> contient le journal généré par votre dernière exécution ASF. Ce fichier ne contient aucune information sensible et est extrêmement utile lorsqu'il s'agit de problèmes, de plantages ou simplement en tant qu'informations sur ce qui s'est passé lors de la dernière exécution d'ASF. We will very often ask about this file if you run into issues or bugs. ASF gère automatiquement ce fichier pour vous, mais vous pouvez modifier davantage le module de journalisation ASF <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Logging">logging</a></strong> si vous êtes un utilisateur expérimenté.</p>

<p>Le répertoire <code>config` est l'emplacement où se trouve la configuration pour ASF, y compris tous ses bots.

`ASF.json` est un fichier de configuration ASF global. Cette configuration est utilisée pour spécifier comment ASF se comporte comme un processus qui affecte tous les bots et le programme lui-même. Vous pouvez y trouver des fonctions globales, telles que le propriétaire du processus ASF, les mises à jour automatiques ou le debug.

`BotName.json` est une configuration d'instance de bot. Cette configuration est utilisée pour spécifier le comportement d'une instance de bot. Par conséquent, ces paramètres sont spécifiques à ce bot uniquement et ne sont pas partagés entre eux. Cela vous permet de configurer des bots avec différents paramètres et ne fonctionnant pas nécessairement tous de la même manière. Every bot is named using unique identifier, chosen by you in place of `BotName`.

Outre les fichiers de configuration, ASF utilise également le répertoire `config` pour stocker les bases de données.

`ASF.json` est un fichier de configuration ASF global. Il agit comme un stockage persistant global et est utilisé pour enregistrer diverses informations liées au processus ASF, telles que les adresses IP des serveurs Steam locaux. **Vous ne devez pas éditer ce fichier**.

`BotName.db` est une base de données d'instance de bot. Ce fichier est utilisé pour stocker des données cruciales relatives à une instance de bot dans un stockage persistant, telles que des clés de connexion ou ASF 2FA. **Vous ne devez pas éditer ce fichier**.

`BotName.bin` est un fichier spécial d'une instance de bot, qui contient des informations sur le hash Sentry de Steam. Sentry est utilisé pour l'authentification à l'aide du mécanisme `SteamGuard`, très similaire au fichier Steam `ssfn`. **Vous ne devez pas éditer ce fichier**.

`BotName.keys` est un fichier spécial qui peut être utilisé pour importer des clés dans **[background de jeux d’arrière-plan](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer)**. Ce n'est pas obligatoire ni généré, mais reconnu par ASF. Ce fichier est automatiquement supprimé une fois les clés importées.

`BotName.maFile` est un fichier spécial qui peut être utilisé pour importer **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)**. Ce n'est pas obligatoire ni généré, mais reconnu par ASF si votre `BotName` n'utilise pas encore ASF 2FA. Ce fichier est automatiquement supprimé une fois ASF 2FA importé avec succès.

* * *

## JSON mapping

Chaque fonction de configuration a son type. Le type de la fonction définit les valeurs qui lui sont valables. Vous ne pouvez utiliser que des valeurs valides pour un type donné. Si vous utilisez une valeur non valide, ASF ne pourra pas analyser votre configuration.

**Nous vous recommandons vivement d’utiliser ConfigGenerator pour générer des configs** - il gère la plupart des tâches de bas niveau (telles que la validation des types), il vous suffit donc de saisir les valeurs appropriées, et vous n'avez pas besoin de comprendre les types de variable spécifiés ci-dessous. Cette section est principalement destinée aux personnes générant / modifiant des configurations manuellement afin qu’elles sachent quelles valeurs elles peuvent utiliser.

Les types utilisés par ASF sont des types C # natifs, spécifiés ci-dessous:

* * *

`bool` - type Boolean accepte uniquement les valeurs `true` et `false`.

Exemple : `"Enabled": true`

* * *

`byte` - Unsigned byte type, accepting only integers from `0` to `255` (inclusive).

Exemple: `"ConnectionTimeout": 60`

* * *

`ushort` - Unsigned short type, accepting only integers from `0` to `65535` (inclusive).

Exemple: `"WebLimiterDelay": 300`

* * *

`uint` - Unsigned integer type, accepting only integers from `0` to `4294967295` (inclusive).

* * *

`ulong` - Unsigned long integer type, accepting only integers from `0` to `18446744073709551615` (inclusive).

Example: `"SteamMasterClanID": 103582791440160998`

* * *

`string` - String type, accepting any sequence of characters, including empty sequence `""` and `null`. Empty sequence and `null` value are treated the same by ASF, so it's up to your preference which one you want to use (we stick with `null`).

Exemples: `"SteamLogin": null`, `"SteamLogin": ""`, `"SteamLogin": "MyAccountName"`

* * *

`ImmutableHashSet<valueType>` - Immutable collection (set) of unique values in given `valueType`. In JSON, it's defined as array of elements in given `valueType`. ASF uses `HashSet` to indicate that given property makes sense only for unique values, therefore it'll intentionally ignore any potential duplicates during parsing (if you happened to supply them anyway).

Example for `ImmutableHashSet<uint>`: `"Blacklist": [267420, 303700, 335590]`

* * *

`ImmutableDictionary<keyType, valueType>` - Immutable dictionary (map) that maps a unique key specified in its `keyType`, to value specified in its `valueType`. In JSON, it's defined as an object with key-value pairs. Keep in mind that `keyType` is always quoted in this case, even if it's value type such as `ulong`. There is also a strict requirement of the key being unique across the map, this time enforced by JSON as well.

Exemple pour `ImmutableDictionary<ulong, byte>`: `"SteamUserPermissions": { "76561198174813138": 3, "76561198174813137": 1 }` 

* * *

`flags` - L'attribut Flags combine plusieurs propriétés différentes en une valeur finale en appliquant des opérations au niveau du bit. Cela vous permet de choisir toute combinaison possible de différentes valeurs autorisées en même temps. La valeur finale est construite comme la somme des valeurs de toutes les options activées.

Par exemple, à partir des valeurs suivantes:

| Valeur  | Nom    |
| ------- | ------ |
| 0       | Aucune |
| 1       | A      |
| 2       | B      |
| 4       | C      |

Using `B + C` would result in value of `6`, using `A + C` would result in value of `5`, using `C` would result in value of `4` and so on. This allows you to create any possible combination of enabled values - if you decided to enable all of them, making `None + A + B + C`, you'd get value of `7`. Notez également que le drapeau avec la valeur `0` est activé par définition dans toutes les autres combinaisons disponibles. Il s'agit donc très souvent d'un drapeau qui n'active rien de particulier (tel que `None`). 

So as you can see, in above example we have 3 available flags to switch on/off (`A`, `B`, `C`), and `8` possible values overall:

- `None -> 0`
- `A -> 1`
- `B -> 2`
- `A+B -> 3`
- `C -> 4`
- `A+C -> 5`
- `B+C -> 6`
- `A+B+C -> 7`

Example: `"SteamProtocols": 7`

* * *

## Mode de compatibilité

En raison de limitations JavaScript empêchant de sérialiser correctement les champs `ulong` simples dans JSON lors de l'utilisation de ConfigGenerator sur le Web, les champs `ulong` seront restitués sous forme de chaînes avec `s_` préfixe dans la configuration résultante. Cela inclut par exemple `"SteamOwnerID": 76561198006963719` qui sera écrit par notre ConfigGenerator en tant que `"s_SteamOwnerID": "76561198006963719"`. ASF inclut une logique appropriée pour la gestion automatique de ce mappage de chaîne. Les entrées `s _` de vos configurations sont donc valides et correctement générées. Si vous générez vous-même des configurations, nous vous recommandons, si possible, de vous en tenir aux champs d'origine `ulong`, mais en cas d'impossibilité, vous pouvez également suivre ce schéma et les encoder sous forme de chaînes avec <0 >s_ </code> ajouté à leurs noms. Nous espérons résoudre cette limitation de JavaScript éventuellement.

* * *

## Compatibilité des configurations

La priorité absolue pour ASF de rester compatible avec les anciennes configurations. Comme vous le savez sûrement déjà, les propriétés de configuration manquantes sont traitées de la même manière que celles définies avec leurs **valeurs par défaut**. Par conséquent, si une nouvelle propriété de configuration est introduite dans la nouvelle version d’ASF, toutes vos configurations resteront **compatibles** avec la nouvelle version, et ASF traitera cette nouvelle propriété de configuration telle qu’elle serait définie avec sa valeur **valeur par défaut**. Vous pouvez toujours ajouter, supprimer ou modifier les propriétés de configuration en fonction de vos besoins. We recommend to limit defined config properties only to those that you want to change, since this way you automatically inherit default values for all other ones, not only keeping your config clean but also increasing compatibility in case we decide to change a default value for property that you don't want to explicitly set yourself (e.g. `WebLimiterDelay`).

* * *

## Auto-reload

Depuis ASF V2.1.6.2 +, le programme est maintenant conscient des modifications de configuration à la volée. Grâce à cela, ASF va automatiquement:

- Créez (et démarrez, si nécessaire) une nouvelle instance de bot lorsque vous créez sa configuration
- Arrêtez (si nécessaire) et supprimez l'ancienne instance de bot lorsque vous supprimez sa configuration
- Arrêtez (et démarrez, si nécessaire) toute instance de bot lorsque vous modifiez sa configuration
- Redémarrez (si nécessaire) le bot sous un nouveau nom lorsque vous renommez sa configuration

Tout ce qui précède est transparent et se fera automatiquement sans qu'il soit nécessaire de redémarrer le programme ou de tuer d'autres instances de bot (non affectées).

En plus de cela, ASF redémarrera également de lui-même (si ` AutoRestart ` le permet) si vous modifiez ASF de base ` ASF.json ` config. De même, le programme quittera si vous le supprimez ou le renommez.
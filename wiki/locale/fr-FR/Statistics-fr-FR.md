# Statistiques

Le développement d’ASF est pris en charge par 3 choses principales : dons, avis des utilisateurs et statistiques. Les dons influencent directement nos possibilités pour travailler sur le projet, l'avis des utilisateurs est toujours agréable à lire (surtout positive), alors que les statistiques nous fournissent des informations d’utilisation de notre logiciel ainsi que le nombre de personnes - de cette façon nous pouvons savoir ce qu’il faut améliorer, résoudre et voir sur ce qu’il faut se concentrer.

ASF, par défaut, dans les paramètres de configuration global a d'activer `Statistics` . Si vous voulez voir de nouvelles versions à venir, avec bugs corrigés et des nouveautés cette mise en place est préférable d'être garder, afin que nous fassions usage de ces données pour vous fournir un meilleur logiciel (mais pas seulement). Ceci est particulièrement important parce que ASF est proposé **gratuitement**, et c’est le moins que vous pouvez faire pour dire Merci - **dites-nous que vous utilisez ASF**, car c’est ce que font principalement nos statistiques actuelles. ASF does not collect or make use of any data that could be considered private and/or used against you. We keep the usage of statistics to bare minimum, and every single information being collected requires from us to precisely state it here, together with practical explanation. This gives you full insight into what we collect, for what purpose, and how it's supposed to help. All of that info can be found further below.

* * *

## Politique de confidentialité actuelle

Lorsque les `statistiques` sont actives, les choses suivantes arriveront :

1. Account being used in ASF will join our **[Steam group](https://steamcommunity.com/gid/103582791440160998)** upon login. Pour trois raisons :

* Il **vous** fournit des annonces, surtout concernant les nouvelles versions, les questions cruciales, problèmes Steam et autres choses qui sont importantes afin de maintenir la communauté à jour (pas de spam avec ASF garanti)
* Il **vous**permet d’utiliser notre support technique, en posant des questions, résoudre les problèmes, ou suggérer des améliorations
* Il nous permet de voir combien de comptes Steam réels est utilisés par ASF

We consider Steam group as a crucial part in regards to ASF community. This is our main communication channel which we use for all important matters in regards to ASF, especially keeping you up-to-date with the development, potential issues, eventual warnings and all other matters that you should have access to as an user. We do not benefit from maintenance of that group in any way, it's the place dedicated to ASF users and we consider you part of our community. Since membership of the group in no way identifies you as ASF user, we do not consider this to be a problem in terms of privacy.

2. If your account is **[unrestricted](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663)**, using **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication#asf-2fa)**, has **[public inventory](https://steamcommunity.com/my/edit/settings)** with at least 100 `MatchableTypes` items in it and you intentionally enabled `SteamTradeMatcher` in your `TradingPreferences`, then ASF will periodically communicate with our **[server](https://asf.justarchi.net)** in order to fulfill the enabled functionality. Les données réelles se compose de l’ID unique de ASF (généré par ASF) et des informations suivantes concernant le compte :

* Votre identificateur Steam (sous forme 64 bits, pour générer des liens)
* Votre pseudo (pour l'affichage)
* Votre avatar (hash, pour l'affichage)
* Votre ** jeton de d'échange </ 0> (afin que des personnes ne faisant pas partie de votre liste d'amis puissent vous envoyer des échanges)</li> 
    
    * Your `MatchableTypes` (for display purposes and matching)
    * Value of `MatchEverything` in your `TradingPreferences` (for display purposes and matching)
    * Total number of `MatchableTypes` Steam items in your inventory (for display purposes and matching)
    * Total number of unique games that above `MatchableTypes` Steam items are made of (for display purposes and matching)</ul> 
    
    ASF ** ne </ 0> collectera aucune autre donnée non répertoriée ci-dessus sans préavis important dans le journal des modifications, ce qui constitue une très bonne raison en premier lieu We do not consider anything above to be a serious matter, and we mention it to let you know what precisely ASF does apart of what you configured it to do yourself, so people can better understand our point of view.</p> 
    
    * * *
    
    # Données d'utilisation
    
    All values specified in second point are being used for our **Public ASF STM listing**, which is explained below. We do not use any other data for any other purpose.
    
    * * *
    
    ## Liste publique ASF STM
    
    Our public ASF STM listing is located on **[our website](https://asf.justarchi.net/STM)** and used as a public service for both ASF users that make use of `MatchActively`, as well as ASF and non-ASF users for manual matching.
    
    ### Comment ça marche exactement
    
    ASF envoie les données initiales une fois après la connexion, contenant toutes les propriétés que la liste publique utilise. Ensuite, toutes les 10 minutes, ASF envoie une très petite requête de type "pulsation" qui informe notre serveur que le bot est toujours opérationnel. Si, pour une raison quelconque, la pulsation n’arrivait pas, par exemple à cause de problèmes de réseau, ASF tentera de l’envoyer à chaque minute, jusqu’à ce que le serveur l’enregistre.
    
    Cela permet à notre site Web d'enregistrer les comptes pouvant être utilisés pour le matching, ainsi que s'ils sont toujours actifs. Grâce à cela, notre site Web peut afficher tous les comptes ASF 2FA + STM actifs au cours des ** 15 dernières minutes </ 0>.</p> 
    
    Users are sorted according to their inventories (in descending order) - `MatchEverything` bots with `Any` banner that accept all 1:1 trades, then `MatchableTypes` unique games count, and finally `MatchableTypes` items count.
    
    Veuillez noter que vous ne serez **pas** affiché sur le site si vous ne remplissez pas toutes les exigences. ASF won't even bother communicating with our server in this case, so second point is entirely skipped for you if you didn't intentionally enable `SteamTradeMatcher` in order to help yourself match dupes. Also public listing is compatible only with latest stable version of ASF and may refuse to display outdated bots, especially if they're missing core functionality that can be found only in newer versions.
    
    ### API
    
    ASF STM accepte seulement les bots ASF pour l’instant. Il n’y a aucun moyen pour répertorier les bots de tiers sur notre liste pour l’instant (nous ne pouvons pas revoir leur code facilement et s’assurer qu’ils répondent à notre toute logique commerciale).
    
    Si vous cherchez un moyen facile de notre liste d’accès de manière programmatique, nous avons un point de **/Api/Bots</0 très simple que vous pouvez utiliser. This is also the endpoint that ASF uses internally for `MatchActively` users.</p> 
    
    ### Notice
    
    *L'ensemble du concept, ainsi que l'intégration du site Web et les rapports ASF, sont toujours en version bêta - ils peuvent être améliorés / modifiés au fil du temps - également supprimés si nous estimons que l'intérêt pour cette fonctionnalité n'est pas suffisante.*
    
    * * *
    
    ## Désinscription
    
    La participation dans les statistiques n’est **pas obligatoire**, mais fortement recommandé pour l’avenir du programme. Nous ne vous jugeons pas, et si vous avez l'envie de cacher le fait que vous êtes un utilisateur ASF, vous pouvez désactiver complètement les statistiques en changeant `Statistics`de la configuration global par `false`. Les statistiques désactivées rendent tout le module non opérationnel, et ne feront aucune des actions spécifiées dans notre politique de confidentialité ci-dessus.
    
    Disabling statistics may influence **our technical support, ASF functionality and other things that are offered to you for free**. For example, you can't make use of `MatchActively` without having `Statistics` enabled (as you refuse to communicate with our server for the functionality to work).
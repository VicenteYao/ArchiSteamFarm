# Partage Familial Steam

ASF prend en charge le partage de famille Steam depuis la version 2.1.5.5+. Pour comprendre comment ASF fonctionne avec cela, vous devez d’abord lire le fonctionnement du **[Partage familial](https://store.steampowered.com/promotion/familysharing)**, disponible sur le magasin Steam.

* * *

## ASF

La prise en charge de la fonctionnalité du partage familial dans ASF est transparente, ce qui signifie qu’elle ne présente aucune nouvelle propriété de configuration bot / process. Elle fonctionne immédiatement en tant que fonctionnalité intégrée supplémentaire.

ASF inclut une logique appropriée afin de s’assurer que la bibliothèque est verrouillée par les utilisateurs qui partagent en famille. Par conséquent, elle ne les "coupera" pas de la session en raison du lancement d’une partie. ASF agira exactement de la même manière que le compte principal qui détient le verrou. Par conséquent, si ce verrou est détenu par votre client steam ou par l’un de vos utilisateurs partageant la famille, ASF ne tentera pas de créer une ferme, mais attendra que le verrou soit libre.

En plus de ce qui précède, une fois connecté, ASF accédera à vos **[ paramètres de partage de jeux ](https://store.steampowered.com/account/managedevices)**, à partir desquels il extraira jusqu'à 5 ` steamIDs ` autorisés à utiliser votre bibliothèque. Ces utilisateurs reçoivent l’autorisation ` PartageFamilial` d’utiliser **[les commandes](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**, leur permettant notamment d’utiliser la commande ` pause ~ ` sur le compte bot qui partage des jeux avec eux, qui permet de mettre en pause le module de farm automatique de cartes afin de lancer un jeu pouvant être partagé.

La connexion des deux fonctionnalités décrites ci-dessus permet à vos amis de ` mettre en pause ~` le processus de farm de cartes, de démarrer une partie, de jouer aussi longtemps qu’ils le souhaitent, puis, une fois la lecture terminée, le processus de farm sera automatiquement repris par ASF. Bien entendu, l'émission de ` pause ~` n'est pas nécessaire si ASF n'effectue actuellement aucune activité de farm active, car vos amis peuvent lancer le jeu immédiatement, et la logique décrite ci-dessus garantit qu'ils ne seront pas expulsés du serveur.

* * *

## Restriction

Steam network loves to mislead ASF by broadcasting false status updates, which may lead to ASF believing it's fine to resume process, and in result kick your friend too soon. This is exactly the same issue as the one already explained by us in **[this FAQ entry](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ#asf-is-kicking-my-steam-client-session-while-im-playing--this-account-is-logged-on-another-pc)**. We recommend exactly the same solutions, mainly promoting your friend to `Operator` permission (or above) and telling him to make use of `pause` and `resume` commands.
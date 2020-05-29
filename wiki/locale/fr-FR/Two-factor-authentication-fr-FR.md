# Authentification à deux facteurs

Depuis plusieurs années, Valve a mis en place un système de sécurité qui requiert un appareil supplémentaire pour pouvoir effectuer diverses actions liées au compte (comme les échanges, les ventes, ou la connection). Vous pouvez en apprendre plus par rapport à ce système **[ici](https://support.steampowered.com/kb_article.php?ref=1284-WTKB-4729)** et **[ici](https://support.steampowered.com/kb_article.php?ref=8078-TPHC-6195)**. Il est important de comprendre la logique du système 2FA, avant d'essayer de comprendre celle du 2FA de ASF.

Comme vous aurez pu le remarquer, tous les échanges venant de compte sans authentification 2FA sont bloqués pendant 15 jours, ce qui, malgré le fait que ce n'est pas un problème majeur par rapport à l'utilisation d'ASF, peut être assez problématique, surtout pour les personnes souhaitant profiter d'une automatisation complète. Heureusement, ASF inclut une solution à ce problème, qui est appelée ASF 2FA.

* * *

# Logique de ASF

Peu importe si vous utilisez le ASF 2FA expliqué ci-dessous ou non, ASF inclut une logique qui lui permet de définir quels comptes sont protégés par le 2FA. It will ask you for required details when they're needed (such as during logging in). Mais si vous utilisez ASF 2FA, le programme pourra passer ces vérifications et générer automatiquement les codes nécessaires, ce qui pourra vous éviter les ennuis et vous permettra d'avoir accès à des fonctionnalités supplémentaires (décrites ci-dessous).

* * *

# ASF 2FA

ASF 2FA is built-in module responsible for providing 2FA features to ASF process, such as generating tokens and accepting confirmations. Il fait double emploi avec votre authentificateur existante, il n’est pas nécessaire d’utiliser exclusivement le 2FA d'ASF.

You can verify whether your bot account is using ASF 2FA already by executing `2fa` **[commands](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. Unless you've already imported your authenticator as ASF 2FA, all `2fa` commands will be non-operative, which means that your account is not using ASF 2FA, therefore it's also ineligible for advanced ASF features that require the module to be operative.

Pour activer ASF 2FA, vous devez avoir :

- Authentificateur steam actif sur votre Android
- Authentificateur steam actif sur votre IOS
- ou authentificateur steam actif sur **[SteamDesktopAuthenticator](https://github.com/Jessecar96/SteamDesktopAuthenticator)**
- ou authentificateur steam actif sur **[WinAuth](https://winauth.github.io/winauth)**
- or any other working implementation of Steam authenticator with access to shared/identity secret and device's ID

* * *

## Importation

In order to complete the steps explained below, you should have already linked and operational authenticator that is supported by ASF. ASF currently supports a few different sources of 2FA - Android, iOS, SteamDesktopAuthenticator and WinAuth. If you don't have any authenticator yet, you need to choose one of those and set it up firstly. If you don't know better which one to pick, we recommend WinAuth, but any of the above will work fine assuming you follow the instructions.

Tous les guides suivants exigent que vous ayez déjà un authentificateur ** fonctionnel et opérationnel </ 0> utilisé avec un outil / ou une application tiers. ASF 2FA ne fonctionnera pas correctement si vous importez des données non valides. Assurez-vous donc que votre authentificateur fonctionne correctement avant de tenter de l'importer. Cela inclut de tester et de vérifier que les fonctions suivantes de l'authentificateur fonctionnent correctement:</p> 

- Vous pouvez générer des jetons et ces jetons sont acceptés par le réseau Steam 
- Vous pouvez récupérer les confirmations et elles arrivent sur votre authentificateur mobile
- Vous pouvez accepter ces confirmations et elles sont correctement reconnues par le réseau Steam comme confirmées / rejetées.

Assurez-vous que votre authentificateur fonctionne en vérifiant si les actions ci-dessus fonctionnent. Si elles ne fonctionnent pas, elles ne fonctionneront pas non plus dans ASF, vous perdrez seulement du temps et vous causerez des problèmes.

* * *

### Téléphone Android

En général, pour l’importation de l'authentificateur depuis votre téléphone Android, vous aurez besoin de l'accès **[root](https://en.wikipedia.org/wiki/Rooting_(Android_OS))**. La méthode pour effectué un root varie d’un appareil à l’autre, donc je ne vous dirai pas comment root votre périphérique. Visit **[XDA](https://www.xda-developers.com/root)** for excellent guides on how to do that, as well as general information on rooting in general. If you can't find your device or the guide that you need, try to find it on google second.

At least officially, it's not possible to access protected Steam files without root. The only official non-root method for extracting Steam files is creating unencrypted `/data` backup in one way or another and manually fetching appropriate files from it on your PC, however because such thing highly depends on your phone manufacturer and **is not** in Android standard, we won't discuss it here. Si vous avez de la chance d'avoir une telle fonctionnalité, vous pouvez l'utiliser, mais la majorité des utilisateurs n'ont rien de cela.

Unofficially, it is possible to extract the needed files without root access, by installing or downgrading your Steam app to version 2.1 (or earlier), setting up mobile authenticator and then creating a snapshot of the app (together with the `data` files that we need) through `adb backup`. However, since it's a serious security breach and entirely unsupported way to extract the files, we won't elaborate further on this, Valve disabled this security hole in newer versions for a reason, and we only mention it as a possibility.

Assuming that you've successfully rooted your phone, you should afterwards download any root explorer available on the market, such as **[this one](https://play.google.com/store/apps/details?id=com.jrummy.root.browserfree)** (or any other one of your preference). You can also access the protected files through ADB (Android Debug Bridge) or any other available to you method, we'll do it through the explorer since it's definitely the most user-friendly way.

Once you opened your root explorer, navigate to `/data/data` folder. N'oubliez pas que le répertoire `/data/data` est protégé et que vous ne pourrez pas y accéder sans accès root. Once there, find `com.valvesoftware.android.steam.community` folder and copy it to your `/sdcard`, which points to your built-in internal storage. Afterwards, you should be able to plug your phone to your PC and copy the folder from your internal storage like usual. If by any chance the folder won't be visible despite you being sure that you copied it to the right place, try restarting your phone first.

Now, you can choose if you want to import your authenticator to WinAuth first, then to ASF, or to ASF right away. La première option est plus pratique et vous permet de dupliquer votre authentificateur également sur votre PC, vous permettant ainsi d’effectuer des confirmations et de générer des jetons à partir de 3 endroits différents - votre téléphone, votre PC et ASF. If you want to do that, simply open WinAuth, add new Steam authenticator and choose importing from Android option, then follow instructions by accessing the files that you've obtained above. When done, you can then import this authenticator from WinAuth to ASF, which is explained in dedicated WinAuth section below.

If you don't want to or don't need to go through WinAuth, then simply copy `files/Steamguard-SteamID` file from our protected directory, where `SteamID` is your 64-bit Steam identificator of the account that you want to add (if more than one, because if you have only one account then this will be the only file). You need to place that file in ASF's `config` directory. Once you do that, rename the file to `BotName.maFile`, where `BotName` is the name of your bot you're adding ASF 2FA to. Après cette étape, lancer ASF - il devrait remarquer la `.maFile` et l’importer.

    [*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
    <1> Please enter your Device ID (including "android:"):
    

You will need to do only one more step - find your `DeviceID` property in `shared_prefs/steam.uuid.xml`. Ce sera à l'intérieur des balises XML et commençant par `android:`. Copy that (or write it down) and put it in ASF as asked. Si vous avez tout fait correctement, l'importation devrait être terminée.

    [*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
    

Veuillez confirmer que l'acceptation des confirmations fonctionne réellement. Si vous faites une erreur en entrant votre `DeviceID`, vous aurez un authentificateur à moitié cassé - les jetons fonctionneront, mais l'acceptation des confirmations ne fonctionneront pas. Vous pouvez toujours supprimer `Bot.db` et recommencer si nécessaire.

* * *

### ios

Pour iOS, vous pouvez utiliser **[ios-steamguard-extracteur](https://github.com/CaitSith2/ios-steamguard-extractor)**. Cela est possible grâce au fait que vous pouvez effectuer une sauvegarde décryptée, la placer sur votre PC et utiliser l'outil afin d'extraire des données Steam qui seraient autrement impossibles à obtenir (du moins sans jailbreak, en raison du cryptage iOS).

Rendez-vous sur ** dernière version </ 0> pour télécharger le programme. Une fois les données extraites, vous pouvez les insérer, par exemple. dans WinAuth, puis de WinAuth à ASF (bien que vous puissiez également simplement copier un fichier json généré à partir de `{` se terminant par `}` dans `BotName.maFile` et continuer comme d'habitude). Si vous me le demandez, je vous recommande fortement d'importer d'abord dans WinAuth, puis de vous assurer que les deux types de génération de jetons et d'acceptation des confirmations fonctionnent correctement. Vous pouvez ainsi être sûr que tout va bien. Si vos informations d'identification ne sont pas valides, ASF 2FA ne fonctionnera pas correctement. Il est donc préférable de faire de la dernière étape d'importation ASF.</p> 

Pour les questions / problèmes, consultez la page ** questions </ 0>.</p> 

*N’oubliez pas que l’outil ci dessus n’est pas officiel, vous l’utilisez à vos propres risques. Nous n’offrons pas de support technique, si cela ne fonctionne pas correctement - nous avons quelques signaux que c'est lexportateur 2FA qui n'a pas fonctionné correctement - vérifier que les confirmations fonctionne dans un authentificateur comme WinAuth avant d’importer ces données à ASF !*

* * *

### SteamDesktopAuthenticator

Si vous avez votre authentificateur SDA déjà en fonctionnement, vous devriez remarquer que le fichier `steamID.maFile` est disponible dans le dossier `maFiles`. Copiez ce fichier dans le répertoire ` config </ 0> de ASF. Assurez-vous que <code>.maFile` est sous forme non cryptée, comme ASF ne peut pas déchiffrer les fichiers SDA - le contenu non chiffrée du fichier doit commencer par `{`.

Une fois que votre fichier est sur votre PC, saisissez-le sous la forme `BotName.maFile</ 0> dans le répertoire de configuration ASF, où <code>BotName</ 0> correspond au nom de votre bot auquel vous ajoutez ASF 2FA. Sinon, vous pouvez le laisser tel quel, ASF le sélectionnera automatiquement après vous être connecté. Aider ASF permet d’utiliser ASF 2FA avant la connexion. Si vous ne voulez pas aider ASF, le fichier ne peut être sélectionné qu’après que ASF s’est connecté (car ASF ne connaît pas <code> steamID </ 0> de votre compte avant de vous connecter).</p>

<p>Si vous avez tout fait correctement, lancez ASF, et vous devriez remarquer:</p>

<pre><code>[*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
[*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
`</pre> 

Votre ASF 2FA devrait désormais être opérationnel pour ce compte.

* * *

### WinAuth

Une fois que votre fichier est sur votre PC, saisissez-le sous la forme `BotName.maFile</ 0> dans le répertoire de configuration ASF, où <code>BotName</ 0> correspond au nom de votre bot auquel vous ajoutez ASF 2FA. N'oubliez pas qu'il doit s'agir de <code> BotName.maFile </ 0> et NON <code> BotName.maFile.txt </ 0>, Windows  masque parfois les extensions connues par défaut. Si vous indiquez un nom incorrect, il ne sera pas sélectionné par ASF.</p>

<p>Maintenant, lancez WinAuth comme d’habitude. Faites un clic droit sur l'icône Steam et sélectionnez "Afficher le code SteamGuard et le code de récupération". Puis cochez "Autoriser la copie". Vous devez connaître la structure JSON que vous connaissez bien en bas de la fenêtre, en commençant par <code> {</ 0>. Copiez le texte entier dans un fichier <code> BotName.maFile </ 0> créé par vous même à l'étape précédente.</p>

<p>Si vous avez tout fait correctement, lancez ASF, et vous devriez remarquer:</p>

<pre><code>[*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
<1> Please enter your Device ID (including "android:"):
`</pre> 

C'est à ce moment que la partie délicate commence. WinAuth ne possède pas la propriété deviceID requise par ASF. Vous devez donc effectuer une dernière tâche.

Retournez à « SteamGuard et Code de récupération » de WinAuth et vous devriez remarquer « Device ID » au-dessus du code JSON que vous avez copié il y pas si longtemps. Copier l’identité entière de l’appareil Android, y compris la partie ` android: </ 0>, dans ASF.</p>

<p>Si vous avez fait cela correctement aussi , vous avez maintenant terminé !</p>

<pre><code>[*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
`</pre> 

Veuillez confirmer que l'acceptation des confirmations fonctionne réellement. Si vous faites une erreur en entrant votre `DeviceID`, vous aurez un authentificateur à moitié cassé - les jetons fonctionneront, mais l'acceptation des confirmations ne fonctionneront pas. Vous pouvez toujours supprimer `Bot.db` et recommencer si nécessaire.

* * *

## Done

From this moment, all `2fa` commands will work as they'd be called on your classic 2FA device. Vous pouvez utiliser ASF 2FA et l'authentificateur de votre choix (Android, iOS, SDA ou WinAuth) pour générer des jetons et accepter les confirmations.

Si vous avez l’authentificateur sur votre téléphone, vous pouvez éventuellement supprimer SteamDesktopAuthenticator et/ou WinAuth, car vous n'en aurez plus réellement besoin. Cependant, je conseille de le garder juste au cas où, pour ne pas dire qu’il est plus maniable que l’authentificateur Steam ordinaire. Juste n’oubliez pas que ASF 2FA est **pas** l'authentificateur principal et il ne devrait **jamais** être le seul que vous utilisez, car il ne comprend même pas toutes les données que l'authentificateur devrait avoir. Il n'est pas possible de reconvertir ASF 2FA en authentificateur d'origine. Par conséquent, assurez-vous toujours que vous avez un authentificateur à usage général ailleurs, par exemple dans WinAuth / SDA, ou sur votre téléphone.

* * *

## FAQ

### Comment ASF utilise-t-il le module 2FA?

Si ASF 2FA est disponible, ASF l'utilisera pour la confirmation automatique des transactions en cours d'envoi / acceptées par ASF. Il sera également capable de générer automatiquement des jetons 2FA au besoin, par exemple pour se connecter. De plus, ASF 2FA active également les commandes ` 2fa </ 0> que vous pouvez utiliser. Cela devrait être tout pour le moment, si je n'ai rien oublié - ASF utilise essentiellement le module 2FA au besoin.</p>

<hr />

<h3>Et si j'ai besoin d'un jeton 2FA?</h3>

<p>Vous aurez besoin du jeton 2FA pour accéder à un compte protégé par 2FA, qui comprend également chaque compte ASF 2FA. Vous devez générer des jetons dans l'authentificateur que vous avez utilisés pour l'importation, mais vous pouvez également générer des jetons temporaires via la commande <code> 2fa </ 0> envoyée via le chat à un bot. Vous pouvez également utiliser la commande <code> 2fa &lt;BotNames&gt; </ 0> pour générer un jeton temporaire pour des instances de bot. Cela devrait vous suffire pour accéder à des comptes de bot via, par exemple, navigateur, mais comme indiqué ci-dessus - vous devez utiliser votre authentificateur  (Android, iOS, SDA ou WinAuth) à la place.</p>

<hr />

<h3>Puis-je utiliser mon authentificateur d'origine après l'avoir importé au format ASF 2FA?</h3>

<p>Oui, votre authentificateur d'origine reste fonctionnel et vous pouvez l'utiliser avec ASF 2FA. C’est l’essentiel du processus: nous importons vos informations d’authentification dans ASF afin qu’ASF puisse les utiliser et accepter les confirmations sélectionnées en votre nom.</p>

<hr />

<h3>Où l'authentificateur mobile ASF est-il enregistré?</h3>

<p>L’authentificateur mobile ASF est enregistré dans le fichier <code>BotName.db</ 0> de votre répertoire de configuration, avec d’autres données cruciales relatives à un compte. Si vous souhaitez supprimer ASF 2FA, lisez la procédure ci-dessous.</p>

<hr />

<h3>Comment supprimer ASF 2FA?</h3>

<p>Arrêtez simplement ASF et supprimez le fichier <code>BotName.db</ 0>  du bot associé à ASF 2FA que vous souhaitez supprimer. Cette option supprimera les fichiers 2FA importés associés à ASF, mais ne supprimera PAS votre authentificateur. Si vous souhaitez plutôt désactivé votre authentificateur, mis à part le supprimer d'ASF (en premier lieu), vous devez le désactivé dans l'authentificateur de votre choix (Android, iOS, SDA ou WinAuth), ou - si vous ne pouvez pas pour une raison quelconque, utilisez le code de révocation que vous avez reçu lors de la liaison de cet authentificateur sur le site Web de Steam. It's not possible to unlink your authenticator through ASF, this is what general-purpose authenticator that you already have should be used for.</p>

<hr />

<h3>J'ai lié l'authentifiant dans SDA / WinAuth, puis importé dans ASF. Can I now unlink it and link it again on my phone?</h3>

<p><strong>Non</strong> ASF <strong> importe </ 0> vos données d'authentificateur pour pouvoir les utiliser. Si vous désactivé votre authentificateur, ASF 2FA cessera de fonctionner, que vous le supprimiez ou non comme indiqué ci-dessus. Si vous souhaitez utiliser votre authentificateur à la fois sur votre téléphone et sur ASF (plus éventuellement dans SDA / WinAuth), vous devez <strong> importer </ 0> votre authentificateur à partir de votre téléphone et ne pas en créer un nouveau dans SDA / WinAuth. Vous ne pouvez avoir seulement <strong> un </ 0> authentificateur lié, c’est pourquoi ASF <strong> importe </ 0> cet authentificateur et ses données afin de l’utiliser en tant que ASF 2FA - <strong> identique </ 0> authentificateur, juste existant à deux endroits. Si vous décidez de supprimer les informations d'identification de votre authentificateur mobile, quelle que soit la méthode utilisée, ASF 2FA cessera de fonctionner, car les informations d'identification de l'authentificateur mobile précédemment copiées ne seront plus valides. Pour utiliser ASF 2FA avec Authenticator sur votre téléphone, vous devez l'importer à partir d'Android / iOS, comme décrit ci-dessus.</p>

<hr />

<h3>L'utilisation de ASF 2FA est-elle meilleure que l'authentificateur WinAuth / SDA /  pour accepter toutes les confirmations?</h3>

<p><strong> Oui </ 0>, à plusieurs niveaux. Le premier et le plus important - utiliser ASF 2FA <strong> de manière significative </ 0> augmente votre sécurité, car le module ASF 2FA garantit qu'ASF n'acceptera automatiquement que ses propres confirmations. Ainsi, même si l'attaquant demande une transaction, ASF 2FA <strong> n'acceptera pas un tel échange, car il n'a pas été généré par ASF. Outre la partie sécurité, l'utilisation d'ASF 2FA apporte également des avantages en termes de performances / optimisation, car ASF 2FA extrait et accepte les confirmations immédiatement après leur génération, , par opposition à une interrogation inefficace des confirmations toutes les X minutes effectuées, par exemple. par SDA ou WinAuth. En bref, il n’ya aucune raison d’utiliser un authentifiant tiers sur ASF 2FA, si vous envisagez d’automatiser les confirmations générées par ASF - c’est exactement ce à quoi ASF 2FA est destiné, et son utilisation n’est pas en conflit avec vous tout le reste de l’authentificateur de votre choix. Nous vous recommandons vivement d’utiliser ASF 2FA pour l’ensemble de l’activité ASF - cette solution est beaucoup plus sécurisée que toute autre solution.</p>

<hr />

<h2>Configuration avancée</h2>

<p>Si vous êtes un utilisateur expérimenté, vous pouvez également générer manuellement le fichier maFile. Il devrait avoir une structure JSON valide de ce type :</p>

<pre><code class="json">{
  "shared_secret": "STRING",
  "identity_secret": "STRING",
  "device_id": "STRING"
}
`</pre> 

est facultatif lors de l'importation, mais obligatoire pour l'opération ASF - ASF le demandera lors de l'importation si vous l'omettez. Bien sûr, vous devez remplacer `"STRING"` par un contenu valide dans chaque champ."STRING" par un contenu valide dans chaque champ.

Les données d'authentificateur standard comportent plus de champs. ASF les ignore totalement lors de l'importation, car elles ne sont pas nécessaires. Vous n'avez pas non plus besoin de les supprimer - ASF requiert uniquement un JSON valide avec les 2 champs obligatoires décrits ci-dessus, et éventuellement aussi  device_id </ 0>.</p>
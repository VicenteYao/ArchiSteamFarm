# Arguments de ligne de commande

ASF prend en charge plusieurs arguments de ligne de commande qui peuvent affecter l’exécution du programme. Ils peuvent être utilisés par des utilisateurs avancés pour spécifier au programme son mode de fonctionnement. Par rapport au fichier de configuration `ASF.json` par défaut, les arguments de ligne de commande sont utilisés pour l'initialisation principale (e.g. `--path`). paramètres spécifiques à la plate-forme (e.g. `--system-required`) ou données sensibles (e.g. `--cryptkey`).

* * *

## Utilisation

Son utilisation dépend de votre OS et ASF.

Générique :

```shell
dotnet ArchiSteamFarm.dll --argument --otherOne
```

Windows :

```powershell
.\ArchiSteamFarm.exe --argument --otherOne
```

Linux/OS X

```shell
./ArchiSteamFarm --argument --otherOne
```

Les arguments de ligne de commande sont également pris en charge dans les scripts d'assistance génériques tels que ` ArchiSteamFarm.cmd </ 0> ou <code> ArchiSteamFarm.sh </ 0>. De plus, lorsque vous utilisez des scripts d'assistance, vous pouvez également utiliser la propriété d'environnement <code> ASF_ARGS </ 0>, comme indiqué dans notre section <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker#command-line-arguments">docker</ 1>.</p>

<p>Si votre argument comprend des espaces, n'oubliez pas de le citer. Ces deux exemples sont faux:</p>

<pre><code class="shell">./ArchiSteamFarm --path /home/archi/My Downloads/ASF # Bad
./ArchiSteamFarm --path=/home/archi/My Downloads/ASF # Bad!
`</pre> 

Cependant, ces deux la sont complètement valides:

```shell
./ArchiSteamFarm --path "/home/archi/My Downloads/ASF" # OK
./ArchiSteamFarm "--path=/home/archi/My Downloads/ASF" # OK
```

## Arguments

`--cryptkey <key>` ou `--cryptkey=<key>` - démarrera ASF avec une clé cryptographique personnalisée de la valeur `<key>`. Cette option affecte la ** sécurité </ 0> et obligera ASF à utiliser votre clé `<key>` fournie à la place de la clé par défaut codée dans l'exécutable. Gardez à l'esprit que les mots de passe chiffrés avec cette clé nécessiteront sa transmission à chaque exécution du fichier ASF.</p> 

En raison de la nature de cette propriété, il est également possible de définir la clé de chiffrement en déclarant la variable d'environnement `ASF_CRYPTKEY`, qui peut être plus appropriée pour les personnes qui voudraient éviter de divulguer des informations privées dans les arguments du processus.

* * *

`--network-group <group>` or `--network-group=<group>` - will cause ASF to init its limiters with additional custom network group having `<group>` value. This option affects running ASF in **[multiple instances](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** by signalizing that given instance is dependent only on instances sharing the same network group, and independent of the rest. Typically you want to use this property only if you're routing ASF requests through custom mechanism (e.g. different IP addresses) and you want to set networking groups yourself, without relying on ASF to do it automatically (which currently includes taking into account `WebProxy` only). Keep in mind that when using a custom network group, this is unique identifier within the local machine, and ASF will not take into account any other details, such as `WebProxy` value, allowing you to e.g. start two instances with different `WebProxy` values which are still dependent on each other.

Due to the nature of this property, it's also possible to set the value by declaring `ASF_NETWORK_GROUP` environment variable, which may be more appropriate for people that would want to avoid sensitive details in the process arguments.

* * *

`--no-restart` - cette fonctionnalité est principalement utilisé par nos **[docker](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker)** ` et force  <code>AutoRestart` de <0> false</ 0>. Sauf si vous avez un besoin particulier, vous devriez plutôt configurer la propriété `AutoRestart` directement dans votre configuration. Cette fonctionalité est ici pour que notre script docker n'ait pas besoin de toucher votre configuration globale afin de l'adapter à son propre environnement. Bien sûr, si vous exécutez ASF dans un script, vous pouvez également utiliser cette fonctionnalité (sinon, la configuration globale est préférable).

* * *

`--path <path>` ou `--path=<path>` - ASF navigue toujours vers son propre répertoire au démarrage. En spécifiant cet argument, ASF naviguera vers un répertoire donné après l’initialisation, ce qui vous permet d’utiliser un chemin personnalisé pour plusieurs parties de l'application (dont les répertoires `config`, `plugins`, et `www`, mais aussi le ficher `NLog.config`), sans avoir besoin de dupliquer le binaire au même endroit. Cela peut s'avérer particulièrement utile si vous souhaitez séparer le binaire de la configuration réelle, comme cela est fait dans un paquet de type Linux - de cette façon, vous pouvez utiliser un binaire (à jour) avec plusieurs configurations différentes. Le chemin peut être relatif en fonction de l'emplacement actuel du binaire ASF ou en absolu. Keep in mind that this command points to new "ASF home" - the directory that has the same structure as original ASF, with config directory inside, see below example for explanation.

En raison de la nature de cet propriété, il est aussi possible de définir le chemin attendu en déclarant la variable d'environnement `ASF_PATH`, ce qui peut être plus approprié pour les personnes voulant éviter des données sensibles dans les arguments du processus.

If you're considering using this command-line argument for running multiple instances of ASF, we recommend reading our **[compatibility page](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** on this manner.

Exemples:

```shell
dotnet /opt/ASF/ArchiSteamFarm.dll --path /opt/TargetDirectory # Absolute path
dotnet /opt/ASF/ArchiSteamFarm.dll --path ../TargetDirectory # Relative path works as well
ASF_PATH=/opt/TargetDirectory dotnet /opt/ASF/ArchiSteamFarm.dll # Same as env variable
```

    ├── /opt
    │     ├── ASF
    │     │     ├── ArchiSteamFarm.dll
    │     │     └── ...
    │     └── TargetDirectory
    │           ├── config
    │           ├── logs (généré)
    │           ├── plugins (optionnel)
    │           ├── www (optionnel)
    │           ├── log.txt (généré)
    │           └── NLog.config (optionnel)
    └── ...
    

* * *

`--process-required` la fonctionnalité de ce commutateur désactivera le la méthode ASF par défaut d'arrêt lorsque aucun bot ne s'exécute. Aucun comportement d’arrêt automatique n’est particulièrement utile en combinaison avec **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC)**, où la majorité des utilisateurs s’attendraient à ce que leur service Web s’exécute, quel que soit le nombre de bots activés. Si vous utilisez l'option IPC ou si vous avez besoin que le processus ASF soit actif jusqu'à ce que vous le fermiez vous-même, c'est la bonne option.

Si vous n'avez pas l'intention de faire fonctionner IPC, cette option sera plutôt inutile pour vous, car vous pouvez simplement relancer le processus si nécessaire (contrairement au serveur Web d'ASF où vous avez besoin de l'écouter tout le temps pour pouvoir envoyer des commandes).

* * *

`--system-required</ 0> - la déclaration de ce commutateur incitera ASF à signaler au système d'exploitation que le processus nécessite que le système soit opérationnel pendant toute sa durée de vie. Actuellement, ce commutateur n'a d'effet que sur les machines Windows où il sera interdit à votre système de passer en mode veille tant que le processus est en cours d'exécution. Cela peut s’avérer particulièrement utile lorsque vous marchez au ralenti sur votre PC ou votre ordinateur portable pendant la nuit, car ASF sera en mesure de maintenir votre système en veille pendant son repos, puis, une fois cette opération effectuée, elle s’arrêtera comme d’habitude, ce qui permettra à votre système de fonctionner correctement. entrer à nouveau en mode veille, donc économiser de l'énergie immédiatement une fois que la phase de repos est terminé.</p>

<p>Gardez à l’esprit que pour un arrêt automatique correct de la fonction ASF, il vous faut d’autres paramètres, en particulier pour éviter <code>--process-required` et en veillant à ce que tous vos robots suivent `ShutdownOnFarmingFinished`. Bien sûr, l’arrêt automatique n’est qu’une possibilité pour cette fonction, et non une obligation, car vous pouvez également utiliser cet fonction avec, par exemple, `--process-required`, ce qui rend votre système éveillé à l'infini après le démarrage d'ASF.
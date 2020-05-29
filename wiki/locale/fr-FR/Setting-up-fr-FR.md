# Mise en place: 

Si vous arrivez ici pour la première fois, bienvenue! Nous sommes très heureux de voir encore un autre voyageur intéressé par notre projet, bien que vous ayez à l'esprit qu'une grande responsabilité implique une grande responsabilité - ASF est capable de faire beaucoup de choses différentes liées à Steam, mais seulement tant que vous ** prenez le temps pour apprendre à l'utiliser** Il s’agit là d’une courbe d’apprentissage, et nous attendons de vous que vous lisiez le wiki à cet égard, qui explique en détail le fonctionnement de tout.

Si vous êtes toujours la, cela signifie que vous avez pris le temps de lire ce long texte au dessus, ce qui n'est pas trop mal. A moins que vous ne l'ayez passé, auquel cas vous allez **[galérer](https://www.youtube.com/watch?v=WJgt6m6njVw)** très bientôt... Bref, ASF est une application en console, ce qui signifie que le programme n'a pas de jolie interface avec des boutons comme vous avez sûrement l'habitude de voir. ASF était en fait censé tourner en majorité sur des serveurs, donc il agit comme un service (daemon) et non une application de bureau.

Mais, cela ne veut pas dire que vous ne pouvez pas vous en servir sur votre PC, ou que vous allez galérer à le mettre en place. ASF est un programme indépendant qui ne nécessite pas d'installation et fonctionne directement dès son premier lancement, mais qui requiert un peu de configuration avant de pouvoir devenir vraiment utile. La configuration va en fait dire à ASF ce qu'il est censé faire après que vous l'ayez lancé. Si vous le lancez sans configuration, ASF ne fera rien, tout simplement.

* * *

## Installation spécifique à l'OS

En gros, vous aurez besoin de faire ceci dans les prochaines minutes:

- Installer les prérequis du **[.NET Core](#net-core-prerequisites)**.
- Télécharger **[la dernière version d'ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** dans la variante qui correspond à votre système d'exploitation.
- Extraire l'archive dans un nouvel emplacement (et `chmod +x ArchiSteamFarm` si vous êtes sour Linux/OS X).
- **[Configurer ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**.
- Lancer ASF et voir la magie opérer.

Ca a l'air plutôt simple, pas vrai ? Alors c'est parti.

* * *

### Prérequis du .NET Core

La première étape consiste à vérifier que votre OS peut lancer ASF correctement. ASF is written in C#, based on .NET Core and may require native libraries that are not available on your platform yet. Suivant votre OS (Windows, Linux, ou OS X), vous aurez des prérequis différents, qui sont tous listés dans **[les prérequis du .NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)** que vous devriez suivre. C'est le guide de référence qui devrait être utilisé, mais pour plus de simplicité (et pour vous éviter de devoir lire le doc entier), nous avons détaillé tous les packages requis ci-dessous.

Il est parfaitement normal que quelques (ou mêmes tous) les prérequis soient déjà installés sur votre système, notamment par des programmes tiers qui les nécessitent. Mais vous devriez quand même vous assurer que ça soit le cas en lançant les installeurs appropriés pour votre OS - sans ces dépendances, ASF ne se lancera même pas.

Gardez bien à l'esprit que ça sera sera la seule chose à installer sur votre OS, car la plupart des OS incluent déjà les autres packages requis. Vous aurez seulement besoin des prérequis (dépendances) du .NET Core pour faire tourner l'environnement .NET Core inclus dans ASF.

#### **[Windows](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-windows)**:

- **[Microsoft Visual C++ 2015 Redistributable Update](https://www.microsoft.com/en-us/download/details.aspx?id=53587)** (x64 for 64-bit Windows, x86 for 32-bit Windows)
- It's highly recommended to ensure that all Windows updates are already installed. At the very least you need **[KB2533623](https://support.microsoft.com/en-us/help/2533623/microsoft-security-advisory-insecure-library-loading-could-allow-remot)** and **[KB2999226](https://support.microsoft.com/en-us/help/2999226/update-for-universal-c-runtime-in-windows)**, but more updates may be needed. All of them are already installed if your Windows is up-to-date. Ensure that you meet those requirements prior to installing Visual C++ package.

#### **[Linux](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-linux)**:

Package names depend on the Linux distribution that you're using, we've listed the most common ones. You can obtain all of them with native package manager for your OS (such as `apt` for Debian or `yum` for CentOS).

- `libcurl` (`libcurl4`, `libcurl3`)
- `libicu` (latest version for your distribution, for example `libicu60`)
- `libkrb5-3` (`krb5-libs`)
- `liblttng-ust0` (`lttng-ust`)
- `libssl` (`libssl1.1`, `openssl-libs`, latest 1.1.X version for your distribution)
- `zlib1g` (`zlib`)

At least a few of those should be already natively available on your system (such as `zlib1g` that is required in almost every Linux distro nowadays).

#### **[OS X](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-macos)**:

- None for now, but you should have latest version of OS X installed, at least 10.13+

* * *

### Téléchargement

Maintenant que nous avons toutes les dépendances requises, la prochaine étape est de télécharger **[la dernière version d'ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**. ASF est disponible dans de nombreuses variantes, mais vous n'aurez qu'à vous soucier du package qui correspond à votre système d'exploitation et son architecture. Par example, si vous utilisez `64`-bit `Win`dows, alors vous devrez télécharger `ASF-win-x64`. Pour plus d'informations sur les variantes disponibles, visitez la section **[compatibilité](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility)**. ASF peut aussi tourner sur des OS qui n'ont pas de package spécifique, comme une version **32-bit Windows**, allez sur la page d'**[Installation générique](#generic-setup)** pour ça.

![Assets](https://i.imgur.com/Ym2xPE5.png)

After download, start from extracting the zip file into its own folder. We recommend using **[7-zip](https://www.7-zip.org)**, standard utilities like `unzip` from Linux/OS X should work without problems as well. Afterwards, you'll have a huge mess of folders and files. Ne vous inquiétez pas, on va nettoyer tout ça dans une petite seconde.

Si vous utilisez Linux / OS X, n'ouvliez pas de `chmod +x ArchiSteamFarm`, vu que les permissions ne sont pas mises automatiquement dans le fichier zip. Cette étape doit être faite une seule fois après la décompression du zip.

Be advised to unpack ASF to **its own directory** and not to any existing directory you're already using for something else - ASF's auto-updates feature will delete all old and unrelated files when upgrading, which may lead to you losing anything unrelated you put in ASF directory. Si vous avez d'autres scripts ou fichiers que vous souhaitez utiliser avec ASF, placez les un dossier au dessus.

Un structure d'example ressemblerait à ça:

    C:\ASF (où vous placez vos fichiers perso)
        ├── Raccourci vers ASF.lnk (optionnel)
        ├── Raccourci vers Config.lnk (optionnel)
        ├── Commandes.txt (optionnel)
        ├── MonScriptPersoEnPlus.bat (optionnel)
        ├── ... (autres fichiers de votre choix, optionnels)
        └── Core (dédié à ASF uniquement, c'est ici que vous devez extraire l'archive)
             ├── ArchiSteamFarm.dll
             ├── config
             └── (...)
    

C'est une structure que nous recommandons, car vous n'avez pas besoin de passer par un grand nombre de fichiers et dossiers inclus dans ASF, vu que vous avez uniquement besoin d'un raccourci vers le dossier de config et vers le .exe principal.

Let's prepare ASF structure for usage. If you want to, you can now skip to the next step, since cleaning up ASF structure is not required (especially if you're using OS-specific builds that are already bundled), but it can make your life a bit easier.

You can open ASF folder and find core executable file, this will be `ArchiSteamFarm.exe` on Windows, and `ArchiSteamFarm` on Linux/OS X. Right click it and select "copy". Maintenant, naviguez jusque l'emplacement où vous souhaitez avoir votre raccourci vers ASF (comme votre bureau), clic droit et sélectionnez "coller le raccourci". Vous pouvez ensuite renommer le raccourci comme vous voulez, comme par exemple en l'appelant "ASF". Maintenant, faites la même chose avec le dossier `config` que vous trouverez au même endroit que l’exécutable ASF.

Après un peu de nettoyage, vous devriez donc avoir une structure similaire à ci dessous:

![Structure](https://i.imgur.com/k85csaZ.png)

Cela vous aidera à accéder facilement à l’exécutable et aux fichiers de config d'ASF sans trop de problèmes. Dans mon cas j'ai décidé d'utiliser la structure mentionnée ci dessus, donc mes fichiers ASF sont dans le dossier "Core". Vous pouvez adapter cette structure à votre guise, comme en aillant les raccourcis de ASF et de config sur votre bureau et ASF core placé dans dans `C:\ASF`, c'est comme vous voulez.

Les utilisateurs de Linux/OS X devraient faire de même, vous pouvez utiliser de liens symboliques disponibles via `In -s`.

* * *

### Configuration

Nous sommes maintenant prêts à faire la dernière étape, la configuration. C'est de loin l'étape la plus compliqué, car elle implique beaucoup de nouvelles infos avec lesquelles vous n'êtes pas encore familiers. Nous essayerons donc de vous fournir des exemples et explications simplifiées.

Tout d'abord, la **[page de configuration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)** vous expliquera **tout** ce qui est lié à la configuration, mais elle contient surtout un énorme masse de nouvelles informations, que nous n'allons pas utiliser directement pour la grande majorité. On va plutôt vous expliquer comment trouver les infos que vous cherchez.

La Configuration d'ASF peut être faite de deux manières - soit en utilisant notre interface web (config generator), ou manuellement. Tout cela est expliqué plus précisément dans la section **[configuration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**, donc référez-y vous si vous avez besoin d'informations plus détaillées. Nous allons utiliser la page de configuration vu que c'est plus simple.

Naviguez à la **[page de génération de config](https://justarchinet.github.io/ASF-WebConfigGenerator)** avec votre browser, vous aurez besoin de javascript activé si vous l'avez manuellement désactivé. Nous recommandons Chrome ou Firefox, mais ça devrait marcher sur la plupart des browsers.

Après avoir ouvert la page, allez sur l'onglet "Bot". Vous devriez maintenant voire une page similaire à celle ci-dessous:

![Bot tab](https://i.imgur.com/aF3k8Rg.png)

Si par hasard la version d'ASF que vous avez téléchargée est plus vieille que le config generator utilise par défaut, choisissez simplement votre version d'ASF dans le menu déroulant. Cela peut habituellement arriver lorsque vous vous servez du config generator pour des pre-releases d'ASF, qui ne sont pas encore distribuées en stable You've downloaded latest stable release of ASF that is verified to work reliably.

Start from putting name for your bot into the field highlighted as red. This can be any name you'd like to use, such as your nickname, account name, a number, or anything else. There is only one word that you can't use, `ASF`, as that keyword is reserved for global config file. In addition to that your bot name can't start with a dot (ASF intentionally ignores those files). We also recommend that you avoid using spaces, you can use `_` as a word separator if needed.

After you decided about your name, change `Enabled` switch to be on, this defines whether your bot is supposed to be started by ASF automatically after launch (of the program).

Now you can decide upon two things:

- You can put your login in `SteamLogin` field and your password in `SteamPassword` field
- Or you can leave them empty

Doing the first thing will allow ASF to automatically use your account credentials during startup, so you won't need to input them manually each time ASF needs them. You can however decide to omit them, in which case they're not being saved, so ASF won't be able to automatically start without your help and you'll need to input them during runtime.

ASF requires your login credentials because it includes its own implementation of Steam client and needs the same details to log in as the one that you use yourself. Your login credentials are not saved anywhere but on your PC in ASF `config` directory only, our web config generator is client-based which means that the code is run locally in your browser to generate valid ASF configs, without details you're inputting ever leaving your PC in the first place, so there is no need to worry about any possible sensitive data leak. Still, if you for whatever reason don't want to put your credentials there, we understand that, and you can put them manually later in generated files, or omit them entirely and put them only in ASF command prompt. More on security matter can be found in **[configuration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)** section.

You can also decide to leave just one field empty, such as `SteamPassword`, ASF will then be able to use your login automatically, but will still ask for password (similar to Steam Client). If you're using Steam parental to unlock the account, you'll need to put it into `SteamParentalCode` field.

After the decision and optional details, your web page will now look similar to the one below:

![Bot tab 2](https://i.imgur.com/yf54Ouc.png)

You can now hit "download" button and our web config generator will generate new `json` file based on your chosen name. Save that file into `config` directory of ASF. You can use previously-created `config` shortcut, or find `config` directory manually, directly in ASF file structure.

Your `config` directory will now look like this:

![Structure 2](https://i.imgur.com/crWdjcp.png)

Congratulations! You've just finished the very basic ASF bot configuration. We'll extend this shortly, for now this is everything that you need.

* * *

### Running ASF

You're now ready to launch the program for the first time. Simply double-click ASF shortcut, or `ArchiSteamFarm` binary in ASF directory.

After doing so, assuming you installed all required dependencies in the first step, ASF should launch properly, notice your first bot (if you didn't forget to put generated config in `config` directory), and attempt to log in:

![ASF](https://i.imgur.com/u5hrSFz.png)

If you supplied `SteamLogin` and `SteamPassword` for ASF to use, you'll be asked for your SteamGuard token only (e-mail, 2FA or none, depending on your Steam settings). If you didn't, you'll also be asked for your Steam login and password.

Now is a good time to review our **[privacy policy](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics#current-privacy-policy)** section if you're concerned about what will happen next, as stated by ASF itself.

After passing through initial login gate, assuming your details are correct, you'll successfully log in, and ASF will start idling using default settings that you didn't change as of now:

![ASF 2](https://i.imgur.com/Cb7DBl4.png)

This proves that ASF is now successfully doing its job on your account, so you can now minimize the program and do something else. After enough of time (depending on **[performance](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance)**), you'll see Steam trading cards slowly being dropped. Of course, for that to happen you must have valid games to idle, showing as "you can get X more card drops from playing this game" on your **[badges page](https://steamcommunity.com/my/badges)** - if there are no games to idle, then ASF will state that there is nothing to do, as stated in our **[FAQ](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ#what-is-asf)**.

This concludes our very basic setting up guide. You can now decide whether you want to configure ASF further, or let it do its job in default settings. We'll cover a few more basic details, then leave you entire wiki for discovery.

* * *

### Extended configuration

#### Idling several accounts at once

ASF supports idling more than one account at a time, which is its primary function. You can add more accounts to ASF by generating more bot config files, in exactly the same way as you've generated your first one just a few minutes ago. You need to ensure only two things:

- Unique bot name, if you already have your first bot named "MainAccount", you can't have another one with the same name.
- Valid login details, such as `SteamLogin`, `SteamPassword` and `SteamParentalCode` (if using Steam parental settings)

In other words, simply jump to configuration again and do exactly the same, just for your second or third account. Remember to use unique names for all of your bots.

* * *

#### Changing settings

You change existing settings in exactly the same way - by generating a new config file. If you didn't close our web config generator yet, click on "toggle advanced settings" and see what is there for you to discover. For this tutorial we'll change `CustomGamePlayedWhileFarming` setting, which allows you to set custom name being displayed when ASF is idling, instead of showing actual game.

So let's do that, if you run ASF and start idling, in default settings you'll see that your Steam account is in-game now:

![Steam](https://i.imgur.com/1VCDrGC.png)

Let's change that then. Toggle advanced settings in web config generator and find `CustomGamePlayedWhileFarming`. Once you do that, put your own custom text there that you want to display, such as "Idling cards":

![Bot tab 3](https://i.imgur.com/gHqdEqb.png)

Now download the new config file in exactly the same way, then **overwrite** your old config file with new one. You can also delete your old config file and put new one in its place of course.

Once you do that and start ASF again, you'll notice that ASF now displays your custom text in previous place:

![Steam 2](https://i.imgur.com/vZg0G8P.png)

This confirms that you've successfully edited your config. In exactly the same way you can change global ASF properties, by switching from bot tab to "ASF" tab, downloading generated `ASF.json` config file and putting it in your `config` directory.

* * *

#### Using ASF-ui

ASF is a console app and doesn't include a graphical user interface. However, we're actively working on **[ASF-ui](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC#asf-ui)** frontend to our IPC interface, which can be a very decent and user-friendly way to access various ASF features.

In order to use ASF-ui, you should ensure that you set up `IPC` and `SteamOwnerID` global configuration properties (ASF tab).

For `SteamOwnerID`, you need to input unique Steam identificator in 64-bit form of your account. You can look it up in various different ways, we'll use **[SteamRep](https://steamrep.com)** for that purpose. Open the website, locate sign in through Steam button in top right corner, then log in. Afterwards, in the same place, click on your avatar, and look up `steamID64` field on your profile.

![SteamRep](https://i.imgur.com/RUuJ63i.png)

For my account, this is `76561198006963719` number. You'll have a similar one, also starting from `7656`. Copy it.

Now navigate once again to our web config generator and input that number as SteamOwnerID.

![SteamOwnerID
](https://i.imgur.com/V6jslfQ.png)

You need to do only one more thing, toggle advanced settings, find `IPC` option, and enable it.

![IPC](https://i.imgur.com/NhujZCN.png)

Now you can download your ASF config and put it in your `config` directory, as usual. Afterwards, launch ASF again, and you should be able to confirm that it properly started IPC interface:

![IPC 2
](https://i.imgur.com/ZmkO8pk.png)

If you did everything properly, you'll now be able to access ASF's IPC interface under **[this](http://localhost:1242)** link, as long as ASF is running. You can use ASF-ui for various purposes, e.g. sending **[commands](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. Feel free to take a look around in order to find out all ASF-ui functionalities.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

Please note that ASF-ui is currently in preview state and not everything is available/working yet, but it's more than enough for simple ASF usage.

* * *

### Résumé

You've successfully set up ASF to use your Steam accounts and you've already customized it to your liking a little. If you followed our entire guide, then you even managed to send a simple command through our ASF-ui interface. Now is a good time to read our entire **[configuration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)** section in order to learn what all those different settings you've seen in advanced tab actually do, and what ASF can offer. If you've stumbled upon some issue or you have some generic question, read **[FAQ](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ)** instead which should cover all, or at least majority of questions that you may have. If you want to learn everything about ASF and how it can make your life easier, head over to the rest of **[our wiki](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home)**. Have fun!

* * *

## Generic setup

This setup is for advanced users that want to set up ASF to run in **[generic](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#generic)** variant. It's not recommended for people that can use **[OS-specific setup](#os-specific-setup)**.

You want to use generic variant mainly in three situations (but of course you can use it regardless):

- When you're using OS that we don't build OS-specific package for (such as 32-bit Windows)
- When you already have .NET Core Runtime/SDK, or want to install and use one
- When you want to minimize ASF structure size by handling runtime requirements yourself

However, keep in mind that you're in charge of .NET Core runtime in this case. This means that if your .NET Core SDK (runtime) is unavailable, outdated or broken, ASF won't work. This is why we don't recommend this setup for casual users, since you now need to ensure that your .NET Core SDK (runtime) matches ASF requirements and can run ASF, as opposed to **us** ensuring that our .NET Core runtime bundled with ASF can do so.

For generic package, you can follow entire OS-specific guide above, with two small changes. In addition to installing .NET Core prerequisites, you also want to install .NET Core SDK, and instead of having OS-specific `ArchiSteamFarm(.exe)` executable file, you now have a generic `ArchiSteamFarm.dll` binary only. Everything else is exactly the same.

With extra steps:

- Installer les prérequis du **[.NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)**.
- Install **[.NET Core SDK](https://www.microsoft.com/net/download)** (or at least runtime) appropriate for your OS. You most likely want to use an installer. Refer to **[runtime requirements](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements)** if you're not sure which version to install.
- Download **[latest ASF release](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** in generic variant.
- Extract the archive into new location (and `chmod +x ArchiSteamFarm.sh` if you're on Linux/OS X).
- **[Configurer ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**.
- Launch ASF by either using a helper script or executing `dotnet /path/to/ArchiSteamFarm.dll` manually from your favourite shell.

Helper scripts (such as `ArchiSteamFarm.cmd` for Windows and `ArchiSteamFarm.sh` for Linux/OS X) are located next to `ArchiSteamFarm.dll` binary - those are included in generic variant only. You can use them if you don't want to execute `dotnet` command manually. You can also make a shortcut to those scripts like showed above, since they're supposed to provide binary replacement in a script way. Obviously helper scripts won't work if you didn't install .NET Core SDK and you don't have `dotnet` executable available in your `PATH`. Helper scripts are entirely optional to use, you can always `dotnet /path/to/ArchiSteamFarm.dll` manually.
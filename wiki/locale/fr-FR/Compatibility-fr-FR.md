# Compatibilité

ASF est une application C# qui s'éxécute sur une plate-forme .NET Core de base. Cela signifie que ASF n’est pas compilé directement en **[code machine](https://en.wikipedia.org/wiki/Machine_code)** qui s’exécute sur votre CPU, mais en **[CIL](https://en.wikipedia.org/wiki/Common_Intermediate_Language)** qui nécessite un runtime compatible CIL pour l’exécuter.

Cette fonction présente des avantages gigantesques, CIL étant indépendant de la plate-forme, c'est pourquoi ASF peut s'exécuter en mode natif sur de nombreux systèmes d'exploitation disponibles, notamment Windows, Linux et OS X. Non seulement aucune émulation n'est nécessaire, mais également une prise en charge de toutes les plates-formes optimisations liées et liées au matériel, telles que les instructions CPU SSE. Grâce à cela, ASF peut atteindre des performances et une optimisation supérieures, tout en offrant une compatibilité et une fiabilité parfaite.

Cela signifie également qu'ASF n'a ** aucune exigence spécifique du système d'exploitation </ 0>, car il nécessite de travailler sur ** runtime </ 0> sur ce système d'exploitation et non sur le système d'exploitation lui-même. Tant que le moteur source exécute correctement le code ASF, peu importe que le système d'exploitation soit Windows, Linux, OS X, BSD, Sony Playstation 4, Nintendo Wii ou votre grille-pain - tant qu'il existe **.NET Core pour lui</ 0>, il existe **[ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** pour cela.</p> 

Toutefois, quel que soit le lieu où vous exécutez ASF, vous devez vous assurer que les **prérequis .NET Core </ 0> sont installés sur votre plate-forme cible. Ce sont des bibliothèques de bas niveau requises pour une fonctionnalité d’exécution correcte et absolument essentielles au bon fonctionnement d’ASF. Très probablement, vous pouvez en avoir certains (ou même tous) déjà installés.</p> 

* * *

## Multiple instances

ASF is compatible with running multiple instances of the process on the same machine. The instances can be completely standalone or derived from the same binary location (in which case, you want to run them with different `--path` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**).

When running multiple instances from the same binary, keep in mind that you should typically disable auto-updates in all of their configs, as there is no synchronization between them in regards to auto-updates. If you'd like to keep having auto-updates enabled, we recommend standalone instances, but you can still make updates work, as long as you can ensure that all other ASF instances are closed.

ASF will do its best to maintain a minimum amount of OS-wide, cross-process communication with other ASF instances. This includes ASF checking its configuration directory against other instances, as well as sharing core process-wide limiters configured with `*LimiterDelay` **[global config properties](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config)**, ensuring that running multiple ASF instances will not cause a possibility to run into a rate-limiting issue. In regards to technical aspects, Windows platforms use native OS-wide named semaphores for this purpose, Unix platforms use fallback mechanism of custom ASF file-based locks created in `/tmp/ASF` directory.

It's not required for running ASF instances to share the same `*LimiterDelay` properties, they can use different values, as each ASF will add its own configured delay to the release time after acquiring the lock. If the configured `*LimiterDelay` is set to `0`, ASF instance will entirely skip waiting for the lock of given resource that is shared with other instances (that could potentially still maintain a shared lock with each other). When set to any other value, ASF will properly synchronize with other ASF instances and wait for its turn, then release the lock after configured delay, allowing other instances to continue.

ASF takes into account `WebProxy` setting when deciding about shared scope, which means that two ASF instances using different `WebProxy` configurations will not share their limiters with each other. This is implemented in order to allow `WebProxy` setups to operate without excessive delays, as expected from different network interfaces. This should be good enough for majority of use cases, however, if you have a specific custom setup in which you're e.g. routing requests yourself in a different way, you can specify network group yourself through `--network-group` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**, which will allow you to declare ASF group that will be synchronized with this instance. Keep in mind that custom network groups are used exclusively, which means that ASF will no longer use `WebProxy` for determining the right group, as you're in charge of grouping in this case.

* * *

## ASF packaging

ASF est disponible en 2 versions principales: package générique et système d'exploitation spécifique. En termes de fonctionnalité, les deux packages sont exactement les mêmes, ils sont également capables de se mettre à jour automatiquement. La seule différence entre eux est de savoir si le package ASF **generic** est également fourni avec un environnement d’exécution **spécifique au système d’exploitation**.

* * *

### Générique 

Le paquet générique est une construction indépendante de la plate-forme qui n'inclut aucun code spécifique à la machine. Cette installation nécessite que vous ayez .NET Core Runtime déjà installé sur votre système d'exploitation **dans la version appropriée**. Nous savons tous à quel point il est difficile de maintenir les dépendances à jour. Ce package s'adresse donc principalement aux utilisateurs **qui utilisent déjà** .NET Core et qui ne souhaitent pas dupliquer leur environnement d'exécution uniquement pour ASF si ils peuvent utiliser ce qu'ils ont déjà installé. Le package générique vous permet également d’exécuter ASF **où que vous puissiez obtenir une implémentation fonctionnelle du runtime .NET Core**, qu’il existe ou non une version ASF spécifique à son système d’exploitation.

Il n'est pas recommandé d'utiliser une version générique si vous êtes un utilisateur occasionnel ou même avancé qui ne souhaite que faire fonctionner ASF sans fouiller dans les détails techniques de .NET Core. En d'autres termes - si vous savez ce que c'est, vous pouvez l'utiliser, sinon il est préférable d'utiliser le paquet spécifique au système d'exploitation comme expliqué ci-dessous.

#### .NET Framework package 

Outre le package générique mentionné ci-dessus, il existe également un package `generic-netf` qui repose sur le .NET Framework (et non sur le .NET Core). Ce package est une variante héritée qui fournit la compatibilité manquante connue deASF V2, et peut être exécuté par exemple. avec **[Mono](https://www.mono-project.com)**, alors que le package .NET Core `générique` ne le peut pas à partir d’aujourd’hui.

En général, **évitez autant que possible ce package**, car la plupart des systèmes d'exploitation et des configurations sont parfaitement (et bien mieux) pris en charge avec le package `générique` mentionné ci-dessus. En fait, ce package a du sens pour être utilisé uniquement sur des plates-formes où .NET Core runtime ne fonctionne pas, tout en ayant une implémentation Mono fonctionnelle. Examples of such platforms include `linux-x86` (32-bit i386/i686 linux), as well as `linux-armel` (ARMv6 boards found e.g. in Raspberry Pi 0 & 1), all of which do not have official working .NET Core runtime as of today.

Au fur et à mesure que le nombre de plates-formes supportées par .NET Core sera réduit et que la compatibilité entre .NET Framework et .NET Core sera moindre, le package `generic-netf` sera entièrement remplacé par `generic` à l'avenir. Veuillez vous abstenir de l'utiliser si vous pouvez utiliser un package .NET Core à la place, car `generic-netf` manque de nombreuses fonctionnalités et de la compatibilité par rapport aux versions .NET Core, et ce ne sera que moins fonctionnel. comme le temps passe. We offer support for this package **only** on machines that can't use `generic` variant above (e.g. `linux-x86`), and only with up-to-date runtime (e.g. latest Mono).

* * *

### OS-spécifique

Le package spécifique au système d'exploitation, outre le code géré inclus dans le package générique, inclut également du code natif pour une plate-forme donnée. En d’autres termes, le package **spécifique au système d’exploitation inclut déjà un environnement .NET Core runtime approprié**, ce qui vous permet de passer complètement le désordre de l’installation et de lancer ASF directement. Comme vous pouvez le deviner, le paquet spécifique à un système d’exploitation est spécifique à chaque système d’exploitation. Par exemple, Windows requiert PE32 + pour `ArchiSteamFarm.exe` alors que Linux fonctionne avec Unix ELF</code> binaire pour `ArchiSteamFarm</0>. Comme vous le savez peut-être, ces deux types ne sont pas compatibles.</p>

<p>ASF est actuellement disponible dans les variantes suivantes spécifiques au système d'exploitation:</p>

<ul>
<li><code>win-x64` fonctionne sur les systèmes d’exploitation Windows 64 bits. Cela inclut Windows 7 (SP1 +), 8.1, 10, Server 2008 R2 (SP1 +), 2012, 2012 R2, 2016 et les versions futures.</li> 

- `linux-arm` fonctionne sur les systèmes d'exploitation GNU/Linux ARM (ARMv7 +) 32 bits. This includes platforms such as Raspberry Pi 2 (and newer) with all GNU/Linux OSes available for them (such as Raspbian), in current and future versions. This variant will not work with older ARM architectures, such as ARMv6 found in Raspberry Pi 0 & 1, it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-arm64` works on 64-bit ARM-based (ARMv8+) GNU/Linux OSes. This includes platforms such as Raspberry Pi 3 (and newer) with all AArch64 GNU/Linux OSes available for them (such as Debian), in current and future versions. This variant will not work with 32-bit OSes that do not have required 64-bit libraries available (such as Raspbian), it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-x64` fonctionne sur les systèmes d’exploitation Linux basés sur GNU/glibc 64 bits. Cela inclut Alpine, CentOS / Fedora / RHEL, Debian / Ubuntu / Linux Mint, OpenSUSE / SLES et de nombreux autres, y compris leurs dérivés, dans les versions actuelles et futures.
- `osx-x64` fonctionne sur les systèmes d’exploitation OS X 64 bits. Cela inclut 10.13, ainsi que les versions futures.</ul> 

Bien entendu, même si aucun package spécifique à un système d'exploitation n'est disponible pour votre combinaison architecture-système d'exploitation, vous pouvez toujours installer vous-même le runtime .NET Core approprié et exécuter la version générique ASF, ce qui est également la raison principale de son existence dans la première version. La version ASF générique est indépendante de la plate-forme et s'exécute sur toutes les plates-formes disposant d'un environnement .NET Core runtime actif. Il est important de noter que ASF nécessite le .NET Core runtime, pas un système d’exploitation ou une architecture spécifique. Par exemple, si vous utilisez Windows 32 bits, malgré l'absence de version `win-x86` ASF dédiée, vous pouvez toujours installer .NET Core SDK dans la version `win-x86` et exécutez ASF générique parfaitement. Nous ne pouvons simplement pas cibler toutes les combinaisons architecture-système existantes qui sont utilisées par quelqu'un, nous devons donc tracer une ligne quelque part. x86 est un bon exemple de cette ligne car son architecture est obsolète depuis au moins 2004.

Pour obtenir une liste complète de toutes les plates-formes et systèmes d'exploitation pris en charge par .NET Core 3.1, consultez la section **[Notes de publication](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md)**.

* * *

## Exigences Runtime

Si vous utilisez un package spécifique au système d'exploitation, vous n'avez pas à vous soucier de la configuration requise, car ASF est toujours livré avec une exécution requise et à jour qui fonctionnera correctement tant que vous avez **[les prérequis .NET Core.](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)** installé et à jour. En d'autres termes, **vous n'avez pas besoin d'installer .NET Core runtime ou SDK**, car les versions spécifiques à un système d'exploitation ne nécessitent que des dépendances de système d'exploitation natives (conditions préalables) et rien d'autre.

Toutefois, si vous essayez d'exécuter le package **générique** ASF, vous devez vous assurer que votre environnement d'exécution .NET Core prend en charge la plate-forme requise par ASF.

ASF as a program is targeting **.NET Core 3.1** (`netcoreapp3.1`) right now, but it may target newer platform in the future. `netcoreapp3.1` is supported since 3.1.100 SDK (3.1.0 runtime), although ASF is configured to target **latest runtime at the moment of compilation**, so you should ensure that you have **[latest SDK](https://dotnet.microsoft.com/download)** (or at least runtime) available for your machine. Des variantes génériques d'ASF pourraient refuser de se lancer si votre runtime est plus vieux que le minimum (cible) connu durant la compilation.

En cas de doute, vérifiez ce que notre **[intégration continue utilise](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** pour compiler et déployer les versions ASF sur GitHub. Vous pouvez trouver la sortie `dotnet --info` au-dessus de chaque construction.

* * *

## Problèmes

### Various lock-related issues running ASF on Linux VPS with OpenVZ virtualization

OpenVZ kernel is usually based on a very old Linux kernel version (2.6) which seems incompatible with latest .NET Core runtime. If you're trying to run ASF in such environment, you may encounter various lock-related issues, usually in form of process freeze. See related CoreCLR issue: https://github.com/dotnet/coreclr/issues/26873

Our recommendation is to ditch OpenVZ in favour of much better virtualization solutions, such as KVM. The issue described here is indeed a .NET Core runtime bug that is supposed to be **[fixed](https://github.com/dotnet/coreclr/pull/26912)** in the next .NET Core runtime upgrade (3.1 service patch), but there is no actual timeframe for it yet. If you're unable to move to better virtualization solution, you can consider running `generic-netf` ASF variant with `mono`, at least until new runtime version is released.

For more advanced users that are not afraid of their Linux OS, there is a **[much better workaround](https://github.com/dotnet/coreclr/issues/26873#issuecomment-559854433)** available which makes it possible to run latest .NET Core ASF without running into this issue.
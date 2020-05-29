# Compilation

La compilation est le processus de création d'un fichier exécutable. C’est ce que vous souhaitez faire si vous souhaitez ajouter vos propres modifications à ASF ou si, pour une raison quelconque, vous ne faites pas confiance aux fichiers exécutables fournis dans les ** versions officielles </ 0>. Si vous êtes utilisateur et non développeur, vous voudrez probablement utiliser des binaires déjà précompilés, mais si vous souhaitez utiliser vos propres binaires ou apprendre quelque chose de nouveau, continuez à lire.</p> 

ASF peut être compilé sur n’importe quelle plate-forme actuellement prise en charge, à condition que vous disposiez de tous les outils nécessaires pour le faire.

* * *

## .NET Core SDK

Quelle que soit la plate-forme, vous avez besoin du SDK .NET Core complet (pas seulement de l'exécutable) pour compiler ASF. Les instructions d'installation se trouvent sur la ** page d'installation de .NET Core </ 0>. Vous devez installer la version appropriée du .NET Core SDK pour votre système d'exploitation. Une fois l'installation réussie, la commande ` dotnet </ 0> devrait fonctionner et être opérationnelle. Vous pouvez vérifier si cela fonctionne avec <code> dotnet --info </ 0>. Assurez-vous également que votre SDK .NET Core répond aux exigences d'exécution ASF <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements"> </ 0>.</p>

<hr />

<h2>Compilation</h2>

<p>En supposant que vous avez un SDK .NET Core opérationnel dans sa version appropriée, il vous suffit de naviguer vers le répertoire source ASF (cloné ou téléchargé et dés-archivé) et d'exécuter :</p>

<pre><code class="shell">dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/generic"
`</pre> 

Si vous utilisez Linux / OS X, vous pouvez utiliser le script ` cc.sh </ 0> qui fera de même, de manière un peu plus complexe.</p>

<p>If compilation ended successfully, you can find your ASF in <code>source` flavour in `out/generic` directory. This is the same as official `generic` ASF build, but it has forced `UpdateChannel` and `UpdatePeriod` of `0`, which is appropriate for self-builds.

### OS-spécifique

Vous pouvez également générer un package .NET Core spécifique au système d'exploitation si vous avez un besoin spécifique. In general you shouldn't do that because you've just compiled `generic` flavour that you can run with your already-installed .NET Core runtime that you've used for the compilation in the first place, but just in case you want to:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/linux-x64" -r "linux-x64"
```

Bien sûr, remplacez ` linux-x64 ` par l'architecture du système d'exploitation que vous souhaitez cibler, tel que ` win-x64 `. Cette mise à jour aura également des mises à jour désactivées.

### .NET Framework 

In a very rare case when you'd want to build `generic-netf` package, you can change target framework from `netcoreapp3.1` to `net48`. N'oubliez pas que vous aurez besoin du pack de développeurs **[ .NET Framework ](https://dotnet.microsoft.com/download/visual-studio-sdks)** approprié pour la compilation de la variante ` netf `, en plus du kit de développement .NET Core SDK, donc les éléments ci-dessous ne fonctionneront que sur Windows :

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "net48" -o "out/generic-netf"
```

Dans des cas encore plus rares, si vous ne pouvez pas installer .NET Framework ou même .NET Core SDK lui-même (par exemple, en raison de la construction sur ` linux-x86 ` avec ` mono `), vous pouvez appeler ` msbuild ` directement. Vous devrez également spécifier `ASFNetFramework` manuellement, car ASF désactive par défaut la construction netf sur les plates-formes non Windows :

```shell
msbuild /m /r /t:Publish /p:Configuration=Release /p:TargetFramework=net48 /p:PublishDir=out/generic-netf /p:ASFNetFramework=true ArchiSteamFarm
```

* * *

## Développement

Si vous souhaitez modifier le code ASF, vous pouvez utiliser n'importe quel IDE compatible avec .NET Core à cette fin, même si cela reste facultatif. Vous pouvez également éditer avec un bloc-notes et compiler avec la commande  dotnet </ 0>. décrit ci-dessus. Néanmoins, pour Windows, nous recommandons la <strong><a href="https://visualstudio.microsoft.com/downloads"> dernière version de Visual Studio </ 0> (la version communautaire gratuite est largement suffisante). Nous vous suggérons également de l’utiliser avec <strong><a href="https://www.jetbrains.com/resharper"> ReSharper </a></strong> (facultatif), bien qu’il ne s’agisse pas d’un produit gratuit.</p>

<p>Si vous souhaitez plutôt utiliser du code ASF sous Linux / OS X, nous vous recommandons <strong><a href="https://code.visualstudio.com/download"> le dernier Visual Studio Code</ 0>. Ce n'est pas aussi complet que le classique Visual Studio, mais c'est suffisant.</p>

<p>Bien sûr, toutes les suggestions ci-dessus ne sont que des recommandations, vous pouvez utiliser ce que vous voulez, cela revient à la commande <code> dotnet build </ 0>. Nous utilisons Visual Studio + ReSharper pour le développement ASF, avec une petite partie des <code> outils tiers </ 0> que vous pouvez trouver dans le référentiel.</p>

<hr />

<h2>Tags</h2>

<p>Il n’est pas garanti que la branche <code> master </ 0> soit dans un état permettant une compilation réussie ou une exécution sans faille du fichier ASF en premier lieu, étant donné qu’elle est une branche de développement comme l’indique notre <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle"> cycle de publication</ 1>. If you want to compile or reference ASF from source, then you should use appropriate <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/tags">tag</a></strong> for that purpose, which guarantees at least successful compilation, and very likely also flawless execution (if build was marked as stable release). Pour vérifier la "santé" actuelle de l’arbre, vous pouvez utiliser nos CIs - <strong><a href="https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm">AppVeyor</a></strong> ou <strong><a href="https://travis-ci.com/JustArchiNET/ArchiSteamFarm">Travis</a></strong>.</p>

<hr />

<h2>Versions Officielles</h2>

<p>Les versions officielles ASF sont compilées par <strong><a href="https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm"> AppVeyor </ 0> sous Windows, avec le dernier SDK .NET Core correspondant aux  <strong><a href="https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements">exigences d'exécution</ 1> ASF Une fois les tests passés, tous les packages sont déployés sur GitHub. Cela garantit également la transparence, car AppVeyor utilise toujours des sources publiques officielles pour toutes les versions, et vous pouvez comparer les taux de contrôle de AppVeyor avec les actifs GitHub. Les développeurs ASF ne compilent, ni ne publient les versions manuellement, sauf pour les processus de développement privés, y compris le débogage.</p>
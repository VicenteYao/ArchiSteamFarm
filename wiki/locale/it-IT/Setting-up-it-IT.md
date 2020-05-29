# Primi passi

Se sei qui per la prima volta, benvenuto! Siamo felici di vedere un altro viaggiatore interessato nel nostro progetto, ma tieni a mente che da un grande potere derivano grandi responsabilità - ASF è in grado di fare innumerevoli cose con Steam, ma soltanto finché **hai la volontà di imparare ad utilizzarlo**. C'è da considerare una ripida curva d'apprendimento e per questo ci aspettiamo che tu legga a dovere questo wiki, dove viene spiegato nel dettaglio il funzionamento di ogni cosa.

Se sei ancora qui vuol dire che hai già letto l'introduzione, il che è positivo. A meno che non ci sei passato sopra, e in tal caso sappi che presto **[avrai il tuo bel da fare](https://www.youtube.com/watch?v=WJgt6m6njVw)**... Ad ogni modo, ASF è un programma con interfaccia a riga di comando, ovvero privo di un'interfaccia grafica (o GUI), contrariamente quindi ai programmi che generalmente usi. ASF è stato principalmente pensato per essere eseguito su server, quindi si comporta più come un servizio (o daemon) che come un'applicazione classica.

Tuttavia ciò non significa che tu non possa usarlo sul tuo PC o che farlo funzionare sia più complicato del solito, per niente. ASF è un programma standalone che non necessita di installazione, subito pronto per essere utilizzato, ma che necessita d'essere configurato per poter essere di qualche uso. È infatti la sua configurazione che dirà ad ASF quello che deve fare una volta avviato. Se lo avvii senza averlo configurato, ASF semplicemente non farà nulla.

* * *

## Installazione per ogni OS

Il linea generale, ecco cosa faremo nei prossimi minuti:

- Installare i **[prerequisiti per .NET Core](#net-core-prerequisites)**.
- Scaricare **[l'ultima versione di ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** specifica per il tuo sistema operativo.
- Estrarre l'archivio in una nuova cartella (ed usare il comando `chmod +x ArchiSteamFarm` se sei su Linux/OS X).
- **[Configurare ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**.
- Avviare ASF e vederlo all'opera.

Suona piuttosto semplice, vero? Allora iniziamo.

* * *

### Prerequisiti per .NET Core

Il primo passo consiste nell'assicurarsi che il proprio OS possa eseguire ASF correttamente. ASF è scritto in C# ed è basato su .NET Core, quindi potrebbe richiedere librerie native che non sono ancora disponibili sulla tua piattaforma. In base al tuo OS (Windows, Linux o OS X) ci saranno requisiti differenti, elencati nel documento **[Prerequisiti per .NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)** che dovresti proprio leggere. È la guida di riferimento che dovresti usare, ma per rendere le cose più semplici ed evitare di farti leggere troppo, sono anche riassunte in dettaglio qui sotto.

È perfettamente normale che alcuni, o anche tutti i prerequisiti siano già presenti nel tuo sistema, magari perché installati da applicazioni di terze parti che stai usando. Nonostante ciò, devi assicurarti che sia davvero questo il caso eseguendo l'installazione di ogni requisito per il tuo OS - senza di essi, ASF non funzionerà.

Tieni a mente che non hai bisogno di fare altro per le build specifiche per il vostro OS, come installare il .NET Core SDK, dacché i pacchetti specifici per ogni OS includono tutto ciò di cui si ha bisogno. Hai solo bisogno dei prerequisiti (o dipendenze) per .NET Core per far funzionare il runtime .NET Core incluso in ASF.

#### **[Windows](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-windows)**:

- **[Microsoft Visual C++ 2015 Redistributable Update](https://www.microsoft.com/en-us/download/details.aspx?id=53587)** (x64 for 64-bit Windows, x86 for 32-bit Windows)
- È fortemente consigliato assicurarsi gli aggiornamenti di Windows siano tutti installati. Gli aggiornamenti **[KB2533623](https://support.microsoft.com/en-us/help/2533623/microsoft-security-advisory-insecure-library-loading-could-allow-remot)** e **[KB2999226](https://support.microsoft.com/en-us/help/2999226/update-for-universal-c-runtime-in-windows)** sono fondamentali, ma potrebbero esserne richiesti anche degli altri. Se mantieni il tuo Windows aggiornato, saranno già installati. Assicurati di soddisfare tali requisiti prima di installare il pacchetto Visual C++.

#### **[Linux](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-linux)**:

Il nome del pacchetto varia in base alla distribuzione di Linux che stai usando, ma elencheremo quelli che sono i più comuni. Li puoi ottenere dal tuo gestore di pacchetti nativo in base all'OS che usi (come `apt` per Debian, o `yum` per CentOS).

- `libcurl` (`libcurl4`, `libcurl3`)
- `libicu` (latest version for your distribution, for example `libicu60`)
- `libkrb5-3` (`krb5-libs`)
- `liblttng-ust0` (`lttng-ust`)
- `libssl` (`libssl1.1`, `openssl-libs`, latest 1.1.X version for your distribution)
- `zlib1g` (`zlib`)

At least a few of those should be already natively available on your system (such as `zlib1g` that is required in almost every Linux distro nowadays).

#### **[OS X](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-macos)**:

- Per il momento nessuno, tuttavia è necessario avere OS X con versione 10.13 o superiore

* * *

### Download

Ora che abbiamo i requisiti necessari, il prossimo passo sarà scaricare **[l'ultima versione di ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**. ASF è disponibile in molte varianti, ma tu sei interessato al pacchetto corrispondente al tuo sistema sperativo ed architettura. Ad esempio, se stai usando `Win`dows a `64` bit, allora ti servirà il pacchetto `ASF-win-x64`. Per maggiori informazioni sulle versioni disponibili, consulta la sezione **[compatibilità](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility)**. ASF è anche in grado di funzionare su sistemi operativi per i quali non è disponibile un pacchetto specifico, come **Windows a 32 bit**; se questo è il tuo caso, vai direttamente alla sezione **[setup generico](#generic-setup)**.

![Assets](https://i.imgur.com/Ym2xPE5.png)

Dopo il download, inizia estraendo i file dell'archivio in cartella dedicata. Raccomandiamo di usare **[7-zip](https://www.7-zip.org)** o le utility standard come `unzip` per Linux/OS X. Dopodiché avrai un bel mucchio di cartelle e file. Non temere, puliremo tutto a breve.

Se stai usando Linux od OS X, non dimenticare di usare il comando `chmod +x ArchiSteamFarm`, dato che i permessi non vengono impostati nel file zip. Ciò va fatto solo dopo aver estratto l'archivio per la prima volta.

Assicurati di estrarre ASF **in una cartella ad esso destinata** e non in una che stai già usando per qualcos'altro - gli aggiornamenti automatici di ASF elimineranno ogni altro file già presente e non necessario al programma, il che significa perdere qualsiasi file non associato ad ASF. Se hai script aggiuntivi o file che vuoi usare con ASF, mettili in una cartella superiore.

Una struttura ideale è simile a questa:

    C:\ASF (dove metterai le tue cose)
        ├── Collegamento ad ASF.lnk (opzionale)
        ├── Collegamento a Config.lnk (opzionale)
        ├── Comandi.txt (opzionale)
        ├── IlMioScript.bat (opzionale)
        ├── ... (opzionalmente, ogni altro file di cui hai bisogno)
        └── Core (chiamala come preferisci, estrarrai l'archivio qui)
             ├── ArchiSteamFarm.dll
             ├── config
             └── (...)
    

Tale è la struttura che raccomandiamo, così che tu non debba passare attraverso lo sproposito di file e sottocartelle inclusi in ASF, dacché per utilizzarlo hai bisogno giusto di un collegamento al file eseguibile e alla cartella config.

Let's prepare ASF structure for usage. If you want to, you can now skip to the next step, since cleaning up ASF structure is not required (especially if you're using OS-specific builds that are already bundled), but it can make your life a bit easier.

You can open ASF folder and find core executable file, this will be `ArchiSteamFarm.exe` on Windows, and `ArchiSteamFarm` on Linux/OS X. Right click it and select "copy". Ora vai nella cartella in cui vorresti avere il collegamento ad ASF (come il tuo Desktop), fai click destro in un punto vuoto e seleziona "Incolla collegamento". Puoi anche rinominare il collegamento se lo vuoi, chiamandolo ad esempio "ASF". Ora fai anche la stessa cosa con la cartella `config` che trovi sempre nella cartella dell'eseguibile di ASF.

Dopo aver messo un po' d'ordine, avrai una comoda struttura di file e cartelle come la seguente:

![Structure](https://i.imgur.com/k85csaZ.png)

Ciò ti permetterà di accedere con facilità all'eseguibile di ASF oltre che ai file di configurazione. Nel mio caso ho deciso di usare la struttura menzionata all'inizio, cosicché i file di ASF risiedono direttamente nella cartella "Core". Puoi adattare tale struttura come meglio preferisci, avendo per esempio i collegamenti ad ASF e config sul Desktop, e la cartella dell'applicazione in `C:\ASF`.

Gli utenti Linux/OS X dovrebbero fare la stessa cosa, usando magari l'eccellente sistema di link simbolici con `ln -s`.

* * *

### Configurazione

Siamo ora pronti per affrontare l'ultima tappa, la configurazione. È di gran lunga la fase più complessa, poiché involve un gran numero di informazioni con le quali non hai ancora familiarità. Per questo ti forniremo diversi esempi e spiegazioni semplici da seguire.

Innanzitutto, è presente la pagina **[configurazione](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)** che spiega **tutto quel che c'è da sapere** sul processo di configurazione; tuttavia offre molte più informazioni di cui al momento abbiamo bisogno. Perciò, ti spiegheremo come ottenere le informazioni che stai cercando.

La configurazione di ASF può essere eseguita in due modi - sia con il nostro generatore web, sia manualmente. Ciò è spiegato nel dettaglio nella sezione **[configurazione](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**, quindi fai riferimento ad essa se vuoi informazioni più dettagliate. Essendo molto più semplice, utilizzeremo il configuratore web.

Apri la pagina del nostro **[generatore di configurazioni web](https://justarchinet.github.io/ASF-WebConfigGenerator)** con il browser che preferisci, avrai bisogno di abilitare javascript in caso tu lo abbia disabilitato manualmente. Raccomandiamo Chrome o Firefox, ma dovrebbe funzionare con i browser più usati.

Una volta caricata la pagina, clicca su "Bot". Dovresti avere davanti una pagina simile a questa:

![Bot tab](https://i.imgur.com/aF3k8Rg.png)

Se per qualche motivo la versione di ASF che hai appena scaricato è più vecchia di quella preselezionata dal generatore di configurazioni, seleziona la versione corretta dal menu a discesa. Ciò può accadere dacché il configuratore può essere usato anche per versioni più recenti di ASF, le "pre-release", che però non sono ancora ritenute stabili. Tu hai invece scaricato la più recente versione stabile di ASF, la cui affidabilità è stata comprovata.

Inizia dall'inserire il nome del tuo bot nel campo evidenziato in rosso. Può essere qualsiasi nome che tu desideri utilizzare, come il tuo nickname, nome dell'account, un numero o altro. C'è solo una parola che non puoi utilizzare, ed è `ASF`, poichè è riservata al file di configurazione generale. Inoltre, il nome del tuo bot non può iniziare con un punto (ASF ignora intenzionalmente questi file). Raccomandiamo inoltre di evitare l'uso degli spazi, ma puoi usare `_` come un separatore tra le parole se necessario.

Dopo aver deciso il nome del bot, clicca il bottone con il tick nella sezione `Enabled</0. Questo dichiara ad ASF se il bot deve essere avviato automaticamente dopo il lancio del programma.</p>

<p>Ora puoi scegliere tra due cose:</p>

<ul>
<li>Puoi inserire i tuoi dati nel campo <code>SteamLogin` e la tua password in `SteamPassword`</li> 

- O puoi lasciarli vuoti</ul> 

Facendo la prima cosa, autorizzi ASF ad utilizzare automaticamente le credenziali del tuo account durante il lancio del programma, evitando così di inserirle manualmente ogni volta che ASF ne ha bisogno. Puoi tuttavia decidere di ometterle: in questo caso non vengono salvate, perciò ASF non potrà iniziare automaticamente il suo lavoro senza il tuo aiuto e dovrai inserirli mentre il programma è in esecuzione.

ASF richiede le tue credenziali di accesso perchè include la propria implementazione del client di Steam, e per accedere ha bisogno degli stessi dettagli che utilizzi tu. Le tue credenziali di login non sono salvate da nessuna parte, ma solo sul tuo PC nella cartella `config` di ASF. Il nostro generazione di configurazioni web è basato sul client, e ciò significa che il codice viene eseguito solo in locale nel tuo browser per generare configurazioni valide di ASF. Non c'è quindi bisogno di preoccuparsi di eventuali fughe di dati sensibili. Nel caso non volessi inserire le tue credenziali lì, lo capiamo, e puoi inserirle manualmente più tardi in file generati, od ometterle interamente ed inserirle solamente nel prompt dei comandi di ASF. Per saperne di più sulla sicurezza, visita la sezione **[configuration](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**.

Puoi anche scegliere di lasciare un solo campo vuoto, come la `Password di Steam`, ed ASF utilizzerà automaticamente le tue credenziali, ma chiederà comunque l'inserimento di una password (simile al Client di Steam). Se utilizzi il sistema di controllo parentale di Steam per sbloccare l'account, dovrai inserirlo nel campo `SteamParentalCode`.

Dopo le tue decisioni e i dettagli facoltativi, la pagina web apparirà simile a quella qui sotto:

![Bot tab 2](https://i.imgur.com/yf54Ouc.png)

Ora puoi premere il tasto "download" ed il nostro generatore di configurazioni web genererà un file `json` basato sul nome scelto. Salva quel file nella cartella `config` di ASF. Puoi utilizzare collegamenti creati in precedenza di `config` oppure puoi trovare la cartella manualmente, direttamente tra i file di ASF.

La cartella `config` ora sarà simile a questa:

![Structure 2](https://i.imgur.com/crWdjcp.png)

Congratulazioni! Hai appena completato la configurazione fondamentale del bot ASF. La estenderemo a breve, ma per ora questo è tutto quello di cui hai bisogno.

* * *

### Eseguire ASF

Ora sei pronto per avviare il programma per la prima volta. Simply double-click ASF shortcut, or `ArchiSteamFarm` binary in ASF directory.

Dopo aver fatto tutto ciò, se hai installato tutte le dipendenze richieste nella prima parte, ASF si avvierà correttamente, noterà il tuo bot (se non hai dimenticato di inserire la configurazione generata nella cartella `config`), e proverà ad accedere:

![ASF](https://i.imgur.com/u5hrSFz.png)

Se hai fornito `SteamLogin` e `SteamPassword` ad ASF, ti sarà solamente chiesto il token per SteamGuard (e-mail, 2FA o nessuno, a seconda delle tue impostazioni di Steam). Se non lo hai fatto, ti sarà chiesto anche di inserire il nome utente e la password di Steam.

Ora è un ottimo momento per leggere la nostra **[privacy policy](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics#current-privacy-policy)** se sei preoccupato di sapere cosa succederà successivamente, come dichiarato da ASF stesso.

Dopo aver passato la fase del login iniziale, e se i tuoi dati sono corretti, eseguirai l'accesso correttamente, ed ASF inizierà a farmare carte utilizzando le impostazioni predefinite che non hai cambiato:

![ASF 2](https://i.imgur.com/Cb7DBl4.png)

Questo prova che ASF sta facendo un ottimo lavoro sul tuo account, ed ora puoi minimizzare il programma e fare qualcos'altro. Dopo un po'di tempo (a seconda della **[performance](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance)**), vedrai le carte di Steam arrivare lentamente nel tuo inventario. Ovviamente, per fare in modo che ciò succeda hai bisogno di giochi validi con cui farmare, e puoi sapere quali sono guardando nella **[pagina delle medaglie](https://steamcommunity.com/my/badges)** del tuo account. Se non ci sono giochi da cui ricavare carte, ASF dirà che non c'è nulla da fare, come indicato nelle nostre **[FAQ](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ#what-is-asf)**.

Con questo si conclude la nostra guida fondamentale sull'impostare ASF. Ora puoi decidere se vuoi configurare ancora di più ASF, oppure lasciare che faccia il suo lavoro con le impostazioni predefinite. Ora ci occuperemo di altri dettagli base, per poi lasciarti l'intera wiki per scoprire cose nuove.

* * *

### Configurazione avanzata

#### Far farmare diversi account in una volta

ASF supporta la possibilità di far farmare più account alla volta, che è la sua funzione primaria. Puoi aggiungere altri account ad ASF generando più file di configurazione dei bot, esattamente come hai generato il primo qualche minuto fa. Devi solo assicurarti di due cose:

- Nome del bot unico, se hai già il tuo primo bot chiamato "MainAccount", non potrai averne un altro con lo stesso nome.
- Dettagli di accesso validi, come `SteamLogin`, `SteamPassword` e `SteamParentalCode` (se usi le impostazioni di controllo parentale di Steam)

In altre parole, ritorna nuovamente alla configurazione ed esegui esattamente gli stessi passaggi per il tuo secondo o terzo account. Ricorda di usare nomi unici per tutti i tuoi bot.

* * *

#### Modifica delle impostazioni

Puoi cambiare le impostazioni esistenti creando un nuovo file di configurazione. Se non hai chiuso il nostro generatore di configurazioni web, clicca su "toggle advanced settings" e vedi cosa c'è da scoprire. Per questo tutorial, cambieremo l'impostazione `CustomGamePlayedWhileFarming`, che permette di impostare un nome personalizzato visibile su Steam quando ASF sta farmando, invece di mostrare il gioco effettivo.

Facciamolo, se esegui ASF e inizi a farmare, con le impostazioni predefinite vedrai che il tuo account di Steam è in-game:

![Steam](https://i.imgur.com/1VCDrGC.png)

Cambiamo il nome del gioco. Attiva le impostazioni avanzate nel generatore di configurazioni web, e trova `CustomGamePlayedWhileFarming`. Una volta fatto questo, metti il tuo testo personalizzato che vuoi far mostrare, come "Farmando carte":

![Bot tab 3](https://i.imgur.com/gHqdEqb.png)

Ora scarica il nuovo file di configurazione e **sovrascrivi** quello esistente. Puoi anche eliminare il file vecchio ed inserire quello nuovo, ovviamente.

Una volta fatto questo, fai ripartire ASF e noterai che il programma mostra il tuo testo personalizzato su Steam:

![Steam 2](https://i.imgur.com/vZg0G8P.png)

Questo conferma che hai modificato con successo la configurazione. Allo stesso modo, puoi cambiare le proprietà generali di ASF, passando dalla scheda "bot" a "ASF", scaricando il file `ASF.json` e inserendolo nella cartella `config`.

* * *

#### Utilizzo di ASF-ui

ASF è un'applicazione via terminale che non include un'interfaccia grafica. Tuttavia, stiamo lavorando molto su essa, e può diventare uno strumento molto dignitoso e facile da usare per accedere alle varie funzioni di ASF.

In order to use ASF-ui, you should ensure that you set up `IPC` and `SteamOwnerID` global configuration properties (ASF tab).

For `SteamOwnerID`, you need to input unique Steam identificator in 64-bit form of your account. You can look it up in various different ways, we'll use **[SteamRep](https://steamrep.com)** for that purpose. Open the website, locate sign in through Steam button in top right corner, then log in. Afterwards, in the same place, click on your avatar, and look up `steamID64` field on your profile.

![SteamRep](https://i.imgur.com/RUuJ63i.png)

For my account, this is `76561198006963719` number. You'll have a similar one, also starting from `7656`. Copy it.

Now navigate once again to our web config generator and input that number as SteamOwnerID.

![SteamOwnerID](https://i.imgur.com/V6jslfQ.png)

You need to do only one more thing, toggle advanced settings, find `IPC` option, and enable it.

![IPC](https://i.imgur.com/NhujZCN.png)

Now you can download your ASF config and put it in your `config` directory, as usual. Afterwards, launch ASF again, and you should be able to confirm that it properly started IPC interface:

![IPC 2](https://i.imgur.com/ZmkO8pk.png)

If you did everything properly, you'll now be able to access ASF's IPC interface under **[this](http://localhost:1242)** link, as long as ASF is running. You can use ASF-ui for various purposes, e.g. sending **[commands](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. Feel free to take a look around in order to find out all ASF-ui functionalities.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

Please note that ASF-ui is currently in preview state and not everything is available/working yet, but it's more than enough for simple ASF usage.

* * *

### Summary

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

- Installare i **[prerequisiti per .NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)**.
- Install **[.NET Core SDK](https://www.microsoft.com/net/download)** (or at least runtime) appropriate for your OS. You most likely want to use an installer. Refer to **[runtime requirements](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements)** if you're not sure which version to install.
- Download **[latest ASF release](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** in generic variant.
- Extract the archive into new location (and `chmod +x ArchiSteamFarm.sh` if you're on Linux/OS X).
- **[Configurare ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**.
- Launch ASF by either using a helper script or executing `dotnet /path/to/ArchiSteamFarm.dll` manually from your favourite shell.

Helper scripts (such as `ArchiSteamFarm.cmd` for Windows and `ArchiSteamFarm.sh` for Linux/OS X) are located next to `ArchiSteamFarm.dll` binary - those are included in generic variant only. You can use them if you don't want to execute `dotnet` command manually. You can also make a shortcut to those scripts like showed above, since they're supposed to provide binary replacement in a script way. Obviously helper scripts won't work if you didn't install .NET Core SDK and you don't have `dotnet` executable available in your `PATH`. Helper scripts are entirely optional to use, you can always `dotnet /path/to/ArchiSteamFarm.dll` manually.
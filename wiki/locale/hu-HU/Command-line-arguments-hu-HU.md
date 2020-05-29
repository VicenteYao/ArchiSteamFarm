# Parancssori argumentumok

Az ASF számos paranacssori argumentumot támogat, amikkel a program futását lehet befolyásolni. Haladó felhasználók számára ajánlott, akik finomítani szeretnék a program futását. Az alapértelmezett `ASF.json` konfigurációs fájlhoz képest a parancssori argumentumokkal az inicializációt (pl.: `--path`), platform specifikus beállításokat (pl.: `--system-required`), valamint érzékeny adatokat (pl.: `--cryptkey`) lehet beállítani.

* * *

## Használat

A használat függ az operációs rendszeredtől és az ASF verziódtól.

Általános használat:

```shell
dotnet ArchiSteamFarm.dll --argumentum --másikArgumentum
```

Windows esetén:

```powershell
.\ArchiSteamFarm.exe --argumentum --másikArgumentum
```

Linux/OS X esetén:

```shell
./ArchiSteamFarm --argumentum --másikArgumentum
```

A parancsori argumentumok az általános segítő szkriptekben is támogatottak, mint például `ArchiSteamFarm.cmd`, vagy `ArchiSteamFarm.sh`. A segítő szkriptekkel egyidejűleg az `ASF_ARGS` környezeti változót is használhatod, ahogy az a **[docker](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker#command-line-arguments)** szekcióban le van írva.

Ha az argumentumod szóközt is tartalmaz, ne felejts el idézőjelet használni. Ez a két sor hibás:

```shell
./ArchiSteamFarm --path /home/archi/Saját letöltések/ASF # Rossz!
./ArchiSteamFarm --path=/home/archi/Saját letöltések/ASF # Rossz!
```

Viszont ez a két sor jó:

```shell
./ArchiSteamFarm --path "/home/archi/Saját letöltések/ASF" # OK 
./ArchiSteamFarm "--path=/home/archi/Saját letöltések/ASF" # OK
```

## Argumentumok

`--cryptkey <key>` vagy `--cryptkey=<key>` - az ASF-t egyedi titkosító kulccsal fogja elindítani, melynek értéke `<key>`. Ez a beállítás a **[biztonságot](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Security)** befolyásolja, és kényszeríteni fogja az ASF-t, hogy az általad megadott `<key>` kulcsot használja az alapértelmezett helyett, ami a futtatható állományba van beleégetve. Tartsd észben, hogy azokat a jelszavakat, amik ezzel a kulccsal vannak titkosítva, minden ASF futtatás során meg kell adni.

A titkosító kulcsot lehetőség van úgy is beállítani, hogy az `ASF_CRYPTKEY` környezeti változót használod, ami azon emberek számára lehet hasznos, akik nem szeretnének érzékeny adatokat argumentumokon keresztül átadni.

* * *

`--network-group <group>` or `--network-group=<group>` - will cause ASF to init its limiters with additional custom network group having `<group>` value. This option affects running ASF in **[multiple instances](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** by signalizing that given instance is dependent only on instances sharing the same network group, and independent of the rest. Typically you want to use this property only if you're routing ASF requests through custom mechanism (e.g. different IP addresses) and you want to set networking groups yourself, without relying on ASF to do it automatically (which currently includes taking into account `WebProxy` only). Keep in mind that when using a custom network group, this is unique identifier within the local machine, and ASF will not take into account any other details, such as `WebProxy` value, allowing you to e.g. start two instances with different `WebProxy` values which are still dependent on each other.

Due to the nature of this property, it's also possible to set the value by declaring `ASF_NETWORK_GROUP` environment variable, which may be more appropriate for people that would want to avoid sensitive details in the process arguments.

* * *

`--no-restart` - ezt a kapcsolót főként a **[docker](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker)** tárolóink használják és kikényszerítik, hogy az `AutoRestart` értéke `false` legyen. Hacsak nincs valami speciális igényed, akkor érdemesebb lehet az `AutoRestart` tulajdonságot beállítani a konfigurációdban. Ez a kapcsoló azért van itt, hogy a docker szkriptünknek ne kelljen hozzányúlnia a globális konfigurációdhoz azért, hogy átállítsa a saját környezetének megfelelően. Természetesen ha az ASF-t egy szkripten belül futtatod, akkor lehet még ennek a kapcsolónak létjogosultsága (de egyébként jobban jársz a globális konfigurációs tulajdonsággal).

* * *

`--path <path>` or `--path=<path>` - alapértelmezetten az ASF mindig a saját könyvtárába fog navigálni induláskor. By specifying this argument, ASF will navigate to given directory after initialization, which allows you to use custom path for various application parts (including `config`, `plugins` and `www` directories, as well as `NLog.config` file), without a need of duplicating binary in the same place. Jól jöhet, ha szeretnéd külön választani a binárist a konfigurációtól, ahogy az a linux-szerű csomagokban megszokott dolog - így egy binárist több különféle beállítással is használhatsz. Az útvonal lehet relatív az ASF bináris helyéhez képest, vagy abszolút. Keep in mind that this command points to new "ASF home" - the directory that has the same structure as original ASF, with config directory inside, see below example for explanation.

Due to the nature of this property, it's also possible to set expected path by declaring `ASF_PATH` environment variable, which may be more appropriate for people that would want to avoid sensitive details in the process arguments.

If you're considering using this command-line argument for running multiple instances of ASF, we recommend reading our **[compatibility page](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** on this manner.

Examples:

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
    │           ├── logs (generated)
    │           ├── plugins (optional)
    │           ├── www (optional)
    │           ├── log.txt (generated)
    │           └── NLog.config (optional)
    └── ...
    

* * *

`--process-required` - ha megadod ezt a kapcsolót, akkor az alapértelmezett viselkedésével ellentétben, az ASF nem fog leállni, ha egyetlen bot sem fut már. Ez a nem leálló viselkedés akkor lehet hasznos, ha az **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC)**-vel együtt használod, mivel a felhasználók nagy része elvárja, hogy a web szolgáltatásuk mindig fusson a futó botok számától függetlenül. Ha IPC-t használsz, vagy más okból szeretnéd, ha az ASF processz addig fusson amíg te magad be nem zárod, akkor ezt az opciót neked találták ki.

Ha nem akarsz IPC-t futtatni, akkor ez az opció elég felesleges, mivel egyszerűen újraindíthatod a processzt, amikor szükség van rá (az ASF web szerverrel ellentétben, ahol folyamatosan futnia kell, ha parancsokat akarsz küldeni).

* * *

`--system-required` - ha beállítod ezt a kapcsolót, akkor az ASF jelezni fog az operációs rendszer számára, hogy a szüksége van arra, hogy az fusson a teljes élettartama alatt. Jelenleg ennek a kapcsolónak csak windowsos gépeken van értelme, mivel ott meg lehet tiltani, hogy a rendszer alvó módba menjen, amíg a processz futna. Ez akkor lehet hasznos, ha a gépeden, vagy laptopodon este farmolnál, így az ASF ébren fogja tartani a géped a farmolás ideje alatt, majd miután végzett, le fogja állítani magát, így a rendszered képes lesz alvó módba lépni, ezzel is áramot spórolva a farmolás befejeztével.

Tartsd észben, hogy ahhoz, hogy az ASF rendesen le tudjon állni más beállításokra is szükséged lesz: kerüld a `--process-required` használatát, valamint bizonyosodj meg róla, hogy a botjaidban be van állítva a `ShutdownOnFarmingFinished`. Természetesen az automatikus leállást csupán lehetővé teszi ez a funkció, de nem teszi kötelezővé, mivel például a `--process-required` argumentummal együtt is használható, így a rendszered gyakorlatilag sosem fog leállni, miután az ASF-t elindítottad.
# Аргументы командной строки

ASF поддерживает несколько аргументов командной строки, которые влияют на работу программы. Они могут использоваться опытными пользователями для изменения поведения программы. По сравнению с обычной настройкой через файл конфигурации `ASF.json`, аргументы командной строки используются для инициализации ядра (например, `--path`), специфичных для данной платформы настроек (например, `--system-required`) или конфиденциальных данных (например, `--cryptkey`).

* * *

## Использование

Способ использования аргументов зависит от вашей операционной системы и варианта ASF.

Общий:

```shell
dotnet ArchiSteamFarm.dll --argument --otherOne
```

Windows:

```powershell
.\ArchiSteamFarm.exe --argument --otherOne
```

Linux/OS X

```shell
./ArchiSteamFarm --argument --otherOne
```

Аргументы командной строки также поддерживаются во вспомогательных скриптах, таких как `ArchiSteamFarm.cmd` или `ArchiSteamFarm.sh`. В дополнении к этому, при использовании вспомогательных скриптов вы можете использовать переменные окружения `ASF_ARGS`, как указано в разделе **[Docker](https://github. com/JustArchiNET/ArchiSteamFarm/wiki/Docker-ru-RU#user-content-Аргументы-командной-строки)**.

Если ваш аргумент содержит пробелы, не забудьте заключить его в кавычки. Эти два примера неправильные:

```shell
./ArchiSteamFarm --path /home/archi/My Downloads/ASF # Плохо!
./ArchiSteamFarm --path=/home/archi/My Downloads/ASF # Тоже плохо!
```

Однако, эти два абсолютно корректны:

```shell
./ArchiSteamFarm --path "/home/archi/My Downloads/ASF" # Нормально
./ArchiSteamFarm "--path=/home/archi/My Downloads/ASF" # Тоже нормально
```

## Аргументы

`--cryptkey <key>` или `--cryptkey=<key>` - запустит ASF с пользовательским значением ключа шифрования `<key>`. Эта настройка влияет на **[безопасность](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Security-ru-RU)** и ASF будет использовать данный ключ шифрования `<key>` вместо внедрённого в исполняемый файл. Имейте в виду, что ключ, которым были зашифрованы пароли, должен передаваться при каждом запуске ASF.

Из-за природы этого параметра также есть возможность задавать ключ шифрования путём задания переменной среды `ASF_CRYPTKEY`, это может оказаться более подходящим для людей, которые хотели бы избежать наличия конфиденциальной информации в аргументах процесса.

* * *

`--network-group <group>` or `--network-group=<group>` - will cause ASF to init its limiters with additional custom network group having `<group>` value. This option affects running ASF in **[multiple instances](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** by signalizing that given instance is dependent only on instances sharing the same network group, and independent of the rest. Typically you want to use this property only if you're routing ASF requests through custom mechanism (e.g. different IP addresses) and you want to set networking groups yourself, without relying on ASF to do it automatically (which currently includes taking into account `WebProxy` only). Keep in mind that when using a custom network group, this is unique identifier within the local machine, and ASF will not take into account any other details, such as `WebProxy` value, allowing you to e.g. start two instances with different `WebProxy` values which are still dependent on each other.

Due to the nature of this property, it's also possible to set the value by declaring `ASF_NETWORK_GROUP` environment variable, which may be more appropriate for people that would want to avoid sensitive details in the process arguments.

* * *

`--no-restart` - данный параметр используется для контейнеров **[Docker](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker-ru-RU)** и принудительно задаёт параметру `AutoRestart` значение `false`. Если у вас нет какой-то конкретной цели, вам следует вместо этого настроить параметр `AutoRestart` в конфигурационном файле. Этот аргумент нужен чтобы нашему скрипту Docker не нужно было трогать ваш файл глобальной конфигурации чтобы приспособиться к своему окружению. Разумеется, если вы запускаете ASF из скрипта, возможно вы захотите тоже воспользоваться этим аргументом (в противном случае лучше использовать параметр глобальной конфигурации).

* * *

`--path <path>` или `--path=<path>` - ASF всегда использует папку из которой был запущен. При использовании этого аргумента ASF будет переходить после инициализации в указанную папку, что позволяет вам использовать произвольный путь для различных частей приложения (включая папки `config`, `plugins` и `www`, а также файл `NLog.config`), без необходимости дублировать исполняемый файл в заданном месте. Это может оказаться особенно полезно если вы хотите разделить исполняемые файлы от собственно файлов конфигурации, как это делается в пакетах Linux - таким образом вы можете использовать один (обновляемый) исполняемый файл с несколькими разными наборами конфигураций. Путь может быть как относительный, по отношению к текущему расположению исполняемого файла ASF, так и абсолютный. Не забывайте, что этот параметр указывает на новую "домашнюю папку ASF" - папку, которая имеет такую же структуру как оригинальная папка ASF, с папкой config внутри неё, см. пример ниже для пояснения.

Из-за природы этого параметра также есть возможность задавать необходимый путь с помощью задания переменной среды `ASF_PATH`, это может оказаться более подходящим для людей, которые хотели бы избежать наличия конфиденциальной информации в аргументах процесса.

Если вы планируете использовать этот аргумент командной строки для запуска нескольких копий ASF, мы рекомендуем вам также раздел, посвященный этому, на странице, посвященной **[совместимости](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-ru-RU#user-content-Запуск-нескольких-экземпляров)**.

Примеры:

```shell
dotnet /opt/ASF/ArchiSteamFarm.dll --path /opt/TargetDirectory # Абсолютный путь
dotnet /opt/ASF/ArchiSteamFarm.dll --path ../TargetDirectory # Относительный путь тоже работает
ASF_PATH=/opt/TargetDirectory dotnet /opt/ASF/ArchiSteamFarm.dll # Как и переменная окружения
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

`--process-required` - использование этого аргумента запретит выключение ASF в ситуации когда нет запущенных ботов. Отсутствие автоматического выключения особенно полезно в сочетании с **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-ru-RU)**, поскольку большинство пользователей будут ожидать что веб-интерфейс будет работать независимо от количества запущенных ботов. Если вы пользуетесь IPC или вам по другой причине нужно чтобы ASF продолжал работу пока вы не выключите его сами, это как раз то, что вам надо.

Если вы не планируете использовать IPC, этот аргумент выглядит довольно бесполезным для вас, поскольку вы всегда можете заново запустить процесс ASF когда он вам нужен (в отличии от серверного использования, когда вы ожидаете что ASF продолжит всё время ожидать от вас новых команд).

* * *

`--system-required` - указание этого аргумента заставит ASF попытаться сообщить ОС что этому процессу требуется чтобы система была запущена в течение всей его работы. На данный момент этот параметр имеет эффект только на машинах использующих Windows, где он запретит системе переходить в режим сна пока запущен процесс. Это может оказаться особенно полезно если вы фармите на вашем ПК или ноутбуке ночью, поскольку ASF сможет оставить вашу систему запущенной пока он фармит, а после окончания работы ASF завершится, и позволит вашей системе снова перейти в режим сна, позволяя сэкономить электроэнергию когда фарм завершен.

Помните, что для штатного автоматического завершения ASF могут понадобиться другие настройки - в особенности отсутствие аргумента `--process-required` и наличие у всех ботов включенного параметра `ShutdownOnFarmingFinished`. Конечно же, автоматическое завершение это только одно из возможных применений этой функции и не является обязательным, поскольку вы можете использовать этот аргумент совместно с например `--process-required`, фактически оставив вашу систему постоянно включенной после запуска ASF.
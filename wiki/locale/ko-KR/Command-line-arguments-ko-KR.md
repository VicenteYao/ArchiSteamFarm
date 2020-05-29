# 명령줄 인자

ASF는 프로그램 실행에 영향을 미칠 수 있는 여러 명령줄 인자에 대한 지원을 포함합니다. 이것은 프로그램이 어떻게 동작해야하는지 특정하기위한 고급사용자가 이용할 수 있습니다. `ASF.json` 설정 파일의 기본 방식과 비교하면, 명령줄 인자는 `--path` 등 주요 초기설정, `--system-required` 등 플랫폼 특화 설정, `--cryptkey` 등 민감한 데이터에 사용합니다.

* * *

## 사용법

사용중인 OS와 ASF 취향에 따라 사용법이 다릅니다.

일반:

```shell
dotnet ArchiSteamFarm.dll --인자1 --인자2
```

윈도우:

```powershell
.\ArchiSteamFarm.exe --인자1 --인자2
```

리눅스/OS X:

```shell
./ArchiSteamFarm --인자1 --인자2
```

명령줄 인자는 `ArchiSteamFarm.cmd`나 `ArchiSteamFarm.sh` 같은 일반 도우미 스크립트에서도 지원합니다. 그외에 도우미 스크립트를 사용할때 **[도커](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker-ko-KR#command-line-arguments)** 항목에 명시된 것 처럼 `ASF_ARGS` 환경변수를 사용할 수 있습니다.

인자에 공백이 들어간다면 따옴표로 표시하는 것을 잊지마십시오. 아래 두개는 잘못되었습니다:

```shell
./ArchiSteamFarm --path /home/archi/My Downloads/ASF # Bad!
./ArchiSteamFarm --path=/home/archi/My Downloads/ASF # Bad!
```

하지만, 다음 두개는 완전히 정상입니다.

```shell
./ArchiSteamFarm --path "/home/archi/My Downloads/ASF" # OK
./ArchiSteamFarm "--path=/home/archi/My Downloads/ASF" # OK
```

## 인자

`--cryptkey <key>` 혹은 `--cryptkey=<key>` - `<key>`값의 자체 암호화 키를 가지고 ASF를 시작합니다. 이 옵션은 **[보안](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Security-ko-KR)**에 영향을 주고 ASF가 실행파일에 하드코딩된 기본 키 대신 제공된 자체 `<key>`키를 사용하도록 합니다. ASF가 실행될 때마다 이 키로 암호화된 암호가 그 키를 필요로함을 명심하십시오.

이 속성값의 특성때문에 `ASF_CRYPTKEY` 환경 변수를 선언하여 cryptkey를 설정하는 것도 가능합니다. 인자 처리 중 민감한 정보를 피하고 싶은 사람들에게 더 적절합니다.

* * *

`--network-group <group>` or `--network-group=<group>` - will cause ASF to init its limiters with additional custom network group having `<group>` value. This option affects running ASF in **[multiple instances](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** by signalizing that given instance is dependent only on instances sharing the same network group, and independent of the rest. Typically you want to use this property only if you're routing ASF requests through custom mechanism (e.g. different IP addresses) and you want to set networking groups yourself, without relying on ASF to do it automatically (which currently includes taking into account `WebProxy` only). Keep in mind that when using a custom network group, this is unique identifier within the local machine, and ASF will not take into account any other details, such as `WebProxy` value, allowing you to e.g. start two instances with different `WebProxy` values which are still dependent on each other.

Due to the nature of this property, it's also possible to set the value by declaring `ASF_NETWORK_GROUP` environment variable, which may be more appropriate for people that would want to avoid sensitive details in the process arguments.

* * *

`--no-restart` - 이 스위치는 주로 **[도커](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Docker-ko-KR)** 컨테이너에서 사용하며 `AutoRestart` 값을 `false`로 강제합니다. 특별한 필요가 없다면 환경설정에서 `AutoRestart` 항목을 직접 설정하여야 합니다. 이 스위치는 도커 스크립트가 자체 환경설정을 적용할때 일반 환경설정을 건드리지 않아도 되도록 합니다. 물론 ASF를 스크립트 내부에서 실행하고 있다면 이 스위치를 활용할 수 있습니다. (그렇지 않다면 일반 환경설정 항목이 낫습니다)

* * *

`--path <path>` 혹은 `--path=<path>` - ASF는 설치시에 자체 디렉토리를 탐색합니다. 이 인자를 지정하면 ASF는 설치 후에 주어진 디렉토리를 탐색하고, 바이너리를 동일한 장소에 복제할 필요 없이 `config`, `plugins` 및 `www` 디렉토리와 `NLog.config`파일을 포함하는 다양한 어플리케이션 부분의 사용자 지정경로로 사용할 수 있습니다. 리눅스형태의 패키징에서 그런 것 처럼 바이너리와 실제 환경설정을 분리하고자 할때 특히 유용합니다. 이 방식으로 여러 다른 설치본을 하나의 (최신) 바이너리만으로 사용할 수 있습니다. 경로는 ASF 바이너리의 현재 위치에서 상대경로 또는 절대경로로 지정할 수 있습니다. Keep in mind that this command points to new "ASF home" - the directory that has the same structure as original ASF, with config directory inside, see below example for explanation.

이 속성값의 특성때문에 `ASF_PATH` 환경 변수를 선언하여 예상 경로를 설정하는 것도 가능합니다. 프로세스 인자 중 민감한 정보를 피하고 싶은 사람들에게 더 적절합니다.

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

`--process-required` - 실행중인 봇이 없는 경우 ASF를 종료하는 기본 설정을 비활성합니다. 자동 종료 금지 설정은 대부분의 사용자가 활성화된 봇의 갯수와 상관없이 웹서비스가 돌아가기를 원하는 **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-ko-KR)**와의 조합에서 특히 유용합니다. IPC 옵션을 사용중이거나 ASF를 직접 종료할때까지 계속 실행되기를 원하는 경우 알맞는 옵션입니다.

IPC를 실행할 생각이 없다면 그다지 쓸모 없습니다. 필요할때마다 ASF를 시작하면 됩니다.(이와 반대로 ASF 웹서버는 당신이 보내는 명령을 위해 항상 대기하고 있습니다)

* * *

`--system-required` - ASF가 전체 생애주기동안 시스템이 살아있어야 한다는 신호를 운영체제에 보내도록 합니다. 현재 이 스위치는 윈도 기기에서만 유효하며 프로세스가 실행되는 한 대기모드로 들어가는 것을 방지합니다. 밤에 PC나 랩탑에서 농사를 짓는 경우 특히 유용합니다. 농사를 짓는 동안 시스템은 계속 깨어있다가 ASF는 끝나면 평소처럼 꺼지고 시스템도 다시 대기모드로 들어가도록 하여 농사가 끝나면 전기를 즉시 절약합니다.

정확한 자동 종료를 위해 다른 ASF 설정도 필요함을 명심하십시오. 특히 `--process-required`를 피하고 모든 봇이 `ShutdownOnFarmingFinished` 설정을 따르는지를 확인하십시오. 물론 자동종료는 이 기능의 오직 한 가능성이고 필요조건은 아닙니다. 예를들어 이 옵션을 `--process-required`와 같이 사용한다면 ASF를 실행한 이후 시스템이 영원히 깨어있도록 할 수 있습니다.
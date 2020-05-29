# 컴파일

컴파일은 실행가능한 파일을 만드는 과정입니다. 만약 ASF에 자신이 만든 변경사항을 추가하고 싶거나 공식 **[릴리스](https://github.com/JustArchiNET/ArchiSteamFarm/releases-ko-KR)** 에서 제공하는 실행파일을 무슨 이유에서건 신뢰하지 않는 경우 하게 됩니다. 만약 당신이 개발자가 아니라 사용자라면 대부분 이미 컴파일된 바이너리를 사용하길 원할겁니다. 하지만 자신만의 바이너리를 사용하고 싶거나 뭔가 새로운 것을 배우고 싶다면 계속 읽으시기 바랍니다.

ASF는 필요로 하는 도구를 모두 가지고만 있다면 현재 지원되는 어떠한 플랫폼에서도 컴파일 될 수 있습니다.

* * *

## .NET Core SDK

ASF를 컴파일하려면 플랫폼과 상관없이 런타임뿐아니라 전체 .NET Core SDK가 필요합니다. **[.NET Core 설치 페이지(영문)](https://dotnet.microsoft.com/download)** 에서 설치 방법을 찾을 수 있습니다. 당신의 OS에 맞는 .NET Core SDK 버전을 설치해야 합니다. 성공적인 설치 후에 `dotnet` 명령이 작동하고 실행되어야 합니다. `dotnet --info` 로 작동 여부를 확인할 수 있습니다. 또한 .NET Core SDK가 ASF의 **[런타임 요구사항](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-ko-KR#runtime-requirements)** 과 일치하는지 확인하시기 바랍니다.

* * *

## 컴파일

적절한 .NET Core SDK 버전이 실행되고 있다고 가정하고, ASF 리포에서 클론되었거나 다운받아 압축해제한 ASF 소스 디렉토리로 이동해서 다음을 실행합니다:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/generic"
```

리눅스나 OS X를 사용한다면, 대신 `cc.sh` 스크립트를 사용할 수도 있습니다. 이는 좀 더 복잡한 방식이지만 동일하게 동작합니다.

컴파일이 성공적으로 끝나면 `out/generic` 디렉토리에 `소스` 맛으로 된 ASF가 있습니다. 이것은 공식 `일반` ASF 빌드도 동일하지만, 자체 빌드에 적합하도록 `UpdateChannel`과 `UpdatePeriod`이 `0` 값으로 되어있다는 점만 다릅니다.

### OS 특화

특정한 필요가 있다면 OS 특화 .NET Core 패키지를 생성할 수도 있습니다. 일반적으로 처음에 컴파일에 사용한 기 설치된 .NET Core 런타임으로 실행 가능한 `일반` 맛을 방금 컴파일 했기 때문에 그럴일은 없습니다만, 만일 필요하다면 다음과 같이 입력하십시오.

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/linux-x64" -r "linux-x64"
```

물론 대상으로 하는 OS 아키텍쳐에 따라 `linux-x64`를 `win-x64` 등으로 변경하십시오. 이 빌드도 업데이트가 비활성화됩니다.

### .NET Framework

드문 경우지만 `generic-netf` 패키지를 빌드하려는 경우 대상 프레임워크를 `netcoreapp3.1`에서 `net48`로 변경할 수 있습니다. `netf` 변수를 컴파일하려면 .NET Core SDK 뿐 아니라 적절한 **[.NET Framework](https://dotnet.microsoft.com/download/visual-studio-sdks)** 개발자 팩이 필요함을 명심하십시오. 따라서 아래의 내용은 오직 윈도우에서만 동작합니다:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "net48" -o "out/generic-netf"
```

.NET Framework나 심지어 .NET Core SDK도 설치할 수 없다면 `msbuild`를 직접 호출할 수 있습니다.(`linux-x86`에서 `mono`를 사용하여 빌드한 경우 등) 또한 ASF는 기본적으로 윈도우가 아닌 플랫폼에서 netf 빌드가 불가능하므로 `ASFNetFramework`를 수동으로 명시하여야 합니다.

```shell
msbuild /m /r /t:Publish /p:Configuration=Release /p:TargetFramework=net48 /p:PublishDir=out/generic-netf /p:ASFNetFramework=true ArchiSteamFarm
```

* * *

## 개발

ASF 코드를 편집하고 싶다면, 아무 .NET Core 호환 IDE나 사용할 수 있습니다. 옵션이긴 하지만 메모장으로 편집하고 위에서 설명한 `dotnet` 명령으로 컴파일 할 수도 있습니다. 하지만 윈도우의 경우 **[최신버전의 Visual Studio](https://visualstudio.microsoft.com/downloads)**를 권장합니다. (무료 커뮤니티 버전이면 충분합니다) 또한 선택적으로 **[ReSharper](https://www.jetbrains.com/resharper)** 를 같이 사용하는 것을 권장하지만 이것은 무료 제품은 아닙니다.

리눅스나 OS X에서 ASF 코드 작업을 하고 싶다면 **[최신 버전의 Visual Studio Code](https://code.visualstudio.com/download)**를 추천합니다. 고전 Visual Studio만큼 풍족하진 않지만, 충분히 좋습니다.

물론 위의 모든 제안은 단지 권장사항일 뿐이고, 당신은 원하는 모든 것을 사용할 수 있지만, 결국 `dotnet build` 명령으로 귀결됩니다. 우리는 ASF 개발에 Visual Studio와 ReSharper, 그리고 repo에 있는 서드 파티 `도구` 일부를 사용합니다.

* * *

## 태그

`master` 분기는 한번에 성공적인 컴파일이나 흠없는 ASF 실행을 보장하는 상태가 아닙니다. 개발 분기는 **[릴리스 주기](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle-ko-KR)**에 게시되어 있습니다. ASF를 소스에서 컴파일하거나 참조하려면 목적에 맞는 적절한 **[태그](https://github.com/JustArchiNET/ArchiSteamFarm/tags)** 를 사용해야 합니다. 이는 최소한 성공적인 컴파일을 보장하고, 안정 릴리스로 표시된 빌드는 거의 흠없는 실행도 가능합니다. 트리의 현재 "상태"를 체크하려면 **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** 나 **[Travis](https://travis-ci.com/JustArchiNET/ArchiSteamFarm)** 등 CI를 사용하시기 바랍니다.

* * *

## 공식 릴리스

공식 ASF 릴리스는 윈도우에서 ASF의 **[런타임 요구사항](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-ko-KR#runtime-requirements)**과 일치하는 최신 .NET Core SDK를 사용하여 **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** 로 컴파일됩니다. 테스트를 통과하면 모든 패키지는 GitHub에 배포됩니다. AppVeyor는 모든 빌드에 항상 공식 소스를 사용하므로 투명성을 보장하고, AppVeyor와 Github 에셋의 체크섬을 비교해볼 수 있습니다. ASF 개발자는 개인 개발과정과 디버깅을 제외하고는 스스로 컴파일하거나 빌드를 게시하지 않습니다.
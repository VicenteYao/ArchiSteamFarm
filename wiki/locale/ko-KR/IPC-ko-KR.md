# IPC

ASF는 유일한 자체 IPC 인터페이스를 가지고 있으며, 이로 인해 프로세스와 더 상호작용할 수 있습니다. IPC **[프로세스 간 통신(inter-process communication)](https://ko.wikipedia.org/wiki/%ED%94%84%EB%A1%9C%EC%84%B8%EC%8A%A4_%EA%B0%84_%ED%86%B5%EC%8B%A0)** 을 뜻하며, 가장 간단한 정의로 **[Kestrel HTTP 서버](https://github.com/aspnet/KestrelHttpServer)** 기반의 "ASF 웹 인터페이스" 입니다. 이는 최종사용자용 프론트엔드(ASF-ui)와 서드파티 통합용 백엔드(ASF API)로써 프로세스와 더 통합될 수 있습니다.

IPC는 우리의 필요와 능력에 따라 수많은 다른 것들로 사용될 수 있습니다. 예를 들어, ASF와 모든 봇의 상태를 가져오고, ASF에 명령어를 보내고, 일반/봇 환경설정을 가져와서 수정하고, 새로운 봇을 추가하고, 기존의 봇을 삭제하고, **[백그라운드 게임 등록기](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer-ko-KR)** 에 키를 보내고, ASF의 로그파일에 접근할 수 있습니다. 이 모든 작업은 API로 제공됩니다. 즉, 당신이 자신의 도구와 스크립트를 짜서 ASF 실행중에 ASF와 통신하고 영향을 미칠수 있다는 뜻입니다. 추가로, 명령어 전송같은 선택된 작업은 ASF-ui에 구현되어 있으며 친숙한 웹 인터페이스로 쉽게 접근할 수 있습니다.

* * *

# 사용법

IPC 인터페이스를 사용하려면 `IPC` **[일반 환경설정 속성값](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-ko-KR#일반-환경설정)** 을 활성화해야 합니다. ASF는 IPC의 실행을 로그에서 표시하여 IPC 인터페이스가 제대로 시작되었는지 확인할 수 있습니다.

    INFO|ASF|Start() Starting IPC server...
    INFO|ASF|Start() IPC server ready!
    

ASF의 http 서버가 이제 선택한 단말에서 수신중입니다. IPC에 사용자 지정 환경설정 파일을 넣지 않았다면, 이는 IPv4-기반의 **[127.0.0.1](http://127.0.0.1:1242)** 와 IPv6-기반의 **[[::1]](http://[::1]:1242)** 이며 포트 기본값은 `1242` 입니다. ASF 프로세스가 실행중인 동일한 기기에서 위의 링크로 IPC에 접속할 수 있습니다.

ASF의 IPC 인터페이스는 사용계획에 따라 세가지의 다른 접근방법을 제공합니다.

가장 낮은 단계로 IPC 인터페이스의 핵심이며 다른 모든 것이 동작하도록 하는 **[ASF API](#asf-api)** 가 있습니다. 자신만의 도구나 유틸리티, 프로젝트에서 ASF와 직접적으로 통신하기 위해 이를 사용해야 합니다.

중간 단계로 ASF API의 프론트엔드 역할을 하는 **[Swagger documentation](#swagger-documentation)** 이 있습니다. 이것은 ASF API의 완전한 문서를 제공하고 더 쉽게 접근할 수 있게 해줍니다. 만약 API를 통해 ASF와 통신하려는 도구, 유틸리티 혹은 다른 프로젝트를 작성하려면 Swagger 문서를 확인해보는 것이 좋습니다.

가장 높은 단계로 ASF API에 기반한 **[ASF-ui](#asf-ui)** 가 있으며, ASF 작업을 수행가능한 사용자 친화적인 방법을 제공합니다. 이것은 최종사용자를 위한 기본 IPC 인터페이스이며, ASF API로 만들수 있는 완벽한 예제입니다. 원한다면 `--path` **[명령줄 인자](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-ko-KR#인자)** 를 지정하고, 그곳에 위치한 사용자 지정 `www` 디렉토리를 사용하여 자신만의 사용자 지정 웹 UI를 사용할 수도 있습니다.

* * *

# ASF-ui

ASF-ui는 최종사용자에게 사용자 친화적인 그래픽 웹 인터페이스를 만들어주는 것을 목표로 하는 커뮤니티 프로젝트입니다. 이를 달성하기 위해 **[ASF API](#asf-api)** 의 프론트엔드로 동작하여 사용자가 다양한 작업을 쉽게 할 수 있습니다. 이것은 ASF와 함께 제공되는 기본 UI 입니다.

위에서 설명했듯이 ASF-ui는 ASF 핵심 개발자들이 유지관리하지 않는 커뮤니티 프로젝트입니다. **[ASF-ui 리포](https://github.com/JustArchiNET/ASF-ui)** 에 있는 자체 흐름을 따라가며, 이곳에서 모든 관련된 질문, 이슈, 버그 보고 및 제안이 이루어집니다.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

* * *

# ASF API

ASF API는 기본 데이터 형식인 JSON에 기반한 전형적인 **[REST 원리를 따르는](https://ko.wikipedia.org/wiki/REST)** 웹 API 입니다. 위리는 HTTP 상태코드(해당하는 경우)와, 직접 파싱해서 요청이 성공했는지 혹은 왜 실패했는지를 알 수 있는 응답을 모두 사용하여 응답을 정확하게 설명하기 위해 최선을 다하고 있습니다.

ASF API는 적절한 `/Api` 단말에 적절한 요청을 보내서 접근이 가능합니다. 이 API 단말을 사용해서 자신만의 도우미 스크립트, 도구, GUI 등등을 만들수 있습니다. 이것이 바로 ASF-ui의 비결이고, 다른 모든 도구도 동일합니다. ASF API는 공식적으로 지원되며 ASF 핵심 팀이 유지관리합니다.

이용가능한 단말, 설명, 요청, 응답, http 상태코드와 ASF API에 대한 다른 모든 것에 대한 완전한 문서는**[swagger 문서](#swagger-documentation)**를 참고하시기 바랍니다.

![ASF API](https://i.imgur.com/yggjf5v.png)

* * *

## 인증

ASF IPC 인터페이스는 기본적으로 `IPCPassword`가 `null`로 설정되어 있으므로 어떠한 종류의 인증도 요구하지 않습니다. 하지만 `IPCPassword`가 빈 값이 아닌것으로 설정되어 활성화되면, 모든 ASF API 호출은 `IPCPassword`에 맞는 암호를 요구할 것입니다. 인증을 생략하거나 잘못된 암호를 입력하면 `401 - Unauthorized` 오류가 발생합니다. 인증 없이 요청을 계속 보내면 결국 `403 - Forbidden` 오류와 함께 당신은 일시적으로 차단됩니다.

인증은 두가지 다른 방법으로 가능합니다.

### `Authentication` 헤더

일반적으로는 `Authentication` 항목에 암호를 값으로 넣어서 HTTP 요청 헤더를 사용해야 합니다. 이 방법은 ASF IPC 인터페이스에 접근하는 실제 도구에 따라 다릅니다. 예를 들어 `curl`을 사용한다면 `-H 'Authentication: MyPassword'` 를 인자로 넣어야 합니다. 이 방법으로 실제 인증이 일어나야 하는 요청의 헤더 부분에서 인증이 통과됩니다.

### 쿼리문 내의 `password` 매개변수

또는 `password` 매개변수를 호출하려는 URL의 마지막에 추가할 수도 있습니다. 예를 들어 `/Api/ASF` 대신에 `/Api/ASF?password=MyPassword`로 호출합니다. 이 접근방법도 충분히 좋지만 명백하게 암호를 열린 곳에 노출하므로 항상 적절하지는 않습니다. 게다가 쿼리문의 추가 인수이므로 URL이 복잡해보이고, 암호가 ASF API 통신 전체에 적용되는데도 마치 그 URL 전용인 것 같은 느낌을 줍니다.

* * *

두 방법 모두 지원되며 어느 것을 사용할지는 당신의 선택입니다. 우리는 가능한 모든 곳에 HTTP 헤더를 사용할 것을 권장합니다. 예를 들었듯이 HTTP 헤더가 쿼리문보다 더 적합합니다. 하지만 헤더 요청과 관련된 다양한 제한이 있기 때문에 우리는 쿼리문도 지원을 합니다. RFC에 따르면 완전히 유효합니다만, 자바스크립트에서 웹소켓 연결 시작시 사용자 지정 헤더의 부재는 좋은 예시 입니다. 이 경우 쿼리문은 유일한 인증방법입니다.

* * *

## Swagger 문서

IPC 인터페이스에는 ASF API, ASF-ui와 더불어 swagger 문서가 있으며 `/swagger` **[URL](http://localhost:1242/swagger)** 로 접근이 가능합니다. Swagger 문서는 API 구현과 이를 사용하는 ASF-ui 등 다른 도구간의 중간다리 역할을 합니다. 이것은 **[OpenAPI](https://swagger.io/resources/open-api)** 사양의 모든 API 단말의 완전한 문서이며 가능성입니다. 다른 프로젝트는 이를 쉽게 소비할 수 있으며 ASF API를 쉽게 작성하고 테스트할 수 있습니다.

Swagger 문서를 ASF API의 완전한 사양으로 사용하는 것과 별개로 주로 ASF-ui에 구현되지 않은 다양한 API 단말을 실행하는 사용자 친화적인 방법으로 사용할 수도 있습니다. Swagger 문서는 ASF 코드에서 자동으로 생성되므로 사용중인 ASF가 포함하는 API 단말과 문서가 항상 최신임을 보장합니다.

![Swagger 문서](https://i.imgur.com/mLpd5e4.png)

* * *

# 자주 묻는 질문(FAQ)

### ASF IPC 인터페이스는 안전합니까?

ASF는 기본적으로 `localhost` 주소를 수신합니다. 즉 당신 자신의 기기가 아닌 다른 기기는 ASF IPC에 접속하는 것은 **불가능합니다**. 당신이 기본 단말을 변경하지 않는 한, 공격자는 ASF IPC에 접근하려면 당신의 기기에 직접접근할 필요가 있습니다. 따라서 가장 안전하며 LAN에 접속된 이를 포함한 다른 누군가의 접근 가능성은 없습니다.

하지만 기본 `localhost` 할당 주소를 다른 무언가로 바꾸기로 했다면 공인된 IP만 ASF IPC 인터페이스에 접근하도록 적절한 방화벽 규칙을**직접** 설정해야 합니다. 이와 더불어 추가로 보안계층을 더해줄 `IPCPassword`를 설정할 것을 강력하게 권장합니다. 이 경우 ASF IPC 인터페이스를 역방향 프록시 뒤에서 실행하고 싶을수도 있습니다. 이는 아래에서 추가로 설명합니다.

### 내 자신의 도구나 사용자스크립트로 ASF API에 접근할 수 있습니까?

예. 이것은 ASF API가 설계된 이유이며 접근을 위한 HTTP 요청을 보내는 것으로 무엇이든 할 수 있습니다. 로컬 사용자스크립트는 **[교차 출처 리소스 공유(CORS, Cross-origin resource sharing)](https://ko.wikipedia.org/wiki/%EA%B5%90%EC%B0%A8_%EC%B6%9C%EC%B2%98_%EB%A6%AC%EC%86%8C%EC%8A%A4_%EA%B3%B5%EC%9C%A0)** 논리 구조를 따르므로, 추가 보안 조치로 `IPCPassword`가 설정되어 있다면 우리는 이를 위해 모든 곳(`*`)에서의 접근을 허용합니다. 이렇게 해서 잠재적으로 악의적인 스크립트가 자동으로 요청을 실행하는 것을 막으면서 다양한 인증받은 ASF API 요청을 실행할수 있습니다.(악의적인 스크립트는 요청 실행을 위해 `IPCPassword`를 알아야만 합니다.)

### 다른 기기 등에서 원격으로 ASF IPC에 접근할 수 있습니까?

예. 이를 위해 아래에서 설명하는 역방향 프록시의 사용을 권장합니다. 이렇게 해서 웹서버에 전형적인 방법으로 접근할 수 있고 ASF IPC를 동일한 기기에서 접근할 수 있습니다. 또는, 역방향 프록시를 실행하지 않으려면 적절한 URL을 가진 **[사용자 지정 환경설정](#사용자-지정-환경설정)** 을 사용할 수도 있습니다. 예를 들어, 당신의 기기가 `10.8.0.1` 주소를 가진 사설 VPN에 있다면 IPC 환경설정에서 수신 URL을 `http://10.8.0.1:1242`로 설정할 수 있습니다. 이렇게 해서 다른 어느 곳에서도 안되지만 당신의 사설 VPN에서는 IPC 접근이 가능해집니다.

### ASF IPC를 Apache나 Nginx 같은 역방향 프록시 뒤에서 사용할 수 있습니까?

**예**. IPC는 그런 설치와 완전히 호환되므로 원한다면 추가 보안과 호환성을 위해 당신의 도구 앞에서 호스팅해도 좋습니다. 일반적으로 ASF의 Kestrel http 서버는 인터넷에 직접 연결되었을 때 매우 안전하고 위험이 없습니다. 하지만 Apache나 Nginx 같은 역방향 프록시 뒤에 놓으면 다른 방법으로는 할 수 없는 추가 기능들, 예를 들면 ASF 인터페이스를 **[기초 인증(Basic auth)](https://en.wikipedia.org/wiki/Basic_access_authentication)** 으로 안전하게 하는 것 같은 기능들을 제공할지도 모릅니다.

Nginx 환경설정의 예시는 아래에 있습니다. 당신은 주로 `location` 블럭에 관심이 있겠지만 우리는 전체 `server` 블럭을 넣었습니다. 더 자세한 설명이 필요하면 **[nginx 문서(영문)](https://nginx.org/en/docs)** 를 참고하십시오.

```nginx
server {
    listen *:443 ssl;
    server_name asf.mydomain.com;
    ssl_certificate /path/to/your/certificate.crt;
    ssl_certificate_key /path/to/your/certificate.key;

    location ~* /Api/NLog {
        proxy_pass http://127.0.0.1:1242;
#       proxy_set_header Host 127.0.0.1; # Only if you need to override default host
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Host $host:$server_port;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_set_header X-Forwarded-Server $host;
        proxy_set_header X-Real-IP $remote_addr;

        # We add those 3 extra options for websockets proxying, see https://nginx.org/en/docs/http/websocket.html
        proxy_http_version 1.1;
        proxy_set_header Connection "Upgrade";
        proxy_set_header Upgrade $http_upgrade;
    }

    location / {
        proxy_pass http://127.0.0.1:1242;
#       proxy_set_header Host 127.0.0.1; # Only if you need to override default host
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Host $host:$server_port;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_set_header X-Forwarded-Server $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

다음은 아파치 환경설정의 예시입니다. 더 자세한 설명이 필요하면 **[apache 문서(영문)](https://httpd.apache.org/docs)** 를 참고하십시오.

```apache
<IfModule mod_ssl.c>
    <VirtualHost *:443>
        ServerName asf.mydomain.com

        SSLEngine On
        SSLCertificateFile /path/to/your/fullchain.pem
        SSLCertificateKeyFile /path/to/your/privkey.pem

        # TODO: Apache can't do case-insensitive matching properly, so we hardcode two most commonly used cases
        ProxyPass "/api/nlog" "ws://127.0.0.1:1242/api/nlog"
        ProxyPass "/Api/NLog" "ws://127.0.0.1:1242/Api/NLog"

        ProxyPass "/" "http://127.0.0.1:1242/"
    </VirtualHost>
</IfModule>
```

### HTTPS 프로토콜로 IPC 인터페이스에 접근할 수 있습니까?

**예**. 두가지 방법으로 가능합니다. 추천하는 방법은 위에서 설명한 역방향 프록시를 사용하여 웹서버에 http로 평소처럼 접근한 뒤, 그 기기에서 ASF IPC 인터페이스에 접속하는 것입니다. 이 방법은 트래픽이 완전히 암호화되므로 이런 설치를 위해 IPC를 어떤 방식으로든 수정할 필요가 없습니다.

두번째 방법은 ASF IPC 인터페이스의 **[사용자 지정 환경설정](#사용자-지정-환경설정)** 에 명시하는 것으로, https 단말을 활성화 하고 Kestrel http 서버에 적절한 인증을 직접 제공하는 것입니다. 이 방법은 다른 웹서버를 실행하지 않고 있으며 오직 ASF를 위해서 웹서버를 추가로 실행하고 싶지 않은 경우 추천합니다. 그렇지 않으면 역방향 프록시 구조를 사용하는 것이 설치하는데 훨씬 쉬울 것입니다.

* * *

## 사용자 지정 환경설정

IPC 인터페이스는 추가 환경설정 파일을 지원합니다.`IPC.config` 파일을 표준 ASF `config` 디렉토리에 넣으십시오.

사용가능한 경우 이 파일은 ASF Kestrel http 서버의 고급 환경설정과 다른 IPC 관련 조정사항을 정의합니다. 특정한 필요가 없다면 이 파일을 사용할 이유는 없습니다. ASF는 이 경우 합리적인 기본값을 사용합니다.

환경설정 파일은 다음의 JSON 구조를 기반으로 합니다.

```json
{
    "Kestrel": {
        "Endpoints": {
            "example-http4": {
                "Url": "http://127.0.0.1:1242"
            },
            "example-http6": {
                "Url": "http://[::1]:1242"
            },
            "example-https4": {
                "Url": "https://127.0.0.1:1242",
                "Certificate": {
                    "Path": "/path/to/certificate.pfx",
                    "Password": "passwordToPfxFileAbove"
                }
            },
            "example-https6": {
                "Url": "https://[::1]:1242",
                "Certificate": {
                    "Path": "/path/to/certificate.pfx",
                    "Password": "passwordToPfxFileAbove"
                }
            }
        },
        "PathBase": "/"
    }
}
```

설명하고 수정할만한 가치가 있는 2개의 속성값이 있습니다. `Endpoints`와 `PathBase` 입니다.

`Endpoints` - 이것은 단말의 집합입니다. 모든 단말은 `example-http4`와 같은 유일한 이름을 가지고 있어야 하며, `Url` 속성값은 `Protocol://Host:Port` 수신주소를 특정합니다. 기본적으로 ASF는 IPv4와 IPv6 http 주소를 수신하지만, 필요할지 몰라 사용할 수 있는 https 예시를 추가해 두었습니다. 필요한 단말 만을 선언해야 합니다. 당신이 수정하기 쉽도록 위에 4개의 예시를 들어놓았습니다.

`Host`는 ASF의 http 서버가 모든 가능한 인터페이스를 할당하는 `*` 값을 포함한 다양한 값을 허용합니다. 원격 접속을 허용하려고 `Host` 값을 사용할 때 매우 주의하십시오. 그렇게 하면 ASF IPC 인터페이스는 다른 기기에서의 접근을 허용하고, 보안 위험이 될 수 있습니다. 이 경우 **최소한** `IPCPassword`, 그리고 방화벽의 사용을 강력하게 권장합니다.

`PathBase` - IPC 인터페이스가 사용될 기본 경로입니다. 이 속성값은 선택사항입니다. 기본값은 `/`인데 대부분의 사용례에서 수정할 필요가 없습니다. 이 속성값을 변경하면 전체 IPC 인터페이스를 사용자 지정 접두사에서 호스팅하게 됩니다. 예를 들어 `http://localhost:1242` 가 아니라 `http://localhost:1242/MyPrefix` 가 됩니다. 사용자 지정 `PathBase` 는 특정한 URL만 프록시하기 원하는 역방향 프록시의 특정 설치와의 조합으로 사용할 수 있습니다. 예를 들어 전체 `mydomain.com` 도메인이 아닌`mydomain.com/ASF` 를 사용할 수 있습니다. 이 경우 보통 `mydomain.com/ASF/Api/X` -> `localhost:1242/Api/X`로 매핑하도록 웹서버 규칙을 다시 작성해야 하지만, 대신 사용자 지정 `PathBase` 를 `/ASF`로 하면 `mydomain.com/ASF/Api/X` -> `localhost:1242/ASF/Api/X`로 설치하는 것이 쉬워집니다.

당신이 사용자 지정 기본 경로를 지정할 필요가 있다고 진심으로 믿지 않는 한, 기본값으로 두는 것이 최선입니다.

### 환경설정 예시

아래의 환경설정은 모든 곳에서의 원격접속을 허락하므로, 위에서 설명한 보안관련 내용을 읽고 이해하였는지 확인하시기 바랍니다.

```json
{
    "Kestrel": {
        "Endpoints": {
            "HTTP": {
                "Url": "http://*:1242"
            }
        }
    }
}
```

모든 곳으로 부터의 접속이 필요하지 않고 예를 들어 내부 LAN만 필요하다면, `*` 대신 `192.168.0.*`를 사용하는 것은 훨씬 좋은 생각입니다. 다른 주소를 사용한다면 적절한 네트워크 주소를 적용하십시오.
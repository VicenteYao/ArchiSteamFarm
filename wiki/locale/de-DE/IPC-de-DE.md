# IPC

ASF beinhaltet eine eigene einzigartige IPC-Schnittstelle, die für die weitere Interaktion mit dem Prozess verwendet werden kann. IPC steht für **[inter-process communication](https://en.wikipedia.org/wiki/Inter-process_communication)** und in der einfachsten Definition ist es eine "ASF-Weboberfläche" basierend auf **[Kestrel HTTP Server](https://github.com/aspnet/KestrelHttpServer)**, welches für die weitere Integration mit dem Prozess, sowohl als Frontend für Endbenutzer (ASF-ui) als auch Backend für Drittanbieter-Integrationen (ASF-API) verwendet werden kann.

IPC kann für viele verschiedene Dinge verwendet werden, je nach deinen Fähigkeiten und Bedürfnissen. Du kannst es beispielsweise dafür verwenden, um den Status von ASF und allen Bots abzurufen, ASF-Befehle zu senden, Globale- und Bot-Konfigurationen abzurufen und zu bearbeiten, neue Bots hinzuzufügen, bestehende Bots zu löschen, Produktschlüssel an den **[BGR](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer-de-DE)** zu senden oder auf das ASF-Protokoll zuzugreifen. Alle diese Aktionen werden durch unsere API offengelegt, was bedeutet, dass du deine eigenen Programme und Skripte schreiben kannst, die in der Lage sind, mit ASF zu kommunizieren und während der Laufzeit zu beeinflussen. Darüber hinaus sind ausgewählte Aktionen (z.B. das Senden von Befehlen) auch in unserem ASF-ui vorhanden, so dass du über eine benutzerfreundliche Weboberfläche einfach darauf zugreifen kannst.

* * *

# Nutzung

Du kannst unsere IPC-Schnittstelle aktivieren, indem du `IPC` **[globale Konfigurationseigenschaft](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-de-DE#ipc)** aktivierst. ASF gibt den IPC-Start in seinem Protokoll an, mit dem du überprüfen kannst, ob die IPC-Schnittstelle ordnungsgemäß gestartet wurde:

    INFO|ASF|Start() Starte IPC-Server...
    INFO|ASF|Start() IPC-Server bereit!
    

ASF's http-Server hört nun auf den ausgewählten Endpunkten. Falls du keine benutzerdefinierte Konfigurationsdatei für IPC angegeben hast wird für IPv4 **[127.0.0.1](http://127.0.0.1:1242)** und für IPv6 **[[::1]](http://[::1]:1242)** auf Standard-Port `1242` verwendet. Du kannst auf unsere IPC-Schnittstelle über die obigen Links von derselben Maschine aus zugreifen, auf der auch der ASF-Prozess läuft.

Die IPC-Schnittstelle von ASF bietet je nach der von dir geplanten Nutzung drei verschiedene Möglichkeiten, darauf zuzugreifen.

Auf der untersten Ebene gibt es **[ASF API](#asf-api)**, das ist der Kern unserer IPC-Schnittstelle und ermöglicht den Betrieb aller anderen. Dies ist es, was du in deinen eigenen Programmen, Dienstprogrammen und Projekten verwenden solltest, um direkt mit ASF zu kommunizieren.

Auf der mittleren Ebene befindet sich unsere **[Swagger-Dokumentation](#swagger-dokumentation)**, die als Frontend für die ASF-API dient. Es bietet eine vollständige Dokumentation der ASF-API und ermöglicht einen einfacheren Zugriff darauf. Dies ist es, was du nutzen solltest, wenn du vorhast ein Programm, ein Dienstprogramm oder andere Projekte zu schreiben, die über die API mit ASF kommunizieren sollen.

Auf der obersten Ebene gibt es **[ASF-ui](#asf-ui)**, das auf unserer ASF-API basiert und eine benutzerfreundliche Möglichkeit bietet, verschiedene ASF-Aktionen auszuführen. Dies ist unsere Standard IPC-Schnittstelle für Endanwender und ein perfektes Beispiel dafür, was du mit der ASF-API erstellen kannst. Wenn du möchtest, kannst du deine eigene benutzerdefinierte Web-Benutzeroberfläche für die Verwendung mit ASF benutzen, indem du `--path` **[Befehlszeilenargument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-de-DE#argumente)** angibst und das benutzerdefinierten `www` Verzeichnis verwendest.

* * *

# ASF-ui

ASF-ui ist ein Gemeinschaftsprojekt, das darauf abzielt, eine benutzerfreundliche grafische Weboberfläche für Endbenutzer zu erstellen. Um dies zu erreichen, fungiert es als Frontend für unsere **[ASF API](#asf-api)**, so dass du verschiedene Aktionen mit Leichtigkeit durchführen kannst. Dies ist die Standardoberfläche mit der ASF ausgeliefert wird.

Wie bereits erwähnt, ist ASF-ui ein Community-Projekt, das nicht von kern ASF-Entwicklern betreut wird. Es folgt seinem eigenen Fluss in der **[ASF-ui Repository](https://github.com/JustArchiNET/ASF-ui)**, welches für alle damit verbundenen Fragen, Probleme, Fehlerberichte und Vorschläge verwendet werden sollte.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

* * *

# ASF API

Unsere ASF-API ist eine typische **[RESTful](https://en.wikipedia.org/wiki/Representational_state_transfer)** Web-API, die auf JSON als primärem Datenformat basiert. Wir tun unser Bestes, um die Antwort genau zu beschreiben, indem wir sowohl HTTP-Statuscodes (wo zutreffend) als auch eine Antwort verwenden, die du selbst analysieren kannst, um zu wissen, ob die Anfrage erfolgreich war und wenn nicht, warum.

Auf unsere ASF-API kann zugegriffen werden, indem entsprechende Anfragen an entsprechende `/Api` Endpunkte gesendet werden. Du kannst diese API-Endpunkte verwenden um deine eigenen Hilfsskripte, Programme, Benutzeroberflächen und ähnliches zu erstellen. Das ist genau das, was unser ASF-ui unter der Haube leistet und jedes andere Programm kann das gleiche erreichen. ASF-API wird offiziell vom ASF-Kernteam unterstützt und gepflegt.

Für eine vollständige Dokumentation der verfügbaren Endpunkte, Beschreibungen, Anfragen, Antworten, http-Statuscodes und alles andere was die ASF-API berücksichtigt, ließ bitte unsere **[swagger-Dokumentation](#swagger-dokumentation)**.

![ASF API](https://i.imgur.com/yggjf5v.png)

* * *

## Authentifizierung

Die ASF IPC-Schnittstelle erfordert standardmäßig keine Art von Authentifizierung, da `IPCPassword` auf `null` gesetzt ist. Wenn jedoch `IPCPassword` durch das Setzen eines beliebigen nicht leeren Wertes aktiviert wird, erfordert jeder Aufruf der ASF-API das Passwort, das mit dem eingestellten `IPCPassword` übereinstimmt. Wenn du die Authentifizierung weglässt oder ein falsches Passwort eingibst, bekommst du einen `401 - Unauthorized` Fehler. Wenn du weiterhin Anfragen ohne Authentifizierung sendest, wirst du schließlich vorübergehend mit dem Fehler `403 - Forbidden` gesperrt.

Die Authentifizierung kann auf zwei verschiedene Arten erfolgen.

### `Authentication` Header

Im Allgemeinen solltest du HTTP-Request-Header verwenden, indem du das Feld `Authentication` mit deinem Passwort als Wert setzt. Die Vorgehensweise hängt davon ab, mit welchem Programm du auf die IPC-Schnittstelle von ASF zugreifst, z.B. wenn du `curl` verwendest, dann solltest du `-H 'Authentication: MeinPasswort'` als Parameter hinzufügen. Auf diese Weise wird die Authentifizierung in den Headern der Anfrage übergeben, wo sie tatsächlich stattfinden soll.

### `password` Parameter im Query-String

Alternativ kannst du den Parameter `password` an das Ende der URL anhängen die du aufrufen möchtest, z.B. indem du `/Api/ASF?password=MeinPasswort` anstelle von `/Api/ASF` allein aufrufst. Dieser Ansatz ist gut genug, aber offensichtlich enthüllt er das Passwort, was nicht unbedingt immer angemessen ist. Darüber hinaus ist es ein zusätzliches Argument im Query-String, das das Aussehen der URL verkompliziert und das Gefühl gibt, dass sie URL-spezifisch ist, während das Passwort für die gesamte ASF-API-Kommunikation gilt.

* * *

Beide Wege werden unterstützt und es liegt ganz bei dir, welche du wählen möchtest. Wir empfehlen HTTP-Header überall dort zu verwenden, wo es möglich ist, da es in der Verwendung sinnvoller ist als ein Query-String. Wir unterstützen jedoch auch Query-Strings, hauptsächlich wegen verschiedener Einschränkungen in Bezug auf Request-Header. Ein gutes Beispiel ist das Fehlen von benutzerdefinierten Headern beim Einleiten einer Websocket-Verbindung in JavaScript (obwohl sie nach RFC vollständig gültig ist). In dieser Situation ist ein Query-String die einzige Möglichkeit sich zu authentifizieren.

* * *

## Swagger Dokumentation

Unsere IPC-Schnittstelle, zusätzlich zu ASF API und ASF-ui, beinhaltet auch die Swagger-Dokumentation, welche unter der `/swagger` **[URL](http://localhost:1242/swagger) ** verfügbar ist. Die Swagger-Dokumentation dient als Mittelsmann zwischen unserer API-Implementierung und anderen Programmen, die sie verwenden (z.B. ASF-ui). Es bietet eine vollständige Dokumentation und Verfügbarkeit aller API-Endpunkte in der Spezifikation **[OpenAPI](https://swagger.io/resources/open-api)**, die von anderen Projekten problemlos genutzt werden kann, so dass du ASF-API mit Leichtigkeit schreiben und testen kannst.

Neben der Verwendung unserer Swagger-Dokumentation als komplette Spezifikation der ASF-API kannst du sie auch als benutzerfreundliche Möglichkeit verwenden, verschiedene API-Endpunkte auszuführen, vor allem solche die nicht von ASF-ui implementiert werden. Da unsere Swagger-Dokumentation automatisch aus ASF-Quelltext generiert wird, hast du die Garantie, dass die Dokumentation immer auf dem neuesten Stand der API-Endpunkte ist die deine Version von ASF enthält.

![Swagger Dokumentation](https://i.imgur.com/mLpd5e4.png)

* * *

# FAQ

### Ist die IPC-Schnittstelle von ASF sicher und sicher in der Anwendung?

ASF hört standardmäßig nur auf `localhost` Adressen, was bedeutet, dass der Zugriff auf ASF IPC von jedem anderen Rechner als deinem eigenen **nicht möglich ist**. Wenn du keine Standard-Endpunkte änderst, bräuchte der Angreifer einen direkten Zugriff auf deinen eigenen Computer, um auf ASF IPC zugreifen zu können, daher ist er so sicher wie möglich und es besteht keine Möglichkeit, dass andere Personen auf ihn zugreifen, auch nicht aus deinem eigenen LAN.

Wenn du dich jedoch dazu entscheidest die standardmäßig eingestellten `localhost` Adressen an etwas anderes zu binden, dann solltest du die richtigen Firewall-Regeln **selbst** festlegen, um nur autorisierten IPs den Zugriff auf die IPC-Schnittstelle von ASF zu ermöglichen. Zusätzlich dazu empfehlen wir dringend, `IPCPassword` einzurichten, was eine weitere Ebene der zusätzlichen Sicherheit bietet. Möglicherweise möchtest du in diesem Fall auch die IPC-Schnittstelle von ASF hinter einem Reverse-Proxy ausführen, was im Folgenden näher erläutert wird.

### Kann ich mit eigenen Programmen oder Benutzerskripten auf die ASF-API zugreifen?

Ja, dafür wurde die ASF-API entwickelt und du kannst alles verwenden was fähig ist eine HTTP-Anfrage zu senden um darauf zuzugreifen. Lokale Benutzerskripte folgen der **[CORS](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing)** Logik, und wir erlauben den Zugriff von allen Ursprüngen für diese (`*`) solange `IPCPassword` gesetzt ist (als zusätzliche Sicherheitsmaßnahme). Auf diese Weise kannst du verschiedene authentifizierte ASF-API-Anfragen ausführen, ohne dass potenziell bösartige Skripte dies automatisch tun können (da sie dazu dein `IPCPassword` kennen müssten).

### Kann ich aus der Ferne auf die IPC-Schnittstelle von ASF zugreifen, z.B. von einer anderen Maschine aus?

Ja, wir empfehlen dafür einen Reverse-Proxy zu verwenden (siehe unten). Auf diese Weise kannst du wie gewohnt auf deinen Webserver zugreifen, der dann auf der gleichen Maschine auf die IPC-Schnittstelle von ASF zugreift. Andernfalls, wenn du nicht mit einem Reverse-Proxy arbeiten möchtest, kannst du die **[benutzerdefinierte Konfiguration](#benutzerdefinierte-konfiguration)** mit entsprechender URL verwenden. Wenn sich dein Computer beispielsweise in einem privaten VPN mit der Adresse `10.8.0.1` befindet, dann kannst du als Abhör-URL `http://10.8.0.1:1242` in der IPC-Konfiguration einstellen, was den IPC-Zugriff von deinem privaten VPN aus ermöglichen würde, aber nicht von irgendwo anders.

### Kann ich die IPC-Schnittstelle von ASF hinter einem Reverse-Proxy wie Apache oder Nginx verwenden?

**Ja**, unser IPC ist voll kompatibel mit einem solchen Setup, so dass wenn du möchtest du ihn auch direkt vor deinen eigenen Programmen hosten kannst, um zusätzliche Sicherheit und Kompatibilität zu gewährleisten. Im Allgemeinen ist der Kestrel http-Server von ASF sehr sicher und birgt kein Risiko, wenn er direkt mit dem Internet verbunden ist, aber wenn man ihn hinter einen Reverse-Proxy wie Apache oder Nginx stellt, kann er zusätzliche Funktionen bieten die sonst nicht möglich wären, wie z.B. die Sicherung der ASF-Schnittstelle mit einer **[Basic Authentication](https://en.wikipedia.org/wiki/Basic_access_authentication)**.

Eine beispielhafte Nginx-Konfiguration findest du unten. Wir haben den kompletten `server` Block hinzugefügt, obwohl du dich hauptsächlich für `location` interessierst. Bitte lies die **[Nginx-Dokumentation](https://nginx.org/en/docs)** falls du weitere Erklärungen brauchst.

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

Ein Apache-Konfiguration Beispiel kann unterhalb gefunden werden. Bitte wenden Sie sich an die **[apache documentation](https://httpd.apache.org/docs)** sollten Sie weitere Erklärungen benötigen.

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

### Kann ich über das HTTPS-Protokoll auf die IPC-Schnittstelle zugreifen?

**Ja**, du kannst es auf zwei verschiedene Arten erreichen. Eine empfohlene Methode wäre die Verwendung eines Reverse-Proxy (oben beschrieben), bei dem du wie üblich über https auf deinen Webserver zugreifen und dich über ihn mit der IPC-Schnittstelle von ASF auf derselben Maschine verbinden kannst. Auf diese Weise ist dein Datenverkehr vollständig verschlüsselt und du musst IPC in keiner Weise ändern um ein solches Setup zu unterstützen.

Die zweite Möglichkeit besteht darin eine **[benutzerdefinierte Konfiguration](#benutzerdefinierte-konfiguration)** für die IPC-Schnittstelle von ASF zu spezifizieren, wo du https-Endpunkt aktivieren und das entsprechende Zertifikat direkt an unseren Kestrel http-Server senden kannst. Dieser Weg wird empfohlen, wenn du keinen anderen Webserver betreibst und keinen ausschließlich für ASF betreiben möchtest. Andernfalls ist es viel einfacher ein zufriedenstellendes Setup mit Hilfe eines Reverse-Proxy-Mechanismus zu erreichen.

* * *

## Benutzerdefinierte Konfiguration

Unsere IPC-Schnittstelle unterstützt eine zusätzliche Konfigurationsdatei, `IPC.config`, die in das Standard ASF-Verzeichnis `config` gelegt werden sollte.

Wenn verfügbar, gibt diese Datei die erweiterte Konfiguration des ASF Kestrel http-Servers zusammen mit anderen IPC-bezogenen Einstellungen an. Wenn du keinen besonderen Anlass hast gibt es keinen Grund für dich diese Datei zu verwenden, da ASF in diesem Fall bereits sinnvolle Standardwerte verwendet.

Die Konfigurationsdatei basiert auf folgender JSON-Struktur:

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

Es gibt 2 Eigenschaften die es wert sind erklärt/bearbeitet zu werden, nämlich `Endpoints` und `PathBase`.

`Endpoints` - Dies ist eine Sammlung von Endpunkten, wobei jeder Endpunkt seinen eigenen eindeutigen Namen hat (wie z.B. `example-http4`) und eine `Url` Eigenschaft, welche die `Protokoll://Host:Port` Abhöradresse angibt. Standardmäßig hört ASF auf IPv4- und IPv6-Http-Adressen, aber wir haben https-Beispiele hinzugefügt die du bei Bedarf verwenden kannst. Du solltest nur die Endpunkte deklarieren die du benötigst. Wir haben oben 4 Beispiele hinzugefügt damit du sie leichter bearbeiten kannst.

`Host` akzeptiert eine Vielzahl von Werten, einschließlich dem Wert `*`, der den http-Server von ASF an alle verfügbaren Schnittstellen bindet. Achte sehr genau darauf wenn du `Host` Werte verwendest, da sie den Fernzugriff erlauben. Dadurch wird der Zugriff auf die IPC-Schnittstelle von ASF von anderen Maschinen aus ermöglicht, was ein Sicherheitsrisiko darstellen kann. In diesem Fall empfehlen wir dringend die Nutzung von `IPCPassword` (und vorzugsweise auch einer eigenen Firewall) **als Mindestmaß**.

`PathBase` - Dies ist der Basispfad der von der IPC-Schnittstelle verwendet wird. Diese Eigenschaft ist optional, voreingestellt auf `/` und sollte für die meisten Anwendungsfälle nicht geändert werden müssen. Wenn du diese Eigenschaft änderst, hostest du die gesamte IPC-Schnittstelle auf einem benutzerdefinierten Präfix, zum Beispiel `http://localhost:1242/MeinPrefix` anstelle von `http://localhost:1242` allein. Die Verwendung von einem benutzerdefinierten `PathBase` könnte in Kombination mit der spezifischen Einrichtung eines Reverse-Proxy erwünscht sein, bei dem du nur eine bestimmte URL proxyen möchtest, z.B. `meinedomain.com/ASF` statt der gesamten `meinedomain.com` Domain. Normalerweise würde das erfordern, dass du eine Umschreibungsregel für deinen Webserver schreibst, die `meinedomain.com/ASF/Api/X` -> `localhost:1242/Api/X` abbilden würde. Aber stattdessen kannst du einen benutzerdefinierten `PathBase` von `/ASF` definieren und eine einfachere Einrichtung von `meinedomain.com/ASF/Api/X` -> `localhost:1242/ASF/Api/X` erreichen.

Wenn du nicht wirklich einen benutzerdefinierten Basispfad angeben musst, ist es am besten ihn bei der Standardeinstellung zu belassen.

### Beispielhafte Konfiguration

Die folgende Konfiguration ermöglicht den Fernzugriff von allen Quellen. Daher solltest du sicherstellen, dass du unseren Sicherheitshinweis dazu, der oben verfügbar ist, gelesen und verstanden hast.

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

Wenn du nicht von allen Quellen Zugriff benötigst, aber zum Beispiel nur von deinem Netzwerk, dann ist es viel besser, so etwas wie `192.168.0.*` anstelle von `*` zu verwenden. Passe die Netzwerkadresse entsprechend an, wenn du eine andere verwendest.
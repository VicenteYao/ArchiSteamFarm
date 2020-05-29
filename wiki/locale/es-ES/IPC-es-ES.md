# IPC

ASF incluye su propia interfaz IPC que puede utilizarse para mayor interacción con el proceso. IPC significa **[comunicación entre procesos](https://es.wikipedia.org/wiki/Comunicaci%C3%B3n_entre_procesos)** (por sus siglas en inglés) y en la definición más simple esto es "interfaz web de ASF" basada en **[Kestrel HTTP server](https://github.com/aspnet/KestrelHttpServer)** que puede utilizarse para mayor integración con el proceso, tanto como un "frontend" para el usuario final (ASF-ui), como un "backend" para integración de aplicaciones de terceros.

El IPC puede ser utilizado para muchas cosas diferentes, dependiendo de tus necesidades y habilidades. Por ejemplo, puedes usarlo para obtener el estatus de ASF y de todos los bots, enviar comandos de ASF, obtener y editar configuraciones globales/de bot, añadir nuevos bots, eliminar bots existentes, enviar claves para **[BGR](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer)** o acceder al archivo de registro de ASF. Todas esas acciones están expuestas por nuestra API, lo que significa que puedes codificar tus propias herramientas y scripts que sean capaces de comunicarse con ASF e influenciarlo durante el tiempo de ejecución. Además, algunas acciones selectas (como enviar comandos) también son implementadas por nuestro ASF-ui que te permite acceder a ellas fácilmente a través de una interfaz web amigable.

* * *

# Utilización

Puedes habilitar nuestra interfaz IPC habilitando la **[propiedad de configuración global](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es#configuraci%C3%B3n-global)** `IPC`. ASF declarará la ejecución de IPC en su registro, lo que puedes usar para verificar que la interfaz IPC inició correctamente:

    INFO|ASF|Start() Iniciando servidor IPC...
    INFO|ASF|Start() ¡Servidor IPC listo!
    

El servidor http de ASF ahora está escuchando en endpoints seleccionados. Si no proporcionaste un archivo de configuración personalizada para IPC, estos serán **[127.0.0.1](http://127.0.0.1:1242)** basado en IPv4 y el basado en IPv6 **[[::1]](http://[::1]:1242)** en el puerto por defecto `1242`. Puedes acceder a nuestra interfaz IPC mediante los enlaces anteriores, desde la misma máquina en que se ejecuta el proceso ASF.

La interfaz IPC de ASF expone tres formas diferentes de acceder a ella, dependiendo del uso previsto.

En el nivel inferior está **[ASF API](#asf-api)** que es el núcleo de nuestra interfaz IPC y permite que todo lo demás opere. Esto es lo que quieres usar en tus herramientas, utilidades y proyectos para comunicarte directamente con ASF.

En el terreno medio está nuestra **[documentación Swagger](#swagger-documentation)** que actúa como un frontend para la ASF API. Contiene documentación completa de la ASF API y también te permite acceder a ella más fácilmente. Esto es lo que quieres revisar si planeas escribir una herramienta, utilidad u otro proyecto que deba comunicarse con ASF a través de su API.

En el nivel más alto está **[ASF-ui](#asf-ui)** que se basa en nuestra ASF API y proporciona una forma amigable de ejecutar varias acciones en ASF. Esta es una interfaz IPC predeterminada diseñada para los usuarios finales, y es un perfecto ejemplo de lo que puedes construir con la API de ASF. Si quieres, puedes usar tu UI web personalizada para utilizar con ASF, especificando el **[argumento de línea de comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-es-es#argumentos)** `--path` y usando el directorio personalizado `www` ubicado allí.

* * *

# ASF-ui

ASF-ui es un proyecto comunitario que pretende crear una interfaz gráfica amigable para los usuarios finales. Para lograrlo, actúa como un frontend de nuestra **[ASF API](#asf-api)**, permitiéndote realizar varias acciones con facilidad. Esta es la interfaz por defecto con la que viene ASF.

Como se dijo antes, ASF-ui es un proyecto comunitario que no es mantenido por los desarrolladores de ASF. Sigue su propio curso en **[ASF-ui repo](https://github.com/JustArchiNET/ASF-ui)** el cual debe ser usado para todas las preguntas relacionadas, problemas, reporte de bugs y sugerencias.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

* * *

# ASF API

Nuestra ASF API es una típica API web **[RESTful](https://es.wikipedia.org/wiki/Transferencia_de_Estado_Representacional)** que se basa en JSON para su formato de datos principal. Hacemos todo lo posible para describir la respuesta con precisión, usando tanto códigos de estatus HTTP (donde sea apropiado), así como una respuesta que tú mismo puedas analizar para saber si la solicitud tuvo éxito, y si no, por qué.

Nuestra ASF API puede ser accedida enviando las solicitudes correctas a los endpoints `/Api` apropiados. Puedes usar esos endpoints API para hacer tus propios scripts de ayuda, herramientas, interfaces gráficas de usuario y así por el estilo. Esto es exactamente lo que logra nuestra ASF-ui bajo el capó, y cualquier otra herramienta puede lograr lo mismo. ASF API es soportada oficialmente y mantenida por el equipo de ASF.

Para una documentación completa de endpoints disponibles, descripciones, solicitudes, respuestas, códigos de estatus http y todo lo demás considerando ASF API, por favor, consulta nuestra **[documentación swagger](#documentación-swagger)**.

![ASF API](https://i.imgur.com/yggjf5v.png)

* * *

## Autenticación

La interfaz IPC de ASF por defecto no requiere ningún tipo de autenticación, ya que `IPCPassword` está configurado en `null`. Sin embargo, si `IPCPassword` está habilitado al tener cualquier valor no vacío, cada llamada a la API de ASF requiere la contraseña que coincida con la establecida en `IPCPassword`. Si omites la autenticación o introduces una contraseña incorrecta, obtendrás el error `401 - Unauthorized`. Si continúas enviando solicitudes sin autenticación, serás temporalmente bloqueado con el error `403 - Forbidden`.

La autenticación puede hacerse a través de dos formas separadas.

### `Authentication` header

En general debes usar cabeceras de solicitud HTTP, estableciendo el campo `Authentication` con tu contraseña como valor. La forma de hacerlo depende de la herramienta que estés usando para acceder a la interfaz IPC de ASF, por ejemplo, si usas `curl` entonces debes añadir `-H 'Authentication: MyPassword'` como parámetro. De esta manera la autenticación es pasada en las cabeceras de la solicitud, donde de hecho debería tener lugar.

### Parámetro `password` en una cadena de consulta

Alternativamente puedes añadir el parámetro `password` al final de la URL que estás a punto de llamar, por ejemplo, llamando `/Api/ASF?password=MyPassword` en lugar de solamente `/Api/ASF`. Este enfoque es suficiente, pero obviamente expone la contraseña, lo que no siempre es apropiado. Además de eso está el argumento adicional en la cadena de consulta, lo que complica el aspecto de la URL y hace que se sienta como si fuera URL específica, mientras que la contraseña se aplica a toda la comunicación de la API de ASF.

* * *

Ambas formas son soportadas y depende totalmente de ti cuál quieres elegir. Recomendamos usar cabeceras HTTP siempre que sea posible, ya que es más apropiado que la cadena de consulta. Sin embargo, también soportamos cadena de consulta, principalmente por las varias limitaciones relacionadas con las solicitudes de cabeceras. Un buen ejemplo incluye la falta de cabeceras personalizadas al iniciamos una conexión websocket en javascript (aunque sea completamente válida de acuerdo al RFC). En esta situación una cadena de consulta es la única forma de autenticar.

* * *

## Documentación Swagger

Nuestra interfaz IPC, además a la ASF AIP y ASF-ui también incluye documentación de swagger, la cual está disponible en la **[URL](http://localhost:1242/swagger)** `/swagger`. La documentación de Swagger sirve como intermediario entre nuestra implementación API y otras herramientas que la utilicen (por ejemplo ASF-ui). Proporciona una documentación completa y disponibilidad de todos los endpoints API en especificación **[OpenAPI](https://swagger.io/resources/open-api)** que puede ser fácilmente integrada por otros proyectos, permitiéndote escribir y probar ASF API con facilidad.

Además de usar nuestra documentación swagger como una especificación completa de API API, también puedes usarla como una forma amigable de ejecutar varios endpoints API, principalmente aquellos que no están implementados por ASF-ui. Dado que nuestra documentación swagger es generada automáticamente a partir del código de ASF, tienes la garantía de que la documentación siempre estará actualizada con los endpoints API que incluye tu versión de ASF.

![Documentación Swagger](https://i.imgur.com/mLpd5e4.png)

* * *

# Preguntas frecuentes

### ¿Es seguro usar la interfaz IPC de ASF?

ASF por defecto solo escucha en direcciones `localhost`, lo que significa que acceder al IPC de ASF desde otra máquina que no sea la tuya **es imposible**. A menos que modifiques los endpoints predeterminados, el atacante necesitaría acceso directo a tu máquina para acceder a la IPC de ASF, por lo tanto es tan seguro como puede ser y no hay posibilidad de que alguien más acceda a ella, incluso desde tu propia LAN.

Sin embargo, si decides cambiar las direcciones `localhost` a otra cosa, entonces **tú mismo**debes establecer reglas adecuadas de cortafuegos para permitir que solo las IP autorizadas accedan a la interfaz IPC de ASF. Además de eso, recomendamos altamente establecer `IPCPassword`, que añadirá otra capa de seguridad extra. En este caso también es posible que quieras ejecutar la interfaz IPC de ASF tras un proxy inverso, lo que se explica más abajo.

### ¿Puede acceder a la API de ASF a través de mis propias herramientas o scripts de usuario?

Sí, para eso fue diseñada la API de ASF y puedes usar cualquier cosa capaz de enviar solicitudes HTTP para acceder. Los scripts de usuario locales siguen la lógica **[CORS](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing)**, y permitimos acceso de todos los orígenes para ellos (`*`), siempre y cuando `IPCPassword` esté configurado, como una medida de seguridad adicional. Esto te permite ejecutar varias solicitudes ASF API autenticadas, sin permitir que scripts maliciosos lo hagan automáticamente (ya que necesitarían saber tu `IPCPassword` para lograrlo).

### ¿Puedo acceder remotamente a la IPC de ASF, por ejemplo, desde otra máquina?

Si, recomendamos usar un proxy inverso para eso (explicado a abajo). De esta manera puedes acceder a tu servidor web de forma usual, que luego accederá a la IPC de ASF en la misma máquina. Alternativamente, si no quieres correrlo con un proxy inverso, puedes usar una **[configuración personalizada](#configuración-personalizada)** con la URL apropiada para ello. Por ejemplo, si tu máquina está en una VPN privada con la dirección `10.8.0.1`, puedes establecer la URL `http://10.8.0.1:1242` en configuración IPC, lo que permitiría el acceso IPC desde tu VPN privada, pero no desde cualquier otro lugar.

### ¿Puedo usar la IPC de ASF tras un proxy inverso como Apache o Nginx?

**Sí**, nuestra IPC es totalmente compatible con tal configuración, por lo que eres libre de alojarlo frente a tus herramientas para seguridad adicional y compatibilidad, si es lo que quieres. En general, el servidor Kestrel http de ASF es muy seguro y no hay riesgo cuando se conecta directamente a Internet, pero ponerlo tras un proxy inverso como Apache o Nginx podría proporcionar funcionalidad adicional que no sería podría lograr de otra forma, como asegurar la interfaz de ASF con **[autenticación básica](https://es.wikipedia.org/wiki/Autenticación_de_acceso_básica)**.

A continuación puedes encontrar un ejemplo de configuración Nginx. Incluimos el bloque `server` completo, aunque te interesan principalmente los `location`. Por favor, consulta la **[documentación nginx](https://nginx.org/en/docs)** si necesitas más detalles.

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

A continuación puedes encontrar un ejemplo de configuración Apache. Por favor, consulta la **[documentación apache](https://httpd.apache.org/docs)** si necesitas más detalles.

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

### ¿Puedo acceder a la interfaz IPC a través del protocolo HTTPS?

**Sí**, puedes conseguirlo a través de dos formas diferentes. Una forma recomendada sería usar un proxy inverso (descrito arriba) donde puedas acceder a tu servidor a través de https como de costumbre, y conectarte a través de él con la interfaz IPC de ASF en la misma máquina. De esta manera tu tráfico está totalmente cifrado y no necesitas modificar IPC de ninguna manera para soportar dicha configuración.

La segunda forma incluye especificar una **[configuración personalizada](#configuración-personalizada)** para la interfaz IPC de ASF donde puedas habilitar un endpoint https y proporcionar un certificado apropiado directamente a nuestro servidor Kestrel http. Esta forma se recomienda si no estás ejecutando ningún otro servidor web y no quieres ejecutar uno más exclusivamente para ASF. De otro modo, es mucho más fácil lograr una configuración satisfactoria usando un mecanismo de proxy inverso.

* * *

## Configuración personalizada

Nuestra interfaz IPC soporta archivo de configuración adicional, `IPC.config` que debe ser puesto en el directorio `config` de ASF.

Cuando está disponible, este archivo especifica la configuración avanzada del servidor Kestrel http de ASF, junto con otros ajustes relacionados con IPC. A menos que tengas una necesidad particular, no hay razón para que uses este archivo, dado que ASF ya está usando valores por defecto en este caso.

El archivo de configuración se basa en la siguiente estructura JSON:

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

Hay dos propiedades que vale la pena explicar/editar, estas son `Endpoints` y `PathBase`.

`Endpoints` - Esta es una colección de endpoints, con cada endpoint teniendo nombre único (como `ejemplo-http4`) y la propiedad `Url` que especifique la dirección de escucha `Protocol://Host:Port`. Por defecto, ASF escucha en la direcciones http IPv4 y IPv6, pero hemos añadido ejemplos https para que uses, si es necesario. Solo debes declara aquellos endpoints que necesitas, hemos incluido 4 ejemplos arriba para que puedas editarlos más fácilmente.

`Host` acepta una variedad de valores, incluyendo el valor `*` que enlaza el servidor http de ASF a todas las interfaces disponibles. Ten extremo cuidado cuando uses valores `Host` que permitan el acceso remoto. Si lo haces se habilitará el acceso a la interfaz IPC de ASF desde otras máquinas, lo que puede suponer un riesgo de seguridad. Recomendamos altamente usar `IPCPassword` (y de preferencia también tu propio cortafuegos) **como mínimo** en este caso.

`PathBase` - Esta es la ruta base que será utilizada por la interfaz IPC. Esta propiedad es opcional, por defecto en `/` y no debería ser necesario modificarla para la mayoría de los casos. Al cambiar esta propiedad alojarás toda la interfaz IPC en un prefijo personalizado, por ejemplo, `http://localhost:1242/MyPrefix` en lugar de solo `http://localhost:1242`. Usar una `PathBase` puede que se quiera en combinación con la configuración específica de un proxy inverso donde quieres hacer proxy a una URL específica, por ejemplo, `midominio.com/ASF` en lugar de todo el dominio `midominio.com`. Normalmente eso requeriría que escribas una regla para el servidor de tu web que mapee `mydomain.com/ASF/Api/X` -> `localhost:1242/Api/X`, pero en en su lugar puedes definir un `PathBase` personalizado de `/ASF` y lograr más fácilmente la configuración de `mydomain.com/ASF/Api/X` -> `localhost:1242/ASF/Api/X`.

A menos que realmente necesites especificar una ruta base personalizada, es mejor dejarlo en sus valores por defecto.

### Ejemplo de configuración

La siguiente configuración permitirá el acceso remoto desde todas las fuentes, por lo tanto debes asegurarte de leer y entender nuestro aviso de seguridad al respecto, disponible arriba.

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

Si no requieres acceso desde todas las fuentes, pero, por ejemplo, solo de tu LAN, entonces es mucho mejor idea usar algo como `192.168.0.*` en lugar de `*`. Adapta la dirección de red apropiadamente si utilizas una diferente.
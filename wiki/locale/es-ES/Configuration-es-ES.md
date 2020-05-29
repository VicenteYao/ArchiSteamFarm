# Configuración

Esta página está dedicada a la configuración de ASF. Sirve como documentación completa del directorio `config`, permitiéndote ajustar ASF a tus necesidades.

- **[Introducción](#introducción)**
- **[ConfigGenerator basado en la Web](#configgenerator-basado-en-la-web)**
- **[Configuración manual](#configuración-manual)**
- **[Configuración global](#configuración-global)**
- **[Configuración de bot](#configuración-de-bot)**
- **[Estructura de archivos](#estructura-de-archivos)**
- **[Mapeo JSON](#mapeo-json)**
- **[Compatibilidad de mapeo](#compatibilidad-de-mapeo)**
- **[Compatibilidad de configuraciones](#compatibilidad-de-configuraciones)**
- **[Autorrecarga](#autorrecarga)**

* * *

## Introducción

La configuración de ASF está dividida en dos partes principales - configuración global (proceso), y la configuración de cada bot. Cada bot tiene su propio archivo de configuración nombrado `NombreDeBot.json` (donde `NombreDeBot` es el nombre del bot), mientras que la configuración global de ASF (proceso) es un archivo nombrado `ASF.json`.

Un bot es una cuenta de Steam que forma parte en el proceso de ASF. Para funcionar correctamente, ASF necesita por lo menos **una** instancia de bot definida. No hay un límite impuesto de instancias de bot, así que puedes usar tantos bots (cuentas de Steam) como desees.

ASF usa el formato **[JSON](https://es.wikipedia.org/wiki/JSON)** para almacenar sus archivos de configuración. Es un formato amigable, legible y universal en el cual puedes configurar el programa. Pero no te preocupes, no necesitas saber JSON para poder configurar ASF. Solo lo mencioné en caso de que ya quieras crear en masa configuraciones de ASF con algún tipo de script bash.

La configuración puede ser hecha manualmente - creando configuraciones JSON, o usando nuestro **[ConfigGenerator basado en la web](https://justarchinet.github.io/ASF-WebConfigGenerator)**, lo cual debería ser mucho más fácil y conveniente. A menos que seas un usuario avanzado, sugiero usar el generador de configuraciones, que será descrito a continuación.

* * *

## ConfigGenerator basado en la web

El propósito del **[ConfigGenerator basado en la web](https://justarchinet.github.io/ASF-WebConfigGenerator)** es proporcionar una interfaz amigable usada para generar archivos de configuración de ASF. El ConfigGenerator basado en la web está 100% basado en el cliente, lo que significa que los detalles que introduces no son enviados a ningún lado, sino que solo son procesados localmente. Esto garantiza seguridad y confiabilidad, que incluso puede funcionar **[offline](https://github.com/JustArchiNET/ASF-WebConfigGenerator/tree/master/docs)** si quieres descargar todos los archivos y ejecutar `index.html` en tu navegador favorito.

Está verificado que el ConfigGenerator basado en la web funciona correctamente en Chrome y Firefox, pero también debería funcionar correctamente en todos los navegadores más populares que usen javascript.

El uso es bastante simple - elige si quieres generar una configuración de `ASF` o de `Bot` cambiando a la pestaña adecuada, asegúrate que la versión elegida del archivo de configuración coincida con la de ASF, luego introduce todos los detalles y haz clic en el botón "Download". Mueve este archivo al directorio `config` de ASF, sobreescribiendo los archivos existentes si es necesario. Repite para todas las posteriores modificaciones y dirígete al resto de esta sección para una explicación de todas las opciones disponibles para configurar.

* * *

## Configuración manual

Recomiendo fuertemente usar el ConfigGenerator basado en la web, pero por si alguna razón no quieres, entonces también puedes crear archivos de configuración tú mismo. Revisa los ejemplos de JSON debajo para un buen comienzo en la estructura apropiada, puedes copiar el contenido en un archivo y usarlo como base para tu configuración. Ya que no estás usando nuestra interfaz, asegúrate de que tu configuración es **[válida](https://jsonlint.com)**, ya que ASF se negará a cargarla si no puede ser analizada. Para una apropiada estructura JSON de todos los campos disponibles, dirígete a la sección **[mapeo JSON](#mapeo-json)** y la documentación debajo.

* * *

## Configuración global

La configuración global se encuentra en el archivo `ASF.json` y tiene la siguiente estructura:

```json
{
    "AutoRestart": true,
    "Blacklist": [],
    "CommandPrefix": "!",
    "ConfirmationsLimiterDelay": 10,
    "ConnectionTimeout": 90,
    "CurrentCulture": null,
    "Debug": false,
    "FarmingDelay": 15,
    "GiftsLimiterDelay": 1,
    "Headless": false,
    "IdleFarmingPeriod": 8,
    "InventoryLimiterDelay": 3,
    "IPC": false,
    "IPCPassword": null,
    "LoginLimiterDelay": 10,
    "MaxFarmingTime": 10,
    "MaxTradeHoldDuration": 15,
    "OptimizationMode": 0,
    "Statistics": true,
    "SteamMessagePrefix": "/me ",
    "SteamOwnerID": 0,
    "SteamProtocols": 7,
    "UpdateChannel": 1,
    "UpdatePeriod": 24,
    "WebLimiterDelay": 300,
    "WebProxy": null,
    "WebProxyPassword": null,
    "WebProxyUsername": null
}
```

* * *

Todas las opciones se explican a continuación:

### `AutoRestart`

Tipo `bool` con valor predeterminado de `true`. Esta propiedad define si ASF tiene permitido realizar un autorreinicio cuando sea necesario. Hay algunos eventos que requerirán un autorreinicio de ASF, tal como una actualización de ASF (hecho con `UpdatePeriod` o el comando `update`), así como al editar el archivo de configuración `ASF.json`, con el comando `restart` y así por el estilo. Por lo general, el reinicio incluye dos partes - crear un nuevo proceso, y terminar el actual. La mayoría de los usuarios debería estar bien así y dejar esta propiedad con su valor predeterminado de `true`, sin embargo - si estás ejecutando ASF por medio de tu propio script y/o con `dotnet`, tal vez quieras tener control total sobre el proceso de inicio, y evitar una situación como tener un nuevo (reiniciado) proceso de ASF ejecutándose en silencio en segundo plano, y no en el primer plano del script, que se cerró junto con el proceso antiguo de ASF. Esto es especialmente importante considerando el hecho de que el nuevo proceso ya no será tu "hijo" directo, lo que hará que no puedas, por ejemplo, usar la entrada de consola estándar para ello.

Si ese es el caso, esta propiedad es especialmente para ti y puedes la puedes establecer en `false`. Sin embargo, ten en cuenta que en tal caso **tú** eres responsable de reiniciar el proceso. Esto es importante de alguna manera ya que ASF solo saldrá en lugar de generar un nuevo proceso (por ejemplo, después de actualizar), así que si no hay ninguna lógica introducida por ti, simplemente dejará de funcionar hasta que lo inicies de nuevo. ASF siempre termina con el código de error apropiado indicando éxito (cero) o no éxito (no cero), de esta manera serás capaz de añadir la lógica correcta en tu script que debe evitar que ASF se autorreinicie en caso de fallo, o por lo menos hacer una copia local de `log.txt` para mayor análisis. También ten en cuenta que el comando `restart` siempre reiniciará ASF sin importar cómo esté establecida esta propiedad, puesto que esta propiedad define el comportamiento predeterminado, mientras que el comando `restart` siempre reinicia el proceso. A menos que tengas una razón para deshabilitar esta característica, debes dejarla habilitada.

* * *

### `Blacklist`

Tipo `ImmutableHashSet<uint>` con valor predeterminado estando vacío. Como su nombre lo indica, esta propiedad de configuración global define las appIDs (juegos) que serán completamente ignorados por el proceso automático de "idleo" de ASF. Desafortunadamente a Steam le encanta etiquetar las insignias de las ofertas de verano/invierno como "con cromos obtenibles", lo que confunde al proceso de ASF haciéndole creer que es un juego válido que debería ser "farmeado". Si no hubiera ningún tipo de lista negra, ASF eventualmente se "colgaría" "farmeando" un juego que en realidad no es un juego, y esperaría infinitamente para obtener cromos, lo que no va a suceder. La lista negra de ASF sirve al propósito de marcar esas insignias como no disponibles para "farmear", para que podamos ignorarlas silenciosamente al momento de decidir qué "farmear", y no caer en la trampa.

ASF incluye dos listas negras por defecto - la `GlobalBlacklist`, que está codificada en el código de ASF y no es posible editarla, y la `Blacklist` normal, que se define aquí. La `GlobalBlacklist` se actualiza junto con ASF y generalmente incluye todas las appIDs "malas" al momento de la liberación, así que si usas la versión más reciente de ASF no necesitas mantener tu propia `lista negra` definida aquí. El objetivo principal de esta propiedad es permitirte poner en la lista negra appIDs nuevas, no conocidas al momento de la liberación de ASF, que no deben ser "farmeadas". La `GlobalBlacklist` se actualiza tan rápido como es posible, por lo tanto no es necesario que actualices tu propia `Blacklist` si estás usando la última versión de ASF, pero sin la `Blacklist` te verías obligado a actualizar ASF para que pueda "seguir funcionando" cuando Valve libere una nueva insignia de ofertas - no quiero obligarte a usar el último código de ASF, por lo tanto esta propiedad está aquí para permitirte "arreglar" ASF por ti mismo si por alguna razón no quieres, o no puedes, actualizar la `GlobalBlacklist` a través de la nueva versión de ASF, pero quieres mantener funcionando tu viejo ASF. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

Si en cambio lo que buscas es una lista negra basada en los bots, echa un vistazo a los **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `ib`, `ibadd` e `ibrm`.

* * *

### `CommandPrefix`

Tipo `string` con valor predeterminado de `!`. Esta propiedad especifica el prefijo **sensible a mayúsculas** usado para los **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** de ASF. En otras palabras, esto es lo que necesitas anteponer a cada comando de ASF para poder hacer que ASF te escuche. Es posible establecer este valor a `null` o vacío para hacer que ASF no use un comando de prefijos, en cuyo caso ingresas los comandos solo con sus identificadores. Sin embargo, haciéndolo así se disminuye potencialmente el rendimiento de ASF ya que ASF está optimizado para no analizar los mensajes si no empiezan con `CommandPrefix` - si intencionalmente decides no usarlo, ASF se verá forzado a leer todos los mensajes y responderlos, incluso si no son comandos de ASF. Por lo tanto se recomienda seguir usando algún `CommandPrefix`. tal como `/` si no te gusta el valor predeterminado de `!`. Por consistencia, `CommandPrefix` afecta a todo el proceso de ASF. A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `ConfirmationsLimiterDelay`

Tipo `byte`con valor predeterminado de `10`. ASF se asegurará de que haya al menos `ConfirmationsLimiterDelay` segundos entre dos solicitudes de confirmación de 2FA consecutivas para evitar activar el límite de tarifa - estos son usados por **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)** durante, por ejemplo, el comando `2faok`, así como cuando sea necesario al realizar varias operaciones relacionadas con intercambios. El valor predeterminado se estableció basado en nuestras pruebas y no debe ser reducido si no quieres tener problemas. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `ConnectionTimeout`

Tipo `byte` con valor predeterminado de `90`. Esta propiedad define el tiempo de espera para varias acciones de red realizadas por ASF, en segundos. En particular, `ConnectionTimeout` define el tiempo de espera en segundos para las solicitudes HTTP e IPC, `ConnectionTimeout / 10` define el máximo número de latidos fallidos, mientras que `ConnectionTimeout / 30` define el número de minutos que permitimos para la solicitud inicial de conexión a la red de Steam. El valor predeterminado de `90` debería estar bien para la mayoría de las personas, sin embargo, si tienes una PC o una conexión lenta, tal vez quieras incrementar este número (como a `120`). Ten en cuenta que valores mayores no solucionarán mágicamente unos servidores de Steam lentos o incluso inaccesibles, así que no deberíamos esperar infinitamente por algo que no sucederá y simplemente intentarlo más tarde. Establecer este valor demasiado alto dará lugar a un retraso excesivo en capturar problemas de red, así como una disminución en el rendimiento general. Establecer este valor demasiado bajo disminuirá la estabilidad y rendimiento general, ya que ASF abortará solicitudes válidas mientras todavía están siendo analizadas. Por lo tanto, establecer este parámetro con un valor inferior al predeterminado no tiene ninguna ventaja en general, ya que los servidores de Steam tienden a ser superlentos de vez en cuando, y podría requerir más tiempo para analizar las solicitudes de ASF. El valor predeterminado es un balance entre creer que nuestra conexión a la red es estable, y dudar de la red de Steam para manejar nuestra solicitud en determinado tiempo de espera. Si quieres detectar problemas antes y hacer que ASF se reconecte/responda más rápido, el valor predeterminado debería bastar (o ligeramente menos, como `60`, haciendo a ASF menos paciente). Si notas que ASF está teniendo problemas de red, como solicitudes fallidas, latidos perdidos o conexión a Steam interrumpida, tendría sentido aumentar este valor si estás seguro que **no** son causados por tu red, sino por Steam, ya que incrementar los tiempos de espera hace a ASF más "paciente y decide no reconectarse de inmediato.

Una situación de ejemplo que podría requerir un aumento de esta propiedad es permitir a ASF lidiar con ofertas de intercambio muy grandes que pueden tomar hasta más de dos minutos para ser completamente aceptadas y manejadas por Steam. Al incrementar el tiempo de espera predeterminado, ASF será más paciente y esperará más antes de decidir que el intercambio funcionará y abandonar la solicitud inicial.

Otra situación podría ser causada por una máquina muy lenta o una conexión a internet que requiera más tiempo para manejar la información que se transmite. Esta es una situación bastante rara, ya que la CPU/ancho de banda casi nunca tiene cuello de botella, pero aún así es una posibilidad que vale la pena mencionar.

En breve, el valor predeterminado debería ser adecuado para la mayoría de los casos, pero tal vez quieras aumentarlo si es necesario. Aún así, ponerlo por encima del valor predeterminado no tiene mucho sentido, ya que tiempos de espera mayores no solucionarán mágicamente servidores de Steam inaccesibles. A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `CurrentCulture`

Tipo `string` con valor predeterminado de `null`. Por defecto ASF intenta utilizar el idioma de tu sistema operativo, y preferirá usar las traducciones en ese idioma si están disponibles. Esto es posible gracias a nuestra comunidad que intenta **[localizar](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Localization-es-es)** ASF en casi todos los idiomas populares. Si por alguna razón no quieres usar el idioma nativo de tu SO, puedes usar esta propiedad para elegir cualquier idioma válido que quieras usar en cambio. Para una lista de todas las culturas disponibles, por favor visita **[MSDN](https://msdn.microsoft.com/en-us/library/cc233982.aspx)** y busca `Language tag`. Es bueno notar que ASF acepta tanto culturas específicas, tal como `en-GB`, pero también las neutrales, tal como `en`. Especificar una cultura también afectará otros comportamientos específicos de la cultura, tal como moneda/formato de fecha y así por el estilo. Por favor ten en cuenta que tal vez requieras paquetes adicionales de fuente/idioma para mostrar correctamente caracteres específicos de un idioma, si seleccionaste una cultura no nativa que haga uso de ellos. Generalmente querrás hacer uso de esta propiedad si prefieres ASF en inglés en vez de tu idioma nativo.

* * *

### `Debug`

Tipo `bool` con valor predeterminado de `false`. Esta propiedad define si el proceso debe ejecutarse en modo de depuración. Estando en modo de depuración, ASF crea un directorio especial `debug` junto al `config`, que mantiene un seguimiento de toda la comunicación entre ASF y los servidores de Steam. La información de depuración puede ayudar a detectar errores relacionados con la red y el flujo de trabajo general de ASF. Demás de eso, algunas rutinas del programa serán mucho más detalladas, como `WebBrowser` especificando la razón exacta por la que algunas solicitudes fallan - esas entradas son escritas en el registro normal de ASF. **No debes ejecutar ASF en modo de depuración, a menos que te sea solicitado por el desarrollador**. Ejecutar ASF en modo de depuración **reduce el rendimiento**, **afecta negativamente la estabilidad** y es **mucho más detallado en algunas partes**, así que debe ser usado **solamente** de forma intencional, a corto plazo, para depurar un problema particular, reproducir el problema u obtener más información acerca de una solicitud fallida, y así por el estilo, pero **no** para la ejecución normal del programa. Verás **muchos** errores nuevos, problemas, y excepciones - asegúrate de tener un conocimiento decente sobre ASF, Steam y sus peculiaridades si decides analizar todo eso tú mismo, ya que no todo es relevante.

**ADVERTENCIA:** habilitar este modo incluye el registro de información **potencialmente sensible** tal como los nombres de usuario y contraseñas que utilizas para iniciar sesión en Steam (debido al registro de red). Esa información es escrita tanto en el directorio `debug`, como en el estándar `log.txt` (que ahora es mucho más detallado para registrar esta información). No debes publicar el contenido de depuración generado por ASF en ningún sitio público, el desarrollador de ASF siempre debe recordarte enviarlo a su correo, u otro sitio seguro. No almacenamos, ni hacemos uso de esos detalles sensibles, son escritos como parte de la rutina de depuración ya que su presencia podría ser relevante para el problema que te esté afectando. Preferimos que no alteres el registro de ASF en ningún modo, pero si te preocupes, eres libre de redactar apropiadamente los detalles sensibles.

> Redactar consiste en reemplazar los detalles sensibles, por ejemplo con asteriscos. Debes abstenerte de remover líneas completas, ya que su mera existencia podría ser relevante y deben ser preservadas.

* * *

### `FarmingDelay`

Tipo `byte` con valor predeterminado de `15`. A fin de que ASF funcione, comprobará el juego que actualmente se esté "farmeando" cada `FarmingDelay` minutos, por si acaso ya se han obtenido todos los cromos. Establecer esta propiedad demasiado baja puede resultar en una cantidad excesiva de solicitudes enviadas, mientras que establecerla muy alta puede resultar en que ASF siga "farmeando" determinado juego por hasta `FarmingDelay` minutos después de que haya sido "farmeado" por completo. El valor predeterminado debería ser excelente para la mayoría de los usuarios, pero si tienes muchos bots ejecutándose tal vez debas considerar aumentarlo a algo como `30` minutos para limitar las solicitudes de Steam que son enviadas. Es bueno señalar que ASF usa un mecanismo basado en eventos y comprueba la página de insignia del juego cada vez que se obtiene un artículo de Steam, para que en general **no tengamos que comprobarlo en intervalos de tiempo fijos**, pero ya que no confiamos completamente en la red de Steam - comprobamos la página de insignia del juego de todos modos, por si no lo comprobamos al obtener un cromo en los últimos `FarmingDelay` minutos (en caso de que la red de Steam no nos haya informado acerca del artículo obtenido o algo por el estilo). Suponiendo que la red de Steam está funcionando correctamente, reducir este valor **no mejorará de ningún modo la eficiencia del "farmeo"**, mientras se **incrementa significativamente la carga general de la red** - solo se recomienda (si es necesario) incrementar su valor por defecto de `15` minutos. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `GiftsLimiterDelay`

Tipo `byte` con valor predeterminado de `1`. ASF se asegurará de que haya por lo menos `GiftsLimiterDelay` segundos entre dos solicitudes de manejo de regalo/clave/licencia (activación) para evitar que se active el límite de tarifa. Además, también será usado como limitador global para las solicitudes de listas de juegos, como la enviada por el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `owns`. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `Headless`

Tipo `bool` con valor predeterminado de `false`. Esta propiedad define si el proceso debe ejecutarse en modo "headless". En modo "headless", ASf asume que se está ejecutando en un servidor o en otro ambiente no interactivo, por lo tanto no intentará leer ninguna información que se ingrese mediante consola. Esto incluye detalles en demanda (credenciales de cuenta como el código 2FA, el código de SteamGuard, contraseña o cualquier otra variable requerida por ASF para funcionar) así como todas las demás entradas mediante consola (tal como la consola interactiva de comandos). En otras palabras, el modo `Headless` es equivalente a hacer a ASF consola de solo lectura. Esta configuración es útil principalmente para usuarios que ejecutan ASF en sus servidores, ya que en cambio de solicitar, por ejemplo, el código 2FA, ASF abortará la operación al detener una cuenta. A menos que estés ejecutando ASF en un servidor, y si previamente confirmaste que ASF es capaz de operar en modo no headless, debes dejar esta propiedad deshabilitada. Cualquier interacción del usuario será rechazada estando en modo headless, y tus cuentas no se ejecutarán si requieren **cualquier** entrada de consola durante el inicio. Esto es útil para servidores, ya que ASF puede abortar al intentar iniciar sesión cuando se le soliciten credenciales, en lugar de esperar (infinitamente) a que el usuario las proporcione. Habilitar este modo también te permitirá usar el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `input` que funciona como un reemplazo para la entrada de consola estándar. Si no estás seguro de cómo establecer esta propiedad, déjala con su valor predeterminado de `false`.

Si estás ejecutando ASF en un servidor, tal vez quieras usar esta opción junto con el **[argumento de línea de comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-es-es)** `--process-required`.

* * *

### `IdleFarmingPeriod`

Tipo `byte` con valor predeterminado de `8`. Cuando ASF no tiene nada para "farmear", periódicamente comprobará cada `IdleFarmingPeriod` horas si la cuenta tiene nuevos juegos para "farmear". Esta característica no es necesaria cuando hablamos de nuevos juegos que obtenemos, ya que ASF es lo suficientemente inteligente para comprobar las páginas de insignias en este caso. `IdleFarmingPeriod` es principalmente para situaciones como cuando se le añaden cromos a juegos viejos que ya tenemos. En este caso no hay ningún evento, por lo que ASF tiene que comprobar periódicamente las páginas de insignias si queremos tener esto cubierto. El valor `0` desactiva esta función. También revisa: `ShutdownOnFarmingFinished`.

* * *

### `InventoryLimiterDelay`

Tipo `byte` con valor predeterminado de `3`. ASF se asegurará de que haya por lo menos `InventoryLimiterDelay` segundos entre dos solicitudes consecutivas de inventario para evitar que se active el límite de tarifa - estos son usados para obtener el inventario de Steam, especialmente durante tus propios comandos tal como `transfer`, y también en funciones como `MatchActively`. El valor predeterminado de `3` fue establecido basado en obtener el inventario de más de 100 instancias de bot consecutivas, y debería satisfacer a la mayoría (si no a todos) los usuarios. Sin embargo tal vez quieras reducirlo, o incluso cambiarlo a `0` si tienes una poca cantidad de bots, así ASF ignorará el retraso y "looteará" los inventarios de Steam mucho más rápido. Se advierte, sin embargo, que establecerlo muy bajo **resultará** en que Steam prohíba temporalmente tu IP, y eso impedirá obtener tu inventario por completo. También podría ser necesario que incrementes este valor si estás ejecutando muchos bots con muchas solicitudes de inventario, aunque en este caso tal vez debas tratar de limitar el límite de esas solicitudes. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `IPC`

Tipo `bool` con valor predeterminado de `false`. Esta propiedad define si el servidor **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-es-es)** de ASF debe empezar junto con el proceso. El IPC permite la comunicación entre procesos al iniciar un servidor HTTP local. Si no vas a hacer uso del servidor IPC de ASF, entonces no hay razón para que habilites esta opción.

* * *

### `IPCPassword`

Tipo `string` con valor predeterminado de `null`. Esta propiedad define la contraseña obligatoria para cada llamada API hecha vía IPC y sirve como una medida de seguridad adicional. Cuando se establece un valor no vacío, todas las solicitudes IPC requerirán adicionalmente la propiedad `password` establecida a la contraseña especificada aquí. El valor predeterminado de `null` omitirá la necesidad de una contraseña, haciendo que ASF respete todas las consultas. Además de eso, activar esta opción también habilita un mecanismo integrado anti fuerza bruta que restringirá temporalmente determinada `Dirección IP` al enviar muchas solicitudes no autorizadas en un corto período de tiempo. A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `LoginLimiterDelay`

Tipo `byte`con valor predeterminado de `10`. ASF se asegurará de que haya por lo menos `LoginLimiterDelay` segundos entre dos intentos de conexión consecutivos para evitar que se active el límite de tarifa. El valor predeterminado de `10` fue establecido basado en conectar más de 100 instancias de bot, y debería satisfacer a la mayoría (si no a todos) de usuarios. Sin embargo, tal vez o quieras aumentar/disminuir, o incluso cambiarlo a `0` si tienes una poca cantidad de bots, así ASF ignorará el retraso y se conectará a Steam mucho más rápido. Se advierte, sin embargo, que establecerla muy bajo teniendo demasiados bots **resultará** en que Steam restrinja temporalmente tu IP, y eso impedirá que inicies sesión **por completo**, con el error `InvalidPassword/RateLimitExceeded` - y eso también incluye tu cliente de Steam, no solamente ASF. Igualmente, si estás ejecutando un número excesivo de bots, especialmente junto con otros clientes/herramientas de Steam usando la misma dirección IP, probablemente necesites aumentar este valor para distribuir los inicios de sesión entre períodos de tiempo más largos.

Como nota, esto valor también es usado como un búfer balance de carga in todas las acciones de programadas de ASF, tal como intercambios en `SendTradePeriod`. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `MaxFarmingTime`

Tipo `byte`con valor predeterminado de `10`. Como debes saber, Steam no siempre funciona correctamente, algunas veces pueden suceder situaciones extrañas como que Steam no registre tu tiempo de juego, a pesar de en efecto jugar un juego. ASF permitirá "farmear" un juego en modo individual por un máximo de `MaxFarmingTime` horas, y lo considerará como completamente "farmeado" después de ese período. Esto es necesario para no congelar el proceso de "farmeo" in caso de que suceda alguna situación extraña, pero también si por alguna razón Steam liberara una nueva insignia que impediría que ASF siga funcionando (véase: `Blacklist`). El valor predeterminado de `10` horas debería ser suficiente para obtener todos los cromos de un juego. Establecer esta propiedad muy bajo puede resultar en que juegos válidos sean omitidos (y sí, hay juegos válidos que pueden tomar hasta 9 horas para "farmear"), mientras que establecerlo muy alto puede resultar en que se congele el proceso de "farmeo". Ten en cuenta que esta propiedad solo afecta a un juego en una sesión de "farmeo" (por lo que después de completar toda a lista ASF regresará a ese título), además no está basado en el tiempo de juego total sino en el tiempo interno de ASF, por lo que ASF también regresará a ese título después de un reinicio. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `MaxTradeHoldDuration`

Tipo `byte` con valor predeterminado de `15`. Esta propiedad define la duración máxima en días de la retención de intercambio que estés dispuesto a aceptar - ASF rechazará intercambios que sean retenidos por más de `MaxTradeHoldDuration` días, como se explica en la sección **[intercambios](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading-es-es)**. Esta opción tiene sentido solo para bots con la propiedad `TradingPreferences` establecida en `SteamTradeMatcher`, ya que no afecta los intercambios de `Master`/`SteamOwnerID`, ni las donaciones. Las retenciones de intercambio son molestas para todos, y realmente nadie quiere lidiar con ellas. Se supone que ASF funcione con reglas liberales y ayude a todos, sin importar si tiene retención de intercambio o no - es por eso que esta opción está establecida en `15` por defecto. Sin embargo, si en cambio prefieres rechazar todos los intercambios afectados por retenciones de intercambio, puedes especificar `0` aquí. Por favor, ten en cuenta el hecho de que los cromos con cortos períodos de vida no son afectados por esta opción y son rechazados automáticamente para las personas con retención de intercambio, como se describe en la sección **[intercambios](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading-es-es)**, así que no hay necesidad de rechazar globalmente a todos solo por eso. A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `OptimizationMode`

Tipo `byte` con valor predeterminado de `0`. Esta propiedad define el modo de optimización que ASF preferirá durante el tiempo que esté siendo ejecutado. Actualmente ASF soporta dos modos - `0` que se llama `MaxPerformance` y `1` que se llama `MinMemoryUsage`. Por defecto ASF prefiere ejecutar tantas cosas en paralelo (concurrentemente) como sea posible, lo que mejora el rendimiento al distribuir la carga de trabajo entre todos los núcleos de la CPU, múltiples hilos de CPU, múltiples sockets y múltiples tareas de subprocesos. Por ejemplo, ASF solicitará tu primera página de insignias al comprobar si hay juegos para "idlear", y una vez que llegue la solicitud, ASF leerá cuántas páginas de insignias tienes realmente, y entonces solicitará cada una de forma concurrente. Esto es lo que deberías querer **casi siempre**, ya que la carga de trabajo en la mayoría de los casos es mínima y los beneficios del código asincrónico de ASF puede ser notado incluso en el hardware más viejo con un solo núcleo de CPU y poder altamente limitado. Sin embargo, con muchas tareas siendo procesadas en paralelo, el runtime de ASF es responsable de su mantenimiento, por ejemplo, mantener sockets abiertos, hilos en ejecución y tareas siendo procesadas, lo que puede resultar en un uso incrementado de memoria de vez en cuando, y si estás muy limitado por la memoria disponible, tal vez quieras cambiar esta propiedad a `1` (`MinMemoryUsage`) para forzar a ASF a usar tan pocas tareas como sea posible, y ejecutar código asincrónico cercano a lo paralelo de forma sincrónica. Debes considerar cambiar esta propiedad solo si previamente hasta leído la **[configuración de bajo uso de memoria](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-es-es)** e intencionalmente quieres sacrificar una gran mejora de rendimiento, por una pequeña disminución de uso de memoria. Esta opción suele ser **mucho peor** que lo que puedes lograr con otras posibilidades, tal como limitar tu uso de ASF o ajustar el recolector de basura de runtime, como s explica en **[configuración de bajo uso de memoria](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-es-es)**. Por lo tanto, debes usar `MinMemoryUsage` como **último recurso**, justo antes de la recompilación de runtime, si no pudiste lograr resultados satisfactorios con otras (mucho mejores) opciones. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `Statistics`

Tipo `bool` con valor predeterminado de `true`. Esta propiedad define si ASF debe tener las estadísticas habilitadas. Una explicación detallada de lo que esta opción hace exactamente está disponible en la sección **[estadísticas](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-es-es)**. A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `SteamMessagePrefix`

Tipo `string` con valor predeterminado de `"/me "`. Esta propiedad define un prefijo que se antepondrá a todos los mensajes de Steam que envíe ASF. Por defecto ASF utiliza el prefijo `"/me "` para distinguir más fácilmente los mensajes de bots al mostrarlos con un color diferente en el chat de Steam. Otra cosa digna de mención es que el prefijo `"/pre "` logra resultados similares, pero usa un formato diferente. También puedes establecer esta propiedad en vacío o `null` para desactivar por completo el uso de prefijos y mostrar todos los mensajes de ASF de forma tradicional. Es bueno notar que esta propiedad solo afecta a los mensajes de Steam - las respuestas a través de otros canales (como IPC) no son afectadas. A menos que quieras personalizar el comportamiento estándar de ASF, es buena idea dejarlo en su valor predeterminado.

* * *

### `SteamOwnerID`

Tipo `ulong` con valor predeterminado de `0`. Esta propiedad define el Steam ID del propietario del proceso ASF en forma de 64 bits, y es muy similar al permiso `Master` de alguna instancia de bot (pero de manera global). Casi siempre querrás establecer esta propiedad al ID de tu cuenta de Steam principal. Los permisos `Master` incluyen control total sobre su instancia de bot, pero los comandos globales como `exit`, `restart` o `update` están reservados solamente para `SteamOwnerID`. Esto es conveniente, ya que puedes querer ejecutar bots para tus amigos, sin permitirles controlar el proceso de ASF, tal como salir a través del comando `exit`. El valor predeterminado de `0` especifica el proceso de ASF no tiene propietario, lo que se signifca que nadie podrá enviar comandos globales de ASF. Te en cuenta que los comandos **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-es-es)** funcionan con `SteamOwnerID`, así que si quieres usarlos, debes proporcionar un valor válido aquí.

* * *

### `SteamProtocols`

Tipo `byte flags` con valor predeterminado de `7`. Esta propiedad define los protocolos de Steam que ASF usará cuando se conecte a los servidores de Steam, los cuales se definen a continuación:

| Valor | Nombre    | Descripción                                                                                                       |
| ----- | --------- | ----------------------------------------------------------------------------------------------------------------- |
| 0     | Ninguno   | Sin protocolo                                                                                                     |
| 1     | TCP       | **[Protocolo de Control de Transmisión](https://es.wikipedia.org/wiki/Protocolo_de_control_de_transmisi%C3%B3n)** |
| 2     | UDP       | **[Protocolo de Datagramas de Usuario](https://es.wikipedia.org/wiki/Protocolo_de_datagramas_de_usuario)**        |
| 4     | WebSocket | **[WebSocket](https://es.wikipedia.org/wiki/WebSocket)**                                                          |

Por favor, ten en cuenta que esta propiedad es de campo `flags`, por lo tanto es posible elegir cualquier combinación de valores disponibles. Revisa **[mapeo de banderas](#mapeo-json)** si quieres aprender más. No habilitar ninguna bandera es equivalente a la opción `None`, y esa opción es inválida por sí misma.

Por defecto ASF utilizará todos los protocolos de Steam disponibles como una medida para evitar caídas y otros problemas similares de Steam. Normalmente querrás cambiar esta propiedad si quieres limitar a que ASF solamente use uno o dos protocolos específicos en vez de todos los disponibles. Tal medida podría ser necesaria, por ejemplo, si habilitas solamente el tráfico TCP en tu firewall y no quieres que ASF intente conectarse a través de UDP. Sin embargo, a menos que estés depurando un problema o error en particular, casi siempre querrás asegurarse que ASF sea libre de usar cualquier protocolo que sea soportado en el momento y no solo uno o dos. A menos que tengas una **buena** razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `UpdateChannel`

Tipo `byte` con valor predeterminado de `1`. Esta propiedad define el canal de actualización que será usado, ya sea para autoactualizaciones (si `UpdatePeriod` es mayor a `0`), o notificaciones de actualización. Actualmente ASF soporta tres canales de actualización - `0` llamado `None`, `1`, llamado `Stable`, y `2`, que se llama `Experimental`. El canal `Stable` es el canal de actualización predeterminado, que debe ser utilizado por la mayoría de los usuarios. El canal `Experimental` además de las versiones `Stable`, también incluye **prelanzamientos** dedicado a usuarios avanzados y otros desarrolladores para probar nuevas características, confirmar correcciones o proporcionar comentarios sobre mejoras planificadas. **Las versiones experimentales a menudo contienen bugs sin corregir, funciones aún en desarrollo o implementaciones reescritas**. Si no te consideras un usuario avanzado, por favor mantén el canal de actualización predeterminado `1` (Stable). El canal `Experimental` está dedicado a usuarios que saben cómo reportar bugs, lidiar con problemas y dar retroalimentación - no se dará soporte técnico. Echa un vistazo al **[ciclo de lanzamiento](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle-es-es)** de ASF si quieres saber más. También puedes establecer `UpdateChannel` a `0` (`None`), si quieres remover por completo la comprobación de versiones. Establecer `UpdateChannel` a `0` deshabilitará toda funcionalidad relacionada con actualizaciones, incluyendo el comando `update`. Usar el canal `None` **no se recomienda** debido a que te expones a toda clase de problemas (mencionado en `UpdatePeriod` a continuación).

**A menos que sepas lo que haces**, recomendamos **fuertemente** mantener el valor predeterminado.

* * *

### `UpdatePeriod`

Tipo `byte` con valor predeterminado de `24`. Esta propiedad define con qué frecuencia ASF debe comprobar las actualizaciones automáticas. Las actualizaciones son importantes no solo para recibir nuevas funciones y parches de seguridad críticos, sino también para recibir correcciones de bugs, mejoras de rendimiento, mejoras de estabilidad y más. Cuando se establece un valor mayor a `0`, ASF automáticamente descargará, reemplazará, y reiniciará por sí mismo (si `AutoRestart` lo permite) cuando una nueva actualización esté disponible. Para lograr esto, ASF comprobará cada `UpdatePeriod` horas si hay una nueva actualización de en nuestro repo de GitHub. Un valor de `0` desactiva las actualizaciones automáticas, pero todavía permite ejecutar manualmente el comando `update`. También podría interesarte establecer un apropiado `UpdateChannel` que `UpdatePeriod` debe seguir.

El proceso de actualización de ASF implica actualizar toda la estructura de carpetas que ASF usa, pero sin tocar tus configuraciones o bases de datos ubicadas en el directorio `config` - esto significa que cualquier archivo adicional no relacionado con ASF ubicado en su directorio puede perderse durante la actualización. El valor predeterminado de `24` tiene un buen balance entre comprobaciones innecesarias, y un ASF suficientemente al día.

A menos que tengas una **buena** razón para desactivar esta función, deberías dejar las actualizaciones automáticas habilitadas con un `UpdatePeriod` razonable **para tu propio bien**. Esto no es solo porque únicamente ofrecemos soporte a la última versión estable de ASF, sino también porque **damos nuestra garantía de seguridad solo para la última versión**. Si estás usando una versión obsoleta de ASF, te estás exponiendo a todo tipo de problemas, desde pequeños bugs, pasando por funcionalidad incierta, terminando con **[suspensión permanente de la cuenta de Steam](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-es-es#alguien-ha-sido-baneado-por-esto)**, así que **recomendamos fuertemente**, por tu propio bien, que siempre te asegures que tu versión de ASF está actualizada. Las actualizaciones automáticas nos permiten reaccionar rápidamente a todo tipo de problemas deshabilitando o aplicando parches a código problemático antes de que empeore - si optas por no usarlas, pierdes todas nuestras garantías de seguridad y te arriesgas a consecuencias desde ejecutar código que podría ser potencialmente peligroso, no solo para la red de Steam, sino también (por definición) para tu cuenta de Steam.

* * *

### `WebLimiterDelay`

Tipo `ushort` con valor predeterminado de `300`. Esta propiedad define, en milisegundos, la cantidad mínima de retraso entre dos solicitudes consecutivas al mismo servicio web de Steam. Dicho retraso es requerido ya que el servicio **[AkamaiGhost](https://www.akamai.com)** que Steam usa internamente incluye límite de cuota basado en el número global de solicitudes enviadas en un período de tiempo dado. En circunstancias normales el bloqueo de akamai es bastante difícil de lograr, pero bajo cargas muy pesadas con muchas solicitudes, es posible activarlo si seguimos enviando demasiadas solicitudes en un período de tiempo muy corto.

El valor predeterminado fue establecido en el supuesto de que ASF es la única herramienta accediendo a los servicios web de Steam, en particular `steamcommunity.com`, `api.steampowered.com` y `store.steampowered.com`. Si usas otras herramientas que envían solicitudes a los mismos servicios web, entonces debes asegurarte que tu herramienta incluya una función similar a `WebLimiterDelay` y establecer ambas al doble del valor por defecto, que sería `600`. Esto garantiza que bajo ninguna circunstancia excederás más de 1 solicitud por cada `300` ms.

En general, reducir `WebLimiterDelay` por debajo del valor predeterminado se **desaprueba fuertemente** ya que podría derivar en varios bloqueos relacionados con la IP, algunos de los cuales es posible que sean permanentes. El valor predeterminado es suficiente para ejecutar una sola instancia de ASF en el servidor, así como para usar ASF en un escenario normal junto con el cliente de Steam original. Debería ser suficiente para la mayoría de los usos, y solo debes aumentarlo (nunca reducirlo), si - además de usar ASF, también estás usando otra herramienta que pueda enviar un número excesivo de solicitudes al mismo servicio web del que ASF esté haciendo uso. En resumen, el número global de todas las solicitudes enviadas desde una misma IP a un mismo dominio de Steam nunca debe exceder más de una solicitud cada `300` ms.

A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `WebProxy`

Tipo `string` con valor predeterminado de `null`. Esta propiedad define una dirección proxy web que se usará para todas las solicitudes http y https internas enviadas por el `HttpClient` de Steam, especialmente a servicios tales como `github.com`, `steamcommunity.com` y `store.steampowered.com`. Pasar las solicitudes de ASF por un proxy en general no tiene ventajas, pero es excepcionalmente útil para eludir todo tipo de cortafuegos, especialmente el gran cortafuegos en China.

Esta propiedad está definida como una cadena uri:

> Una cadena URI se compone de un esquema (http o https), un host, y un puerto opcional. Un ejemplo de una cadena uri completa es `"http://contoso.com:8080"`.

Si tu proxy requiere autenticación de usuario, también necesitarás configurar `WebProxyUsername` y/o `WebProxyPassword`. Si no hay tal necesidad, configurar solo esta propiedad es suficiente.

Ahora mismo ASF usa un proxy web solo para solicitudes `http` y `https`, las cuales **no** incluyen la comunicación interna de la red de Steam hecha en el cliente de Steam integrado a ASF. Actualmente no hay planes para soportar eso, principalmente debido a la falta de la funcionalidad **[SK2](https://github.com/SteamRE/SteamKit/issues/587#issuecomment-413271550)**. Si necesitas/quieres que ocurra, te sugeriría empezar desde ahí.

A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `WebProxyPassword`

Tipo `string` con valor predeterminado de `null`. Esta propiedad define el campo de contraseña usado en la autenticación básica de digest, NTLM, y Kerberos que es soportada por una máquina `WebProxy` destino que proporciona funcionalidad de proxy. Si tu proxy no requiere credenciales de usuario, no hay necesidad de introducir nada aquí. Usar esta opción solo tiene sentido si también se usa `WebProxy`, ya que de lo contrario no tiene ningún efecto.

A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

### `WebProxyUsername`

Tipo `string` con valor predeterminado de `null`. Esta propiedad define el campo nombre de usuario usado en la autenticación básica de digest, NTLM, y Kerberos que es soportada por una máquina `WebProxy` destino que proporciona funcionalidad de proxy. Si tu proxy no requiere credenciales de usuario, no hay necesidad de introducir nada aquí. Usar esta opción solo tiene sentido si también se usa `WebProxy`, ya que de lo contrario no tiene ningún efecto.

A menos que tengas una razón para editar esta propiedad, deberías dejarla en su valor predeterminado.

* * *

## Configuración de bot

Como ya deberías saber, cada bot debe tener su propia configuración basada en la estructura JSON de ejemplo a continuación. Empieza por decidir cómo quieres nombrar tu bot (por ejemplo, `1.json`, `principal.json`, `primario.json` or `CualquierOtraCosa.json`) y continúa a la configuración.

**Aviso:** Ningún bot puede ser nombrado `ASF` (ya que esa palabra está reservada para la configuración global), ASF también ignorará todos los archivos de configuración que inicien con un punto.

La configuración del bot tiene la siguiente estructura:

```json
{
    "AcceptGifts": false,
    "AutoSteamSaleEvent": false,
    "BotBehaviour": 0,
    "CustomGamePlayedWhileFarming": null,
    "CustomGamePlayedWhileIdle": null,
    "Enabled": false,
    "FarmingOrders": [],
    "GamesPlayedWhileIdle": [],
    "HoursUntilCardDrops": 3,
    "IdlePriorityQueueOnly": false,
    "IdleRefundableGames": true,
    "LootableTypes": [1, 3, 5],
    "MatchableTypes": [5],
    "OnlineStatus": 1,
    "PasswordFormat": 0,
    "Paused": false,
    "RedeemingPreferences": 0,
    "SendOnFarmingFinished": false,
    "SendTradePeriod": 0,
    "ShutdownOnFarmingFinished": false,
    "SteamLogin": null,
    "SteamMasterClanID": 0,
    "SteamParentalCode": null,
    "SteamPassword": null,
    "SteamTradeToken": null,
    "SteamUserPermissions": {},
    "TradingPreferences": 0,
    "TransferableTypes": [1, 3, 5],
    "UseLoginKeys": true
}
```

* * *

Todas las opciones se explican a continuación:

### `AcceptGifts`

Tipo `bool` con valor predeterminado de `false`. Cuando está activado, ASF automáticamente aceptará y activará todos los regalos de Steam (incluyendo tarjetas de regalo) enviados al bot. Esto también incluye los regalos enviados por usuarios además de los definidos en `SteamUserPermissions`. Ten en cuenta que los regalos enviados a direcciones de correo electrónico no son enviadas directamente al cliente, por lo que ASF no aceptará esas sin tu ayuda.

Esta opción se recomienda solo para cuentas alternas, ya que es muy probable que no quieras activar automáticamente todos los regalos enviados a tu cuenta principal. Si no estás seguro si quieres esta función activada o no, mantén el valor predeterminado de `false`.

* * *

### `AutoSteamSaleEvent`

Tipo `bool` con valor predeterminado de `false`. Durante las ofertas de verano/invierno de Steam es común que se proporcionen cromos extra por explorar la lista de descubrimiento cada día, así como por otras actividades específicas de los eventos. Cuando esta opción está activada, ASF automáticamente comprobará la lista de descubrimiento de Steam cada `8` horas (empezando en una hora desde el inicio del programa), y la completará si es necesario. No se recomendada esta opción si quieres realizar esa acción tú mismo, y normalmente solo tiene sentido en cuentas bot. Además, debes asegurarte que tu cuenta sea al menos nivel `8` si esperas recibir esos cromos en primer lugar, lo que es directamente un requisito de Steam. Si no estás seguro si quieres esta función activada o no, mantén el valor predeterminado de `false`.

Por favor, ten en cuenta que debido a los constantes problemas y cambios de Valve, **no garantizamos que esta función trabaje correctamente**, por lo tanto, es totalmente posible que esta opción **no funcione en absoluto**. No aceptamos **ningún** reporte de bugs, ni solicitudes de soporte para esta opción. Se ofrece sin ninguna garantía, la usas bajo tu propio riesgo.

* * *

### `BotBehaviour`

Tipo `byte flags` con valor predeterminado de `0`. Esta propiedad define el comportamiento tipo bot de ASF durante diversos eventos, como se define a continuación:

| Valor | Nombre                        | Descripción                                                                                                               |
| ----- | ----------------------------- | ------------------------------------------------------------------------------------------------------------------------- |
| 0     | None                          | Sin comportamiento especial del bot, es el modo menos invasivo, por defecto                                               |
| 1     | RejectInvalidFriendInvites    | Causará que ASF rechace (en lugar de ignorar) las solicitudes de amistad inválidas                                        |
| 2     | RejectInvalidTrades           | Causará que ASF rechace (en lugar de ignorar) las ofertas de intercambio inválidas                                        |
| 4     | RejectInvalidGroupInvites     | Causará que ASF rechace (en lugar de ignorar) las invitaciones de grupo inválidas                                         |
| 8     | DismissInventoryNotifications | Causará que ASF descarte automáticamente todas las notificaciones de inventario                                           |
| 16    | MarkReceivedMessagesAsRead    | Causará que ASF automáticamente marque como leídos todos los mensajes recibidos                                           |
| 32    | MarkBotMessagesAsRead         | Causará que ASF automáticamente marque como leídos los mensajes de otros bots de ASF (ejecutándose en la misma instancia) |

Por favor, ten en cuenta que esta propiedad es de campo `flags`, por lo tanto es posible elegir cualquier combinación de valores disponibles. Revisa **[mapeo de banderas](#mapeo-json)** si quieres aprender más. No habilitar ninguna bandera es equivalente a la opción `None`.

En general, querrás modificar esta propiedad si esperas que ASF tenga cierta cantidad de automatización relacionada con su actividad, como se espera de una cuenta bot, pero no de una cuenta principal usada en ASF. Por lo tanto, cambiar esta propiedad tiene sentido principalmente para cuentas alternas, aunque también eres libre de usarla para cuentas principales.

El comportamiento normal (`None`) de ASF es para automatizar solamente cosas que el usuario quiera (por ejemplo: recolección de cromos u ofertas de `SteamTradeMatcher`, si se establece en `TradingPreferences`). Este es el modo menos invasivo, y es beneficioso para la mayoría de los usuarios ya que conservas el control total de tu cuenta y puedes decidir tú mismo si permitir ciertas interacciones fuera del alcance, o no.

Una solicitud de amistad inválida es una invitación que no proviene de un usuario con permiso `FamilySharing` (definido en `SteamUserPermissions`) o superior. En modo normal ASF ignora esas invitaciones, como se esperaría, dándote la libertad de aceptarlas, o no. `RejectInvalidFriendInvites` causará que esas invitaciones sean rechazadas automáticamente, lo que prácticamente desactivará la opción para otras personas de añadirte a su lista de amigos (ya que ASF rechazará todas esas solicitudes, fuera de las personas definidas en `SteamUserPermissions`). A menos que quieras rechazar todas las solicitudes de amistad, no deberías habilitar esta opción.

Una oferta de intercambio inválida es una oferta que no es aceptada a través del módulo incorporado de ASF. Más sobre este tema puede encontrarse en la sección **[intercambios](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading)** en la que explícitamente se describe qué tipos de intercambios ASF está dispuesto a aceptar automáticamente. Los intercambios válidos también son definidos por otros ajustes, especialmente `TradingPreferences`. `RejectInvalidTrades` causará que todas las ofertas de intercambio inválidas sean rechazadas, en lugar de ser ignoradas. A menos que quieras rechazar todas las ofertas de intercambio que no son aceptadas automáticamente por ASF, no deberías habilitar esta opción.

Una invitación a grupo inválida es un invitación que no proviene del grupo `SteamMasterClanID`. En modo normal ASF ignora esas invitaciones de grupo, como se esperaría, permitiéndote decidir si quieres unirte a un grupo de Steam o no. `RejectInvalidGroupInvites` causará que todas esas invitaciones de grupo sean rechazadas automáticamente, haciendo efectivamente imposible invitarte a cualquier otro grupo que no sea `SteamMasterClanID`. A menos que quieras rechazar todas las invitaciones de grupo, no deberías habilitar esta opción.

`DismissInventoryNotifications` es extremadamente útil cuando empiezan a ser molestas las notificaciones de que has recibido nuevos artículos. ASF no puede deshacerse de la notificación en sí, ya que eso está integrado en tu cliente de Steam, pero es capaz de descartar la notificación automáticamente después de recibirla, lo que ya no dejará la notificación de "nuevos artículos en el inventario". Si quieres revisar tú mismo todos los artículos recibidos (especialmente cromos recolectados con ASF), naturalmente no deberías habilitar esta opción. Cuando empieces a volverte loco, recuerda que se ofrece esta opción.

`MarkReceivedMessagesAsRead` automáticamente marcará como leídos **todos** los mensajes recibidos por la cuenta en la que se esté ejecutando ASF, tanto privados como grupales. Normalmente esto debería usarse solo en las cuentas alternas para descartar las notificaciones de "nuevo mensaje" que lleguen, por ejemplo, de ti mismo al ejecutar comandos de ASF. No recomendamos esta opción para cuentas principales, a menos que quieras deshacerte de cualquier notificación de nuevos mensajes, **incluyendo** aquellas que lleguen cuando estés desconectado, asumiendo que ASF fue dejado abierto descartándolas.

`MarkBotMessagesAsRead` funciona de forma similar marcando como leídos solo los mensajes de bots. Sin embargo, ten en cuenta que al usar esta opción en chats grupales con tus bots y otras personas, la implementación de Steam de reconocer un mensaje de chat **también** reconoce todos los mensajes que llegaron **antes** del que decidiste marcar, si no quieres perder mensajes no relacionados que hayan llegado entre tanto, normalmente querrías evitar usar esta función. Obviamente, también es riesgoso cuando tienes varias cuentas principales (por ejemplo, de diferentes usuarios) ejecutándose en la misma instancia de ASF, ya que también puedes perder sus mensajes normales que no sean de ASF.

Si no estás seguro de cómo configurar esta opción, es mejor dejarla en su valor por defecto.

* * *

### `CustomGamePlayedWhileFarming`

Tipo `string` con valor predeterminado de `null`. Cuando ASF está recolectando, puede mostrarse como "Jugando un juego no-Steam: `CustomGamePlayedWhileFarming`" en vez del juego siendo recolectado actualmente. Esto puede ser útil si quieres dejarle saber a tus amigos que estás recolectando, pero ni quieres usar el `OnlineStatus` de `Desconectado`. Por favor, ten en cuenta que ASF no puede garantizar el orden de visualización real de la red de Steam, por lo tanto esta solo es una sugerencia que podría, o no, mostrarse correctamente. Valor predeterminado de `null` desactiva esta función.

* * *

### `CustomGamePlayedWhileIdle`

Tipo `string` con valor predeterminado de `null`. Similar a `CustomGamePlayedWhileFarming`, pero para la situación cuand ASF no tiene nada que hacer (como cuando la cuenta está completamente recolectada). Valor predeterminado de `null` desactiva esta función.

* * *

### `Enabled`

Tipo `bool` con valor predeterminado de `false`. Esta propiedad define si un bot está habilitado. Una instancia de bot habilitada (`true`) iniciará automáticamente al ejecutar ASF, mientras que una instancia de bot deshabilitada (`false`) tendrá que iniciarse manualmente. Por defecto todos los bots están deshabilitados, así que probablemente quieras cambiar esta propiedad a `true` para todos los bots que deban iniciarse automáticamente.

* * *

### `FarmingOrders`

Tipo `ImmutableHashSet<byte>` con valor predeterminado estando vacío. Esta propiedad define el orden de recolección **preferido** usado por ASF para una cuenta bot dada. Actualmente están disponibles los siguientes órdenes de recolección:

| Valor | Nombre                    | Descripción                                                                                            |
| ----- | ------------------------- | ------------------------------------------------------------------------------------------------------ |
| 0     | Unordered                 | Sin ordenar, mejora ligeramente el rendimiento de la CPU                                               |
| 1     | AppIDsAscending           | Intenta recolectar primero los juegos con el `appID` más bajo                                          |
| 2     | AppIDsDescending          | Intenta recolectar primero los juegos con el `appID` más alto                                          |
| 3     | CardDropsAscending        | Intenta recolectar primero los juegos con el menor número restante de cromos obtenibles                |
| 4     | CardDropsDescending       | Intenta recolectar primero los juegos con el mayor número restante de cromos obtenibles                |
| 5     | HoursAscending            | Intenta recolectar primero los juegos con la menor cantidad de horas jugadas                           |
| 6     | HoursDescending           | Intenta recolectar primero los juegos con la mayor cantidad de horas jugadas                           |
| 7     | NamesAscending            | Intenta recolectar los juegos en orden alfabético, empezando por la A                                  |
| 8     | NamesDescending           | Intenta recolectar los juegos en orden alfabético inverso, empezando por la Z                          |
| 9     | Random                    | Intenta recolectar los juegos en orden totalmente aleatorio (diferente en cada ejecución del programa) |
| 10    | BadgeLevelsAscending      | Intenta recolectar primero los juegos con el nivel de insignia más bajo                                |
| 11    | BadgeLevelsDescending     | Intenta recolectar primero los juegos con el nivel de insignia más alto                                |
| 12    | RedeemDateTimesAscending  | Intenta recolectar primero los juegos más antiguos en nuestra cuenta                                   |
| 13    | RedeemDateTimesDescending | Intenta recolectar primero los juegos más nuevos en nuestra cuenta                                     |
| 14    | MarketableAscending       | Intenta recolectar primero los juegos con cromos no comerciables                                       |
| 15    | MarketableDescending      | Intenta recolectar primero los juegos con cromos comerciables                                          |

Dado que esta propiedad es una matriz, permite usar diferentes configuraciones en el orden de tu elección. Por ejemplo, puedes incluir valores de `15`, `11` y `7` para ordenar primero los juegos con cromos comerciables, luego por los que tengan el nivel de insignia más bajo, y finalmente en orden alfabético. Como puedes adivinar, el orden sí importa, ya que al revés (`7`, `11` y `15`) se consigue algo completamente diferente. La mayoría de las personas probablemente usará un orden de entre todos, pero en caso de que quieras, también puedes ordenar más con parámetros adicionales.

También nota la palabra "intenta" en todas las descripciones anteriores - el orden real de ASF se ve fuertemente afectado por el **[algoritmo de recolección de cromos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-es-es)** seleccionado y la ordenación solo afectará a los resultados que ASF considere con el mismo valor de rendimiento. Por ejemplo, en el algoritmo `Simple`, el `FarmingOrders` seleccionado debería ser respetado completamente en la sesión de recolección actual (ya que cada juego tiene el mismo valor de rendimiento), mientras que en el algoritmo `Complex` el orden real es afectado primero por las horas, y luego ordenados de acuerdo al `FarmingOrders` seleccionado. Esto conducirá a diferentes resultados, ya que los juegos con tiempo de juego tendrán prioridad sobre otros, de forma efectiva ASF primero preferirá los juegos que ya hayan pasado las horas requeridas en `HoursUntilCardDrops`, y solo entonces ordenará esos juegos de acuerdo a tu `FarmingOrders` elegido. Del mismo modo, una vez que ASF haya terminado con los juegos priorizados, ordenará los restantes primero por horas (ya que eso reducirá el tiempo requerido para que los títulos restantes alcancen `HoursUntilCardDrops`). Por lo tanto, esta propiedad de configuración solo es una **sugerencia** que ASF intentará respetar, siempre y cuando no afecte negativamente el rendimiento (en este caso, ASF siempre preferirá el rendimiento de la recolección sobre `FarmingOrders`).

También hay una cola de prioridad de recolección accesible a través de los **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `iq`. Si se usa, el orden real de recolección se clasifica primero por rendimiento, luego por cola de recolección, y finalmente por tu `FarmingOrders`.

* * *

### `GamesPlayedWhileIdle`

Tipo `ImmutableHashSet<uint>` con valor predeterminado estando vacío. Si ASF no tiene para recolectar, en su lugar puede jugar tus juegos especificados (`appIDs`). Jugar juegos de este modo incrementa tus "horas jugadas" para esos juegos, pero nada más aparte de eso. Para que esta característica funcione correctamente, tu cuenta de Steam **debe** poseer una licencia válida para todas las `appIDs` que especifiques aquí, esto también incluye juegos F2P. Esta característica puede ser habilitada al mismo tiempo con `CustomGamePlayedWhileIdle` para jugar tus juegos seleccionados mientras se muestra un estado personalizado en la red de Steam, pero en este caso, como en el de `CustomGamePlayedWhileFarming`, el orden de visualización real no está garantizado. Por favor, ten en cuenta que Steam solo le permite a ASF jugar hasta un total de `32` `appIDs`, por lo tanto solo puedes poner esa cantidad de juegos en esta propiedad.

* * *

### `HoursUntilCardDrops`

Tipo `byte` con valor predeterminado de `3`. Esta propiedad define si una cuenta tiene cromos obtenibles restringidos, y si es así, por cuántas horas iniciales. Los cromos obtenibles restringidos significa que una cuenta no recibirá ningún cromo de un juego dado hasta que el juego haya sido jugado por al menos `HoursUntilCardDrops` horas. Desafortunadamente no hay una forma mágica de saber eso, por lo que ASF confía en ti. Esta propiedad afecta el **[algoritmo de recolección de cromos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance)** que será usado. Establecer esta propiedad adecuadamente maximizará los beneficios y minimizará el tiempo requerido para que los cromos sean recolectados. Recuerda que no hay una forma obvio de responder si debes usar uno u otro valor, ya que depende completamente de tu cuenta. Parece ser que las cuentas más antiguas que nunca solicitaron un reembolso tienen cromos obtenibles sin restricción, así que esas deberían usar un valor de `0`, mientras que las cuentas nuevas y aquellas que solicitaron reembolso tienen los cromos obtenibles restringidos con un valor de `3`. Sin embargo, esto es solo teoría, y no debe ser tomado como regla. El valor predeterminado de esta propiedad se estableció basado en "el mal menor" y la mayoría de los casos de uso.

* * *

### `IdlePriorityQueueOnly`

Tipo `bool` con valor predeterminado de `false`. Esta propiedad define si ASF debe considerar para recolección solamente las apps que tú mismo hayas añadido a la cola de prioridad de recolección, disponible mediante los **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** `iq`. Cuando esta opción está activada, ASF omitirá todas las `appIDs` que no estén en la lista, permitiéndote seleccionar los juegos para que ASF recolecte automáticamente. Ten en cuenta que si no añadiste ningún juego a la cola, entonces ASF actuará como si no hubiera nada que recolectar en tu cuenta. Si no estás seguro si quieres esta función activada o no, mantén el valor predeterminado de `false`.

* * *

### `IdleRefundableGames`

Tipo `bool` con valor predeterminado de `true`. Esta propiedad define si ASF tiene permitido recolectar juegos que todavía son reembolsables. Un juego reembolsable es aquel que compraste en las últimas dos semanas a través de la Tienda de Steam y que aún no has jugado por más de 2 horas, como se indica en la página de **[reembolsos de Steam](https://store.steampowered.com/steam_refunds)**. Por defecto, cuando esta opción se establece en `true`, ASF ignora completamente la política de reembolsos de Steam y recolecta todo, como la mayoría espera. Sin embargo, puedes cambiar esta opción a `false` si quieres asegurarte que ASF no recolectará muy pronto ninguno de tus juegos reembolsables, permitiéndote evaluar esos juegos por ti mismo y reembolsarlos si es necesario, sin preocuparte por que ASF afecte negativamente el tiempo de juego. Por favor, ten en cuenta que si deshabilitas esta opción, los juegos comprados en la Tienda de Steam no serán recolectados por ASF hasta por 14 días desde la fecha de activación, lo cual se mostrará como que hay nada para recolectar en tu cuenta si no posees otros juegos. Si no estás seguro si quieres esta función activada o no, deja el valor predeterminado de `true`.

* * *

### `LootableTypes`

Tipo `ImmutableHashSet<byte>` con valor predeterminado de `1, 3, 5` tipos de artículo de Steam. Esta propiedad define el comportamiento de ASF cuando lootea - tanto manual, usando un **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)**, así como automáticamente, a través de una o más propiedades de configuración. ASF se asegurará que solos los artículos de `LootableTypes` sea incluidos en una oferta de intercambio, por lo tanto, esta propiedad te permite elegir lo que quieres recibir en una oferta de intercambio que te sea enviada.

| Valor | Nombre                | Descripción                                                                         |
| ----- | --------------------- | ----------------------------------------------------------------------------------- |
| 0     | Unknown               | Cualquier tipo que no pertenezca a ninguno de los siguientes                        |
| 1     | BoosterPack           | Pack de refuerzo que contiene 3 cromos aleatorios de un juego                       |
| 2     | Emoticon              | Emoticón para usar en el Chat de Steam                                              |
| 3     | FoilTradingCard       | Variante reflectante de `TradingCard`                                               |
| 4     | ProfileBackground     | Fondo de perfil para usar en tu perfil de Steam                                     |
| 5     | TradingCard           | Cromo de Steam, usado para fabricar insignias (no reflectante)                      |
| 6     | SteamGems             | Gemas de Steam usadas para fabricar packs de refuerzo, incluidos los sacos de gemas |
| 7     | SaleItem              | Artículos especiales otorgados durante las ofertas de Steam                         |
| 8     | Consumable            | Artículos consumibles especiales que desaparecen después de ser usados              |
| 9     | ProfileModifier       | Artículos especiales que pueden modificar la apariencia del perfil de Steam         |
| 10    | Sticker               | Artículos especiales que se pueden usar en el chat de Steam                         |
| 11    | ChatEffect            | Artículos especiales que se pueden usar en el chat de Steam                         |
| 12    | MiniProfileBackground | Fondo especial para el perfil de Steam                                              |

Por favor, ten en cuenta que, independientemente de los ajustes anteriores, ASF solo solicitará artículos de la comunidad (`contextID` of 6) de Steam (`appID` of 753), por lo que todos los artículos de juegos, regalos y demás, están excluidos de la oferta por definición.

La configuración por defecto de ASF está basada en el uso más común de un bot, solo "looteando" packs de refuerzo, y cromos (incluyendo los reflectantes). Esta propiedad te permite alterar ese comportamiento de cualquier modo que gustes. Por favor, ten en cuenta que todos los tipos no definidos arriba se mostrarán como tipo `Unknown`, lo que es especialmente importante cuando Valve lanza un nuevo artículo de Steam, el cual también será marcado por ASF como `Unknown`, hasta que sea añadido aquí (en futuras versiones). Es por eso que en general no se recomienda incluir el tipo `Unknown` en tu `LootableTypes`, a menos que sepas lo que haces, y entiendes que ASF enviará todo tu inventario en una oferta de intercambio si la Red de Steam se desconfigura de nuevo y marca todos tus artículos como `Unknown`. Mi recomendación es no incluir el tipo `Unknown` en los `LootableTypes`, incluso si esperas "lootear" todo (lo demás).

* * *

### `MatchableTypes`

Tipo `ImmutableHashSet<byte>` con valor predeterminado de tipos de artículo de Steam `5`. Esta propiedad define qué tipos de artículo de Steam están permitidos para emparejamiento cuando la opción `SteamTradeMatcher` está activada en `TradingPreferences`. Los tipos se describen a continuación:

| Valor | Nombre                | Descripción                                                                         |
| ----- | --------------------- | ----------------------------------------------------------------------------------- |
| 0     | Unknown               | Cualquier tipo que no pertenezca a ninguno de los siguientes                        |
| 1     | BoosterPack           | Pack de refuerzo que contiene 3 cromos aleatorios de un juego                       |
| 2     | Emoticon              | Emoticón para usar en el Chat de Steam                                              |
| 3     | FoilTradingCard       | Variante reflectante de `TradingCard`                                               |
| 4     | ProfileBackground     | Fondo de perfil para usar en tu perfil de Steam                                     |
| 5     | TradingCard           | Cromo de Steam, usado para fabricar insignias (no reflectante)                      |
| 6     | SteamGems             | Gemas de Steam usadas para fabricar packs de refuerzo, incluidos los sacos de gemas |
| 7     | SaleItem              | Artículos especiales otorgados durante las ofertas de Steam                         |
| 8     | Consumable            | Artículos consumibles especiales que desaparecen después de ser usados              |
| 9     | ProfileModifier       | Artículos que pueden modificar la apariencia del perfil de Steam                    |
| 10    | Sticker               | Artículos especiales que se pueden usar en el chat de Steam                         |
| 11    | ChatEffect            | Artículos especiales que se pueden usar en el chat de Steam                         |
| 12    | MiniProfileBackground | Fondo especial para el perfil de Steam                                              |

Por supuesto, los tipos que normalmente debes usar para esta propiedad solo incluyen `2`, `3`, `4` y `5`, ya que solo esos tipos son soportados por STM. ASF incluye una lógica adecuada para descubrir la rareza de los artículos, por lo tanto también es seguro para emparejar emoticonos o fondos de perfil, dado que ASF solo considerará como justos aquellos artículos del mismo juego y tipo, que también compartan la misma rareza.

Por favor, ten en cuenta que **ASF no es un bot de intercambio** y **NO le importará el precio del mercado**. Si no consideras que los artículos de la misma rareza y del mismo set son de precio similares, entonces esta opción NO es para ti. Por favor, considera dos veces si entiendes y estás de acuerdo con esta declaración antes de decidir cambiar esta configuración.

A menos que sepas lo que haces, deberías dejarlo con el valor predeterminado de `5`.

* * *

### `OnlineStatus`

Tipo `byte` con valor predeterminado de `1`. Esta propiedad especifica el estado de la comunidad de Steam con que el bot será mostrado después de iniciar sesión en la red de Steam. Actualmente puedes elegir uno de los siguientes estados:

| Valor | Nombre                |
| ----- | --------------------- |
| 0     | Desconectado          |
| 1     | En línea              |
| 2     | Ocupado               |
| 3     | Ausente               |
| 4     | Snooze                |
| 5     | Deseando intercambiar |
| 6     | Deseando jugar        |
| 7     | Invisible             |

El estado `Desconectado` es extremadamente útil para cuentas principales. Como debes saber, recolectar un juego muestra tu estado de Steam como "Jugando XXX", lo cual puede ser engañoso para tus amigos, haciéndoles creer que estás jugando un juego cuando en realidad solo lo estás recolectando. Usar el estado `Desconectado` soluciona ese problema - tu cuenta nunca se mostrará como "Jugando" cuando estés recolectando cromos con ASF. Esto es posible gracias al hecho de que ASF no tiene que iniciar sesión en la Comunidad de Steam para funcionar correctamente, así que estamos de hecho jugando esos juegos, conectados a la red de Steam, pero sin anunciar nuestra presencia en línea. Ten en cuenta que los juegos jugados usando el estado desconectado seguirán contando para tu tiempo de juego, y mostrados en "Actividad reciente" en tu perfil.

Además, está característica también es importante si quieres recibir notificaciones y mensaje no leídos cuando ASF se esté ejecutando, sin mantener abierto el cliente de Steam al mismo tiempo. Esto se debe a que ASF actúa como un cliente de Steam en sí, y ya sea que ASF quiera o no, Steam le envía todos esos mensajes y otros eventos. Esto no es problema si tienes ASF y el cliente de Steam ejecutándose al mismo tiempo, ya que ambos clientes reciben exactamente los mismos eventos. Sin embargo, si solo se está ejecutando ASF, la red de Steam podría marcar ciertos eventos y mensajes como "entregados", a pesar de que el cliente tradicional de Steam no los reciba debido a que no está presente. El estado desconectado también soluciona este problema, ya que ASF nunca es considerado en este caso para ningún evento de la comunidad, así que todos los mensajes no leídos y otros eventos serán marcados apropiadamente como no leídos cuando regreses.

Es importante tener en cuenta que ASF ejecutándose en modo `Desconectado` **no** será capaz de recibir comandos de la forma habitual a través del chat de Steam, puesto que el chat, así como toda presencia en la comunidad, está de hecho, completamente desconectado. Una solución a este problema es usar en su lugar el modo `Invisible`, que funciona de manera similar (sin exponer el estado), pero mantiene la capacidad de recibir y responder a los mensajes (así como la posibilidad de descartar notificaciones y mensajes no leídos como se dijo anteriormente). El modo `Invisible` tiene más sentido en cuentas alternas que no quieres exponer (en cuanto al estado), pero aún poder enviarles comandos.

Sin embargo, hay una trampa con el modo `Invisible` - no va bien con cuentas principales. Esto es porque **cualquier** sesión de Steam que esté actualmente en línea **expone** el estado, incluso si ASF no lo hace. Esto es causado por la actual limitación/bug de la red de Steam que no es posible solucionar desde el lado de ASF, así que si quieres usar el modo `Invisible` además tendrás que asegurarte que **todas** las otras sesiones de la misma cuenta también usen el modo `Invisible`. Este será el caso en cuentas alternas donde ASF es posiblemente la única sesión activa, pero en las cuentas principales casi siempre preferirás mostrarte a tus amigos como `e línea`, ocultando solamente la actividad de ASF, y en este caso el modo `Invisible` será completamente inútil para ti (en su lugar recomendamos usar el modo `Desconectado`). Con suerte esta limitación/bug será resuelta en el futuro por Valve, pero no esperaría que eso suceda pronto...

Si no estás seguro de cómo configurar esta propiedad, se recomienda usar un valor de `0` (`Desconectado`) para cuentas principales, y el predeterminado `1` (`En Línea`) de otro modo.

* * *

### `PasswordFormat`

Tipo `byte` con valor predeterminado de `0`. Esta propiedad define el formato de la propiedad `SteamPassword`, y actualmente soporta - `0` para `PlainText`, `1` para `AES` y `2` para `ProtectedDataForCurrentUser`. Por favor, consulta la sección **[Seguridad](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Security)** si deseas aprender más, ya que necesitarás asegurarte que la propiedad `SteamPassword` incluye una contraseña que coincida con `PasswordFormat`. En otras palabras, cuando cambias `PasswordFormat` tu `SteamPassword` **ya** debería estar en ese formato, no solo apuntando a estarlo. A menos que sepas lo que haces, deberías dejarlo con el valor predeterminado de `0`.

* * *

### `Paused`

Tipo `bool` con valor predeterminado de `false`. Esta propiedad define el estado inicial del módulo `CardsFarmer`. Con un valor predeterminado de `false`, el bot comenzará a recolectar automáticamente cuando se inicie, ya sea a causa de `Enabled` o el comando `start`. Solo debes cambiar esta propiedad a `true` si quieres `resume` manualmente el proceso automático de recolección, por ejemplo, porque quieres usar `play` todo el tiempo y nunca usar el módulo automático de `CardsFarmer` - esto funciona exactamente igual que el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** `pause`. Si no estás seguro si quieres esta función activada o no, mantén el valor predeterminado de `false`.

* * *

### `RedeemingPreferences`

Tipo `byte flags` con valor predeterminado de `0`. Esta propiedad define el como de ASF al activar claves de juego, y se describe a continuación:

| Valor | Nombre                             | Descripción                                                                                                                                                     |
| ----- | ---------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 0     | None                               | Sin preferencias especiales de activación, por defecto                                                                                                          |
| 1     | Forwarding                         | Envía a otros bots las claves no disponibles para activar                                                                                                       |
| 2     | Distributing                       | Distribuye todas las claves entre sí y otros bots                                                                                                               |
| 4     | KeepMissingGames                   | Conserva las claves de (potencialmente) juegos no poseídos cuando se envían, dejándolas sin usar                                                                |
| 8     | AssumeWalletKeyOnBadActivationCode | Asume que las claves con el estado `BadActivationCode` equivalen a `CannotRedeemCodeFromClient`, y por lo tanto intenta activarlas como un código de la cartera |

Por favor, ten en cuenta que esta propiedad es de campo `flags`, por lo tanto es posible elegir cualquier combinación de valores disponibles. Revisa **[mapeo de banderas](#json-mapping)** si quieres aprender más. No habilitar ninguna bandera es equivalente a la opción `None`.

`Forwarding` causará que el bot envíe una clave que no es posible activar, a otro bot conectado que no tenga ese juego en particular (si es posible comprobarlo). La situación más común es enviar un juego `AlreadyPurchased` a otro bot al que le falte ese juego en particular, pero esta opción también cubre otros escenarios, tal como `DoesNotOwnRequiredApp`, `RateLimited` o `RestrictedCountry`.

`Distributing` causará que el bot distribuya todas las claves recibidas entre sí mismo y otros bots. Esto significa que cada bot recibirá una sola clave del lote. Normalmente esto se usa cuando estás activando muchas claves del mismo juego, y quieres distribuirlas equitativamente entre tus bots, contrario a activar claves de diferentes juegos. Esta característica no tiene sentido si solo estás activando una clave en una acción `redeem` (ya que no hay claves extra que distribuir).

`KeepMissingGames` causará que el bot omita `Forwarding` cuando no podemos estar seguros si la clave efectivamente ya la tiene nuestro bot, o no. Básicamente esto significa que `Forwarding` **solo** aplicará a claves `AlreadyPurchased`, en lugar de abarcar otros casos tales como `DoesNotOwnRequiredApp`, `RateLimited` o `RestrictedCountry`. Normalmente querrás usar esta opción en la cuenta principal, para asegurarte que las claves activadas en ella no sean enviadas si tu bot, por ejemplo, tiene temporalmente `RateLimited`. Como puedes adivinar por la descripción, este campo no ningún efecto si `Forwarding` no está habilitado.

`AssumeWalletKeyOnBadActivationCode` causará que las claves con el estado `BadActivationCode` sean tratadas como `CannotRedeemCodeFromClient`, y por lo tanto resultará en que ASF intente activarlas como códigos de la cartera. Esto es necesario porque Steam podría anunciar los códigos de la cartera como `BadActivationCode` (y no `CannotRedeemCodeFromClient` como lo hacía antes), resultando en que ASF nunca las intente activar. Sin embargo, **desaconsejamos** usar esta preferencia, ya que dará como resultado que ASF intente activar como código de la cartera todas las claves inválidas, lo que conducirá a una cantidad excesiva (potencialmente inválidas) de solicitudes enviadas al servicio de Steam, con todas las potenciales consecuencias. En cambio, recomendamos usar el modo de activación `ForceAssumeWalletKey` del comando **[`redeem^`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es#modos-redeem)** al activar a sabiendas códigos de la cartera, lo que habilitará la alternativa necesaria cuando sea requerido.

Habilitar `Forwarding` y `Distributing` pondrá la función de distribuir por encima de la de enviar, lo que hace que ASF intente activar una clave en todos los bots (enviar) antes de pasar a la siguiente (distribuir). Normalmente querrás usar esta opción solo cuando quieras `Forwarding`, pero con el comportamiento modificado de cambiar el bot en que se intenta activar la clave, en lugar de siempre ir en orden con cada clave (lo que sería solamente `Forwarding`). Este comportamiento puede ser beneficioso si sabes que la mayoría o todas tus claves pertenecen al mismo juego, porque en esta situación `Forwarding` intentaría activar todo en un bot (lo que tiene sentido si tus claves son de juegos diferentes), y `Forwarding` + `Distributing` cambiará el bot en la siguiente clave, "distribuyendo" la tarea de activar una nueva clave en otro bot diferente al inicial (lo cual tiene sentido si las claves son para el mismo juego, omitiendo un intento de activación).

El orden real de los bots para todos los escenarios de activación es alfabético, excluyendo bots que no están disponibles (no conectados, detenidos y así por el estilo). Por favor, ten en cuenta que hay un límite por hora de intentos de activación por IP y por cuenta, y cada intento de activación que no termina con `OK` contribuye a los intentos fallidos. ASF hará todo lo posible por minimizar el número de fallos `AlreadyPurchased`, por ejemplo, intentando evitar enviar una clave a otro bot que ya tenga ese juego en particular, pero no siempre se garantiza que funcione debido a cómo Steam está manejando las licencias. Usar banderas de activación como `Forwarding` o `Distributing` siempre aumentará tu probabilidad de alcanzar `RateLimited`.

También ten en cuenta que no puedes enviar o distribuir claves a bots a los que no tienes acceso. Esto debería ser obvio, pero asegúrate que al menos eres `Operator` de todos los bots que quieras incluir en tu proceso de activación, por ejemplo con el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** `status ASF`.

* * *

### `SendOnFarmingFinished`

Tipo `bool` con valor predeterminado de `false`. Cuando ASF termine de recolectar una cuenta dada, pueden enviar automáticamente un intercambio que contenga todo lo recolectado hasta este punto al usuario con permisos `Master`, lo cual es conveniente si no quieres molestarte haciendo los intercambios tú mismo. Esta opción funciona igual que el comando `loot`, por lo tanto, ten en cuenta que requiere establecer un usuario con permiso `Master`, también es posible que necesites un `SteamTradeToken` válido, así como usar una cuenta que sea elegible para intercambios en primer lugar. Además de iniciar `loot` después de terminar la recolección, ASF también iniciará `loot` en cada notificación de nuevos artículos (cuando no está recolectando), y después de completar cada intercambio que resulte en nuevos artículos (siempre) cuando esta opción está activa. Esto es especialmente útil para "enviar" artículos recibidos de otras personas a nuestra cuenta.

Normalmente querrás usar **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-es-es)** junto con esta función, aunque no es un requisito si tienes intención de confirmar manualmente de forma oportuna. Si no estás seguro de cómo establecer esta propiedad, déjala con su valor predeterminado de `false`.

* * *

### `SendTradePeriod`

Tipo `byte` con valor predeterminado de `0`. Esta propiedad funciona muy similar a la propiedad `SendOnFarmingFinished`, con una diferencia - en lugar de enviar un intercambio cuando se termine de recolectar, también podemos enviarlo cada `SendTradePeriod` horas, independientemente de cuánto falte por recolectar. Esto es útil si quieres `"lootear"` tus cuentas alternas de forma habitual en lugar de esperar a que terminen de recolectar. El valor predeterminado de `0` desactiva esta función, si quieres que tu bot te envíe un intercambio, por ejemplo, cada día, debes poner aquí `24`.

Normalmente querrás usar **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-es-es)** junto con esta función, aunque no es un requisito si tienes intención de confirmar manualmente de forma oportuna. Si no estás seguro de cómo establecer esta propiedad, déjala con su valor predeterminado de `0`.

* * *

### `ShutdownOnFarmingFinished`

Tipo `bool` con valor predeterminado de `false`. ASF "ocupa" una cuenta durante todo el tiempo que el proceso está activo. Cuando una cuenta dada termina de recolectar, ASF comprueba periódicamente (cada `IdleFarmingPeriod` horas), si en ese tipo se añadió algún juego nuevo con cromos, así puede continuar recolectando en esa cuenta sin necesidad de reiniciar el proceso. Esto es útil para la mayoría de las personas, ya que ASF puede continuar recolectando automáticamente cuando sea necesario. Sin embargo, puede que quieras detener el proceso cuando una cuenta dada termine de recolectar, puedes lograr eso estableciendo esta propiedad a `true`. Cuando está habilitado, ASF procederá a cerrar sesión cuando la cuenta esté totalmente recolectada, lo que significa que ya no será comprobada periódicamente ni será usada. Debes decidir si prefieres que ASF trabaje en una instancia de bot dada todo el tiempo, o si ASF debe detenerla cuando el proceso de recolección esté terminado. Cuando todas las cuentas se detienen y el proceso no se está ejecutando en el **[modo](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments)** `--process-required`, ASF también se cerrará, haciendo descansar tu máquina y permitiéndote programar otras acciones, tal como suspender o apagarse al momento de obtener el último cromo disponible.

Si no estás seguro de cómo establecer esta propiedad, déjala con su valor predeterminado de `false`.

* * *

### `SteamLogin`

Tipo `string` con valor predeterminado de `null`. Esta propiedad define tu nombre de usuario de Steam - el que usas para iniciar sesión en Steam. Además de definir el nombre de usuario, también puedes mantener el valor predeterminado de `null` si quieres introducir tu nombre de usuario en cada inicio de ASF en vez de ponerlo en la configuración. Esto puede ser útil si no quieres guardar datos sensibles en el archivo de configuración.

* * *

### `SteamMasterClanID`

Tipo `ulong` con valor predeterminado de `0`. Esta propiedad define el steamID del grupo de Steam al que el bot debe unirse automáticamente, incluyendo su chat de grupo. Puedes comprobar el steamID de tu grupo navegando a su **[página](https://steamcommunity.com/groups/archiasf)**, luego añadiendo `/memberslistxml?xml=1` al final del enlace, para que el enlace se vea **[así](https://steamcommunity.com/groups/archiasf/memberslistxml?xml=1)**. Entonces puedes obtener el steamID de tu grupo del resultado, está en la etiqueta `<groupID64>`. En el ejemplo anterior sería `103582791440160998`. Además de intentar unirse a un grupo dado durante el inicio, el bot también aceptará automáticamente invitaciones de este grupo, lo que hace posible que invites a tu bot manualmente si tu grupo tiene membresía privada. Si no tienes un grupo dedicado a tus bots, debes dejar esta propiedad con el valor predeterminado de `0`.

* * *

### `SteamParentalCode`

Tipo `string` con valor predeterminado de `null`. Esta propiedad define el PIN del modo familiar. ASF requiere acceso a recursos protegidos por el modo familiar, por lo tanto si usas esa función, debes proporcionar el PIN del modo familiar, para que pueda funcionar normalmente. El valor por defecto de `null` significa que no se requiere un PIN del modo familiar para desbloquear esta cuenta, y esto es probablemente lo que quieres si no usas la funcionalidad del modo familiar. Además de definir el PIN del modo familiar, también puedes usar el valor de `0` si quieres introducir tu PIN del modo familiar en cada inicio de ASF, cuando sea necesario, en vez de ponerlo en la configuración. Esto puede ser útil si no quieres guardar datos sensibles en el archivo de configuración.

En circunstancias limitadas, ASF también es capaz de generar por sí mismo un PIN de modo familiar válido, aunque eso requiere una gran cantidad de recursos del sistema operativo y tiempo adicional para terminar, sin mencionar que no se garantiza que tenga éxito, por lo tanto recomendamos no confiar en esa función y en su lugar poner `SteamParentalCode` en la configuración para que utilice ASF.

* * *

### `SteamPassword`

Tipo `string` con valor predeterminado de `null`. Esta propiedad define tu contraseña de Steam - la que usas para iniciar sesión en Steam. Además de definir la contraseña de Steam, también puedes mantener el valor predeterminado de `null` si quieres introducir tu contraseña en cada inicio de ASF en vez de ponerlo en la configuración. Esto puede ser útil si no quieres guardar datos sensibles en el archivo de configuración.

* * *

### `SteamTradeToken`

Tipo `string` con valor predeterminado de `null`. Cuando tienes tu bot en tu lista de amigos, entonces el bot puede te puede enviar un intercambio directamente sin preocuparte por el token de intercambio, por lo tanto puedes dejar esta propiedad con el valor predeterminado de `null`. No obstante, si decides NO tener tu bot en tu lista de amigos, entonces tendrás que generar e ingresar un token de intercambio del usuario al que este bot se espera que le envié los intercambios. En otras palabras, esta propiedad debe llenarse con el token de intercambio de la cuenta que está definida con permiso `Master` en `SteamUserPermissions` de **esta** instancia de bot.

Para encontrar tu token, inicia la sesión del usuario con permiso `Master`, dirígete **[aquí](https://steamcommunity.com/my/tradeoffers/privacy)** y echa un vistazo a tu URL de intercambio. El token que estamos buscando está compuesto de 8 caracteres después de la parte `&token=` en tu URL de intercambio. Debes copiar y poner esos 8 caracteres aquí, como `SteamTradeToken`. No incluyas toda la URL de intercambio, tampoco la parte de `&token=`, solo el token en sí (8 caracteres).

* * *

### `SteamUserPermissions`

Tipo `ImmutableDictionary<ulong, byte>` con valor predeterminado estando vacío. Esta propiedad es de tipo diccionario, la cual mapea a un usuario de Steam identificado por su ID de 64 bits, a un número `byte` que especifica sus permisos en la instancia de ASF. Los permisos de bot disponibles actualmente en ASF se definen a continuación:

| Valor | Nombre           | Descripción                                                                                                                                                                                                                                       |
| ----- | ---------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 0     | Ninguno          | Sin permisos especiales, este principalmente es un valor de referencia asignado a las ID de Steam faltantes en este diccionario - no hay necesidad de definir a nadie con este permiso                                                            |
| 1     | PréstamoFamiliar | Proporciona acceso mínimo para usuarios del préstamo familiar. Una vez más, este es principalmente es un valor de referencia ya que ASF es capaz de descubrir automáticamente los ID de Steam a los que tenemos permitido usar nuestra biblioteca |
| 2     | Operador         | Proporciona acceso básico a instancias de bot dadas, principalmente para agregar licencias y activar claves                                                                                                                                       |
| 3     | Master           | Proporciona acceso total a una instancia de bot dada                                                                                                                                                                                              |

En resumen, esta propiedad te permite manejar los permisos para determinados usuarios. Los permisos son importantes principalmente para acceder a los **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** de ASF, pero también para habilitar varias características de ASF, como aceptar intercambios. Por ejemplo, puede que quieras establecer tu propia cuenta como `Master`, y darle acceso de `Operator` a 2-3 de tus amigos para que puedan activar claves para tu bot con ASF, mientras que **no** son elegibles, por ejemplo, para detenerlo. Gracias a eso puedes asignar permisos a ciertos usuarios y dejarlos usar tu bot en un grado especificado por ti.

Recomendamos establecer exactamente un usuario como `Master`, y cualquier cantidad que quieras como `Operators` e inferior. Aunque es técnicamente posible establecer múltiples `Masters` y ASF funcionará correctamente con ellos, por ejemplo, aceptando todos sus intercambios enviados al bot, ASF solo usará uno de ellos (con el ID de Steam más bajo) para cada acción que requiera un solo objetivo, por ejemplo una solicitud de `loot`, y en propiedades tales como `SendOnFarmingFinished` y `SendTradePeriod`. Si entiendes perfectamente esas limitaciones, especialmente el hecho de que la solicitud de `loot` siempre enviará los artículos al `Master` con el ID de Steam más bajo, independientemente del `Master` que haya ejecutado el comando, entonces puedes definir múltiples usuarios con permisos `Master`, pero aún así recomendamos el esquema con un solo master - el esquema con múltiples master es una configuración no recomendada y no se le da soporte.

Es bueno notar que hay un permiso `Owner` adicional, que se declara como la propiedad `SteamOwnerID` en la configuración global. Aquí no puedes asignar el permiso `Owner` a nadie, ya que la propiedad `SteamUserPermissions` solo define permisos relacionados con la instancia de bot, y no con ASF como proceso. Para tareas relacionadas con el bot, `SteamOwnerID` se trata igual que `Master`, así que definir aquí tu `SteamOwnerID` no es necesario.

* * *

### `TradingPreferences`

Tipo `byte flags` con valor predeterminado de `0`. Esta propiedad define el comportamiento de ASF al intercambiar, y se describe a continuación:

| Valor | Nombre              | Descripción                                                                                                                                                                                                                  |
| ----- | ------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 0     | None                | Sin preferencias especiales de intercambio, por defecto                                                                                                                                                                      |
| 1     | AcceptDonations     | Acepta intercambios en los que no estamos perdiendo nada                                                                                                                                                                     |
| 2     | SteamTradeMatcher   | Participa pasivamente en intercambios tipo **[STM](https://www.steamtradematcher.com)**. Visita **[intercambios](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading-es-es#steamtradematcher)** para más información |
| 4     | MatchEverything     | Requiere que `SteamTradeMatcher` esté configurado, y en combinación - también acepta intercambios malos además de los buenos y neutrales                                                                                     |
| 8     | DontAcceptBotTrades | No acepta automáticamente intercambios `loot` de otras instancias de bot                                                                                                                                                     |
| 16    | MatchActively       | Participa activamente en intercambios tipo **[STM](https://www.steamtradematcher.com)**. Visita **[intercambios](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading-es-es#matchactively)** para más información     |

Por favor, ten en cuenta que esta propiedad es de campo `flags`, por lo tanto es posible elegir cualquier combinación de valores disponibles. Revisa **[mapeo de banderas](#json-mapping)** si quieres aprender más. No habilitar ninguna bandera es equivalente a la opción `None`.

Para más información sobre la lógica de intercambios de ASF, y descripción de cada bandera disponible, por favor, visita la sección **[intercambios](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Trading)**.

* * *

### `TransferableTypes`

Tipo `ImmutableHashSet<byte>` con valor predeterminado de `1, 3, 5` tipos de artículo de Steam. Esta propiedad define qué tipos de artículo de Steam serán considerados para transferirse entre bots, durante el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** `transfer`. ASF se asegurará que solo los artículos de `TransferableTypes` sean incluidos en una oferta de intercambio, por lo tanto esta propiedad te permite elegir lo que quieres recibir en una oferta de intercambio enviada a uno de tus bots.

| Valor | Nombre                | Descripción                                                                         |
| ----- | --------------------- | ----------------------------------------------------------------------------------- |
| 0     | Unknown               | Cualquier tipo que no pertenezca a ninguno de los siguientes                        |
| 1     | BoosterPack           | Pack de refuerzo que contiene 3 cromos aleatorios de un juego                       |
| 2     | Emoticon              | Emoticón para usar en el Chat de Steam                                              |
| 3     | FoilTradingCard       | Variante reflectante de `TradingCard`                                               |
| 4     | ProfileBackground     | Fondo de perfil para usar en tu perfil de Steam                                     |
| 5     | TradingCard           | Cromo de Steam, usado para fabricar insignias (no reflectante)                      |
| 6     | SteamGems             | Gemas de Steam usadas para fabricar packs de refuerzo, incluidos los sacos de gemas |
| 7     | SaleItem              | Artículos especiales otorgados durante las ofertas de Steam                         |
| 8     | Consumable            | Artículos consumibles especiales que desaparecen después de ser usados              |
| 9     | ProfileModifier       | Artículos especiales que pueden modificar la apariencia del perfil de Steam         |
| 10    | Sticker               | Artículos especiales que se pueden usar en el chat de Steam                         |
| 11    | ChatEffect            | Artículos especiales que se pueden usar en el chat de Steam                         |
| 12    | MiniProfileBackground | Fondo especial para el perfil de Steam                                              |

Por favor, ten en cuenta que, independientemente de los ajustes anteriores, ASF solo solicitará artículos de la comunidad (`contextID` of 6) de Steam (`appID` of 753), por lo que todos los artículos de juegos, regalos y demás, están excluidos de la oferta por definición.

La configuración por defecto de ASf está basada en el uso más común de un bot, solo transfiriendo packs de refuerzo, y cromos (incluyendo los reflectantes). Esta propiedad te permite alterar ese comportamiento de cualquier modo que gustes. Por favor, ten en cuenta que todos los tipos no definidos arriba se mostrarán como tipo `Unknown`, lo que es especialmente importante cuando Valve lanza un nuevo artículo de Steam, el cual también será marcado por ASF como `Unknown`, hasta que sea añadido aquí (en futuras versiones). Es por eso que en general no se recomienda incluir el tipo `Unknown` en tu `TransferableTypes`, a menos que sepas lo que haces, y entiendes que ASF enviará todo tu inventario en una oferta de intercambio si la Red de Steam se desconfigura de nuevo y marca todos tus artículos como `Unknown`. Mi recomendación es no incluir el tipo `Unknown` en los `TransferableTypes`, incluso si esperas transferir todo.

* * *

### `UseLoginKeys`

Tipo `bool` con valor predeterminado de `true`. Esta propiedad define si ASF debe usar el mecanismo de claves de acceso para esta cuenta de Steam. El mecanismo de claves de acceso funciona muy similar a la opción "recordarme" del cliente oficial de Steam, lo que hace posible que ASF almacene y use una clave de acceso temporal de un solo uso para el siguiente intento de inicio de sesión, omitiendo la necesidad de proporcionar contraseña, código de Steam Guard o 2FA siempre que nuestra clave de acceso sea válida. La clave de acceso se almacena en el archivo `BotName.db` y se actualiza automáticamente. Por eso no necesitas proporcionar contraseña/código Steam Guard/código 2FA después de iniciar sesión con éxito en ASF una vez.

Las claves de acceso se usan por defecto para tu comodidad, para que no tengas que introducir `SteamPassword`, SteamGuard o código 2FA (cuando no se está usando **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)**) en cada inicio de sesión. También es una alternativa superior ya que la clave de acceso solo puede ser utilizada una vez y no revela de ninguna manera tu contraseña original. Exactamente el mismo método es usado por el cliente de Steam, que guarda el nombre de tu cuenta y la clave de acceso para el siguiente intento de inicio de sesión, siendo lo mismo que usar en ASF `SteamLogin` con `UseLoginKeys` y dejar vacío `SteamPassword`.

Sin embargo, algunas personas podrían preocuparse incluso por este pequeño detalle, por lo tanto esta opción está disponible si quieres asegurarte que ASF no almacenará ningún tipo de token que permita reanudar sesiones previas después de cerrarlo, lo que dará como resultado que la autenticación completa sea obligatoria en cada intento de inicio de sesión. Desactivar esta opción funcionará exactamente igual que no marcar "recordarme" en el cliente oficial de Steam. A menos que sepas lo que haces, deberías dejarlo con el valor predeterminado de `true`.

* * *

## Estructura de archivos

ASF usa una está de archivos bastante sencilla.

    ├── config
    │     ├── ASF.json
    │     ├── ASF.db
    │     ├── Bot1.json
    │     ├── Bot1.db
    │     ├── Bot1.bin
    │     ├── Bot2.json
    │     ├── Bot2.db
    │     ├── Bot2.bin
    │     └── ...
    ├── ArchiSteamFarm.dll
    ├── log.txt
    └── ...
    

Para mover ASF a una nueva ubicación, por ejemplo, otro PC, basta con mover/copiar solo el directorio `config`, y esa es la forma recomendada de hacer cualquier tipo de "respaldos de ASF", ya que siempre puedes descargar la parte restante (el programa) del GitHub, sin arriesgarse a corromper archivos internos de ASF, por ejemplo, por un respaldo defectuoso.

El archivo `log.txt` contiene el registro generado por la última ejecución de ASF. Este archivo no contiene ninguna información sensible, y es extremadamente útil cuando se trata de problemas, fallos o simplemente como información de lo que ocurrió en la última ejecución de ASF. A menudo preguntaremos por este archivo si te encuentras con problemas o bugs. ASF gestiona automáticamente este archivo, pero puedes modificar más el módulo de **[registro](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Logging)** si eres un usuario avanzado.

El directorio `config` es el lugar que contiene la configuración de ASF, incluyendo la de todos sus bots.

`ASF.json` es un archivo de configuración global de ASF. Esta configuración se usa para especificar cómo se comporta ASF como proceso, lo que afecta a todos los bots y al propio programa. Aquí puedes encontrar propiedades globales, como el propietario del proceso de ASD, actualizaciones automáticas o depuración.

`BotName.json` es la configuración de una instancia de bot dada. Esta configuración se utiliza para especificar cómo se comporta una instancia de bot dada, por lo tanto esas configuraciones son específicas de ese bot y no se comparte con otros. Esto te permite configurar bots con diferentes ajustes y no necesariamente que todos funcionen exactamente igual. Cada bot es nombrado usando un identificador único, elegido por ti en lugar `BotName`.

Aparte de los archivos de configuración, ASF también usar el directorio `config` para almacenar bases de datos.

`ASF.db` es un archivo de base de datos global de ASF. Actúa como un almacenamiento global persistente y se utiliza para guardar información diversa relacionada con el proceso de ASF, como la IP de los servidores locales de Steam. **No debes editar este archivo**.

`BotName.db` es la base de datos una instancia de bot dada. Este archivo se utiliza para almacenar información crucial sobre una instancia de bot dada en almacenamiento persistente, como claves de acceso o ASF 2FA. **No debes editar este archivo**.

`BotName.bin` es un archivo especial de una instancia de bot dada, que contiene información sobre el sentry hash de Steam. El sentry hash se utiliza para autenticar usando el mecanismo `SteamGuard`, muy similar al archivo `ssfn` de Steam. **No debes editar este archivo**.

`BotName.keys` es un archivo especial que puede ser usado para importar claves al **[activador de juegos en segundo plano](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer)**. No es obligatorio ni generado, pero ASF lo reconoce. Este archivo se elimina automáticamente después de que las claves son importadas correctamente.

`BotName.maFile` es un archivo especial que puede ser usado para importar **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)**. No es obligatorio ni generado, pero ASF lo reconoce si tu `NombreDeBot` todavía no usa ASF 2FA. Este archivo se elimina automáticamente después de que ASF 2FA es importado correctamente.

* * *

## Mapeo JSON

Cada tipo de configuración tiene un tipo. El tipo de la propiedad define los valores que son válidos para esta. Solo puedes usar valores que sean válidos para un tipo dado - si usas un valor inválido, ASF no será capaz de analizar tu configuración.

**Recomendamos fuertemente usar ConfigGenerator para generar las configuraciones** - maneja por ti la mayoría de las cosas de bajo nivel (como validación de los tipos), para que solo tengas que introducir los valores apropiados, y no tengas que entender los tipos de variables especificados a continuación. Esta sección es principalmente para las personas que generan/editan las configuraciones manualmente, para que sepan qué valores pueden usar.

Los tipos usados por ASF son tipos nativos de C#, que se describen a continuación:

* * *

`bool` - Tipo booleano, solo acepta los valores `true` y `false`.

Ejemplo: `"Enabled": true`

* * *

`byte` - Tipo byte sin firmar, solo acepta enteros desde `0` hasta `255` (inclusive).

Ejemplo: `"ConnectionTimeout": 60`

* * *

`ushort` - Tipo short sin firmar, solo acepta enteros desde `0` hasta `65535` (inclusive).

Ejemplo: `"WebLimiterDelay": 300`

* * *

`uint` - Tipo entero sin firmar, solo acepta enteros desde `0` hasta `4294967295` (inclusive).

* * *

`ulong` - tipo entero largo sin firmar, solo acepta enteros desde `0` hasta `18446744073709551615` (inclusive).

Ejemplo: `"SteamMasterClanID": 103582791440160998`

* * *

`string` - Tipo string, acepta cualquier secuencia de caracteres, incluyendo una secuencia vacía `""` y `null`. Una secuencia vacía y `null` son tratados igual por ASF, depende de tu preferencia cuál quieres usar (nosotros preferimos `null`).

Ejemplos: `"SteamLogin": null`, `"SteamLogin": ""`, `"SteamLogin": "MyAccountName"`

* * *

`ImmutableHashSet<valueType>` - Colección (set) inmutable de valores únicos en determinado `valueType`. En JSON, está definido como una matriz de elementos en determinado `valueType`. ASF usa `HashSet` para indicar que una propiedad dada solo tiene sentido para valores únicos, por lo tanto, intencionalmente ignorará cualquier posible duplicado (si por alguna razón los proporcionas).

Ejemplo para `ImmutableHashSet<uint>`: `"Blacklist": [267420, 303700, 335590]`

* * *

`ImmutableDictionary<keyType, valueType>` - Diccionario (mapa) inmutable que mapea una clave única especificada en su `keyType`, al valor especificado en su `valueType`. En JSON, esta definida como un objeto con pares de valor clave. Ten en cuenta que `keyType` siempre se cita en este caso, incluso si es tipo de valor, como `keyType`. También existe un requisito estricto de que la clave sea única en el mapa, aplicado por JSON.

Ejemplo para `ImmutableDictionary<ulong, byte>`: `"SteamUserPermissions": { "76561198174813138": 3, "76561198174813137": 1 }`

* * *

`flags` - Los atributos de bandera combinan varias propiedades en un valor final al aplicar operaciones de bits. Esto permite elegir al mismo tempo cualquier posible combinación de diferentes valores permitidos. El valor final es el resultado de la suma de los valores de todas las opciones habilitadas.

Por ejemplo, dados los siguientes valores:

| Valor | Nombre  |
| ----- | ------- |
| 0     | Ninguno |
| 1     | A       |
| 2     | B       |
| 4     | C       |

Usar `B + C` resultaría en el valor `6`, usar `A + C` resultaría en el valor `5`, usar `C` resultaría en el valor `4` y así sucesivamente. Esto permite crear cualquier posible combinación de valores habilitados - si decides habilitar todos, usando `Ninguno + A + B + C`, obtendrías el valor `7`. También ten en cuenta que la bandera con valor `0` está habilitada por definición en todas las otras combinaciones disponibles, por lo tanto muy a menudo es una bandera que no habilitada nada específico (como en `Ninguno`).

Como puedes ver, en el ejemplo anterior tenemos 3 banderas disponibles para activar/apagar (`A`, `B`, `C`), y `8` posibles valores en total:

- `None -> 0`
- `A -> 1`
- `B -> 2`
- `A+B -> 3`
- `C -> 4`
- `A+C -> 5`
- `B+C -> 6`
- `A+B+C -> 7`

Ejemplo: `"SteamProtocols": 7`

* * *

## Compatibilidad de mapeo

Debido a las limitaciones de JavaScript de ser incapaz de serializar correctamente los campos `ulong` en JSON cuando se usa el ConfigGenerator basado en la web, los campos `ulong` serán interpretados como líneas on el prefijo `s_` en la configuración resultante. Esto incluye, por ejemplo, `"SteamOwnerID": 76561198006963719` que será escrito por nuestro ConfigGenerator como `"s_SteamOwnerID": "76561198006963719"`. ASF incluye la lógica adecuada para manejar este mapeo de líneas automáticamente, así que las entradas `s_` en tus configuraciones son válidas y correctamente generadas. Si estás generando las configuraciones tú mismo, recomendamos apegarse a los campos `ulong` originales de ser posible, pero si no es posible, también puedes seguir este esquema y codificarlos con el prefijo `s_` añadido a sus nombres. Esperamos resolver eventualmente esta limitación de JavaScript.

* * *

## Compatibilidad de configuraciones

Es prioridad para ASF permanecer compatible con configuraciones antiguas. Como ya debes saber, las propiedades de configuración faltantes son tratadas como si estuvieran en sus **valores predeterminados**. Por lo tanto, si una nueva propiedad de configuración es introducida en una nueva versión de ASF, todas tus configuraciones permanecerán **compatibles** con la nueva versión, y ASF tratará esa nueva propiedad como si estuviera definida en su **valor predeterminado**. Siempre puedes acuerdo, eliminar o editar las propiedades de configuración de acuerdo a tus necesidades. Recomendamos limitar las propiedades de configuración definidas solo a aquellas que quieres cambiar, ya que de esta manera automáticamente se adquieren los valores por defecto para todas las demás, no solo manteniendo limpia tu configuración sino además aumentando la compatibilidad en caso de que decidamos cambiar el valor por defecto de una propiedad que no quieras establecer explícitamente tú mismo (ejemplo, `WebLimiterDelay`).

* * *

## Autorrecarga

Empezando en ASF V2.1.6.2+, el programa ahora puede detectar "al momento" la modificación de las configuraciones - gracias a eso ASF automáticamente:

- Crea (e iniciar, si es necesario) una nueva instancia de bot, cuando creas su archivo de configuración
- Detiene (si es necesario) y elimina una antigua instancia de bot, cuando eliminas su archivo de configuración
- Detiene (e inicia, si es necesario) cualquier instancia de bot, cuando editas su archivo de configuración
- Reinicia (si es necesario) el bot con un nuevo nombre, cuando renombras su archivo de configuración

Todo lo anterior es transparente y se hará automáticamente sin necesidad de reiniciar el programa, o detener otras (no afectadas) instancias de bot.

Además, ASF también se reiniciará a sí mismo (si `AutoRestart` lo permite) si modificas la configuración `ASF.json`. Del mismo modo, el programa se cerrará si lo eliminas o renombras.
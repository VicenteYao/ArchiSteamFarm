# Instalación

Si llegaste aquí por primera vez, ¡bienvenido! Estamos muy contentos de ver a otro viajero interesado en nuestro proyecto, aunque ten en cuenta que con un gran poder viene una gran responsabilidad - ASF es capaz de hacer muchas cosas relacionadas con Steam, siempre y cuando **te intereses lo suficiente para aprender cómo usarlo**. Hay una difícil curva de aprendizaje involucrada aquí, y esperamos que leas la wiki en este sentido, la cual explica en detalle cómo funciona todo.

Si todavía sigues aquí significa que soportaste nuestro texto de arriba, lo cual es bueno. A menos que te lo hayas saltado, entonces vas a tener un **[mal momento](https://www.youtube.com/watch?v=WJgt6m6njVw)** muy pronto... En cualquier caso, ASF es una aplicación de consola, lo que significa que el programa en sí no tiene una GUI (Interfaz Gráfica de Usuario) amigable como a las que en general estás acostumbrado. ASF estaba pensado principalmente para ser ejecutado en servidores, por lo que actúa como un servicio (daemon) y no como una aplicación de escritorio.

Sin embargo, esto no significa que no puedas usarlo en tu PC o que usarlo es de alguna manera más complicado de lo usual, nada de eso. ASF es un programa independiente que no necesita instalación, y funciona en seguida, pero requiere una configuración antes de ser útil. La configuración es decirle a ASF lo que en realidad debe hacer después de ejecutarlo. Si lo ejecutas sin configuración, entonces ASF no hará nada, simple.

* * *

## Configuración de SO específico

En general, aquí está lo que haremos los próximos minutos:

- Instalar los **[prerrequisitos de .NET Core](#net-core-prerequisites)**.
- Descargar la **[última versión de ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** en su apropiada variante de SO específico.
- Extraer el archivo en una nueva ubicación (y usar `chmod +x ArchiSteamFarm` si estás en Linux/OS X).
- **[Configurar ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es)**.
- Ejecutar ASF y ver la magia.

Suena bastante simple, ¿cierto? Así que hagámoslo.

* * *

### Prerrequisitos de .NET Core

El primer paso es asegurarte de que tu SO puede siquiera ejecutar ASF correctamente. ASF está escrito en C#, basado en .NET Core y podría requerir librerías nativas que todavía no estén disponibles en tu plataforma. Dependiendo de si usas Windows, Linux o OS X, tendrás diferentes requerimientos, aunque todos ellos están enlistados en el documento **[Prerrequisitos de .NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)** que debes seguir. Este es nuestro material de referencia que debe ser usado, pero por simplicidad también hemos detallado debajo todos los paquetes requeridos, para que no tengas que leer el documento completo.

Es perfectamente normal que algunas (o incluso todas) las dependencias ya existan en tu sistema por haber sido instaladas por software de terceros que utilices. Aún así, debes asegurarte que verdaderamente ese es el caso ejecutando el instalador adecuado para tu sistema operativo - sin esas dependencias ASF no se ejecutará.

Ten en cuenta que no necesitas hacer nada más para la compilación de SO específico, especialmente instalar .NET Core SDK o incluso "runtime", ya que el paquete de SO específico ya incluye todo eso. Solamente necesitas los prerrequisitos de .NET Core (dependencias) para correr el tiempo de ejecución de .NET Core incluido en ASF.

#### **[Windows](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-windows)**:

- **[Microsoft Visual C++ 2015 Redistributable Update](https://www.microsoft.com/en-us/download/details.aspx?id=53587)** (x64 para Windows de 64 bits, x86 para Windows de 32 bits)
- Es altamente recomendado que te asegures que todas las actualizaciones de Windows ya estén instaladas. Por lo menos necesitar **[KB2533623](https://support.microsoft.com/en-us/help/2533623/microsoft-security-advisory-insecure-library-loading-could-allow-remot)** y **[KB2999226](https://support.microsoft.com/en-us/help/2999226/update-for-universal-c-runtime-in-windows)**, pero podrían necesitarse más actualizaciones. Todas ellas ya están instaladas si tu Windows está actualizado. Asegúrate de cumplir esos requisitos antes de instalar el paquete Visual C++.

#### **[Linux](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-linux)**:

Los nombres de los paquetes depende de la distribución de Linux que estés usando, hemos listado las más comunes. Puedes obtener todas con el administrador de paquetes nativos para tu sistema operativo (tal como `apt` para Debian o `yum` para CentOS).

- `libcurl` (`libcurl4`, `libcurl3`)
- `libicu` (última versión para tu distribución, por ejemplo `libicu60`)
- `libkrb5-3` (`krb5-libs`)
- `liblttng-ust0` (`lttng-ust`)
- `libssl` (`libssl1.1`, `openssl-libs`, última versión 1.1.X para tu distribución)
- `zlib1g` (`zlib`)

Al menos algunas de esas ya deberían estar disponibles nativamente en tu sistema (tal como `zlib1g` que es requerida en casi cualquier distro de Linux hoy en día).

#### **[OS X](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-macos)**:

- Ninguno por ahora, pero debes tener instalada la última versión de OS X, al menos 10.13+

* * *

### Descargando

Ya que tengamos todas las dependencias requeridas, el siguiente paso es descargar la **[última versión de ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**. ASF está disponible en diversas variantes, pero te interesa el paquete que concuerde con tu sistema operativo y arquitectura. Por ejemplo, si usas `Win`dows de `64`-bits, entonces necesitas el paquete `ASF-win-x64`. Para más información acerca de las variantes disponibles, visita la sección de **[compatibilidad](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-es-es)**. ASF también es capaz de ejecutarse en sistemas operativos para los que no construimos un paquete de SO específico, tal como **Windows de 32-bits**, dirígete a **[configuración genérica](#configuración-genérica)** para eso.

![Recursos](https://i.imgur.com/Ym2xPE5.png)

Después de la descarga, empieza extrayendo el archivo zip en su propia carpeta. Recomendamos usar **[7-zip](https://www.7-zip.org)**, utilidades estándar como `unzip` de Linux/OS X también deberían funcionar sin problemas. Posteriormente, tendrás un gran desorden de carpetas y archivos. No te preocupes, lo limpiaremos en un segundo.

Si estás usando Linux/OS X, no te olvides de usar `chmod +x ArchiSteamFarm`, ya que los permisos no están establecidos automáticamente en el archivo zip. Esto solo se tiene que hacer una vez después del desempaquetado inicial.

Se recomienda desempaquetar ASF en **su propio directorio** y no en algún directorio existente que ya estés usando para algo más - la función de actualizaciones automáticas de ASF eliminará todos los archivos antiguos y no relacionados cuando se actualice, lo que podría resultar en la pérdida de cualquier cosa no relacionada que pongas en el directorio de ASF. Si tienes algún script o archivo adicional que quieras usar con ASF, pónlos en una carpeta superior.

Un ejemplo de la estructura se vería así:

    C:\ASF (donde pones tus cosas)
        ├── ASF shortcut.lnk (opcional)
        ├── Config shortcut.lnk (opcional)
        ├── Commands.txt (opcional)
        ├── MyExtraScript.bat (opcional)
        ├── ... (cualquier otro archivo de tu elección, opcional)
        └── Core (dedicado a ASF solamente, donde extraes el archivo)
             ├── ArchiSteamFarm.dll
             ├── config
             └── (...)
    

Esta es una estructura que recomendamos, para que no tengas la necesidad de navegar por un gran número de archivos y carpetas incluidos en ASF, ya que para su uso solamente necesitas un acceso directo a la carpeta de configuración (config) y el ejecutable principal.

Preparemos la estructura de ASF para su uso. Si lo deseas, ahora puedes saltar al siguiente paso, ya que limpiar la estructura de ASF no es necesario (especialmente si estás usando compilaciones de sistema operativo específico que ya están empaquetadas), pero puede hacer tu vida un poco más fácil.

Puedes abrir la carpeta de ASF y buscar el archivo ejecutable, este será `ArchiSteamFarm.exe` en Windows, y `ArchiSteamFarm` en Linux/OS X. Haz clic derecho y selecciona "copiar". Ahora navega al lugar donde quieras tener el acceso directo de ASF (como tu escritorio), haz clic derecho y elige "pegar acceso directo". Puedes renombrar tu acceso directo si lo deseas, como nombrarlo "ASF". Ahora haz lo mismo con el directorio `config` que puedes encontrar en el mismo lugar que el binario de ASF.

Después de una pequeña limpieza, tendrás una estructura conveniente similar a la de abajo:

![Estructura](https://i.imgur.com/k85csaZ.png)

Esto te permitirá acceder fácilmente al binario de ASF y a los archivos de configuración sin mucho problema. En mi caso decidí usar la estructura mencionada arriba, así que mis archivos de ASF están directamente en el directorio "Core". Puedes adaptar esta estructura a tu gusto, tal como tener ASF más los accesos directos de configuración en el escritorio y el directorio de ASF, por ejemplo, en `C:\ASF`; es tu decisión.

Se aconseja a los usuarios de Linux/OS X que hagan lo mismo, pueden usar excelentes mecanismos de enlaces simbólicos disponibles a través de `ln -s`.

* * *

### Configuración

Ahora estamos listos para hacer el último paso, la configuración. Este es por mucho el paso más difícil, ya que involucra mucha información nueva con la que todavía no estás familiarizado, así que intentaremos proporcionar ejemplos fáciles de entender y una explicación simplificada.

Primero y más importante, hay una página de **[configuración](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es)** que explica **todo** lo que se relaciona con la configuración, pero es una enorme cantidad de información nueva, mucha de la cual no necesitamos saber ahora mismo. En cambio, te enseñaremos cómo obtener la información que realmente necesitas.

La configuración de ASF puede realizarse de dos maneras - ya sea usando nuestro generador de configuración web, o manualmente. Esto se explica a detalle en la sección de **[configuración](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es)**, así que consúltala si quieres información más detallada. Usaremos el generador de configuración web, ya que es mucho más fácil.

Dirígete a la página de nuestro **[generador de configuración web](https://justarchinet.github.io/ASF-WebConfigGenerator)** con tu navegador preferido, necesitas tener javascript activado en caso de que lo hayas desactivado manualmente. Recomendamos Chrome o Firefox, pero debería funcionar en todos los navegadores más populares.

Después de abrir la página, cambia a la pestaña "Bot". Ahora deberías ver una página similar a la siguiente:

![Pestaña de bot](https://i.imgur.com/aF3k8Rg.png)

Si por cualquier motivo la versión de ASF que hayas descargado es más antigua que la que el generador de configuración usa por defecto, simplemente elige tu versión de ASF del menú desplegable. Esto puede ocurrir ya que el generador de configuración puede ser usado para generar configuraciones para versiones más nuevas de ASF (prelanzamiento) que aún no han sido marcadas como estables. Has descargado la última versión estable de ASF que se ha verificado que funciona confiablemente.

Empieza por poner un nombre para tu bot en el campo resaltado en rojo. Este puede ser cualquier nombre que desees utilizar, tal como tu "nickname", nombre de la cuenta, un número, o cualquier otra cosa. Solo hay una palabra que no se puede utilizar, `ASF`, ya que esa palabra está reservada para el archivo de configuración global. Además de eso el nombre de tu bot no puede empezar con un punto (ASF intencionalmente ignora esos archivos). También recomendamos que evites usar espacios, puedes usar `_` como separador de palabras si es necesario.

Después de decidir tu nombre, cambia el interruptor de `Enabled` para que esté activo, esto determina si tu bot es iniciado automáticamente por ASF tras la ejecución (del programa).

Ahora puedes decidir entre dos cosas:

- Puedes poner tu nombre de usuario en el campo `SteamLogin` y tu contraseña en el campo `SteamPassword`
- O puedes dejarlos vacíos

Haciendo lo primero permitirá que ASF use automáticamente las credenciales de tu cuenta durante el inicio, por lo que no necesitarás ingresarlas manualmente cada vez que ASF las necesite. Sin embargo puedes decidir omitirlas, en cuyo caso no serán guardadas, por lo que ASF no será capaz de iniciar automáticamente sin tu ayuda y necesitarás ingresarlas durante el tiempo de ejecución (runtime).

ASF requiere tus credenciales de inicio de sesión porque incluye su propia implementación del cliente de Steam y necesita los mismos detalles para iniciar sesión como el que usas. Tus credenciales de inicio de sesión no se guardan en ninguna parte, excepto en tu PC, solo en el directorio `config` de ASF, nuestro generador web de configuración está basado en el cliente, lo que significa que el código es ejecutado localmente en tu explorador para generar configuraciones de ASF válidas, sin que los detalles que ingresas salgan de tu PC en primer lugar, por lo que no hay necesidad de preocuparse por ninguna posible fuga de datos sensibles. Aún, si por cualquier razón no quieres ingresar tus credenciales ahí, entendemos eso, y puedes ingresarlas manualmente luego en los archivos generados, u omitirlas completamente e ingresarlas solo en la línea de comandos ASF. Puedes encontrar más acerca de seguridad en la sección **[configuración](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es)**.

También puedes decidir dejar un campo vacío, como `SteamPassword`, entonces ASF será capaz de usar tu inicio de sesión automáticamente, pero todavía te pedirá la contraseña (similar al cliente Steam). Si usas el control parental de steam para desbloquear la cuenta, necesitarás ponerlo en el campo `SteamParentalCode`.

Después de la decisión y detalles opcionales, ahora tu página web se verá similar a la siguiente:

![Pestaña de bot 2](https://i.imgur.com/yf54Ouc.png)

Ahora puedes presionar el botón "Descargar" y nuestro generador de configuración web creará un nuevo archivo `json` basado en el nombre que hayas elegido. Guarda ese archivo en el directorio `config` de ASF. Puedes usar el atajo a `config` previamente creado, o buscar manualmente el directorio `config`, directamente en la estructura de archivos de ASF.

Tu directorio `config` ahora se verá así:

![Estructura 2](https://i.imgur.com/crWdjcp.png)

¡Felicidades! Acabas de terminar la configuración más básica de un bot en ASF. Explicaremos más en breve, por ahora esto es todo lo que necesitas.

* * *

### Ejecutando ASF

Ahora estás listo para ejecutar el programa por primera vez. Simplemente haz doble clic en el acceso directo de ASF, o en el ejecutable `ArchiSteamFarm` que se encuentra en el directorio de ASF.

Posteriormente, asumiendo que instalaste todas las dependencias necesarias en el primer paso, ASF debería ejecutarse correctamente, detectar tu primer bot (si no olvidaste poner el archivo generado en el directorio `config`), e intentar iniciar sesión:

![ASF](https://i.imgur.com/u5hrSFz.png)

Si proporcionaste `SteamLogin` y `SteamPassword` para que utilice ASF, se te pedirá solamente tu código de Steam Guard (e-mail, 2FA o ninguno, dependiendo de tu configuración en Steam). Si no lo hiciste, también se te pedirá tu nombre de usuario y contraseña.

Ahora es buen momento para revisar nuestra sección **[política de privacidad](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-es-es#pol%C3%ADtica-de-privacidad-actual)** si te preocupa qué pasará a continuación, como se expresa por el mismo ASF.

Después del inicio de sesión, asumiendo que tus datos son correctos, iniciarás sesión con éxito, y ASF comenzará a recolectar cromos usando la configuración predeterminada:

![ASF 2](https://i.imgur.com/Cb7DBl4.png)

Esto prueba que ASF está haciendo su trabajo con éxito en tu cuenta, ahora puedes minimizar el programa y hacer algo más. Después del suficiente tiempo (dependiendo de **[rendimiento](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-es-es)**) verás que lentamente recibes cromos de Steam. Por supuesto, para que eso suceda debes tener juegos válidos para recolectar, mostrado como "puedes obtener X cromos más jugando este juego" en tu **[página de insignias](https://steamcommunity.com/my/badges)** - si no hay juegos para recolectar, entonces ASF indicará que no hay nada que hacer, como se expresa en nuestras **[preguntas frecuentes](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-es-es#qu%C3%A9-es-asf)**.

Esto concluye nuestra guía de configuración básica. Ahora puedes decidir si quieres seguir configurando ASF, o dejarlo hacer su trabajo con la configuración predeterminada. Abarcaremos algunos detalles básicos más, y luego dejaremos toda la wiki para que descubras.

* * *

### Configuración extendida

#### Recolectar varias cuentas a la vez

ASF soporta la recolección de más de una cuenta a la vez, la cual es su función principal. Puedes añadir más cuentas a ASF generando más archivos de configuración de bots, exactamente del mismo modo que generaste el primero hace algunos minutos. Solo debes de asegurarte de dos cosas:

- Nombre de bot único, si ya nombraste tu primer bot como "CuentaPrincipal", no puedes tener otro con el mismo nombre.
- Detalles de inicio de sesión válidos, tales como `SteamLogin`, `SteamPassword` y `SteamParentalCode` (si usas la configuración parental de Steam)

En otras palabras, simplemente ve a configuración de nuevo y haz exactamente los mismo, solo que para tu segunda o tercera cuenta. Recuerda usar nombres únicos para todos tus bots.

* * *

#### Cambiar la configuración

Puedes cambiar ajustes existentes de la misma forma - generando un nuevo archivo de configuración. Si aún no has cerrado nuestro generador de configuración web, haz clic en "toggle advanced settings" y ve lo que hay ahí para descubrir. Para este tutorial cambiaremos el ajuste `CustomGamePlayedWhileFarming`, que te permite establecer que se muestre un nombre personalizado cuando ASF está recolectando, en lugar de mostrar el nombre real del juego.

Empecemos, si ejecutas ASF y empieza a recolectar, en ajustes predeterminados verás que tu cuenta de Steam está jugando:

![Steam](https://i.imgur.com/1VCDrGC.png)

Cambiemos eso. Cambia a configuración avanzada en el generador de configuración web y busca `CustomGamePlayedWhileFarming`. Una vez que hagas eso, pon el texto personalizado que quieras que se muestre, tal como "Recolectando cromos":

![Pestaña de bot 3](https://i.imgur.com/gHqdEqb.png)

Ahora descarga el nuevo archivo de configuración exactamente de la misma manera, luego **sobrescribe** tu archivo de configuración previo con el nuevo. También puedes eliminar tu viejo archivo de configuración y poner el nuevo en su lugar.

Una vez que hagas eso y ejecutes ASF de nuevo, notarás que ASF ahora muestra tu texto personalizado:

![Steam 2](https://i.imgur.com/vZg0G8P.png)

Esto confirma que editaste correctamente tu configuración. De la misma manera puedes cambiar las propiedades globales de ASF, cambiando de la pestaña de bot a la pestaña "ASF", y luego descargando el archivo de configuración `ASF.json` generado y poniéndolo en tu directorio `config`.

* * *

#### Usando la interfaz de ASF

ASF es una aplicación de consola y no incluye una interfaz gráfica de usuario. Sin embargo, estamos trabajando en un **[ASF-ui](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-es-es#asf-ui)** "frontend" para nuestra interfaz IPC, la cual puede ser una manera muy decente y fácil de usar para acceder a varias funciones de ASF.

Para poder usar la interfaz de usuario de ASF, debes asegurarte de establecer las propiedades de configuración global `IPC` y `SteamOwnerID` (pestaña ASF).

Para `SteamOwnerID`, necesitas introducir el identificador único de tu cuenta de Steam en forma de 64-bits. Puedes encontrarlo de diversas formas, usaremos **[SteamRep](https://steamrep.com)** para ello. Abre el sitio web, ubica el botón "sign in through Steam" en la esquina superior derecha, luego inicia sesión. Posteriormente, en el mismo lugar, haz clic en tu avatar, y busca el campo `steamID64` en tu perfil.

![SteamRep](https://i.imgur.com/RUuJ63i.png)

Para mi cuenta, es este número `76561198006963719`. Tendrás uno similar, que también inicia con `7656`. Cópialo.

Ahora dirígete una vez más a nuestro generador de configuración web e ingresa ese número como SteamOwnerID.

![SteamOwnerID](https://i.imgur.com/V6jslfQ.png)

Solo necesitas hacer una cosa más, cambia a configuración avanzada, busca la opción `IPC`, y actívala.

![IPC](https://i.imgur.com/NhujZCN.png)

Ahora puedes descargar tu configuración de ASF y ponerla en el directorio `config`, como de costumbre. Posteriormente, ejecuta ASF de nuevo, y debes poder confirmar que inició correctamente la interfaz IPC:

![IPC 2](https://i.imgur.com/ZmkO8pk.png)

Si hiciste todo correctamente, ahora serás capaz de acceder a la interfaz IPC de ASF en **[este](http://localhost:1242)** enlace, siempre que ASF se esté ejecutando. Puedes usar la interfaz de usuario de ASF para diversos propósitos, por ejemplo, enviar **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)**. No dudes en echar un vistazo para descubrir todas las funcionalidades de la interfaz de usuario de ASF.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

Ten en cuenta que la interfaz de usuario de ASF está actualmente en estado anticipado y no todo está disponible/funcionando aún, pero es más que suficiente para un uso simple de ASF.

* * *

### Sumario

Has configurado ASF con éxito para usar tus cuentas de Steam y ya lo has personalizado un poco a tu gusto. Si has seguido toda nuestra guía, entonces lograste enviar un comando simple a través de la interfaz de usuario de ASF. Ahora es un buen momento para leer toda nuestra sección de **[configuración](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es)** para aprender lo que haces las diferentes configuraciones que viste en la pestaña de configuración avanzada, y lo que ASF puede ofrecer. Si te has encontrado con algún problema o tienes alguna pregunta genérica, lee las **[preguntas frecuentes](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-es-es)** lo que debería cubrir todo, o al menos la mayoría de las preguntas que puedas tener. Si quieres aprender todo acerca de ASF y de cómo puede hacer tu vida más fácil, dirígete al resto de **[nuestra wiki](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home-es-es)**. ¡Diviértete!

* * *

## Configuración genérica

Esta configuración es para usuarios avanzados que quieren establecer ASF para ejecutarlo en su variante **[genérica](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-es-es#gen%C3%A9rico)**. No se recomienda para personas que pueden usar la **[configuración de SO específico](#configuración-de-so-específico)**.

Querrás usar la variante genérica principalmente en tres situaciones (por supuesto la puedes usar de todos modos):

- Cuando usas un sistema operativo para el cual no creamos un paquete de SO específico (tal como Windows de 32-bits)
- Cuando ya tienes .NET Core Runtime/SDK, o quieres instalarlo y usarlo
- Cuando quieres minimizar el tamaño de la estructura de ASF manejando los requerimientos de "runtime" por ti mismo

Sin embargo, ten en cuenta que tú eres responsable del .NET Core runtime en este caso. Esto significa que si tu .NET Core SDK (runtime) no está disponible, está desactualizado o roto, ASF no funcionará. Es por esto que no recomendamos esta configuración para usuarios casuales, ya que ahora necesitas asegurarte de que tu .NET Core SDK (runtime) coincida con los requerimientos de ASF y puede ejecutarlo, en contraposición a que **nosotros** nos aseguremos que nuestro .NET Core runtime en conjunto con ASF puede hacerlo.

Para el paquete genérico, puedes seguir toda la guía de SO específico de arriba, con dos pequeños cambios. Además de instalar los prerrequisitos de .NET Core, también querrás instalar .NET Core SDK, y en lugar de tener un archivo ejecutable `ArchiSteamFarm(.exe)` para SO específico, ahora tienes un binario genérico `ArchiSteamFarm.dll` solamente. Todo lo demás es exactamente igual.

Con pasos extra:

- Instalar los **[prerrequisitos de .NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)**.
- Instalar **[.NET Core SDK](https://www.microsoft.com/net/download)** (o por lo menos "runtime") apropiado para tu SO. Probablemente querrás usar un instalador. Dirígete a **[requisitos de runtime](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-es-es#requisitos-de-runtime)** si no estás seguro de qué versión instalar.
- Descarga la **[última versión de ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** en su variante genérica.
- Extrae el archivo en una nueva ubicación (y usa `chmod +x ArchiSteamFarm.sh` si estás en Linux/OS X).
- **[Configurar ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es)**.
- Ejecuta ASF ya sea usando un script auxiliar (helper script) o ejecutando `dotnet /path/to/ArchiSteamFarm.dll` manualmente desde tu "shell" favorito.

Los scripts auxiliares (tal como `ArchiSteamFarm.cmd` para Windows y `ArchiSteamFarm.sh` para Linux/OS X) están ubicados junto al binario `ArchiSteamFarm.dll` - estos solo están incluidos en la variante genérica. Puedes usarlos si no quieres ejecutar manualmente el comando `dotnet`. También puedes crear un acceso directo a esos scripts cómo se mostró anteriormente, ya que deben proporcionar un reemplazo de binario en forma de script. Obviamente los scripts auxiliares no funcionarán si no instalaste .NET Core SDK y no tienes el ejecutable `dotnet` disponible en tu `PATH`. Los scripts auxiliares son completamente opcionales, siempre puedes usar manualmente `dotnet /path/to/ArchiSteamFarm.dll`.
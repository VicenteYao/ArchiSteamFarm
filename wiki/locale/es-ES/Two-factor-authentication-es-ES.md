# Autenticación de dos factores

Hace un tiempo Valve introdujo un sistema conocido como "Escrow" que requiere autenticación adicional para varias actividades relacionadas con la cuenta. Puedes leer más al respecto **[aquí](https://support.steampowered.com/kb_article.php?ref=1284-WTKB-4729)** y **[aquí](https://support.steampowered.com/kb_article.php?ref=8078-TPHC-6195)**. Es crucial entender primero el sistema 2FA, antes de intentar entender la lógica detrás de ASF 2FA.

Ahora, como puedes ver todos los intercambios son retenidos hasta por 15 días, lo que no es un problema mayor en lo que respecta a nuestro ASF, especialmente para aquellos que quieres automatización completa. Afortunadamente, ASF incluye una solución a ese problema, llamada ASF 2FA.

* * *

# Lógica de ASF

Independientemente de si usas o no ASF 2FA explicado abajo, ASF incluye lógica adecuada y es plenamente consciente de las cuentas protegidas por 2FA estándar. Te pedirá la información requerida cuando se necesite (como durante el inicio de sesión). Si usas ASF 2FA, el programa podrá omitir esas solicitudes y generar automáticamente los códigos requeridos, ahorrándote la molestia y habilitando funcionalidad adicional (descrita abajo).

* * *

# ASF 2FA

ASF 2FA es el módulo integrado responsable de proporcionar características 2FA al proceso de ASF, tal como generar códigos y aceptar confirmaciones. Duplica tu autenticador existente, así que no hay necesidad usar ASF 2FA exclusivamente.

Puedes verificar si tu cuenta bot ya está usando ASF 2FA ejecutando **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `2fa`. A menos que ya hayas importado tu autenticador como ASF 2FA, todos los comandos `2fa` no serán operativos, lo que significa que tu cuenta no está usando ASF 2FA, por lo tanto tampoco es elegible para las características avanzadas de ASF que requieren que el módulo esté operativo.

Para activar ASF 2FA, necesitas tener:

- Autenticador de Steam funcionando en tu Android
- o autenticador de Steam funcionando en tu iOS
- o autenticador de Steam funcionando en **[SteamDesktopAuthenticator](https://github.com/Jessecar96/SteamDesktopAuthenticator)**
- o autenticador de Steam funcionando en **[WinAuth](https://winauth.github.io/winauth)**
- o cualquier otra implementación funcional del autenticador de Steam con acceso a secreto compartido/identidad y el ID de dispositivo

* * *

## Importar

Para completar los pasos que se explican a continuación, ya debes tener un autenticador vinculado y operativo que sea soportado por ASF. Actualmente ASF soporta varias fuentes de 2FA - Android, iOS, SteamDesktopAuthenticator y WinAuth. Si aún no tienes ningún autenticador, necesitas elegir uno de ellos y configurarlo primero. Si no sabes cuál elegir, recomendamos WinAuth, pero cualquier de esos funcionará bien suponiendo que sigas las instrucciones.

Todas las siguientes guías requieren que ya tengas un autenticador **funcional y operativo** siendo usado con una herramienta/aplicación dada. ASF 2FA no funcionará correctamente si importas datos inválidos, por lo tanto asegúrate de que tu autenticador funciona correctamente antes de intentar importarlo. Esto incluye probar y verificar que las siguientes funciones del autenticador trabajan correctamente:

- Puedes generar códigos y esos códigos son aceptados por la red de Steam
- Puedes obtener confirmaciones, y están llegando a tu autenticador móvil
- Puedes aceptar esas confirmaciones, y son reconocidas apropiadamente por la red de Steam como confirmadas/rechazadas

Asegúrate de que tu autenticador funciona comprobando si las acciones anteriores funcionan - si no lo hacen, entonces tampoco funcionarán en ASF, solo perderás tiempo y te causarás problemas.

* * *

### Teléfono Android

En general, para importar el autentificador desde tu teléfono Android necesitarás acceso **[root](https://es.wikipedia.org/wiki/Android_rooting)**. El rooteo varia entre dispositivos, así que no te diré cómo rootear tu dispositivo. Visita **[XDA](https://www.xda-developers.com/root)** para excelentes guías sobre cómo hacer eso, así como para información sobre rooteo en general. Si no puedes encontrar tu dispositivo o la guía que necesitas, intenta buscando en Google.

Al menos oficialmente, no es posible acceder a los archivos protegidos de Steam sin root. El único método oficial no-root para extraer los archivos de Steam es crear un respaldo no encriptado de `/data` de un modo u otro y manualmente obtener los archivos correctos y ponerlos en tu PC, sin embargo, debido a que esto depende en gran medida del fabricante de tu teléfono y **no es** un estándar de Android, no lo discutiremos aquí. Si eres afortunado de tener tal funcionalidad, puedes hacer uso de ella, pero la mayoría de los usuarios no tienen nada parecido.

No oficialmente, es posible extraer los archivos necesarios sin acceso root, instalando o regresando tu aplicación de Steam a la versión 2.1 (o anterior), configurando el autenticador móvil y luego creando un respaldo de la app (junto con los archivos `data` que necesitamos) a través de `adb backup`. Sin embargo, dado que es una grave violación de seguridad y una manera totalmente no soportada de extraer los archivos, no vamos a profundizar al respecto, Valve deshabilitó este hueco en la seguridad en versiones más nuevas por una razón, y solo la mencionamos como una posibilidad.

Suponiendo que has rooteado exitósamente tu teléfono, posteriormente debes descargar cualquier explorador root disponible en el mercado, tal como **[este](https://play.google.com/store/apps/details?id=com.jrummy.root.browserfree)** (o cualquier otro de tu preferencia). También puedes acceder a los archivos protegidos a través de ADB (Android Debug Bridge) o cualquier otro método que tengas disponible, lo haremos por medio del explorador ya que definitivamente es la forma más amigable.

Una vez que abras tu explorador root, navega a la carpeta `/data/data`. Ten en cuenta que el directorio `/data/data` está protegido y no podrás acceder a él sin acceso root. Una vez ahí, encuentra la carpeta `com.valvesoftware.android.steam.community` y cópiala a tu `/sdcard`, que dirige a tu almacenamiento interno. Después, deberías poder conectar tu teléfono a tu PC y copiar la carpeta desde tu almacenamiento interno como de costumbre. Si por alguna razón la carpeta no es visible a pesar estar seguro que la copiaste al lugar correcto, intenta reiniciar tu teléfono primero.

Ahora, puedes elegir si quieres importar primero tu autenticador a WinAuth, luego a ASF, o directamente a ASF. La primera opción es más amigable y te permite duplicar tu autenticador también en tu PC, permitiéndote hacer confirmaciones y generar códigos desde 3 lugares diferentes - tu teléfono, tu PC y ASF. Si quieres hacer eso, simplemente abre WinAuth, añade un nuevo autenticador de Steam y elige la opción de importar desde Android, luego sigue las instrucciones accediendo a los archivos que obtuviste antes. Cuando termines, puedes importar este autenticador de WinAuth a ASF, lo que se explica en la sección dedicada a WinAuth más abajo.

Si no quieres o no necesitas pasar por WinAuth, entonces solo copia el archivo `files/Steamguard-SteamID` de nuestro directorio protegido, donde `SteamID` es el identificador 64-bit Steam de la cuenta que quieres añadir (si hay más de uno, porque si solo tienes una cuenta entonces este será el único archivo). Necesitas poner ese archivo en el directorio `config` de ASF. Una vez que lo hagas, renombre el archivo a `BotName.maFile`, donde `BotName` es el nombre del bot al que le estás añadiendo ASF 2FA. Después de este paso, ejecuta ASF - debería notar el archivo `.maFile` e importarlo.

    [*] INFO: ImportAuthenticator() <1> Convirtiendo .maFile al formato ASF...
    <1> Por favor ingrese su ID de dispositivo autenticador móvil (incluyendo "android:"):
    

Tendrás que hacer un paso más - encuentra la propiedad `DeviceID` en `shared_prefs/steam.uuid.xml`. Estará dentro de las etiquetas XML y empieza con `android:`. Copia (o escribe) y ponlo en ASF como se pide. Si hiciste todo correctamente, la importación debería estar terminada.

    [*] INFO: ImportAuthenticator() <1> ¡Autenticador móvil importado exitosamente!
    

Por favor, confirma que la aceptación de confirmaciones realmente funciona. Si cometiste un error al introducir tu `DeviceID` entonces tendrás un autenticador medio roto - los códigos funcionarán, pero aceptar confirmaciones no. Puedes eliminar `Bot.db` y empezar de nuevo si es necesario.

* * *

### iOS

Para iOS puedes usar **[ios-steamguard-extractor](https://github.com/CaitSith2/ios-steamguard-extractor)**. Esto es posible gracias a que puedes hacer respaldos no encriptados, ponerlos en tu PC y usar la herramienta para extraer los datos de Steam que de otro modo es imposible de obtener (al menos sin jailbreak, debido a la encriptación de iOS).

Dirígete a la **[última versión](https://github.com/CaitSith2/ios-steamguard-extractor/releases/latest)** para descargar el programa. Una vez que extraigas los datos puedes ponerlos, por ejemplo, en WinAuth, luego de WinAuth a ASF (aunque también puedes simplemente copiar el json generado empezando desde `{` y terminando con `}` a `BotName.maFile` y proceder normalmente). Si me preguntas, recomiendo primero importar primero a WinAuth, luego asegurarte de que ambos generan códigos y funcionan para aceptar confirmaciones, para estar seguro de que todo está correcto. Si tus credenciales no son válidas, ASF 2FA no funcionará correctamente, así que lo mejor es hacer de último el paso de importar ASF.

Para preguntas/problemas, por favor visita **[problemas](https://github.com/CaitSith2/ios-steamguard-extractor/issues)**.

*Ten en cuenta que la herramienta anterior no es oficial, la usas bajo tu propio riesgo. No ofrecemos soporte técnico si no funciona correctamente - tuvimos algunos reportes de que está exportando credenciales 2FA inválidas - ¡verifica que las confirmaciones funcionan en un autenticador como WinAuth antes de importar esos datos a ASF!*

* * *

### SteamDesktopAuthenticator

Si ya tienes tu autenticador en SDA, debes notar que el archivo `steamID.maFile` está disponible en la carpeta `maFiles`. Copia ese archivo al directorio `config` de ASF. Aseguráte de que `.maFile` está en formato no encriptado, ya que ASF no puede desencriptar los archivos de SDA - el contenido de un archivo no encriptado debe empezar con el carácter `{`.

Ahora debes renombrar `steamID.maFile` a `BotName.maFile` en el directorio config de ASF, donde `BotName` es el nombre del bot al que estás añadiendo ASF 2FA. Alternativamente, puedes dejarlo como está, ASF lo elegirá automáticamente después de iniciar sesión. Ayudar a ASF permite usar ASF 2FA antes de iniciar sesión, si no ayudas a ASF, entonces el archivo solo puede ser elegido después de que ASF inicie sesión exitósamente (ya que ASF no sabe el `steamID` de tu cuenta antes de iniciar sesión).

Si hiciste todo correctamente, ejecuta ASF, y deberías ver:

    [*] INFO: ImportAuthenticator() <1> Convirtiendo .maFile al formato ASF...
    [*] INFO: ImportAuthenticator() <1> ¡Autenticador móvil importado exitosamente!
    

A partir de ahora, tu ASF 2FA debería estar operativo para esta cuenta.

* * *

### WinAuth

Primero crea un nuevo `BotName.maFile` vacío en el directorio config de ASF, donde `BotName` es el nombre del bot al que le estás añadiendo ASF 2FA. Recuerda que debe ser `BotName.maFile` y NO `BotName.maFile.txt`, Windows oculta las extensiones conocidas por defecto. Si proporcionas un nombre incorrecto, no será elegido por ASF.

Ahora ejecuta WinAuth normalmente. Haz clic derecho en el ícono de Steam y selecciona "Show SteamGuard and Recovery Code". Luego marca la casilla "Allow copy". Debes notar una estructura JSON familiar al fondo de la ventana, que empieza con `{`. Copiar todo el texto al archivo `BotName.maFile` que creaste en el paso anterior.

Si hiciste todo correctamente, ejecuta ASF, y deberías ver:

    [*] INFO: ImportAuthenticator() <1> Convirtiendo .maFile al formato ASF...
    <1> Por favor ingrese su ID de dispositivo autenticador móvil (incluyendo "android:"):
    

Aquí viene la parte difícil. WinAuth no tiene la propiedad deviceID requerida por ASF, así que tendrás que hacer una cosa más.

Regresa a "Show SteamGuard and Recovery Code" de WinAuth y deberías ver la propiedad "Device ID" sobre el código JSON que copiaste hace poco. Copia todo el ID de dispositivo android, incluyendo la parte `android:` a ASF.

Si lo hiciste correctamente, ¡has terminado!

    [*] INFO: ImportAuthenticator() <1> ¡Autenticador móvil importado exitosamente!
    

Por favor, comprueba que aceptar confirmaciones funciona. Si cometiste un error al introducir tu `DeviceID` entonces tendrás un autenticador medio roto - los códigos funcionarán, pero aceptar confirmaciones no. Puedes eliminar `Bot.db` y empezar de nuevo si es necesario.

* * *

## Listo

Desde este momento, todos los comandos `2fa` funcionarán como si fueran generados en tu dispositivo 2FA normal. Puedes usar ambos, ASF 2fa y el autenticador de tu elección (Android, iOS, SDA o WinAuth) para generar códigos y aceptar confirmaciones.

Si tienes un autenticador en tu teléfono, opcionalmente puedes eliminar SteamDesktopAuthenticator and/o WinAuth. ya que no los necesitaremos más. Sin embargo, sugiero conservarlo por si acaso, sin mencionar que es más práctico que el autenticador de Steam normal. Solo ten en cuenta que ASF 2FA **NOT** es un autenticador de propósito general y **nunca** debe ser el único que uses, ya que ni siquiera incluye toda la información que un autenticador debería. No es posible convertir ASF 2FA de vuelva al autenticador original, por lo tanto siempre asegúrate de que tienes un autenticador en otra parte, como en WinAuth/SDA, o en tu teléfono.

* * *

## Preguntas frecuentes

### ¿Cómo hace uso ASF del módulo 2FA?

Si ASF 2FA está disponible, ASF lo usará para la confirmación automática de intercambios que sean enviados/aceptados por ASF. También será capaz de generar automáticamente códigos 2FA cuando sea necesario, por ejemplo para iniciar sesión. Además, tener ASF 2FA también habilita para su uso los comandos `2fa`. Eso sería todo por ahora, si no me olvidé de nada - básicamente ASF usa el módulo 2FA cuando lo necesita.

* * *

### ¿Qué pasa si necesito un código 2FA?

Necesitarás códigos 2FA para acceder a cuentas protegidas por 2FA, eso también incluye todas las cuentas con ASF 2FA. Debes generar los códigos en el autenticador que usaste para importar, pero también puedes generar códigos temporales a través del comando `2fa` enviado mediante el chat a un bot dado. También puedes usar el comando `2fa <BotNames>` para generar códigos temporales para instancias de bot determinadas. Esto debería ser suficiente para que accedas a las cuentas bot a través de, por ejemplo un navegador, pero como se mencionó antes - en su lugar debes usar tu autenticador normal (Android, iOS, SDA o WinAuth).

* * *

### ¿Puedo usar mi autenticador original después de importarlo como ASF 2FA?

Sí, tu autenticador original sigue siendo funcional y puedes usar junto con ASF 2FA. Ese es todo el punto del proceso - importamos a ASF las credenciales de tu autenticador, para que ASF pueda hacer eso de ellos y aceptar las confirmaciones seleccionadas en tu nombre.

* * *

### ¿Dónde se guarda el autenticador móvil de ASF?

El autenticador móvil de ASF se guarda en el archivo `BotName.db` en tu directorio config, junto con otra información crucial relacionada con una cuenta determinada. Si quieres eliminar ASF 2FA, lee cómo a continuación.

* * *

### ¿Cómo puedo eliminar ASF 2FA?

Simplemente cierra ASF y elimina el archivo `BotName.db` del bot con ASF 2FA que quieras eliminar. Esta opción eliminará el 2FA asociado con ASF, pero NO desvinculará tu autenticador. Si en cambio quieres desvincular tu autenticador, además de eliminarlo de ASF (primero), debes desvincularlo en el autenticador de tu elección (Android, iOS, SDA or WinAuth), o - si por alguna razón no puedes, usa el código de revocación que recibiste durante la vinculación del autenticador, en la página de Steam. No es posible desvincular tu autenticador a través de ASF, esto es para lo que debe ser usado el autenticador general que ya tienes.

* * *

### Vinculé mi autenticador en SDA/WinAuth, luego lo importé a ASF. ¿Puedo desvincularlo y vincularlo de nuevo en mi teléfono?

**No**. ASF **importa** los datos de tu autenticador para usarlo. Si desvinculas tu autenticador también causarás que ASF 2FA deje de funcionar, independientemente de si lo eliminas primero como se indica en la pregunta anterior. Si quieres usar tu autenticador tanto en tu teléfono como en ASF (opcionalmente también en SDA/WinAuth), necesitarás **importar** tu autenticador desde tu teléfono, y no crear uno nuevo en SDA/WinAuth. Solo puedes tener **one** autenticador vinculado, por eso ASF **importa** ese autenticador y sus datos para usarlo como ASF 2FA - es **el mismo** autenticador, solo que existiendo en dos lugares. Si decides desvincular las credenciales de tu autenticador móvil - independientemente de la forma, ASF 2FA dejará de funcionar, ya que las credenciales del autenticador móvil previamente copiadas ya no serán válidas. Para usar ASF 2FA junto con el autenticador en tu teléfono, debes importarlo desde Android/iOS, lo que se describe arriba.

* * *

### ¿Usar ASF 2FA es mejor que WinAuth/SDA/Otro autenticador establecido para aceptar todas las confirmaciones?

**Sí**, de varias maneras. Primero y más importante - usar ASF 2FA aumenta tu seguridad **significativamente**, ya que el módulo ASF 2FA asegura que ASF solo aceptará automáticamente sus propias confirmaciones, por lo que incluso si un atacante solicita un intercambio dañino, ASF 2FA **no** aceptará dicho intercambio, ya que no fue generado por ASF. Además a la parte de seguridad, usar ASF 2FA también trae beneficios de rendimiento/optimización, ya que ASF 2FA obtiene y acepta confirmaciones inmediatamente después de que son generadas, y solo entonces, contrario a la ineficiente comprobación cada X minutos hecha por SDA o WinAuth. En resumen, no hay razón para usar un autentificador de terceros sobre ASF 2FA, si planeas automatizar las confirmaciones generadas por ASF - exactamente para eso es ASF 2FA, y usarlo no causa conflicto con que confirmes todo lo demás en el autenticador de tu elección. Recomendamos usar ASF 2FA para toda la actividad de ASF - esto es mucho más seguro que cualquier otra solución.

* * *

## Avanzado

Si eres un usuario avanzado, también puedes generar los maFile manualmente. Debe tener una **[estructura JSON válida](https://jsonlint.com)** de:

```json
{
  "shared_secret": "STRING",
  "identity_secret": "STRING",
  "device_id": "STRING"
}
```

`device_id` es opcional durante la importación, pero obligatorio para el funcionamiento de ASF - ASF lo solicitará durante la importación si lo omites. Por supuesto, necesitas reemplazar `"STRING"` con contenido válido en cada campo.

Los datos de un autenticador estándar tienen más campos - son ignorados completamente por ASF durante la importación, ya que no son necesarios. Tampoco tienes que eliminarlos - ASF solo requiere un JSON válido con los 2 campos obligatorios descritos arriba, y opcionalmente también `device_id`.
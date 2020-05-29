# Estadísticas

El desarrollo de ASF está soportado por 3 cosas importantes: donaciones, retroalimentación de los usuarios, y las estadísticas. Las donaciones influyen directamente en nuestro deseo de trabajar en el proyecto, la retroalimentación de los usuarios siempre es agradable (especialmente la positiva), mientras que las estadísticas nos proporcionan información sobre cómo es usado nuestro software, y por cuántas personas - así podemos saber qué mejorar, que arreglar, y en qué enfocarnos.

ASF en ajustes por defecto tiene la propiedad de configuración global `Statistics` habilitada. Si quieres ver nuevas versiones, solución de errores, y nuevas características implementadas, deberías considerar mantenerlas activas, así podemos hacer uso de esa información para proporcionarte un mejor software (pero no solo eso). Esto es especialmente importante porque ASF se te otorga **gratuitamente**, y esto es lo menos que puedes hacer para decir gracias - **decirnos que estás usando ASF**, ya que esto es lo que nuestras estadísticas actuales hacen principalmente. ASF no recolecta ni hace uso de ningún dato que pueda ser considerado privado y/o usado en tu contra. Mantenemos el uso de las estadísticas al mínimo, y toda la información recolectada requiere que lo indiquemos aquí con precisión, junto con una explicación práctica. Esto te da una visión completa de lo que recopilamos, para qué propósito, y cómo se supone que ayuda. Todo esa información puede ser encontrada más abajo.

* * *

## Política de privacidad actual

Cuando `Statistics` está activo, sucederán las siguientes cosas:

1. La cuenta usado en ASF se unirá a nuestro **[grupo de Steam](https://steamcommunity.com/gid/103582791440160998)** al iniciar sesión. Esto se hace por tres razones:

* Te proporciona anuncios de grupo, especialmente nuevas versiones, problemas críticos, problemas de Steam y otras cosas que son importantes para mantener actualizada a la comunidad (se garantiza que no habrá nada de spam no relacionado con ASF) 
* Te permite usar nuestro soporte técnico, haciendo preguntas, resolviendo problemas, reportando problemas o sugiriendo mejoras
* Nos permite ver cuántas cuentas de Steam están siendo usadas por ASF

Consideramos el grupo de Steam como una parte crucial en relación a la comunidad de ASF. Este es nuestro principal canal de comunicación que utilizamos para todos los asuntos importantes relacionados con ASF, especialmente mantenerte actualizado con el desarrollo, problemas potenciales, avisos eventuales y todos los demás asuntos a los que deberías tener acceso como usuario. No nos beneficiamos en ninguna forma del mantenimiento de ese grupo, es un lugar dedicado a usuarios de ASF y te consideramos parte de nuestra comunidad. Ya que ser miembro del grupo de ninguna forma te identifica como usuario de ASF, no consideramos que este sea un problema en términos de privacidad.

2. Si tu cuenta es **[ilimitada](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663&l=spanish)**, usa **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-es-es#asf-2fa)**, tiene **[inventario público](https://steamcommunity.com/my/edit/settings)** con al menos 100 artículos `MatchableTypes` e intencionalmente habilitaste `SteamTradeMatcher` en tus `TradingPreferences`, entonces ASF se comunicará periódicamente con nuestro **[servidor](https://asf.justarchi.net)** para llevar a cabo la funcionalidad habilitada. Los datos reales consisten en ASF ID único (generado por ASF), y la siguiente información relacionada con la cuenta:

* Tu identificador de Steam (en forma de 64 bits, para generar enlaces)
* Tu nombre de usuario (para efectos de visualización)
* Tu avatar (para efectos de visualización)
* Tu **[token de intercambio](https://steamcommunity.com/my/tradeoffers/privacy)** (para que las personas fuera de tu lista de amigos puedan enviarte intercambios)
* Tus `MatchableTypes` (para efectos de visualización y emparejamiento)
* El valor de `MatchEverything` en tus `TradingPreferences` (para efectos de visualización y emparejamiento)
* El número total de artículos `MatchableTypes` en tu inventario (para efectos de visualización y emparejamiento)
* El número total de juegos de los que provienen los artículos `MatchableTypes` mencionados antes (para efectos de visualización y emparejamiento)

ASF **no** recolectará ninguna otra información no enlistada arriba sin el previo aviso en el registro de cambios, y sin una buena razón práctica en primer lugar. No consideramos que nada de lo anterior sea un asunto serio, ya que lo mencionamos para que sepas exactamente lo que hace ASF además de aquello para lo que tú mismo lo configuraste, así la gente puede entender mejor nuestro punto de vista.

* * *

# Uso de la información

Todos los valores especificados en el segundo punto son usados para nuestra **lista pública ASF STM**, la que se explica a continuación. No utilizamos ninguna otra información para ningún propósito.

* * *

## Lista pública ASF STM

Nuestra lista pública ASF STM está ubicada en **[nuestro sitio web](https://asf.justarchi.net/STM)** y es usada como un servicio público tanto para usuarios de ASF que hacen uso de `MatchActively`, así como para usuarios y no usuarios de ASF para emparejamiento manual.

### Cómo funciona exactamente

ASF envía información inicial después de iniciar sesión, que contiene todas las propiedades de las que hace uso la lista pública. Luego, cada 10 minutos ASF envía una pequeña solicitud "latido" que notifica a nuestro servidor que el bot todavía está funcionando. Si por alguna razón el latido no llega, por ejemplo debido a problemas de red, entonces ASF intentará enviarlo cada minuto, hasta que el servidor lo registre.

Esto permite a nuestro sitio web registrar qué cuentas pueden ser usadas para emparejamiento, así como para registrar si todavía están activas. Gracias a eso, nuestro sitio web puede mostrar todas las cuentas ASF 2FA+STM que estaban activas en los **últimos 15 minutos**.

Los usuarios se ordenan de acuerdo a sus inventarios (en orden descendente) - bots con `MatchEverything` con la etiqueta `Any` que aceptan todos los intercambios 1:1, luego por conteo de juegos únicos `MatchableTypes`, y finalmente por conteo de artículos `MatchableTypes`.

Te en cuenta que **no** serás mostrado en el sitio web si no cumples con todos los requisitos. En este caso ASF ni siquiera se tomará la molestia de comunicarse con nuestro servidor, así que el segundo punto se omite completamente para ti si intencionalmente no habilitaste `SteamTradeMatcher` para ayudarte a emparejar duplicados. Además la lista pública es compatible solo con la última versión estable de ASF y puede negarse a mostrar bots desactualizados, especialmente si les falta alguna funcionalidad central que solo se puede encontrar en las versiones más recientes.

### API

La lista ASF STM solo acepta bots de ASF por el momento. No hay forma de listar bots de terceros por ahora (ya que no podemos revisar fácilmente su código y asegurarnos que cumplen con nuestra lógica de intercambio).

Si buscas una forma sencilla de acceder a nuestro listado de manera programática, tenemos un endpoint **[/Api/Bots](https://asf.justarchi.net/Api/Bots)** muy simple que puedes usar. Este es también el endpoint que ASF usa internamente para los usuarios de `MatchActively`.

### Aviso

*Todo el concepto, junto con la integración web y los reportes de ASF todavía están en beta - puede ser mejorado/cambiado con el tiempo - también eliminado si sentimos que no hay suficiente interés por esta característica.*

* * *

## No participar

Participar en las estadísticas **no es obligatorio**, aunque se anima a hacerlo por el futuro del programa. No te juzgamos, y si tienes la necesidad de ocultar el hecho de que usas ASF entonces puedes deshabilitar las estadísticas **completamente** cambiando la propiedad de configuración global `Statistics` a `false`. Con las estadísticas deshabilitadas todo el módulo se vuelve no operativo, y no hará ninguna de las acciones especificadas en nuestra política de privacidad mencionada antes.

Desactivar las estadísticas puede influir **nuestro soporte técnico, funcionalidades de ASF y otras cosas que se te ofrecen gratuitamente**. Por ejemplo, no puedes hacer uso de `MatchActively` sin tener `Statistics` habilitado (ya que no quieres comunicarte con nuestro servidor para que la funcionalidad trabaje).
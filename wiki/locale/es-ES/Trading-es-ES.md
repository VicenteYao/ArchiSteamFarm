# Intercambios

ASF incluye soporte para intercambios de Steam no interactivos (offline). Tanto recibir (aceptar/rechazar) como enviar intercambios está disponible inmediatamente y no requieres una configuración especial, pero obviamente se requiere una cuenta de Steam sin restricciones (aquella que ya ha gastado 5$ en la tienda). El módulo de intercambios no está disponible para cuentas limitadas.

* * *

## Lógica

ASF siempre aceptará todos los intercambios, independientemente de los artículos, enviados por el usuario con acceso `Master` (o superior) al bot. Esto permite no solo "lootear" fácilmente los cromos recolectados por el bot, sino también administrar fácilmente los artículos que el bot almacena en el inventario.

ASF rechazará ofertas de intercambio, independientemente del contenido, de cualquier usuario (no master) que esté bloqueado del módulo de intercambios. La lista negra de usuarios bloqueados se almacena en la base de datos estándar `BotName.db`, y puede ser administrada por medio de los **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `bl`, `bladd` y `blrm`. Esto debería funcionar como una alternativa al bloqueo de usuarios que ofrece Steam - usar con precaución.

ASF aceptará todos los intercambios de `loot` enviados entre bots, a menos que `DontAcceptBotTrades` se especifique en `TradingPreferences`. En resumen, las `TradingPreferences` por defecto de `None` hará que ASF acepte automáticamente intercambios del usuario con acceso `Master` al bot (explicado arriba), así como todos los intercambios de donación de otros bots que formen parte del proceso de ASF. Si quieres deshabilitar los intercambios de donación de otros bots, entonces para eso es `DontAcceptBotTrades` en tus `TradingPreferences`.

Cuando habilitas `AcceptDonations` en tus `TradingPreferences`, ASF también aceptará cualquier intercambio de donación - un intercambio en el que la cuenta bot no pierde ningún artículo. Esta propiedad solo afecta cuentas no bot, ya que las cuentas bot son afectadas por `DontAcceptBotTrades`. `AcceptDonations` te permite aceptar fácilmente donaciones de otras personas, y también de bots que no formen parte del proceso de ASF.

Es agradable notar que `AcceptDonations` no requiere **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-es-es)**, puesto que no se requiere confirmación si no estamos perdiendo ningún artículo.

También puedes personalizar aún más las capacidades de intercambio de ASF modificando `TradingPreferences` como corresponde. Una de las características principales de `TradingPreferences` es la opción `SteamTradeMatcher` que hará que ASF use su lógica integrada para aceptar intercambios que te ayuden a completar insignias faltantes, lo que es especialmente útil en cooperación con la lista pública de **[SteamTradeMatcher](https://www.steamtradematcher.com)**, pero también puede funcionar sin ella. Se describe con detalle abajo.

* * *

## `SteamTradeMatcher`

Cuando `SteamTradeMatcher` está activo, ASF usará un algoritmo bastante complejo para comprobar si el intercambio pasa las reglas de STM y que al menos sea neutral para nosotros. La lógica es la siguiente:

- Rechazará el intercambio si estamos perdiendo cualquier cosa además de los tipos de artículo especificados en `MatchableTypes`.
- Rechazará el intercambio si no estamos recibiendo al menos el mismo número de artículos por juego y por tipo.
- Rechazará el intercambio si el usuario solicita cromos especiales de las ofertas de verano/invierno, y tiene una retención de intercambio.
- Rechazará el intercambio si la duración de la retención de intercambio excede lo establecido en la propiedad de configuración global `MaxTradeHoldDuration`.
- Rechazará el intercambio si no tenemos establecido `MatchEverything`, y es peor que neutral para nosotros.
- Aceptará el intercambio si no lo rechazamos por ninguno de los puntos anteriores.

Es agradable notar que ASF también soporta sobrepagos - la lógica funcionará correctamente cuando el usuario añada algo extra al intercambio, siempre y cuando se cumplan todas las condiciones anteriores.

Los primeros 4 predicados de rechazo deberían ser obvios para todos. El último incluye lógica de duplicados que comprueba el estado actual de nuestro inventario y decide cuál es el estatus del intercambio.

- Un intercambio es **bueno** si nuestro progreso hacia completar el set avanza. A A (antes) <-> A B (después)
- Un intercambio es **neutral** si nuestro progreso hacia completar el set se mantiene intacto. A B (antes) <-> A C (después)
- Un intercambio es **malo** si nuestro progreso hacia completar el set retrocede. A C (antes) <-> A A (después)

STM solo opera con intercambios buenos, lo que significa que un usuario utilizando STM para emparejar duplicados siempre debería sugerirnos intercambios buenos. Sin embargo, ASF es liberal, y también acepta intercambios neutrales, porque en esos intercambios realmente no estamos perdiendo nada, así que hay una razón real para rechazarlos. Esto es especialmente útil para tus amigos, ya que pueden cambiar tus cromos excesivos sin usar STM en absoluto, siempre y cuando no pierdas tu progreso hacia el set.

Por defecto ASF rechazará intercambios malos - como usuario, esto es lo que querrás casi siempre. Sin embargo, puedes habilitar opcionalmente `MatchEverything` en tus `TradingPreferences` para hacer que ASF acepte todos los intercambios de duplicados, incluyendo los **malos**. Esto es útil si quieres manejar un bot de intercambio 1:1 en tu cuenta, por lo que entiendes que **ASF ya no te ayudará en el progreso para completar las insignias, y que podrías perder sets completos por N cantidad de duplicados del mismo cromo**. A menos que intencionalmente quieras manejar un bot de intercambio que **nunca** se supone que termine ningún set, no quieres habilitar esta opción.

Independientemente de tus `TradingPreferences` seleccionadas, que un intercambio sea rechazado por ASF no significa que tú no puedas aceptarlo. Si dejaste el valor predeterminado de `BotBehaviour`, el cual no incluye `RejectInvalidTrades`, ASF simplemente ignorará esos intercambios - permitiéndote decidir si estás interesado en ellos o no. Lo mismo ocurre con los intercambios con artículos fuera de `MatchableTypes`, así como con todo lo demás - se supone que el módulo te ayude a automatizar intercambios de STM, no decidir cuál es un intercambio bueno y cuál no. La única excepción a esta regla es cuando se trata de usuarios que pusiste en la lista negra del módulo de intercambios usando el comando `bladd` - los intercambios de esos usuarios son rechazados inmediatamente, independientemente de la configuración de `BotBehaviour`.

Es altamente recomendable usar **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-es-es)** cuando habilitas esta opción, ya que esta función pierde todo su potencial si decides confirmar manualmente cada intercambio. `SteamTradeMatcher` funcionará correctamente incluso sin la capacidad de confirmar intercambios, pero puede generar un atraso en las confirmaciones si no las aceptas a tiempo.

* * *

### `MatchActively`

El ajuste `MatchActively` es una versión extendida de `SteamTradeMatcher` que además del emparejamiento pasivo de esa opción, también incluye emparejamiento activo en el que el bot enviará intercambios a otras personas.

Para hacer uso de esa opción, tienes que cumplir ciertos requisitos. Primero, necesitas habilitar `SteamTradeMatcher` (puesto que esta característica es una extensión de aquella), y asegurarte que tienes `MatchEverything` **deshabilitado** (ya que los bots de intercambio nunca emparejan activamente). Después, tienes que ser elegible para nuestra **[lista ASF STM](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-es-es#pol%C3%ADtica-de-privacidad-actual)**, con requisitos más relajados. Al menos debes tener `Statistics` habilitado, una cuenta **[ilimitada](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663)**, **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-es-es#asf-2fa)** activo y por lo menos un tipo válido en `MatchableTypes`, tal como cromos.

Si cumples todos los requisitos anteriores, ASF se comunicará periódicamente con nuestra **[lista pública de ASF STM](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-es-es#lista-p%C3%BAblica-asf-stm)** para emparejar activamente bots que estén disponibles.

- Cada emparejamiento se compone de "rondas", con hasta `10` siendo el máximo en una sesión de emparejamiento.
- En cada ronda ASF analizará nuestro inventario y el inventario de los bots seleccionados que estén listados para encontrar artículos `MatchableTypes` que pueden ser emparejados. Si se encuentra una coincidencia, ASF enviará y confirmará automáticamente la oferta de intercambio.
- Cada set (composición de appID, tipo y rareza del objeto) puede ser emparejado en una ronda solo una vez. Esto se implementa para minimizar "los artículos ya no están disponibles para intercambiar" y evitar la necesidad de esperar a que cada bot reaccione antes de enviar todos los intercambios. También es la razón principal por la que el emparejamiento se compone de rondas y no de un proceso continuo.
- ASF no enviará más de `255` artículos en un solo intercambio, y no más de `5` intercambios al mismo usuario en una sola ronda. Esto es impuesto por los límites de Steam, así como por nuestro propio equilibrio de carga.
- La ronda de emparejamiento termina cuando intentamos emparejar un total de `40` bots, si no es cancelado antes por haberse quedado sin sets para emparejar o debido a una cantidad excesiva de emparejamientos vacíos.
- Si la última ronda de emparejamiento termina con al menos un intercambio enviado, la siguiente ronda comienza dentro de `5` minutos desde la última (para añadir un tiempo de espera y permitir que todos los bots reaccionen a nuestros intercambios), de lo contrario la sesión de emparejamiento termina y se repite en `8` horas.

Se supone que este módulo sea transparente. El emparejamiento comenzará en aproximadamente `1` hora desde el inicio de ASF, y se repetirá cada `8` horas (si es necesario). La función `MatchActively` está diseñada para ser usada como una medida periódica a largo plazo, para asegurar que avanzamos activamente hacia completar sets, pero la sin presión a corto plazo de tiempo y recursos que sucedería si esto se ofreciera como un comando. Los usuarios objetivo de este módulo son cuentas principales y cuentas alternas para "almacenar", aunque puede ser usado por cualquier bot que no esté configurado a `MatchEverything`.

ASF hará todo lo posible para minimizar la cantidad de solicitudes y presión generada por usar esta opción, al mismo tiempo que maximizará la eficiencia del emparejamiento al límite superior. El algoritmo exacto para elegir bots para emparejar es el detalle de implementación de ASF, pero ahora mismo ASF tiende a favorecer bots con mejor diversidad de juegos de los que provienen sus objetos, con preferencia por los bots con `Any`.

`MatchActively` toma en cuenta los bots que bloqueaste del intercambio a través del **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `bladd` y no intentará emparejar activamente con ellos. Esto puede ser usado para decirle a ASF con qué bots nunca debería emparejar, incluso si tienen posibles duplicados que nos pudieran servir.
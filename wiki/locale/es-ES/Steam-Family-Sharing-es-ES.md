# Préstamo familiar de Steam

ASF soporta el Préstamo Familiar de Steam desde la versión 2.1.5.5+. Para entender cómo funciona ASF con eso, primero debes leer cómo **[funciona el Préstamo Familiar de Steam](https://store.steampowered.com/promotion/familysharing)**, que está disponible en la tienda de Steam.

* * *

## ASF

El soporte para el Préstamo Familiar de Steam en ASF es transparente, esto significa que no introduce ninguna propiedad de configuración nueva para el bot/proceso - funciona inmediatamente como una funcionalidad integrada.

ASF incluye la lógica apropiada para estar consciente de la biblioteca bloqueada por usuarios del préstamo familiar, por lo tanto no los "expulsará" de su sesión de juego por ejecutar un juego. ASF actuará exactamente igual como si la cuenta primaria estuviese jugando, por lo tanto si el estado de jugando es fijado por tu cliente de Steam, o por uno de los usuarios de tu préstamo familiar, ASF no intentará recolectar, en cambio, esperará a que se libere.

Además de lo anterior, después de iniciar sesión, ASF accederá a tu **[configuración del préstamo familiar](https://store.steampowered.com/account/managedevices)**, de donde extraerá hasta 5 `steamIDs` permitidas para usar tu biblioteca. A esos usuarios se les asignan permisos `FamilySharing` para usar **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)**, especialmente permitiéndoles usar el comando `pause~` en la cuenta bot que les está compartiendo juegos, lo que permite pausar el módulo de recolección automática de cromos para ejecutar un juego que puede ser compartido.

Conectar ambas funcionalidades descritas arriba permite a tus amigos `pause~` pausar tu proceso de recolección de cromos, iniciar un juego, jugar siempre que lo deseen, cuando hayan terminado de jugar, el proceso de recolección de cromos será reanudado automáticamente por ASF. Por supuesto, enviar el comando `pause~` no es necesario si ASF no está recolectando nada activamente, porque tus amigos pueden ejecutar el juego directamente, y la lógica descrita antes asegura que no serán expulsados de la sesión.

* * *

## Limitaciones

A la red de Steam le gusta engañar a ASF transmitiendo actualizaciones de estado falsas, lo que puede conducir a que ASF crea que está bien reanudar el proceso, y en consecuencia expulsar a tu amigo demasiado pronto. Este es exactamente el mismo problema que el ya explicado en **[esta entrada de las preguntas frecuentes](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-es-es#asf-est%C3%A1-expulsando-mi-sesi%C3%B3n-en-el-cliente-de-steam-mientras-estoy-jugando--esta-cuenta-tiene-iniciada-una-sesi%C3%B3n-en-otro-equipo)**. Recomendamos exactamente las mismas soluciones, principalmente ascender a tu amigo al permiso `Operator` (o superior) y decirle que use los comandos `pause` y `resume`.
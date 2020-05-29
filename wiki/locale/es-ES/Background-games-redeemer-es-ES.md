# Activador de juegos en segundo plano

El activador de juegos en segundo plano es una función especial de ASF que permite importar una lista de claves de producto (junto con sus nombres) para ser activados en segundo plano. Esto es especialmente útil si tienes una gran cantidad de claves para activar y es seguro que alcanzarás el **[estatus](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-es-es#cu%C3%A1l-es-el-significado-de-estatus-al-activar-una-clave)** `RateLimited` antes de que termines con tu lote entero.

El activador de juegos en segundo plano está hecho para usarse en un solo bot, lo que significa que no hace uso de `RedeemingPreferences`. Esta función puede usarse junto con (o en vez de) el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `redeem`, de ser necesario.

* * *

## Importar

El proceso de importación puede hacerse de dos maneras - ya sea a través de un archivo o por IPC.

### Archivo

ASF reconocerá en su directorio `config` un archivo llamado `BotName.keys` donde `BotName` es el nombre de tu bot. Se espera que el archivo tenga una estructura definida de nombre de juego con clave de producto, separados entre sí por un carácter de tabulación y que termine con una nueva línea para indicar la siguiente entrada. Si se utilizan varias tabulaciones, entonces la primera entrada es considerada nombre del juego, la última entrada es considerada clave de producto, y todo lo intermedio es ignorado. Por ejemplo:

    POSTAL 2    ABCDE-EFGHJ-IJKLM
    Domino Craft VR 12345-67890-ZXCVB
    A Week of Circus Terror POIUY-KJHGD-QWERT
    Terraria    EstoSeIgnora   EstoSeIgnoraTambién    ZXCVB-ASDFG-QWERT
    

Alternativamente, también puedes usar el formato de solo claves (también con una nueva línea entre cada entrada). En este caso ASF utilizará la respuesta de Steam (si es posible) para rellenar el nombre correcto. Para cualquier tipo de etiquetado, recomendamos que nombres las claves tú mismo, ya que los paquetes activados en Steam no tienen que seguir la lógica de los juegos que están activando, así que dependiendo de lo que el desarrollador haya puesto, puedes ver nombres de juegos correctos, nombres de paquetes personalizados (por ejemplo, Humble Indie Bundle 18) o incorrectos y potencialmente maliciosos (por ejemplo, Half-Life 4).

    ABCDE-EFGHJ-IJKLM
    12345-67890-ZXCVB
    POIUY-KJHGD-QWERT
    ZXCVB-ASDFG-QWERT
    

Independientemente del formato que hayas decidido mantener, ASF importará tu archivo de `keys`, ya sea al iniciar el bot, o luego durante la ejecución. Después de revisar el archivo y eventualmente omitir las entradas inválidas, todos los juegos detectados serán añadidos a una cola en segundo plano, y el archivo `BotName.keys` será removido del directorio `config`.

### IPC

Además de poder usar el archivo antes mencionado, ASF también posee `GamesToRedeemInBackground` **[ASF API endpoint](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-es-es#asf-api)** que se puede ejecutar desde cualquier herramienta IPC, incluyendo nuestro ASF-ui. Usar IPC puede ser más provechoso, ya que puedes usar una sintaxis apropiada, tal como usar un delimitador personalizado en lugar de estar forzado a usar un carácter de tabulación, o incluso introduciendo tu propia estructura de claves personalizada.

* * *

## Cola

Una vez que los juegos sean importados exitosamente, serán añadidos a la cola. ASF analizará automáticamente la cola en segundo plano mientras el bot esté conectado a la red de Steam y la cola no esté vacía. Cuando una key se intente activar y no resulte en `RateLimited`, esta será removida de la cola, con el resultado de dicho intento escrito en un archivo en el directorio `config` - ya sea `BotName.keys.used` si la key fue usada en el proceso (por ejemplo `NoDetail`, `BadActivationCode`, `DuplicateActivationCode`) o `BotName.keys.unused` en caso contrario. ASF usará el nombre que hayas proporcionado, esto debido a que no es posible garantizar que la red de Steam retorne un nombre significativo a la hora de activar una clave - de esta manera puedes etiquetar tus claves usando nombres personalizados en caso de ser necesario o si el usuario así lo desea.

Si durante el proceso la cuenta llega a su `RateLimited`, la cola será temporalmente suspendida por una hora para esperar que el límite de activación desaparezca. Cuando este límite desaparezca, el proceso continuará, hasta que la cola quede completamente vacía.

* * *

## Ejemplo

Supongamos que tienes una lista de 100 claves. Primeramente debes crear un nuevo archivo `BotName.keys.new` en el directorio `config` de ASF. Le agregamos la extensión `.new` para que ASF no intente usar este archivo de inmediato en el momento que es creado (ya que el mismo está en blanco y no está listo para ser importado aún).

Ahora puedes abrir el archivo y pegar las 100 claves dentro del mismo, arreglando el formato de la lista si es necesario. Después de arreglar nuestro archivo `BotName.keys.new` tendremos exactamente 100 (o 101, incluyendo el último salto de línea) líneas, cada uno con un estructura de `GameName\tcd-key\n` donde `\t` es un carácter de tabulación y `\n` es un salto de línea.

Ahora puedes cambiarle el nombre al archivo de `BotName.keys.new` a `BotName.keys` para que ASF sepa que está listo para ser leído. En el momento en que hagas esto, ASF automáticamente importará el archivo (sin necesidad de reiniciar) y posteriormente lo borrará, confirmando que todos los juegos fueron leídos y agregados a la cola.

En vez de usar el archivo `BotName.keys`, también puedes usar el endpoint de la API de ASF, o incluso combinar ambas si así lo desea.

Después de un rato, los archivos `BotName.keys.used` y `BotName.keys.unused` serán generados. Esos archivos contienen el resultado del proceso de activación. Por ejemplo, podrías cambiar el nombre del archivo `BotName.keys.unused` a `BotName2.keys` y pasarle las claves que no fueron usadas a otro bot, ya que el anterior no las utilizó. O simplemente puedes copiar y pegar las claves sin usar a algún otro archivo y guardarlas para después, como gustes. Ten en cuenta, puesto que ASF recorre la lista, que las nuevas entradas se añadirán a nuestros archivos `used` y `unused`, por lo tanto se recomienda esperar a que la lista quede completamente vacía antes de hacer uso de los archivos. Si debes acceder a estos archivos antes de que la lista esté completamente vacía, primero debes **mover** el archivo a otro directorio, **luego** leerlo. Esto es porque ASF puede añadir nuevos resultados mientras estás usándolos, y eso podría ocasionar la pérdida potencial de algunas claves si abres un archivo que tenga, por ejemplo, 3 claves, y luego lo eliminas, ignorando el hecho de que mientras tanto ASF añadió 4 claves más a tu archivo eliminado. Si quieres acceder a esos archivos, asegúrate de moverlos fuera del directorio `config` antes de abrirlos, por ejemplo, al renombrarlos.

También es posible añadir juegos adicionales para importar aun ya teniendo juegos en cola, repitiendo todos los pasos anteriores. ASF añadirá correctamente nuestras entradas adicionales a una cola ya en curso y las tratará eventualmente.

* * *

## Observaciones

El activador de claves en segundo plano usa `OrderedDictionary` bajo el capó, lo que significa que tus claves conservarán el orden especificado en el archivo (o en la llamada IPC API). Esto significa que puedes (y debes) proporcionar una lista en la que una clave dada solo puede tener dependencia directa con claves listadas arriba de ella, pero no debajo. Por ejemplo, esto significa que si tienes el DLC `D` que requiere que primero se active el juego `G`, entonces la clave para el juego `G` **siempre** debe ser incluida antes de la clave para el DLC `D`. De la misma manera, si el DLC `D` depende de `A`, `B` y `C`, entonces los 3 deben ser incluidos antes (en cualquier orden, a menos que tengan dependencia entre ellos).

No seguir el esquema anterior dará como resultado que tu DLC no sea activado con el mensaje `DoesNotOwnRequiredApp`, incluso si tu cuenta fuese elegible para activarlo después de pasar por toda la lista. Si quieres evitar eso, debes asegurarte que tu DLC siempre esté incluido después del juego base en tu lista.
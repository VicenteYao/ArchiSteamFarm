# Seguridad

## SteamPassword

Actualmente ASF soporta 4 tipos de contraseña - `PlainText`, `AES`, `ProtectedDataForCurrentUser` y None (`null` / `""`).

Para utilizar una contraseña cifrada, primero debes iniciar sesión en Steam como de costumbre con `PlainText`, luego generar una contraseña encriptada usando el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es)** `password`. Elige el método de encriptación que quieras, luego introduce la contraseña encriptada en la propiedad de configuración del bot `SteamPassword`, y finalmente no olvides cambiar `PasswordFormat` al que corresponda con tu método de encriptación seleccionado.

* * *

### PlainText

Esta es la forma más sencilla e insegura de almacenar la contraseña, definida como `PasswordFormat` de `0`. ASF espera que la propiedad `SteamPassword` sea texto plano - la contraseña usada para iniciar sesión en Steam en su forma directa. Es la más fácil de usar, y 100% compatible con todas las configuraciones, por lo tanto es la predeterminada.

* * *

### AES

Considerado seguro para los estándares actuales, la forma **[AES](https://es.wikipedia.org/wiki/Advanced_Encryption_Standard)** de almacenar la contraseña es definida como `PasswordFormat` de `1`. ASF espera que la propiedad `SteamPassword` sea una secuencia de caracteres **[base64-encoded](https://es.wikipedia.org/wiki/Base64)** resultante en un byte array encriptado en AES después de la traducción, que luego debe ser desencriptado usando el **[vector de inicialización](https://es.wikipedia.org/wiki/Vector_de_inicializaci%C3%B3n)** incluido y la clave de encriptación de ASF.

El método anterior garantiza la seguridad siempre que el atacante no conozca la clave de encriptación integrada en ASF, la cual es usada tanto para la desencriptación como para la encriptación de contraseñas. ASF te permite especificar la clave a través del **[argumento de la línea de comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments-es-es)** `--cryptkey`, el cual deberías usar para máxima seguridad. Si decides omitirlo, ASF usará su propia clave que es **conocida** y está en el código de la aplicación, lo que significa que cualquiera puede revertir la encriptación de ASF y obtener la contraseña desencriptada. Requiere algo de esfuerzo y no es tan fácil de hacer, pero es posible, por eso casi siempre debes usar la encriptación `AES` con tu propia `--cryptkey` que es mantenida en secreto. El método AES usado en ASF proporciona seguridad que debería ser satisfactoria y tiene un balance entre la simplicidad de `PlainText` y la complejidad de `ProtectedDataForCurrentUser`, pero se recomienda mucho usarlo con una `--cryptkey` personalizada.

* * *

### ProtectedDataForCurrentUser

Actualmente es la forma más segura que ofrece ASF para almacenar la contraseña, y mucho más segura que el método `AES` explicado anteriormente, se define como `PasswordFormat` de `2`. La ventaja principal de este método es al mismo tiempo la mayor desventaja - en lugar de usar una clave de encriptación (como en `AES`), la información es encriptada usando las credenciales de inicio de sesión del usuario que actualmente tiene la sesión iniciada, lo que significa que es posible desencriptar la información **solo** en la máquina en la que fue encriptada, y además **solo** por el usuario que hizo la encriptación. Esto asegura que incluso si envías todo tu `Bot.json` a alguien más, no será capaz de desencriptar la contraseña sin acceso a tu PC. Esta es una excelente medida de seguridad, pero al mismo tiempo tiene una gran desventaja, ya que la contraseña encriptada usando este método no será compatible con ningún otro usuario ni otra máquina - incluyendo **la tuya** si decides, por ejemplo, reinstalar tu sistema operativo. Aún así, es uno de los mejores método para almacenar contraseñas, y si te preocupa la seguridad de `PlainText`, y no quieres poner tu contraseña cada vez con la opción `None`, entonces esta es tu mejor apuesta siempre que no tengas que acceder a tus configuraciones desde otra máquina que no sea la tuya.

**Por favor, ten en cuenta que esta opción está disponible solo para máquinas que ejecuten el sistema operativo Windows.**

* * *

### None

La única forma que garantiza seguridad al 100% y asegura que nadie pueda robar tu contraseña de Steam. Para usar esta opción simplemente establece tu `SteamPassword` para que sea una cadena vacía (`""`) o el valor `null`. ASF pedirá tu contraseña de Steam cuando sea necesario, y no la guardará en ningún lugar, solo la mantendrá en la memoria del proceso actual, hasta que lo cierres. Mientras que es el método más seguro de tratar con contraseñas, también es el más problemático ya que necesitas introducir tu contraseña manualmente en cada ejecución de ASF (cuando sea requerido). Si eso no es un problema para ti, esta es tu mejor apuesta en lo que se refiere a seguridad.

* * *

## Recomendación

Si la compatibilidad no es un problema para ti, y estás bien con cómo funciona el método `ProtectedDataForCurrentUser`, es la opción **recomendada** para almacenar la contraseña en ASF, ya que proporciona la mejor seguridad. El método `AES` es una buena opción para las personas que quieren usar sus configuraciones en cualquiera máquina que quieran, mientras que `PlainText` es la forma más simple de almacenar la contraseña, si no te importa que cualquiera pueda buscarla en la configuración JSON.

Por favor, ten en cuenta que los 3 se consideran **insecure** si el atacante tiene acceso a tu PC. ASF debe poder desencriptar la contraseña encriptada, y si el programa que se ejecuta en tu máquina puede hacer eso, entonces cualquier programa que se ejecute en la misma máquina será capaz de hacerlo también. `ProtectedDataForCurrentUser` es la variante más segura ya que **incluso otro usuario usando la misma PC no podrá desencriptarla**, pero aún es posible desencriptar la información si alguien es capaz de robar tus credenciales de acceso e información de tu máquina además del archivo de configuración de ASF.

Para las personas que raramente ejecutan ASF o aquellas que no les molesta introducir la contraseña en cada inicio de ASF, la forma `None` es la más segura ya que la contraseña de Steam no se guarda en ninguna parte.

* * *

# Desencriptación

ASF no soporta ninguna forma de desencriptar contraseñas ya encriptadas, ya que los métodos de desencriptación solo son usados internamente para acceder a la información dentro del proceso. Si quieres revertir el procedimiento de encriptación, por ejemplo, para mover ASF a otra máquina cuando usas `ProtectedDataForCurrentUser`, entonces simplemente cambia tu `PasswordFormat` de nuevo a `0` (PlainText), y llena `SteamPassword` apropiadamente. Entonces puedes iniciar ASF como de costumbre, y repite el procedimiento desde el principio.
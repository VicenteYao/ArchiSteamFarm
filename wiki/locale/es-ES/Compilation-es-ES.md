# Compilación

La compilación es el proceso de crear un archivo ejecutable. Esto es lo que quieres hacer si quieres añadir tus propios cambios a ASF, o si por cualquier razón no confías en los archivos ejecutables proporcionados en las **[versiones](https://github.com/JustArchiNET/ArchiSteamFarm/releases)** oficiales. Si eres usuario y no desarrollador, lo más probable es que quieras usar ejecutables ya compilados, pero si quieres usar los tuyos propios, a aprender algo nuevo, continua leyendo.

ASF puede ser compilado en cualquier plataforma actualmente soportada, siempre que tengas todas las herramientas para hacerlo.

* * *

## .NET Core SDK

Independientemente de la plataforma, necesita .NET Core SDK completo (no solo runtime) para compilar ASF. Las instrucciones de instalación se pueden encontrar en la **[página de instalación de .NET Core](https://dotnet.microsoft.com/download)**. Necesitas instalar la versión apropiada de .NET Core SDK para tu sistema operativo. Después de una instalación exitosa, el comando `dotnet` debería estar funcionando y operativo. Puedes verificar si funciona con `dotnet --info`. También asegúrate de que tu .NET Core SDK coincida con los **[requisitos de runtime](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-es-es#requisitos-de-runtime)** de ASF.

* * *

## Compilación

Suponiendo que tienes .NET Core SDK operativo y en la versión apropiada, simplemente navega al directorio fuente de ASF (repositorio de AF clonado o descargad y desempaquetado) y ejecuta:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/generic"
```

Si usas Linux/OS X, en su lugar puedes usar el script `cc.sh` que hará lo mismo, de una forma un poco más compleja.

Si la compilación terminó con éxito, podrás encontrar ASF en su variante `source` en el directorio `out/generic`. Esto es lo mismo que la compilación `generic` oficial de ASF, pero está forzado a `UpdateChannel` y `UpdatePeriod` de `0`, lo que es apropiado para autocompilaciones.

### Sistema operativo específico

Si tienes una necesidad específica también puedes generar un paquete .NET Core específico de un sistema operativo. En general no deberías hacer eso porque acabas de compilar la variante `generic` que puedes ejecutar con el ya instalado .NET Core runtime que usaste para la compilación en primer lugar, pero en caso de que lo quieras hacer:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/linux-x64" -r "linux-x64"
```

Por supuesto, reemplaza `linux-x64` con la arquitectura del sistema operativo que tienes por objetivo, como `win-x64`. Esta compilación también tendrá las actualizaciones deshabilitadas.

### .NET Framework

En un caso muy raro en el que quieras compilar un paquete `generic-netf`, puedes cambiar framework objetivo de `netcoreapp3.1` a `net48`. Ten en cuenta que necesitarás el paquete de desarrollador **[.NET Framework](https://dotnet.microsoft.com/download/visual-studio-sdks)** adecuado para compilar la variante `netf`m además de .NET Core SDK, así que lo siguiente solo funcionará en Windows:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "net48" -o "out/generic-netf"
```

En caso de no poder instalar .NET Framework o incluso .NET Core SDK (por ejemplo, por compilar en `linux-x86` con `mono`), puedes llamar directamente `msbuild`. También necesitarás especificar `ASFNetFramework` manualmente, ya que ASF por defecto desactiva la compilación netf en plataformas que no son Windows:

```shell
msbuild /m /r /t:Publish /p:Configuration=Release /p:TargetFramework=net48 /p:PublishDir=out/generic-netf /p:ASFNetFramework=true ArchiSteamFarm
```

* * *

## Desarrollo

Si quieres editar el código de ASF, puedes usar cualquier IDE (entorno de desarrollo integrado) para ese propósito, aunque eso es opcional, ya que también puedes editarlo con un bloc de notas y compilarlo con el comando `dotnet` descrito arriba. Aún así, para Windows recomendamos el **[Visual Studio más reciente](https://visualstudio.microsoft.com/downloads)** (la versión free community es más que suficiente). También sugerimos usarlo junto con **[ReSharper](https://www.jetbrains.com/resharper)** (opcionalmente), aunque no es un producto gratuito.

Si quieres trabajar con el código de ASF en Linux/OS X, recomendamos el **[Visual Studio Code más reciente](https://code.visualstudio.com/download)**. No es tan rico como Visual Studio clásico, pero es bastante bueno.

Claro, todas las sugerencias anteriores son solo recomendaciones, puedes usar lo que desees, de todas forma se reduce al comando `dotnet build`. Usamos Visual Studio + ReSharper para el desarrollo de AFS, con una pequeña parte de `tools` herramientas de terceros que puedes encontrar en el repositorio.

* * *

## Etiquetas

La rama `master` no garantiza estar en un estado que permita una compilación exitosa o una ejecución de ASF sin fallas, ya que es una rama de desarrollo como indicamos en nuestro **[ciclo de lanzamiento](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle-es-es)**. Si quieres compilar o referenciar ASF desde la fuente, entonces debes usar la **[etiqueta](https://github.com/JustArchiNET/ArchiSteamFarm/tags)** apropiada para ese fin, lo que garantiza una compilación exitosa, y muy probablemente también una ejecución sin fallas (si la compilación fue marcada como versión estable). Para comprobar la "salud" actual del árbol, puedes usar nuestras CI (integración continua) - **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** o **[Travis](https://travis-ci.com/JustArchiNET/ArchiSteamFarm)**.

* * *

## Versiones oficiales

Las versiones oficiales de ASF son compiladas por **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** en Windows, con el último .NET Core SDK que coincida con los **[requisitos de runtime](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-es-es#requisitos-de-runtime)** de ASF. Después de pasar las pruebas, todos los paquetes se despliegan en GitHub. Esto también garantiza transparencia, ya que AppVeyor siempre usa una fuente pública oficial para todas las compilaciones, y puedes comprar las sumas de verificación de los artefactos de AppVeyor con los activos de GitHub. Los desarrolladores de ASF no compilan ni publican versiones por ellos mismos, excepto para el proceso de desarrollo privado y depuración.
# Compatibilidad

ASF es una aplicación C# que se ejecuta en la plataforma .NET Core. Esto significa que ASF no se compila directamente en el **[código de máquina](https://es.wikipedia.org/wiki/Lenguaje_de_m%C3%A1quina)** que se ejecuta en tu PC, sino en **[CIL](https://es.wikipedia.org/wiki/Common_Intermediate_Language)** que requiere tiempo de ejecución compatile con CIL para ejecutarse.

Este enfoque tiene una enorme cantidad de ventajas, ya que CIL es independiente de las plataformas, por lo que ASF puede ejecutarse nativamente en muchos sistemas operativos, especialmente Windows, Linux y OS X. No solo no es necesaria la emulación, sino también hay soporte para todas las optimizaciones relacionadas con plataformas y hardware, tal como instrucciones CPU SSE. Gracias a eso, ASF puede lograr rendimiento y optimización superiores, mientras que ofrece una perfecta compatibilidad y fiabilidad.

Esto también significa que ASF **no tiene requisitos específicos de sistema operativo**, porque requiere **runtime** funcionando en ese sistema operativo y no el sistema operativo en sí. Siempre que runtime ejecute correctamente el código de ASF, no importa si el sistema operativo subyacente es Windows, Linux, OS X, BSD, Sony Playstation 4, Nintendo Wii o tu tostadora - siempre que exista **[.NET Core para el dispositivo](https://github.com/dotnet/core-setup#daily-builds)**, también hay **[ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** para este.

Sin embargo, independientemente de dónde ejecutes ASF, debes asegurarte que tu plataforma objetivo tiene los **[prerrequisitos .NET Core](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)** instalados. Estas son bibliotecas de bajo nivel requeridas para la correcta funcionalidad de runtime y absolutamente necesarias para que ASF funcione en primer lugar. Muy probablemente puedes tener algunas (o incluso todas) ya instaladas.

* * *

## Múltiples instancias

ASF es compatible con ejecutar múltiples instancias del proceso en la misma máquina. Las instancias pueden ser completamente independientes o derivadas de la ubicación del mismo ejecutable (en cuyo caso, querrás ejecutarlas con un diferente **[argumento de la línea de comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-es-es)** `--path`).

Al ejecutar múltiples instancias del mismo ejecutable, ten en cuenta que normalmente debes deshabilitar las actualizaciones automáticas en todas sus configuraciones, ya que no hay sincronización entre ellas en lo que se refiere a las actualizaciones automáticas. Si quieres dejar las actualizaciones automáticas activadas, recomendamos instancias independientes, pero aún puedes hacer que las actualizaciones funcionen, siempre y cuando te asegures de que todas las demás instancias de ASF están cerradas.

ASF hará lo posible para mantener una cantidad mínima de comunicación entre procesos del sistema operativo con otras instancias de ASF. Esto incluye que ASF compruebe su directorio de configuración contra otras instancias, así como compartir limitadores en el proceso configurados con las **[propiedades de configuración global](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es#configuraci%C3%B3n-global)** `*LimiterDelay`, asegurando que ejecutar múltiples instancias ASF no causará la posibilidad de encontrarse con un problema de límite de tarifa. En cuanto a aspectos técnicos, las plataformas Windows utilizan semáforos nativos del sistema operativo para este propósito, las plataformas Unix usan un mecanismo para evitar bloqueos basado en archivos de ASF creado en el directorio `/tmp/ASF`.

No es necesario que las instancias de ASF compartan las mismas propiedades `*LimiterDelay`, pueden usar diferentes valores, ya que cada ASF añadirá su propio retraso configurado. Si el `*LimiterDelay` configurado se establece en `0`, la instancia de ASF omitirá esperar para el bloqueo de un recurso dado que se comparte con otras instancias (que potencialmente aún podrían mantener un bloqueo compartido entre sí). Cuando se establece en cualquier otro valor, ASF se sincronizará correctamente con otras instancias y esperará su turno, luego libera el bloqueo después del retraso configurado, permitiendo que otras continúen.

ASF toma en cuenta la configuración de `WebProxy` al decidir sobre el ámbito compartido, lo que significa que dos instancias de ASF que usan diferentes configuraciones `WebProxy` no compartirán sus limitadores entre sí. Esto se implementa para permitir que las configuraciones `WebProxy` funcionen sin retrasos excesivos, como se espera de diferentes interfaces de red. This should be good enough for majority of use cases, however, if you have a specific custom setup in which you're e.g. routing requests yourself in a different way, you can specify network group yourself through `--network-group` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**, which will allow you to declare ASF group that will be synchronized with this instance. Keep in mind that custom network groups are used exclusively, which means that ASF will no longer use `WebProxy` for determining the right group, as you're in charge of grouping in this case.

* * *

## Paquetes de ASF

ASF viene en 2 sabores principales - paquete genérico y específico de SO. En cuanto a funcionalidad ambos paquetes son exactamente iguales, ambos son capaces de actualizarse automáticamente. La única diferencia entre ellos es si el paquete **genérico** de ASF también viene con runtime **específico de SO** para impulsarlo.

* * *

### Genérico

El paquete genérico es una compilación que no depende de plataformas y no incluye ningún código de máquina específico. Esta configuración requiere que tengas .NET Core runtime instalado en su sistema operativo **en su versión apropiada**. Todos sabemos lo problemático que es mantener las dependencias actualizadas, por lo tanto este paquete está aquí principalmente para las personas que **ya usan** .Net Core y no quiere duplicar su runtime solo por ASF si pueden hacer uso de lo que ya tienen instalado. El paquete genérico también te permite ejecutar ASF **en cualquier lugar donde puedas obtener una implementación funcional de .NET Core runtime**, independientemente de si existe o no una compilación de ASF específica para el sistema operativo.

No se recomienda usar el sabor genérico si eres un usuario casual o incluso avanzado que solo quiere hacer funcionar ASF y no entrar en detalles técnicos de .NET Core. En otras palabras - si sabes que es esto, lo puedes usar, de lo contrario es mucho mejor usar un paquete específico de SO.

#### Paquete .NET Framework

Además del paquete genérico mencionado antes, también hay un paquete `generic-netf` construido en .NET Framework (no .NET Core). Este paquete es una variante legacy que proporciona compatibilidad faltante conocida de los tiempos de ASF V2, y puede ejecutarse, por ejemplo, con **[Mono](https://www.mono-project.com)**, mientras que el paquete `generic` de .NET Core no puede hacer eso al día de hoy.

En general, deberías **evitar este paquete como sea posible**, ya que la mayoría de los sistemas operativos y configuraciones están perfectamente (y mucho mejor) soportados con el paquete `generic` mencionado antes. De hecho, este paquete tiene sentido usarlo solo en plataformas que no tienen .NET Core runtime funcional, mientras que tienen implementación Mono funcional. Algunos ejemplos de tales plataformas incluyen `linux-x86` (32-bit i386/i686 linux), así como `linux-armel` (tarjetas ARMv6 encontradas, por ejemplo, en Raspberry Pi 0 & 1).

A medida que el tiempo pasa más plataformas son soportadas por .NET Core y hay menos compatibilidad entre .NET Framework y .NET Core, el paquete `generic-netf` será totalmente reemplazado por `generic` en el futuro. Por favor, absténte de usarlo si en su lugar puedes usar cualquier paquete .NET Core, ya que `generic-netf` carece de mucha funcionalidad y compatibilidad en comparación con las versiones .NET Core, y solo será menos funcional mientras pasa el tiempo. Ofrecemos soporte para este paquete **solo** en máquinas que no pueden usar la variante `generic` antes mencionada (por ejemplo, `linux-x86`), y solo con runtime actualizado (por ejemplo, el más reciente Mono).

* * *

### Sistema operativo específico

El paquete específico de SO, aparte del código incluido en el paquete genérico, también incluye código nativo para una plataforma dada. En otras palabras, el paquete de SO específico **ya incluye dentro el adecuado .NET Core runtime**, lo que permite omitir todo el desastre de la instalación y ejecutar ASF directamente. El paquete de SO específico, como puedes adivinar por el nombre, es específico del SO y cada SO requiere su propia versión - por ejemplo, Windows requiere el binario PE32+ `ArchiSteamFarm.exe` mientras que Linux funciona con el binario ELF `ArchiSteamFarm`. Como debes saber, esos dos tipos no son compatibles entre sí.

ASF actualmente está disponible en las siguientes variantes específicas de SO:

- `win-x64` funciona con sistemas operativos Windows de 64 bits. Esto incluye Windows 7 (SP1+), 8.1, 10, Server 2008 R2 (SP1+), 2012, 2012 R2, 2016, así como las futuras versiones.
- `linux-arm` funciona en sistemas operativos ARM-based (ARMv7+) GNU/Linux de 32 bits. Esto incluye plataformas tales como Raspberry Pi 2 (y más recientes) con todos los sistemas operativos GNU/Linux disponibles para ellos (tal como Raspbian), en presentes y futuras versiones. Esta variante no funcionará con arquitecturas ARM antiguas, tal como ARMv6 encontrada en Raspberry Pi 0 y 1, tampoco funcionará con sistemas operativos que no implementen el entorno GNU/Linux requerido (tal como Android).
- `linux-arm64` funciona en sistemas operativos GNU/Linux de 64 bits basados en ARM (ARMv8+). Esto incluye plataformas tales como Raspberry Pi 3 (y más recientes) con todos los sistemas operativos GNU/Linux disponibles para ellos (tal como Debian), en presentes y futuras versiones. Esta variante no funcionará con sistemas operativos de 32 bits que no tengan las bibliotecas de 64 bits requeridas (tal como Raspbian), tampoco funcionará con sistemas operativos que no implementen el entorno GNU/Linux requerido (tal como Android).
- `linux-x64` funciona en sistemas operativos GNU/Linux de 64 bits. Esto incluye Alpine, CentOS/Fedora/RHEL, Debian/Ubuntu/Linux Mint, OpenSUSE/SLES y muchos otros, incluyendo sus derivados, en versiones actuales y futuras.
- `osx-x64` funciona en sistemas operativos OS X de 64 bits. Esto incluye 10.13, así como futuras versiones.

Por supuesto, incluso si no tienes un paquete de SO específico disponible para tu combinación SO-arquitectura, siempre puedes instalar el .NET Core runtime adecuado y ejecutar el sabor genérico de ASF, que también es la razón principal por la que existe en primer lugar. La compilación genérica de ASF no depende de la plataforma y se ejecutará en cualquier plataforma que tenga .NET Core runtime funcional. Es importante notar esto - ASF requiere .NET Core runtime, no un sistema operativo específico o arquitectura. Por ejemplo, si estás ejecutando Windows de 32 bits a pesar no haber una versión `win-x86` de ASF, simplemente puedes instalar .NET Core SDK en la versión `win-x86` y ejecutar ASF genérico sin problemas. Simplemente no podemos apuntar a cada combinación de SO-arquitectura que exista y sea usada por alguien, así que tenemos que trazar la línea en algún punto. x86 es un buen ejemplo de esa línea, ya que es una arquitectura obsoleta al menos desde 2004.

Para una lista completa de todas las plataformas y sistemas operativos soportados por .NET Core 3.1, visita **[notas de lanzamiento](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md)**.

* * *

## Requisitos de runtime

Si estás usando un paquete de SO específico entonces no necesitas preocuparte por los requisitos de runtime, porque ASF siempre se publica con el runtime requerido y actualizado que funcionará correctamente siempre que tengas los **[prerrequisitos de .NET Core](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)** instalados y actualizados. En otras palabras, **no necesitas instalar .NET Core runtime o SDK**, ya que las compilaciones de SO específico solo requieren las dependencias nativas del SO (prerrequisitos) y nada más.

Sin embargo, si estás intentando ejecutar un paquete **genérico** de ASF entonces debes asegurarte de que tu .NET Core runtime soporta la plataforma requerida por ASF.

ASF como programa actualmente tiene como objetivo **.NET Core 3.1** (`netcoreapp3.1`), pero podría apunta a una plataforma más nueva en el futuro. `netcoreapp3.1` es soportado desde 3.1.100 SDK (3.1.0 runtime), aunque ASF está configurado para apuntar al **último runtime al momento de la compilación**, así que debes asegurarte de que tienes el **[último SDK](https://dotnet.microsoft.com/download)** (o al menos runtime) disponible para tu máquina. La variante genérica de ASF podría negarse a ejecutar si tu runtime en más antiguo que el mínimo (objetivo) conocido durante la compilación.

En caso de duda, revisa nuestros **[usos de integración continua](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** para compilar y publicar versiones de ASF en GitHub. Puedes encontrar `dotnet --info` en la parte de arriba de cada compilación.

* * *

## Problemas

### Varios problemas relacionados con el bloqueo al ejecutar ASF en Linux VPS con virtualización OpenVZ

El kernel OpenVZ normalmente se basa en una versión muy antigua del kernel Linux (2.6) que parece ser incompatible con el último .NET Core runtime. Si intentas ejecutar ASF en dicho entorno, podrías encontrar varios problemas de bloqueo, normalmente en forma de congelación del proceso. Revisa el problema relacionado con CoreCLR: https://github.com/dotnet/coreclr/issues/26873

Nuestra recomendación es deshacerse de OpenVZ en favor de soluciones de virtualización mucho mejores, como KVM. El problema descrito aquí es, de hecho, un bug de .NET Core runtime que se supone será **[solucionado](https://github.com/dotnet/coreclr/pull/26912)** en la siguiente actualización de .NET Core runtime (parche de actualización 3.1 ), pero todavía no hay un plazo para ello. Si no puedes cambiar a mejores soluciones de virtualización, puedes considerar ejecutar la variante `generic-netf` de ASF con `mono`, al menos hasta que se libere la nueva versión de runtime.

Para usuarios más avanzados que no le tienen miedo a su sistema operativo Linux, hay disponible una **[solución mucho mejor](https://github.com/dotnet/coreclr/issues/26873#issuecomment-559854433)** que hace posible ejecutar el último .NET Core ASF sin encontrarse con este problema.
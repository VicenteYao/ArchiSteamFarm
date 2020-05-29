# Plugins

A partir de ASF V4, el programa incluye soporte para plugins personalizados que pueden ser cargados durante el tiempo de ejecución. Los plugins permiten personalizar el comportamiento de ASF, por ejemplo añadiendo comandos personalizados, lógica de intercambio personalizada o integración con servicios de terceros y APIs.

* * *

## Para usuarios

ASF carga los plugins desde el directorio `plugins` ubicado en tu carpeta de ASF. Es una práctica recomendada mantener un directorio dedicado para cada plugin que quieras usar, que se puede basar en su nombre, como `MyPlugin`. Hacerlo de este modo resultará en la estructura final de `plugins/MyPlugin`. Finalmente, todos los archivos binarios deben ser colocados dentro de esa carpeta dedicada, y ASF descubrirá y utilizará tu plugin después de reiniciar.

Generalmente los desarrolladores de plugins los publicarán en forma de archivo `zip` con una estructura ya preparada, por lo que basta con desempaquetar el archivo zip en el directorio `plugins`, lo que creará la carpeta apropiada automáticamente.

Si el plugin se cargó con éxito, verás su nombre y versión en el registro. Debes consultar al desarrollador de tu plugin en caso de preguntas, problemas o uso relativo a los plugins que hayas decidido usar.

Puedes encontrar algunos plugins destacados en nuestra sección **[aplicaciones de terceros](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Third-party-es-es#plugins-de-asf)**.

**Por favor, ten en cuenta que los plugins de ASF podrían ser maliciosos**. Siempre debes asegurarte de que usas plugins hechos por desarrolladores en los que confíes. Los desarrolladores de ASF ya no pueden garantizarte los beneficios usuales de ASF (como falta de malware y ser libre de VAC) si decides usar cualquier plugin personalizado. Tampoco podemos dar soporte a configuraciones que utilicen plugins personalizados, dado que ya no estás usando el código original de ASF.

* * *

## Para desarrolladores

Los plugins son bibliotecas .NET estándar que heredan la interfaz común `IPlugin` con ASF. Puedes desarrollar plugins de forma totalmente independiente de la línea principal de ASF y reutilizarlos en la versión actual y futuras de ASF, siempre que la API siga siendo compatible. El sistema de plugins usado en ASF se basa en `System.Composition`, anteriormente conocido como **[Managed Extensibility Framework](https://docs.microsoft.com/dotnet/framework/mef)** que permite a ASF descubrir y cargar tus bibliotecas durante el tiempo de ejecución.

* * *

### Comenzando

Tu proyecto debe ser una biblioteca .NET estándar que tenga como objetivo el entorno de trabajo apropiado para tu versión de ASF, como se especifica en la **[compilación](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compilation-es-es)**. Recomendamos que apuntes a .NET Core, pero los plugins en .NET Framework también están.

El proyecto debe hacer referencia al ensamblado principal `ArchiSteamFarm`, ya sea una biblioteca `ArchiSteamFarm.dll` preconstruida que hayas descargado como parte de la versión, o el proyecto fuente (por ejemplo, si decides añadir ASF tree como submódulo). Este te permitirá acceder y descubrir estructuras, métodos y propiedades de ASF, especialmente la interfaz `IPlugin` la que necesitarás para heredar en el siguiente paso. El proyecto también debe referenciar `System.Composition.AttributedModel` al mínimo, lo que te permite exportar `[Export]` tu `IPlugin` para que ASF use. Además, tal vez quieras/necesites referenciar otros bibliotecas comunes para interpretar las estructuras de datos que se te presentan en algunos interfaces, pero a menos que las necesite explícitamente, eso sería suficiente por ahora.

Si hiciste todo correctamente, tu `csproj` será similar al siguiente:

```csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="System.Composition.AttributedModel" Version="*" />
  </ItemGroup>

  <ItemGroup>
    <Reference Include="ArchiSteamFarm">
      <HintPath>C:\\Path\To\Downloaded\ArchiSteamFarm.dll</HintPath>
    </Reference>

    <!-- If building as part of ASF source tree, use this instead of <Reference> above -->
    <!-- <ProjectReference Include="C:\\Path\To\ArchiSteamFarm\ArchiSteamFarm.csproj" /> -->
  </ItemGroup>
</Project>
```

Por el lado del código, la clase de tu plugin debe heredar de la interfaz `IPlugin` (ya sea explícitamente, o implícitamente heredando desde una interfaz más especializada, como `IASF`) y `[Export(typeof(IPlugin))]`para ser reconocido por ASF durante el tiempo de ejecución. El ejemplo más sencillo que consigue eso sería el siguiente:

```csharp
using System;
using System.Composition;
using ArchiSteamFarm;
using ArchiSteamFarm.Plugins;

namespace YourNamespace.YourPluginName {
    [Export(typeof(IPlugin))]
    public sealed class YourPluginName : IPlugin {
        public string Name => nameof(YourPluginName);
        public Version Version => typeof(YourPluginName).Assembly.GetName().Version;

        public void OnLoaded() {
            ASF.ArchiLogger.LogGenericInfo("Meow");
        }
    }
}
```

Para usar tu plugin, primero debes compilarlo. Puedes hacerlo ya sea desde tu IDE (Entorno de Desarrollo Integrado), o desde el directorio raíz de tu proyecto a través de comando:

```shell
# Si tu proyecto es individual (no hay necesidad de definir su nombre ya que es el único)
dotnet publish -c "Release" -o "out"

# Si tu proyecto es parte del árbol fuente de ASF (para evitar la compilación de partes innecesarias)
dotnet publish YourPluginName -c "Release" -o "out"
```

Después, tu plugin está lista para su despliegue. Depende de ti como quieres distribuirlo y publicarlo, pero recomendamos crear un archivo zip con un solo archivo llamado `YourNamespace.YourPluginName` (TuNombreespacio.NombreDeTuPlugin), dentro del cual pondrás tu plugin compilado junto con sus **[dependencias](#plugin-dependencies)**. Así el usuario simplemente necesitará desempaquetar tu archivo zip en su directorio `plugins` y nada más.

Este solo es el escenario más básico para empezar. Tenemos el proyecto **[`ExamplePlugin`](https://github.com/JustArchiNET/ArchiSteamFarm/tree/master/ArchiSteamFarm.CustomPlugins.ExamplePlugin)** que muestra interfaces de ejemplo y acciones que puedes realizar dentro de tu propio plugin, incluyendo comentarios útiles. Siéntete libre de echar un vistazo si quieres aprender de un código funcional, o descubrir el namespace (espacio de nombres) `ArchiSteamFarm.Plugins` y dirígete a la documentación incluida para todas las opciones disponibles.

* * *

### Disponibilidad de API

ASF, además de a lo que tienes acceso en las interfaces, te expone un montón de APIs internas de las que puedes hacer uso, para ampliar la funcionalidad. Por ejemplo, si quisieras enviar algún tipo de nueva solicitud a la web de Steam, entonces no necesitas implementar todo desde cero, especialmente al tratar con todos los problemas con los que hemos tenido que tratar antes que tú. Simplemente usar nuestro `Bot.ArchiWebHandler` el cual ya expone un montón de métodos `UrlWithSession()` para que utilices, manejando por ti todas las cosas de bajo nivel, como autenticación, actualización de sesión o limitación web. Del mismo modo, para enviar solicitudes web fuera de la plataforma Steam, podrías usar la clase estándar .NET `HttpClient`, pero es mucho mejor idea usar `Bot.ArchiWebHandler.WebBrowser` que está disponible para ti, lo que una vez más ofrece ayuda, por ejemplo en lo que respecta a reintentar solicitudes fallidas.

Tenemos una política muy abierta en términos de nuestra disponibilidad de API, si quieres usar algo de lo que ya incluye el código de ASF, simplemente **[abre un problema](https://github.com/JustArchiNET/ArchiSteamFarm/issues)** y explica el uso que planeas darle a nuestra API interna de ASF. Seguramente no tendremos nada en contra, siempre que tu caso tenga sentido. Es simplemente imposible que abramos todo lo que alguien podría querer usar, así que hemos abierto lo que tiene más sentido para nosotros, y esperamos tu solicitud en caso de que quieras tener acceso a algo que todavía no es `public` (público). Esto también incluye todas las sugerencias respecto a nuevas interfaces `IPlugin` que podría tener sentido añadirlas para ampliar la funcionalidad existente.

De hecho, la API interna de ASF es la única limitación real en términos de lo que puede hace tu plugin. Nada te impide, por ejemplo, incluir la biblioteca `Discord.Net` en tu aplicación y crear un puente entre tu bot de Discord y los comandos de ASF, ya que tu plugin también puede tener dependencias propias. Las posibilidades son infinitas, e hicimos lo mejor para darte tanta libertad y flexibilidad como sea posible dentro de tu plugin, así que no hay límites artificiales, pero no estamos seguros de qué partes de ASF son cruciales para el desarrollo de tu plugin (lo que puedes solucionar haciéndonos saberlo, e incluso sin eso siempre puedes reimplementar la funcionalidad que necesites).

* * *

### Compatibilidad de API

Es importante destacar que ASF es una aplicación de consumo y no una biblioteca típica con una superficie de API fija de la que puedes depender incondicionalmente. Esto significa que no puedes suponer que tu plugin una vez compilado seguirá funcionando con todas las futuras versiones de ASF, es imposible si quieres seguir desarrollando el programa, y ser incapaz de adaptarse a los constantes cambios de Steam por el bien de la retrocompatibilidad simplemente no es adecuado para nuestro caso. Esto debería ser lógico para ti, pero es importante destacar ese hecho.

Haremos lo posible para mantener las partes públicas de ASF funcionando y estables, pero no dudaremos en romper la compatibilidad si surgen buenas razones, siguiendo nuestra política de **[depreciación](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Deprecation-es-es)** en el proceso. Esto es especialmente importante en lo que se refiere a las estructuras internas de ASF que están expuestas como parte de la infraestructura de ASF, explicadas arriba (por ejemplo, `ArchiWebHandler`) que podría ser mejorada (y por lo tanto reescrita) como parte de las mejoras de ASF en una de las versiones futuras. Haremos todo lo posible para informarte adecuadamente en los changelogs, e incluir las advertencias apropiadas durante el tiempo de ejecución sobre las características obsoletas. No pretendemos reescribir todo solo por reescribirlo, así que puedes estar bastante seguro que la siguiente versión menor de ASF simplemente no destruirá tu plugin solo porque tiene un número de versión superior, pero es buena idea estar pendiente de los changelogs y verificar ocasionalmente que todo funciona bien.

* * *

### Dependencias de plugin

Tu plugin incluirá al menos dos dependencias por defecto, `ArchiSteamFarm` referencia para API interna, y `PackageReference` de `System.Composition.AttributedModel` que se requiere para ser reconocida como un plugin de ASF. Además, podría incluir más dependencias de acuerdo a lo que hayas decidido hacer en tu plugin (por ejemplo, la biblioteca `Discord.Net` si decidiste integrar con Discord.Net).

El resultado de tu compilación incluirá tu biblioteca `YourPluginName.dll`, y todas las dependencias que hayas referenciado, al menos `ArchiSteamFarm.dll` y `System.Composition.AttributedModel.dll`.

Ya que estás desarrollando un plugin para un programa ya funcional, no tienes que, e incluso **no debes** incluir todas las dependencias que fueron generadas durante la compilación. Esto es porque ASF ya incluye la mayoría de ellas, por ejemplo `ArchiSteamFarm`, `SteamKit2` o `Newtonsoft.Json`. Reducir las dependencias de tu compilación compartidas con AFS no es un requisito absoluto para que funcione tu plugin, pero hacerlo reducirá drásticamente la huella de memoria y el tamaño de tu plugin, además de aumentar el rendimiento, debido al hecho de que ASF compartirá sus propias dependencias, y cargará solo aquellas bibliotecas que no conozca por sí mismo.

Por lo tanto, es una práctica recomendada incluir solo las bibliotecas que ASF no incluye, o las incluye en la versión incorrecta/incompatible. Ejemplos de esto sería obviamente `YourPluginName.dll`, pero como ejemplo también `Discord.Net.dll` si decidiste depender de ella. Agrupar bibliotecas que se comparten con ASF aún puede tener sentido si quieres asegurar la compatibilidad de API (por ejemplo, asegurar que `Newtonsoft.Json` del que dependes en tu plugin siempre estará en la versión `X` y no aquella con la que se publica ASF), obviamente hacer eso viene con un precio de memoria/tamaño mayor y peor rendimiento.

Si estás confundido por la declaración anterior y no sabes más, comprueba qué bibliotecas `dll` están incluidas en el paquete `ASF-generic.zip` y asegúrate de que tu plugin incluye solo las que no todavía no son parte de él. En mayoría de los casos esto solo será tu `YourPluginName.dll`. Si tienes algún problema durante el tiempo de ejecución relación con algunas bibliotecas, incluye también esas bibliotecas afectadas. Si todo lo demás falla, siempre puedes decidir agrupar todo.

* * *

### Dependencias nativas

Las dependencias nativas se generan como parte de las compilaciones de sistemas operativos específicos, ya que no hay tiempo de ejecución .NET Core en el host y ASF se ejecuta a través de su propio tiempo de ejecución .NET Core que está incluido como parte de la compilación de sistema operativo específico. Para minimizar el tamaño de la compilación, ASF recorta sus dependencias nativas para incluir solo el código que puede ser usado en el programa, lo que corta las partes sin usar del tiempo de ejecución. Esto puede crear un problema potencial en lo que respecta a tu plugin, si de repente te encuentras en una situación en la que tu plugin depende de alguna característica de .NET Core que es usada en ASF, y por lo tanto las compilaciones de sistema operativo específico no puede ejecutarla correctamente.

Esto nunca es un problema con las compilaciones genéricas, porque estas nunca tratan con dependencias nativas en primer lugar (ya que tienen un tiempo de ejecución completamente funcional en el host, ejecutando ASF). También es una solución al problema, **usa tu plugin exclusivamente en compilaciones genéricas**, pero eso tiene el inconveniente de excluir de tu plugin a los usuarios que ejecutan ASF en una compilación de OS específico. Si te estás preguntando si tu problema está relacionado con dependencias nativas, también puedes usar este método para verificación, carga tu plugin en una compilación genérica de ASF y ve si funciona. Si funciona, tienes cubiertas las dependencias de tu plugin, por lo que solo queda trabajar con las dependencias nativas.

La solución a este problema, muy similar a las dependencias de plugin generales, es una vez más empaquetar tu plugin con las dependencias que ASF no tiene, o tiene en la versión incorrecta/incompatible (por ejemplo, recortada). Comparado con las dependencias de plugin, no puedes estar seguro si la dependencia nativa de una versión recortada de ASF tiene todo lo que necesitas, así que puedes decidir usar la forma fácil y empaquetar todo, o la forma difícil y verificar manualmente qué partes faltan en ASF e incluir solo esas partes. Para obtener las dependencias que necesitas, primero tendrás que compilar ASF manualmente para la variante de sistema operativo específico (sin recortarlo), luego copiar los archivos que necesitas para que funcione tu plugin.

Esto también significa que **podrías necesitar tener un compilación dedicada de tu plugin para cada variante de ASF**, ya que a cada compilación de SO específico de ASF le pueden faltar diferentes características que necesitarás proporcionarse tú mismo, y tu plugin genérico no puede proporcionarlas todas por sí mismo. Esto depende en gran medida de lo que realmente hace tu plugin y de qué depende, ya que los plugins muy simples basados puramente en las funciones de ASF está garantizado que funcionarán bien en todas las configuraciones, ya que no tienen dependencia propias y tienen todas las dependencias nativas cubiertas por definición. Los plugins más complicados (especialmente aquellos que tienen dependencias propias) podrían necesitar tomar medidas adicionales para asegurar que en efecto proporcionan todas las partes del código requeridas, no solo dependencias de alto nivel (descritas en la sección anterior), sino también las dependencias nativas de bajo nivel. Si todo lo demás falla, igual que antes, siempre puedes compilar tu plugin para la misma variante de sistema operativo específico que quieras usar, luego empaquetar todas las dependencias generadas junto con las generadas por la compilación de ASF para la misma variante de sistema operativo específico.
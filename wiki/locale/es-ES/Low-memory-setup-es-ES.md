# Configuración de bajo uso de memoria

Esto es exactamente lo contrario de **[configuración de alto rendimiento](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/High-performance-setup-es-es)** y normalmente quieres seguir estos consejos si quieres reducir el uso de memoria de ASF, por el costo de reducir el rendimiento general.

* * *

ASF es extremadamente ligero en recursos por definición, dependiendo de tu uso incluso 128 MB VPS con Linux es capaz de ejecutarlo, aunque ir tan bajo no se recomienda y puede resultar en varios problemas. Mientras que es ligero, ASF no tiene miedo de pedir más memoria al sistema operativo, si dicha memoria es necesaria para que ASF funcione con una velocidad óptima.

ASF como aplicación intenta ser lo más optimizada y eficiente posible, lo que también toma en cuenta los recursos usados durante su ejecución. Cuando se trata de memoria, ASF prefiere el rendimiento sobre el consumo de memoria, lo que puede resultar en "picos" de memoria temporales, que puede notarse, por ejemplo con cuentas que tienen más de 3 páginas de insignias, como ASF analizará la primera página, leerá el número total de páginas, luego ejecutará la tarea por cada página adicional, lo que resulta en la obtención y análisis de las páginas restantes. Ese uso de memoria "extra" (comparado con el mínimo para la operación) puede acelerar drásticamente la ejecución y el rendimiento en general, por el costo de un aumento en el uso de memoria necesaria para hacer todas esas cosas en paralelo. Algo similar ocurre en todas las demás tareas generales de ASF que pueden ejecutarse en paralelo, por ejemplo al analizar ofertas de intercambio activas, ASF puede analizar todas al mismo tiempo, al ser independientes entre sí. Además, ASF (C# runtime) **no** regresará la memoria sin usar al sistema operativo inmediatamente después, lo que puedes notar porque el proceso de ASF está tomando más y más memoria, pero nunca regresándola al sistema operativo. Algunas personas pueden encontrarlo cuestionable, incluso sospechar una fuga de memoria, pero no te preocupes, todo es esperado.

ASF está extremadamente bien optimizado, y hace uso de los recursos disponibles lo más posible. El alto uso de memoria de ASF no significa que ASF activamente **usa** esa memoria y **la necesita**. Con frecuencia ASF mantendrá la memoria asignada como "espacio" para futuras acciones, porque podemos mejorar drásticamente el rendimiento si no necesitamos pedir al sistema operativo cada pedazo de memoria que estemos por usar. El runtime debe liberar automáticamente la memoria no usada por ASF de vuelta al sistema operativo cuando este **realmente** la necesite. **[La memoria no usada es memoria desperdiciada](https://www.howtogeek.com/128130/htg-explains-why-its-good-that-your-computers-ram-is-full)**. Tienes problemas cuando la memoria que **necesitas** es mayor que la memoria disponible, no cuando ASF mantiene alguna adicional asignada con el propósito de acelerar las funciones que ejecutará en un momento. Tienes problemas cuando tu kernel Linux está deteniendo el proceso de ASF debido a OOM (out of memory), no cuando ves el proceso de ASF como el consumidor de memoria más alto en `htop`.

El recolector de basura usado en ASF es un mecanismo muy complejo, lo suficientemente inteligente para tener en cuenta no solo el propio ASF, sino también tu sistema operativo y sus procesos. Cuando tienes mucha memoria libre, ASF pedirá la que sea necesaria para mejorar el rendimiento. Esto puede ser hasta 1 GB (con servidor GC). Cuando la memoria de tu sistema operativo esta por llenarse, ASF automáticamente liberará parte de ella de vuelta al sistema operativo para ayudar a que las cosas se calmen, lo que puede resultar en un uso de memoria tan bajo como 50 MB. La diferencia entre 50 MB y 1 GB es enorme, pero también lo es la diferencia entre un pequeño VPS de 512 MB y un enorme servidor de 32 GB. Si ASF puede garantizar que esta memoria será útil, y al mismo tiempo que nada más la requiere ahora mismo, preferirá conservarla y optimizarse automáticamente basándose en rutinas que fueron ejecutadas en el pasado. El recolector de basura usado en ASF es autoajustable y logrará mejores resultados mientras más tiempo se ejecute el proceso.

Esta también es la razón por la que la memoria del proceso ASF varía entre configuraciones, ya que ASF hará todo lo posible por utilizar los recursos disponibles de la **manera más eficiente posible**, y no de una manera fija como se hacía durante los tiempos de Windows XP. El uso real de memoria de ASF puede ser verificado con el **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-es-es) **`stats`, y normalmente es alrededor de 4 MB para unos cuantos bots, hasta 30 MB si usas cosas como **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-es-es)** y otras características avanzadas. Ten en cuenta que la memoria devuelta por el comando `stats` también incluye la memoria libre que todavía no ha sido reclamada por el recolector de basura. Todo lo demás es memoria compartida de runtime (alrededor de 40-50 MB) y espacio para ejecución (varía). Por eso también el mismo ASF puede usar tan solo 50 MB en un ambiente VPS de baja memoria, mientras que puede usar incluso hasta 1 GB en tu máquina de escritorio. ASF se adapta activamente a tu entorno e intentará encontrar un equilibrio óptimo para no poner tu sistema operativo bajo presión, ni limitar su propio rendimiento cuando tengas mucha memoria sin usar que se podría utilizar.

* * *

Por supuesto, hay muchas formas de ayudar a ASF a apuntar en la dirección correcta en términos de la memoria que esperas utilizar. En general, si no necesitas hacerlo, es mejor dejar que el recolector de basura trabaje en paz y haga lo que considere mejor. Pero esto no siempre es posible, por ejemplo si tu servidor de Linux también alova varios sitios web, MySQL database y PHP workers, entonces no puedes permitirte que ASF se reduzca a sí mismo cuando estás cerca de OOM (out of memory), ya que es demasiado tarde y la degradación de rendimiento viene antes. Usualmente esto es cuando podría interesarte hacer más ajustes, y por lo tanto leer esta página.

Las siguientes sugerencias se dividen en algunas categorías, con variada dificultad.

* * *

## Configuración de ASF (fácil)

Los siguientes trucos **no afectan negativamente el rendimiento** y se pueden aplicar de forma segura a todas las configuraciones.

- Nunca ejecutes más de una instancia de ASF. ASF está hecho para manejar un número ilimitado de bots al mismo tiempo, y a menos que vincules cada instancia de ASF a diferentes interfaces/direcciones IP, debes tener exactamente **un** proceso de ASF, con múltiples bots (si es necesario).
- Haz uso de `ShutdownOnFarmingFinished`. Un bot activo toma más recursos que uno desactivado. No es un ahorro significativo, ya que el estado del bot aún necesita ser conservado, pero estás ahorrando algunos recursos, especialmente todos los recursos relacionados con la red, al como los sockets TCP. Solo necesitas un bot activo para mantener la instancia de ASF en ejecución, y siempre puedes iniciar más bots si es necesario.
- Mantén bajo el número de tus bots. Una instancia de bot no `Enabled` consume menos recursos, ya que ASF no se molesta en iniciarlo. También ten en cuenta que ASF tiene que crear un bot por cada una de tus configuraciones, por lo tanto si no necesitas `start` iniciar un bot dado y quieres ahorrar algo de memoria adicional, puedes renombrar temporalmente `Bot.json` a algo como `Bot.json.bak` para evitar crear un estado en ASF para tu bot desactivado. De esta manera no podrás `start` iniciarlo sin renombrarlo de nuevo, pero ASF tampoco se molestará en mantener el estado de este bot en la memoria, dejando espacio para otras cosas (muy bajo ahorro, en el 99.9% de los casos no deberías molestarte con esto, solo mantén tus bots con la propiedad `Enabled` en `false`).
- Ajusta tus configuraciones. Especialmente las configuraciones globales de ASF tienen muchas variables para ajustar, por ejemplo aumentando `LoginLimiterDelay` los bots se activarán más lento, lo que permitirá que la instancia ya iniciada obtenga las insignias mientras tanto, contrario a iniciar los bots más rápido, lo que tomará más recursos ya que más bots harán un trabajo importante (como obtener las insignias) al mismo tiempo. Cuanto menos trabajo se tenga que hacer al mismo tiempo - menos memoria se utilizará.

Estas son algunos cosas que puedes tener en cuenta cuando tengas que lidiar con el uso de memoria. Sin embargo, estas cosas no tiene ninguna importancia "crucial" en el uso de memoria, porque este proviene principalmente de cosas con las que ASF tiene que tratar, y no de estructuras internas usadas para la recolección de cromos.

Las funciones más pesadas en cuanto a recursos son:

- Analizar la página de insignias
- Analizar el inventario

Lo que significa que la memoria se elevará más cuando ASF está leyendo páginas de insignias, y cuando está tratando con su inventario (por ejemplo, enviar un intercambio o trabajar con STM). Esto es porque ASF tiene que tratar con una gran cantidad de datos - el uso de memoria de tu navegador ejecutando esas dos páginas no será mucho menor que eso. Lo sentimos, así es como funciones - disminuye el número de tus páginas de insignias, y mantén baja la cantidad de artículos en tu inventario, eso seguro puede ayudar.

* * *

## Ajustes de runtime (avanzado)

Los siguientes trucos **involucran reducción del rendimiento** y deben ser usados con precaución.

.NET Core runtime te permite **[modificar el recolector de basura](https://docs.microsoft.com/dotnet/core/run-time-config/garbage-collector)** de muchas formas, afinando eficazmente el proceso de recolección de basura de acuerdo a tus necesidades.

La forma recomendada de aplicar estas configuraciones es a través de las propiedades de entorno `COMPlus_`. Por supuesto, también podrías usar otros métodos, por ejemplo, `runtimeconfig.json`, pero algunas configuraciones son imposibles de establecer de esta manera, encima de eso ASF reemplazará tu `runtimeconfig.json` personalizado en la siguiente actualización, por lo tanto recomendamos propiedades de entorno que puedas establecer fácilmente antes de ejecutar el proceso.

Consulta la documentación para todas las propiedades que puedes utilizar, a continuación mencionaremos las más importantes (en nuestra opinión):

### `GCHeapHardLimitPercent`

> Especifica el uso de la recolección de elementos no utilizados como un porcentaje de la memoria total.

El límite de memoria para el proceso de ASF, este parámetro ajusta la recolección de basura (GC) para usar solamente un subconjunto de la memoria total y no toda. Puede resultar especialmente útil en situaciones de servidor donde puedes dedicar una porcentaje fijo de la memoria de tu servidor para ASF, pero nunca más que eso. Ten en cuenta que limitar la memoria de ASF no hará que todas las asignaciones de memoria desaparezcan, por lo tanto establecer este valor muy bajo podría resultar en escenarios en los que no hay memoria suficiente, en los que el proceso ASF se verá forzado a terminar.

Por otro lado, establecer este valor lo suficientemente alto es una forma perfecta de asegurar que ASF nunca usará más memoria de la que puedes permitirte realmente, dando a tu máquina un respiro incluso bajo una carga pesada, y permitiendo al programa hacer su trabajo de manera eficiente cuando sea posible.

### `GCLatencyLevel`

> Especifica el nivel de latencia de GC para el que quieres optimizar.

Esta es una propiedad no documenta que resultó funcionar excepcionalmente bien para ASF, limitando los tamaños de generación del recolector de basura y en consecuencia hace que este los purgue más frecuente y agresivamente. El nivel de latencia (equilibrado) por defecto es `1`, queremos usar `0`, lo que ajustará el uso de memoria.

### `gcTrimCommitOnLowMemory`

> Cuando está establecido se recorta el espacio comprometido más agresivamente para el segmento efímero. Esto se utiliza para ejecutar muchas instancias de procesos de servidor donde quieren mantener tan poca memoria comprometida como sea posible.

Esto ofrece pocos beneficios, pero puede hacer que el recolector de basura (GC) sea más agresivo cuando el sistema tenga poca memoria, especialmente para ASF que hace un uso fuerte de las tareas del grupo de subprocesos (threadpool).

* * *

Puedes habilitar todas las propiedades del recolector de basura (GC) estableciendo las variables de entorno `COMPlus_` apropiadas. Por ejemplo, en Linux (shell):

```shell
# Don't forget to tune this one if you're going to use it
export COMPlus_GCHeapHardLimitPercent=75

export COMPlus_GCLatencyLevel=0
export COMPlus_gcTrimCommitOnLowMemory=1

./ArchiSteamFarm # For OS-specific build
```

O en Windows (powershell):

```powershell
# Don't forget to tune this one if you're going to use it
$Env:COMPlus_GCHeapHardLimitPercent=75

$Env:COMPlus_GCLatencyLevel=0
$Env:COMPlus_gcTrimCommitOnLowMemory=1

.\ArchiSteamFarm.exe # For OS-specific build
```

Especialmente `GCLatencyLevel` será muy útil ya que verificado que runtime realmente optimiza el código para la memoria y por lo tanto reduce el uso de memoria promedio significativamente, incluso con server GC. Es uno de los mejores trucos que puedes aplicar si quieres reducir significativamente el uso de memoria de ASF sin degradar demasiado el rendimiento con `OptimizationMode`.

* * *

## Ajuste de ASF (intermedio)

Los siguientes trucos **involucran mucha reducción del rendimiento** y deben ser usados con precaución.

- Como último recurso, puedes ajustar ASF para `MinMemoryUsage` a través de la **[propiedad de configuración global](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-es-es#configuración-global)** `OptimizationMode`. Lee cuidadosamente su propósito, ya que esto es una grave degradación del rendimiento por casi ningún de memoria. Normalmente esto es **lo último que quieres hacer**, mucho después de pasar por **[ajustes de runtime](#ajustes-de-runtime-avanzado)** para asegurarte de que estás obligado a hacer esto.

* * *

## Optimización recomendada

- Empieza con trucos de configuración simples, tal vez solo estás usando tu ASF de una manera equivocada como iniciar el procesos varias veces para todos tus bots, o mantenerlos todos activos si solo necesitas uno o dos para autoiniciar.
- Si aún no es suficiente, habilita todas las propiedades de configuración mencionadas arriba estableciendo las variables de entorno `COMPlus_` apropiadas. Especialmente `GCLatencyLevel` ofrece significativas mejoras de runtime por poco costo en rendimiento.
- Si ni siquiera eso ayudó, como último recurso habilita `MinMemoryUsage` `OptimizationMode`. Esto obliga a ASF a ejecutar casi todo de forma sincrónica, haciéndolo funcionar mucho más lento pero además no confía en el grupo de hilos para equilibrar las cosas cuando se trata de ejecución en paralelo.

Es físicamente imposible reducir aún más la memoria, ASF ya está muy degradado en términos de rendimiento y ya agotaste todas las posibilidades, tanto en términos de código como de runtime. Considere añadir algo de memoria adicional para que use ASF, incluso 128 MB harían una gran diferencia.
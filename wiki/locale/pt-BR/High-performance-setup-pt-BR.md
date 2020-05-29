# Configuração de alto desempenho

Isto é exatamente o oposto da **[configuração de pouca memória](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup)** e provavelmente você vai querer seguir essas dicas se você quer aumentar a performance do ASF (em termos de velocidade da CPU), ao custo de aumentar o uso de memória.

* * *

O ASF tenta preferir desempenho quando se trata equilíbrio, portanto não há muito que você possa fazer para aumentar sua performance, embora não estejamos totalmente sem opções. No entanto, tenha em mente que essas opções não são habilitadas por padrão, o que significa que elas não são boas o bastante para considerá-las equilibradas para a maioria das situações de uso, portanto você deve decidir por si mesmo se o aumento de memória que elas trazem valem a pena para você.

* * *

## Ajuste do tempo de execução (avançado)

Os truques abaixo **envolve um grande aumento do uso de memória** e devem ser usados com cautela.

O tempo de execução.NET Core permite que você **[ajuste o coletor de lixo](https://docs.microsoft.com/pt-br/dotnet/core/run-time-config/garbage-collector)** de muitas maneiras, aperfeiçõando efetivamente o processo de coleta de lixo de acordo com suas necessidades.

A forma recomendada de aplicar essas configurações é através das propriedades de ambiente `COMPlus_`. Claro, você também pode usar outros métodos, p. ex.: `runtimeconfig.json`, mas é impossível definir algumas configurações desta maneira e, além disso, o ASF substituirá o seu arquivo personalizado `runtimeconfig.json` pelo arquivo próprio do ASF, portanto recomendamos propriedades de ambiente que você possa definir facilmente antes de iniciar o processo.

Consulte a documentação para todas as propriedades que você pode usar, mencionaremos as mais importantes (na nossa opinião) abaixo:

### `gcServer`

> Configura se o aplicativo usa a coleta de lixo da estação de trabalho ou do servidor.

Você pode ler as especificações exatas do coletor de lixo de servidor em **[noções básicas da coleta de lixo](https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/fundamentals)**.

O ASF usa a coleta de lixo de estação de trabalho por padrão. Principalmente por causa do balanço entre uso de memória e desempenho, o que é mais que suficiente para alguns bots, já que um único coletor de lixo simultaneamente em segundo plano é rápido o bastante para cuidar de toda a memória alocada pelo ASF.

No entanto, hoje nós temos um monte de núcleos de CPU dos quais o ASF pode se beneficiar por ter um thread de coleta de lixo dedicado para cada núcleo de CPU virtual disponível. Isto poderá melhorar muito o desempenho durante tarefas pesadas do ASF tais como análise de páginas de insígnias ou inventário, já que cada núcleo virtual de CPU pode ajudar, ao invés de apenas 2 (o principal e o de coleta de lixo). A coleta de lixo do servidor é recomendado para computadores com 3 núcleos virtuais de CPU ou mais e a coleta de lixo de estação de trabalho é forçada automaticamente se seu computador tem apenas um núcleo virtual de CPU, e se você tem exatamente 2, considere tentar ambos (os resultados podem variar).

A coleta de lixo de servidor em si não resulta em um aumento de memória muito grande apenas por estar ativo, mas tem muito mais capacidade de geração e portanto é muito mais lento quando se trata de retorno de memória ao sistema operacional. Você pode achar que encontrou um bom ponto quando o coletor de lixo de servidor aumenta significativamente o desempenho e você deseja usa-lo, mas ao mesmo tempo você não pode permitir um aumento significativo do uso de memória que acompanha o uso dele. Felizmente há uma configuração que proporciona o "melhor dos dois mundos": usar o coletor de lixo do servidor com a propriedade de configuração **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-pt-BR#gclatencylevel)** definida como `0`, que vai permitir o coletor de lixo do servidor, mas limitar os tamanhos de geração e focar mais na memória. Como alternativa, você pode experimentar a propriedade **[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-pt-BR#gcheaphardlimitpercent)**, ou até mesmo ambos ao mesmo tempo.

No entanto, se memória não é um problema para você (como o coletor de lixo leva em conta sua memória disponível e se auto-ajusta) é melhor não alterar essas opções, alcançando um desempenho superior como resultado.

* * *

Você pode habilitar ambas as propriedade de coletor de lixo definindo as variáveis `COMPlus_` apropriadas. Por exemplo, no linux (shell):

```shell
export COMPlus_gcServer=1

./ArchiSteamFarm # Para compilação específica para SO
```

Ou no Windows (powershell):

```powershell
$Env:COMPlus_gcServer=1

.\ArchiSteamFarm.exe # Para compilação específica para SO
```

* * *

## Otimização recomendada

- Certifique-se de estar usando o valor padrão em `OptimizationMode` (modo de otimização) que é `MaxPerformance` (máximo desempenho). Esse é de longe a configuração mais importante uma vez que usar o valor `MinMemoryUsage` (uso mínimo de memória) traz sérios efeitos ao desempenho.
- Habilitar coletor de lixo no servidor. A ativação da coleta de lixo do servidor pode ser percebida imediatamente por um aumento significativo de memória em comparação com o coletor de lixo de estação de trabalho.
- Se você não puder suportar esse aumento de memória, considere mudar **[`GCLatencyLevel`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-pt-BR#gclatencylevel)** e/ou **[`GCHeapHardLimitPercent`](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Low-memory-setup-pt-BR#gcheaphardlimitpercent)** para alcançar "o melhor de ambos os mundos". No entanto, se sua memória não aguenta é melhor manter tudo nos valores padrão; o coletor de lixo de servidor se auto-ajusta durante o tempo de execução e é inteligente o bastante para usar menos memória quando seu sistema operacional necessita dela.

Se você habilitou o coletor de lixo de servidor e manteve as outras opções com o valor padrão, então você terá uma performance superior do ASF que deverá estar funcionando rapidamente mesmo com centenas ou milhares de bots ativos. O CPU não deverá mais ser um gargalo, já que o ASF pode usar todo o desempenho do seu CPU caso necessário, reduzindo o tempo necessário ao mínimo possível. O próximo passo seria um upgrade em sua CPU e memória RAM.
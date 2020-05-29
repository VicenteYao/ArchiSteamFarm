# Trocas

O ASF possui suporte não interativo (offline) para trocas Steam. Tanto receber (aceitar/rejeitar) quanto enviar trocas é uma função disponível de imediato e não requer uma configuração especial, mas obviamente requer uma conta Steam sem restrições (que já tenha gasto 5 dólares na loja). O módulo de trocas não está disponível para contas restritas.

* * *

## Lógica

O ASF sempre aceitará todas as trocas, independente dos itens, enviadas pelo usuário com acesso `Master` (ou superior) ao bot. Isso permite não apenas pegar facilmente as cartas obtidas pela conta bot como também ajuda a administrar de forma fácil os itens que o bot guarda no inventário.

O ASF rejeitará a oferta de troca, independente do conteúdo, de qualquer usuário (não Master) que esteja na lista negra do módulo de trocas. A lista negra é armazenada no banco de dados padrão `BotName.db` e pode ser gerenciada através dos **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** `bl`, `bladd` e `blrm`. Isso deve funcionar como um alternativa ao bloqueio de usuário padrão da Steam - use com cautela.

O ASF aceitará todos os `loots` enviados entre os bots, a menos que `TradingPreferences` esteja definido como `DontAcceptBotTrades`. Em resumo, a configuração padrão `None` de `TradingPreferences` fará com que o ASF aceite automaticamente trocas do usuário com acesso `Master` ao bot (como explicado anteriormente), assim como todas as trocas de doação de outros bots que façam parte do processo do ASF. Se você quer desativar trocas de doação de outros bots, então é para isso que `DontAcceptBotTrades` na configuração `TradingPreferences` serve.

Quando você ativa a configuração `AcceptDonations` em `TradingPreferences`, o ASF também aceitará qualquer troca de doação - uma troca na qual a conta bot não vá perder nenhum item. Esta propriedade afeta apenas contas não-bot, uma vez que contas de bot são afetadas por `DontAcceptBotTrades`. `AcceptDonations` permite que você aceite doações facilmente de outras pessoas e também de bots que não estejam conectados ao processo do ASF.

Vale notar que `AcceptDonations` não requer o **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)**, já que não há necessidade de confirmação se não estamos perdendo nenhum item.

Você também pode personalizar a capacidade de trocas do ASF modificando `TradingPreferences` de acordo com o desejado. Uma das principais características do `TradingPreferences` é a opção `SteamTradeMatcher` que faz o ASF usar uma lógica interna para aceitar trocas que te ajudarão a completar insígnias faltantes, o que é especialmente útil em conjunto com a listagem pública do **[ SteamTradeMatcher](https://www.steamtradematcher.com)**, mas também funciona sem ele. Isso é descrito logo abaixo.

* * *

## `SteamTradeMatcher`

Quando o `SteamTradeMatcher` estiver ativo, o ASF usará um algorítimo um tanto complexo para verificar se a troca passa pelas regras do STM e é pelo menos neutra. A lógica atual é a seguinte:

- Rejeitar a troca se formos perder algo além dos tipos de item especificados em nosso `MatchableTypes`.
- Rejeitar a troca se não vamos receber ao menos a mesma quantidade de itens por jogo e por tipo.
- Rejeitar a troca se o usuário pedir por cartas especiais das promoções Steam de verão/inverno, e o mesmo tiver as trocas retidas.
- Rejeitar a troca se o tempo de retenção exceder a propriedade `MaxTradeHoldDuration` da configuração global.
- Rejeitar a troca se não tivermos configurado `MatchEverything`, e a mesma for pior que neutro para nós.
- Aceitar a troca se nós não a rejeitarmos através de qualquer um dos pontos acima.

É bom notar que o ASF também aceita contraproposta - a lógica funcionará corretamente quando o usuário estiver adicionando algo extra para a troca, desde que todas as condições acima forem atendidas.

Os 4 primeiros atributos devem ser óbvios para todos. A última inclui uma lógica para cartas duplicadas que analisa o estado atual do nosso inventário e decide qual é o status da troca.

- A troca é **boa** se aumentar nosso progresso em busca de completar o set. A A (antes) <-> A B (depois)
- A troca é **neutra** se nosso progresso em busca de completar o set continuar o mesmo. A B (antes) <-> A C (depois)
- A troca é **ruim** se diminuir nosso progresso em busca de completar o set. A C (antes) <-> A A (depois)

O STM só opera em trocas boas, o que significa que o usuário que estiver usando o STM para juntar cartas duplicadas deve sempre nos sugerir apenas trocas boas. No entanto, o ASF é liberal, e também aceita trocas neutras, já que nessas trocas não perdemos nada, então não há nenhuma razão para rejeitá-las. Isso é especialmente útil para os seus amigos, uma vez que eles podem trocar suas cartas extras sem usar o STM, contanto que você não esteja perdendo o progresso para completar a insígnia.

Por padrão, o ASF rejeitará trocas ruins - isso é o que você quase sempre vai querer como usuário. No entanto, você tem a opção de permitir `MatchEverything` em sua configuração de `TradingPreferences` para permitir que o ASF aceite tudas as trocas de cartas duplicadas, incluindo as **ruins**. Isso é útil apenas se você deseja executar uma bot de troca 1:1 em sua conta, uma vez que você entenda que ** o ASF não vai mais te ajudar a progredir para conclusão de insígnia e vai te deixar propenso a substituir um set completo por N cartas duplicadas.**. A não ser que você realmente queira rodar um bot de trocas que **nunca** finalizará um set, você não vai querer ativar essa opção.

Independentemente do que você escolher em `TradingPreferences`, uma troca que está sendo rejeitada pelo ASF não significa que você não pode aceitá-la você mesmo. Se você manteve o valor padrão de `BotBehaviour`, que não inclui `RejectInvalidTrades`, o ASF vai simplesmente ignorar essas trocas, permitindo que você decida se está interessado nelas ou não. O mesmo vale para trocas de itens fora dos cobertos pelo `MatchableTypes`, bem como tudo o resto - o módulo é feito para ajudá-lo a automatizar trocas STM, e não decidir o que é um bom negócio e o que não é. A única exceção esta regra é quando falamos de usuários que você colocou na lista negra do módulo de trocas usando o comando `bladd` - trocas propostas por esses usuários são imediatamente rejeitadas independentemente das configurações de `BotBehaviour`.

É altamente recomendado usar o **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication)** quando você habilitar essa opção, uma vez que esta função perde todo o seu potencial, se você decidir confirmar manualmente cada troca. O `SteamTradeMatcher` funcionará corretamente mesmo sem a capacidade de confirmar as trocas, mas pode gerar atraso de confirmações se você não estiver aceitando-as a tempo.

* * *

### `MatchActively`

A opção `MatchActively` é uma versão estendida do `SteamTradeMatcher` que, além das combinações passivas oferecidas por esta, também inclui combinações ativas nas quais o bot enviará propostas de trocas para outras pessoas.

Para usar essa opção há uma série de requisitos para atender. Primeiro você precisa habilitar o `SteamTradeMatcher` (já que esse recurso é uma extensão do outro), e se certificar de que `MatchEverything` está **desativado** (já que bots de troca nunca fazem combinações ativamente). Além disso você deve ser elegível à nossa **[listagem STM do ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-pt-BR#pol%C3%ADtica-de-privacidade-atual)**, com requisitos um pouco mais relaxados. Você deve ter o parâmetro `Statistics` ativo, uma conta sem **[restrições](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663&l=brazilian)**, o **[ASF-2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-pt-BR#asf-2fa)** ativo, e ao menos um tipo válido em `MatchableTypes`, tal como cartas colecionáveis.

Se você cumprir todos os requisitos acima o ASF vai se comunicar periodicamente com a nossa **[listagem STM pública do ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-pt-BR#listagem-p%C3%BAblica-do-asf-stm)** a fim de combinar bots disponíveis.

- Cada correspondência é composta por "rodadas", com até `10` sendo o máximo em uma única rodada.
- A cada rodada o ASF vai buscar nosso inventário e o inventário dos bots listados selecionados para encontrar itens `MatchableTypes` que possam ser combinados. Se for encontrada uma correspondência, o ASF vai enviar e confirmar a oferta de troca automaticamente.
- Cada conjunto (composto de appID, tipo de item e raridade do mesmo) pode ser combinado apenas uma vez em cada rodada. Isso foi implementado para minimizar o problema de "itens indisponíveis" e evitar a necessidade de esperar que cada bot reaja antes de enviar todas as trocas. É também a principal razão pela qual a correspondência é feita em rodadas e não por um processo constante.
- O ASF não enviará mais que `255` itens em uma única troca, e não mais que `5` trocas para um mesmo usuário em uma única rodada. Isso é imposto pelos limites do Steam, bem como por nosso próprio balanceamento.
- A rodada termina no momento em que tentamos combinar um total de `40` bots, se não for cancelado antes por falta de sets para combinar ou uma quantidade excessiva de rodadas vazias.
- Se a última rodada resultou no envio de pelo menos uma proposta de troca, a próxima rodada começará `5` minutos depois (para adicionar um cooldown e permitir que todos os bots reajam às nossas trocas), caso contrário o processo termina e recomeça em `8` horas.

Esse módulo deve ser transparente. O processo de correspondência deve começar aproximadamente `1` hora após o início do ASF e vai se repetir a cada `8` horas (se necessário). O recurso `MatchActively` foi feito para ser usado a longo prazo, verificando periodicamente para garantir que estamos na direção de completar o set, sem o curto espaço de tempo e a quantidade de pressão sobre os recursos necessários que ocorreria se isso fosse oferecido como um comando. Os usuários alvos desse módulo são principalmente contas principais e contas alternativas "ocultas", embora ele possa ser usado em qualquer bot que não foi configurado para `MatchEverything`.

O ASF fará o seu melhor para minimizar a quantidade de solicitações e a pressão gerada por usar esta opção, enquanto maximiza a eficiência das correspondências até o limite possível. O algorítimo exato da escolha dos bots para correspondência é um detalhe na implementação do ASF, mas no momento ele busca favorecer bots com uma diversidade melhor de jogos dos quais os itens vieram, com preferência para bots setados para `Any`.

O `MatchActively` leva em conta os bots que você pôs na lista negra de trocas através do **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-pt-BR)** `bladd` e não vai tentar trocas com eles. Pode ser usado para dizer ao ASF quais bots nunca devem ser combinados, mesmo se eles tiverem potenciais duplicatas que poderíamos usar.
# Desempenho

O principal objetivo do ASF é coletar cartas da forma mais eficiente possível, baseado em dois tipos de dados com os quais ele pode operar: um pequeno conjunto de dados fornecidos pelo usuário e que são impossíveis que o ASF saiba de outra forma, e uma grande quantidade de dados que podem ser analisados automaticamente pelo ASF.

No modo automático o ASF não permite que você escolha quais jogos que devem ser executados, nem permite que você altere o algoritmo de coleta de cartas. **O ASF sabe melhor do que você o que ele deve fazer e quais decisões ele deve tomar para tornar o processo de coleta o mais rápido possível**. Seu objetivo é definir as propriedades de configuração corretamente já que ASF não pode adivinhá-las por conta própria, todo o resto é oculto.

* * *

Há algum tempo a Valve mudou o algoritmo de recebimento de cartas. Desse ponto em diante, nós podemos dividir as contas do Steam em duas categorias: aquelas **com** recebimento de cartas restito e aquelas **sem**. A única diferença entre esses dois tipos é o fato de que as contas com restrição não recebem nenhuma carta de determinado jogo até que ele seja jogado por, no mínimo, `X` horas. Parece que contas mais antigas que nunca pediram reembolso têm **a coleta de cartas irrestrita**, enquanto novas contas e aqueles que pediram reembolso têm **a restrição**. No entanto isso é apenas uma teoria e não deve ser tomada como regra. É por isso que **não há uma resposta óbvia**, e o ASF depende de **você** dizer qual o caso da sua conta.

* * *

O ASF possui atualmente dois algorítimos de coleta:

O algorítimo **simples (Simple)** funciona melhor para contas que não tem restrição no recebimento de cartas. Este é o algoritmo padrão usado pelo ASF. O bot encontra jogos para coletar e os executa um por um até que todas as cartas sejam recebidas. Isso porque a taxa de obtenção de cartas quando se coleta de mais de um jogo ao mesmo tempo é próxima a zero e totalmente ineficaz.

O **complexo (Complex)** é um algoritmo novo que foi implementado para ajudar contas restritas a maximizar seus lucros. O ASF usará primeiro o algorítimo padrão **Simple** em todos os jogos que já tem mais tempo de jogo que o definido em `HoursUntilCardDrops`, após isso ele executará todos os jogos (até o limite de `32`) que ainda não tenham atingido o valor de `HoursUntilCardDrops` simultaneamente até um deles atinja esse limite, então o ASF vai recomeçar o loop (usar o algorítimo **Simple** naquele jogo, retornar para a execução simultânea até atingir `HoursUntilCardDrops` e assim por diante). Podemos rodar vários jogos neste caso para aumentar as horas dos jogos que precisamos coletar até que eles atinjam o valor necessário. Tenha em mente que durante a "coleta de horas" o ASF **não** coleta cartas, portanto ele também não vai verificar por recebimento de cartas durante esse período (pelas razões citadas acima).

Atualmente, o ASF escolhe o algoritmo de coleta baseado puramente no parâmetro de configuração `HoursUntilCardDrops` (que é definida por **você**). Se `HoursUntilCardDrops` for definida como `0`, o algorítimo **Simple** será usado, caso contrário, será usado o algorítimo **Complex**.

* * *

### **Não há resposta óbvia sobre qual algorítimo é melhor para você**.

Esta é uma das razões pela qual você não escolhe o algorítimo de coleta, em vez disso, você dizer ao ASF se sua conta tem resrições no recebimento de cartas ou não. Se ela não tiver, o algorítimo **simples será o melhor**, já que não perderemos tempo fazendo todos os jogos terem `X` horas jogadas - a taxa de recebimento de cartas quando se coleta de vários jogos ao mesmo tempo é tende a 0%. Por outro lado, se sua conta tiver restrições no recebimento, o algorítimo **Complex** será melhor para você, pois não há lógica em coletar de um jogo sozinho que ainda não tenha atingido as horas definidas em `HoursUntilCardDrops` - então nós coletaremos **horas de jogo** primeiro, e **depois** cartas em modo solo.

Não defina cegamente `HoursUntilCardDrops` só porque alguém te disse para fazer - faça testes, compare resultados e, com base nos dados, decida qual opção deve ser melhor para você. Se você colocar um mínimo de esforço nisso, você vai garantir que ASF esteja trabalhando com a máxima eficiência possível na sua conta, que é provavelmente o que você quer já que você está lendo esta página da wiki agora. Se houvesse uma solução que funcione para todos, você não teria uma escolha, o ASF decidiria por conta própria.

* * *

### Qual é a melhor maneira de descobrir se sua conta é restrita?

Tenha certeza de que você tem jogos dos quais coletar, preferencialmente cinco ou mais, e rode o ASF com o valor `0` em `HoursUntilCardDrops`. Seria uma boa ideia não jogar nada durante o período de coleta para obter resultados mais precisos (melhor rodar o ASF durante a noite). Deixe o ASF fazer o processo de coleta desses 5 jogos e, depois, confira o log para ver os resultados.

O ASF diz claramente quando uma carta para determinado jogo foi recebida. Você está interessado no recebimento mais rápido alcançado pelo ASF. Por exemplo, se sua conta não tiver restrição, então a primeira carta deve aparecer após cerca de 30 minutos de coleta. Se você notar que recebeu uma carta de **pelo menos um ** jogo nesses 30 minutos iniciais é um indicador de que sua conta **não** tem restrições e você deve usar `HoursUntilCardDrops` com o valor `0`.

Por outro lado, se você notar que **cada** jogo está levando pelo menos `X` horas antes de você receber sua primeira carta, então isso é um indicador de que você deve definir `HoursUntilCardDrops`. A maioria (se não todos) dos usuários com restrições precisam de pelo menos `3` horas de tempo de jogo até que as cartas apareçam e esse é o valor padrão da configuração `HoursUntilCardDrops`.

Lembre-se que os jogos podem ter uma taxa de tempo de recebimento de cartas diferente, por isso você deve testar se sua teoria está certa com **pelo menos** 3 jogos, preferencialmente com mais de 5 para garantir que você não está tendo resultados falsos por coincidência. Uma carta recebida de um jogo em menos de uma hora é uma confirmação de que sua conta **não é** restrita e pode usar `HoursUntilCardDrops` com o valor `0`, mas para confirmar que a sua conta **é** restrita, você precisa de pelo menos alguns jogos que não estão dando cartas até você atingir um valor fixo de horas.

É importante notar que no passado `HoursUntilCardDrops` tinha apenas o valor `0` ou `2` e é por isso que o ASF tinha uma propriedade única `CardDropsRestricted` que permitia alternar entre esses dois valores. Com as mudanças recentes constatamos que não só a maioria dos usuários necessitam de `3` horas ao invés de `2`, mas também que `HoursUntilCardDrops` agora é dinâmico e pode atingir qualquer valor baseado na conta.

No fim, claro, a decisão é sua.

E para deixar tudo ainda pior - eu presenciei casos onde as pessoas mudaram de restrito para irrestrito e vice versa - tanto por causa de uma falha no Steam (sim, temos muitas) quanto por alguns ajustes de lógica feitos pela Valve. Então, mesmo que você tenha confirmado se sua conta é restrita (ou não), não acredite que ela permanecerá assim - uma vez que para mudar de irrestrita para restrita basta pedir um reembolso. Se você sentir que o valor definido anteriormente não é mais apropriado, você pode sempre refazer o teste e ajustar de acordo.

* * *

Por padrão, o ASF assume que `HoursUntilCardDrops` com o valor `3`, já que o efeito negativo de configurar para `3` quando deveria ser menos é menor do que o contrário. Isso pelo fato de que no pior caso possível nós perderemos `3` horas de coleta a cada `32` jogos, comparado a perder `3` horas de coleta em cada jogo se `HoursUntilCardDrops` fosse configurado para `0` por padrão. No entanto, você ainda deve configurar essa variável de acordo com a sua conta para obter a máxima eficiência, uma vez que o valor padrão é apenas uma hipótese baseado em potenciais inconvenientes e na maioria dos usuários (estamos tentando escolher o "mau menor" por padrão).

No momento os dois algorítimos acima são suficientes para coletar automaticamente da forma mais efetiva em todos os cenários atuais possíveis, portanto não há planejamento de adicionar novos.

É legal notar que o ASF também inclui o modo de coleta `Manual` que pode ser ativado pelo comando `play`. Você pode ler mais sobre isso na seção **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-pt-BR)**.

* * *

## Falhas no Steam

O algoritmo de coleta de cartas nem sempre funciona da maneira que deveria, e é inteiramente possível que várias falhas no Steam aconteçam, tais como cartas recebidas em contas restritas, cartas recebidas quando se fecha/muda o jogo, cartas não recebidas quando se joga um jogo, entre outros.

Esta seção é principalmente para pessoas que estão se perguntando por que o ASF não faz **X**, como alternar rapidamente entre jogos para coletar cartas mais rápido.

O que é uma **falha no Steam**: uma ação específica que desencadeia um **comportamento indefinido**, que **não é intencional, não é documentado e é considerado uma falha de lógica**. Ele **não confiável por definição**, o que significa que ele não pode ser reproduzido confiantemente em um ambiente de teste limpo e, portanto, codificado sem recorrer a hacks que supõem adivinhar quando a falha está acontecendo e como lutar com/abusar dela. Normalmente essas falhas são temporárias até que os desenvolvedores as corrijam, embora algumas possam passar despercebidas por um longo período de tempo.

Um bom exemplo do que é considerado uma **falha no Steam** é a situação não tão incomum de receber uma carta quando um jogo é fechado, que pode ser usado até certo ponto com a função de trocar jogos do Idle Master.

- **Comportamento indefinido** - não dá para dizer se alguma carta será recebida ou não quando você ativa a falha.
- **Não intencional** - com base em experiências anteriores e no comportamento da rede Steam que não tem o mesmo comportamento ao enviar uma única solicitação.
- **Não-documentado** - é claramente documentado no site Steam como as cartas são obtidas, e **em todos os lugares** afirma-se claramente que são obtidas **jogando**, e não fechando jogos, conseguindo conquistas, mudando de jogo ou abrindo 32 jogos de uma vez.
- **Considerado uma falha de lógica** - fechar jogos ou trocar entre os jogos abertos não deveria ter relação com o recebimento de cartas, já que a descrição define claramente que elas devem ser recebidas por **tempo gasto no jogo**.
- **Não confiável por definição, não pode ser reproduzido confiantemente** - não funciona para todos, e mesmo que tenha funcionado uma vez para você, pode não funcionar pela segunda vez.

Uma vez que percebemos o que é um erro do Steam e que o fato de recebermos cartas quando um jogo é fechado **é** um deles, nós podemos partir para o segundo ponto: **o ASF não abusa da rede Steam de forma alguma e faz o possível para se enquadrar no Acordo de Assinatura do Steam, seus protocolos e o que é geralmente aceito**. Sobrecarregar a rede Steam com constantes pedidos de abertura/fechamento de jogos pode ser considerado um **[Ataque DoS](https://pt.wikipedia.org/wiki/Ataque_de_nega%C3%A7%C3%A3o_de_servi%C3%A7o)** e **viola diretamente [o Código de Conduta Online do Steam](https://store.steampowered.com/online_conduct/?l=portuguese-br)**.

> Como um assinante do Steam, você aceita seguir o código de conduta abaixo.
> 
> Você não irá:
> 
> Organizar ataques contra um servidor do Steam ou contra o próprio Steam.

Não importa se você é capaz de provocar uma falha do Steam com outros programas (como o IM), e também não importa se você concorda conosco e considera tal comportamento como um ataque DoS ou não - cabe a Valve julgar isso, mas se considerarmos isso como um comportamento não-intencional de exploração/abuso através de solicitações excessivas para a rede Steam, então você pode ter certeza de que a Valve terá uma visão semelhante quanto a isso.

O ASF **nunca** vai tirar proveito de falhas no Steam, abusos, hacks ou qualquer outra atividade que nós vemos como **ilegais ou indesejáveis** de acordo com o Acordo de Assinatura do Steam, o Código de Conduta Online do Steam, ou qualquer outra fonte confiável que poderia indicar que a atividade do ASF é indesejada pela rede Steam, como afirmado na seção **[contribuindo](https://github.com/JustArchiNET/ArchiSteamFarm/blob/master/.github/CONTRIBUTING.md)**.

Se você quer a todo custo arriscar sua conta Steam pela coleta de algumas centenas de cartas mais rápido do que o habitual, então infelizmente o ASF nunca oferecerá algo assim no modo automático, embora você tenha o **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-pt-BR)** `play` que pode ser usado como uma ferramenta para fazer o que quiser em termos de interação com a rede Steam. Não recomendamos aproveitar falhas no Steam e explorá-las para seu ganho próprio - não só com o ASF, mas com qualquer outra ferramenta também. Pois no fim, a conta é sua e é você que escolhe o que quer fazer com ela - só tenha em mente que nós avisamos você.
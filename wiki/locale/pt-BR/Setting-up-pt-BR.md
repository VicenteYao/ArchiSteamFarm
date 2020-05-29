# Primeiros passos

Se você está aqui pela primeira vez, bem-vindo! Estamos felizes em saber que outro viajante está interessado em nosso projeto, no entanto tenha em mente que grandes poderes trazem grandes responsabilidades; o ASF é capaz de fazer muitas coisas relacionadas ao Steam, mas somente enquanto você se **preocupar em aprender como usá-lo**. Há uma curva de aprendizagem aqui e esperamos que você leia a wiki a este respeito, que explica detalhadamente como tudo funciona.

Se você ainda está aqui significa que você suportou o texto acima, o que é bom. A não ser que você tenha pulado ele, então você vai ter alguns **[problemas](https://www.youtube.com/watch?v=WJgt6m6njVw)** logo mais... De qualquer forma, o ASF é um aplicativo de console, o que significa que o programa não tem uma interface amigável que você geralmente está acostumado. O ASF foi projetado principalmente para ser executado em servidores e, portanto, funciona como um serviço (daemon) e não como um aplicativo da área de trabalho.

Isso não significa que você não possa usá-lo no seu computador ou que o uso é mais complicado que o normal, não é nada disso. O ASF é um programa autônomo que funciona imediatamente e não necessita de instalação, mas necessita de uma configuração para se tornar útil. É a configuração que vai dizer ao ASF o que ele deve fazer depois que você executá-lo. Se você o iniciar sem configurar antes ele simplesmente não fará nada.

* * *

## Instalador para sistemas operacionais específicos

Em geral, é isso que vamos fazer nos próximos minutos:

- Instalar o **[.NET Core](#net-core-prerequisites)**.
- Baixar a **[última versão do ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** na versão correta para o seu SO.
- Extrair o arquivo em um novo local (e `chmod +x ArchiSteamFarm` se você usar Lunux/OS X).
- **[Configurar o ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-pt-BR)**.
- Executar o ASF e ver a mágica.

Parece simples o bastante, certo? Então, vamos ver.

* * *

### Pré-requisitos .NET Core

O primeiro passo é garantir que seu sistema operacional pode mesmo executar o ASF corretamente. O ASF é escrito em C#, com base no .NET Core e pode requerer bibliotecas nativas que ainda não estão disponíveis na sua plataforma. Os requisitos serão diferentes se você usa Windows, Linux ou OS X, embora todos estejam listados no documento **[pré-requisitos .NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)** que você deve seguir Esse é nosso material de referência e ele deve ser usado, mas para simplificar nós detalhamos todos os pacotes necessários abaixo para que você não precise ler todo o documento.

É perfeitamente normal que algumas dependências (ou mesmo todas) já tenham sido instaladas no seu sistema por algum outro software que você use. Ainda assim, você deve garantir executando o instalador apropriado para seu sistema operacional - sem essas dependências o ASF não vai nem iniciar.

Você não precisa fazer mais nada para as versões para SOs específicos, especialmente instalar o .NET Core SDK ou mesmo o tempo de execução, já que o pacote específico para SO já inclui tudo. Você precisa apenas dos pré-requisitos (dependências) .NET Core para rodar o tempo de execução .NET Core incluído no ASF.

#### **[Windows](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-windows)**:

- **<a href="[">Pacotes Redistribuíveis do Microsoft Visual C++ 2015 Update 3 RC](https://www.microsoft.com/pt-br/download/details.aspx?id=53587)** (x64 para Windows 64-bit, x86 para Windows 32-bit)
- É altamente recomendado garantir que todas as atualizações do Windows estejam instaladas. Você precisa pelo menos das atualizações **[KB2533623](https://support.microsoft.com/pt-br/help/2533623/microsoft-security-advisory-insecure-library-loading-could-allow-remot)** e **[KB2999226](https://support.microsoft.com/pt-br/help/2999226/update-for-universal-c-runtime-in-windows)**, mas outras atualizações podem ser necessárias. Todas elas já estarão instaladas se o seu Windows estiver atualizado. Certifique-se de que você atende a esses requisitos antes de instalar o pacote do Visual C++.

#### **[Linux](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-linux)**:

Os nomes dos pacotes dependem da distribuição do Linux que você esteja usando, nós listamos as mais comuns. Você pode obter todas elas com o gerenciador de pacotes nativo do seu SO (como `apt` para Debian ou `yum` por CentOS).

- `libcurl` (`libcurl4`, `libcurl3`)
- `libicu` (a ultima versão para a sua distribuição, por exemplo `libicu60`)
- `libkrb5-3` (`krb5-libs`)
- `liblttng-ust0` (`lttng-ust`)
- `libssl` (`libssl1.1`, `openssl-libs`, a última versão 1.1.X para a sua distribuição)
- `zlib1g` (`zlib`)

Pelo menos alguns desses já devem estar disponíveis nativamente no seu sistema (como o `zlib1g` que é exigido em quase todas as distribuições do Linux hoje em dia).

#### **[OS X](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-macos)**:

- Nenhum por enquanto, mas você deve ter a versão mais recente do OS X instalada, pelo menos 10.13+

* * *

### Baixando

Uma vez que já tenhamos todas as dependências, o próximo passo é baixar a **[última versão do ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**. ASF está disponível em diversas variantes, mas você está interessado no pacote que corresponde ao seu sistema operacional e a arquitetura dele. Por exemplo, se você estiver usando o `Win`dows `64`-bit, então você vai baixar o pacote `ASF-win-x64`. Para obter mais informações sobre as variantes disponíveis, visite a seção</strong> **[compatibilidade](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-pt-BR). O ASF é também capaz de rodar em SOs para os quais ele não tem um pacote específico, tal como o **Windows 32-bit**, vá até **[configuração genérica](#configuração-genérica)** para saber mais.</p> 

![Arquivos](https://i.imgur.com/Ym2xPE5.png)

Após o download, comece extraindo o arquivo zip para sua própria pasta. Recomendamos usar o **[7-zip](https://www.7-zip.org)**, utilitários padrão como o `unzip` do Linux/OS X também devem funcionar sem problemas. Depois, você terá uma enorme confusão de pastas e arquivos. Não se preocupe, nós vamos resolver isso em um segundo.

Se você estiver usando Linux/OS X não se esqueça do `chmod + x ArchiSteamFarm`, já que as permissões não são definidas automaticamente no arquivo zip. Isso tem que ser feito somente uma vez após descompactar.

Certifique-se de descompactar o ASF para a **sua própria pasta** e não para outra existente que você esteja usando para outra coisa - as atualizações automáticas do ASF vão excluir todos os arquivos velhos e não relacionados, o que vao fazer você perder qualquer coisa não relacionada que esteja na mesma pasta. Se você tiver qualquer scripts ou arquivos extras que você quer usar com o ASF, coloque-os uma pasta acima.

Uma exemplo de extrutura seria assim:

    C:\ASF (onde você pode colocar suas coisas)
        ├── ASF shortcut.lnk (opcional)
        ├── Config shortcut.lnk (opcional)
        ├── Commands.txt (opcional)
        ├── MyExtraScript.bat (opcional)
        ├── ... (qualquer outro arquivo que você quiser, opcional)
        └── Core (dedicado apenas ao ASF, onde você vai extrair o arquivo)
             ├── ArchiSteamFarm.dll
             ├── config
             └── (...)
    

Essa também é a estrutura que recomendamos, então você não precisa passar por um grande número de arquivos e pastas incluídas no ASF, já que para o uso você só precisa de um atalho para a pasta de configuração e para o executável principal.

Vamos preparar a estrutura do ASF para ser usada. Caso você queira, você pode pular essas etapas, já que preparar a estrutura do ASF não é necessária (especialmente se estiver usando uma build para um SO especifico que já estiver pronta), mas pode fazer sua vida um pouco mais fácil.

Você pode abrir o diretorio do ASF e achar os arquivos executaveis, eles são `ArchiSteamFarm.exe` no Windows, e `ArchiSteamFarm` no Linux/OS X. Botão direito e copie. Agora, navegue até o local que você quer colocar o atalho do ASF (por exemplo, sua área de trabalho), clique com o botão direito e escolha "colar atalho aqui". Você pode renomear o atalho se você quiser, dando-lhe o nome de "ASF" por exemplo. Agora faça o mesmo com a pasta `config` que você pode encontrar no mesmo lugar que o executável do ASF.

Após uma pequena limpeza você terá uma estrutura muito conveniente, similar a essa abaixo:

![Estrutura](https://i.imgur.com/k85csaZ.png)

Isso te permite acessar facilmente o executável do ASF e os arquivos de configuração sem muita trabalheira. No meu caso que eu decidi usar a estrutura mencionada acima, então meus arquivos do ASF estão diretamente dentro da pasta "Core". Você pode adaptar essa estrutura ao seu gosto, tal como colocar atalhos na área de trabalho para o ASF e para a pasta config e a pasta do ASF em `C:\ASF` por exemplo, isso fica a seu critério.

Aconselhamos o mesmo para usuários do Linux/OS X, você pode usar o mecanismo de links simbólicos disponível através de `ln -s`.

* * *

### Configuração

Agora estamos prontos para a última etapa, a configuração. Este é de longe o passo mais complicado, uma vez que envolve um monte de informações com as quais você ainda não está familiarizado, então vamos tentar fornecer alguns exemplos fáceis de entender e uma explicação simplificada.

Primeiro e mais importante, há uma página dedicada a **[configuração](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-pt-BR)** que explica **tudo** relacionado a isso, mas ela contém uma enorme quantidade de informações e a maioria não precisamos saber de imediato. Em vez disso, nós ensinaremos a você como obter as informações que você está procurando.

A configuração de ASF pode ser feita de duas maneiras - ou usando nosso gerador de configuração web, ou manualmente. Isto é explicado profundamente na seção **[configuração](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-pt-BR)**, consulte-a se você quer informações mais detalhadas. Nós vamos usaremos o gerador de configuração web, uma vez que é muito mais fácil.

Navegue até a página do nosso **[gerador de configuração web](https://justarchinet.github.io/ASF-WebConfigGenerator)** com o seu navegador favorito, você precisará ter o javascript habilitado no caso de você tê-lo desativado manualmente. Recomendamos o Chrome ou o Firefox, mas ele deve funcionar em todos os navegadores mais populares.

Depois de abrir a página, vá até a guia "Bot". Você verá uma página semelhante a mostrada abaixo:

![Aba bot](https://i.imgur.com/aF3k8Rg.png)

Se por acaso a versão do ASF que você acabou de baixar é mais velha que a versão que o gerador de configuração está definido para usar por padrão, escolha a sua versão ASF no menu suspenso. Isso acontece porque ele pode gerar configurações para as versões mais novas (pré-lançamentos) do ASF que ainda não foram marcadas como estáveis. Você certamente baixou a última versão estável do ASF, que é verificada para trabalhar de forma confiável.

Comece colocando o nome que você quer dar ao bot no campo marcado em vermelho. Pode ser qualquer nome que você quiser, tais como seu apelido, nome da conta, um número ou qualquer outra coisa. Há apenas uma palavra que você não pode usar: `ASF`, pois esse nome é reservado para o arquivo de configuração global. Além disso, o nome não pode começar com um ponto (o ASF ignora esses arquivos). Também recomendamos que você evite usar espaços, você pode usar `_` como espaçamento, se necessário.

Depois que você escolheu um nome, ative o botão `Enabled`, ele define se o ASF deve iniciar seu bot automaticamente quando ele (o programa) for aberto.

Agora você pode decidir sobre duas coisas:

- Você pode por seu login no campo `SteamLogin` e sua senha no campo `SteamPassword`
- Ou pode deixá-los em branco

A primeira vai permitir que o ASF use suas credenciais de conta automaticamente durante a inicialização, então você não precisará colocá-las manualmente cada vez que ASF necessite. Você pode, no entanto, decidir omiti-las e nesse caso elas não serão salvas, mas assim o ASF não será capaz de iniciar automaticamente sem a sua ajuda e você precisará entrar com esses durante o tempo de execução.

O ASF precisa de suas credenciais porque inclui a sua própria implementação do cliente Steam e precisa dos mesmos dados que você usa para se conectar. Suas credenciais de login não são salvas em nenhum lugar além da pasta `config` do ASF no seu próprio PC, nosso gerador de configuração web é baseado no cliente, o que significa que seu código roda localmente no seu navegador para gerar as configurações válidas, sem que os dados que você entra deixem seu PC de forma alguma, então não precisa se preocupar com qualquer possibilidade de vazamento de dados. Ainda assim, se por qualquer motivo você não quer colocar seus dados lá, nós entendemos, você pode colocá-los manualmente mais tarde nos arquivos gerados ou omiti-los totalmente e colocá-los somente no prompt de comando do ASF. Mais informações sobre segurança podem ser encontradas na seção **[configuração](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-pt-BR)**.

Você também pode optar por deixar apenas um campo vazio, como `SteamPassword` por exemplo, o ASF então será capaz de usar seu login automaticamente, mas ainda vai pedir a senha (semelhante ao cliente Steam). Se você usa o PIN do modo família para desbloquear a conta você precisará ira para as configurações avançadas e colocá-lo no campo `SteamParentalCode`.

Depois que você fizer suas decisões sobre dados opcionais, sua página estará semelhante a abaixo:

![Aba bot 2](https://i.imgur.com/yf54Ouc.png)

Agora você pode clicar em "baixar" e o gerador de configuração web vai gerar um arquivo `json` com o nome que você escolheu. Salve esse arquivo na pasta `config` do ASF. Você pode usar o atalho `config` criado anteriormente, ou encontrar a pasta `config` diretamente na estrutura de arquivos do ASF.

Sua pasta `config` ficará assim:

![Estrutura 2](https://i.imgur.com/crWdjcp.png)

Parabéns! Você acabou de terminar a configuração básica de um bot ASF. Nós vamos ampliar isso em breve, mas por enquanto isso é tudo o que você precisa saber.

* * *

### Executando o ASF

Agora você está pronto para abrir o programa pela primeira vez. Simplesmente clique duas vezes no atalho do ASF ou no executável `ArchiSteamFarm` na pasta ASF.

Depois disso, supondo que você instalou todas as dependências listadas na primeira etapa, o ASF deve iniciar corretamente, detectar seu primeiro bot (se você não se esqueceu de colocar o arquivo de configuração gerado na pasta `config`) e tentar se conectar:

![ASF](https://i.imgur.com/u5hrSFz.png)

Se você definiu o `SteamLogin` e o `SteamPassword` no arquivo de configuração, será solicitado apenas o seu token do SteamGuard (e-mail, 2FA ou nenhum, dependendo das configurações do Steam). Caso contrário o ASF também pedirá seu login e senha do Steam.

Agora é uma boa hora para rever a nossa seção de **[política de privacidade](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics#current-privacy-policy)** se você estiver preocupado com o que vai acontecer a seguir, como afirmado pelo ASF.

Após a etapa de conexão, supondo que seus dados estejam corretos, você vai entrar com êxito no Steam e o ASF começará a coleta automática, usando as configurações padrão que você ainda não alterou:

![ASF 2](https://i.imgur.com/Cb7DBl4.png)

Isso prova que o ASF está fazendo seu trabalho na sua conta, então você pode minimizá-lo e fazer outra coisa. Depois de decorrido algum tempo (dependendo do **[desempenho](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance)**) você verá as cartas aparecerem no seu inventário. É claro, para que isso aconteça você deve ter jogos válidos para coleta, eles são mostrados como "Jogo pode dar mais X cartas" na sua **[página de insígnias](https://steamcommunity.com/my/badges)**; se não houver nenhum jogo para coleta o ASF vai agir como se não houvesse nada a fazer, conforme mencionado em nossa seção de **[Perguntas Frequentes (FAQ)](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-pt-BR#o-que-%C3%A9-o-asf)**.

Isso conclui nosso guia de configuração básica. Agora você pode decidir se deseja configurar o ASF ainda mais ou deixá-lo fazer seu trabalho com as configurações padrão. Vamos cobrir mais alguns detalhes básicos e, em seguida, deixar toda a wiki para você descobrir.

* * *

### Configuração estendida

#### Coletando de várias contas ao mesmo tempo

O ASF suporta a coleta automática em mais de uma conta por vez, o que é sua principal função. Você pode adicionar mais contas ao ASF gerando mais arquivos de configuração de bot, exatamente da mesma forma que o primeiro foi gerado a poucos minutos atrás. Você precisa garantir apenas duas coisas:

- Um nome exclusivo para cada bot, se o seu primeiro bot se chama "ContaPrincipal", você não pode ter outro com o mesmo nome.
- Dados de login válidos, como `SteamLogin`, `SteamPassword,` e `SteamParentalCode` (se estiver usando o modo família do Steam)

Em outras palavras, simplesmente volte a configuração e faça exatamente a mesma coisa, só que dessa vez para a segunda ou terceira conta. Lembre-se de usar nomes exclusivos para todos os teus bots.

* * *

#### Alterando configurações

Você pode alterar as configurações existentes exatamente da mesma forma: gerando um novo arquivo de configuração. Se você não fechou o nosso gerador de configuração web ainda, clique em "Alternar configurações avançadas" e veja o que há para descobrir. Para este tutorial, vamos alterar a configuração `CustomGamePlayedWhileFarming`, que permite que você defina um nome personalizado para ser exibido no Steam quando o ASF está coletando em vez de mostrar o jogo real.

Então vamos lá, se você executar o ASF e iniciar a coleta, com as configurações padrão você vai ver que o Steam mostra "Em jogo":

![Steam](https://i.imgur.com/1VCDrGC.png)

Vamos mudar isso. Vá para as configurações avançada do gerador de configuração web e encontre `CustomGamePlayedWhileFarming`. Coloque no campo o texto que você quer mostrar, por exemplo: "Idling Cards":

![Aba bot 3](https://i.imgur.com/gHqdEqb.png)

Agora baixe o arquivo de configuração da mesma forma que antes e **substitua** o arquivo antigo por esse. Você também pode apagar o antigo e colocar esse novo no lugar.

Assim que fizer isso e abrir o ASF novamente você vai perceber que o Steam agora mostra seu texto:

![Steam 2](https://i.imgur.com/vZg0G8P.png)

Isso confirma que você editou sua configuração com sucesso. Da mesma forma você pode editar as configurações globais do ASF, apenas mudando da aba "Bot" para a aba "ASF", baixando o arquivo de configuração `ASF.json` e o colocando na pasta `config`.

* * *

#### Usando a ASF-ui

O ASF é um aplicativo de console e não inclui uma interface gráfica de usuário (GUI). No entanto, estamos trabalhando ativamente em nossa interface IPC, o **[ASF-ui](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-pt-BR#asf-ui)** que pode ser uma forma muito boa e amigável de acessar vários recursos do ASF.

Para usar a ASF-ui, você deve configurar `IPC` e `SteamOwnerID` nos parâmetros correspondentes na configuração global (na aba ASF).

Em `SteamOwnerID` você precisa colocar o ID Steam de 64-bits exclusivo da sua conta. Você pode encontrá-lo de várias formas, nós vamos usar o **[SteamRep](https://steamrep.com)**. Abra o site, encontre o botão "sign in through Steam" no canto superior direito e conecte-se. Depois, no mesmo lugar, clique no seu avatar e procure o campo `steamID64` em seu perfil.

![SteamRep](https://i.imgur.com/RUuJ63i.png)

Na minha conta, ele é o número `76561198006963719`. Você vai ter um código parecido, também começando com `7656`. Copie-o.

Agora navegue mais uma vez até nosso gerador de configuração web e coloque esse número em SteamOwnerID.

![SteamOwnerID](https://i.imgur.com/V6jslfQ.png)

Você precisa fazer só mais uma coisa: alternar para as configurações avançadas, encontrar o opção `IPC` e habilitá-la.

![IPC](https://i.imgur.com/NhujZCN.png)

Agora você pode baixar o arquivo de configuração e colocá-lo na pasta `config`. Aí é só abrir o ASF de novo e você verá que ele vai iniciar a interface IPC:

![IPC 2](https://i.imgur.com/ZmkO8pk.png)

Se você fez tudo corretamente, você será capaz de acessar a interface IPC do ASF **[nesse](http://localhost:1242)** link, enquanto ASF estiver em execução. Você pode usar a ASF-ui para muitas coisas, como por exemplo enviar **[comandos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-pt-BR)**. Sinta-se a vontade para dar uma olhada e descobrir todas as funcionalidade da ASF-ui.

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

Por favor, note que a ASF-ui está atualmente em fase de testes e nem tudo está disponível/funcionando ainda, mas é mais que o suficiente para o uso básico do ASF.

* * *

### Resumo

Você já configurou com sucesso o ASF para que ele use suas contas Steam e você já o customizou um pouco a seu gosto. Se você seguiu o guia inteiro, então você até conseguiu enviar um comando simples através da ASF-ui. Agora é uma boa hora para ler a seção completa de **[configuração](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-pt-BR)** para aprender sobre todos os outros parâmetros que você viu na guia de configuração avançada e ver o que o ASF pode oferecer. Se você de deparou com algum problema ou você tem alguma pergunta genérica, leia a seção de **[perguntas frequentes (FAQ)](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-pt-BR)** que deve cobrir tudo, ou pelo pelo menos a maioria das perguntas que você possa ter. Se você quer aprender tudo sobre o ASF e como le pode tornar sua vida mais fácil, visite o resto da nossa **[wiki](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home-pt-BR)**. Divirta-se!

* * *

## Configuração genérica

Esta configuração é para usuários avançados que desejam configurar a versão **[genérica](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#generic)** do ASF. Ela não é recomendada para pessoas que podem usar o **[instalador para sistemas operacionais específicos](#instalador-para-sistemas-operacionais-específicos)**.

Você vai precisar usar a variante genérica em três situações (mas é claro você pode usá-la independentemente disso):

- Quando você estiver usando um sistema operacional para o qual não temos um pacote específico (como o Windows 32-bit, por exemplo)
- Quando você já tem o .NET Core Runtime/SDK, ou deseja instalar e usar um
- Quando você deseja minimizar o tamanho da estrutura do ASF lidando com requisitos de tempo de execução você mesmo

No entanto, tenha em mente que nesre caso você é responsável pelo tempo de execução do .NET Core. Isto significa que se o seu .NET SDK (tempo de execução) estiver indisponível, desatualizado ou com erro, o ASF não funcionará. É por isso que não recomendamos essa configuração para usuários casuais, já que você agora precisa garantir que seu .NET Core SDK (tempo de execução) corresponda as exigências do ASF e pode executá-lo, ao contrário da **nossa** garantia de que o.NET Core empacotado junto com o ASF faça isso.

Para o pacote genérico você pode acompanhar o guia de instalação para sistemas operacionais inteiro acima, com duas pequenas alterações. Além de instalar os pré-requisitos .NET Core, você também vai precisar instalar o .NET Core SDK, e ao invés vez de ter o arquivo executável `ArchiSteamFarm(.exe)` específico para o seu sistema operacional, agora você tem um apenas um binário genérico `ArchiSteamFarm.dll`. Todo o resto permanece igual.

Com etapas extras:

- Instalar o **[.NET Core](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)**.
- Instalar o **[.NET Core SDK](https://www.microsoft.com/net/download)** (ou o tempo de execução mais recente) apropriado para seu sistema operacional. Você provavelmente vai desejar usar um instalador. Veja **[requisitos de tempo de execução](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements)** se você não tiver certeza de qual versão instalar.
- Baixe a **[última versão do ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)** na versão genérica.
- Extrair o arquivo em um novo local (e `chmod +x ArchiSteamFarm.sh` se você usar Lunux/OS X).
- **[Configurar o ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-pt-BR)**.
- Abra o ASF usando um script auxiliar ou executando `dotnet /path/to/ArchiSteamFarm.dll` manualmente pelo seu shell favorito.

Scripts de ajuda (como `ArchiSteamFarm.cmd` para Windows e `ArchiSteamFarm.sh` para Linux/OSX) estão juntos com o `ArchiSteamFarm.dll` - eles são inclusos apenas na variante genérica. Você pode usá-los se você não quer executar o comando `dotnet` manualmente. Você também pode criar um atalho para esses scripts como mostrado acima, uma vez que eles devem fornecer a substituição do executável em forma de script. Obviamente os scripts de ajuda não vão funcionar se você não instalou o SDK do .NET Core e não tem o executável `dotnet` disponível em seu `PATH`. Os scripts de ajuda são inteiramente opcionais, você pode sempre usar o método manual `dotnet /path/to/ArchiSteamFarm.dll`.
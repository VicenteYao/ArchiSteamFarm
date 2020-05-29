# Compilação

Compilação é o processo de criação de arquivo executável. É isso que você quer fazer se você quiser adicionar suas próprias mudanças ao ASF, ou se você, por alguma razão não confia em arquivos executáveis fornecidos em **[lançamentos](https://github.com/JustArchiNET/ArchiSteamFarm/releases)** oficiais. Se você é um usuário e não um desenvolvedor, é mais provável que você queira usar binários pré-compilados, mas se você quiser usar os seus próprios, ou aprender algo novo, continue a leitura.

O ASF pode ser compilado em qualquer plataforma suportada atualmente, desde que você tenha todas as ferramentas necessárias.

* * *

## .NET Core SDK

Independente da plataforma, você precisa do SDK completo do .NET Core (e não apenas o tempo de execução) em para compilar o ASF. Instruções de instalação podem ser encontradas na **[página de instalação do .NET Core](https://dotnet.microsoft.com/download)**. Você precisa instalar a versão apropriada do SDK do .NET Core para seu sistema operacional. Após a instalação bem sucedida, o comando `dotnet` deverá estar funcional e operante. Você pode verificar se ele funciona com `dotnet --info`. Certifique-se também de que o seu SDK do .NET Core corresponde aos **[requisitos de tempo de execução](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements)** do ASF.

* * *

## Compilação

Assumindo que você tenha o SDK do .NET Core na versão apropriada, simplesmente navegue para o diretório raiz do ASF (copiado ou baixado e descompactado do repositório do ASF) e execute:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/generic"
```

Se você estiver usando Linux/OS X, você pode usar o código `cc.sh`, que fará o mesmo de uma maneira um pouco mais complexa.

Se a compilação obteve sucesso você poderá encontrar a `source` da sua versão do ASF na pasta `out/generic`. Essa compilação é a mesma que a `genérica` do ASF, mas ela força o valor de `UpdateChannel` e `UpdatePeriod` para `0`, o que é o correto para versões auto compiladas.

### SO específico

Você também pode gerar um pacote .NET Core específico para OS se você tiver uma necessidade particular. Em geral, você não deverá fazer isso, pois você já compilou o tipo `genérico` que você pode rodar em seu já instalado tempo de execução .NET Core, que você usou para a compilação, mas caso você queira:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "netcoreapp3.1" -o "out/linux-x64" -r "linux-x64"
```

Claro, troque `linux-x64` pela arquitetura de SO que você quer atender, tal como `win-x64`. Essa compilação também terá as atualizações desabilitadas.

### .NET framework

Em casos muito raros, quando você quiser compilar um pacote `generic-netf`, você pode mudar a estrutura desejada de `netcoreapp3.1` para `net48`. Tenha em mente que você vai precisar do pacote de desenvolvedor **[.NET Framework](https://dotnet.microsoft.com/download/visual-studio-sdks)** apropriado para compilar a variante `netf`, além do SDK do .NET Core, então a instrução abaixo funcionará apenas no Windows:

```shell
dotnet publish ArchiSteamFarm -c "Release" -f "net48" -o "out/generic-netf"
```

No caso de você não conseguir instalar o .NET Framework ou mesmo o próprio SDK do .NET Core (p. ex., por estar compilando no `linux-x86` com `mono`), você pode chamar `msbuild` diretamente. Você também precisará especificar o `ASFNetFramework` manualmente, já que o ASF desativa por padrão a compilação netf em plataformas não-Windows:

```shell
msbuild /m /r /t:Publish /p:Configuration=Release /p:TargetFramework=net48 /p:PublishDir=out/generic-netf /p:ASFNetFramework=true ArchiSteamFarm
```

* * *

## Desenvolvimento

Se você gostaria de editar o código do ASF, você pode usar qualquer IDE compatível com o .NET Core, embora até mesmo isso seja opcional, uma vez que você pode editar em um bloco de notas e compilar com o comando `dotnet` descrito acima. Ainda assim, para Windows, recomendamos **[o Visual Studio mais recente](https://visualstudio.microsoft.com/downloads)** (a versão gratuita community é mais que o suficiente). Também sugerimos usá-lo juntamente com o **[ReSharper](https://www.jetbrains.com/resharper)** (opcionalmente), embora este não seja um produto gratuito.

Se, em vez disso, você quiser trabalhar com o código ASF no Linux/Mac OS X, recomendamos o **[Visual Studio Code mais recente](https://code.visualstudio.com/download)**. Ele não é tão rico quanto o Visual Studio clássico, mas é bom o suficiente.

Claro que todas as sugestões acima são apenas recomendações, você pode usar o que quiser, tudo se resume ao comando `dotnet build` de qualquer maneira. Nós utilizamos o Visual Studio + ReSharper para o desenvolvimento do ASF, com uma pequena parte de `ferramentas` de terceiros que você pode encontrar no repositório.

* * *

## Marcadores

Não é garantido que a ramificação `master` esteja em um estado que propicie uma compilação bem sucedida ou uma execução sem falhas do ASF, uma vez que é uma ramificação em desenvolvimento, confirme especificado em nosso **[ciclo de lançamentos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle)**. Se você deseja compilar ou referenciar o ASF desde a fonte, então você deve usar a **[tag](https://github.com/JustArchiNET/ArchiSteamFarm/tags)** apropriada para tal, o que garante ao menos uma compilação bem sucedida, e muito provavelmente uma execução sem erros (se a compilação foi marcada como versão estável). Para verificar a "saúde" atual da árvore, você pode usar nossos CIs - **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** ou **[Travis](https://travis-ci.com/JustArchiNET/ArchiSteamFarm)**.

* * *

## Versões oficiais

Os lançamentos oficiais do ASF são compilados pelo **[AppVeyor](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)** no Windows, com o SDK .NET Core mais recente que corresponde com os **[requisitos de tempo de execução](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#runtime-requirements)** do ASF. Depois de passar nos testes, todos os pacotes são implantados no GitHub. Isto também garante transparência, uma vez que o AppVeyor sempre usa uma fonte pública oficial para todas as compilações, e você pode comparar as somas de verificação dos artefatos do AppVeyor com os ativos no GitHub. Os desenvolvedores do ASF não compilam ou publicam as compilações por conta própria, exceto para o processo de desenvolvimento privado e depuração.
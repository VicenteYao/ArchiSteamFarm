# Docker

A partir da versão 3.0.3.2, o ASF também está disponível como um **[container docker](https://www.docker.com/what-container)**. Executar o ASF em um contêiner docker normalmente não tem vantagens para usuários casuais, mas pode ser uma excelente forma de usar o ASF em servidores, garantindo que a ASF esteja sendo executado em um ambiente restrito, separado de todos os outros aplicativos. Nosso repositório docker pode ser encontrado **[aqui](https://hub.docker.com/r/justarchi/archisteamfarm)**.

* * *

## Marcadores

O ASF está disponível 4 tipos **[marcadores](https://hub.docker.com/r/justarchi/archisteamfarm/tags)** principais:

### `master`

Esse marcador sempre aponta para a compilação do ASF no "commit" mais recente do ramo master, que funciona da mesma forma que a compilação experimental do AppVeyour descrita em nosso **[ciclo de lançamento](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle)**. Normalmente você deve evitar essa versão, já que ela contém o nível mais elevado de software com erros, dedicado para desenvolvedores e usuários avançados para fins de desenvolvimento. A imagem é atualizada a cada "commit" no ramo master do GitHub, portanto você pode esperar por muitas atualizações (e coisas falhando), assim como em nossa compilação AppVeyour. Esse marcador está aqui para anotarmos o estado atual do projeto ASF, que não tem necessariamente garantia de ser estável ou testado, como salientado no nosso ciclo de lançamento. Esse marcador não deve ser usado em nenhum ambiente de produção.

### `released`

Muito semelhante ao anterior, esse marcador sempre aponta para a **[versão](https://github.com/JustArchiNET/ArchiSteamFarm/releases)** mais recente do ASF, incluindo pré-lançamentos. Comparado ao marcador `master`, essa imagem é atualizada toda vez que um novo marcador é criado no GitHub. Dedicado a usuários avançados que gostam de viver no limite do que pode ser considerado estável e mais novo ao mesmo tempo. Esse é o marcador que recomendamos se você não quer usar o `latest`. Observe que usar esse marcador é igual a usar o **[pre-lançamentos](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle)**.

### `latest`

Em comparação com os demais marcadores, esse é o único que contém o recurso de atualizações automáticas do ASF e normalmente aponta para aquela versão considerada estável, e não necessariamente a mais recente. O objetivo desse marcador é fornecer um contêiner Docker padrão que é capaz de executar a atualização automática do ASF na versão específica de OS. Por conta disso, a imagem não precisa ser atualizada tão frequentemente quanto possível, já que a versão inclusa do ASF será capaz de se auto atualizar sempre que preciso. Claro, `UpdatePeriod` pode ser desabilitado com segurança (definido como `0`), mas neste caso você provavelmente deverá usar a versão congelada `A.B.C.D`. Da mesma forma, você pode modificar o `UpdateChannel` padrão para o canal de atualização automática `released`.

Uma vez que a imagem da tag `latest` vem com capacidade de atualização automática, ela inclui apenas versão da SO determinada na versão especifica para SO, ao contrário de todas as outras tags que incluem a SO com tempo de execução .NET Core principal e versão a genérica do ASF. Isso acontece porque a versão mais recente do ASF (atualizada) também pode exigir um tempo de execução mais recente do que aquele com que a imagem possivelmente pode ter sido compilada, o que exigiria que a imagem fosse reconstruída do zero, anulando o tipo de uso planejado.

### `A.B.C.D`

Em comparação com os marcadores acima, esse marcador é completamente congelado, o que significa que a imagem não será atualizada uma vez que foi lançada. Ele funciona de forma semelhante as nossas versões do GitHub que nunca mais foram tocadas após o lançamento, o que te garante um ambiente estável e congelado. Normalmente você deverá usar esse marcador quando você quer usar uma versão específica do ASF (mais antiga que a `latest`) e você não quer usar nenhum tipo de atualização automática (por exemplo, as oferecidas no marcador `latest`).

* * *

## Qual o melhor marcador para mim?

Isso depende do que você procura. Para a maioria dos usuários o marcador `latest` deve ser o melhor, uma vez que ele oferece exatamente a mesma coisa que o ASF da área de trabalho oferece, com a diferença do serviço especial do contêiner Docker. Pessoas que recompilam suas imagens com frequência e prefeririam ter sua versão do ASF amarrada a determinada versão são bem vindas a usar o marcador `released`. Se, ao invés disso, você preferir usar uma versão congelada específica do ASF que nunca vai mudar sem a sua intenção, as versões `A.B.C.D.` estão disponíveis para você como marcas as quais você sempre pode voltar.

Nós geralmente desencorajamos o uso de compilações `master`, assim como compilações automatizadas do AppVeyour; essas compilações estão aqui para marcarmos o estado atual do projeto ASF. Nada garante que tal versão vai funcionar corretamente, mas é claro, você é mais que bem vindo para fazer um teste se estiver interessado no desenvolvimento do ASF.

* * *

## Arquiteturas

A imagem docker do ASF está disponível atualmente para 3 arquiteturas: `x64`, `arm` e `arm64`. Você pode ler mais sobre elas em **[estatísticas](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-pt-BR)**.

Uma vez que os marcadores docker multi-arquiteturas ainda são um trabalho em andamento, compilações diferentes do padrão `x64` atualmente estão disponíveis com `-{ARCH}` no nome. Eu outras palavras, se você quer usar o marcador `latest` para a arquitetura `arm`, simpesmente use `latest-arm`.

* * *

## Uso

Para uma referência completa você deve usar a **[documentação docker oficial](https://docs.docker.com/engine/reference/commandline/docker)**, nós cobriremos apenas o uso básico nesse guia, você é mais que bem vindo a pesquisar mais a fundo.

### Olá ASF!

Primeiro devemos verificar se nosso docker está funcionando corretamente, isso funcionará como uma versão do ASF do "olá mundo":

```shell
docker pull justarchi/archisteamfarm
docker run -it --name asf --rm justarchi/archisteamfarm
```

O comando `docker pull` garante que você está usando uma versão atualizada da imagem `justarchi/archisteamfarm`, para o caso de você ter uma cópia local desatualizada no seu cache. `docker run` cria um novo contêiner docker do ASF para você e o executa em primeiro plano (`-it`). `--rm` garante que nosso contêiner seja excluído assim que pare, já que estamos apenas testando por enquanto.

Se tudo correu bem, após receber todas as camadas e iniciar o contêiner, você deverá notar que o ASF foi iniciado e nos informou que não há bots definidos, o que é bom; nós acabamos de verificar que o ASF funcionou corretamente no docker. Aperte `CTRL+P` e então `CTRL+Q` para sair do contêiner docker em primeiro plano, então pare o contêiner ASF com o comando `docker stop asf`.

Se você olhar para o comando você vai notar que nós não declaramos nenhum marcador, que automaticamente usa o padrão `latest`. Se você quiser usar um marcador diferente, por exemplo `latest-arm`, então você deve declará-lo explicitamente:

```shell
docker pull justarchi/archisteamfarm:latest-arm
docker run -it --name asf --rm justarchi/archisteamfarm:latest-arm
```

* * *

## Usando um volume

Se você estiver usando o ASF em um contêiner docker, então obviamente você precisa configurar o programa. Você pode fazer isso de diversas formas diferentes, mas a forma recomendada é criar a pasta `config` do ASF no computador local, então carregá-la como um volume compartilhado no contêiner docker do ASF.

Por exemplo, assumiremos que sua pasta config do ASF está no diretório `/home/archi/ASF/config`. Essa pasta contém o arquivo principal `ASF.json` bem como os bots que queremos executar. Agora tudo o que temos que fazer é anexar essa pasta como um volume compartilhado no nosso contêiner docker, onde o ASF espera sua pasta config (`/app/config`).

```shell
docker pull justarchi/archisteamfarm
docker run -it -v /home/archi/ASF/config:/app/config --name asf justarchi/archisteamfarm
```

É isso, agora nosso contêiner docker do ASF usará a pasta compartilhada com nosso computador local em modo de leitura e gravação, isso é tudo o que você precisa para configurar o ASF. Da mesma forma, você pode montar outros volumes que você gostaria de compartilhar com o ASF, tais como `/app/logs` ou `/app/plugins`.

Claro, essa é apenas uma forma específica de alcançar o resultado que queremos, nada te impede de, por exemplo, criar seu próprio `Dockerfile` que copie seus arquivos de configuração para a pasta `/app/config` dentro do contêiner docker do ASF. Estamos cobrindo apenas o uso básico nesse guia.

### Permissões de volume

O ASF roda sob o usuário `root` dentro de um contêiner por padrão. Isso não é um problema quanto a segurança, uma vez que já estamos dentro de um contêiner Docker, mas isso afeta o volume compartilhado uma vez que novos arquivos gerados normalmente pertencerão ao `root`, o que pode não ser desejado quando se usa um volume compartilhado.

O Docker te permite adicionar uma **[flag](https://docs.docker.com/engine/reference/run/#user)** `--user` no comando `docker run` para definir sob qual usuário padrão o ASF deve ser executado. Você pode verificar, por exemplo, seu `uid` e `gid` com o comando `id` e, em seguida, passar ao resto do comando. Por exemplo, se seu usuário alvo tem o valor 1000 de `uid` e `gid`:

```shell
docker pull justarchi/archisteamfarm
docker run -it -u 1000:1000 -v /home/archi/ASF/config:/app/config --name asf justarchi/archisteamfarm
```

Lembre-se que por padrão a pasta `/app` usada pelo ASF ainda pertence ao `root`. Se você executar ASF sob um usuário personalizado, então seu processo ASF não terá acesso de gravação aos seus próprios arquivos. Este acesso não é obrigatório para a operação, mas é crucial, por exemplo, para o recurso de atualizações automáticas. Para corrigir esse problema, basta alterar a propriedade de todos os arquivos ASF do padrão `root` para seu novo usuário personalizado.

```shell
docker exec -u root asf chown -hR 1000:1000 /app
```

Isso só tem que ser feito uma vez depois que você criou seu contêiner com `docker run`, e somente se você decidiu usar um usuário personalizada para o processo do ASF. Também não se esqueça de alterar o argumento `1000:1000` em ambos os comandos acima para o `uid` e `gid` sob o qual você deseja executar o ASF.

* * *

## Multiple instances synchronization

ASF includes support for multiple instances synchronization, as stated in **[compatibility](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** section. When running ASF in docker container, you can optionally "opt-in" into the process, in case you're running multiple containers with ASF and you'd like for them to synchronize with each other.

By default, each ASF running inside a docker container is standalone, which means that no synchronization takes place. In order to enable synchronization between them, you must bind `/tmp/ASF` path in every ASF container that you want to synchronize, to one, shared path on your docker host, in read-write mode. This is achieved exactly the same as binding a volume which was described above, just with different paths:

```shell
mkdir -p /tmp/ASF-g1
docker pull justarchi/archisteamfarm
docker run -v /tmp/ASF-g1:/tmp/ASF -v /home/archi/ASF/config:/app/config --name asf1 justarchi/archisteamfarm
docker run -v /tmp/ASF-g1:/tmp/ASF -v /home/john/ASF/config:/app/config --name asf2 justarchi/archisteamfarm
# And so on, all ASF containers are now synchronized with each other
```

We recommend to bind ASF's `/tmp/ASF` directory also to a temporary `/tmp` directory on your machine, but of course you're free to choose any other one that satisfies your usage. Each ASF container that is expected to be synchronized should have its `/tmp/ASF` directory shared with other containers that are taking part in the same synchronization process.

As you've probably guessed from example above, it's also possible to create two or more "synchronization groups", by binding different docker host paths into ASF's `/tmp/ASF`.

Mounting `/tmp/ASF` is completely optional and actually not recommended, unless you explicitly want to synchronize two or more ASF containers. We do not recommend mounting `/tmp/ASF` for single-container usage, as it brings absolutely no benefits if you expect to run just one ASF container, and it might actually cause issues that could otherwise be avoided.

* * *

## Argumentos da linha de comando

O ASF te permite passar **[argumentos de linha de comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-pt-BR)** no contêiner docker através de variáveis de ambiente. Você deve usar variáveis de ambiente específicas para os switches suportados, e `ASF_ARGS` para o resto. Isso pode ser feito com o switch `-e` adicionado ao `docker run`, por exemplo:

```shell
docker pull justarchi/archisteamfarm
docker run -it -e "ASF_CRYPTKEY=MyPassword" -e "ASF_ARGS=--process-required" --name asf justarchi/archisteamfarm
```

Esse comando passará seu argumento `--cryptkey` para o processo do ASF sendo executado dentro do contêiner docker, assim como outros argumentos. Claro, se você for um usuário avançado então você também pode mudar o `ENTRYPOINT` ou adicionar `CMD` e passar seus argumentos personalizados você mêsmo.

A menos que você deseje fornecer chaves de criptografia personalizadas ou outras opções avançadas, geralmente você não precisa incluir qualquer variável de ambiente em especial, já que nosso contêiner docker já está configurado para ser executado com opções padrões esperadas de `--no-restart` `--process-required` `--system-required`, então, como você pode ver nossos `ASF_ARGS` são redundantes nesse caso, e apenas `ASF_CRYPTKEY` é relevante.

* * *

## IPC

Para usar o IPC, em primeiro lugar você deve mudar a **[propriedade de configuração global](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-pt-BR#configuração-global)** `IPC` para `true`. Além disso, você **deve** modificar o endereço de escuta padrão `localhost`, já que o docker não pode rotear tráfego externo para a interface de loopback. Um exemplo de configuração que irá escutar em todas as interfaces é `http://*:1242`. Claro, você também pode usar ligações mais restritivas, tais como apenas rede local ou VPN, mas tem que ser uma rota acessível de fora; `localhost` não funciona, já que a rota está inteiramente dentro do computador de convidado.

Para obter o resultado descrito acima você deve usar uma **[configuração personalizada de IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-pt-BR#configuração-personalizada)**, como o exemplo abaixo:

```json
{
    "Kestrel": {
        "Endpoints": {
            "HTTP": {
                "Url": "http://*:1242"
            }
        }
    }
}
```

Depois de configurarmos o IPC em uma interface sem loopback, precisamos indicar ao docker para se conectar a porta `1242/tcp` do ASF com o argumento `-P` ou `-p`.

Por exemplo, este comando vai expor a interface do IPC do ASF para o computador host (somente):

```shell
docker pull justarchi/archisteamfarm
docker run -it -p 127.0.0.1:1242:1242 -p [::1]:1242:1242 --name asf justarchi/archisteamfarm
```

Se você definiu tudo corretamente, o comando `docker run` acima fará com que a interface **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-pt-BR)** funcione em seu computador host, no caminho padrão `localhost:1242` que agora é redirecionada para seu computador convidado. Também é bom notar que nós não expomos esta rota além disso, então a conexão só pode ser feita dentro do host docker, e, portanto, o mantém seguro.

* * *

### Exemplo completo

Combinando todo conhecimento acima, um exemplo de uma configuração completa ficaria assim:

```shell
docker pull justarchi/archisteamfarm
docker run -it -p 127.0.0.1:1242:1242 -p [::1]:1242:1242 -v /home/archi/asf:/app/config --name asf justarchi/archisteamfarm
```

This assumes that you'll use a single ASF container, with all ASF config files in `/home/archi/asf`. You should modify the config path to the one that matches your machine. Essa configuração também está pronta para o uso opcional do IPC se você decidiu incluir o arquivo `IPC.config` na sua pasta config com o conteúdo abaixo:

```json
{
    "Kestrel": {
        "Endpoints": {
            "HTTP": {
                "Url": "http://*:1242"
            }
        }
    }
}
```

* * *

## Dicas profissionais

Quando você já tem o contêiner docker do ASF pronto, você não precisa usar o comando `docker run` toda vez. Você pode parar/iniciar o contêiner docker do ASF facilmente com `docker stop asf` e `docker start asf`. Tenha em mente que se você não estiver usando o marcador `latest` então atualizar o ASF ainda trará a necessidade dos comandos `docker stop`, `docker rm`, `docker pull` e `docker run` novamente. Isso porque você deve recompilar seu contêiner à partir de uma imagem nova do ASF toda vez que você quiser usar a versão do ASF inclusa naquela imagem. Na tag `latest`, o ASF incluiu a capacidade de se atualizar automaticamente, então recompilar a imagem não é necessário para usar o ASF atualizado (mas ainda é uma boa ideia fazer isso de tempos em tempos para obter uma versão recente do tempo de execução .NET Core e do Sistema Operacional utilizados).

Como dito acima, em qualquer outro marcador que não seja o `latest`, o ASF não vai se atualizar automaticamente, o que significa que **você** é o responsável por usar uma verão atualizada do repositório `justarchi/archisteamfarm`. Isso traz muitas vantagens já que normalmente o aplicativo não precisa mudar seu código durante a operação, mas nós entendemos a conveniência de não precisar se preocupar com a versão do ASF em seu contêiner docker. Se você se importa com boas práticas e o uso correto do contêiner docker, sugerimos o marcador `released` ao invés de `latest`, mas se você não quer se preocupar com isso e só quer fazer o ASF tanto funcionar quanto se atualizar automaticamente, então `latest` serve.

Você normalmente deve rodar o ASF no contêiner docker com a configuração global `Headless: true`. Isso indicará explicitamente ao ASF que você não poderá inserir os dados ausentes e que ele não deverá solicitá-los. Claro, para a configuração inicial você deve deixar essa opção como `false` então você poderá configurar tudo facilmente, mas no longo prazo você normalmente não estará ligado diretamente ao console do ASF, portanto faz sentido informar o ASF sobre isso e usar o **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-pt-BR)** `input` caso seja necessário. Dessa forma o ASF não vai esperar infinitamente por um comando do usuário que não será inserido (e gastar recursos enquanto isso). Isso também permitirá que o ASF rode em modo não-interativo dentro do contêiner, o que é crucial, por exemplo, no que diz respeito a sinais de encaminhamento, tornando possível que o ASF feche com o comando `docker stop asf`.
# Segurança

## SteamPassword

O ASF suporta atualmente 4 tipos de senhas - `PlainText`, `AES`, `ProtectedDataForCurrentUser` e None (nenhuma) (`null` / `""`).

Para usar uma senha criptografada, você deve primeiro entrar no Steam como de costume com `PlainText`, e então gerar senhas criptografadas usando o **[comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-pt-BR)** `password`. Pegue o método de encriptação que você preferir e coloque a senha encriptada que você recebeu no parâmetro de configuração do bot `SteamPassword`, e não se esqueça de mudar `PasswordFormat` para combinar com o método de encriptação escolhido.

* * *

### PlainText (Texto sem formatação)

É a forma mais simples e menos segura de salvar uma senha, definido com o valor `0` em `PasswordFormat`. O ASF espera que o parâmetro `SteamPassword` seja um texto sem formatação: a senha usada para se conectar ao Steam na sua forma original. É o mais fácil de usar e 100% compatível com todas as configurações, portanto é o padrão.

* * *

### AES

Considerado seguro pelos padrões de hoje, a forma de armazenar as senhas **[AES](https://pt.wikipedia.org/wiki/Advanced_Encryption_Standard)** é definida como `1` em `PasswordFormat`. ASF espera que o parâmetro `SteamPassword` seja uma sequencia de caracteres **[codificada em base64](https://pt.wikipedia.org/wiki/Base64)**, resultando em uma matriz de bytes com criptografia AES após a tradução, que então deve ser descriptografada usando o **[vetor de inicialização](https://pt.wikipedia.org/wiki/Vetor_de_inicializa%C3%A7%C3%A3o)** e a chave de criptografia do ASF.

O método acima garante segurança enquanto o agressor não saiba a chave de criptografia embutida do ASF que está sendo usada para descriptografia e criptografia de senhas. O ASF permite que você especifique a chave através do **[argumento de linha de comando](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments-pt-BR)** `--cryptkey`, que você deve usar para segurança máxima. Se você decidir para omiti-lo, o ASF usará sua própria chave, que é **conhecida** e codificada no aplicativo, ou seja, qualquer um pode reverter a criptografia ASF e obter a senha descriptografada. Isso requer esforço e não é fácil de fazer, mas é possível e é por isso que você deve sempre que possível usar a encriptação `AES` com sua própria `--cryptkey` mantida em segredo. O método AES utilizado pelo ASF fornece segurança suficiente e é um equilíbrio entre a simplicidade do `PlainText` e a complexidade do `ProtectedDataForCurrentUser`, mas é altamente recomendado usá-lo com uma `--cryptkey` personalizada.

* * *

### ProtectedDataForCurrentUser

Atualmente a maneira mais segura de armazenar a senha que o ASF oferece, e muito mais segura que o `AES` explicado acima, é definido com o valor `2` em `PasswordFormat`. A maior vantagem deste método é ao mesmo tempo a maior desvantagem - ao invés de usar uma chave de criptografia (como no `AES`), os dados são criptografados usando credenciais de login do usuário conectado no momento, o que significa que **só** é possível descriptografar os dados na máquina em que eles foram criptografados e, além disso, **somente** pelo usuário que emitiu a criptografia. Isso garante que mesmo que você envie seu arquivo `Bot.json` inteiro para outra pessoa, ele não será capaz de descriptografar a senha sem acessar o seu PC. Esta é uma medida de segurança excelente, mas ao mesmo tempo tem a grande desvantagem de ser menos compatível, já que a senha criptografada usando este método será incompatível com qualquer outro usuário, bem como outro computador - incluindo o **seu próprio** se você decidir, por exemplo, reinstalar seu sistema operacional. Ainda assim, é um dos melhores métodos de armazenamento de senhas, e se você está preocupado com a segurança do `PlainText`e não quer colocar senha toda vez com a opção `None`, então essa é sua melhor aposta, desde que você não precise acessar suas configurações de qualquer outro computador que não seja o seu.

**Por favor, note que esta opção está disponível apenas para computadores que executam o Windows.**

* * *

### None (nenhum)

A única forma de garantir 100% de segurança e ter certeza de que ninguém vai roubar sua senha Steam. Para utilizar esta opção simplesmente defina o parâmetro `SteamPassword` como vazio (`""`) ou `null`. O ASF vai pedir sua senha Steam quando for necessário, e não a salvará em lugar algum, mas a manterá na memória do processo executado no momento, até que você o feche. Enquanto sendo o método mais seguro de lidar com senhas, é também o mais problemático já que você tem que entrar com sua senha manualmente cada vez que abrir o ASF (quando for necessário). Se isso não for um problema para você, então é sua melhor aposta em termos de segurança.

* * *

## Recomendação

Se compatibilidade não é um problema para você, e você se sente tranquilo com a forma que o método `ProtectedDataForCurrentUser` funciona, é esse o método **recomendado** para salvar suas senhas no ASF, já que ele fornece a melhor segurança. O método `AES` é uma boa escolha para as pessoas que querem usar suas configurações em mais de um computador, enquanto `PlainText` é a forma mais simples de salvar a senha, se você não se importar que qualquer um pode pegá-la no arquivo JSON.

Tenha em mente que todos esses 3 métodos são considerados **inseguros** se um atacante tiver acesso ao seu PC. O ASF deve ser hábil a descriptografar a senha criptografada, e se o programa é capaz de fazer isso em seu computador, então qualquer outro programa rodando no mesmo computador também será capaz disso. `ProtectedDataForCurrentUser` é a variante mais segura já que **mesmo outro usuário usando o mesmo PC não será capaz de descriptografá-lo**, mas ainda é possível descriptografar os dados se alguém for capas de roubar suas credenciais de login e informações do seu computador, juntamente com o arquivo de configuração do ASF.

Para pessoas que raramente rodam o ASF ou pessoas que não se importam em entrar com a senha toda vez que abre o ASF, usar o `None` é a forma mais segura já que a senha Steam não é salva em lugar algum.

* * *

# Descriptografar

O ASF não suporta nenhuma forma de descriptografar senha já criptografadas, já que os métodos de descriptografia são usados internamente para acessar os dados dentro do processo. Se você quiser reverter o processo de criptografia, por exemplo, para mover o ASF para outro computador, quando estiver usando `ProtectedDataForCurrentUser`, então simplesmente mude `PasswordFormat` para `0` novamente (PlainText) e preencha `SteamPassword` adequadamente. Então você pode abrir o ASF como de costume e repetir o processo desde o início.
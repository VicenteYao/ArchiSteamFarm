# 安全性

## SteamPassword

ASF 目前支持 4 种类型的密码——`PlainText`、`AES`、`ProtectedDataForCurrentUser` 和 None（`null` / `""`）。

如要使用加密的密码，您应该先像往常一样以 `PlainText` 形式的密码登录 Steam，然后通过 `password` **[命令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-zh-CN)**&#8203;生成加密的密码。 选择您喜欢的加密方式，然后将您获得的加密后密码填入 `SteamPassword` 机器人配置属性，并且不要忘记对应修改 `PasswordFormat` 为符合加密方式的选项。

* * *

### PlainText

这是最简单但也不安全的存储密码的方式，定义 `PasswordFormat` 为 `0`。 此时 ASF 需要 `SteamPassword` 属性为纯文本——即登录 Steam 所需密码的原文。 它是最容易使用的，并且与所有设置 100% 兼容，因此它是默认值。

* * *

### AES

按照当今的标准，**[AES](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard)** 可以被视为安全，将 `PasswordFormat` 设置为 `1` 即可启用这种加密存储密码。 ASF 需要 `SteamPassword` 属性是一个由 AES 加密的字节数组转换成的 **[Base64 编码](https://en.wikipedia.org/wiki/Base64)**&#8203;的字符序列，此数据需要其包含的&#8203;**[初始向量](https://en.wikipedia.org/wiki/Initialization_vector)**&#8203;和 ASF 内置加密密钥来解密。

此方法保证了安全性，只要攻击者不知道用于加密和解密的 ASF 内置密钥。 ASF 允许您通过 `--cryptkey` **[命令行参数](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments-zh-CN)**&#8203;指定自定义密钥增强 ASF 的安全性。 如果您决定省略它，ASF 将使用自己提供的密钥，这个密钥是**已知**的并已硬编码到应用程序中，这意味着任何人都可以撤消 ASF 的加密并获取解密后的密码。 虽然这种攻击仍然需要一定时间而且并不容易，但是这是可以做到的。所以您总应该同时使用 `AES` 加密并用 `--cryptkey` 指定自定义密钥。 ASF 使用的 AES 方法提供了相对令人满意的安全性，并且它在 `PlainText` 的简单和 `ProtectedDataForCurrentUser` 的复杂之间取得了平衡，但强烈建议您将它与自定义密钥 `--cryptkey` 一起使用。

* * *

### ProtectedDataForCurrentUser

这是目前 ASF 存储密码最安全的方式，比上述 `AES` 加密安全得多，您需要将 `PasswordFormat` 设置为 `2`。 这种方法的主要优点同时也是它主要的缺点——它并不使用加密密钥（像 `AES` 那样），数据会使用当前计算机登录的用户凭据加密，这意味着数据**仅**能在这台机器上进行解密，事实上，**仅仅**触发加密的计算机用户才能解密。 这保证了即使您将整个 `Bot.json` 发送给其他人，他也无法在不接触您的计算机的情况下获得密码。 这是非常优秀的安全措施，但也有兼容性差的缺点，因为使用此方法加密的密码将不能兼容其他任何用户和计算机——假设您需要重新安装操作系统，这其中甚至包括**您自己的**计算机。 不过，这仍然是存储密码的最佳方法之一，如果您担心 `PlainText` 的安全性，也不想因为设置 `None` 选项而每次输入密码，那么只要您不会在其他机器上使用您的配置文件，这就是您最好的选择。

**请注意，此选项仅适用于运行 Windows 操作系统的计算机。**

* * *

### None

这是保证 100% 安全，确保没有人可以窃取您的 Steam 密码的唯一方法。 为了使用这一选项，只需要将 `SteamPassword` 留空（`""`）或设置为 `null` 值。 ASF 将会在需要时向您询问 Steam 密码，并且不会将其保存在任何地方，仅仅临时存放在当前进程分配的内存中，一旦您关闭 ASF 就会消失。 随着这是处理密码的最安全的方法，但也是最麻烦的，因为您需要在每次 ASF 运行时手动输入密码（如果需要）。 如果这对您来说不是问题，这就是您在安全方面的最佳选择。

* * *

## 建议

如果兼容性对您来说不是问题，并且您可以接受 `ProtectedDataForCurrentUser` 方式，我们**推荐**您以这种方式存储密码，因为它有着最好的安全性。 对于还需要在其他计算机上使用配置文件的用户来说，`AES` 方法也是一个不错的选择。而 `PlainText` 是存储密码最简单的方法，只要您不介意其他人可能会查看您的 JSON 配置文件。

请注意，如果入侵者能够访问您的计算机，上述 3 种方法都**不安全**。 ASF 必须能够解密已加密的密码，如果在您的计算机上运行的某个程序能够做到这一点，那么在同一计算机上运行的其他程序也能做到这一点。 `ProtectedDataForCurrentUser` 是其中最安全的方法，**即使使用同一台计算机的其他用户也无法解密**，但如果有人窃取了您的登录凭据、计算机信息和 ASF 配置文件，他仍然有可能解密出您的密码。

对于很少启动 ASF 的用户，或者不会因为每次启动输入密码而烦恼的用户，使用 `None` 方式最安全，因为 Steam 密码不会保存在任何地方。

* * *

# 解密

ASF 不支持任何解密已加密密码的方法，因为解密方法仅在内部使用，用于访问进程内的数据。 如果您需要反转加密过程，例如，在使用 `ProtectedDataForCurrentUser` 加密的情况下，将 ASF 迁移到另一台机器，只需要将 `PasswordFormat` 更改回 `0`（PlainText），然后重新在 `SteamPassword` 中填写对应的密码。 您可以像平常一样启动 ASF，然后重做上述的加密过程。
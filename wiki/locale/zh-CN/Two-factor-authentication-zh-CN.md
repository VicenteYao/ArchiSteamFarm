# 两步验证

此前，Valve 引入了交易暂挂系统，要求用户使用额外的验证器，才能进行很多与帐户有关的操作。 您可在&#8203;**[此处](https://support.steampowered.com/kb_article.php?ref=1284-WTKB-4729)**&#8203;与&#8203;**[此处](https://support.steampowered.com/kb_article.php?ref=8078-TPHC-6195)**&#8203;了解更多详情。 在理解 ASF 两步验证器背后的逻辑之前，至关重要的是先了解两步认证系统。

目前，所有的交易可被暂挂至最多 15 天。虽然对 ASF 来说这并不是大问题，但是仍然很烦人，尤其是对想要完全自动化的用户而言。 幸运的是，ASF 拥有对此问题的解决方案，名为 ASF 两步验证（ASF 2FA）。

* * *

# ASF 逻辑

无论您是否使用下方所述的 ASF 两步验证，ASF 都包含正确逻辑且完全了解如何处理由标准两步验证器所保护的帐户。 它将在有需要的时候（如登录时）向您请求所需的详细信息。 若您使用 ASF 两步验证，程序将跳过这些请求并自动生成所需的令牌，为您免除麻烦并启用额外功能（下文所述）。

* * *

# ASF 两步验证

ASF 2FA 是为 ASF 进程提供 2FA 特性支持的内部模块，包括生成令牌和确认交易。 它可以复制您已有的验证器，所以您无需仅单独使用 ASF 2FA。

您可以执行 `2fa` **[命令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-zh-CN)**&#8203;来验证机器人帐户是否已经启用 ASF 2FA。 除非您已经将验证器导入为 ASF 2FA，否则所有 `2fa` 命令都是无效的，这意味着您的帐户没有启用 ASF 2FA，因此一些需要此模块的 ASF 高级功能也无法正常运行。

要启用两步认证，您需要：

- Android 设备上可用的 Steam 身份验证器
- 或 iOS 设备上可用的 Steam 身份验证器
- 或 **[SteamDesktopAuthenticator](https://github.com/Jessecar96/SteamDesktopAuthenticator)**
- 或使用 **[WinAuth](https://winauth.github.io/winauth)** 实现的 Steam 身份验证器
- 或任何其他能够获取到 shared secret、identity secret 和 device id 的可用 Steam 验证器实现。

* * *

## 导入

为了完成以下步骤，您应该已经拥有且绑定了受 ASF 支持的可用验证器。 ASF 支持导入不同来源的两步验证——Android、iOS、SteamDesktopAuthenticator 和 WinAuth。 如果您还没有任何验证器，就需要选择上述验证器之一，并首先设置好它。 如果您不知道选择哪个更好，我们推荐 WinAuth，但只要您正确按照说明操作，上述的任何一个验证器都可以正常工作。

以下所有指南都需要您在指定的工具/应用中已有**正常工作**的验证器。 如果导入了无效数据，ASF 2FA 将无法正常运行，因此在尝试导入之前，请确保您的验证器正常工作。 这包括测试和验证以下验证器功能是否正常工作：

- 您可以生成令牌，并且 Steam 网络接受这些令牌
- 您可以获取交易确认，并且您的手机验证器也可以收到这些确认
- 您可以接受这些交易确认，并且 Steam 网络能够正确将它们识别为已接受/已拒绝

检查上述操作是否正常来确保您的验证器正常工作——如果不正常，它们也不会在 ASF 中正常运作，不仅浪费您的时间还会带来麻烦。

* * *

### Android 手机

一般情况下，从 Android 手机导入验证器需要您拥有 **[root](https://en.wikipedia.org/wiki/Rooting_(Android_OS))** 权限。 不同的设备有不同的 root 方法，所以我无法告诉您如何 root 您的设备。 您可以访问 **[XDA](https://www.xda-developers.com/root)** 查找相关的指南，以及关于 root 的一般信息。 如果您找不到适合您设备的指南，可以再尝试在搜索引擎中搜索。

至少从官方角度来说，没有 root 权限就无法访问受保护的 Steam 文件。 目前唯一正式的无需 root 的提取 Steam 文件的方法是以某种方法制作一份未加密的 `/data` 的备份，然后在 PC 上手动提取所需的文件，但这种功能在很大程度上依赖于您的手机制造商，并且**不属于** Android 标准，因此我们不会在此讨论该方法。 如果您的设备很幸运地支持这样的功能，您可以使用它，但大多数用户没有这样的机会。

也有无需 root 权限的非正式方法，即安装或者降级 Steam 应用为 2.1 或者更旧版本，在此版本绑定手机验证器，然后通过 `adb backup` 命令创建一份应用的快照（包含我们所需的 `data` 文件）。 然而，由于该方法属于严重的安全漏洞以及不受支持的提取文件的方式，我们不会进一步详述这一点，Valve 在新版本中禁用了这个安全漏洞，我们在此提及此方法仅仅是把它作为一种可能性。

假设您已经成功 root 了您的手机，接下来您应该在应用商店下载任何一款 root 文件管理器，例如&#8203;**[这个](https://play.google.com/store/apps/details?id=com.jrummy.root.browserfree)**（或者任何您喜爱的类似应用）。 您也可以通过 ADB（Android Debug Bridge，Android 调试桥）或者任何其他可用的方法访问受保护的文件，我们采用文件管理器是因为这是对大多数用户最友好的方式。

打开 root 文件管理器之后，前往 `/data/data` 文件夹。 请注意，`/data/data` 文件夹受保护，如果您没有 root 权限则无法访问它。 然后，找到 `com.valvesoftware.android.steam.community` 文件夹，将它复制到您的 `/sdcard` 文件夹，也就是您的内置存储设备。 之后，您应该可以将手机连接到 PC，将上述文件夹从内置存储设备复制到 PC 上。 如果您无法看见这个文件夹，但您确定您已经将它复制到了正确的位置，请先尝试重新启动手机。

现在，您可以选择先将验证器导入到 WinAuth，再导入到 ASF，或者直接导入到 ASF。 第一个选项更友好，使您在 PC 上也复制一份验证器，这样您就有 3 种方式确认交易和生成令牌——您的手机、您的 PC 和 ASF。 如果您要这样做，只需要打开 WinAuth，添加新的 Steam 验证器，然后选择从 Android 设备导入，按照屏幕上的指示访问之前您获取到的文件。 完成后，您可以从 WinAuth 将验证器导入 ASF，详见下文 WinAuth 一节。

如果您不想这样做，或者只是不想用 WinAuth，则可以直接从受保护文件夹中复制 `files/Steamguard-SteamID` 文件，其中 `SteamID` 是您要添加的帐户的 64 位 ID（可能有多个文件，如果只有一个帐户则只有一个文件）。 您需要将这个文件放入 ASF 的 `config` 文件夹。 完成这一步之后，将此文件重命名为 `BotName.maFile`，其中 `BotName` 是您需要导入 ASF 2FA 的机器人名称。 在这一步之后，运行 ASF——它将会发现 `.maFile` 文件并导入。

    [*] INFO: ImportAuthenticator() <1> 正在将 .maFile 转换为 ASF 格式……
    <1> 请输入您的设备 ID (包括"android:"):
    

您还需要一步操作——在 `shared_prefs/steam.uuid.xml` 文件中找到您的 `DeviceID` 属性值。 这个值应该在 XML 标签中，以 `android:` 开头。 复制它（或者记下来）并将其输入 ASF。 如果您的操作完全正确，导入过程应该已完成。

    [*] INFO: ImportAuthenticator() <1> 成功导入手机验证器！
    

请确认接受交易确认功能能够正常工作。 如果您在输入 `DeviceID` 时出错，您的验证器将只有一半功能正常工作——验证令牌正常，但无法接受交易确认。 您可以随时删除 `Bot.db` 文件以重新开始这个过程。

* * *

### iOS

对于 iOS 设备，您可以使用 **[ios-steamguard-extractor](https://github.com/CaitSith2/ios-steamguard-extractor)**。 其原理是您可以制作一份解密后的备份，将其放到 PC 上，用这个工具从中解出 Steam 数据，这些数据原本是无法获得的（因为 iOS 的加密机制，至少在不越狱的情况下不行）。

前往&#8203;**[发布页面](https://github.com/CaitSith2/ios-steamguard-extractor/releases/latest)**&#8203;下载此程序的最新版本。 在您提取数据之后，可以将其导入 WinAuth，然后再从 WinAuth 导入 ASF（但您也可以将生成的 json 文件中从 `{` 到 `}` 的部分复制到 `BotName.maFile` 文件中再继续）。 如果您问我的看法，我强烈建议先导入 WinAuth，然后确认生成令牌和交易确认都能够正常工作，这样您就可以确保之前的操作没有问题。 如果您的凭据无效，ASF 2FA 将不会正常工作，所以最好将 ASF 的导入步骤放在最后。

如果有问题，请访问 **[Issues](https://github.com/CaitSith2/ios-steamguard-extractor/issues)** 页面。

*请注意，上述工具是非官方的，您需要自行承担使用的风险。 如果它不能正常工作，我们无法提供技术支持——我们得到一些反馈称其导出的 2FA 凭据无效——请在向 ASF 导入之前，在 WinAuth 之类的验证器中验证交易确认功能正常工作！*

* * *

### SteamDesktopAuthenticator

如果您已有运行于 SDA 中的验证器，您应该已经注意到 `maFiles` 文件夹下有 `steamID.maFile` 文件。 将该文件复制到 ASF 的 `config` 文件夹。 确保 `.maFile` 是未加密形式，因为 ASF 无法解密 SDA 文件——未加密的文件内容应该以一个 `{` 符号开头。

现在您应该将 `steamID.maFile` 文件重命名为 `BotName.maFile` 并将其放入 ASF 配置文件夹，其中 `BotName` 是您需要导入 ASF 2FA 的机器人名称。 或者，您可以将其保持原样，ASF 将会在登录帐户后自动选择此文件。 如果您在这一步帮助 ASF 重命名，ASF 就可以在登录之前使用 ASF 2FA，否则，ASF 就只能在成功登录之后导入文件（因为 ASF 在登录之前无法获取您帐户的 `steamID`）。

如果一切正确，启动 ASF，您将会看到：

    [*] INFO: ImportAuthenticator() <1> 正在将 .maFile 转换为 ASF 格式……
    [*] INFO: ImportAuthenticator() <1> 成功导入手机验证器！
    

从现在开始，此帐户的 ASF 2FA 功能已经可用。

* * *

### WinAuth

首先在 ASF 的配置文件夹内新建一个空的 `BotName.maFile` 文件，其中 `BotName` 是您需要导入 ASF 2FA 的机器人名称。 记住，文件名应该为 `BotName.maFile` 而不是 `BotName.maFile.txt`，Windows 默认会隐藏文件的扩展名。 如果您提供的文件名错误，ASF 将无法识别它。

现在像平常一样启动 WinAuth。 右键单击 Steam 图标，选择“Show SteamGuard and Recovery Code”。 然后勾选“Allow copy”。 您应该能在窗口底部找到熟悉的以 `{` 开头的 JSON 结构。 将完整文本复制到上一步创建的 `BotName.maFile` 文件中。

如果一切正确，启动 ASF，您将会看到：

    [*] INFO: ImportAuthenticator() <1> 正在将 .maFile 转换为 ASF 格式……
    <1> 请输入您的设备 ID (包括"android:"):
    

现在棘手的部分来了。 WinAuth 缺少 ASF 所需的 deviceID 属性，所以您需要再做一件事。

返回 WinAuth 的“Show SteamGuard and Recovery Code”界面，在您之前复制的 JSON 代码上方有一个“Device ID”属性。 复制整个 Android 设备 ID，包括开头的 `android:`，提交给 ASF。

如果您的一切操作都正确，现在导入过程已完成！

    [*] INFO: ImportAuthenticator() <1> 成功导入手机验证器！
    

请确认接受交易确认功能能够正常工作。 如果您在输入 `DeviceID` 时出错，您的验证器将只有一半功能正常工作——验证令牌正常，但无法接受交易确认。 您可以随时删除 `Bot.db` 文件以重新开始这个过程。

* * *

## 完成导入

从此，所有的 `2fa` 命令将会像原有的 2FA 设备一样正常工作。 您可以用 ASF 2FA 和其他验证器（Android、iOS、SDA 或 WinAuth）生成令牌或者接受确认。

如果您的手机上有验证器，也可以选择移除 SteamDesktopAuthenticator 和/或 WinAuth，因为我们不再需要它了。 不过，我建议保留它们以防万一，而且它们比官方的 Steam 验证器更好用。 需要注意的是，ASF 2FA **不是**通用的验证器，您**不能**将其作为唯一的验证器，因为它甚至不包括验证器所需的所有数据。 无法将 ASF 2FA 转换成原始的验证器，因此请始终确保您在其他地方（如 WinAuth、SDA 或手机）有完整功能的通用验证器。

* * *

## 常见问题

### ASF 的两步验证模块的作用是？

如果 ASF 2FA 可用，ASF 将会使用它来自动确认 ASF 所发送/接受的交易报价。 它还能够在需要时自动生成两步验证令牌，例如在登录时。 除此之外，拥有 ASF 2FA 也会为您启用 `2fa` 命令。 如果我没记错的话，这就是目前的所有功能——基本上 ASF 会按需使用 2FA 模块。

* * *

### 如果我需要一份两步验证令牌该怎么做？

您需要两步验证令牌才能访问受两步验证保护的帐户，这也包括启用了 ASF 2FA 的帐户。 您应该在原有的身份验证器中生成令牌，但您也可以通过聊天向指定机器人发送 `2fa` 命令，生成临时令牌。 您也可以使用 `2fa <BotNames>` 命令为指定的机器人实例生成临时令牌。 在浏览器等环境中访问机器人帐户时，这种方法应该足够用了，但如上文所述——您应该使用更友好的验证器（Android、iOS、SDA 或 WinAuth）来代替。

* * *

### 将验证器导入 ASF 2FA 之后，我原来的验证器还能用吗？

是的，您原有的验证器会保留所有功能，并且可以与 ASF 2FA 一起使用。 整个过程的重点是——我们将您的验证器凭据导入 ASF，使 ASF 可以利用它们代表您接受选定的交易确认。

* * *

### ASF 将验证器数据存放在哪里？

ASF 验证器数据被保存在配置文件文件夹的 `BotName.db` 文件里，这个文件中也包含指定帐户的其他关键数据。 如果您希望移除 ASF 2FA，请阅读下文。

* * *

### 如何移除 ASF 2FA？

只需要关闭 ASF 并移除指定机器人的 `BotName.db` 文件。 此选项将会移除 ASF 与导入的两步验证的关联，但不会解绑您的身份验证器。 如果您打算解绑验证器，除了将其从 ASF 删除外，还需要在您原有的验证器设备上（Android、iOS、SDA 或 WinAuth）解绑，或者，如果您已无法使用验证器，则应该在 Steam 网站上使用绑定验证器时保存的恢复代码。 您无法通过 ASF 解绑您的验证器，这也是您已拥有的一般用途验证器的目标。

* * *

### 我在 SDA/WinAuth 中启用了验证器，然后将它导入了 ASF。 我现在可以解绑原来的验证器，然后重新绑定我的手机吗？

**不**。 ASF 是为了使用目的**导入**您的验证器数据。 如果您解绑了验证器，无论您是否像上文所述移除了它，ASF 2FA 都会失效。 如果您想在手机和 ASF 上（包括 SDA/WinAuth）同时使用身份验证器，您需要从手机中**导入**验证器，而不是在 SDA/WinAuth 中创建新的验证器。 您只能绑定**一个**验证器，这也是 ASF **导入**您的验证器及其数据的原因——它们是**同一个**验证器，只是在两个不同的位置。 如果您决定解绑您的手机验证器凭据——无论以何种方式，ASF 2FA 都会停止工作，因为之前复制的手机验证器凭据将会失效。 若要同时使用 ASF 2FA 和您手机上的验证器，您必须如上所述从 Android/iOS 导入。

* * *

### 使用 ASF 2FA 批量确认交易比 WinAuth/SDA/其他验证器更好吗？

从几个方面来说，**是的**。 第一点也是最重要的一点——使用 ASF 2FA 会**显著**增强安全性，因为 ASF 2FA 模块会确保 ASF 只自动接受它自己的确认，所以即使攻击者向您发送了有害的交易请求，ASF 2FA 也**不会**接受此交易，因为它并非来自 ASF。 除了更安全，使用 ASF 2FA 也会对性能/优化有帮助，因为 ASF 2FA 会且只会在生成交易报价之后立刻获取并确认交易请求，而不是像 SDA 或者 WinAuth 那样低效地每隔 X 分钟拉取一次交易确认。 简而言之，如果您计划自动确认来自 ASF 的报价，就没有理由使用其他第三方验证器代替 ASF 2FA——这正是 ASF 2FA 的作用，并且这与您使用其他验证器确认其他交易并不冲突。 我们强烈建议您为所有 ASF 活动启用 ASF 2FA——这比其他的解决方案更安全。

* * *

## 高级

如果您是高级用户，也可以手动生成 maFile。 它的&#8203;**[有效 JSON 结构](https://jsonlint.com)**&#8203;如下：

```json
{
  "shared_secret": "STRING",
  "identity_secret": "STRING",
  "device_id": "STRING"
}
```

`device_id` 在导入过程中是可选的，但是 ASF 操作强制需要它——如果您在文件中省略了它，ASF 将会在导入过程中要求您提供。 当然，您需要将上述内容中的 `"STRING"` 替换为实际的内容。

标准的身份验证器数据含有更多字段——在导入过程中，ASF 会完全忽略这些字段，因为 ASF 不需要它们。 您也不必删除它们——只要 JSON 中有上述强制要求的 2 个字段就可以，同时您还可选 `device_id` 字段。
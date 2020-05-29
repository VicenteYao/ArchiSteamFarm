# 交易

ASF 支持 Steam 非交互式（离线）的交易。 收取（接受/拒绝）以及发送交易都可以立即使用，不需要特殊配置，但显然需要有不受限制的 Steam 帐户（已在商店中花费至少 5 美元）。 交易模块无法在受限帐户上生效。

* * *

## 逻辑

ASF 将始终接受来自机器人 `Master`（或更高权限）帐户的交易，无论交易物品是什么。 这样可以很方便地拾取由机器人实例挂到的卡牌，也可以轻松管理机器人库存内存储的物品。

ASF 将会驳回来自于交易模块黑名单中的用户的交易报价，无论交易物品是什么。 黑名单被存放在标准的 `BotName.db` 数据库，可以通过 `bl`、`bladd` 和 `blrm` **[命令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-zh-CN)**&#8203;管理。 这应该可以代替 Steam 提供的标准用户屏蔽——谨慎使用。

ASF 将会接受机器人之间发送的 `loot`（拾取）交易，除非 `TradingPreferences` 中设置了 `DontAcceptBotTrades`。 简单来说，将 `TradingPreferences` 设置为 `None` 会使 ASF 自动接受来自机器人 `Master` 用户（上文所述）的交易，和来自同一 ASF 进程的其他机器人的赠送交易。 如果您想禁用来自其他机器人的赠送交易，就应该在 `TradingPreferences` 中设置 `DontAcceptBotTrades`。

当您在 `TradingPreferences` 中设置 `AcceptDonations` 时，ASF 也会接受一切赠送交易（机器人帐户不损失任何物品的交易）。 这一属性仅仅影响非机器人帐户，因为机器人帐户受 `DontAcceptBotTrades` 属性影响。 `AcceptDonations` 属性允许您轻松接受来自于其他人（以及其他 ASF 进程中的机器人）的赠送。

值得注意的是，`AcceptDonations` 不需要 **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-zh-CN)**，因为在不损失物品的情况下不需要进行交易确认。

您还可以通过修改 `TradingPreferences` 属性进一步自定义 ASF 交易行为。 `TradingPreferences` 的一个主要功能是 `SteamTradeMatcher` 选项，使 ASF 使用内置逻辑接受能够帮助您收集合成徽章所需卡牌的交易，这一功能在结合 **[SteamTradeMatcher](https://www.steamtradematcher.com)** 公开列表时特别有用，但是也可以单独使用。 下面将进一步介绍此功能。

* * *

## `SteamTradeMatcher`

当 `SteamTradeMatcher` 启用时，ASF 将会使用一套复杂的算法检查交易是否符合 STM 规则，以及交易对我们来说是否至少公平。 具体的逻辑是：

- 如果我们损失 `MatchableTypes` 以外的物品，则驳回交易。
- 对于每款游戏、每种物品类型，如果我们收到的物品少于发出的物品，则驳回交易。
- 如果交易者想要特殊 Steam 夏季/冬季特卖卡牌，但是有交易暂挂，则驳回交易。
- 如果交易暂挂时间达到 `MaxTradeHoldDuration` 全局配置属性的值，则驳回交易。
- 如果我们没有设置 `MatchEverything`（接受所有匹配交易），而交易内容对我们不利，则驳回交易。
- 如果交易不符合上述任何情况，则接受交易。

值得注意的是，ASF 还支持 Overpay（超额付款，或溢价）——只要满足上述条件，交易者在提供更多物品时，此逻辑也会正常工作。

前 4 种驳回条件应该是显而易见的。 最后一种条件则包含重复检查逻辑：检查我们当前的库存状态来决定交易的状态。

- 如果交易倾向于增加您的卡牌套组收集进度，则视为**有利**。 A A（交易前） <-> A B（交易后）
- 如果交易不会影响您的卡牌套组收集进度，则视为**平衡**。 A B（交易前） <-> A C（交易后）
- 如果交易倾向于减少您的卡牌套组收集进度，则视为**不利**。 A C（交易前） <-> A A（交易后）

STM 仅会处理有利的交易，这意味着使用 STM 进行重复卡牌匹配的用户只能向我们推荐对我们有利的交易。 然而，ASF 更加宽松，它也接受平衡交易，因为在这种交易中我们实际上并没有任何损失，所以没有理由拒绝它们。 这对朋友之间的交易特别有用，因为他们可以在不使用 STM 的情况下与您交换多余的卡牌，只要这不会导致您损失卡牌收集进度。

默认情况下，ASF 会驳回不利的交易——对于普通用户来说，这正是我们想要的。 然而，您可以在 `TradingPreferences` 中启用 `MatchEverything`，使 ASF 接受所有重复物品交易，即使是**不利的**。 只有您想要在您的帐户下运行 1:1 交易机器人时，此功能才有用，因为您了解 **ASF 将不再帮助您完成徽章进度，并且您可能会因为 N 张相同的卡牌失去进度**。 除非您有意运行一个**永远**不收集卡牌套组的交易机器人，否则您应该禁用此功能。

无论您的 `TradingPreferences` 如何设置，ASF 驳回交易不意味着您不能自己接受它。 如果您保留了 `BotBehaviour` 的默认值，不含 `RejectInvalidTrades`，ASF 仅会忽略这些交易——使您可以自行决定是否接受交易。 同样的情况适用于 `MatchableTypes` 以外的物品——这个模块仅仅用于自动化 STM 交易，而不是代替您判断交易的优劣。 此规则的唯一例外是通过 `bladd` 命令添加进交易黑名单的用户——无论您的 `BotBehaviour` 如何设置，来自这些用户的交易都会被立即驳回。

启用此选项时，强烈建议您使用 **[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-zh-CN)**，因为如果您还需要手动确认每次交易，这一功能也就失去了它的潜力。 即使没有确认交易的能力，`SteamTradeMatcher` 也可以正常工作，但是如果您没有及时手动确认，就会留下积压的确认请求。

* * *

### `MatchActively`

`MatchActively`（主动匹配）是 `SteamTradeMatcher` 的扩展版本，除了被动匹配以外，机器人还可以主动向其他人发送交易报价。

为了使用该选项，您需要满足一系列需求。 首先，您需要启用 `SteamTradeMatcher`（因为此功能是它的扩展版），并且**禁用** `MatchEverything` 属性（因为交易机器人不会主动发起交易）。 之后，您需要有加入我们的 **[ASF STM 列表](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-zh-CN#当前隐私政策)**&#8203;的资格，但条件略微宽松。 您至少应该保证 `Statistics` 启用、帐户&#8203;**[不受限](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663)**、**[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-zh-CN#asf-两步验证)** 启用，并且在 `MatchableTypes` 中设置了至少一种有效的类型，例如集换式卡牌。

如果您满足上述所有要求，ASF 将会定期与我们的&#8203;**[公共 ASF STM 列表](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-zh-CN#公共-asf-stm-列表)**&#8203;通信，以主动匹配当前在线的机器人。

- 每次匹配都由数个“轮次”组成，单次匹配流程最多可含 `10` 轮。
- 每一轮，ASF 将会获取我们的库存与列表中选定机器人的库存，以寻找可匹配的 `MatchableTypes` 物品。 如果找到合适的匹配，ASF 将会自动发送并确认交易报价。
- 每套物品（相同 appID、物品类型和稀有程度的组合为一套）只能在一轮中匹配一次。 这是为了尽可能减少“物品当前不能用于交易”的情况，并且无需在发出所有交易报价之前等待每个机器人作出回应。 这也是匹配由多个轮次组成而不是持续进行的主要原因。
- ASF 不会在单个交易报价中发送超过 `255` 个物品，每轮中不会向同一个用户发送超过 `5` 个交易报价。 这个强制限制来自于 Steam 和我们的负载平衡机制。
- 当我们尝试匹配共计 `40` 个机器人时，本轮匹配结束。但如果匹配物品套组用尽，或空白匹配次数过多，匹配也会提前结束。
- 如果上一轮匹配中至少发出了一个交易报价，下一轮将会在 `5` 分钟内开始（冷却一段时间使所有机器人作出回应），否则匹配流程将会结束，并且在 `8` 小时内重新开始这一过程。

这一模块应该是透明的。 匹配过程会在 ASF 启动后大约 `1` 小时后开始，并且每 `8` 小时重复一次（如果有需要）。 `MatchActively` 功能旨在作为一种长期的周期性措施，确保我们向集齐卡牌套组的方向前进，但如果我们将其作为命令提供，就会造成突发的时间与资源压力。 此模块的目标用户是主帐户和用于存储的子帐户，但也可以用于任何没有设置 `MatchEverything` 的机器人。

ASF 将会尽力减少由此选项带来的请求和压力，同时将匹配的效率提升至极限。 选择匹配机器人的具体算法是 ASF 的实现细节，但目前 ASF 更倾向于物品所属游戏数更多的机器人，并且更有可能选择 `Any` 标记的机器人。

`MatchActively` 支持交易黑名单，您可以通过 `bladd` **[命令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-zh-CN)**&#8203;向其中添加机器人的帐户，您的机器人将不会尝试与黑名单中的机器人主动匹配。 这告诉 ASF 永远不匹配这些机器人，即使这些机器人有我们可能需要的卡牌。
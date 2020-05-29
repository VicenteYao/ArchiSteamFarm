# 指令

ASF 支援各種指令，以此控制程式和 BOT 執行個體的行為。

您可以透過這些不同的方式發送指令：

- 透過互動式 ASF 主控台
- 透過 Steam 私人/群組聊天
- 透過 **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-zh-TW)** 介面

請注意，與 ASF 交互需要您擁有執行相關指令的許可權。 查看 `SteamUserPermissions` 和 `SteamOwnerID` 設定檔屬性瞭解更多。

透過 Steam 聊天執行的指令都受 `CommandPrefix` **[全域設定檔屬性影響](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-zh-TW#commandprefix)**，該屬性的預設值為 `!`。 這意味著，當您要執行 `status` 指令時，實際應該發送 `!status`（或者使用您自訂的 `CommandPrefix`）。 當您使用主控台或 IPC 時可以省略 `CommandPrefix`，這項屬性不是強制性的。

* * *

### 互動式主控台

從 V4.0.0.9 版本開始，ASF 已經支援了互動式主控台，可以透過設定 [**`SteamOwnerID`**](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-zh-TW#steamownerid) 屬性來啟用。 之後，只需按一下 `c` 鍵即可啟用指令模式，輸入指令然後按一下 Enter 鍵確認。

![截圖](https://i.imgur.com/bH5Gtjq.png)

互動式主控台在 [**`Headless`**](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-zh-TW#headless) 模式下不可用。

* * *

### Steam 聊天

您也可以透過 Steam 聊天對指定的 ASF BOT 執行指令。 顯然，您不能直接跟自己聊天，因此，如果您想在自己的主帳戶執行指令，您需要至少另一個 BOT 帳戶。

![截圖](https://i.imgur.com/IvFRJ5S.png)

類似的，您也可以使用指定的 Steam 群組聊天。 請注意，此選項需要您正確設定 `SteamMasterClanID` 屬性，使 BOT 同樣監聽（並加入）指定的群組交談。 不同於私人聊天，因為這種方法不需要專用的 BOT 帳戶，所以可以用於「和自己交談」。 您可以簡單的將 `SteamMasterClanID` 屬性設定為您新建立的群組，然後透過設定 BOT 的 `SteamOwnerID` 或 `SteamUserPermissions` 給您自己存取權限。 這樣，ASF BOT（即您自己的帳戶）將會加入這個群組和群組聊天室，並且開始監聽您發送的指令。 您可以加入同一個群組聊天室，以便向自己發送指令（因為在您向聊天室發送指令時，同樣在聊天室內的 ASF 執行個體將會收到指令，即使界面上顯示只有您自己在聊天室內）。

請注意，傳送指令至群組聊天就像是一個中繼。 如果您向一個含有 3 個 BOT 的群組聊天發送 `redeem X` 指令，其效果跟分別向每個 BOT 私人聊天發送 `redeem X` 指令一樣。 在大多數情況下，**這不是您想要的效果**，您應該像之前與**單個 BOT 交談**時一樣，使用`特定 BOT` 名稱的指令形式。 ASF 支持群組聊天，是因為在多數情況下它是一種與您唯一的 BOT 通訊的有效方式，但如果您的群組中有多個 ASF BOT，就最好不要在這裡執行指令，除非您完全理解 ASF 的相關行為，並且您確實想要讓所有 BOT執行相同的指令。

*即使在這種情況下，您也應該使用 `[Bots]` 私人交談向 BOT 發送指令。*

* * *

### IPC

這是最先進、靈活的執行指令方式，非常適合使用者互動（ASF-ui）或者第三方工具腳本（ASF API）。這種方式需要 ASF 執行在 `IPC` 模式下，並且用戶端需要透過 **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-zh-TW)** 介面來執行指令。

![截圖](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/commands.png)

* * *

## 指令

| 指令                                                                   | 存取              | 描述                                                                                                                                                     |
| -------------------------------------------------------------------- | --------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `2fa [Bots]`                                                         | `Master`        | 為指定 BOT 產生臨時的**[兩步驟驗證](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-zh-TW)**​權杖。                                      |
| `2fano [Bots]`                                                       | `Master`        | 為指定 BOT 拒絕所有待處理的**[兩步驟驗證](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-zh-TW)**​交易確認。                                 |
| `2faok [Bots]`                                                       | `Master`        | 為指定 BOT 接受所有待處理的**[兩步驟驗證](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication-zh-TW)**​交易確認。                                 |
| `addlicense [Bots] <Licenses>`                                 | `Operator`      | 在指定 BOT 上啟用給定的 `licenses `，請參閱**[下文](#addlicense-licenses)**解釋。                                                                                        |
| `balance [Bots]`                                                     | `Master`        | 顯示指定 BOT 的 Steam 錢包餘額。                                                                                                                                 |
| `bgr [Bots]`                                                         | `Master`        | 印出關於指定 BOT 的**[背景序號啟動器](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Background-games-redeemer-zh-TW)**佇列的資訊。                                   |
| `bl [Bots]`                                                          | `Master`        | 列出指定 BOT 的使用者交易黑名單。                                                                                                                                    |
| `bladd [Bots] <SteamIDs64>`                                    | `Master`        | 將指定 `steamIDs` 新增至指定 BOT 的使用者交易黑名單。                                                                                                                    |
| `blrm [Bots] <SteamIDs64>`                                     | `Master`        | 將指定 `steamIDs` 移除自指定 BOT 的使用者交易黑名單。                                                                                                                    |
| `exit`                                                               | `Owner`         | 完全終止 ASF 行程。                                                                                                                                           |
| `farm [Bots]`                                                        | `Master`        | 重啟指定 BOT 的掛卡模塊。                                                                                                                                        |
| `help`                                                               | `FamilySharing` | 顯示幫助（指向此頁面的連結）。                                                                                                                                        |
| `input [Bots] <Type> <Value>`                            | `Master`        | 設定指定 BOT 為指定的輸入類型，僅在 `Headless` 模式工作──請參閱**[下文](#input-指令)**解釋。                                                                                        |
| `ib [Bots]`                                                          | `Master`        | 列出指定 BOT 的自動掛卡黑名單。                                                                                                                                     |
| `ibadd [Bots] <AppIDs>`                                        | `Master`        | 將指定 `appIDs` 新增至指定 BOT 的自動掛卡黑名單。                                                                                                                       |
| `ibrm [Bots] <AppIDs>`                                         | `Master`        | 將指定 `appIDs` 移除自指定 BOT 的自動掛卡黑名單。                                                                                                                       |
| `iq [Bots]`                                                          | `Master`        | 列出指定 BOT 的優先掛卡佇列。                                                                                                                                      |
| `iqadd [Bots] <AppIDs>`                                        | `Master`        | 將指定 `appIDs` 新增至指定 BOT 的優先掛卡佇列。                                                                                                                        |
| `iqrm [Bots] <AppIDs>`                                         | `Master`        | 將指定 `appIDs` 移除自指定 BOT 的優先掛卡佇列。                                                                                                                        |
| `level [Bots]`                                                       | `Master`        | 顯示指定 BOT 的 Steam 等級。                                                                                                                                   |
| `loot [Bots]`                                                        | `Master`        | 將指定 BOT 的所有 `LootableTypes` 社區物品交易給其 `SteamUserPermissions` 屬性中設定的 `Master` 使用者（如有多個則取 steamID 最小的）。                                                   |
| `loot@ [Bots] <RealAppIDs>`                                    | `Master`        | 將指定 BOT 所有符合指定 `RealAppIDs` 的 `LootableTypes` 社區物品交易給其 `SteamUserPermissions` 屬性中設定的 `Master` 使用者（若有多個則取 steamID 最小的）。 這是跟 `loot%` 相反的指令。              |
| `loot% [Bots] <RealAppIDs>`                                    | `Master`        | 將指定 BOT 所有不符合指定 `RealAppIDs` 的 `LootableTypes` 社區物品交易給其 `SteamUserPermissions` 屬性中設定的 `Master` 使用者（若有多個則取 steamID 最小的）。 這是跟 `loot@` 相反的指令。             |
| `loot^ [Bots] <AppID> <ContextID>`                       | `Master`        | 將指定機器人的` ContextID` 庫存分類中符合特定 `AppID` 的物品交易給其 `SteamUserPermissions` 屬性中設置的 `Master` 用戶（如果有多個則取 steamID 最小的）。                                          |
| `nickname [Bots] <Nickname>`                                   | `Master`        | 將指定 BOT 的 Steam 暱稱變更為`nickname`。                                                                                                                       |
| `owns [Bots] <Games>`                                          | `Operator`      | 檢查指定 BOT 是否已擁有指定 `games`，請參閱**[下文](#owns-games)**解釋。                                                                                                   |
| `password [Bots]`                                                    | `Master`        | 顯示指定 BOT 加密後的密碼（配合 `PasswordFormat` 使用）。                                                                                                               |
| `pause [Bots]`                                                       | `Operator`      | 停止指定 BOT 的自動掛卡模塊。 ASF 在本次會話中將不會再嘗試對此帳戶進行掛卡，除非您手動 `resume` 或者重啟 ASF。                                                                                    |
| `pause~ [Bots]`                                                      | `FamilySharing` | 暫停指定 BOT 的自動掛卡模塊。 掛卡行程將會在下次遊戲事件被觸發時或 BOT 斷開連接時自動恢復。 您可以` resume` 以恢復掛卡。                                                                                |
| `pause& [Bots] <Seconds>`                                  | `Operator`      | 暫停指定 BOT 的自動掛卡模塊 `seconds` 秒。 之後，掛卡將自動恢復。                                                                                                              |
| `play [Bots] <AppIDs,GameName>`                                | `Master`        | 切換至手動掛卡模式——使指定 BOT 執行特定的 `AppIDs`，並且可選自訂 `GameName` 為當前遊戲名稱。 若要使此功能正常工作，您的 Steam 帳戶**必須**擁有您指定的所有 `AppIDs` 的有效許可，包括免費遊玩遊戲。 使用 `reset` 或 `resume` 指令恢復。 |
| `privacy [Bots] <Settings>`                                    | `Master`        | 變更指定 BOT 的 **[Steam 隱私設定](https://steamcommunity.com/my/edit/settings)**，可用選項將於**[​下文](#privacy-settings)**詳述。                                         |
| `redeem [Bots] <Keys>`                                         | `Operator`      | 為指定 BOT 啟用給出的遊戲序列號或兌換給出的錢包儲值碼。                                                                                                                         |
| `redeem^ [Bots] <Modes> <Keys>`                          | `Operator`      | 以將於**[​下文](#redeem-modes)**解釋的 `Modes` 模式為指定 BOT 啟用給出的遊戲序列號或兌換給出的錢包儲值碼。                                                                                |
| `reset [Bots]`                                                       | `Master`        | 重設遊玩狀態為正常，使用 `play` 指令手動掛卡時使用此指令。                                                                                                                      |
| `restart`                                                            | `Owner`         | 重新啟動 ASF 行程。                                                                                                                                           |
| `resume [Bots]`                                                      | `FamilySharing` | 恢復指定 BOT 的自動掛卡行程。 參見 `pause` 和 `play`。                                                                                                                 |
| `start [Bots]`                                                       | `Master`        | 啟動指定 BOT。                                                                                                                                              |
| `stats`                                                              | `Owner`         | 顯示行程統計資訊，例如託管記憶體用量。                                                                                                                                    |
| `status [Bots]`                                                      | `FamilySharing` | 顯示指定 BOT 的狀態。                                                                                                                                          |
| `stop [Bots]`                                                        | `Master`        | 停止指定 BOT。                                                                                                                                              |
| `transfer [Bots] <TargetBot>`                                  | `Master`        | 將指定 BOT 所有 `TransferableTypes` 社群物品交易至目標 BOT。                                                                                                          |
| `transfer@ [Bots] <RealAppIDs> <TargetBot>`              | `Master`        | 將指定 BOT 所有符合指定 `RealAppIDs` 的 `TransferableTypes` 社群物品交易至目標 BOT。 這是跟 `transfer%` 相反的指令。                                                                |
| `transfer% [Bots] <RealAppIDs> <TargetBot>`              | `Master`        | 將指定 BOT 所有不符合指定 `RealAppIDs` 的 `TransferableTypes` 社群物品交易至目標 BOT。 這是跟 `transfer@` 相反的指令。                                                               |
| `transfer^ [Bots] <AppID> <ContextID> <TargetBot>` | `Master`        | 將指定BOT的 `ContextID` 庫存分類中符合特定 `AppID` 的物品交易至目標 BOT。                                                                                                    |
| `unpack [Bots]`                                                      | `Master`        | 拆開指定 BOT 物品庫中的所有補充包。                                                                                                                                   |
| `update`                                                             | `Owner`         | 檢查 GitHub 上的 ASF 更新（每 `UpdatePeriod` 自動執行一次）。                                                                                                          |
| `version`                                                            | `FamilySharing` | 顯示 ASF 的版本號。                                                                                                                                           |

* * *

### 註解

所有的指令都不區分大小寫，但它們的參數（例如 BOT 名稱）通常是區分大小寫的。

`[Bots]` 參數在所有指令內都是可省略的。 當指定該參數時，指令會在指定的 BOT 上執行。 但省略時，指令會在當前接收指令的 BOT 上執行。 換句話說，向 BOT `B` 發送 `status A` 指令等於向 BOT `A` 發送 `status` 指令，在這種情況下 BOT `B` 只是作為一個代理 BOT。 這也可用於向不可用的 BOT 傳送指令，例如啟動已停止的 BOT，或者在（您用於執行指令的）主要帳戶上執行動作。

指令的**許可權**定義了需要執行此指令所需的**最低**許可權，即 `SteamUserPermissions `中定義的 `EPermission`，例外情況是 `Owner` 指全域設定檔中定義的 `SteamOwnerID` 使用者（擁有最高許可權）。

複數參數，例如 `[Bots]`、`<Keys>`或`<AppIDs>`意味著該參數支援多個由逗號分隔的同類型值。 例如，`status [Bots]` 可以用為 `status MyBot,MyOtherBot,Primary`。 這樣，該指令會在**所有目標 BOT** 上執行，效果等同分別向所有 BOT 單獨發送 `status`指令。 需要注意的是「`,`」後面不能有空格。

ASF 使用所有空白字元作為指令的分隔符，例如空格和換行符。 這意味著您不僅可以使用空格來分隔參數，還可以使用任何其他空白字元（如製表符和換行符）。

ASF 會將指令末尾超出規定範圍的多餘參數「連接」到符合語法規定的最後一個複數參數上。 這表示對於 `redeem [Bots] <Keys>`指令，`redeem bot key1 key2 key3` 跟 `redeem bot key1,key2,key3` 的作用完全一樣。 再加上您可以使用換行符作為指令分隔符，這樣您就可以先寫 `redeem bot`，然後在其後貼上一個產品序號列表，其中每一項可以由任意空白字元（例如換行符）或者 ASF 標準的`,`分隔。 請注意，要使用這一技巧，您必須採用該指令參數數量最多的形式（所以在這種情況下，您必須指定 `[Bots]` 參數）。

如上所述，空白字元被用於分隔指令參數，所以參數內部無法再使用空白字元。 但同樣如上所述，ASF 可以連接超出範圍的參數，這意味著您可以在指令的最後一個參數中使用空白字元。 例如，`nickname bob Great Bob` 指令能夠正確地將 BOT `bob` 的暱稱變更為「Great Bob」。 與此類似，您也可以使用 `owns` 指令檢查含有空格的名稱。

* * *

一些指令有較短的別名可用，便於輸入。

| 指令           | 別名   |
| ------------ | ---- |
| `owns ASF`   | `oa` |
| `status ASF` | `sa` |
| `redeem`     | `r`  |
| `redeem^`    | `r^` |

* * *

### `[Bots]` 參數

`[Bots]` 是複數參數的一個特殊變體，除了接受多個值以外，它還有額外的功能。

首要的是，您可以使用特殊的關鍵字 `ASF` 來表示「所有 BOT」，因此 `status ASF` 指令等同於 `status all,your,bots,listed,here`。 這也可以方便地辨識您有權操作哪些 BOT，因為儘管 `ASF` 關鍵字的目標是所有 BOT，但只有您能夠實際發送指令的 BOT 才會作出回應。

`[Bots]` 參數支援特殊的「範圍」語法，這可以讓您更容易地選擇特定範圍的 BOT。 `[Bots]` 參數支援特殊的「範圍」語法，這可以讓您更容易地選擇特定範圍的 BOT。 例如，假設您有 BOT `A, B, C, D, E, F`，如果您執行 `status B..E`，效果等同于執行 `status B,C,D,E`。 在使用此語法時，ASF 將會以字母排序以決定哪些 BOT 在指定範圍內。 `firstBot` 和`lastBot`必須是 ASF 能夠辨識的有效 BOT 名稱，否則範圍語法將不會生效。

除了上述的範圍語法，`[Bots]` 參數還支援**[規則運算式](https://en.wikipedia.org/wiki/Regular_expression)**符合。 您可以使用 `r!<pattern>` 作為 BOT 名稱來激活規則運算式模式，其中 `r!` 是用於規則運算式符合的 ASF 啟動指令，而 `<pattern>` 是您的規則運算式。 一個使用規則運算式的例子為 `status r!\d{3}` 指令，它會向所有名稱為 3 個數字的 BOT（例如 `123` 和 `981`）發送` status` 指令。 您可以閱讀這份**[文件](https://docs.microsoft.com/dotnet/standard/base-types/regular-expression-language-quick-reference)**，進一步瞭解規則運算式的解釋和示例。

* * *

## `privacy` 設定

`<Settings>` 參數接受**最多 7 個**不同的選項，使用 ASF 標準的逗號分隔格式。 這些選項分別是：

| 參數 | 名稱             | 從屬於        |
| -- | -------------- | ---------- |
| 1  | Profile        |            |
| 2  | OwnedGames     | Profile    |
| 3  | Playtime       | OwnedGames |
| 4  | FriendsList    | Profile    |
| 5  | Inventory      | Profile    |
| 6  | InventoryGifts | Inventory  |
| 7  | Comments       | Profile    |

關於上述選項的說明，請訪問 **[Steam 隱私設定](https://steamcommunity.com/my/edit/settings)**。

每個選項的有效值可以是：

| 值 | 名稱            |
| - | ------------- |
| 1 | `Private`     |
| 2 | `FriendsOnly` |
| 3 | `Public`      |

您可以使用它們的名稱（不區分大小寫）或者數值。 未賦值的參數將會被設定為預設值 `Private`。 請謹記上述參數的從屬關係非常重要，因為子選項無法擁有比父選項更高的許可權。 例如，如果您將個人檔案設定為 `Private`，就**無法**再將遊戲資料設定為 `Public`。

### 範例

如果您希望將 BOT `Main` 的**所有**隱私設定都設為 `Private`，可以使用以下任一指令：

    privacy Main 1
    privacy Main Private
    

這是因為 ASF 會預設所有未賦值選項為 `Private`，所以您不需要全部寫出它們。 另一方面，如果您希望設定所有選項為 `Public`，可以使用以下任一指令：

    privacy Main 3,3,3,3,3,3,3
    privacy Main Public,Public,Public,Public,Public,Public,Public
    

也可以為每個選項設定不同的值：

    privacy Main Public,FriendsOnly,Private,Public,Public,Private,Public
    

上述指令將會設定個人檔案為公開、遊戲資料為僅限好友、總遊玩時數為私人、好友名單為公開、物品庫為公開、物品庫禮物為私密、留言為私人。 若有需要，您也可以使用數字值來實現相同效果。

請記住子選項的許可權無法比父選項更高。 有關可用選項，請參閱參數關係。

* * *

## `addlicense` licenses

`addlicense` 指令支援兩種不同的授權類型：

| 類型    | 別名  | 範例           | 描述                              |
| ----- | --- | ------------ | ------------------------------- |
| `app` | `a` | `app/292030` | 透過遊戲唯一的 `appID` 授權。             |
| `sub` | `s` | `sub/47807`  | 透過遊戲套裝唯一的 `subID` 授權，包括一款以上的遊戲。 |

其中的區別很重要，因為 ASF 將對 app 類型使用 Steam 網路激活，對sub 類型使用 Steam 商店激活。 兩者互不相容，通常您將會對免費週末和永久免費遊玩遊戲使用 app 類型，而對其他遊戲使用 sub。

我們建議您明確定義每一個項目的類型，以避免引起歧義的的結果，但為了向後相容性，在您提供了無效的類型或省略了類型的情況下，ASF 將會假設您想要使用 `sub` 類型。 您也可以使用 ASF 標準的分隔符「`,`」來同時啟用多個許可。

完整的指令範例：

    addlicense ASF app/292030,sub/47807
    

* * *

## `owns` games

`owns` 指令支援幾種不同的 `<games>` 遊戲參數類型，分別是：

| 類型      | 別名  | 範例               | 描述                                                                                                                                                                                                                                       |
| ------- | --- | ---------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `app`   | `a` | `app/292030`     | 透過遊戲唯一的 `appID` 授權。                                                                                                                                                                                                                      |
| `sub`   | `s` | `sub/47807`      | 透過遊戲套裝唯一的 `subID` 授權，包括一款以上的遊戲。                                                                                                                                                                                                          |
| `regex` | `r` | `regex/^\d{4}:` | 用於遊戲名稱的**[規則運算式](https://en.wikipedia.org/wiki/Regular_expression)**，區分大小寫。 See the **[docs](https://docs.microsoft.com/dotnet/standard/base-types/regular-expression-language-quick-reference)** for complete syntax and more examples. |
| `name`  | `n` | `name/Witcher`   | 部分遊戲名稱，不區分大小寫。                                                                                                                                                                                                                           |

我們建議您明確定義每一個項目的類型，以避免引起歧義的的結果，但為了向後相容性，在您提供了無效的類型或省略了類型的情況下，如果您填入了數字，ASF 將會假設您想要使用 `app` 類型，如果不是數字則視為 `name` 類型。 您也可以使用 ASF 標準的分隔符「`,`」來同時查詢多個遊戲。

完整的指令範例：

    owns ASF app/292030,name/Witcher
    

* * *

## `redeem^` 模式

`redeem^` 指令允許您微調用於單個兌換場景的模式。 此指令會臨時覆蓋 `RedeemingPreferences` **[BOT 設定屬性](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-zh-TW#BOT-設定檔)**。

`<Modes>` 參數接受多個模式值，通常用逗號分隔。 可用的模式值如下所示：

| 值    | 名稱                    | 描述                                               |
| ---- | --------------------- | ------------------------------------------------ |
| FAWK | ForceAssumeWalletKey  | 強制啟用 `AssumeWalletKeyOnBadActivationCode` 兌換偏好設定 |
| FD   | ForceDistributing     | 強制啟用 `Distributing` 兌換偏好設定                       |
| FF   | ForceForwarding       | 強制啟用 `Forwarding` 兌換偏好設定                         |
| FKMG | ForceKeepMissingGames | 強制啟用 `KeepMissingGames` 兌換偏好設定                   |
| SAWK | SkipAssumeWalletKey   | 強制停用 `AssumeWalletKeyOnBadActivationCode` 兌換偏好設定 |
| SD   | SkipDistributing      | 強制停用 `Distributing` 兌換偏好設定                       |
| SF   | SkipForwarding        | 強制停用 `Forwarding` 兌換偏好設定                         |
| SI   | SkipInitial           | 跳過初始 BOT 的產品序號兌換過程                               |
| SKMG | SkipKeepMissingGames  | 強制停用 `KeepMissingGames` 兌換偏好設定                   |
| V    | Validate              | 檢查產品序號格式，自動跳過無效產品序號                              |

例如，我們打算為尚未擁有遊戲的 BOT 兌換 3 個產品序號，但不包括 `primary` BOT。 為此我們需要執行指令：

`redeem^ primary FF,SI key1,key2,key3`

需要注意的是，進階兌換模式只會替換你**在指令中使用**的`RedeemingPreferences`選項。 例如，如果您在`RedeemingPreferences` 中啟用了 `Distributing`，那麼無論是否使用 `FD` 模式，都不會有任何區別，因為您已經在 `RedeemingPreferences` 中兌換了 Distributing。 這就是為什麼每個可強制啟用的模式也有一個可強制停用的選項，您可以決定自己是希望在啟用的情況下強制覆蓋，反之亦然。

* * *

## `input` 指令

輸入指令只能在 `Headless` 模式下使用，用於在 ASF 執行而不与支援使用者交互的情況下，透過 **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-zh-TW)** 或 Steam 交談輸入給定的資料。

通用語法為 `input [Bots] <Type> <Value>`。

`<Type>` 不區分大小寫，並定義由 ASF 辨識的輸入類型。 目前，ASF 可辨識以下類型：

| 類型                      | 描述                                               |
| ----------------------- | ------------------------------------------------ |
| DeviceID                | 兩步驟裝置驗證器，在 `.maFile` 中缺失這個值時使用。                  |
| Login                   | `SteamLogin` BOT 設定檔屬性，在設定檔缺失這個值時使用。             |
| Password                | `SteamPassword` BOT 設定檔屬性，在設定檔缺失這個值時使用。          |
| SteamGuard              | 如果您未啟用兩步驟驗證，驗證碼將以電子郵件的方式發送。                      |
| SteamParentalCode       | `SteamParentalCode` BOT 設定檔屬性，在設定檔缺失這個值時使用。      |
| TwoFactorAuthentication | 如果您正在使用兩步驟驗證，但未使用 ASF 的兩步驟驗證，則兩步驟驗證權杖會從您的行動裝置產生。 |

`<Value>` 是要為指定類型設定的值。 目前所有的值都是字串。

### 範例

假設我們有一個未啟用兩步驟驗證，僅由 SteamGuard 保護的 BOT。 我們希望在 `Headless` 模式下啟動這個 BOT 。

為此，我們需要執行以下指令：

`start MySteamGuardBot` -> BOT 會嘗試登入，但缺少驗證碼會導致登入失敗，在 `Headless` 模式下，BOT 會停止執行。 我們做這一步的目的是讓 Steam 網路向我們發送驗證碼電子郵件——否則我們就可以跳過這一步了。

`input MySteamGuardBot SteamGuard ABCDE` -> 我們將 `MySteamGuardBot` BOT 的 `SteamGuard` 輸入設定為 `ABCDE`。 當然，在這種情況下，`ABCDE` 是我們從電子郵件中獲得的驗證代碼。

`start MySteamGuardBot` ->我們重新啟動（已停止的）BOT，這一次會自動使用我們在上一步中設定的驗證碼，登入將會成功，並且之前的驗證碼輸入會被清除。

同樣，我們可以控制受兩步驟驗證保護的 BOT（如果它們不使用 ASF 兩步驟驗證），只需在執行時設定其他必需的屬性。
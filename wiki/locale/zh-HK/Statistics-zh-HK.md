# 統計資訊

ASF 開發的主要支援來自三個渠道：捐贈、用戶回饋和統計資訊。 捐贈直接影響我們在項目上工作的意願，閱讀用戶的回饋總是令我開心（特別是積極的），而統計資訊為我們提供了用戶如何使用我們的軟件的資訊，以及用戶的數量——這樣我們就可以知道該如何改進，該修復什麼，以及工作的重點應該是什麼。

ASF在全域配置中預設啟用`統計資訊`。 如果您想見證新版本發佈，錯誤修復，新功能實現，您應該考慮保持統計功能啟用，所以我們可以利用這些數據，以便為您提供更好的軟件（但不僅如此）。 這一點尤為重要，因為 ASF 是**免費**的，但您總是可以表達一些小小謝意—— **告訴我們您在使用ASF **，因為這是我們目前主要統計的數據。 ASF 並不會收集或利用任何可能被視為個人隱私和/或針對您的數據。 我們將統計資料的使用保持在最低限度，我們所收集的每一個資訊會在這裡準確地陳述，並作出實際解釋。 這讓您充分瞭解我們收集的東西、目的是什麼，以及應該如何協助。 所有這些資訊都可以在下面找到。

* * *

## 當前隱私政策

當`統計資訊`被啟用時，將發生以下情況：

1. 使用 ASF 的帳戶將在登錄後加入我們的 **[Steam群組](https://steamcommunity.com/gid/103582791440160998)**。 這樣做有三個原因：

* 它為**您**提供群組公告，特別是新版本，關鍵問題，Steam故障和其他重要的東西，以保持社區更新（保證不會有與ASF無關的垃圾消息）。
* 它允許**您**使用我們的技術支援，您可以提問、回答、報告漏洞或提交建議。
* 它可以讓我們看到 ASF 實際上有多少 Steam 用戶。

我們認為 Steam 群組是 ASF 社區的關鍵部分。 This is our main communication channel which we use for all important matters in regards to ASF, especially keeping you up-to-date with the development, potential issues, eventual warnings and all other matters that you should have access to as an user. 我們不會從該群組的維護中受益，它是專門支援 ASF 用戶的地方，我們認為您是我們社區的一部分。 由於該組的成員資格絕不表明您是 ASF 使用者，因此我們不認為這是隱私方面的問題。

2. 如果您的帳戶**[未受限](https://support.steampowered.com/kb_article.php?ref=3330-IAGK-7663)**，啓用**[ASF 2FA](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Two-factor-authentication#asf-2fa)**，**[物品庫隱私為公開](https://steamcommunity.com/my/edit/settings)**且擁有大於100個符合`MatchableTypes`類型的物品，並在`TradingPreferences`中啓用了`SteamTradeMatcher`，那麽ASF將定期與我們的**[伺服器](https://asf.justarchi.net)**通信以實現此功能。 實際資料由唯一的 ASF ID（由ASF生成）和以下與帳戶相關的資訊組成：

* 您的Steam身份驗證器（64位形式，用於生成連結）
* 您的昵稱（用於顯示）
* 您的頭像（哈希，用於顯示）
* 您的**[交易代碼](https://steamcommunity.com/my/tradeoffers/privacy)**（用於允許不在您好友名單中的用戶對您發起交易）
* 您的 `匹配類型`（用於顯示和匹配）
* 您在`TradingPreferences`中設置 的`MatchEverything`的值（用於顯示和匹配）
* 您庫存里符合`MatchableTypes`的 Steam 物品總數量（用於顯示和匹配）
* 符合`MatchableTypes`的 Steam 物品所屬遊戲總數（用於顯示和匹配）

ASF **不會**收集任何其他上述清單之外的數據，因此這一點不會在更改日誌中另行通知。 我們不認為上述任何事情應該被嚴肅地看待，我們提到它是為了讓您知道ASF在您自己配置的內容中做了什麼，因此人們可以更好地理解我們的觀點。

* * *

# 數據使用方式

在第二點中提到的所有值都用於我們的 **公開 ASF STM 清單**，這將在下面解釋。 我們不會將任何其他數據用於任何其他目的。

* * *

## 公開 ASF STM 清單

我們的公開 ASF STM 清單位于**[此處](https://asf.justarchi.net/STM)**，它被用作使用` MatchActively `的ASF用戶的公共服務，以及幫助ASF和非ASF用戶進行手動匹配。

### 工作原理

登錄後，ASF 會發送一次初始數據，其中包含公開清單會使用的所有屬性。 然後，每10分鐘，ASF 發送一個非常小的心跳請求，通知我們的伺服器機械人仍在運行。 如果由於某種原因心跳訊號沒有到達，例如由於網絡問題，ASF 將每分鐘重試發送一次，直到伺服器註冊它。

這允許我們的網站記錄哪些帳戶可用於匹配，以及它們是否仍處於活動狀態。 多虧了這一點，我們的網站可以顯示在**過去15分鐘**中活躍的所有啟用 ASF 2FA 和 STM 的帳戶。

用戶按照他們的庫存（按降序排序）——首先是` MatchEverything `機械人，`Any`橫幅意味著它接受所有1：1交易，然後是符合` MatchableTypes `的遊戲計數，最後是符合` MatchableTypes `的物品計數。

請注意，如果您未符合所有要求，您將**不會**在網站上顯示 。 在這種情況下，ASF甚至不會費心與我們的服務器通信，因此，如果您沒有啟用` SteamTradeMatcher `以幫助自己匹配冗餘物品，則會完全跳過第二點。 Also public listing is compatible only with latest stable version of ASF and may refuse to display outdated bots, especially if they're missing core functionality that can be found only in newer versions.

### API

ASF STM 清單暫時只接受 ASF 機械人。 目前無法在我們的清單中列出第三方機械人（因為我們無法輕鬆查看其代碼並確保它們符合我們的整個交易邏輯）。

如果您正在尋找以編程方式訪問我們清單的簡便方法，我們有一個非常簡單的**[/Api/Bots](https://asf.justarchi.net/Api/Bots)**端點供您使用。 這也是ASF在內部用於` MatchActively `用戶的端點。

### 注意

*整個概念，網站集成以及 ASF 報表仍處於測試階段——可能隨著時間的推移進行更新/删除（一旦我們覺得對某功能沒有足夠的興趣，就會將其刪除）。*

* * *

## 選擇退出權

儘管我們高度鼓勵未來的計劃，但參與統計資訊**並非強制性的**。 如果您希望不為人知地使用ASF，我們不予置評，您可以將全域配置中的` Statistics `屬性切換為 `false`來**完全**禁用統計。 禁用統計資訊將使整個模組無法運行，且不會執行上述隱私政策中指定的任何操作。

Disabling statistics may influence **our technical support, ASF functionality and other things that are offered to you for free**. For example, you can't make use of `MatchActively` without having `Statistics` enabled (as you refuse to communicate with our server for the functionality to work).
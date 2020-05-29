# 設置指南

如果您是首次來訪，歡迎！ 我們很高興又見到一位對我們的項目感興趣的訪客，但請記住能力越強責任越大──只要你**足夠認真去學習如何使用它**，ASF 有能力完成非常多的 Steam 相關事務。 書山有路勤為徑，學海無涯苦作舟，在開始這段陡峭的學習旅途之前，我們期待您閱讀這方面的wiki，詳細了解這一切是如何運作的。

如果您還在這裏，那就意味著您認同我們上面的文字，不錯。 除非你跳過它，否則您很快就會經歷**[一段艱難的時間](https://www.youtube.com/watch?v=WJgt6m6njVw)**⋯⋯ 無論如何，ASF是一個控制台應用程序，這意味著應用程式不會提供您習慣的友好圖型介面。 ASF 主要設計以在伺服器上執行，所以它僅作為一個服務（守護進程）運行，而非桌面應用程式。

然而這並不代表您不能在 PC 上使用它，或它在某種程度上比常見的程式更複雜，並非如此。 ASF是一個獨立的程式，不需要安裝並且可以立即使用，但這之前需要進行設置。 設置 ASF 以定義它啟動之後應該做什麼。 如果你在沒有設置的情況下啟動它，那麼 ASF 將不會做任何事情，就是這麼簡單。

* * *

## 特定操作系統設置

一般來說，這是我們在接下來的幾分鐘內要做的事情：

- 安裝 **[.NET 核心套件](#net-core-prerequisites)**。
- 下載適合您操作系統的**[最新版 ASF ](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**
- 將安裝包解壓縮到您指定的位置 ( 若你使用Linux/OS X 系統，則需執行 `chmod +x ArchiSteamFarm`命令)。
- **[配置ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**。
- 啟動 ASF，見證奇蹟的時刻！

輕而易舉，對吧？ 所以讓我們繼續吧。

* * *

### .NET 核心套件

第一步是確保您的操作系統可以正確地啟動 ASF。 ASF 是用 C# 語言編寫的，基於 .NET Core，並且可能需要你的平台上尚不可用的原生程式庫。 根據您是否使用 Windows、Linux 或 OS X, 您將有不同的要求, 它們都列在您應該遵循的 **[.NET 核心套件](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)** 文檔中。 這是我們應該使用的參考資料，但為了簡單起見，我們還在下面詳細介紹了所有需要的套裝軟件。因此您無需閱讀完整的文檔。

在通常情況下，您正在使用的其他軟件已安裝了其依賴項，因此您的操作系統上可能已經存在某些（甚至全部）依賴項。 然而，您還是應運行相應安裝程式以確保您的操作系統上確實已經存在 ASF 的依賴項──否則 ASF 無法啟動。

請謹記，您不需要為特定于操作系統的 ASF 安裝包執行任何其他操作，尤其是 .NET Core SDK 甚至是運行時環境，因為此包自帶這些內容。 您只需要 .NET Core 核心套件（依賴項）即可運行 ASF 中包含的 .NET Core 運行時。

#### **[Windows](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-windows)**:

- **[Microsoft Visual C++ 2015 可轉散發套件 Update](https://www.microsoft.com/en-us/download/details.aspx?id=53587)**（64 位元版本 Windows 請下載 x64，32 位元版本 Windows 請下載 x86）
- 強烈建議您確保已安裝所有 Windows 更新。 至少需要 **[KB2533623](https://support.microsoft.com/en-us/help/2533623/microsoft-security-advisory-insecure-library-loading-could-allow-remot)** 和 **[KB2999226](https://support.microsoft.com/en-us/help/2999226/update-for-universal-c-runtime-in-windows)** 版本，並可能需要更新的版本。 如果您的 Windows 是最新版本，則上述所有更新都已安裝。 在安裝 Visual C ++ 套件之前，請確保滿足這些要求。

#### **[Linux](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-linux)**:

套裝名稱因您使用的 Linux 版本而異，我們列出了最常見的套裝軟件名稱。 您可以獲得所有這些適用於您操作系統的本機套件管理器（如適用於 Debian 的`apt`或適用於 CentOS 的 `yum`）。

- `libcurl`（`libcurl4`、`libcurl3`）
- `libicu`（您的 Linux 發行版的最新版本，例如 `libicu60`）
- `libkrb5-3`（`krb5-libs`）
- `liblttng-ust0`（`lttng-ust`）
- `libssl`（`libssl1.1`、`openssl-libs`、您的 Linux 發行版最新的 1.1.X 版本）
- `zlib1g`（`zlib`）

其中至少應該有幾個套件已經可用於您的系統本地了（例如現今幾乎每一個 Linux 發行版都需要 `zlib1g`）。

#### **[OS X](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-macos)**:

- 暫時沒有必要條件，但你應該安裝最新的 OS X 版本，至少 10.13 以上版本。

* * *

### 下載

由於我們已經有了所有必需的依賴項，下一步是下載 **[最新發佈的ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**。 ASF有多種型號可供選擇，您盡可選擇與您的操作系統和架構相匹配的軟件包。 例如，如果您使用` 64 `位` Windows `，那麼您需要` ASF-win-x64 `包。 有關可用變體的更多信息，請訪問** [兼容性](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility) **部分。 ASF也可以運行在我們沒有為其構建特定於操作系統的軟件包的操作系統上，例如** 32位Windows **，詳情請見** <a href =“＃generic-setup” >通用設置</a> **。

![資產](https://i.imgur.com/Ym2xPE5.png)

下載完成之後，從解壓縮 ZIP 檔至一個資料夾中開始。 我們建議使用 **[7-zip](https://www.7-zip.org)**，Linux/OS X 的標準工具程式（例如 `unzip`）應該也沒有問題。 解壓縮之後，您將會擁有一大堆資料夾和檔案。 別擔心，我們會迅速清理它。

如果您使用的是Linux / OS X，請不要忘記執行` chmod + x ArchiSteamFarm `命令，因為權限不會自動設置在zip檔案中。 這在解壓縮後只需進行一次。

建議將 ASF 解壓縮到**獨立的目錄**中而不是任何已有其他用途的目錄—— ASF 的自動更新功能將會在升級時刪除所有舊的和無關檔案，可能會使您丟失您儲存於 ASF 目錄的任何檔案。 如果您有任何要與ASF一起使用的額外腳本或档案，請將它們放在之前提到的資料夾中。

示例結構如下所示：

    C:\ASF (where you put your own things)
        ├── ASF shortcut.lnk (optional)
        ├── Config shortcut.lnk (optional)
        ├── Commands.txt (optional)
        ├── MyExtraScript.bat (optional)
        ├── ... (any other files of your choice, optional)
        └── Core (dedicated to ASF only, where you extract the archive)
             ├── ArchiSteamFarm.dll
             ├── config
             └── (...)
    

這也是我們推薦的結構，因此您不需要查看ASF中包含的大量資料夾和檔案，因為您只需要一個配置資料夾和主二進制檔的快捷方式用於使用。

開始準備 ASF 結構吧。 您可以跳到下一個步驟，因為整理 ASF 結構並不是必需的（特別是特定作業系統建置套件使用者），但進行此步驟可以使您的生活多一點簡單。

開啟 ASF 資料夾，然後找到核心執行檔：在 Windows 上是 `ArchiSteamFarm.exe`，在 Linux/OS X 上是 `ArchiSteamFarm`。右鍵按一下它然後選取「複製」。 現在導航到您實際想要放置 ASF 快捷方式的地方（如桌面）， 按右鍵並選擇「粘貼快捷方式」。 如果需要，可以重命名快捷方式，例如為其命名為「ASF」。 現在，您可以在與二進位ASF檔案相同的位置找到 `config` 目錄。

經過一次迅速清理後，您現在將擁有一個簡便的結構，類似這樣：

![架構](https://i.imgur.com/k85csaZ.png)

這將允許您輕鬆訪問二進位ASF檔案和設定檔。 於我而言，我決定使用上面提到的結構，所以我將 ASF 檔直接放置在 「Core」 目錄中。 您可以根據自己的喜好調整此結構，例如在桌面和 ASF 目錄中使用 ASF + 配置快捷方式，例如在 `C:\ASF` 中，這取決於您自己。

Linux/OS X 使用者也建議做相同的事情，您可以透過 `ln -s` 指令來使用出色的符號連結機制。

* * *

### 配置

我們現在已經準備好做最後一步，配置。 這是迄今為止最複雜的一步，因為它涉及許多您還不熟悉的新信息，因此我們將嘗試在此提供一些易於理解的示例和簡化說明。

首先，** [配置](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration) **頁面解釋了**一切**與配置有關的事情，但它包含大量的新信息，其中的很多信息我們並不需要立即理解。 相反，我們將教您如何獲取您實際要找的資訊。

ASF 設定檔可以用兩種方法完成——要不使用我們的網頁設定檔產生器，要不就是手動設定。 這將在 **[配置](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)** 部分中進行深入解釋，因此如果您想要更詳細的資訊，請參考。 我們將使用便於操作的網頁設定檔產生器。

使用您喜歡的瀏覽器導航到我們的** [網頁設定檔產生器](https://justarchinet.github.io/ASF-WebConfigGenerator) **頁面，若您之前手動禁用了 Javascript，您需要啓用之。 我們推薦 Chrome 或 Firefox，但它應該適用于當前所有流行的瀏覽器。

打開頁面後，切換到「機械人」選項卡。 現在，您應該會看到類似于下面的頁面：

![Bot tab](https://i.imgur.com/aF3k8Rg.png)

如果您剛剛下載的 ASF 版本比網頁設定檔產生器的預設版本更舊，從下拉菜單中選擇您的 ASF 版本即可。 因為配置生成器可用於生成配置到尚未標記為穩定的較新（預發布）ASF 版本，所以這种情況有可能發生。 您下載的是經過驗證可靠運行的 ASF 最新穩定版本。

從在突出顯示為紅色的字段填入機械人的名稱開始。 這可以是您想要使用的任何名稱，例如您的昵稱、帳戶名、號碼或任何其他名稱。 只有一個詞不能使用，`ASF`，因為該關鍵字是為全域設定檔保留的。 除此之外，您的機械人名稱不能以點開頭（ASF會忽略這些檔）。 我們還建議您避免使用空格，如果需要，可以使用 `_` 作為單詞分隔符號。

在確定名稱後，開啓` Enabled `開關，這將定義 ASF 是否應該在啟動（程序之後）自動啟動您的機械人 。

現在，您可以決定兩件事：

- 您可以在` SteamLogin `字段中填入您的登錄帳號，並在` SteamPassword `字段中填入您的登錄密碼
- 或者您可以讓它們空著

前者將允許 ASF 在啟動期間自動使用您的帳戶憑據，使您不必在每次 ASF 需要時手動輸入它們。 但是，您可以決定省略它們，在這種情況下它們不會被保存，因此 ASF 將無法在沒有您幫助的情況下自動啟動，您需要在運行時輸入它們。

ASF 需要您的登錄憑據，因為它使用內置的Steam用戶端實現，並且需要與您自己使用的用戶端相同的詳細資訊登錄。 您的登入認證只會儲存在您的電腦上的 ASF `Config` 目錄，不會儲存在其他地方，我們的網頁設定檔產生器是基於用戶端的，也就是說代碼是在您的瀏覽器本地執行來產生 ASF 設定檔的，您輸入的資訊永遠不會離開您的電腦，所以沒有必要擔心任何可能的敏感資料洩漏。 然而，無論是什麼原因讓您不想輸入登入認證，我們能夠理解，您可以稍後手動輸入到產生的檔案裡，或者徹底省略並只在 ASF 命令提示字元輸入。 更多安全性問題可以在**[設定檔](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-zh-TW)**部分找到。

您也可以選擇只省略一個欄位，例如 `SteamPassword`（Steam 密碼），這樣將能使 ASF 自動登入，不過仍然會詢問密碼（類似 Steam 用戶端）。 如果您在使用 Steam 家庭監護，要解鎖您的帳戶，您需要將 PIN 碼填入 `SteamParentalCode` 欄位。

在一連串的決定和選擇過後，您的網頁現在看起來會像是下圖：

![Bot tab 2](https://i.imgur.com/yf54Ouc.png)

您現在可以按下您現在可以按下「下載」按鈕了，網頁設定檔產生器將會產生一個名為您剛才輸入的名稱的 `json` 檔。 將檔案儲存到 ASF 的 `config` 目錄裡。 您可以使用您稍早建立的`設定檔`捷徑，或者直接在 ASF 檔案結構裡手動找到 `config` 目錄。

您的 `config` 目錄現在看來會像是這樣：

![架構 2](https://i.imgur.com/crWdjcp.png)

恭喜！ 您剛剛完成了 ASF 機械人的基礎配置。 我們很快就會對此進行擴展説明，現在這就是您需要的一切。

* * *

### 運行 ASF

您現在已準備好首次啟動該程序。 Simply double-click ASF shortcut, or `ArchiSteamFarm` binary in ASF directory.

按下以後，如果您在第一步正確地安裝了所有相依性，ASF 應該會正確地啟動，注意您的第一個 BOT（如果您沒忘記把產生的設定檔置放在 `config` 目錄裡），然後嘗試登入：

![ASF](https://i.imgur.com/u5hrSFz.png)

如果您在設定檔提供了 `SteamLogin` 和 `SteamPassword` 給 ASF 使用，ASF 將只會詢問您的 Steam Guard 權杖（電子郵件、兩步驟驗證或無，取決於您的 Steam 設定）。 如果您沒有提供這兩項，ASF 也會同時詢問您的 Steam 帳戶名稱和密碼。

如果您擔心 ASF 陳述的接下來會發生的事情，現在是仔細檢查我們的**[隱私權政策](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Statistics-zh-TW#當前的隱私權政策)**的好時機。

初次登入通過後，假設您的資訊填寫無誤，您的帳戶將會成功登入，ASF 也將會使用您未曾變更的預設設定開始掛卡：

![ASF 2](https://i.imgur.com/Cb7DBl4.png)

這表明 ASF 現在成功在您的帳戶上工作了，所以您現在可以最小化程式然後幹其他事。 經過足夠的時間後（取決於**[效能](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Performance-zh-TW)**），您將會看到 Steam 交換卡片慢慢掉落。 當然，要使那發生，您必須擁有可以掛卡的遊戲，可以在 **[Steam 徽章](https://steamcommunity.com/my/badges)**頁面看到「還有 N 張卡片會掉落」的藍色提示——如果沒有遊戲可以掛卡，那麼 ASF 將會提示「這個帳戶目前沒有需要掛卡的遊戲！」，跟我們的**[常見問題](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-zh-TW#什麼是-ASF)**裡描述的一樣。

我們最基本的新手上路指南到此結束。 您現在可以決定要進一步設定 ASF，還是讓 ASF 以預設設定工作。 我們將會介紹更多基本細節，然後您可以自己探索整個 Wiki。

* * *

### 擴展配置

#### 同時讓多個帳戶掛卡

ASF 支援不止一個帳戶同時掛卡，也是它的主要功能。 您可以透過產生更多 BOT 設定檔來添加更多帳戶，方法跟您幾分鐘前產生的第一個設定檔完全相同。 您只需要確保兩件事：

- BOT 名稱唯一，例如假設您的第一個 BOT 名為「主帳戶」，您就不能擁有另一個跟它名稱一樣的 BOT。
- 登入資訊有效，例如 `SteamLogin`、`SteamPassword` 和 `SteamParentalCode`（如果正在使用 Steam 家庭監護）

換句話說，只要再次跳轉到設定檔部分，然後做完全一樣的事，只不過這次要填入您第二或是第三個帳戶的資訊。 記住為您所有的 BOT 使用唯一的名稱。

* * *

#### 變更設定

您可以透過完全相同的方式來變更現有的設定——產生一個新的設定檔。 如果您還沒有關閉網頁設定檔產生器，按一下「開啟進階設定」然後看看有什麼是您發現您所需要的選項。 在這個教學課程裡，我們要來變更 `CustomGamePlayedWhileFarming` 設定，這個選項使您可以設定 ASF 掛卡時顯示自訂遊戲名稱，而不是真正的遊戲名稱。

那就開始吧，如果您用預設設定執行 ASF 並開始掛卡，您將會看見您的 Steam 帳戶正在遊戲中：

![Steam](https://i.imgur.com/1VCDrGC.png)

那就讓我們一起來完善吧。 在網頁設定檔產生器中按一下「開啟進階設定」然後找到 `CustomGamePlayedWhileFarming`。 找到後，輸入您想要顯示的的自訂文字，例如「掛卡中（Idling cards）」：

![Bot tab 3](https://i.imgur.com/gHqdEqb.png)

現在跟之前一樣下載新的設定檔，然後用新的設定檔**取代**舊的。 當然，您也可以先刪除舊的設定檔，然後再放置新的。

一旦完成並重新啟動 ASF，您將注意到現在 ASF 會在之前的位置顯示您的自訂文字：

![Steam 2](https://i.imgur.com/vZg0G8P.png)

這表示您成功地編輯了設定檔。您也可以用完全相同的方法來變更全域 ASF 屬性，切換到「分頁」，下載產生的 `ASF.json` 設定檔，然後放置在 `config` 目錄裡。

* * *

#### 使用 ASF-ui

ASF 是一個沒有圖形使用者介面的主控台應用程式。 但是，我們正積極致力於**[ ASF-ui ](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC#asf-ui)**我們的 IPC 前端接口，它將以合適且對用戶友好的方式來訪問各種 ASF 功能。

要使用 ASF-ui，您應該確保（在 ASF 分頁）設定了全域設定檔屬性 `IPC` 和 `SteamOwnerID`。

您需要將您帐户獨一無二的 Steam 64 位 ID 填入 `SteamOwnerID`。 您可以透過各種方法查看 ID，我們將會使用 **[SteamRep](https://steamrep.com)** 來查看。 開啟網站，在右上角找到「Sign in through Steam」按鈕，然後登入。 然後在相同的位置，按一下您的個人頭像，然後在您的個人資料中查看 `steamID64` 欄位。

![SteamRep](https://i.imgur.com/RUuJ63i.png)

例如我的帳戶的 Steam 64位 ID 就是這串數字 `76561198006963719`。 您的 ID 也與其相似並同樣以 `7656` 開始。 複製它。

現在重新回到網頁設定檔產生器，然後把剛剛複製的數字貼上 SteamOwnerID。

![SteamOwnerID](https://i.imgur.com/V6jslfQ.png)

您只需要再做一件事：開啟進階設定，找到並啟用 `IPC` 選項。

![IPC](https://i.imgur.com/NhujZCN.png)

現在您可以下載 ASF 設定檔並放置在 `config` 目錄，跟往常一樣。 然後重新啟動 ASF，您應該可以確認 IPC 介面正確啟動了：

![IPC 2](https://i.imgur.com/ZmkO8pk.png)

如果您所做的一切無誤，只要 ASF 正在執行，您現在將可以透過**[此連結](http://localhost:1242)**來存取 ASF 的 IPC 介面。 您可以使用 ASF-ui 用於各種目的，例如發送**[指令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-zh-TW)**。 請隨意看看來發現 ASF-ui 的全部功能。

![ASF-ui](https://raw.githubusercontent.com/JustArchiNET/ASF-ui/master/.github/previews/bots.png)

請注意，ASF-ui 當前處於預覽狀態，暫時無法保證所有功能都可用/正常工作，但要應對簡單的 ASF 使用應該綽綽有餘。

* * *

### 概要

您已經成功設定好讓 ASF 使用您的 Steam 帳戶，並且根據您的喜好進行了客製化。 如果您每一步都按照指南進行，那麼您甚至可以透過 ASF-ui 介面發送一條簡單的指令。 現在您可以閱讀整個**[設定檔](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-zh-TW)**部分以瞭解「進階設定」裡所有選項的用途和 ASF 的功能。 如果您有任何問題，請參閱**[常見問題](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-zh-TW)**，您可能遇到的所有，或至少大部分的问题都包含在內。 如果您想瞭解關於 ASF 的一切以及它如何讓您掛卡事半功倍，請閱讀 **[ASF Wiki](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Home-zh-TW)** 的剩餘部分。 祝您愉快！

* * *

## 通用設定

這部分是為想要安裝 ASF **[通用（Generic）](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-zh-TW#通用Generic)** 套件的進階使用者準備的。 如果您可以**[安裝特定作業系統（OS-specific）套件](#安裝特定作業系統OS-specific套件)**，我們不推薦安裝您安装通用套件。

您主要在以下三種情況才會用到通用套件（不過當然，您無須理由也可使用）：

- 當您正在使用沒有建置特定作業系統套件的系統（例如 32 位元 Windows）
- 當您已經或想要安裝 .NET Core 執行階段/SDK
- 當您想透過自行管理執行階段必要條件來最小化 ASF 結構大小

不過，請注意在這種情況下您將需要安裝 .NET Core 執行階段。 這表示一旦您的 .NET Core SDK（執行階段）不可用、過時或損壞，ASF 就無法工作。 這就是一般使用者不推薦使用這個套件的原因，因為這樣您就需要確保 .NET Core SDK（執行階段）符合執行 ASF 的需求，使用**我們**驗證過的 ASF 配套的 .NET Core 執行階段則不需要這麼做。

對於通用套件，您可以按照上面的「安裝特定作業系統（OS-specific）套件」指南來安裝，但有兩個微小的變化。 除了要安裝 .NET Core 必要條件以外，您還需安裝 .NET Core SDK，而且二進位檔 `ArchiSteamFarm.dll` 將取代特定作業系統套件的執行檔 `ArchiSteamFarm(.exe)`。 其他步驟完全一樣。

額外步驟：

- 安裝 **[.NET 核心套件](https://docs.microsoft.com/dotnet/core/install/dependencies?tabs=netcore31)**。
- 安裝適用於您作業系統的 **[.NET Core SDK](https://www.microsoft.com/net/download)**（或者至少安裝執行階段）。 大部分情況下您會需要一個安裝程式。 如果您不知道要安裝 .NET Core 的哪一個版本，請參閱**[執行階段必要條件](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-zh-TW#執行階段必要條件)**。
- 下載**[最新版本的 ASF ](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**通用（Generic）套件。
- 解壓縮檔案到新位置（若您正在使用 Linux/OS X 系統，請執行指令 `chmod +x ArchiSteamFarm.sh`）。
- **[配置ASF](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration)**。
- 透過輔助腳本或是手動在 Shell 中執行 `dotnet /path/to/ArchiSteamFarm.dll` 指令來啟動 ASF。

輔助腳本位於 `ArchiSteamFarm.dll` 二進位檔所在的位置（例如用於 Windows 的 `ArchiSteamFarm.cmd` 和 Linux/OS X 的 `ArchiSteamFarm.sh`）——這些檔案只包含在通用套件內。 如果您不想手動執行 `dotnet` 指令，請使用輔助腳本。 您也可以跟特定作業系統套件一樣，為輔助脚本建立捷徑，因為輔助腳本就是用來代替主程式的。 很明顯，如果您未安裝 .NET Core SDK 而且 `dotnet` 執行檔不在系統 `PATH` 環境變數中，輔助腳本將無法執行。 輔助腳本完全是非必要的，您永遠可以透過手動執行 `dotnet /path/to/ArchiSteamFarm.dll` 指令來啟動 ASF。
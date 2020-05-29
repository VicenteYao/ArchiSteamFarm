# 背景序號啟動器

背景序號啟動器是 ASF 內建的特殊功能，可以讓你匯入一批 Steam 序號（包含遊戲名稱）以便在背景啟用。 如果你需要啟動大量的序號，且在全數啟動前肯定會觸發 `RateLimited` **[狀態](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/FAQ-zh-TW#啟用遊戲序號時的狀態是什麼意思)**，此時背景啟動功能將十分有用。

背景序號啟動器僅對單個 BOT 有效，也就是說它不會採用 `RedeemingPreferences` 的設定。 如有需要，這個功能可以和 `redeem` **[指令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-zh-TW)**一起使用（或者代替它）。

* * *

## 匯入

匯入可以透過兩種方式進行，使用文字檔或 IPC。

### 文字檔

ASF 會辨識 `config` 資料夾下名為 `BotName.keys` 的檔案，其中 `BotName` 是你的 BOT 名稱。 該檔案必須按固定格式編寫，每行由遊戲名稱和遊戲序號組成，兩者之間須以 Tab 分隔，最後以一個換行符結束表示開始下一項。 如果使用多個 Tab，則第一項會被認定是遊戲名稱，最後一項會被認定是遊戲序號，中間的所有內容將被忽略。 範例：

    POSTAL 2	ABCDE-EFGHJ-IJKLM
    Domino Craft VR	12345-67890-ZXCVB
    A Week of Circus Terror	POIUY-KJHGD-QWERT
    Terraria	忽略	忽略	ZXCVB-ASDFG-QWERT
    

此外，你也可以只使用遊戲序號（序號之間仍須隔一個換行符）。 在這種情況下，如果可能，ASF 將會向 Steam 詢問正確的遊戲名稱。 我們建議你自行標記所有序號的名稱，因為在 Steam 上啟動的 Package 名稱不一定會符合 Package 中的遊戲名稱，所以根據開發人員填寫的內容，你可能會看到正確的遊戲名稱、自訂名稱（例如 Humble Indie Bundle 18），或完全錯誤、甚至是惡意的名稱（例如 Half-Life 4）。

    ABCDE-EFGHJ-IJKLM
    12345-67890-ZXCVB
    POIUY-KJHGD-QWERT
    ZXCVB-ASDFG-QWERT
    

無論你選擇哪種格式，ASF 都將在 BOT 啟動或執行時匯入你的 `keys` 檔案。 成功解析並忽略無效的序號後，所有正確辨識的遊戲將會加入背景佇列，而 `BotName.keys` 檔案也會自動從 `config` 資料夾移除。

### IPC

除了使用上述的遊戲序號文字檔外，ASF 也開放了可供任意 IPC 工具（包括我們的 ASF-ui）使用的 `GamesToRedeemInBackground` **[ASF API 端點](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-zh-TW#asf-api)**。 IPC 將可能提供更完善的功能，因為你可以使用你覺得合適的方式進行解析。例如使用自訂分隔符，而非強制使用 Tab，甚至可以完全自訂序號格式。

* * *

## 佇列

成功匯入遊戲後，它們會加入佇列中。 只要 BOT 與 Steam 網路保持連線，且佇列中仍有遊戲，ASF 就會自動處理背景佇列。 嘗試啟動序號且沒有觸發 `RateLimited` 後，該序號將會被移出佇列並根據其啟動結果寫入位於 `config` 資料夾中的檔案。當序號被使用時（例如結果為：`NoDetail`、`BadActivationCode`、`DuplicateActivationCode`），將會寫入 `BotName.keys.used`，否則就會寫入 `BotName.keys.unused`。 由於 Steam 網路不一定會回傳序號所屬遊戲的正確名稱，所以 ASF 會使用你提供的遊戲名稱。這樣你就可以根據需要使用自訂名稱標記你的序號。

如果在啟動過程中帳戶觸發 `RateLimited` 狀態，佇列將會暫停一小時以等待冷卻時間結束。 接著，啟動程序將會從中斷的地方繼續，直到佇列完全清空。

* * *

## 範例

假設你有一個包含 100 個序號的清單。 首先你應該在 ASF 的 `config` 資料夾中建立一個名為 `BotName.keys.new` 的檔案。 我們加上 `.new` 後綴是為了防止 ASF 在建立檔案時立刻讀取該檔案（因為它是一個空白檔案，尚未準備匯入）。

現在你可以打開剛建立的檔案並將 100 個序號貼上，並視情況修正格式。 之後 `BotName.keys.new` 檔案中應該正好有 100 行（或如果末尾有空行的話就是 101 行），每一行的格式均為 `遊戲名稱\t遊戲序號\n`，其中 `\t` 是 Tab 符，`\n` 是換行符。

你現在可以將該檔案從 `BotName.keys.new` 重新命名為 `BotName.keys`，以便讓 ASF 知道該檔案已準備好匯入。 重新命名後，ASF 會自動匯入該檔案（不需要重啟），並在確認所有遊戲皆解析成功、加入佇列後刪除該檔案。

除了 `BotName.keys` 檔案，您也可以使用 IPC API 端點，甚至也可以根據需要將兩種方式合併使用。

經過一段時間後，可能會產生 `BotName.keys.used` 和 `BotName.keys.unused` 等兩個檔案。 這兩個檔案包含了啟動過程的結果。 舉例來說，你可以將 `BotName.keys.unused` 重新命名為 `BotName2.keys`，以便將未使用的序號交給其他的 BOT 啟動，因為前一個 BOT 並未用到這些序號。 或者您也可以將未使用的序號複製貼上到其他檔案留作他用。 請留意，當 ASF 處理佇列時，新的項目會逐一寫入 `used` 和 `unused` 等兩個輸出檔案中，因此建議等待佇列完全清空後再使用這兩個檔案。 如果要在佇列完全清空之前存取這些輸出檔案的話，請先將欲存取的檔案**移動**到別的資料夾，**然後**再對其做進一步處理。 這是因為 ASF 可能會在你處理這些檔案的時候寫入新的結果，且可能導致某些序號遺失。例如，你讀取了一個包含 3 個序號的檔案，然後將其刪除，但 ASF 在此期間又寫入了 4 個新序號，那些序號便會遺失。 如果你想存取這些檔案，請務必先將它們從 ASF 的 `config` 資料夾中移出，例如將其重新命名。

你也可以在佇列已有遊戲的情況下匯入更多遊戲，只需要重覆上述步驟就行了。 ASF 會正確地將其加入正在處理的佇列中並完成啟動程序。

* * *

## 備註

背景序號啟動器在底層使用了 `OrderedDictionary`，意思是遊戲序號將會按照檔案中（或 IPC API 呼叫）的順序啟動。 這代表如果某些序號需要先擁有另一個序號才能啟動，請將其列於該序號後方。 舉例來說，如果你有 DLC `D`，且需要先啟動遊戲 `G` 才能啟動，那麼你**必須**將遊戲 `G` 的序號排在 DLC `D` 的前面。 同樣地，如果啟動 DLC `D` 之前需先啟動 `A`、`B` 和 `C`，那麼這三個序號就應該放在前面（任意順序均可，除非它們各自也有相依關係）。

如果未按照上方所述的方式啟動，就會導致 DLC 啟動失敗並回傳 `DoesNotOwnRequiredApp` 結果，即使你的帳戶在完成整個佇列後就可以啟動該 DLC，它也不會在此時被啟動。 如要避免這種錯誤，請務必確保佇列中的 DLC 列在遊戲本體之後。
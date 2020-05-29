# 雙重驗證

2015年12月，Valve啟用「Escrow」系統，推出交易暫掛功能，該系統要求用戶提供額外的身份驗證器以確認各種與帳戶相關的活動。 詳情請見**[「交易與市場確認」](https://support.steampowered.com/kb_article.php?ref=1284-WTKB-4729)**及**[交易與市集託管](https://support.steampowered.com/kb_article.php?ref=8078-TPHC-6195)**。 在試圖理解 ASF 2FA 背後的邏輯之前，首先要了解 2FA 「雙重驗證」系統。

當前交易託管期最長達15天，雖說這對ASF來說無關緊要，但對想要實現完全自動化的用戶而言相當麻煩。 幸運的是，ASF提供了解決這個問題的方案，稱為ASF 2FA。

* * *

# ASF 邏輯

ASF擁有適當的邏輯並完全適用於受標準2FA保護的帳戶，無論您是否啟用ASF 2FA。 它會在需要時「例如在登錄期間」向您請求所需的詳細資訊。 如果您使用ASF 2FA，程式將能夠跳過這些請求並自動生成所需的代碼，從而省去您的麻煩並啟用額外的功能「如下所述」。

* * *

# ASF 2FA

ASF 2FA是負責為ASF進程提供2FA功能的內置模組，例如生成代碼和接受確認。 它複製了您現有的身份驗證器，因此無需專門使用ASF 2FA。

您可以執行`2fa`**[命令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**以檢查機械人帳戶是否已啟用2FA。 除非您已將身份驗證器導入為ASF 2FA，否則所有`2fa`命令都將無法執行，當您的帳戶未使用ASF 2FA時，一些需要2FA模組支援的進階功能亦無法運行。

要啟用ASF 2FA，您需要具備：

- 適用於Android裝置的Steam行動驗證器
- 或適用於iOS裝置的Steam行動驗證器
- 或適用於**[SteamDesktopAuthenticator](https://github.com/Jessecar96/SteamDesktopAuthenticator)**的Steam行動驗證器
- 或適用於**[WinAuth](https://winauth.github.io/winauth)**的Steam行動驗證器
- 或其他任何能夠獲取shared/identity secret及裝置ID以實現Steam行動驗證器功能的應用

* * *

## 導入

為了完成下面解釋的步驟，您應拥有 ASF 支援的已連結並可操作的身份驗證器。 ASF目前支援幾個不同的2FA來源──Android、iOS、SteamDesktopAuthenticator以及WinAuth。 如果您還沒有任何驗證器，則需要先選擇其中一個並進行設置。 如果您不知道選擇哪一個，我們推薦 WinAuth，但只要您按照說明操作，上述任何一項都可以正常工作。

以下所有指南都要求您已擁有在上述工具/應用程式中 **可運行的**身份驗證器。 如果導入無效資料，ASF 2FA將無法正常運行，因此在嘗試導入資料之前，請確保您的身份驗證器運行正常。 這包括測試和驗證以下身份驗證器功能能否正常運行：

- 您可以生成代碼，且它們受Steam網絡承認
- 您可以由流動身份驗證器獲取交易確認
- 您可以接受這些交易確認，並且它們被Steam網絡正確地識別為確認/拒絕

通過檢查上述操作是否有效來確保您的身份驗證器正常工作──如果它們不起作用，那麼它們也不能在ASF中運行，您只會浪費時間給自己添麻煩。

* * *

### Android手機

通常情況下，您需要**[root](https://en.wikipedia.org/wiki/Rooting_(Android_OS))**權限以從您的Android手機導入身份驗證器。 Root方法因裝置而異，所以我無法指導您root您的設備。 您可以訪問**[XDA](https://www.xda-developers.com/root)**查詢實用指南並瞭解更多關於 rooting 的通用資訊。 如果您找不到適用於您的設備或教程，嘗試有效利用Google搜索。

理論上來説，沒有root權限就無法訪問受保護的Steam檔案。 The only official non-root method for extracting Steam files is creating unencrypted `/data` backup in one way or another and manually fetching appropriate files from it on your PC, however because such thing highly depends on your phone manufacturer and **is not** in Android standard, we won't discuss it here. 如果您很幸運有這樣的功能，你可以考慮利用它，但大多數用戶並非如此。

Unofficially, it is possible to extract the needed files without root access, by installing or downgrading your Steam app to version 2.1 (or earlier), setting up mobile authenticator and then creating a snapshot of the app (together with the `data` files that we need) through `adb backup`. 但是，由於這種提取文件的方式存在嚴重的安全漏洞，且完全沒有技術支援，我們將不會在此詳細說明，原因之一是Valve在新版本中禁用此安全漏洞，我們僅是提到存在使用此方法的可能性。

Assuming that you've successfully rooted your phone, you should afterwards download any root explorer available on the market, such as **[this one](https://play.google.com/store/apps/details?id=com.jrummy.root.browserfree)** (or any other one of your preference). 您還可以通過ADB（Android Debug Bridge）或任何其他可用的方法訪問受保護的檔案，我們將通過資源管理器進行訪問，因為它絕對是對用戶最友好的方式。

打開根瀏覽器後，導航到`/data/data`資料夾。 請記住，` /data/data `目錄受到保護，如果沒有root訪問權限，您將無法訪問它。 在那找到` com.valvesoftware.android.steam.community `資料夾並將其複製到` /sdcard `，它指向您的內置內部存儲。 之後，您應該可以將手機連接到PC並像往常一樣從內部存儲器中復製資料夾。 如果您確定已將資料夾複製到正確的位置可該資料夾無法顯示，請嘗試重新啟動手機。

Now, you can choose if you want to import your authenticator to WinAuth first, then to ASF, or to ASF right away. 先將驗證器導入WinAuth的選項更友好，它允許您在您的PC上備份身份驗證器，這樣您就可以從3個不同的地方生成代碼並確認交易──您的手機，您的PC以及ASF。 如果您想這樣做，只需打開WinAuth，添加新的Steam身份驗證器並從Android選項中選擇導入，然後遵循指南，訪問您之前獲得的檔案。 完成後，您可以將此驗證器從WinAuth導入ASF，這將在下面的WinAuth部分中專門進行說明。

If you don't want to or don't need to go through WinAuth, then simply copy `files/Steamguard-SteamID` file from our protected directory, where `SteamID` is your 64-bit Steam identificator of the account that you want to add (if more than one, because if you have only one account then this will be the only file). 您需要將該檔放入ASF` config `目錄中。 Once you do that, rename the file to `BotName.maFile`, where `BotName` is the name of your bot you're adding ASF 2FA to. After this step, launch ASF - it should notice the `.maFile` and import it.

    [*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
    <1> 請輸入您的裝置識別碼（包括"android:"）：
    

You will need to do only one more step - find your `DeviceID` property in `shared_prefs/steam.uuid.xml`. It will be inside XML tags and starting with `android:`. Copy that (or write it down) and put it in ASF as asked. If you did everything correctly, import should be finished.

    [*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
    

請確認接受確認實際上可用。 If you made a mistake while entering your `DeviceID` then you'll have half-broken authenticator - tokens will work, but accepting confirmations will not. You can always remove `Bot.db` and start over if needed.

* * *

### iOS

For iOS you can use **[ios-steamguard-extractor](https://github.com/CaitSith2/ios-steamguard-extractor)**. This is possible thanks to the fact that you can make decrypted backup, put in on your PC and use the tool in order to extract Steam data that is otherwise impossible to get (at least without jailbreak, due to iOS encryption).

Head over to **[latest release](https://github.com/CaitSith2/ios-steamguard-extractor/releases/latest)** in order to download the program. Once you extract the data you can put it e.g. in WinAuth, then from WinAuth to ASF (although you can also simply copy generated json starting from `{` ending on `}` into `BotName.maFile` and proceed like usual). If you ask me, I strongly recommend to import to WinAuth first, then making sure that both generating tokens as well as accepting confirmations work properly, so you can be sure that everything is alright. If your credentials are invalid, ASF 2FA will not work properly, so it's much better to make ASF import step your last one.

有關問題/錯誤，請訪問** [issues](https://github.com/CaitSith2/ios-steamguard-extractor/issues) **。

*請記住，上面的工具是非官方的，您使用它需要自擔風險。 We do not offer technical support if it doesn't work properly - we got a few signals that it's exporting invalid 2FA credentials - verify that confirmations work in authenticator like WinAuth prior to importing that data to ASF!*

* * *

### Steam桌面驗證器

如果您的身份驗證器已經在SDA中運行，您應該注意到` maFiles `資料夾中存在` steamID.maFile `文件。 將該檔複製到ASF的` config `目錄。 Make sure that `.maFile` is in unencrypted form, as ASF can't decrypt SDA files - unencrypted file content should start with `{` character.

You should now rename `steamID.maFile` to `BotName.maFile` in ASF config directory, where `BotName` is the name of your bot you're adding ASF 2FA to. 或者您可以保持原樣，ASF會在登錄後自動識別它。 Helping ASF makes it possible to use ASF 2FA before logging in, if you won't help ASF, then the file can be picked only after ASF successfully logs in (as ASF doesn't know `steamID` of your account before in fact logging in).

如果您正確執行了所有操作，請啟動ASF，您應該注意到：

    [*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
    [*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
    

從現在開始，您的ASF 2FA應該可以在此帳戶運行。

* * *

### WinAuth

Firstly create new empty `BotName.maFile` in ASF config directory, where `BotName` is the name of your bot you're adding ASF 2FA to. Remember that it should be `BotName.maFile` and NOT `BotName.maFile.txt`, Windows likes to hide known extensions by default. 如果您提供的名稱不正確，ASF將不會識別它。

現在像往常一樣啟動WinAuth。 右鍵單擊Steam圖標，然後選擇“顯示SteamGuard和恢復代碼”。 然後選擇“允許複製”。 You should notice familiar to you JSON structure on the bottom of the window, starting with `{`. Copy whole text into a `BotName.maFile` file created by you in previous step.

如果您正確執行了所有操作，請啟動ASF，您應該注意到：

    [*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
    <1> 請輸入您的裝置識別碼（包括"android:"）：
    

This is when tricky part comes in. WinAuth is missing deviceID property that is required by ASF, so you'll need to do one more thing.

Go back to WinAuth's "Show SteamGuard and Recovery Code" and you should notice "Device ID" property above the JSON code you were copying not that long ago. Copy whole android device ID, including `android:` part into ASF.

If you've done that properly as well, you're now done!

    [*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
    

請確認接受確認實際上可用。 If you made a mistake while entering your `DeviceID` then you'll have half-broken authenticator - tokens will work, but accepting confirmations will not. You can always remove `Bot.db` and start over if needed.

* * *

## 完成

From this moment, all `2fa` commands will work as they'd be called on your classic 2FA device. You can use both ASF 2FA and your authenticator of choice (Android, iOS, SDA or WinAuth) to generate tokens and accept confirmations.

If you have authenticator on your phone, you can optionally remove SteamDesktopAuthenticator and/or WinAuth, as we won't need it anymore. However, I suggest to keep it just in case, not to mention that it's more handy than normal steam authenticator. Just keep in mind that ASF 2FA is **NOT** general purpose authenticator and it should **never** be the only one you use, since it doesn't even include all data that authenticator should have. It's not possible to convert ASF 2FA back to original authenticator, therefore always make sure that you have general-purpose authenticator in other place, such as in WinAuth/SDA, or on your phone.

* * *

## 常見問題

### ASF如何使用2FA模組？

如果ASF 2FA可用，ASF將使用它自動確認由ASF發送/接受的交易。 它還可以根據需要自動生成2FA代碼，例如為了登錄。 除此之外，還可以執行` 2fa `命令以使用ASF 2FA。 That should be all for now, if I didn't forget about anything - basically ASF uses 2FA module on as-needed basis.

* * *

### 為何我需要2FA代碼？

您需要2FA代碼才能訪問受2FA保護的帳戶，其中包括具有ASF 2FA的每個帳戶。 您應該在用於導入的身份驗證器中生成代碼，但您也可以通過聊天向給定機器人的發送` 2fa `命令生成臨時代碼。 您還可以使用`2fa <BotNames>`命令為給定的機械人實例生成臨時代碼。 這應該足以讓您訪問機械人帳戶，例如通過瀏覽器，但如上所述——您應該使用友好的身份驗證器（Android，iOS，SDA或WinAuth）。

* * *

### 在導入ASF 2FA後，我可以使用我原有的身份驗證器嗎？

是的，您的原始驗證器仍然可用並可以與ASF 2FA一起使用。 這就是整個過程——我們將您的身份驗證器憑據導入ASF，因此ASF可以使用它們並代表您接受選定的確認。

* * *

### ASF流動身份驗證器在哪裏保存？

ASF流動驗證器以及與給定帳戶相關的其他關鍵數據保存在配置目錄中的` BotName.db `檔案中。 如果您想移除ASF 2FA，請閱讀以下內容。

* * *

### 如何移除ASF 2FA？

Simply stop ASF and remove associated `BotName.db` of the bot with linked ASF 2FA you want to remove. This option will remove associated imported 2FA with ASF, but will NOT delink your authenticator. If you instead want to delink your authenticator, apart from removing it from ASF (firstly), you should delink it in authenticator of your choice (Android, iOS, SDA or WinAuth), or - if you can't for some reason, use revocation code that you received during linking that authenticator, on the Steam website. It's not possible to unlink your authenticator through ASF, this is what general-purpose authenticator that you already have should be used for.

* * *

### 我將身份驗證器鏈接到SDA/WinAuth，然後導入到ASF。 我現在可以取消鏈接並在手機上再次鏈接嗎？

**不**。 ASF **導入**您的身份驗證器數據以便使用它。 如上所述，如果您使用身份驗證器，那麼您也會導致ASF 2FA停止運行，無論您是否首先將其移除。 如果您想在手機和ASF上使用身份驗證器（加上SDA/WinAuth中的身份驗證器），那麼您需要從手機中**導入**您的身份驗證器，而不是在SDA/WinAuth中創建新身份驗證器。 您只能擁有**一個**鏈接身份驗證器，這就是ASF **導入**該身份驗證器及其數據的原因，以便將其用作ASF 2FA——它與原本的身份驗證器**相同**，只是存在於兩個地方。 If you decide to delink your mobile authenticator credentials - regardless in which way, ASF 2FA will stop working, as previously copied mobile authenticator credentials will no longer be valid. 如上所述，要在手機上將ASF 2FA與身份驗證器一起使用，您必須將其從Android/iOS導入。

* * *

### 使用ASF 2FA比WinAuth/SDA/其他驗證器接受所有確認更好嗎？

**是的**，有幾個原因。 First and most important one - using ASF 2FA **significantly** increases your security, as ASF 2FA module ensures that ASF will only accept automatically its own confirmations, so even if attacker does request a trade that is harmful, ASF 2FA will **not** accept such trade, as it was not generated by ASF. In addition to security part, using ASF 2FA also brings performance/optimization benefits, as ASF 2FA fetches and accepts confirmations immediately after they're generated, and only then, as opposed to inefficient polling for confirmations each X minutes done e.g. by SDA or WinAuth. In short, there is no reason to use third-party authenticator over ASF 2FA, if you plan on automating confirmations generated by ASF - that's exactly what ASF 2FA is for, and using it does not conflict with you confirming everything else in authenticator of your choice. We strongly recommend to use ASF 2FA for entire ASF activity - this is much more secure than any other solution.

* * *

## 進階

如果您是高級用戶，還可以手動生成maFile。 它應有的**[有效JSON結構](https://jsonlint.com)**如下：

```json
{
  "shared_secret": "STRING",
  "identity_secret": "STRING",
  "device_id": "STRING"
}
```

` device_id `在導入期間是可選的，但對於ASF操作是必需的——如果省略它，ASF將在導入期間請求它。 當然，您需要將`“STRING”`替換為每個字段中的有效內容。

標準驗證器數據有更多字段——在導入期間它們完全被ASF忽略，因為它們不是必需的。 You also don't have to remove them - ASF only requires valid JSON with 2 mandatory fields described above, and optionally also `device_id`.
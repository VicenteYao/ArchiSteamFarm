# 安全性

## SteamPassword

ASF目前支援4種類型的密碼：`PlainText`，`AES`，`ProtectedDataForCurrentUser`和無（`null` /`""`）。

為了使用加密密碼，您應該像往常一樣使用` PlainText `登錄Steam，然後使用 `password` ** <a href =“https：//github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands">命令</a> **生成加密密碼。 選擇您喜歡的加密方法，然後將您獲得的加密密碼作為機械人配置的` SteamPassword ` 屬性，最後不要忘記將` PasswordFormat `更改為與您選擇的加密方法匹配的格式。

* * *

### 明文

定義為` 0 `的` PasswordFormat `，這是存儲密碼的最簡單且不安全的方式。 ASF希望` SteamPassword `屬性為純文本——用於以直接形式登錄Steam的密碼。 它是最容易使用的，並且與所有設置100％兼容，因此它是預設值 。

* * *

### AES

** [ AES ](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard) **，符合當前安全標準的存儲密碼方式，定義為` 1 `的` PasswordFormat `。 ASF expects `SteamPassword` property to be a **[base64-encoded](https://en.wikipedia.org/wiki/Base64)** sequence of characters resulting in AES-encrypted byte array after translation, which then should be decrypted using included **[initialization vector](https://en.wikipedia.org/wiki/Initialization_vector)** and ASF encryption key.

只要攻擊者不知道用於解密的ASF內置加密密鑰以及密碼，上述方法就可以保證安全性。 ASF allows you to specify key via `--cryptkey` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments)**, which you should use for maximum security. If you decide to omit it, ASF will use its own key which is **known** and hardcoded into the application, meaning anybody can reverse the ASF encryption and get decrypted password. It still requires some effort and is not that easy to do, but possible, that's why you should almost always use `AES` encryption with your own `--cryptkey` which is kept in secret. AES method used in ASF provides security that should be satisfying and it's a balance between simplicity of `PlainText` and complexity of `ProtectedDataForCurrentUser`, but it's highly recommended to use it with custom `--cryptkey`.

* * *

### ProtectedDataForCurrentUser

目前存儲ASF密碼的最安全方式，比上面解釋的` AES `方法更安全，定義為` 2 `的` PasswordFormat `。 The major advantage of this method is at the same time the major disadvantage - instead of using encryption key (like in `AES`), data is encrypted using login credentials of currently logged in user, which means that it's possible to decrypt the data **only** on the machine it was encrypted on, and in addition to that, **only** by the user who issued the encryption. 這確保即使您將整個` Bot.json `發送給其他人，他也無法在不訪問您的PC的情況下解密密碼。 這是一個很好的安全措施，但同時主要缺點是兼容性最差，因為使用此方法加密的密碼將與任何其他用戶以及機器不兼容——包括**您自己的**，例如您想要重新安裝您的操作系統。 Still, it's one of the best methods of storing passwords, and if you're worried about security of `PlainText`, and don't want to put password each time with `None` option, then this is your best bet as long as you don't have to access your configs from any other machine than your own.

**請注意，此選項僅適用於運行Windows操作系統的電腦。**

* * *

### 無

保證100％安全性並確保沒有人可以竊取您的Steam密碼的唯一方法。 要使用此選項，只需將` SteamPassword `設置為空（`""`）或` null `。 ASF會在需要時詢問您的Steam密碼，並且不會將其保存在任何地方，僅保留於當前正在運行的進程中，直到您關閉它為止。 雖然這是處理密碼最安全的方法，但它也是最麻煩的，因為您需要在ASF運行時手動輸入密碼（每當需要時）。 如果這對您來說不是問題，那麼這是安全方面最好的選擇。

* * *

## 推薦

如果兼容性對您來說不是問題，並且您對` ProtectedDataForCurrentUser `方法的工作方式沒有問題，那麼將密碼存儲在ASF中是**推薦**選項，因為它提供最好的安全性。 ` AES `方法對於那些仍想在他們想要的任何機器上使用他們的配置的人來說是一個不錯的選擇，而` PlainText `是存儲密碼的最簡單的方法，如果您不介意任何人都可以查看JSON配置文件。

請記住，如果攻擊者可以訪問您的PC，則所有這3種方法都被視為**不安全**。 ASF必須能夠解密加密的密碼，如果您的機器上運行的程序能夠這樣做，那麼在同一台機器上運行的任何其他程序也能夠這樣做。 ` ProtectedDataForCurrentUser `是最安全的，因為**即使使用同一台PC的其他用戶也無法解密**，但如果有人能夠竊取您的登錄憑據和機器信息以及ASF配置文件，仍然可以解密數據 。

對於很少啟動ASF或者沒有在每個ASF啟動時輸入密碼而煩惱的人，` None `方式最安全，因為Steam密碼不會保存在任何地方。

* * *

# 解密

ASF不支持任何解密已加密密碼的方法，因為解密方法僅在內部用於訪問進程內的數據。 如果您想恢復加密程序，例如使用` ProtectedDataForCurrentUser `將ASF移動到其他機器，然後只需將` PasswordFormat `切換回` 0 `（PlainText），並填寫` SteamPassword `即可。 然後，您可以照常啟動ASF，並從頭開始重複該過程。
# Keamanan

## SteamPassword

ASF currently supports 4 types of passwords - `PlainText`, `AES`, `ProtectedDataForCurrentUser` and None (`null` / `""`).

In order to use encrypted password, you should firstly log in to Steam as usual with `PlainText`, then generate encrypted passwords using `password` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. Pick the encryption method you like, then put the encrypted password you got as `SteamPassword` bot config property, and finally don't forget to change `PasswordFormat` to the one that matches your chosen encryption method.

* * *

### PlainText

This is the most simple and insecure way of storing the password, defined as `PasswordFormat` of `0`. ASF expects `SteamPassword` property to be a plain text - password being used to log in to Steam in its direct form. It's the easiest one to use, and 100% compatible with all setups, therefore it's default.

* * *

### AES

Considered secure by today standards, **[AES](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard)** way of storing the password is defined as `PasswordFormat` of `1`. ASF expects `SteamPassword` property to be a **[base64-encoded](https://en.wikipedia.org/wiki/Base64)** sequence of characters resulting in AES-encrypted byte array after translation, which then should be decrypted using included **[initialization vector](https://en.wikipedia.org/wiki/Initialization_vector)** and ASF encryption key.

The method above guarantees security as long as attacker doesn't know built-in ASF encryption key which is being used for decryption as well as encryption of passwords. ASF allows you to specify key via `--cryptkey` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-Line-Arguments)**, which you should use for maximum security. If you decide to omit it, ASF will use its own key which is **known** and hardcoded into the application, meaning anybody can reverse the ASF encryption and get decrypted password. It still requires some effort and is not that easy to do, but possible, that's why you should almost always use `AES` encryption with your own `--cryptkey` which is kept in secret. AES method used in ASF provides security that should be satisfying and it's a balance between simplicity of `PlainText` and complexity of `ProtectedDataForCurrentUser`, but it's highly recommended to use it with custom `--cryptkey`.

* * *

### ProtectedDataForCurrentUser

Currently the most secure way of storing the password that ASF offers, and much safer than `AES` method explained above, is defined as `PasswordFormat` of `2`. The major advantage of this method is at the same time the major disadvantage - instead of using encryption key (like in `AES`), data is encrypted using login credentials of currently logged in user, which means that it's possible to decrypt the data **only** on the machine it was encrypted on, and in addition to that, **only** by the user who issued the encryption. This ensures that even if you send your entire `Bot.json` to somebody else, he will not be able to decrypt the password without access to your PC. This is excellent security measure, but at the same time has major disadvantage of being least compatible, as the password encrypted using this method will be incompatible with any other user as well as machine - including **your own** if you decide to e.g. reinstall your operating system. Still, it's one of the best methods of storing passwords, and if you're worried about security of `PlainText`, and don't want to put password each time with `None` option, then this is your best bet as long as you don't have to access your configs from any other machine than your own.

**Please note that this option is available only for machines running Windows OS.**

* * *

### Tak satupun

The only way that guarantees 100% security and ensures that nobody can steal your Steam password. In order to use this option simply set your `SteamPassword` to empty string (`""`) or `null` value. ASF will ask you for your Steam password when it's required, and won't save it anywhere but keep in memory of currently running process, until you close it. While being the most secure method of dealing with passwords, it's also the most troublesome as you need to enter your password manually on each ASF run (when it's required). If that's not a problem for you, this is your best bet security-wise.

* * *

## Recommendation

If compatibility is not an issue for you, and you're fine with the way how `ProtectedDataForCurrentUser` method works, it is the **recommended** option of storing the password in ASF, as it provides the best security. `AES` method is a good choice for people who still want to make use of their configs on any machine they want, while `PlainText` is the most simple way of storing the password, if you don't mind that anybody can look into JSON configuration file for it.

Please keep in mind that all of those 3 methods are considered **insecure** if attacker has access to your PC. ASF must be able to decrypt the encrypted password, and if the program running on your machine is capable of doing that, then any other program running on the same machine will be capable of doing so, too. `ProtectedDataForCurrentUser` is the most secure variant as **even other user using the same PC will not be able to decrypt it**, but it's still possible to decrypt the data if somebody is able to steal your login credentials and machine info in addition to ASF config file.

For people launching ASF rarely or those who are not bothered with entering the password on each ASF startup, `None` way is the most secure as Steam password is not saved anywhere.

* * *

# Decryption

ASF doesn't support any way of decrypting already encrypted passwords, as decryption methods are used only internally for accessing the data inside the process. If you want to revert encryption procedure e.g. for moving ASF to other machine when using `ProtectedDataForCurrentUser`, then simply switch your `PasswordFormat` back to `0` (PlainText), and fill `SteamPassword` appropriately. You can then launch ASF as usual, and repeat the procedure from beginning.
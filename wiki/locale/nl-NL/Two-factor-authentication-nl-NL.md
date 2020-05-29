# Tweestapsverificatie

A while ago Valve has introduced a system known as "Escrow" that requires extra authenticator for various account-related activity. You can read more about it **[here](https://support.steampowered.com/kb_article.php?ref=1284-WTKB-4729)** and **[here](https://support.steampowered.com/kb_article.php?ref=8078-TPHC-6195)**. It's crucial to understand 2FA system firstly, before trying to understand the logic behind ASF 2FA.

Now as you can see all trades are being hold for up to 15 days, which is not a major problem when it comes to our ASF, but can still be annoying, especially for those who want full automation. Luckily, ASF includes a solution to that problem, called ASF 2FA.

* * *

# ASF logic

Regardless if you use ASF 2FA explained below or not, ASF includes proper logic and is fully aware of accounts protected by standard 2FA. It will ask you for required details when they're needed (such as during logging in). If you use ASF 2FA, program will be able to skip those requests and automatically generate required tokens, saving you hassle and enabling extra functionality (described below).

* * *

# ASF 2FA

ASF 2FA is built-in module responsible for providing 2FA features to ASF process, such as generating tokens and accepting confirmations. It duplicates your existing authenticator, so there is no need to use ASF 2FA exclusively.

You can verify whether your bot account is using ASF 2FA already by executing `2fa` **[commands](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)**. Unless you've already imported your authenticator as ASF 2FA, all `2fa` commands will be non-operative, which means that your account is not using ASF 2FA, therefore it's also ineligible for advanced ASF features that require the module to be operative.

To enable ASF 2FA, you need to have:

- Working steam authenticator in your Android
- or working steam authenticator in your iOS
- or working steam authenticator in **[SteamDesktopAuthenticator](https://github.com/Jessecar96/SteamDesktopAuthenticator)**
- or working steam authenticator in **[WinAuth](https://winauth.github.io/winauth)**
- or any other working implementation of Steam authenticator with access to shared/identity secret and device's ID

* * *

## Importeren

In order to complete the steps explained below, you should have already linked and operational authenticator that is supported by ASF. ASF currently supports a few different sources of 2FA - Android, iOS, SteamDesktopAuthenticator and WinAuth. If you don't have any authenticator yet, you need to choose one of those and set it up firstly. If you don't know better which one to pick, we recommend WinAuth, but any of the above will work fine assuming you follow the instructions.

All following guides require from you to already have **working and operational** authenticator being used with given tool/application. ASF 2FA will not operate properly if you import invalid data, therefore make sure that your authenticator works properly before attempting to import it. This does include testing and verifying that following authenticator functions work properly:

- You can generate tokens and those tokens are accepted by Steam network
- You can fetch confirmations, and they are arriving on your mobile authenticator
- You can accept those confirmations, and they're properly recognized by Steam network as confirmed/rejected

Ensure that your authenticator works by checking if above actions work - if they don't, then they won't work in ASF either, you'll only waste time and cause yourself trouble.

* * *

### Android telefoon

In general for importing authenticator from your Android phone you will need **[root](https://en.wikipedia.org/wiki/Rooting_(Android_OS))** access. Rooting varies from device to device, so I won't tell you how to root your device. Visit **[XDA](https://www.xda-developers.com/root)** for excellent guides on how to do that, as well as general information on rooting in general. If you can't find your device or the guide that you need, try to find it on google second.

At least officially, it's not possible to access protected Steam files without root. The only official non-root method for extracting Steam files is creating unencrypted `/data` backup in one way or another and manually fetching appropriate files from it on your PC, however because such thing highly depends on your phone manufacturer and **is not** in Android standard, we won't discuss it here. If you're lucky to have such functionality, you can make use of it, but majority of users don't have anything like that.

Unofficially, it is possible to extract the needed files without root access, by installing or downgrading your Steam app to version 2.1 (or earlier), setting up mobile authenticator and then creating a snapshot of the app (together with the `data` files that we need) through `adb backup`. However, since it's a serious security breach and entirely unsupported way to extract the files, we won't elaborate further on this, Valve disabled this security hole in newer versions for a reason, and we only mention it as a possibility.

Assuming that you've successfully rooted your phone, you should afterwards download any root explorer available on the market, such as **[this one](https://play.google.com/store/apps/details?id=com.jrummy.root.browserfree)** (or any other one of your preference). You can also access the protected files through ADB (Android Debug Bridge) or any other available to you method, we'll do it through the explorer since it's definitely the most user-friendly way.

Once you opened your root explorer, navigate to `/data/data` folder. Keep in mind that `/data/data` directory is protected and you won't be able to access it without root access. Once there, find `com.valvesoftware.android.steam.community` folder and copy it to your `/sdcard`, which points to your built-in internal storage. Afterwards, you should be able to plug your phone to your PC and copy the folder from your internal storage like usual. If by any chance the folder won't be visible despite you being sure that you copied it to the right place, try restarting your phone first.

Now, you can choose if you want to import your authenticator to WinAuth first, then to ASF, or to ASF right away. First option is more friendly and allows you to duplicate your authenticator also on your PC, allowing you to make confirmations and generate tokens from 3 different places - your phone, your PC and ASF. If you want to do that, simply open WinAuth, add new Steam authenticator and choose importing from Android option, then follow instructions by accessing the files that you've obtained above. When done, you can then import this authenticator from WinAuth to ASF, which is explained in dedicated WinAuth section below.

If you don't want to or don't need to go through WinAuth, then simply copy `files/Steamguard-SteamID` file from our protected directory, where `SteamID` is your 64-bit Steam identificator of the account that you want to add (if more than one, because if you have only one account then this will be the only file). You need to place that file in ASF's `config` directory. Once you do that, rename the file to `BotName.maFile`, where `BotName` is the name of your bot you're adding ASF 2FA to. After this step, launch ASF - it should notice the `.maFile` and import it.

    [*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
    <1> Please enter your Device ID (including "android:"):
    

You will need to do only one more step - find your `DeviceID` property in `shared_prefs/steam.uuid.xml`. It will be inside XML tags and starting with `android:`. Copy that (or write it down) and put it in ASF as asked. If you did everything correctly, import should be finished.

    [*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
    

Please confirm that accepting confirmations in fact works. If you made a mistake while entering your `DeviceID` then you'll have half-broken authenticator - tokens will work, but accepting confirmations will not. You can always remove `Bot.db` and start over if needed.

* * *

### iOS

For iOS you can use **[ios-steamguard-extractor](https://github.com/CaitSith2/ios-steamguard-extractor)**. This is possible thanks to the fact that you can make decrypted backup, put in on your PC and use the tool in order to extract Steam data that is otherwise impossible to get (at least without jailbreak, due to iOS encryption).

Head over to **[latest release](https://github.com/CaitSith2/ios-steamguard-extractor/releases/latest)** in order to download the program. Once you extract the data you can put it e.g. in WinAuth, then from WinAuth to ASF (although you can also simply copy generated json starting from `{` ending on `}` into `BotName.maFile` and proceed like usual). If you ask me, I strongly recommend to import to WinAuth first, then making sure that both generating tokens as well as accepting confirmations work properly, so you can be sure that everything is alright. If your credentials are invalid, ASF 2FA will not work properly, so it's much better to make ASF import step your last one.

For questions/issues, please visit **[issues](https://github.com/CaitSith2/ios-steamguard-extractor/issues)**.

*Keep in mind that above tool is unofficial, you're using it at your own risk. We do not offer technical support if it doesn't work properly - we got a few signals that it's exporting invalid 2FA credentials - verify that confirmations work in authenticator like WinAuth prior to importing that data to ASF!*

* * *

### SteamDesktopAuthenticator

If you have your authenticator running in SDA already, you should notice that there is `steamID.maFile` file available in `maFiles` folder. Copy that file to `config` directory of ASF. Make sure that `.maFile` is in unencrypted form, as ASF can't decrypt SDA files - unencrypted file content should start with `{` character.

You should now rename `steamID.maFile` to `BotName.maFile` in ASF config directory, where `BotName` is the name of your bot you're adding ASF 2FA to. Alternatively you can leave it as it is, ASF will then pick it automatically after logging in. Helping ASF makes it possible to use ASF 2FA before logging in, if you won't help ASF, then the file can be picked only after ASF successfully logs in (as ASF doesn't know `steamID` of your account before in fact logging in).

If you did everything correctly, launch ASF, and you should notice:

    [*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
    [*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
    

From now on, your ASF 2FA should be operational for this account.

* * *

### WinAuth

Firstly create new empty `BotName.maFile` in ASF config directory, where `BotName` is the name of your bot you're adding ASF 2FA to. Remember that it should be `BotName.maFile` and NOT `BotName.maFile.txt`, Windows likes to hide known extensions by default. If you provide incorrect name, it won't be picked by ASF.

Now launch WinAuth as usual. Right click on Steam icon and select "Show SteamGuard and Recovery Code". Then check "Allow copy". You should notice familiar to you JSON structure on the bottom of the window, starting with `{`. Copy whole text into a `BotName.maFile` file created by you in previous step.

If you did everything correctly, launch ASF, and you should notice:

    [*] INFO: ImportAuthenticator() <1> Converting .maFile into ASF format...
    <1> Please enter your Device ID (including "android:"):
    

This is when tricky part comes in. WinAuth is missing deviceID property that is required by ASF, so you'll need to do one more thing.

Go back to WinAuth's "Show SteamGuard and Recovery Code" and you should notice "Device ID" property above the JSON code you were copying not that long ago. Copy whole android device ID, including `android:` part into ASF.

If you've done that properly as well, you're now done!

    [*] INFO: ImportAuthenticator() <1> Successfully finished importing mobile authenticator!
    

Please confirm that accepting confirmations in fact works. If you made a mistake while entering your `DeviceID` then you'll have half-broken authenticator - tokens will work, but accepting confirmations will not. You can always remove `Bot.db` and start over if needed.

* * *

## Gereed

From this moment, all `2fa` commands will work as they'd be called on your classic 2FA device. You can use both ASF 2FA and your authenticator of choice (Android, iOS, SDA or WinAuth) to generate tokens and accept confirmations.

If you have authenticator on your phone, you can optionally remove SteamDesktopAuthenticator and/or WinAuth, as we won't need it anymore. However, I suggest to keep it just in case, not to mention that it's more handy than normal steam authenticator. Just keep in mind that ASF 2FA is **NOT** general purpose authenticator and it should **never** be the only one you use, since it doesn't even include all data that authenticator should have. It's not possible to convert ASF 2FA back to original authenticator, therefore always make sure that you have general-purpose authenticator in other place, such as in WinAuth/SDA, or on your phone.

* * *

## FAQ (veelgestelde vragen)

### How is ASF making use of 2FA module?

If ASF 2FA is available, ASF will use it for automatic confirmation of trades that are being sent/accepted by ASF. It will also be capable of automatically generating 2FA tokens on as-needed basis, for example in order to log in. In addition to that, having ASF 2FA also enables `2fa` commands for you to use. That should be all for now, if I didn't forget about anything - basically ASF uses 2FA module on as-needed basis.

* * *

### What if I need a 2FA token?

You will need 2FA token to access 2FA-protected account, that includes every account with ASF 2FA as well. You should generate tokens in authenticator that you used for import, but you can also generate temporary tokens through `2fa` command sent via the chat to given bot. You can also use `2fa <BotNames>` command to generate temporary token for given bot instances. This should be enough for you to access bot accounts through e.g. browser, but as noted above - you should use your friendly authenticator (Android, iOS, SDA or WinAuth) instead.

* * *

### Can I use my original authenticator after importing it as ASF 2FA?

Yes, your original authenticator remains functional and you can use it together with using ASF 2FA. That's the whole point of the process - we're importing your authenticator credentials into ASF, so ASF can make use of them and accept selected confirmations on your behalf.

* * *

### Where is ASF mobile authenticator saved?

ASF mobile authenticator is saved in `BotName.db` file in your config directory, along with some other crucial data related to given account. If you want to remove ASF 2FA, read how below.

* * *

### How to remove ASF 2FA?

Simply stop ASF and remove associated `BotName.db` of the bot with linked ASF 2FA you want to remove. This option will remove associated imported 2FA with ASF, but will NOT delink your authenticator. If you instead want to delink your authenticator, apart from removing it from ASF (firstly), you should delink it in authenticator of your choice (Android, iOS, SDA or WinAuth), or - if you can't for some reason, use revocation code that you received during linking that authenticator, on the Steam website. It's not possible to unlink your authenticator through ASF, this is what general-purpose authenticator that you already have should be used for.

* * *

### I linked authenticator in SDA/WinAuth, then imported to ASF. Can I now unlink it and link it again on my phone?

**No**. ASF **imports** your authenticator data in order to use it. If you delink your authenticator then you'll also cause ASF 2FA to stop functioning, regardless if you remove it firstly like stated in above question or not. If you want to use your authenticator on both your phone and ASF (plus optionally in SDA/WinAuth), then you'll need to **import** your authenticator from your phone, and not create new one in SDA/WinAuth. You can have only **one** linked authenticator, that's why ASF **imports** that authenticator and its data in order to use it as ASF 2FA - it's **the same** authenticator, just existing in two places. If you decide to delink your mobile authenticator credentials - regardless in which way, ASF 2FA will stop working, as previously copied mobile authenticator credentials will no longer be valid. In order to use ASF 2FA together with authenticator on your phone, you must import it from Android/iOS, which is described above.

* * *

### Is using ASF 2FA better than WinAuth/SDA/Other authenticator set to accept all confirmations?

**Yes**, in several ways. First and most important one - using ASF 2FA **significantly** increases your security, as ASF 2FA module ensures that ASF will only accept automatically its own confirmations, so even if attacker does request a trade that is harmful, ASF 2FA will **not** accept such trade, as it was not generated by ASF. In addition to security part, using ASF 2FA also brings performance/optimization benefits, as ASF 2FA fetches and accepts confirmations immediately after they're generated, and only then, as opposed to inefficient polling for confirmations each X minutes done e.g. by SDA or WinAuth. In short, there is no reason to use third-party authenticator over ASF 2FA, if you plan on automating confirmations generated by ASF - that's exactly what ASF 2FA is for, and using it does not conflict with you confirming everything else in authenticator of your choice. We strongly recommend to use ASF 2FA for entire ASF activity - this is much more secure than any other solution.

* * *

## Geavanceerd

If you're advanced user, you can also generate maFile manually. It should have a **[valid JSON structure](https://jsonlint.com)** of:

```json
{
  "shared_secret": "STRING",
  "identity_secret": "STRING",
  "device_id": "STRING"
}
```

`device_id` is optional during import, but mandatory for ASF operation - ASF will ask for it during importing if you omit it. Of course, you need to replace `"STRING"` with valid content in each field.

Standard authenticator data has more fields - they're entirely ignored by ASF during import, as they're not needed. You also don't have to remove them - ASF only requires valid JSON with 2 mandatory fields described above, and optionally also `device_id`.
# 相容性

ASF 是一個在 .NET Core 平台上執行的 C# 應用程式。 這意味著 ASF 並非被編譯為可供 CPU 直接執行的​**[機器碼](https://en.wikipedia.org/wiki/Machine_code)**，而是被編譯為 **[CIL（通用中間語言）](https://en.wikipedia.org/wiki/Common_Intermediate_Language)**碼，一種需要相容的執行階段才能執行的語言。

這種方法能夠帶來巨大的方便。由於 CIL 是跨平台的，這使得 ASF 能夠在許多作業系統上執行，特別是 Windows、Linux 和 OS X 這三個系統。ASF 不但無須透過模擬執行，同時所有對於系統及其相關硬體的最佳化也對其有效。 因此，ASF可以實現卓越的效能和最佳化，同時仍然提供完美的相容性和可靠性。

這也意味著執行 ASF **沒有特定的作業系統需求**，因為它需要的只是執行於作業系統上的**執行階段**而非作業系統本身。 只要執行環境能夠正確地執行 ASF 的程式碼，底層系統是 Windows、Linux、OS X 還是 BSD，硬體是 Sony Playstation 4、Nintendo Wii甚至是您的烤麵包機都無所謂。只要有**[.NET Core ](https://github.com/dotnet/core-setup#daily-builds)**，就能用 **[ASF](https://github.com/JustArchiNET/ArchiSteamFarm/releases/latest)**。

但是，無論您想要在哪個平台上執行 ASF，您必須確保該平台安裝了 **[.NET Core 必要條件](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)**。 這些都是確保執行階段功能正常的底層庫，也是確保 ASF 能夠第一時間工作的絕對核心。 很有可能你已經安裝了其中的一些（甚至全部）。

* * *

## Multiple instances

ASF is compatible with running multiple instances of the process on the same machine. The instances can be completely standalone or derived from the same binary location (in which case, you want to run them with different `--path` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**).

When running multiple instances from the same binary, keep in mind that you should typically disable auto-updates in all of their configs, as there is no synchronization between them in regards to auto-updates. If you'd like to keep having auto-updates enabled, we recommend standalone instances, but you can still make updates work, as long as you can ensure that all other ASF instances are closed.

ASF will do its best to maintain a minimum amount of OS-wide, cross-process communication with other ASF instances. This includes ASF checking its configuration directory against other instances, as well as sharing core process-wide limiters configured with `*LimiterDelay` **[global config properties](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config)**, ensuring that running multiple ASF instances will not cause a possibility to run into a rate-limiting issue. In regards to technical aspects, Windows platforms use native OS-wide named semaphores for this purpose, Unix platforms use fallback mechanism of custom ASF file-based locks created in `/tmp/ASF` directory.

It's not required for running ASF instances to share the same `*LimiterDelay` properties, they can use different values, as each ASF will add its own configured delay to the release time after acquiring the lock. If the configured `*LimiterDelay` is set to `0`, ASF instance will entirely skip waiting for the lock of given resource that is shared with other instances (that could potentially still maintain a shared lock with each other). When set to any other value, ASF will properly synchronize with other ASF instances and wait for its turn, then release the lock after configured delay, allowing other instances to continue.

ASF takes into account `WebProxy` setting when deciding about shared scope, which means that two ASF instances using different `WebProxy` configurations will not share their limiters with each other. This is implemented in order to allow `WebProxy` setups to operate without excessive delays, as expected from different network interfaces. This should be good enough for majority of use cases, however, if you have a specific custom setup in which you're e.g. routing requests yourself in a different way, you can specify network group yourself through `--network-group` **[command-line argument](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)**, which will allow you to declare ASF group that will be synchronized with this instance. Keep in mind that custom network groups are used exclusively, which means that ASF will no longer use `WebProxy` for determining the right group, as you're in charge of grouping in this case.

* * *

## ASF 套件

ASF 主要有兩種封裝方式——通用（Generic）套件和特定作業系統（OS-specific）套件。 從功能上來講，這兩種套件是完全一樣的，都能夠自動進行更新。 唯一的區別就是**通用（Generic）**套件中不包含能使 ASF 直接執行的**特定作業系統**執行階段。

* * *

### 通用（Generic）

通用（Generic）套件與平台無關，所以它不包含任何給特定機器的程式碼。 所以使用這個套件需要您的作業系統中已經安裝**合適版本**的 .NET Core 執行階段。 眾所周知，一直保持相依性是最新版本非常麻烦，所以這個套件主要面向**已經在使用** .NET Core，不想為 ASF 單獨複製已有的執行階段的使用者。 通用（Generic）套件還使您能將 ASF 執行在**任何可以正常執行 .NET Core 執行階段的裝置上**，不需要擔心 ASF 是否為特定作業系統建置了套件。

如果您只是想要執行 ASF 而不想要深入瞭解 . NET Core 的技術細節，我们并不推薦一般甚至是進階使用者使用通用（Generic）套件。 也就是說，如果你瞭解通用套件，那你可以使用它，不然特定作業系統套件才是更合適的。

#### .NET Framework 套件

除了上面提到的通用（Generic）套件，我們也提供 `Generic-netf` 套件，它基於 .NET Framework（而非 .NET Core）。 該包是一種舊式套件，它補全了從 ASF V2 時代即已知的相容性缺失，並且可以使用 **[Mono](https://www.mono-project.com)** 執行，當前來自 .NET Core 的 `Generic`（通用）套件無法用於Mono。

通常，您應該**儘量避免使用此程式套件**，因為大多數作業系統都完全（並且更好地）支援上面提到的 `Generic`（通用）套件。 事實上，這個軟體套件只適用於缺失 .NET Core 執行階段，但能夠執行 Mono 的平台。 這些平台包含 `linux-x86`（32 位元 i386/i686 Linux），以及 `linux-armel`（Raspberry Pi 0 & 1 等開發板所用的 ARMv6），這些平台目前官方沒有支援 .NET Core 執行階段。

隨著時間的推移，.NET Core 會支援更多平台，而 .NET Framework 和 .NET Core 之間會更加不相容，`Generic-netf` 套件將會在未來完全被 `Generic`（通用）套件取代。 如果您可以使用任何 .NET Core 軟體套件，就不要使用 Framework 套件，因為 `Generic-netf` 與 .NET Core 版本相比缺少許多功能和相容性，並且隨著時間的推移，它的功能只會變少。 我們**僅**對無法使用`通用`套件的平台提供此版本的支援（例如 `linux-x86`），並且也僅支援基於最新版本的執行階段（例如最新版 Mono）。

* * *

### 特定作業系統（OS-specific）

除了通用（Generic）套件中包含的託管程式碼之外，特定作業系統套件還包括指定平台的本地程式碼。 換句話說，特定作業系統套件內部**已經包含了可用的 .NET Core 執行階段**，使您可以跳過麻煩的安裝過程，直接啟動 ASF。 特定作業系統套件，顧名思義，是針對不同作業系統的，每種作業系統都需要其特定的版本——例如 Windows 需要 PE32+ `ArchiSteamFarm.exe`二進位檔案，而 Linux 則需要 Unix ELF `ArchiSteamFarm`二進位檔案。 您可能已經知道，這兩種類型之間是完全不相容的。

ASF 目前可用於以下作業系統：

- `win-x64` 支援 64 位 Windows 作業系統。 包括 Windows 7（SP1+）、8.1、10、Server 2008 R2（SP1+）、2012、2012 R2、2016，以及未來的版本。
- `linux-arm`支援 32 位基於 ARM（ARMv7+）的 GNU/Linux 作業系統。 包括所有像是 Raspberry Pi 2（或更新版本）的平台可用的 GNU/Linux 作業系統（例如 Raspbian）的當前和未來版本。 This variant will not work with older ARM architectures, such as ARMv6 found in Raspberry Pi 0 & 1, it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-arm64` works on 64-bit ARM-based (ARMv8+) GNU/Linux OSes. This includes platforms such as Raspberry Pi 3 (and newer) with all AArch64 GNU/Linux OSes available for them (such as Debian), in current and future versions. This variant will not work with 32-bit OSes that do not have required 64-bit libraries available (such as Raspbian), it will also not work with OSes that do not implement required GNU/Linux environment (such as Android).
- `linux-x64` 支援 64 位 GNU/Linux 作業系統。 包括 Alpine、CentOS/Fedora/RHEL、Debian/Ubuntu/Linux Mint、OpenSUSE/SLES 等作業系統以及它們的衍生版的當前和未來版本。
- `osx-x64` 支援64 位 OS X 作業系統。 包括 10.13 及更新版本。

當然，即使沒有適合您作業系統及架構的特定作業系統套件，您也可以手動安裝適當的 .NET Core 執行階段並執行通用（Generic）ASF 套件，這也是這個套件存在的主要原因。 通用（Generic）ASF 包與平台無關，可在任何具有可用 .NET Core 執行階段的平台上執行。 需要注意——ASF 需要的是 .NET Core 執行階段，而不是特定的作業系統或架構。 例如，如果您使用的是 32 位 Windows，但 ASF 沒有 `win-x86` 版本，您仍然可以安裝 `win-x86` 版本的 .NET Core SDK，然後執行通用（Generic）版本的 ASF。 我們無法為所有作業系統和架構組合都產生一份可執行档案，所以我們為此畫下一道分隔線。 x86 就是這條線的範例之一，因為這種架構自 2004 年開始就過時了。

您可以造訪​**[發行說明​](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md)**來查看 .NET Core 3.1 支持的平台與作業系統的完整列表。

* * *

## 執行階段必要條件

如果您正在使用特定作業系統套件，那麼您不必擔心執行環境必要條件，因為 ASF 總會將所需的最新執行環境封裝在一起，只要您已安裝並更新 **[.NET Core 必要條件](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md)**，就能夠正常執行。 換句話說，**您不需要安裝 .NET Core 執行階段或 SDK**，因為特定作業系統版本只需要本機已安裝對應作業系統的必要條件，而不需要其他項目。

但如果您使用 **Generic**（通用）套件，則必須保證已安裝 ASF 所需的對應平台的 .NET Core 執行階段。

ASF 程式目前的目標是 **.NET Core 3.1**（`netcoreapp3.1`），但在未來可能會以更高版本為目標。 即使 ASF 以**編譯時最新版本的執行階段**為建置目標，`netcoreapp3.1` 從 3.1.100 SDK（3.1.0 執行階段）之後就受支援，所以您應該確保您的機器上有**[最新版本的 SDK](https://dotnet.microsoft.com/download)**（或至少有執行階段）。 如果您的執行階段版本低於編譯時已知的最低（目標）版本，通用 ASF 套件將會拒絕啟動。

如有疑問，您可以訪問我們用於編譯並在 GitHub 上部署新版本的 **[CI](https://ci.appveyor.com/project/JustArchi/ArchiSteamFarm)**。 您可以在每個構建的頂端找到 `dotnet --info` 的輸出。

* * *

## 執行問題

### 在 OpenVZ 虛擬化的 Linux VPS 上執行 ASF 遭遇的許多與鎖相關的問題

OpenVZ 核心通常基於非常舊的 Linux 核心版本（2.6），這似乎不相容最新版本的 .NET Core 執行階段。 如果您嘗試執行 ASF 於這種環境下，您可能會遭遇許多與鎖相關的問題，通常表現為行程畫面凍結。 請參閱相關的 CoreCLR Issue：https://github.com/dotnet/coreclr/issues/26873

我們建議是放棄 OpenVZ 架構，使用更好的虛擬化技術，例如 KVM。 這裡提到的問題的確是 .NET Core 執行階段的一個錯誤，並且應當在下個版本 .NET Core 執行階段（3.1 服務修補程式）中被**[修復](https://github.com/dotnet/coreclr/pull/26912)**，但暫無明確的時間。 如果您不能使用更好的虛擬化技術，在執行階段更新之前，您可以考慮使用 `mono` 來執行 ASF 的 `generic-netf` 套件。

對於 Linux 作業系統的進階使用者，有一個**[更好的解決方法](https://github.com/dotnet/coreclr/issues/26873#issuecomment-559854433)**，使其可以執行最新的 .NET Core ASF 而不會發生這個問題。
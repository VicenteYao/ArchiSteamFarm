# Docker

從版本3.0.3.2 開始, ASF 現在也可使用 **[ docker container](https://www.docker.com/what-container)**。 Running ASF in docker container typically has no advantages for casual users, but it could be an excellent way of making use of ASF on servers, ensuring that ASF is being run in sandboxed environment separated from all other apps. 我們的docker repo位于** [此處](https://hub.docker.com/r/justarchi/archisteamfarm) **。

* * *

## 標籤

ASF 支援4種主要類型的**[ 標籤 ](https://hub.docker.com/r/justarchi/archisteamfarm/tags)**：

### `master`

此標籤始終指向從主分支中的最新提交產生的 ASF，其工作原理與**[發佈週期](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle-zh-TW)**中描述的實驗 AppVeyor 產生相同。 通常，您應該避免使用此標記，因為它以開發為目的，專門提供給開發人員和高級用戶，可能有大量漏洞。 這映像檔會隨這每次 commit 至 GitHub 分支時更新，因此可以預期他會常常更新（以及有些東西損壞），就像我們的 AppVeyor 構建一樣。 我們在此標記ASF項目的現時狀態，不一定保證穩定或測試，就像在我們的發布週期中指出的那樣。 此標記不應在任何生產環境中使用。

### `released`

與上面類似的是，此標記始終指向**[最新發佈的ASF版本](https://github.com/JustArchiNET/ArchiSteamFarm/releases)**，包括預發佈版本。 與` master `標記相比，每次按下新的GitHub標記時此圖像都會更新。 Dedicated to advanced/power users that love to live on the edge of what can be considered stable and fresh at the same time. 如果您不想使用 `最新` 標記，我們建議您使用此選項。 請注意，使用此標籤等於使用我們的 **[預發佈版本](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle)**。

### `latest`

This tag in comparison with others, as the only one includes ASF auto-updates feature and will typically point to the one of the stable versions, but not necessarily the latest one. The objective of this tag is to provide a sane default Docker container that is capable of running self-updating, OS-specific build of ASF. Because of that, the image doesn't have to be updated as often as possible, as included ASF version will always be capable of updating itself if needed. Of course, `UpdatePeriod` can be safely turned off (set to `0`), but in this case you should probably use frozen `A.B.C.D` release instead. Likewise, you can modify default `UpdateChannel` in order to make auto-updating `released` tag instead.

Due to the fact that the `latest` image comes with capability of auto-updates, it includes bare OS with OS-specific ASF version, contrary to all other tags that include OS with .NET Core runtime and generic ASF version. This is because newer (updated) ASF version might also require newer runtime than the one the image could possibly be built with, which would otherwise require image to be re-built from scratch, nullifying the planned use-case.

### `A.B.C.D`

In comparison with above tags, this tag is completely frozen, which means that the image won't be updated once published. This works similar to our GitHub releases that are never touched after the initial release, which guarantees you stable and frozen environment. Typically you should use this tag when you want to use some specific ASF release (older than `latest`) and you don't want to use any kind of auto-updates (e.g. those offered in `latest` tag).

* * *

## 哪個標籤最適合我？

這取決於您需要什麼。 For majority of users, `latest` tag should be the best one as it offers exactly what desktop ASF does, just in special Docker container as a service. People that are rebuilding their images quite often and would instead prefer to have ASF version tied to given release are welcome to use `released` tag. If you instead want to use some specific frozen ASF version that will never change without your clear intention, `A.B.C.D` releases are available for you as fixed ASF milestones you can always fall back to.

我們通常不鼓勵嘗試` master `構建，就像自動AppVeyor構建一樣——這個構建在這裏是為了標記ASF項目的現時狀態。 Nothing guarantees that such state will work properly, but of course you're more than welcome to give them a try if you're interested in ASF development.

* * *

## 架構

ASF docker image is currently available for 3 architectures - `x64`, `arm` and `arm64`. You can read more about them in **[compatibility](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility)** section.

Since multi-arch docker tags are still work-in-progress, builds for other architectures than default `x64` are currently available with `-{ARCH}` appended to the tag name. In other words, if you want to use `latest` tag for `arm` architecture, simply use `latest-arm`.

* * *

## 使用方法

For complete reference you should use **[official docker documentation](https://docs.docker.com/engine/reference/commandline/docker)**, we'll cover only basic usage in this guide, you're more than welcome to dig deeper.

### 你好，ASF！

Firstly we should verify if our docker is even working correctly, this will serve as our ASF "hello world":

```shell
docker pull justarchi/archisteamfarm
docker run -it --name asf --rm justarchi/archisteamfarm
```

`docker pull` command ensures that you're using up-to-date `justarchi/archisteamfarm` image, just in case you had outdated local copy in your cache. `docker run` creates a new ASF docker container for you and runs it in the foreground (`-it`). `--rm` ensures that our container will be purged once stopped, since we're just testing if everything works fine for now.

If everything ended successfully, after pulling all layers and starting container, you should notice that ASF properly started and informed us that there are no defined bots, which is good - we verified that ASF in docker works properly. Hit `CTRL+P` then `CTRL+Q` in order to quit foreground docker container, then stop ASF container with `docker stop asf`.

如果您仔細查看該命令，那麼您會注意到我們沒有聲明任何標記，該標記自動預設為` latest `。 如果您想使用` latest `之外的其他標記，例如` latest-arm `，那麼您應該明確聲明它：

```shell
docker pull justarchi/archisteamfarm:latest-arm
docker run -it --name asf --rm justarchi/archisteamfarm:latest-arm
```

* * *

## 使用 Volume

顯而易見，如果您在docker容器中使用ASF，您需要自己配置程序。 You can do it in various different ways, but the recommended one would be to create ASF `config` directory on local machine, then mount it as a shared volume in ASF docker container.

例如，我們假設您的ASF配置資料夾位於 `/home/archi/ASF/config`目錄中。 This directory contains core `ASF.json` as well as bots that we want to run. Now all we need to do is simply attaching that directory as shared volume in our docker container, where ASF expects its config directory (`/app/config`).

```shell
docker pull justarchi/archisteamfarm
docker run -it -v /home/archi/ASF/config:/app/config --name asf justarchi/archisteamfarm
```

就是這樣，現在您的ASF docker容器將在讀寫模式下使用與本地計算機的共享目錄，這是配置ASF所需的一切。 In similar way you can mount other volumes that you'd like to share with ASF, such as `/app/logs` or `/app/plugins`.

Of course, this is just one specific way to achieve what we want, nothing is stopping you from e.g. creating your own `Dockerfile` that will copy your config files into `/app/config` directory inside ASF docker container. 此指南僅涵蓋基本用法。

### Volume 權限

預設情況下，ASF在容器內使用` root `用戶運行。 This is not a problem security-wise, since we're already inside Docker container, but it does affect the shared volume as newly-generated files will be normally owned by `root`, which may not be desired situation when using a shared volume.

Docker allows you to pass `--user` **[flag](https://docs.docker.com/engine/reference/run/#user)** to `docker run` command which will define default user that ASF will run under. You can check your `uid` and `gid` for example with `id` command, then pass it to the rest of the command. 例如，如果目標用戶的` uid `和` gid `為1000：

```shell
docker pull justarchi/archisteamfarm
docker run -it -u 1000:1000 -v /home/archi/ASF/config:/app/config --name asf justarchi/archisteamfarm
```

請記住，預設情況下，ASF使用的` / app `目錄仍歸` root `所有。 如果您在自訂用戶下運行ASF，則ASF進程將無權對其自己的檔案進行寫訪問。 這種存取權限對於操作不是強制性的，但它是至關重要的，例如用於自動更新功能。 為了解決這個問題，只需將所有ASF文件的所有權從默認的` root `更改為新的自訂用戶即可。

```shell
docker exec -u root asf chown -hR 1000:1000 /app
```

This has to be done only once after you created your container with `docker run`, and only if you decided to use custom user for ASF process. Also don't forget to change `1000:1000` argument in both commands above to the `uid` and `gid` you actually want to run ASF under.

* * *

## Multiple instances synchronization

ASF includes support for multiple instances synchronization, as stated in **[compatibility](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility#multiple-instances)** section. When running ASF in docker container, you can optionally "opt-in" into the process, in case you're running multiple containers with ASF and you'd like for them to synchronize with each other.

By default, each ASF running inside a docker container is standalone, which means that no synchronization takes place. In order to enable synchronization between them, you must bind `/tmp/ASF` path in every ASF container that you want to synchronize, to one, shared path on your docker host, in read-write mode. This is achieved exactly the same as binding a volume which was described above, just with different paths:

```shell
mkdir -p /tmp/ASF-g1
docker pull justarchi/archisteamfarm
docker run -v /tmp/ASF-g1:/tmp/ASF -v /home/archi/ASF/config:/app/config --name asf1 justarchi/archisteamfarm
docker run -v /tmp/ASF-g1:/tmp/ASF -v /home/john/ASF/config:/app/config --name asf2 justarchi/archisteamfarm
# And so on, all ASF containers are now synchronized with each other
```

We recommend to bind ASF's `/tmp/ASF` directory also to a temporary `/tmp` directory on your machine, but of course you're free to choose any other one that satisfies your usage. Each ASF container that is expected to be synchronized should have its `/tmp/ASF` directory shared with other containers that are taking part in the same synchronization process.

As you've probably guessed from example above, it's also possible to create two or more "synchronization groups", by binding different docker host paths into ASF's `/tmp/ASF`.

Mounting `/tmp/ASF` is completely optional and actually not recommended, unless you explicitly want to synchronize two or more ASF containers. We do not recommend mounting `/tmp/ASF` for single-container usage, as it brings absolutely no benefits if you expect to run just one ASF container, and it might actually cause issues that could otherwise be avoided.

* * *

## 命令列參數

ASF allows you to pass **[command-line arguments](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments)** in docker container through environment variables. You should use specific environment variables for supported switches, and `ASF_ARGS` for the rest. This can be achieved with `-e` switch added to `docker run`, for example:

```shell
docker pull justarchi/archisteamfarm
docker run -it -e "ASF_CRYPTKEY=MyPassword" -e "ASF_ARGS=--process-required" --name asf justarchi/archisteamfarm
```

This will properly pass your `--cryptkey` argument to ASF process being run inside docker container, as well as other args. Of course, if you're advanced user then you can also modify `ENTRYPOINT` or add `CMD` and pass your custom arguments yourself.

Unless you want to provide custom encryption key or other advanced options, usually you don't need to include any special environment variables, as our docker containers are already configured to run with a sane expected default options of `--no-restart` `--process-required` `--system-required`, so as you can see our `ASF_ARGS` above are redundant in this case, and only `ASF_CRYPTKEY` is relevant.

* * *

## IPC

For using IPC, firstly you should switch `IPC` **[global configuration property](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration#global-config)** to `true`. In addition to that, you **must** modify default listening address of `localhost`, as docker can't route outside traffic to loopback interface. An example of a setting that will listen on all interfaces would be `http://*:1242`. Of course, you can also use more restrictive bindings, such as local LAN or VPN network only, but it has to be a route accessible from the outside - `localhost` won't do, as the route is entirely within guest machine.

For doing the above you should use **[custom IPC config](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC#custom-configuration)** such as the one below:

```json
{
    "Kestrel": {
        "Endpoints": {
            "HTTP": {
                "Url": "http://*:1242"
            }
        }
    }
}
```

Once we set up IPC on non-loopback interface, we need to tell docker to map ASF's `1242/tcp` port either with `-P` or `-p` switch.

例如，此命令（僅）會將ASF IPC接口暴露給主機：

```shell
docker pull justarchi/archisteamfarm
docker run -it -p 127.0.0.1:1242:1242 -p [::1]:1242:1242 --name asf justarchi/archisteamfarm
```

If you set everything properly, `docker run` command above will make **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC)** interface work from your host machine, on standard `localhost:1242` route that is now properly redirected to your guest machine. 值得注意的是，我們不會進一步公開此路由，因此只能在docker主機中進行連接，從而保證其安全。

* * *

### 完整範例

綜上所述，完整設置的範例如下所示：

```shell
docker pull justarchi/archisteamfarm
docker run -it -p 127.0.0.1:1242:1242 -p [::1]:1242:1242 -v /home/archi/asf:/app/config --name asf justarchi/archisteamfarm
```

This assumes that you'll use a single ASF container, with all ASF config files in `/home/archi/asf`. You should modify the config path to the one that matches your machine. This setup is also ready for optional IPC usage if you've decided to include `IPC.config` in your config directory with a content like below:

```json
{
    "Kestrel": {
        "Endpoints": {
            "HTTP": {
                "Url": "http://*:1242"
            }
        }
    }
}
```

* * *

## Pro tips

When you already have your ASF docker container ready, you don't have to use `docker run` every time. You can easily stop/start ASF docker container with `docker stop asf` and `docker start asf`. Keep in mind that if you're not using `latest` tag then updating ASF will still require from you to `docker stop`, `docker rm`, `docker pull` and `docker run` again. This is because you must rebuild your container from fresh ASF docker image every time you want to use ASF version included in that image. In `latest` tag, ASF has included capability to auto-update itself, so rebuilding the image is not necessary for using up-to-date ASF (but it's still a good idea to do it from time to time in order to use fresh .NET Core runtime and underlying OS).

As hinted by above, ASF in tag other than `latest` won't automatically update itself, which means that **you** are in charge of using up-to-date `justarchi/archisteamfarm` repo. This has many advantages as typically the app should not touch its own code when being run, but we also understand convenience that comes from not having to worry about ASF version in your docker container. If you care about good practices and proper docker usage, `released` tag is what we'd suggest instead of `latest`, but if you can't be bothered with it and you just want to make ASF both work and auto-update itself, then `latest` will do.

You should typically run ASF in docker container with `Headless: true` global setting. This will clearly tell ASF that you're not here to provide missing details and it should not ask for those. Of course, for initial setup you should consider leaving that option at `false` so you can easily set up things, but in long-run you're typically not attached to ASF console, therefore it'd make sense to inform ASF about that and use `input` **[command](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands)** if need arises. This way ASF won't have to wait infinitely for user input that will not happen (and waste resources while doing so). It will also allow ASF to run in non-interactive mode inside container, which is crucial e.g. in regards to forwarding signals, making it possible for ASF to gracefully close on `docker stop asf` request.
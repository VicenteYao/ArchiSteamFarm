# Docker

从 3.0.3.2 版本开始，ASF 也可以在 **[Docker 容器](https://www.docker.com/what-container)**&#8203;中运行。 对于普通用户来说，在 Docker 容器中运行 ASF 并没有什么特别的好处。但对于服务器用户来说，这可能是运行 ASF 的最佳方式，因为这样可以确保 ASF 运行在与其他应用分离的沙盒中。 我们的 Docker 仓库可以在&#8203;**[这里](https://hub.docker.com/r/justarchi/archisteamfarm)**&#8203;找到。

* * *

## 分支

ASF 有 4 种主要的&#8203;**[分支](https://hub.docker.com/r/justarchi/archisteamfarm/tags)**。

### `master`

这个分支始终指向 GitHub 中 master 分支的最新提交构建的 ASF，其工作原理与&#8203;**[发布周期](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle-zh-CN)**&#8203;中描述的实验性 AppVeyor 构建相同。 一般而言，您应该避免使用该分支，因为它是用于开发目的，为开发人员和高级用户准备的，有最高的漏洞风险。 该映像会在每次提交到 GitHub master 分支后更新，因此您会发现它的更新十分频繁（并且经常出错），就像 AppVeyor 构建一样。 该分支记录了 ASF 项目的当前状态，但该状态不一定稳定或者经过测试，就像我们在发布周期中描述的那样。 这个分支不应该在任何的生产环境中使用。

### `released`

与上述分支类似，这个分支始终指向最新&#8203;**[发布](https://github.com/JustArchiNET/ArchiSteamFarm/releases)**&#8203;的 ASF 版本，包括预览版本。 与 `master` 分支不同，该映像会在推送新的 GitHub 版本标签时更新。 一些高级用户喜欢立刻尝试最新的功能，选择处于稳定边缘的版本，这一分支就是为他们准备的。 如果您不想使用 `latest` 分支的话，我们推荐您使用这个分支。 请注意，使用此分支等同于使用我们的&#8203;**[预览版](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Release-cycle-zh-CN)**。

### `latest`

与其他分支相比，仅有此分支包括 ASF 的自动更新功能，通常会指向一个稳定版本，但不一定是最新版本。 此分支的目标是提供一个默认的 Docker 容器，支持 ASF 操作系统包的自动更新功能。 因此，此映像不需要频繁更新，因为其中的 ASF 会在需要时自动更新。 当然，`UpdatePeriod` 可以被安全禁用（设置为 `0`），但在这种情况下，您可能更应该选择冻结版本的 `A.B.C.D` 分支。 同样，您可以修改默认的 `UpdateChannel`，使其像 `released` 分支一样更新。

由于 `latest` 映像具有自动更新的能力，它包含了基本的操作系统和对应版本的 ASF，而其他所有分支包含的是操作系统、.NET Core 运行时环境以及 Generic 版本的 ASF。 这是因为新版本的 ASF 可能需要比映像内置版本更新的运行时环境，使映像必须彻底重新构建，这与此分支的预期使用场景不符。

### `A.B.C.D`

与上面的分支相比，这个分支是完全冻结的，这意味着映像一旦发布就不会再更新。 这类似于我们的 GitHub 发布版本，一经发布就不会更改，这保证了环境的长期稳定。 通常，如果您希望使用特定的（早于 `latest` 的）ASF 版本，并且不希望启用任何自动更新功能（例如 `latest` 所提供的），就应该使用这种分支。

* * *

## 哪个分支最适合我？

这取决于您的目标。 对于大多数用户来说，`latest` 分支是最好的，因为它的行为与在桌面上运行 ASF 是相同的，区别仅仅在于它以服务形式运行在 Docker 容器内。 经常重建映像以及喜欢抢先尝试 ASF 最新功能的人可能会更喜欢 `released` 分支。 如果您希望使用某个特定版本的 ASF，可以选择 `A.B.C.D` 分支，如果没有您主动操作，这个分支就不会有任何变化，您可以将其视为一个固定的里程碑，随时可以返回到与之前完全相同的状态。

我们通常不建议使用 `master` 构建，就像 AppVeyor 构建一样——这个构建仅仅是用来标记 ASF 项目当前状态的。 我们无法保证这种状态能够正常工作，但如果您对 ASF 的开发感兴趣，可以尝试一下。

* * *

## 架构

ASF Docker 映像目前支持 3 种架构——`x64,`、`arm` 和 `arm64`。 您可以阅读&#8203;**[兼容性](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-zh-CN)**&#8203;章节了解更多。

由于 Docker 多架构分支仍然未完成，非 `x64` 架构的构建需要在分支名称后面加上架构名称 `-{ARCH}`。 也就是说，如果您需要使用 `arm` 架构的 `latest` 分支，则实际的分支名应为 `latest-arm`。

* * *

## 用法

请阅读 **[Docker 官方文档](https://docs.docker.com/engine/reference/commandline/docker)**&#8203;获得完整的说明，我们仅会在本指南中介绍基本用法，您可能还需要更深入的挖掘。

### 你好，ASF！

首先，我们应该验证 Docker 是否工作正常，这将是我们 ASF 的“Hello World”：

```shell
docker pull justarchi/archisteamfarm
docker run -it --name asf --rm justarchi/archisteamfarm
```

`docker pull` 命令确保您使用的 `justarchi/archisteamfarm` 映像是最新的，防止您本地有旧版映像的副本。 `docker run` 会为您创建一个新的 ASF Docker 容器，并在前台运行它（`-it` 参数）。 `--rm` 参数用于确保我们的容器在停止之后会被完整删除，因为我们现在只是想测试一下是否一切正常，不需要保留。

如果一切正常，在拉取所有层并启动容器后，您应该注意到 ASF 已正确启动并通知我们目前没有任何机器人，这是正常的——我们已经验证了 Docker 中的 ASF 运行正常。 按下 `CTRL+P` 和 `CTRL+Q` 以退出前台 Docker 容器，然后执行 `docker stop asf` 命令停止该容器。

如果您仔细观察这些命令就会发现我们没有指定任何分支，实际上使用的是默认的 `latest` 分支。 如果您希望使用非 `latest` 分支，例如 `latest-arm`，就应该显式地指明：

```shell
docker pull justarchi/archisteamfarm:latest-arm
docker run -it --name asf --rm justarchi/archisteamfarm:latest-arm
```

* * *

## 使用数据卷

如果您在 Docker 容器中使用 ASF，那么显然您需要配置程序本身。 您可以通过不同的方式做到这一点，但建议的方式是在本机创建 ASF `config` 目录，然后将其挂载为 ASF Docker 容器的共享数据卷。

例如，我们假设您的 ASF 配置文件夹位于 `/home/archi/ASF/config` 目录。 这个目录包含了核心的 `ASF.json` 以及机器人配置。 现在，我们只需要将这个目录作为共享数据卷附加到 Docker 容器内 ASF 的配置文件夹（`/app/config`）。

```shell
docker pull justarchi/archisteamfarm
docker run -it -v /home/archi/ASF/config:/app/config --name asf justarchi/archisteamfarm
```

就这样，现在 ASF Docker 容器将会以读写模式使用您本地的共享目录，您可以在其中对 ASF 进行一切配置。 您可以用同样方式挂载 ASF 的其他目录，例如 `/app/logs` 或 `/app/plugins`。

当然，这只是其中一种方法，如果您打算创建自己的 `Dockerfile` 将配置文件复制到 ASF 容器内的 `/app/config` 目录，我们也无法阻止您。 我们只会在本指南中介绍基本用法。

### 卷权限

默认情况下，ASF 会以容器内默认的 `root` 用户运行。 这并非有安全方面的问题，因为我们已经在 Docker 容器内了，但这会影响共享数据卷，使其中新文件的所有者为 `root`，这可能不是您使用数据卷时所期望的情况。

Docker 允许您向 `docker run` 命令传递 `--user` **[参数](https://docs.docker.com/engine/reference/run/#user)**，定义运行 ASF 的默认用户。 您可以通过 `id` 命令等查询您的 `uid` 和 `gid`，然后将其放到命令参数中传递。 例如，假设您的目标用户的 `uid` 和 `gid` 都为 1000：

```shell
docker pull justarchi/archisteamfarm
docker run -it -u 1000:1000 -v /home/archi/ASF/config:/app/config --name asf justarchi/archisteamfarm
```

请记住，在默认情况下，ASF 使用的 `/app` 目录仍然为 `root` 用户所有。 如果您在自定义用户下运行 ASF，则 ASF 进程将没有权限向自己的文件写入内容。 该权限不是必需的，但对于某些功能来说很重要，例如自动更新功能。 为了解决这个问题，只需要将所有 ASF 文件的所有者从默认的 `root` 更改为您设定的新用户。

```shell
docker exec -u root asf chown -hR 1000:1000 /app
```

在通过 `docker run` 命令创建 Docker 容器之后，您只需要执行一次该命令，并且只有您需要自定义执行 ASF 进程的用户时才需要。 也不要忘记将上述命令中的 `1000:1000` 更改为实际用户的 `uid` 和 `gid`。

* * *

## 多实例同步

ASF 支持多实例同步，如&#8203;**[兼容性](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Compatibility-zh-CN#多实例)**&#8203;章节所述。 在 Docker 容器内运行 ASF 时，如果您需要多个 ASF 容器互相同步，可以手动选择启用此功能。

默认情况下，每个运行在 Docker 容器内的 ASF 都是独立的，这意味着不会有任何同步。 要启用它们之间的同步，您必须将每个需要同步的 ASF 容器内的 `/tmp/ASF` 路径以读写模式绑定到宿主机上的共享目录。 实现方式与上文所述的绑定数据卷完全相同，只有路径有区别：

```shell
mkdir -p /tmp/ASF-g1
docker pull justarchi/archisteamfarm
docker run -v /tmp/ASF-g1:/tmp/ASF -v /home/archi/ASF/config:/app/config --name asf1 justarchi/archisteamfarm
docker run -v /tmp/ASF-g1:/tmp/ASF -v /home/john/ASF/config:/app/config --name asf2 justarchi/archisteamfarm
# 以此类推，所有 ASF 容器都会互相同步
```

我们建议将 ASF 的 `/tmp/ASF` 目录也绑定到宿主机上的 `/tmp` 临时目录之下，但您当然也可以根据需要绑定到其他任何位置。 此时，预期要互相同步的每个 ASF 容器的 `/tmp/ASF` 目录都应该已经与其他需要同步的容器共享。

可能您已经从上文猜到，您也可以创建两个或多个这样的“同步组”，只需要将 ASF 容器的 `/tmp/ASF` 目录绑定到宿主机上的不同位置。

挂载 `/tmp/ASF` 是完全可选的，并且也不推荐，除非您明确需要同步两个或多个 ASF 容器。 我们不推荐为单个容器挂载 `/tmp/ASF`，因为如果只运行一个 ASF 容器，这个操作是完全无用的，反而可能会带来本可以避免的其它问题。

* * *

## 命令行参数

ASF 允许您通过设定环境变量，来向 Docker 容器内传递&#8203;**[命令行参数](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Command-line-arguments-zh-CN)**。 对于部分受支持的参数，您应该使用特定的环境变量，而 `ASF_ARGS` 适用于其他参数。 您可以向 `docker run` 命令添加 `-e` 参数，例如：

```shell
docker pull justarchi/archisteamfarm
docker run -it -e "ASF_CRYPTKEY=MyPassword" -e "ASF_ARGS=--process-required" --name asf justarchi/archisteamfarm
```

这会把您的 `--cryptkey` 以及其他参数正确地传递给 Docker 容器内部的 ASF 进程。 当然，如果您是一名高级用户，也可以修改 `ENTRYPOINT`，或者添加 `CMD`，以手动传递自定义参数。

除非您需要提供自定义加密密钥或者使用其他高级选项，否则通常您不需要指定任何环境变量，因为我们的 Docker 容器已被配置为使用合适的默认选项 `--no-restart` `--process-required` `--system-required`，所以您可能会发现，在上述示例中，`ASF_ARGS` 是多余的，只有 `ASF_CRYPTKEY` 发挥了作用。

* * *

## IPC

若要使用 IPC，首先您需要将 `IPC` **[全局配置属性](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Configuration-zh-CN#全局配置)**&#8203;设置为 `true`。 此外，您**必须**修改默认的监听地址 `localhost`，因为 Docker 无法将外部流量路由至环回接口。 一个合适的例子是将其设置为 `http://*:1242`，监听所有网络接口。 当然，您也可以使用更严格的绑定，例如仅限本地局域网或者 VPN 网络，但它必须可被外部访问——`localhost` 就不能，因为此路由完全在客户机内部。

要做到这一点，您需要使用以下形式的&#8203;**[自定义 IPC 配置](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-zh-CN#自定义配置)**：

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

在非环回接口上启用 IPC 之后，我们需要使用 `-P` 或 `-p` 参数告诉 Docker 映射 ASF 的 `1242/tcp` 端口。

例如，此命令将会把 ASF IPC 接口暴露给（仅限）宿主机：

```shell
docker pull justarchi/archisteamfarm
docker run -it -p 127.0.0.1:1242:1242 -p [::1]:1242:1242 --name asf justarchi/archisteamfarm
```

如果一切设置正确，上面的 `docker run` 命令将会使 **[IPC](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/IPC-zh-CN)** 接口在宿主机上正常工作，标准的 `localhost:1242` 路由已经被正确重定向到客户机上。 值得注意的是，我们没有进一步暴露此路由，因此只有 Docker 宿主机能够成功连接，确保了接口的安全性。

* * *

### 完整示例

结合上述的全部内容，完整安装的一个示例如下所示：

```shell
docker pull justarchi/archisteamfarm
docker run -it -p 127.0.0.1:1242:1242 -p [::1]:1242:1242 -v /home/archi/asf:/app/config --name asf justarchi/archisteamfarm
```

此示例假定您将使用单个 ASF 容器，所有配置文件都放在 `/home/archi/asf`。 您需要修改此处的配置文件路径以匹配您的环境。 如果您打算编写内容如下的 `IPC.config` 配置文件，则此 ASF 也能够正常启用 IPC 接口：

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

## 高级技巧

在安装好 ASF Docker 容器之后，您不再需要每次使用 `docker run` 命令。 您可以通过 `docker stop asf` 和 `docker start asf` 命令方便地停止/启动 ASF 容器。 请记住，如果您使用的不是 `latest` 分支，则您仍然需要执行 `docker stop`、`docker rm`、`docker pull` 和 `docker run` 这一系列命令来更新 ASF。 这是因为每次要使用映像内包含的版本时，您必须从新的 ASF Docker 映像重建容器。 在 `latest` 分支中，ASF 已经能够自动更新自己，所以您不需要重建映像就可以保证 ASF 为最新（但为了使用最新的 .NET Core 运行时环境和底层操作系统，有时仍然需要重建映像）。

正如上文所述，非 `latest` 分支中的 ASF 不会自动更新，这意味着**您**必须为使用最新 `justarchi/archisteamfarm` 仓库负责。 这种方式有很多优势，因为通常应用程序不应该在运行时修改自己的代码，但我们也理解无需关心容器内 ASF 版本的便利。 如果您关心最佳实践并且希望正确使用 Docker，我们更建议使用 `released` 而非 `latest` 分支，但如果您不在意这些，只想让 ASF 正常工作并且自动更新，则 `latest` 分支足矣。

通常，您应该在全局配置文件中设置 Docker 容器内的 ASF 运行于 `Headless: true` 模式。 这会明确告诉 ASF，您无法直接提供它所需的数据，它不应该在命令行中直接询问这些。 当然，对于初次设置来说，您应该保持这个选项为 `false` 以便于设置，但长期来看，您很少需要连接到 ASF 控制台上，因此使 ASF 这样做是很合理的。当需要向 ASF 输入内容时，使用 `input` **[命令](https://github.com/JustArchiNET/ArchiSteamFarm/wiki/Commands-zh-CN)**。 这样，ASF 就不必无限等待不存在的用户输入（也无需因此而浪费资源）。 这也使 ASF 在容器内以非交互模式运行，这一点很关键，例如这将会转发信号，使 ASF 可以在收到 `docker stop asf` 请求时安全退出。
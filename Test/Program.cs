using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using ASFService;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, true);
            ASFClientManager clientManager = (ASFClientManager)Activator.GetObject
                    (typeof(ASFClientManager) // 第一个参数是远程对象类型。
                    , "tcp://localhost:55090/ASFClientManager");

            clientManager.UpdateBotConfig(new BotConfig()
            {
                Id = Guid.NewGuid(),
                SteamLogin = "qakra",
                SteamPassword = "washineyao99",
                SteamParentalCode = null,
            });
            clientManager.Login("qakra");
            Console.ReadLine();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using ASFService;
using Microsoft.SqlServer.Server;
using System.Threading;

namespace TestApp {
	[Serializable]
	public class Handler : IASFLoginHandler {

		public  string Invoke(string steamLogin, string requestType) {
			//需要邮箱验证码
			//需要二次验证码


			//线程阻塞
			//等到网页用户POST对应的验证码以后
			//允许线程执行。
			//
			return "OK";
		}
	}

	class Program
    {


		static void Main(string[] args)
        {
			TcpChannel channel = new TcpChannel();
			ChannelServices.RegisterChannel(channel, true);
			ASFClientManager clientManager = (ASFClientManager) Activator.GetObject
					(typeof(ASFClientManager) // 第一个参数是远程对象类型。
					, "tcp://localhost:55090/ASFClientManager");

			clientManager.UpdateBotConfig(new BotConfig() {
				SteamLogin = "qakra",
				SteamPassword = "Washineyao99",
				SteamParentalCode = null,
			});
			clientManager.Login("qakra", new Handler());
			Console.ReadLine();


		}

		private static string OnTest(string streamLogin, string requestCode) {
			Console.WriteLine(requestCode);
			return string.Empty;
		}
	}
}

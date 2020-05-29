using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace ASFService
{
    public class ASFClientManager : MarshalByRefObject
    {
        internal ASFClientManager()
        {
            this.sessions = new Dictionary<string, BotSession>();
        }

       

        private Dictionary<string, BotSession> sessions;

        public BotStatus QueryBotStatus(string streamId)
        {
            return BotStatus.Unknown;
        }

        public void UpdateBotConfig(BotConfig botConfig)
        {
            string accountDir, configFilePath;
            EnsureDirectories(botConfig.SteamLogin, out accountDir, out configFilePath);
            string configString = JsonConvert.SerializeObject(botConfig);
            File.WriteAllText(configFilePath, configString);
        }

        private const string UsersDirectory = "Users";

		public void Login(string streamLogin, IASFLoginHandler loginHandler) {
			BotSession botSession = new BotSession();
			string accountDir, configFilePath;
			EnsureDirectories(streamLogin, out accountDir, out configFilePath);
			if (!File.Exists(configFilePath)) {
				return;
			}

			string configString = File.ReadAllText(configFilePath);
			BotConfig botConfig =  JsonConvert.DeserializeObject<BotConfig>(configString);
			botConfig.Enabled = true;
			configString = JsonConvert.SerializeObject(botConfig);
			File.WriteAllText(configFilePath, configString);
			if (!this.sessions.ContainsKey(streamLogin)) {
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = "ArchiSteamFarm.exe";
				processStartInfo.Arguments = $"--path=\"{accountDir}\"";
				processStartInfo.UseShellExecute = true;
				using (NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream(
					botConfig.SteamLogin,
					PipeDirection.InOut,
					1,
					PipeTransmissionMode.Message)) {

					Process process = Process.Start(processStartInfo);
					botSession.ConfigFilePath = configFilePath;
					botSession.Process = process;
					this.sessions[streamLogin] = botSession;

					namedPipeServerStream.WaitForConnection();
					byte[] bytes = new byte[1024];
					char[] chars = new char[1024];
					Decoder decoder = Encoding.UTF8.GetDecoder();
					Encoder encoder = Encoding.UTF8.GetEncoder();
					StringBuilder stringBuilder = new StringBuilder();
					Retry:
					while (!process.HasExited) {
						stringBuilder.Clear();
						do {

							int numBytes = namedPipeServerStream.Read(bytes, 0, bytes.Length);
							if (numBytes==0) {
								goto Retry;
							}
							int numChars = decoder.GetChars(bytes, 0, numBytes, chars, 0);
							stringBuilder.Append(chars, 0, numChars);
						} while (!namedPipeServerStream.IsMessageComplete);

						string text = stringBuilder.ToString();
						string result = loginHandler.Invoke(streamLogin, text);
						chars = result.ToCharArray();
						int length = encoder.GetBytes(chars, 0, chars.Length, bytes, 0, false);
						namedPipeServerStream.Write(bytes, 0, length);
						namedPipeServerStream.WaitForPipeDrain();
					}
				}
			}
			return;
		}

        private static void EnsureDirectories(string streamLogin, out string accountDir, out string configFilePath)
        {
            string workDir = Path.Combine(Environment.CurrentDirectory, UsersDirectory);
            accountDir = Path.Combine(Environment.CurrentDirectory, UsersDirectory, streamLogin);
            string configDir = Path.Combine(Environment.CurrentDirectory, UsersDirectory, streamLogin, "config");
            configFilePath = Path.Combine(configDir, $"{streamLogin}.json");
            if (!Directory.Exists(workDir))
            {
                Directory.CreateDirectory(workDir);
            }
            if (!Directory.Exists(accountDir))
            {
                Directory.CreateDirectory(accountDir);
            }
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }
        }

        public void Logoff(string streamLogin)
        {
            string accountDir, configFilePath;
            EnsureDirectories(streamLogin, out accountDir, out configFilePath);
            if (!File.Exists(configFilePath))
            {
                return;
            }

            string configString = File.ReadAllText(configFilePath);
            BotConfig botConfig = (BotConfig)JsonConvert.DeserializeObject(configString);
            botConfig.Enabled = true;
            configString = JsonConvert.SerializeObject(botConfig);
            File.WriteAllText(configFilePath, configString);
        }

    }
}

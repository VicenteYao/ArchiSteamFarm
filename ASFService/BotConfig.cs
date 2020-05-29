using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASFService
{
    [Serializable]
    public class BotConfig
    {
        public string SteamLogin { get; set; }

        public string SteamPassword { get; set; }

        public string SteamParentalCode { get; set; }

        public bool Enabled { get; set; }
    }
}

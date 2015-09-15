using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class BanPlayer : Command, ICommand
    {
        private const string Command = "BanPlayer";

        public string SteamId { get; set; }

        public BanPlayer()
            :base(CommandType.BanPlayer)
        { }

        public BanPlayer(string steamId)
            :this()
        {
            SteamId = steamId;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"{SteamId} Banned";
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class UnbanPlayer : Command, ICommand
    {
        private const string Command = "UnbanPlayer";

        public string SteamId { get; set; }

        public UnbanPlayer()
            : base(CommandType.UnbanPlayer)
        { }

        public UnbanPlayer(string steamId)
            : this()
        {
            SteamId = steamId;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"{SteamId} Unbanned";
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}

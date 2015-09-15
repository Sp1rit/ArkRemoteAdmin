using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    // TODO: Doesn't work yet!
    class KillPlayer : Command
    {
        private const string Command = "KillPlayer";

        public string SteamId { get; set; }

        public KillPlayer()
            :base(CommandType.KillPlayer)
        { }

        public KillPlayer(string steamId)
            : this()
        {
            SteamId = steamId;
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}

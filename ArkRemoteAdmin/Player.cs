using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin
{
    public class Player
    {
        public string Name { get; set; }
        public string SteamId { get; set; }
        public bool Banned { get; set; }

        public async Task<bool> Kick()
        {
            string response = await Rcon.ExecuteCommand(string.Format("kickplayer {0}", SteamId));
            return response.Trim() == string.Format("{0} Kicked", SteamId);
        }

        public async Task<bool> Ban()
        {
            string response = await Rcon.ExecuteCommand(string.Format("banplayer {0}", SteamId));
            bool retVal = response.Trim() == string.Format("{0} Banned", SteamId);

            if (retVal)
            {
                this.Banned = true;
                Rcon.Server.BannedPlayers.Add(this);
            }

            return retVal;
        }

        public async Task<bool> Unban()
        {
            string response = await Rcon.ExecuteCommand(string.Format("unbanplayer {0}", SteamId));
            bool retVal = response.Trim() == string.Format("{0} Unbanned", SteamId);

            if (retVal)
            {
                this.Banned = false;
                if (Rcon.Server.BannedPlayers.Exists(p => p.SteamId == SteamId))
                    Rcon.Server.BannedPlayers.RemoveAll(p => p.SteamId == SteamId);
            }

            return retVal;
        }

        public async Task<bool> AddToWhitelist()
        {
            string response = await Rcon.ExecuteCommand(string.Format("allowplayertojoinnocheck {0}", SteamId));
            return response.Trim() == string.Format("{0} Allow Player To Join No Check", SteamId);
        }

        public async Task<bool> RemoveFromWhitelist()
        {
            string response = await Rcon.ExecuteCommand(string.Format("DisallowPlayerToJoinNoCheck {0}", SteamId));
            return response.Trim() == string.Format("{0} Disallowed Player To Join No Checknned", SteamId);
        }
    }
}

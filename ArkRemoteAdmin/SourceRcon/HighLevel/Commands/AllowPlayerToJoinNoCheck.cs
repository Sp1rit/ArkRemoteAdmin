using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class AllowPlayerToJoinNoCheck : Command, ICommand
    {
        private const string Command = "AllowPlayerToJoinNoCheck";

        public string SteamId { get; set; }

        public AllowPlayerToJoinNoCheck()
            : base(CommandType.AllowPlayerToJoinNoCheck)
        { }

        public AllowPlayerToJoinNoCheck(string steamId)
            :this()
        {
            SteamId = steamId;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"{SteamId} Allow Player To Join No Check";
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}

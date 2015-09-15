using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    // TODO: Doesn't work yet!
    class GiveExpToPlayer : Command
    {
        private const string Command = "GiveExpToPlayer";

        public string SteamId { get; set; }

        public double Amount { get; set; }

        public bool TakeFromTribeShare { get; set; }

        public bool PreventSharingWithTribe { get; set; }

        public GiveExpToPlayer()
            : base(CommandType.GiveExpToPlayer)
        { }

        public GiveExpToPlayer(string steamId, double amount, bool takeFromTibeShare, bool preventSharingWithTribe)
            : this()
        {
            SteamId = steamId;
            Amount = amount;
            TakeFromTribeShare = takeFromTibeShare;
            PreventSharingWithTribe = preventSharingWithTribe;
        }

        public override string ToString()
        {
            return $"{Command} {SteamId} {Amount.ToString(CultureInfo.InvariantCulture)} {Convert.ToInt32(TakeFromTribeShare)} {Convert.ToInt32(PreventSharingWithTribe)}";
        }
    }
}

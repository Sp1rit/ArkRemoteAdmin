using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class ListPlayers : Command, ICommand
    {
        private static readonly Regex PlayerMatch = new Regex(@"[0-9]\. (?<Name>.*), (?<SteamId>[0-9]{17})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private const string Command = "ListPlayers";

        public ListPlayers()
            :base(CommandType.ListPlayers)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "No Players Connected" || responseBody.Trim().Length > 5;
        }

        public override string ToString()
        {
            return $"{Command}";
        }

        public static List<Player> ParsePlayers(string response)
        {
            List<Player> players =
                (from Match match in PlayerMatch.Matches(response)
                 select new Player()
                 {
                     Name = match.Groups["Name"].Value,
                     SteamId = match.Groups["SteamId"].Value
                 }).ToList();

            return players;
        }
    }
}

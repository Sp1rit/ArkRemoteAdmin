using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rcon;
using Rcon.Commands;
using ArkRcon = ArkRemoteAdmin.Core.ArkRcon;

namespace ArkRemoteAdmin
{
    class Player
    {
        public string Name { get; set; }
        public string SteamId { get; set; }
        public bool Banned { get; set; }

        public void SendMessage(string message, EventHandler<CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new ServerChatTo(SteamId, $"Private Message - {message.ToRcon()}"), callback);
        }

        public void Kick(EventHandler<CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new KickPlayer(SteamId), callback);
        }

        public void Ban(EventHandler<CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new BanPlayer(SteamId), (s, e) =>
            {
                if (e.Successful)
                {
                    Banned = true;
                    ArkRcon.ConnectedServer.BannedPlayers.Add(this);
                }

                callback?.Invoke(s, e);
            });
        }

        public void Unban(EventHandler<CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new UnbanPlayer(SteamId), (s, e) =>
            {
                if (e.Successful)
                {
                    Banned = false;
                    ArkRcon.ConnectedServer.BannedPlayers.RemoveAll(p => p.SteamId == SteamId);
                }
            });
        }

        public void AddToWhitelist(EventHandler<CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new AllowPlayerToJoinNoCheck(SteamId), callback);
        }

        public void RemoveFromWhitelist(EventHandler<CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new DisallowPlayerToJoinNoCheck(SteamId), callback);
        }

        private static readonly Regex PlayerMatch = new Regex(@"[0-9]\. (?<Name>.*), (?<SteamId>[0-9]{17})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
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

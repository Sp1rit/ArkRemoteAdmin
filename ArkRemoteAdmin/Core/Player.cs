using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkRemoteAdmin.SourceRcon.HighLevel.Commands;
using ArkRcon = ArkRemoteAdmin.SourceRcon.HighLevel.ArkRcon;

namespace ArkRemoteAdmin
{
    class Player
    {
        public string Name { get; set; }
        public string SteamId { get; set; }
        public bool Banned { get; set; }

        public void SendMessage(string message, EventHandler<SourceRcon.HighLevel.CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new ServerChatTo(SteamId, $"Private Message - {message.ToRcon()}"), callback);
        }

        public void Kick(EventHandler<SourceRcon.HighLevel.CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new KickPlayer(SteamId), callback);
        }

        public void Ban(EventHandler<SourceRcon.HighLevel.CommandExecutedEventArgs> callback)
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

        public void Unban(EventHandler<SourceRcon.HighLevel.CommandExecutedEventArgs> callback)
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

        public void AddToWhitelist(EventHandler<SourceRcon.HighLevel.CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new AllowPlayerToJoinNoCheck(SteamId), callback);
        }

        public void RemoveFromWhitelist(EventHandler<SourceRcon.HighLevel.CommandExecutedEventArgs> callback)
        {
            ArkRcon.Client.ExecuteCommandAsync(new DisallowPlayerToJoinNoCheck(SteamId), callback);
        }
    }
}

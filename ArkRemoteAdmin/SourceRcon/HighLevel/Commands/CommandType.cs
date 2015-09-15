using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel
{
    enum CommandType
    {
        Raw,
        GetChat,
        AllowPlayerToJoinNoCheck,
        BanPlayer,
        Broadcast,
        DestroyAllEnemies,
        DisableSpectator,
        EnableSpectator,
        DisallowPlayerToJoinNoCheck,
        DoExit,
        ForcePlayerToJoinTribe,
        GiveExpToPlayer,
        KickPlayer,
        KillPlayer,
        ListPlayers,
        SaveWorld,
        ServerChat,
        ServerChatTo,
        ServerChatToPlayer,
        SetMessageOfTheDay,
        SetTimeOfDay,
        ShowMessageOfTheDay,
        UnbanPlayer
    }
}

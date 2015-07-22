using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon
{
    enum PacketType
    {
        ServerdataAuth = 3,
        ServerdataAuthResponse = 2,
        ServerdataExeccommand = 2,
        ServerdataResponseValue = 0
    }
}

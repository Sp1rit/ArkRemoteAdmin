using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    interface ICommand
    {
        CommandType Type { get; }

        bool ValidateResponse(string responseBody);

        string ToString();
    }
}

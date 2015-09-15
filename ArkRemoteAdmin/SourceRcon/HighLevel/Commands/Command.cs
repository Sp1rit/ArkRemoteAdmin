using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel
{
    class Command
    {
        public CommandType Type { get; set; }

        public Command(CommandType type)
        {
            Type = type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel
{
    class CommandExecutedEventArgs
    {
        public bool Successful { get; set; }

        public string Error { get; set; }

        public string Response { get; set; }

        public Command Command { get; set; }
    }
}

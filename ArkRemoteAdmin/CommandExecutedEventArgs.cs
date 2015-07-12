using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin
{
    class CommandExecutedEventArgs
    {
        public string Command { get; set; }
        public string Response { get; set; }

        public CommandExecutedEventArgs(string command, string response)
        {
            this.Command = command;
            this.Response = response;
        }
    }
}

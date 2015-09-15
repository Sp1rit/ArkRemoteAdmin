using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class Raw : Command, ICommand
    {
        public string Command { get; set; }

        public Raw()
            : base(CommandType.Raw)
        { }

        public Raw(string command)
            : this()
        {
            Command = command.ToRcon();
        }

        public bool ValidateResponse(string responseBody)
        {
            return true;
        }
        public override string ToString()
        {
            return Command;
        }
    }
}

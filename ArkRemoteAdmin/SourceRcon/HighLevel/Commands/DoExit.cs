using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class DoExit : Command, ICommand
    {
        private const string Command = "DoExit";

        public DoExit()
            :base(CommandType.DoExit)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Exiting...";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}

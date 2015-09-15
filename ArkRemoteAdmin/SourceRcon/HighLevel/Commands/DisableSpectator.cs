using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    // TODO: Doesn't work yet!
    class DisableSpectator : Command
    {
        private const string Command = "DisableSpectator";

        public DisableSpectator()
            :base(CommandType.DisableSpectator)
        { }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}

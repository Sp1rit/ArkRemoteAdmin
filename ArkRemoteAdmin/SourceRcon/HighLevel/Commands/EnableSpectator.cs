﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    // TODO: Doesn't work yet!
    class EnableSpectator : Command
    {
        private const string Command = "EnableSpectator";

        public EnableSpectator()
            :base(CommandType.EnableSpectator)
        { }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
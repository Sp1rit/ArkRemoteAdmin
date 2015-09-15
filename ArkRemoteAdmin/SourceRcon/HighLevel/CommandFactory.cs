using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel
{
    static class CommandFactory
    {
        public static T Create<T>() where T : Command
        {
            return Activator.CreateInstance<T>();
        }
    }
}

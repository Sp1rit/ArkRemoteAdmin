using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class SaveWorld : Command, ICommand
    {
        private const string Command = "SaveWorld";

        public SaveWorld()
            :base(CommandType.SaveWorld)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "World Saved";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}

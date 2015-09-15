using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class ShowMessageOfTheDay : Command, ICommand
    {
        private const string Command = "ShowMessageOfTheDay";

        public ShowMessageOfTheDay()
            :base(CommandType.ShowMessageOfTheDay)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Message of the day Shown";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}

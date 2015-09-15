using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class SetMessageOfTheDay : Command, ICommand
    {
        private const string Command = "SetMessageOfTheDay";

        public string Message { get; set; }

        public SetMessageOfTheDay()
            : base(CommandType.SetMessageOfTheDay)
        { }

        public SetMessageOfTheDay(string message)
            : this()
        {
            Message = message;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"Message of set to {Message}";
        }

        public override string ToString()
        {
            return $"{Command} {Message}";
        }
    }
}

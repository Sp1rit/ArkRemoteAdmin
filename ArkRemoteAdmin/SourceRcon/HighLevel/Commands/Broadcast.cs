using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class Broadcast : Command, ICommand
    {
        private const string Command = "Broadcast";

        public string Message { get; set; }

        public Broadcast()
            : base(CommandType.Broadcast)
        { }

        public Broadcast(string message)
            : this()
        {
            Message = message;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Server received, But no response!!";
        }

        public override string ToString()
        {
            return $"{Command} {Message}";
        }
    }
}

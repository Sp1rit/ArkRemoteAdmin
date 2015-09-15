using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon.HighLevel.Commands
{
    class ServerChat : Command, ICommand
    {
        private const string Command = "ServerChat";

        public string Message { get; set; }

        public ServerChat()
            :base(CommandType.ServerChat)
        { }

        public ServerChat(string message)
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

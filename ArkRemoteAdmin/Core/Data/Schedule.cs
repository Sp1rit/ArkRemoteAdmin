using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Rcon.Commands;
using ArkRcon = ArkRemoteAdmin.Core.ArkRcon;

namespace ArkRemoteAdmin.Data
{
    [Serializable]
    public class Schedule
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool Active { get; set; }
        public ScheduleType Type { get; set; }
        public MessageType MessageType { get; set; }

        [XmlIgnore]
        public TimeSpan TimeSpan
        {
            get { return XmlConvert.ToTimeSpan(TimeSpan_string); }
            set { TimeSpan_string = XmlConvert.ToString(value); }
        }

        [Browsable(false)]
        [XmlElement("TimeSpan")]
        public string TimeSpan_string { get; set; }

        public Server Server
        {
            get { return Data.Servers.FirstOrDefault(s => s.Id == ServerId); }
        }

        public Schedule()
        {
            Id = Guid.NewGuid();
            Type = ScheduleType.Minute;
            MessageType = MessageType.BroadCast;
            Active = false;
        }

        public void Run()
        {
            if (ArkRcon.Client.IsConnected && ArkRcon.ConnectedServer.Id == Server.Id)
            {
                if (MessageType == MessageType.Chat)
                {
                    string message = Message.ToRcon();
                    if (!string.IsNullOrEmpty(ArkRcon.ConnectedServer.ChatName))
                        message = ArkRcon.ConnectedServer.ChatName + ": " + message;
                    ArkRcon.Client.ExecuteCommandAsync(new ServerChat(message), null);
                }
                else if (MessageType == MessageType.BroadCast)
                    ArkRcon.Client.ExecuteCommandAsync(new Broadcast(Message), null);
                else
                {
                    foreach (string command in Message.Split(Environment.NewLine.ToCharArray()))
                        ArkRcon.Client.ExecuteCommandAsync(new Raw(command.ToRcon()), null);
                }
            }
        }
    }

    public enum ScheduleType
    {
        Minute,
        Hourly,
        Daily
    }

    public enum MessageType
    {
        BroadCast,
        Chat,
        Commands
    }
}

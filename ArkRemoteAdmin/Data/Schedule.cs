using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
        }
    }

    public enum ScheduleType
    {
        Minute,
        Hourly,
        Daily
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.Data
{
    [Serializable]
    public class Server
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public bool Default { get; set; }

        public Server()
        {
            Id = Guid.NewGuid();
        }

        //[OptionalField(VersionAdded = 2)]
        //[OnDeserializing]
        //private void SetDefaults(StreamingContext sc)
        //{
        //    Default = false;
        //}
    }
}

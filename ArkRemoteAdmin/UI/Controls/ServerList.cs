using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.UI.Controls
{
    internal sealed class ServerList : FlowLayoutPanel
    {
        public ServerList()
        {
            this.FlowDirection = FlowDirection.TopDown;
            this.WrapContents = false;
            this.AutoScroll = true;
            this.DoubleBuffered = true;
            this.Padding = new Padding(0, 5, 0, 0);
            //this.Font = new System.Drawing.Font("Tahoma", 8f);
        }

        public void AddItem()
        {
            ServerEntry aeroPanel = new ServerEntry();
            //aeroPanel.Width = this.Width - SystemInformation.VerticalScrollBarWidth - 10 - 100;
            this.Controls.Add(aeroPanel);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            foreach (ServerEntry entry in Controls)
                entry.Width = Width - 25;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.UI
{
    public partial class Servers : UserControl
    {
        public Servers()
        {
            InitializeComponent();
            serverList1.AddItem();
            serverList1.AddItem();
            serverList1.AddItem();
            serverList1.AddItem();
        }
    }
}

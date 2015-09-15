using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            test1.Columns.Add(new ColumnHeader {Text = "Name", Width = 100});
            test1.Columns.Add(new ColumnHeader {Text = "SteamId", Width = 150 });
            test1.Columns.Add(new ColumnHeader {Text = "Level", Width = test1.Width});
        }
    }
}

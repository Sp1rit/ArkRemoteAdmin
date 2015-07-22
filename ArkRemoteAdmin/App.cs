using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin
{
    class App
    {
        private FormMain formMain;

        public void Run()
        {
            formMain = new FormMain();
            Application.Run(formMain);
        }

        public void Focus()
        {
            formMain.SetVisible();
            formMain.Activate();
        }
    }
}

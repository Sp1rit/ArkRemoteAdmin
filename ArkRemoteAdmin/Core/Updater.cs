using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin
{
    class Updater
    {
        private static FileInfo updaterFile = new FileInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "updater.exe"));

        public static bool Exists()
        {
            return updaterFile.Exists;
        }

        public static void CheckNow()
        {
            try
            {
                if (Exists())
                    Process.Start(updaterFile.FullName, "/checknow");
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void CheckSilent()
        {
            try
            {
                if (Exists())
                    Process.Start(updaterFile.FullName, "/silentall -nofreqcheck");
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}

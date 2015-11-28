using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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

        public static async Task<Version> CheckWebVersion()
        {
            try
            {
                WebClient client = new WebClient();
                Version newVersion = Version.Parse(await client.DownloadStringTaskAsync("http://xunion.net/ArkRemoteAdmin/version.txt"));
                Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                if (newVersion > currentVersion)
                    return newVersion;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

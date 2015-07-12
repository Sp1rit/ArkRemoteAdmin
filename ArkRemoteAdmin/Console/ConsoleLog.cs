using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin
{
    static class ConsoleLog
    {
        private static string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArkRemoteAdmin");
        private static string logPath = Path.Combine(appDataPath, "ConsoleLog");

        public static void Append(string log)
        {
            try
            {
                if (!Directory.Exists(Path.Combine(logPath, Rcon.Server.Id.ToString())))
                    Directory.CreateDirectory(Path.Combine(logPath, Rcon.Server.Id.ToString()));

                using (FileStream fs = File.Open(Path.Combine(logPath, Rcon.Server.Id.ToString(), string.Format("{0:yyyyMMdd}.log", DateTime.Now)), FileMode.Append))
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine("[{0:yyyy-MM-dd HH:mm:ss}] {1}", DateTime.Now, log.Replace("\n", Environment.NewLine));
                }
            }
            catch (Exception ex)
            {
                // TODO: log
            }
        }
    }
}

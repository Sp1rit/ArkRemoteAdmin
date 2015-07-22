using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkRemoteAdmin
{
    static class Logging
    {
        public static readonly DirectoryInfo LogPath = new DirectoryInfo(Path.Combine(Data.Data.AppDataPath.FullName, "Log"));
        public static readonly DirectoryInfo ChatLogPath = new DirectoryInfo(Path.Combine(Data.Data.AppDataPath.FullName, "ChatLog"));
        public static readonly DirectoryInfo ConsoleLogPath = new DirectoryInfo(Path.Combine(Data.Data.AppDataPath.FullName, "ConsoleLog"));

        public static void LogChat(string message)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(ChatLogPath.FullName, Rcon.Server.Id.ToString()));
            if (!di.Exists)
                di.Create();

            Log(di.FullName, message);
        }

        public static void LogConsole(string message)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(ConsoleLogPath.FullName, Rcon.Server.Id.ToString()));
            if (!di.Exists)
                di.Create();

            Log(di.FullName, message.Replace("\n", Environment.NewLine));
        }

        public static void Log(string message)
        {
            if (!LogPath.Exists)
                LogPath.Create();

            Log(LogPath.FullName, message);
        }

        private static void Log(string path, string message)
        {
            try
            {
                using (FileStream fs = File.Open(Path.Combine(path, string.Format("{0:yyyyMMdd}.log", DateTime.Now)), FileMode.Append))
                using (TextWriter tw = new StreamWriter(fs))
                    tw.WriteLine("[{0:HH:mm:ss}] {1}", DateTime.Now, message);
            }
            catch (Exception ex)
            {
                // TODO: log
            }
        }
    }
}

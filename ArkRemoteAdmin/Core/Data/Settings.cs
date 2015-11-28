using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ArkRemoteAdmin.UserInterface.Modules;

namespace ArkRemoteAdmin.Data
{
    static class Settings
    {
        private static readonly FileInfo SettingsFile = new FileInfo(Path.Combine(Data.AppDataPath.FullName, "Settings.xml"));

        public static void Load()
        {
            General.AllowMultipleInstances = false;
            General.AutoReconnect = true;
            Server.GetChatInterval = 10;
            Server.PlayerRefreshInterval = 30;
            Server.AutoRefreshPlayers = true;

            if (SettingsFile.Exists)
            {
                XDocument xDoc = XDocument.Load(SettingsFile.FullName);
                XElement settings = xDoc.Element("Settings");
                if (settings != null)
                {
                    XElement general = settings.Element("General");
                    if (general != null)
                    {
                        General.AllowMultipleInstances = general.Element("AllowMultipleInstances").GetValue(false);
                        General.AutoReconnect = general.Element("AutoReconnect").GetValue(true);
                    }
                    XElement server = settings.Element("Server");
                    if (server != null)
                    {
                        Server.GetChatInterval = server.Element("GetChatInterval").GetValue(10);
                        Server.PlayerRefreshInterval = server.Element("PlayerRefreshInterval").GetValue(30);
                        Server.AutoRefreshPlayers = server.Element("AutoRefreshPlayers").GetValue(true);
                    }
                }
            }
        }

        public static void Save()
        {
            if (!SettingsFile.Directory.Exists)
                SettingsFile.Directory.Create();

            XDocument xDoc = new XDocument(
                new XElement("Settings",
                    new XElement("General",
                        new XElement("AllowMultipleInstances", General.AllowMultipleInstances),
                        new XElement("AutoReconnect", General.AutoReconnect)),
                    new XElement("Server",
                        new XElement("GetChatInterval", Server.GetChatInterval),
                        new XElement("PlayerRefreshInterval", Server.PlayerRefreshInterval),
                        new XElement("AutoRefreshPlayers", Server.AutoRefreshPlayers))
                        ));

            xDoc.Save(SettingsFile.FullName);
        }

        public static class General
        {
            public static bool AllowMultipleInstances { get; set; }
            public static bool AutoReconnect { get; set; }
        }

        public static class Server
        {
            public static int GetChatInterval { get; set; }
            public static int PlayerRefreshInterval { get; set; }
            public static bool AutoRefreshPlayers { get; set; }
        }
    }
}

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
                    }
                }

                return;
            }

            General.AllowMultipleInstances = false;
        }

        public static void Save()
        {
            if (!SettingsFile.Directory.Exists)
                SettingsFile.Directory.Create();

            XDocument xDoc = new XDocument(
                new XElement("Settings",
                    new XElement("General",
                        new XElement("AllowMultipleInstances", General.AllowMultipleInstances))
                        ));

            xDoc.Save(SettingsFile.FullName);
        }

        public static class General
        {
            public static bool AllowMultipleInstances { get; set; }
        }
    }
}

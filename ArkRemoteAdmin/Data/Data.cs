﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ArkRemoteAdmin.Data
{
    static class Data
    {
        private static string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArkRemoteAdmin");
        private static DirectoryInfo directory = new DirectoryInfo(Path.Combine(appDataPath, "Data"));

        public static void Load()
        {
            if (!directory.Exists)
            {
                // TODO: Log
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Server));
            servers = new List<Server>();
            foreach (var file in directory.GetFiles("*.server"))
            {
                try
                {
                    using (FileStream fs = File.OpenRead(file.FullName))
                    {
                        var server = serializer.Deserialize(fs) as Server;
                        if (server != null)
                        {
                            servers.Add(server);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Log
                }
            }

            serializer = new XmlSerializer(typeof(Schedule));
            schedules = new List<Schedule>();
            foreach (var file in directory.GetFiles("*.schedule"))
            {
                try
                {
                    using (FileStream fs = File.OpenRead(file.FullName))
                    {
                        var schedule = serializer.Deserialize(fs) as Schedule;
                        if (schedule != null)
                        {
                            schedules.Add(schedule);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Log
                }
            }
        }

        #region Server

        private static List<Server> servers = new List<Server>();

        public static Server[] Servers
        {
            get { return servers.ToArray(); }
        }

        public static void Set(Server server)
        {
            try
            {
                if (!directory.Exists)
                    directory.Create();

                XmlSerializer xml = new XmlSerializer(typeof(Server));

                File.Delete(Path.Combine(directory.FullName, server.Id + ".server"));
                using (FileStream fs = File.Open(Path.Combine(directory.FullName, server.Id + ".server"), FileMode.OpenOrCreate, FileAccess.Write))
                    xml.Serialize(fs, server);

                if (!servers.Exists(s => s.Id == server.Id))
                    servers.Add(server);
            }
            catch (Exception ex)
            {
                // TODO: Log
            }
        }

        public static void Remove(Server server)
        {
            try
            {
                if (File.Exists(Path.Combine(directory.FullName, server.Id + ".server")))
                    File.Delete(Path.Combine(directory.FullName, server.Id + ".server"));

                if (servers.Contains(server))
                    servers.Remove(server);
            }
            catch (Exception ex)
            {
                // TODO: Log
            }
        }

        #endregion // Server

        #region Schedule

        private static List<Schedule> schedules = new List<Schedule>();

        public static Schedule[] Schedules
        {
            get { return schedules.ToArray(); }
        }

        public static void Set(Schedule schedule)
        {
            try
            {
                if (!directory.Exists)
                    directory.Create();

                XmlSerializer xml = new XmlSerializer(typeof(Schedule));

                File.Delete(Path.Combine(directory.FullName, schedule.Id + ".schedule"));
                using (FileStream fs = File.OpenWrite(Path.Combine(directory.FullName, schedule.Id + ".schedule")))
                    xml.Serialize(fs, schedule);

                if (!schedules.Exists(s => s.Id == schedule.Id))
                    schedules.Add(schedule);
            }
            catch (Exception ex)
            {
                // TODO: Log
            }
        }

        public static void Remove(Schedule schedule)
        {
            try
            {
                if (File.Exists(Path.Combine(directory.FullName, schedule.Id + ".schedule")))
                    File.Delete(Path.Combine(directory.FullName, schedule.Id + ".schedule"));

                if (schedules.Contains(schedule))
                    schedules.Remove(schedule);
            }
            catch (Exception ex)
            {
                // TODO: Log
            }
        }

        #endregion // Schedule
    }
}
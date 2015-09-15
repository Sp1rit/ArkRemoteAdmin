using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;
using ArkRemoteAdmin.SourceRcon;
using Quartz;

namespace ArkRemoteAdmin
{
    static class RconOld
    {
        private static RconClient client;
        static readonly Regex PlayerMatch = new Regex(@"[0-9]\. (?<Name>.*), (?<SteamId>[0-9]{17})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static event EventHandler<CommandExecutedEventArgs> CommandExecuted;
        private static void OnCommandExecuted(string command, string response)
        {
            EventHandler<CommandExecutedEventArgs> handler = CommandExecuted;
            if (handler != null)
                handler(null, new CommandExecutedEventArgs(command, response));
        }

        public static event EventHandler Connected;
        private static void OnConnected()
        {
            EventHandler handler = Connected;
            if (handler != null)
                handler(null, new EventArgs());

            StartPlayerRefresher();
            if (Server.GetChat)
                StartChatReceiver();
        }

        public static event EventHandler Disconnected;
        private static void OnDisconnected()
        {
            EventHandler handler = Disconnected;
            if (handler != null)
                handler(null, new EventArgs());

            StopPlayerRefresher();
            StopChatReceiver();
        }

        public static event EventHandler<Player[]> PlayersRefreshed;

        private static void OnPlayersRefreshed(Player[] players)
        {
            EventHandler<Player[]> handler = PlayersRefreshed;
            if (handler != null)
                handler(null, players);
        }

        public static event EventHandler<string> ChatReceived;

        private static void OnChatReceived(string messages)
        {
            EventHandler<string> handler = ChatReceived;
            if (handler != null)
                handler(null, messages);
        }

        public static bool IsConnected
        {
            get { return client != null && client.IsConnected; }
        }

        public static Server Server { get; private set; }

        public static bool Connect(Server server)
        {
            if (IsConnected)
                throw new Exception("You are already connected to a server.");

            Server = server;
            if (client == null)
            {
                client = new RconClient();
                client.Connected += (s, e) => OnConnected();
                client.Disconnected += (s, e) => OnDisconnected();
            }

            string message;
            if (!client.Connect(server.Host, server.Port, Encryption.DecryptString(server.Password, "0?NRAnRBm;SWd41BUbKsT7)kN1y=RHLm=DR4ZZUBk&!JF3i\"Ra2Eg,8qwhA0^ydo"), out message))
                throw new Exception(message);

            return true;
        }

        public static void Disconnect()
        {
            client.Disconnect();
        }

        private static void StartPlayerRefresher()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            JobKey jobKey = new JobKey("UpdatePlayers");
            TriggerKey triggerKey = new TriggerKey("UpdatePlayers");

            IJobDetail job;
            if (scheduler.CheckExists(jobKey))
                job = scheduler.GetJobDetail(jobKey);
            else
            {
                job = JobBuilder.Create<Jobs.UpdatePlayersJob>()
                    .WithIdentity(jobKey)
                    .Build();
            }

            ITrigger trigger;
            if (scheduler.CheckExists(triggerKey))
                trigger = scheduler.GetTrigger(triggerKey);
            else
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerKey)
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(Settings.Server.PlayerRefreshInterval)
                        .RepeatForever()
                        .WithMisfireHandlingInstructionNextWithExistingCount())
                    .StartNow()
                    .ForJob(job)
                    .Build();
            }

            scheduler.ScheduleJob(job, trigger);
        }

        private static void StopPlayerRefresher()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("UpdatePlayers");

            scheduler.UnscheduleJob(triggerKey);
        }

        public static void StartChatReceiver()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            JobKey jobKey = new JobKey("GetChat");
            TriggerKey triggerKey = new TriggerKey("GetChat");

            IJobDetail job;
            if (scheduler.CheckExists(jobKey))
                job = scheduler.GetJobDetail(jobKey);
            else
            {
                job = JobBuilder.Create<Jobs.GetChatJob>()
                    .WithIdentity(jobKey)
                    .Build();
            }

            ITrigger trigger;
            if (scheduler.CheckExists(triggerKey))
                trigger = scheduler.GetTrigger(triggerKey);
            else
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerKey)
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(Settings.Server.GetChatInterval)
                        .RepeatForever()
                        .WithMisfireHandlingInstructionNextWithExistingCount())
                    .StartNow()
                    .ForJob(job)
                    .Build();
            }
            
            scheduler.ScheduleJob(job, trigger);
        }

        public static void StopChatReceiver()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("GetChat");

            scheduler.UnscheduleJob(triggerKey);
        }

        public static async Task<string> ExecuteCommand(string command)
        {
            try
            {
                if (IsConnected)
                {
                    string response = await Task.Run(() => client.ExecuteCommand(command));

                    if (command != "getchat" && command != "listplayers")
                        OnCommandExecuted(command, response);

                    return response ?? "";
                }
            }
            catch (Exception ex)
            {
                Disconnect();
            }

            return "";
        }

        public static async Task<Player[]> ListPlayers()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("listplayers");

            List<Player> players =
                (from Match match in PlayerMatch.Matches(response)
                 select new Player()
                 {
                     Name = match.Groups["Name"].Value,
                     SteamId = match.Groups["SteamId"].Value
                 }).ToList();

            return players.ToArray();
        }

        public static void RefreshPlayers()
        {
            try
            {
                Task<Player[]> task = ListPlayers();
                task.Wait();
                if (task.IsCompleted)
                {
                    Player[] players = task.Result;

                    if (players != null)
                        OnPlayersRefreshed(players);
                }
            }
            catch
            {
                // ignored
            }
        }

        public static async Task<bool> Broadcast(string message)
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand(string.Format("broadcast {0}",
                message
                .Replace("ä", "ae")
                .Replace("ü", "ue")
                .Replace("ö", "oe")
                .Replace("Ä", "Ae")
                .Replace("Ö", "Oe")
                .Replace("Ü", "Ue")
                .Replace(Environment.NewLine, "\n")));

            return response.Trim() == "Server received, But no response!!";
        }

        public static async Task<bool> SetMessageOfTheDay(string message)
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server");

            string motd = message.Replace(Environment.NewLine, "\n").Trim();
            string response = await ExecuteCommand($"setmessageoftheday {motd}");

            return response.Trim() == $"Message of set to {motd}";
        }

        public static async Task<string> GetChat()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("getchat");
            response = response.Trim().Replace("\n", Environment.NewLine);
            if (response != "Server received, But no response!!")
            {
                OnChatReceived(response);
                return response;
            }

            return "";
        }

        public static async Task<bool> SendChatMessage(string message)
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response;
            if (string.IsNullOrEmpty(Server.ChatName))
            {
                response = await ExecuteCommand(string.Format("serverchat {0}",
                    message
                    .Replace("ä", "ae")
                    .Replace("ü", "ue")
                    .Replace("ö", "oe")
                    .Replace("Ä", "Ae")
                    .Replace("Ö", "Oe")
                    .Replace("Ü", "Ue")
                    .Replace(Environment.NewLine, "\n")));
            }
            else
            {
                response = await ExecuteCommand(string.Format("serverchat {0}: {1}", Server.ChatName,
                    message
                    .Replace("ä", "ae")
                    .Replace("ü", "ue")
                    .Replace("ö", "oe")
                    .Replace("Ä", "Ae")
                    .Replace("Ö", "Oe")
                    .Replace("Ü", "Ue")
                    .Replace(Environment.NewLine, "\n")));
            }

            return response.Trim() == "Server received, But no response!!";
        }

        public static async Task<bool> ShowMessageOfTheDay()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("ShowMessageOfTheDay");

            return response.Trim() == "Message of the day Shown";
        }

        public static async Task<bool> SetTimeOfDay(DateTime time)
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand($"SetTimeOfDay {time:HH:mm:ss}");

            return response.Trim() == $"Time of day has been set to {time:HH:mm:ss}";
        }

        public static async Task<bool> ExitServer()
        {
            if (!IsConnected)
                throw new Exception("You are not connected to a server.");

            string response = await ExecuteCommand("DoExit");

            return response.Trim() == "Exiting...";
        }
    }
}

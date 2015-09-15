using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkRemoteAdmin.Data;
using ArkRemoteAdmin.SourceRcon.HighLevel.Commands;
using Quartz;

namespace ArkRemoteAdmin.SourceRcon.HighLevel
{
    static class ArkRcon
    {
        #region Events

        public static event EventHandler<List<Player>> PlayersRefreshed;
        public static event EventHandler<string> ChatMessages;

        #endregion // Events

        public static RconClient Client { get; } = new RconClient();

        public static Server ConnectedServer { get; private set; }

        public static bool Connect(Server server)
        {
            ConnectedServer = server;

            return Client.Connect(server.Host, server.Port, Encryption.DecryptString(server.Password, "0?NRAnRBm;SWd41BUbKsT7)kN1y=RHLm=DR4ZZUBk&!JF3i\"Ra2Eg,8qwhA0^ydo"));
        }

        public static void RefreshPlayers()
        {
            Client.ExecuteLowPrioCommandAsync(new ListPlayers(), PlayersListed);
        }

        private static void PlayersListed(object sender, CommandExecutedEventArgs e)
        {
            if (e.Successful)
            {
                List<Player> players = ListPlayers.ParsePlayers(e.Response);
                PlayersRefreshed?.Invoke(Client, players);
            }
        }

        public static void StartPlayerRefresh()
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

        public static void PausePlayerRefresh()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("UpdatePlayers");

            scheduler.PauseTrigger(triggerKey);
        }

        public static void ResumePlayerRefresh()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("UpdatePlayers");

            scheduler.ResumeTrigger(triggerKey);
        }

        public static void StopPlayerRefresh()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("UpdatePlayers");

            scheduler.UnscheduleJob(triggerKey);
        }

        public static void GetChat()
        {
            Client.ExecuteLowPrioCommandAsync(new GetChat(), ChatReceived);
        }

        private static void ChatReceived(object sender, CommandExecutedEventArgs e)
        {
            if (e.Successful)
            {
                if (e.Response.Trim() != "Server received, But no response!!")
                    ChatMessages?.Invoke(sender, e.Response);
            }
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

        public static void PauseChatReceiver()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("GetChat");

            scheduler.PauseTrigger(triggerKey);
        }

        public static void ResumeChatReceiver()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("GetChat");

            scheduler.ResumeTrigger(triggerKey);
        }

        public static void StopChatReceiver()
        {
            IScheduler scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
            TriggerKey triggerKey = new TriggerKey("GetChat");

            scheduler.UnscheduleJob(triggerKey);
        }
    }
}

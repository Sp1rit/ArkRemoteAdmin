using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArkRemoteAdmin.Data;
using Quartz;
using Quartz.Impl;

namespace ArkRemoteAdmin
{
    static class Scheduler
    {
        private static readonly IScheduler DefaultScheduler = StdSchedulerFactory.GetDefaultScheduler();

        public static void CreateAndActivateSchedules(IEnumerable<Schedule> schedules)
        {
            foreach (Schedule schedule in schedules)
            {
                AddSchedule(schedule);
                if (schedule.Active)
                    ActivateSchedule(schedule);
            }
        }

        public static void ClearSchedules()
        {
            DefaultScheduler.Clear();
        }

        public static void AddSchedule(Schedule schedule)
        {
            IJobDetail job = JobBuilder.Create<Jobs.CommandJob>()
                .WithIdentity(schedule.GetJobKey())
                .UsingJobData(new JobDataMap(new Dictionary<string, Schedule>() { { "Schedule", schedule } }))
                .StoreDurably()
                .Build();

            DefaultScheduler.AddJob(job, true);
        }

        public static void DeleteSchedule(Schedule schedule)
        {
            DefaultScheduler.DeleteJob(schedule.GetJobKey());
        }

        public static void ActivateSchedule(Schedule schedule)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .WithIdentity(schedule.GetTriggerKey())
                .ForJob(schedule.GetJobKey());

            if (schedule.Type == ScheduleType.Daily)
            {
                builder.WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .WithMisfireHandlingInstructionFireNow()
                    .RepeatForever()
                    )
                .StartAt(DateBuilder.DateOf(schedule.TimeSpan.Hours, schedule.TimeSpan.Minutes, schedule.TimeSpan.Seconds));
            }
            else if (schedule.Type == ScheduleType.Hourly)
            {
                builder.WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .WithMisfireHandlingInstructionFireNow()
                    .RepeatForever()
                    )
                .StartAt(NextDateForHour(schedule.TimeSpan.Minutes, schedule.TimeSpan.Seconds));
            }
            else if (schedule.Type == ScheduleType.Minute)
            {
                builder.WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(schedule.TimeSpan.Minutes)
                    .WithMisfireHandlingInstructionFireNow()
                    .RepeatForever()
                    )
                .StartAt(DateBuilder.DateOf((DateTime.Now.Minute + schedule.TimeSpan.Minutes > 59) ? DateTime.Now.Hour + 1 : DateTime.Now.Hour, (DateTime.Now.Minute + schedule.TimeSpan.Minutes) % 60, DateTime.Now.Second));
            }

            ITrigger trigger = builder.Build();

            DefaultScheduler.UnscheduleJob(trigger.Key);
            DefaultScheduler.ScheduleJob(trigger);
        }

        public static void DeactivateSchedule(Schedule schedule)
        {
            if (DefaultScheduler.CheckExists(schedule.GetTriggerKey()))
                DefaultScheduler.UnscheduleJob(schedule.GetTriggerKey());
        }

        private static DateTimeOffset NextDateForHour(int minutes, int seconds)
        {
            DateTime now = DateTime.Now;

            if (now.Minute < minutes || now.Minute == minutes && now.Second < seconds)
                return DateBuilder.DateOf(now.Hour, minutes, seconds);
            else if (now.Hour < 23)
                return DateBuilder.DateOf(now.Hour + 1, minutes, seconds);
            else
                return DateBuilder.TomorrowAt(0, minutes, seconds);
        }

        #region Extensions

        public static JobKey GetJobKey(this Schedule schedule)
        {
            return new JobKey(schedule.Id.ToString(), schedule.Server.Id.ToString());
        }

        public static TriggerKey GetTriggerKey(this Schedule schedule)
        {
            return new TriggerKey(schedule.Id.ToString(), schedule.Server.Id.ToString());
        }

        #endregion // Extensions
    }
}

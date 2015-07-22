using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArkRemoteAdmin.Data;

namespace ArkRemoteAdmin
{
    class Scheduler
    {
        #region Singleton

        private static readonly Lazy<Scheduler> _instance = new Lazy<Scheduler>(() => new Scheduler());

        public static Scheduler Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        #endregion // Singleton

        private readonly Dictionary<Schedule, Timer> timers;

        private Scheduler()
        {
            timers = new Dictionary<Schedule, Timer>();
        }

        public void Activate(Schedule schedule)
        {
            if (!timers.ContainsKey(schedule))
            {
                TimeSpan start = TimeSpan.Zero;
                TimeSpan delay = TimeSpan.Zero;
                DateTime now = DateTime.Now;
                DateTime target;
                switch (schedule.Type)
                {
                    case ScheduleType.Minute:
                        start = new TimeSpan(0, 0, schedule.TimeSpan.Minutes, 0, 0);
                        delay = start;
                        break;
                    case ScheduleType.Hourly:
                        target = new DateTime(now.Year, now.Month, now.Day, now.Hour, schedule.TimeSpan.Minutes, schedule.TimeSpan.Seconds);
                        if (!(now.Minute < schedule.TimeSpan.Minutes || now.Minute == schedule.TimeSpan.Minutes && now.Second < schedule.TimeSpan.Seconds))
                            target = target.AddHours(1);

                        start = target - now;
                        delay = new TimeSpan(0, 1, 0, 0, 0);
                        break;
                    case ScheduleType.Daily:
                        target = new DateTime(now.Year, now.Month, now.Day, schedule.TimeSpan.Hours, schedule.TimeSpan.Minutes, schedule.TimeSpan.Seconds);
                        if (!(now.Hour < schedule.TimeSpan.Minutes || now.Hour == schedule.TimeSpan.Hours && now.Minute < schedule.TimeSpan.Minutes
                              || now.Hour == schedule.TimeSpan.Hours && now.Minute == schedule.TimeSpan.Minutes && now.Second < schedule.TimeSpan.Seconds))
                            target = target.AddDays(1);

                        start = target - now;
                        delay = new TimeSpan(1, 0, 0, 0, 0);
                        break;
                }
                timers.Add(schedule, new Timer(Timer_Tick, schedule, start, delay));
                schedule.Active = true;
                Data.Data.Set(schedule);
            }
        }

        public void Deactivate(Schedule schedule)
        {
            if (timers.ContainsKey(schedule))
            {
                Timer timer = timers[schedule];
                timer.Dispose();

                timers.Remove(schedule);

                schedule.Active = false;
                Data.Data.Set(schedule);
            }
        }

        public void DeactivateAll()
        {
            foreach (var timer in timers)
                timer.Value.Dispose();

            timers.Clear();
        }

        private async void Timer_Tick(object state)
        {
            Schedule schedule = state as Schedule;

            if (schedule != null && Rcon.IsConnected)
            {
                if (schedule.MessageType == MessageType.Chat)
                    await Rcon.SendChatMessage(schedule.Message);
                else
                    await Rcon.Broadcast(schedule.Message);
            }
        }
    }
}

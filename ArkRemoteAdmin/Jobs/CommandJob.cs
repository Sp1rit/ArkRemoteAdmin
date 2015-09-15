using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkRemoteAdmin.Data;
using Quartz;

namespace ArkRemoteAdmin.Jobs
{
    class CommandJob : IJob
    {
        public Schedule Schedule { get; set; }

        public void Execute(IJobExecutionContext context)
        {
            Schedule?.Run();
        }
    }
}

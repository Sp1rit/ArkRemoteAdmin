using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using ArkRcon = ArkRemoteAdmin.Core.ArkRcon;

namespace ArkRemoteAdmin.Jobs
{
    [DisallowConcurrentExecution]
    class GetChatJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ArkRcon.GetChat();
        }
    }
}

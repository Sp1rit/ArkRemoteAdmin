using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using ArkRcon = ArkRemoteAdmin.SourceRcon.HighLevel.ArkRcon;

namespace ArkRemoteAdmin.Jobs
{
    [DisallowConcurrentExecution]
    class UpdatePlayersJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ArkRcon.RefreshPlayers();
        }
    }
}

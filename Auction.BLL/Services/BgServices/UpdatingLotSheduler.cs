using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.BgServices
{
    public class UpdatingLotSheduler
    {
        public static async Task Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<UpdatingLotService>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["TimeToCheckLotsInSeconds"]))
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}

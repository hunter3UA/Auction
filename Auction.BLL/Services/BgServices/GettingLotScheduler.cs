using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace Auction.BLL.Services.BgServices
{
    public class GettingLotScheduler
    {
        public static async Task Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<GettingLotService>().Build();
            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x          
                    .WithIntervalInSeconds(100)         
                    .RepeatForever())                  
                .Build(); 
            
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
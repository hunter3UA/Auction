using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.BgServices
{
    public class GettingLotScheduler
    {
        public static async Task Start()
        {
            IScheduler scheduler=await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<GettingLotService>().Build();
            ITrigger trigger=TriggerBuilder.Create()
                .WithIdentity("trigger1","group1")
                .StartNow()
                .WithSimpleSchedule(x=>x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job,trigger);
        }
    }
}




/*public class EmailScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
 
            IJobDetail job = JobBuilder.Create<EmailSender>().Build();
 
            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInMinutes(1)          // через 1 минуту
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер
 
            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
    }*/
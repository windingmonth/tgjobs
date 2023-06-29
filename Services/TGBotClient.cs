using Quartz;
using Quartz.Impl;
using tgjobs.Jobs;

namespace tgjobs.Services
{
    public class TGBotClient
    {
        public async void Start()
        {
            DoWeatherJob();
        }

        private async void DoWeatherJob()
        {
            //创建一个任务
            IJobDetail job = JobBuilder.Create<WeatherJob>()
             .WithIdentity("WeatherJob", "Test")
            .Build();

            //创建一个触发条件
            ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("WeatherJobTrigger", "Test")
               //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 30))
               .WithSimpleSchedule(x =>
               {
                   x.WithIntervalInHours(1).RepeatForever();
               })
               .Build();

            StdSchedulerFactory factory = new StdSchedulerFactory();
            //创建任务调度器
            IScheduler scheduler = await factory.GetScheduler();
            //启动任务调度器
            await scheduler.Start();
            //将创建的任务和触发器条件添加到创建的任务调度器当中
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
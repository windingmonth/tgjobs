using Newtonsoft.Json;
using Quartz;

namespace tgjobs.Jobs
{
    public class NewsJob : IJob
    {
        private const string APITOKEN = "cb8bd924b59bfb7c2ad6339813a1ac21";
        private const string TGBOTAPITOKEN = "5085940110:AAGAY9Cp1-FH6IskBQeusEw1iGf20-uKR5c";
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using var client = new HttpClient();
                var resp = await client.GetAsync($"http://v.juhe.cn/toutiao/index?type=top&key={APITOKEN}");
                
                await client.PostAsJsonAsync($"https://api.telegram.org/bot{TGBOTAPITOKEN}/sendMessage", new { text = "OK", chat_id = "-961553778" });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}

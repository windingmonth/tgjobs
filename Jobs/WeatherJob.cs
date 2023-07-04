using Newtonsoft.Json;
using Quartz;

namespace tgjobs.Jobs
{
    public class WeatherJob : IJob
    {
        private const string APITOKEN = "1c85eabd72bfb9542a8e851fb23bb170";
        private const string TGBOTAPITOKEN = "5085940110:AAGAY9Cp1-FH6IskBQeusEw1iGf20-uKR5c";

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using var client = new HttpClient();
                var resp = await client.GetAsync($"https://apis.juhe.cn/fapig/soup/query?key=1c85eabd72bfb9542a8e851fb23bb170&key={APITOKEN}");
                var message = await resp.Content.ReadAsStringAsync();

                var infoResp = JsonConvert.DeserializeObject<InfoResp>(message);
                var info = infoResp.result;

                await client.PostAsJsonAsync($"https://api.telegram.org/bot{TGBOTAPITOKEN}/sendMessage", new { text = info.text, chat_id = "@kimorebi_channel" });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }

    public class InfoResp 
    {
        public Result result { get; set; }
    }

    public class Result 
    {
        public string text { get; set; }
    }

    //public class WeatherResp
    //{
    //    public string reason { get; set; }
    //    public Result result { get; set; }
    //}

    //public class Result
    //{
    //    public string city { get; set; }
    //    public Realtime realtime { get; set; }
    //    public List<Future> future { get; set; }
    //    public int? error_code { get; set; }
    //}

    //public class Realtime
    //{
    //    public string temperature { get; set; }
    //    public string humidity { get; set; }
    //    public string info { get; set; }
    //    public string wid { get; set; }
    //    public string direct { get; set; }
    //    public string power { get; set; }
    //    public string aqi { get; set; }
    //}

    //public class Future
    //{
    //    public string date { get; set; }
    //    public string temperature { get; set; }
    //    public string weather { get; set; }
    //    public string direct { get; set; }
    //}
}
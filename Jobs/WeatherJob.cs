using Newtonsoft.Json;
using Quartz;

namespace tgjobs.Jobs
{
    public class WeatherJob : IJob
    {
        private const string APITOKEN = "5085940110:AAGAY9Cp1-FH6IskBQeusEw1iGf20-uKR5c";

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using var client = new HttpClient();
                var resp = await client.GetAsync("http://apis.juhe.cn/simpleWeather/query?city=深圳&key=9b38ee94a41252e3dc8727d7ef7bb08c");
                var weatherMessage = await resp.Content.ReadAsStringAsync();
                var weatherResp = JsonConvert.DeserializeObject<WeatherResp>(weatherMessage);
                var weather = weatherResp.result;
                var sendMessage = $"城市：{weather.city},\r\n今天天气:\r\n[\r\n\t温度: {weather.realtime.temperature},\r\n\t湿度: {weather.realtime.humidity}, \r\n\t天气状况: {weather.realtime.info},\r\n\t风力: {weather.realtime.direct} {weather.realtime.power}, \r\n\t空气质量: {weather.realtime.aqi}\r\n]";

                await client.PostAsJsonAsync($"https://api.telegram.org/bot{APITOKEN}/sendMessage", new { text = sendMessage, chat_id = "-961553778" });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }

    public class WeatherResp
    {
        public string reason { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public string city { get; set; }
        public Realtime realtime { get; set; }
        public List<Future> future { get; set; }
        public int? error_code { get; set; }
    }

    public class Realtime
    {
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string info { get; set; }
        public string wid { get; set; }
        public string direct { get; set; }
        public string power { get; set; }
        public string aqi { get; set; }
    }

    public class Future
    {
        public string date { get; set; }
        public string temperature { get; set; }
        public string weather { get; set; }
        public string direct { get; set; }
    }
}
using tgjobs.Services;

namespace tgjobs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<TGBotClient>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            var botClient = app.Services.GetRequiredService<TGBotClient>();
            botClient.Start();

            app.Run();
        }
    }
}
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging.Console;
using OpenAI.Net;

namespace RenameEpisodeFiles
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }
        public static IOpenAIService OpenAIService { get; private set; }
        public static ILoggerFactory LoggerFactory { get; private set; }
        public static ILogger Logger { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Build configuration to load secrets.json from the application directory
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true)
                .Build();

            // Configure OpenAI service
            var services = new ServiceCollection();
            services.AddOpenAIServices(options =>
            {
                options.ApiKey = Configuration["OpenAI:ApiKey"];
            });
            var provider = services.BuildServiceProvider();
            OpenAIService = provider.GetRequiredService<IOpenAIService>();

            // Configure logging
            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder
                    .SetMinimumLevel(LogLevel.Debug) // Ensure Debug level is enabled
                    .AddDebug()    // Logs to Output window in Visual Studio
                    .AddConsole();
            });
            Logger = LoggerFactory.CreateLogger("App");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
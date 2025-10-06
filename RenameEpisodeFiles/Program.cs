using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging.Console;
using OpenAI.Net;
using Serilog;
using Serilog.Extensions.Logging;

namespace RenameEpisodeFiles
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; } = null!;
        public static IOpenAIService? OpenAIService { get; private set; }
        public static ILoggerFactory LoggerFactory { get; private set; } = null!;
        public static Microsoft.Extensions.Logging.ILogger Logger { get; private set; } = null!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Build configuration to load secrets.json from the application directory
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
                .Build();

            // Configure OpenAI service only if Configuration is not null and ApiKey exists
            if (Configuration != null && !string.IsNullOrWhiteSpace(Configuration["OpenAI:ApiKey"]))
            {
                var services = new ServiceCollection().AddOpenAIServices(static options =>
                {
                    options.ApiKey = Configuration["OpenAI:ApiKey"];
                });
                var provider = services.BuildServiceProvider();
                OpenAIService = provider.GetRequiredService<IOpenAIService>();
            }

            // Configure Serilog for file logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("app.log", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();

            // Integrate Serilog with Microsoft.Extensions.Logging
            LoggerFactory = new SerilogLoggerFactory(Log.Logger, dispose: false);
            Logger = LoggerFactory.CreateLogger("App");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
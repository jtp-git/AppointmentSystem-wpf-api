using AppointmentSystem.App.Infrastructure;
using AppointmentSystem.App.Services;
using AppointmentSystem.App.ViewModels;
using AppointmentSystem.Application.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace AppointmentSystem.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        public App()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            Services = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Bind ApiSettings
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            // HttpClient configuration
            services.AddHttpClient<IAppointmentApiService, AppointmentApiService>((provider, client) =>
            {
                var apiSettings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
            });

            // ViewModels
            services.AddTransient<AppointmentListViewModel>();
            services.AddSingleton<ViewModelLocator>();
            // Views
            services.AddTransient<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
    

}

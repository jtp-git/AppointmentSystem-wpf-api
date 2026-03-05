using AppointmentSystem.App.Services;
using AppointmentSystem.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7106/")
            });

            services.AddSingleton<AppointmentApiService>();
            services.AddSingleton<AppointmentListViewModel>();

            Services = services.BuildServiceProvider();

            base.OnStartup(e);
        }
    }

}

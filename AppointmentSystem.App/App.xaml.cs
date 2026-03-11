using AppointmentSystem.App.Services;
using AppointmentSystem.App.ViewModels;
using AppointmentSystem.Application.DTO;
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

        protected override async void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();


            services.AddHttpClient<IAppointmentApiService, AppointmentApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7029/");
            });

            services.AddSingleton<AppointmentListViewModel>();

            Services = services.BuildServiceProvider();

            base.OnStartup(e);


            // TEMPORARY TEST
            //var api = Services.GetRequiredService<IAppointmentApiService>();


            //var appointment = await api.GetAppointmentByIdAsync(1, CancellationToken.None);

            //if (appointment != null)
            //{
            //    MessageBox.Show($"Appointment: {appointment.Patient}");
            //}
            //else
            //{
            //    MessageBox.Show("Appointment not found");
            //}



            //var dto = new CreateAppointmentDto
            //{
            //    Patient = "Create Test",
            //    StartTime = DateTime.Now.AddHours(1),
            //    EndTime = DateTime.Now.AddHours(2),
            //    Notes = "Initial consultation"
            //};

            //await api.CreateAsync(dto, CancellationToken.None);

            //MessageBox.Show("Appointment created successfully!");


            //var appointment = await api.GetAppointmentByIdAsync(1, CancellationToken.None);

            //if (appointment != null)
            //{
            //    MessageBox.Show($"Appointment Found: {appointment.Patient}");
            //}
            //else
            //{
            //    MessageBox.Show("Appointment not found");
            //}
        }
    }

}

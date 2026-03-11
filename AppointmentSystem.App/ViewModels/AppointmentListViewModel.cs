using AppointmentSystem.App.Services;
using AppointmentSystem.Application.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppointmentSystem.App.ViewModels
{
    public class AppointmentListViewModel : BaseViewModel
    {
        private readonly IAppointmentApiService _appointmentApiService;
        public ObservableCollection<AppointmentDto> Appointments { get; } =  new();

        public ICommand LoadAppointmentsCommand { get; }

        public AppointmentListViewModel(IAppointmentApiService appointmentApiService)
        {
            _appointmentApiService = appointmentApiService;
            LoadAppointmentsCommand = new Commands.RelayCommand(async _ => await LoadAppointmentsAsync());
            _ = LoadAppointmentsAsync();
        }

        private async Task LoadAppointmentsAsync()
        {
            using var cts = new CancellationTokenSource();
            var list = await _appointmentApiService.GetAppointmentsAsync(cts.Token);
            Appointments.Clear();
            foreach (var appointment in list)
            {
                Appointments.Add(appointment);
            }
        }
    }
}

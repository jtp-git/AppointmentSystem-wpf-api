using AppointmentSystem.App.Services;
using AppointmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppointmentSystem.App.ViewModels
{
    public class AppointmentListViewModel : BaseViewModel
    {
        private readonly AppointmentApiService _appointmentApiService;
        public ObservableCollection<AppointmentDto> Appointments { get; } =  new();

        public ICommand LoadAppointmentsCommand { get; }

        public AppointmentListViewModel(AppointmentApiService appointmentApiService)
        {
            _appointmentApiService = appointmentApiService;
            LoadAppointmentsCommand = new Commands.RelayCommand(async _ => await LoadAppointmentsAsync());
            _ = LoadAppointmentsAsync();
        }

        private async Task LoadAppointmentsAsync()
        {
            using var cts = new CancellationTokenSource();
            var list = await _appointmentApiService.GetAppointments(cts.Token);
            Appointments.Clear();
            foreach (var appointment in list)
            {
                Appointments.Add(appointment);
            }
        }
    }
}

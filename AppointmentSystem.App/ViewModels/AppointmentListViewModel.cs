using AppointmentSystem.App.Commands;
using AppointmentSystem.App.Services;
using AppointmentSystem.Application.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace AppointmentSystem.App.ViewModels
{
    public class AppointmentListViewModel : BaseViewModel
    {
        private readonly IAppointmentApiService _appointmentApiService;
        public ObservableCollection<AppointmentDto> Appointments { get; } = new();

        private List<string> _allTimes = new();
        public ObservableCollection<string> StartTimes { get; set; }
        public ObservableCollection<string> EndTimes { get; set; }

        private AppointmentDto? _selectedAppointment;
        public AppointmentDto? SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (SetProperty(ref _selectedAppointment, value) && value != null)
                {
                    Patient = value.Patient;
                    Notes = value.Notes;
                    AppointmentDate = value.StartTime.Date;

                    SelectedStartTime = value.StartTime.ToString("HH:mm");
                    SelectedEndTime = value.EndTime.ToString("HH:mm");
                }
            }
        }
        private string? _patient;
        public string? Patient
        {
            get => _patient;
            set => SetProperty(ref _patient, value);
        }

        private string? _notes;
        public string? Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        private DateTime? _appointmentDate;
        public DateTime? AppointmentDate
        {
            get => _appointmentDate;
            set => SetProperty(ref _appointmentDate, value);
        }

        private string? _selectedStartTime;
        public string? SelectedStartTime
        {
            get => _selectedStartTime;
            set
            {
                if (SetProperty(ref _selectedStartTime, value))
                    FilterEndTimes();
            }
        }


        private string? _selectedEndTime;
        public string? SelectedEndTime
        {
            get => _selectedEndTime;
            set => SetProperty(ref _selectedEndTime, value);
        }
        public ICommand LoadAppointmentsCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public AppointmentListViewModel(IAppointmentApiService appointmentApiService)
        {
            _appointmentApiService = appointmentApiService;

            StartTimes = GenerateTimes();
            EndTimes = new ObservableCollection<string>(StartTimes);

            LoadAppointmentsCommand = new RelayCommand(LoadAppointmentsAsync);
            CreateCommand = new RelayCommand(CreateAppointmentAsync);
            UpdateCommand = new RelayCommand(UpdateAppointmentAsync);
            DeleteCommand = new RelayCommand(DeleteAppointmentAsync);

            _ = LoadAppointmentsAsync();
        }

        private ObservableCollection<string> GenerateTimes()
        {
            var times = new ObservableCollection<string>();

            for (int hour = 8; hour <= 18; hour++)
            {
                var t1 = $"{hour:00}:00";
                var t2 = $"{hour:00}:30";

                times.Add(t1);
                times.Add(t2);

                _allTimes.Add(t1);
                _allTimes.Add(t2);
            }

            return times;
        }

        private void FilterEndTimes()
        {
            if (SelectedStartTime == null)
                return;

            var start = TimeSpan.Parse(SelectedStartTime);

            EndTimes.Clear();

            foreach (var time in _allTimes)
            {
                if (TimeSpan.Parse(time) > start)
                    EndTimes.Add(time);
            }

            SelectedEndTime = null;
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
        private async Task CreateAppointmentAsync()
        {
            if (AppointmentDate == null || SelectedStartTime == null || SelectedEndTime == null)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            var start = AppointmentDate.Value.Date + TimeSpan.Parse(SelectedStartTime);
            var end = AppointmentDate.Value.Date + TimeSpan.Parse(SelectedEndTime);

            var dto = new CreateAppointmentDto
            {
                Patient = Patient,
                StartTime = start,
                EndTime = end,
                Notes = Notes
            };

            await _appointmentApiService.CreateAsync(dto, CancellationToken.None);

            await LoadAppointmentsAsync();
            ClearForm();
        }

        private async Task UpdateAppointmentAsync()
        {
            if (SelectedAppointment == null)
                return;

            var start = AppointmentDate.Value.Date + TimeSpan.Parse(SelectedStartTime);
            var end = AppointmentDate.Value.Date + TimeSpan.Parse(SelectedEndTime);

            var dto = new UpdateAppointmentDto
            {
                Id = SelectedAppointment.Id,
                Patient = Patient,
                StartTime = start,
                EndTime = end,
                Notes = Notes
            };

            await _appointmentApiService.UpdateAsync(dto, CancellationToken.None);

            await LoadAppointmentsAsync();
            ClearForm();
        }

        private async Task DeleteAppointmentAsync()
        {
            if (SelectedAppointment == null)
                return;

            await _appointmentApiService.DeleteAsync(SelectedAppointment.Id, CancellationToken.None);

            await LoadAppointmentsAsync();
            ClearForm();
        }

        private void ClearForm()
        {
            Patient = "";
            Notes = "";
            AppointmentDate = null;
            SelectedStartTime = null;
            SelectedEndTime = null;
            SelectedAppointment = null;
        }
    }
}

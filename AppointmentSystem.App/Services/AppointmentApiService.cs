using AppointmentSystem.Application.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


namespace AppointmentSystem.App.Services
{
    public class AppointmentApiService : IAppointmentApiService
    {
        private readonly HttpClient _httpClient;
        private const string AppointmentEndpoint = "api/v1/appointments";

        public AppointmentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppointmentDto>> GetAppointmentsAsync(CancellationToken cancellation)
        {
            var response = await _httpClient.GetAsync(AppointmentEndpoint, cancellation);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellation);

            return JsonSerializer.Deserialize<List<AppointmentDto>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        }
        public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{AppointmentEndpoint}/{id}", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AppointmentDto>(cancellationToken: cancellationToken);

        }

        public async Task CreateAsync(CreateAppointmentDto dto, CancellationToken cancellationToken)
        {
            if (dto.EndTime <= dto.StartTime)
            {
                throw new ArgumentException("End time must be greater than start time.");
            }
            var response = await _httpClient.PostAsJsonAsync(AppointmentEndpoint, dto, cancellationToken);
            response.EnsureSuccessStatusCode();

        }

        public async Task UpdateAsync(UpdateAppointmentDto dto, CancellationToken cancellationToken)
        {

            var response = await _httpClient.PutAsJsonAsync($"{AppointmentEndpoint}/{dto.Id}", dto, cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.DeleteAsync($"{AppointmentEndpoint}/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();
        }

    }
}

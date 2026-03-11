using AppointmentSystem.Application.DTO;
using System.Net.Http;
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

    }
}

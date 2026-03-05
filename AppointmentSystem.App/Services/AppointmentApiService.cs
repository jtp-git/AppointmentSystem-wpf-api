using AppointmentSystem.Application.DTO;
using System.Net.Http;
using System.Text.Json;


namespace AppointmentSystem.App.Services
{
    public class AppointmentApiService
    {
        private readonly HttpClient _httpClient;

        public AppointmentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppointmentDto>> GetAppointments(CancellationToken cancellation)
        {
            var response = await _httpClient.GetAsync("appointments", cancellation);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellation);
            return JsonSerializer.Deserialize<List<AppointmentDto>>(
                json, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

        }

    }
}

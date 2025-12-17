using HardwareMonitoring.Models;
using System.Net.Http.Json;

namespace HardwareMonitoring.Services
{
    public class DataSender
    {
        private const string REQUARED_SERVICE_URL = "/api/monitor";
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl;

        public DataSender(string serverUrl)
        {
            _httpClient = new HttpClient();
            _serverUrl = serverUrl + REQUARED_SERVICE_URL;
        }

        public async Task SendDataAsync(ComputerModel data)
        {
            try
            {
                // Отправляем POST запрос с JSON телом
                var response = await _httpClient.PostAsJsonAsync(_serverUrl, data);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Success] Data sent to server. Code: {response.StatusCode}");
                }
                else
                {
                    Console.WriteLine($"[Error] Server rejected data. Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Exception] Failed to send data: {ex.Message}");
            }
        }
    }
}

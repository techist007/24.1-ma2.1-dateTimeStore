using HttpStoreApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HttpStoreApi.Repositories
{
    public class HttpStoreRepository : IHttpStoreRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpStoreRepository> _logger;

        public HttpStoreRepository(HttpClient httpClient, IConfiguration configuration, ILogger<HttpStoreRepository> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> CallTimeZoneApiAsync(TimeZoneRequest request)
        {
            try
            {
                var url = _configuration["ApiUrls:TimeZoneApi"];
                _logger.LogInformation("Sending request to {Url} with data: {Request}", url, request);

                var response = await _httpClient.PostAsJsonAsync(url, request);
                _logger.LogInformation("Received response status code: {StatusCode}", response.StatusCode);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP Request error occurred while calling TimeZone API");
                throw new Exception("There was an HTTP error processing your request. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General error occurred while calling TimeZone API");
                throw new Exception("There was a general error processing your request. Please try again later.");
            }
        }

        public async Task<string> CallPacketApiAsync(PacketRequest request)
        {
            try
            {
                var url = _configuration["ApiUrls:PacketApi"];
                _logger.LogInformation("Sending request to {Url} with data: {Request}", url, request);

                var response = await _httpClient.PostAsJsonAsync(url, request);
                _logger.LogInformation("Received response status code: {StatusCode}", response.StatusCode);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP Request error occurred while calling Packet API");
                throw new Exception("There was an HTTP error processing your request. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General error occurred while calling Packet API");
                throw new Exception("There was a general error processing your request. Please try again later.");
            }
        }
    }
}

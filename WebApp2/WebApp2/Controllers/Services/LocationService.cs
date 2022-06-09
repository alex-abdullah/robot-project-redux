using System;
using System.Net.Http;
using System.IO;
using System.Text.Json;
namespace WebApp2.Controller.Services
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LocationService> _logger;
        private readonly string _url;

        public LocationService(HttpClient client, ILogger<LocationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _logger.LogCritical("Created Service");
            _httpClient = client;
            var section = configuration.GetSection("HttpCustomHeaders:User-agent");
            var locationSection = configuration.GetSection("GeocodingAPI:BaseURL");
            _logger.LogCritical(section.Key + ": " + section.Value);
            _logger.LogCritical("BaseURL: " + section.Value);
            _httpClient.DefaultRequestHeaders.Add(section.Key, section.Value);
        }

        public async Task<Location[]> GetNearestWaterLocation(string location)
        {                      
            var x = await _httpClient.GetAsync($"https://nominatim.openstreetmap.org/search?format=json&q=water+near+{location}");
            var JsonObjectString = await x.Content.ReadAsStringAsync();    

            return JsonSerializer.Deserialize<Location[]>(JsonObjectString);             
                        
        }
    }
}
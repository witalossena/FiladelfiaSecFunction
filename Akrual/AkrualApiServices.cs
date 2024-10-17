using FiladelfiaFunction.Akrual.Models;
using FiladelfiaFunction.Akrun.Models;
using FiladelfiaFunction.Filadelfia.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Akrun
{
    public class AkrualApiServices
    {
        private readonly HttpClient _httpClient;
        private readonly AkrualSettings _appSettings;
        private readonly AuthAkrual _authAkrual;


        public AkrualApiServices(HttpClient httpClient, IOptions<AkrualSettings> appSettings, AuthAkrual authService)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
            _authAkrual = authService;
        }


        public async Task<List<Series>> GetAllSeries()
        {
            var accessToken = await _authAkrual.GetAccessTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"{_appSettings.BaseUrl}/Escrituracao/GetAllSeries");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var series = JsonConvert.DeserializeObject<List<Series>>(jsonResponse);

            return series;
        }
        public async Task<Dictionary<string, List<Pu>>> GetPus(string SerieId)
        {
            var accessToken = await _authAkrual.GetAccessTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"{_appSettings.BaseUrl}/CRM/GetPus?serieId={SerieId}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var seriesData = JsonConvert.DeserializeObject<Dictionary<string, List<Pu>>>(jsonResponse);

            return seriesData;  
        }




    }
}

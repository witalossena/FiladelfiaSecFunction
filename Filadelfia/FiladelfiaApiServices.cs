using FiladelfiaFunction.Akrun.Models;
using FiladelfiaFunction.Data;
using FiladelfiaFunction.Filadelfia.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Filadelfia
{

    public class FiladelfiaApiServices
    {
        private readonly HttpClient _httpClient;
        private readonly FiladelfiaSettings _settings;
        private readonly FiladelfiaDb _filadelfiaDb;


        public FiladelfiaApiServices(HttpClient httpClient, IOptions<FiladelfiaSettings> appSettings, FiladelfiaDb filadelfiaDb)
        {
            _httpClient = httpClient;
            _settings = appSettings.Value;
            _filadelfiaDb = filadelfiaDb;

            var byteArray = Encoding.ASCII.GetBytes($"{_settings.Username}:{_settings.Password}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<List<Emissao>> GetEmissoes()
        {
            var response = await _httpClient.GetAsync(_settings.BaseUrl + "/emissoes");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var emissoes = JsonConvert.DeserializeObject<List<Emissao>>(jsonResponse);


            return emissoes;

        }

        public async Task DeleteEmissao(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_settings.BaseUrl}/emissoes/{id}");

        }

        public async Task<string> CreateEmissaoWordPressPost(Emissao emissao)
        {

            var jsonEmissao = JsonConvert.SerializeObject(emissao);

            var emissao_testejson = new StringContent(jsonEmissao, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_settings.BaseUrl}/emissoes", emissao_testejson);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;

        }


        public void CreateEmissao(Emissao emissao)
        {
            try
            {
                _filadelfiaDb.InsertOrUpdateWpEmissao(emissao);

            }
            catch (Exception)
            {

                throw;
            }


        }

        public void CreateDesagio(Desagio desagio)
        {

        }


    }
}

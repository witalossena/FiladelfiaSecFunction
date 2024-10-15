using FiladelfiaFunction.Akrun.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Akrun
{
    public class AuthAkrual
    {
        private readonly AkrualSettings _options;
        private readonly HttpClient _httpClient;

        public AuthAkrual(IOptions<AkrualSettings> options, HttpClient httpClient)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            var authData = new
            {
                username = _options.Username,
                password = _options.Password,
                grant_type = "password"
            };

            var jsonContent = JsonConvert.SerializeObject(authData);

            var content = new StringContent(jsonContent, Encoding.ASCII, "application/json");

            var authResponse = await _httpClient.PostAsync(_options.BaseUrl + "/oauth/token", content);
            authResponse.EnsureSuccessStatusCode();

            var jsonResponse = await authResponse.Content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<AuthTokenResponse>(jsonResponse);

            return tokenResponse?.AccessToken;
        }

    }
}

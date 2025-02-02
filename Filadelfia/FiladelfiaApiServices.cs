using FiladelfiaFunction.Akrun.Models;
using FiladelfiaFunction.Data;
using FiladelfiaFunction.Filadelfia.Models;
using Microsoft.Extensions.Hosting;
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

    public async Task<List<DetalhesEmissao>> GetEmissoesWordpressApi()
    {
      var response = await _httpClient.GetAsync(_settings.BaseUrl + "/emissoes");

      response.EnsureSuccessStatusCode();

      var jsonResponse = await response.Content.ReadAsStringAsync();

      var emissoes = JsonConvert.DeserializeObject<List<DetalhesEmissao>>(jsonResponse);

      return emissoes;

    }

    public async Task DeleteEmissaoWordpressApi(string id)
    {
      var response = await _httpClient.DeleteAsync($"{_settings.BaseUrl}/emissoes/{id}");

    }

    public async Task<List<DetalhesEmissao>> GetEmissoesJetEngine()
    {
      var response = await _httpClient.GetAsync($"{_settings.BaseUrlJet}/detalhes");

      response.EnsureSuccessStatusCode();

      var jsonResponse = await response.Content.ReadAsStringAsync();

      var emissoes = JsonConvert.DeserializeObject<List<DetalhesEmissao>>(jsonResponse);


      return emissoes;

    }

    public async Task<string> CreateEmissaoDetalhesJetEngine(DetalhesEmissao emissao)
    {
      var jsonEmissao = JsonConvert.SerializeObject(emissao);

      var detalhes = new StringContent(jsonEmissao, Encoding.UTF8, "application/json");

      var response = await _httpClient.PostAsync($"{_settings.BaseUrlJet}/detalhes", detalhes);

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStringAsync();

      return responseContent;

    }
    public async Task<string> CreateEmissaoDesagioJetEngine(Pu PuData)
    {
      var jsonEmissao = JsonConvert.SerializeObject(PuData);

      var data = new StringContent(jsonEmissao, Encoding.UTF8, "application/json");

      var response = await _httpClient.PostAsync($"{_settings.BaseUrlJet}/desagio", data);

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStringAsync();

      return responseContent;

    }

    public async Task<string> UpdateEmissaoDetalhesJetEngine(DetalhesEmissao emissao)
    {

      var jsonEmissao = JsonConvert.SerializeObject(emissao);

      var detalhes = new StringContent(jsonEmissao, Encoding.UTF8, "application/json");

      var response = await _httpClient.PostAsync($"{_settings.BaseUrlJet}/detalhes/{emissao.Id}", detalhes);

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStringAsync();

      return responseContent;

    }

    public async Task CreateOrUpdateEmissao(DetalhesEmissao emissao)
    {
      try
      {
        _filadelfiaDb.DeleteDesagioJetCct(emissao.SerieId);

        if (await _filadelfiaDb.PostExist(emissao.NomeSerie))
        {
          _filadelfiaDb.UpdateWpEmissao(emissao);
        }
        else
        {
          await CreateEmissaoDetalhesJetEngine(emissao);
        }

        foreach (var item in emissao.PuData)
        {
          await CreateEmissaoDesagioJetEngine(item);
        }

      }
      catch (Exception)
      {
        throw;
      }
    }


    public async Task<string> UpdateEmissaoDetalhesDocumentosJetEngine(Akrual.Models.Document document)
    {

      var documentJson = JsonConvert.SerializeObject(document);

      var item = new StringContent(documentJson, Encoding.UTF8, "application/json");

      var response = await _httpClient.PostAsync($"{_settings.BaseUrlJet}/detalhesdocumentos", item);

      response.EnsureSuccessStatusCode();

      var responseContent = await response.Content.ReadAsStringAsync();

      return responseContent;

    }
    public async Task DeleteDocuments()
    {
      await _filadelfiaDb.DeleteDocumentos();
    }



  }
}

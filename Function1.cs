using System;
using System.Text.Json;
using FiladelfiaFunction.Akrun;
using FiladelfiaFunction.Filadelfia;
using FiladelfiaFunction.Filadelfia.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FiladelfiaFunction
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly AkrualApiServices _akrualApiServices;
        private readonly FiladelfiaApiServices _filadelfiaApiServices;

        public Function1(ILoggerFactory loggerFactory, AkrualApiServices akrualApiServices, FiladelfiaApiServices filadelfiaApiServices)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _akrualApiServices = akrualApiServices;
            _filadelfiaApiServices = filadelfiaApiServices;
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer)
        {

            var emissao = new Emissao
            {
           
                Title = "POST FUNCTION",
                Status = "publish",
                Meta = new Meta
                {
                    SerieId = "1290",
                    NomeFantasia = "POST FUNCTION",
                    CodigoIn = "",
                    CodigoCetip = "",
                    DataUltimoPagamento = "",
                    JurosUltimoPagamento = "0",
                    DataProximoPagamento = "",
                    JurosProximoPagamento = "0",
                    TipoEmissao = "",
                    NumeroEmissao = "0",
                    TipoSubordinacao = "",                
                    NumeroSerie = "0",
                    DataEmissao = "2024-05-06T00:00:00",
                    DataVencimento = "2029-05-07T00:00:00",
                    Quantidade = "5700",
                    PuEmissao = "1000m",
                    ValorGlobalEmissao = "5700000m",
                    PeriodoPagamentoJuros = "Anual",
                    PeriodoPagamentoAmort = "Anual",
                    Remuneracao = "CDI + 10,0000% a.a",
                    TipoDeOferta = "Indefinido",
                    MedicaoIntegralizacaoCotas = "",
                    Cedentes = "",
                    Escriturador = "",
                    CoordenadorLider = "",
                    NaturezaLastro = "Não definido"
                }
            };

            var data = await _filadelfiaApiServices.CreateEmissao(emissao);
            var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            _logger.LogInformation($"API Data: {jsonData}");
        }
    }
}

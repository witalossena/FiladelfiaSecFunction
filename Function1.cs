using System;
using System.Text.Json;
using FiladelfiaFunction.Akrun;
using FiladelfiaFunction.Akrun.Models;
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
            List<Series> series = await _akrualApiServices.GetAllSeries();

            foreach (Series item in series)
            {
                var emissao = new Emissao
                {
                   Title = item.NomeFantasia ?? "",
                   Status = "publish",
                   Meta = new Meta
                   {
                       SerieId = item.SerieId ?? "",
                       NomeFantasia = item.NomeFantasia ?? "",
                       CodigoISIN = item.CodigoISIN,
                       CodigoCETIP = item.CodigoCETIP,
                       DataUltimoPagamento = item.DataUltimoPagamento ?? "",
                       JurosUltimoPagamento = item.JurosProximoPagamento ?? "",
                       DataProximoPagamento = item.DataProximoPagamento ?? "",
                       JurosProximoPagamento = item.JurosProximoPagamento ?? "",
                       TipoEmissao = item.TipoEmissao ?? "",
                       NumeroEmissao = item.NumeroEmissao ?? "",
                       TipoSuboordinacao = item.TipoSuboordinacao ?? "",                
                       NumeroSerie = item.NumeroSerie ?? "",
                       DataEmissao = item.DataEmissao ?? "",
                       DataVencimento = item.DataVencimento ?? "",
                       Quantidade = item.Quantidade ?? "",
                       PUEmissao = item.PUEmissao ?? "",
                       ValorGlobalEmissao = item.ValorGlobalEmissao ?? "",
                       PeriodoPagamentoJuros = item.PeriodoPagamentoJuros ?? "",
                       PeriodoPagamentoAmort = item.PeriodoPagamentoAmort ?? "",
                       Remuneracao = item.Remuneracao ?? "",
                       TipoDeOferta = item.TipoDeOferta ?? "",
                       MedicaoIntegralizacaoCotas = item.MedicaoIntegralizacaoCotas ?? "",
                       Cedentes = item.Cedentes ?? "",
                       Escriturador = item.Escriturador ?? "",
                       CoordenadorLider = item.CoordenadorLider ?? "",
                       NaturezaLastro = item.NaturezaLastro ?? "",
                       AgenteFiduciario = item.AgenteFiduciario,
                       NomeSerie = item.NomeSerie ?? "",
                       AmortProximoPagamento = item.AmortProximoPagamento ?? "",
                       IndiceCorrecao = item.IndiceCorrecao ?? "",
                       IsSimulada = item.IsSimulada,
                       IsAtivo = "1"
                   }
                };

                await _akrualApiServices.GetPus(emissao.Meta.SerieId);

                //_filadelfiaApiServices.CreateEmissao(emissao);

                //var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions
                //{
                //    WriteIndented = true
                //});

            }     

            _logger.LogInformation($"Finished!");
        }
    }
}

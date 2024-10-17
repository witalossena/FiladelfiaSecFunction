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


        //public async Task Run([TimerTrigger("0 0 8 * * *")] TimerInfo myTimer) //RODARA TODO DIA AS 8H DA MANHA
        public async Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer) //RODARA A CADA 2 MINUTOS

       // public async Task Run([TimerTrigger("0 0 */2 * * *")] TimerInfo myTimer) //TRIGGER SETADO PARA RODAR A CADA 2 HORAS
        {       
            List<Series> series = await _akrualApiServices.GetAllSeries();

            foreach (Series item in series)
            {
                var emissao = new DetalhesEmissao
                {
                    SerieId = item.SerieId ?? "",
                    NomeFantasia = item.NomeFantasia ?? "",
                    CodigoIsin = item.CodigoISIN ?? "",
                    CodigoCetip = item.CodigoCETIP ?? "",
                    DataUltimoPagamento = item.DataUltimoPagamento ?? "",
                    JurosUltimoPagamento = item.JurosProximoPagamento ?? "",
                    DataProximoPagamento = item.DataProximoPagamento ?? "",
                    JurosProximoPagamento = item.JurosProximoPagamento ?? "",
                    TipoEmissao = item.TipoEmissao ?? "",
                    NumeroEmissao = item.NumeroEmissao ?? "",
                    TiposSubordinacao = item.TipoSuboordinacao ?? "",
                    NumeroSerie = item.NumeroSerie ?? "",
                    DataEmissao = item.DataEmissao ?? "",
                    DataVencimento = item.DataVencimento ?? "",
                    Quantidade = item.Quantidade ?? "",
                    PuEmissao = item.PUEmissao ?? "",
                    ValorGlobalEmissao = item.ValorGlobalEmissao ?? "",
                    PeriodoPagamentoJuros = item.PeriodoPagamentoJuros ?? "",
                    PeriodoPagamentoAmort = item.PeriodoPagamentoAmort ?? "",
                    Remuneracao = item.Remuneracao ?? "",
                    TipoDeOferta = item.TipoDeOferta ?? "",
                    MedicacaoIntegralizacaoCotas = item.MedicaoIntegralizacaoCotas ?? "",
                    Cedentes = item.Cedentes ?? "",
                    Escriturador = item.Escriturador ?? "",
                    CoordenadorLider = item.CoordenadorLider ?? "",
                    NaturezaLastro = item.NaturezaLastro ?? "",
                    AgenteFiduciario = item.AgenteFiduciario ?? "",
                    NomeSerie = item.NomeSerie ?? "",
                    AmortProximoPagamento = item.AmortProximoPagamento ?? "",
                    IndiceCorrecao = item.IndiceCorrecao ?? "",
                    IsSimulada = item.IsSimulada,
                    IsAtivo = item.IsAtivo,


                };

                await _filadelfiaApiServices.CreateOrUpdateEmissao(emissao);        

            }

            _logger.LogInformation($"Finished!");
        }
    }
}

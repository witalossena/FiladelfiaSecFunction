using System;
using System.Text.Json;
using FiladelfiaFunction.Akrual.Models;
using FiladelfiaFunction.Akrun;
using FiladelfiaFunction.Akrun.Models;
using FiladelfiaFunction.Filadelfia;
using FiladelfiaFunction.Filadelfia.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Pqc.Crypto.Frodo;
using Pu = FiladelfiaFunction.Filadelfia.Models.Pu;

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

            foreach (Series item in series.Where(x=> x.IsAtivo == "true" /*&& x.IsSimulada == "false" */))
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
                    TipoSuboordinacao = item.TipoSuboordinacao ?? "",
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
                    MedicaoIntegralizacaoCotas = item.MedicaoIntegralizacaoCotas ?? "",
                    Cedentes = item.Cedentes ?? "",
                    Escriturador = item.Escriturador ?? "",
                    CoordenadorLider = item.CoordenadorLider ?? "",
                    NaturezaLastro = item.NaturezaLastro ?? "",
                    AgenteFiduciario = item.AgenteFiduciario ?? "",
                    NomeSerie = item.NomeSerie ?? "",
                    AmortProximoPagamento = item.AmortProximoPagamento ?? "",
                    IndiceCorrecao = item.IndiceCorrecao ?? "",
                    IsSimulada = item.IsSimulada,
                    IsAtivo = item.IsAtivo                   
                };

                var Desagio = await _akrualApiServices.GetPus(item.SerieId);

                foreach (var itemPu in Desagio)
                {
                    var pu = new Pu
                    {
                        CctStatus = "publish",
                        CctAuthorId = "1",
                        CctCreated = DateTime.Now.ToString(),
                        CctModified = DateTime.Now.ToString(),
                        VNAJ = itemPu.VNAJ ?? "0",
                        Data = itemPu.Data,
                        JurosEsperado = itemPu.JurosEsperado ?? "0",
                        JurosPago = itemPu.JurosPago ?? "0",
                        JurosExtra = itemPu.JurosExtra ?? "0",
                        SeriesId = itemPu.Serie_Id,
                        PUResidual = itemPu.PUResidual ?? "0"
                    };

                    emissao.PuData.Add(pu);

                }


                await _filadelfiaApiServices.CreateOrUpdateEmissao(emissao);

            }

            _logger.LogInformation($"Finished!");
        }
    }
}

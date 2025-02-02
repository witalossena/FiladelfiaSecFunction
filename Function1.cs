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
    public async Task Run([TimerTrigger("0 0 8 * * *", RunOnStartup = true)] TimerInfo myTimer)

    {
      List<Series> series = await _akrualApiServices.GetAllSeries();

      //recupera todos os pu da rota Escrituracao/GetAllPus para relacionar ao pu zerado sa serie
      var pus = await _akrualApiServices.GetAllPus();

      foreach (Series item in series.Where(x => x.IsAtivo == "true" && x.IsSimulada == "false"))
      //foreach (Series item in series)
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

        var SingleSerie = await _akrualApiServices.GetSingleSerie(item.SerieId);

        var elementosParaAlterar = Desagio.Where(a => a.PUResidual == "0").ToList();

        foreach (var elemento in elementosParaAlterar)
        {
          // Encontra o PU correspondente na lista `pus` com base no SerieId
          var puCorrespondente = pus.FirstOrDefault(p => p.SerieId == item.SerieId)?.PU;

          // Se encontrar um PU correspondente, atualiza o PUResidual
          if (puCorrespondente.HasValue)
          {
            elemento.PUResidual = puCorrespondente.Value.ToString();
          }
        }

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

        await _filadelfiaApiServices.DeleteDocuments();

        foreach (var itemDoc in SingleSerie.Documents)
        {
          itemDoc.SerieId = emissao.SerieId;

          await _filadelfiaApiServices.UpdateEmissaoDetalhesDocumentosJetEngine(itemDoc);

        }

      }

      _logger.LogInformation($"Finished!");
    }
  }
}

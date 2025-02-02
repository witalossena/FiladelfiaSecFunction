using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Akrual.Models
{
  public class SingleSerie
  {
    public DateTime? DataUltimoPagamento { get; set; }
    public decimal JurosUltimoPagamento { get; set; }
    public decimal AmortUltimoPagamento { get; set; }
    public decimal AmexUltimoPagamento { get; set; }
    public DateTime? DataProximoPagamento { get; set; }
    public decimal JurosProximoPagamento { get; set; }
    public decimal AmortProximoPagamento { get; set; }
    public decimal AmexProximoPagamento { get; set; }
    public double DurationEmAnos { get; set; }
    public List<string> Ratings { get; set; } = new();
    public string NaturezaLastro { get; set; }
    public List<string> CamaraDeLiquidacoes { get; set; } = new();
    public List<string> PrivateDocs { get; set; } = new();
    public string? NomeSerie { get; set; }
    public string? CodigoCETIP { get; set; }
    public int? NumeroEmissao { get; set; }
    public int? TipoEmissao { get; set; }
    public string? Emissor { get; set; }
    public string? AgenteFiduciario { get; set; }
    public int? NumeroSerie { get; set; }
    public decimal? ValorGlobalEmissao { get; set; }
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataVencimento { get; set; }
    public DateTime? DataLiquidacao { get; set; }
    public string? IndiceCorrecao { get; set; }
    public decimal? PercentualIndice { get; set; }
    public decimal? Spread { get; set; }
    public string? Remuneracao { get; set; }
    public string? CodigoISIN { get; set; }
    public string? PeriodoPagamentoAmort { get; set; }
    public string? PeriodoPagamentoJuros { get; set; }
    public int? Quantidade { get; set; }
    public decimal? PUEmissao { get; set; }
    public string? SerieId { get; set; }
    public string? NomeFantasia { get; set; }
    public bool? IsSimulada { get; set; }
    public bool? IsAtivo { get; set; }
    public bool IsRegimeFiduciario { get; set; }
    public string TipoDeOferta { get; set; }
    public string NomeEmissao { get; set; }
    public string Cedentes { get; set; }
    public string CedentesDocumentos { get; set; }
    public string Devedores { get; set; }
    public string TipoSuboordinacao { get; set; }
    public string Escriturador { get; set; }
    public string CoordenadorLider { get; set; }
    public int NumeroDeCotas { get; set; }
    public List<Document> Documents { get; set; } = [];
    public List<MedicaoIntegralizacao> MedicaoIntegralizacaoCotas { get; set; } = new();
  }

  public class Document
  {
    [JsonProperty("serieid")]
    public string SerieId { get; set; }

    [JsonProperty("datadocumento")]
    public DateTime DataDocumento { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("urlpublica")]
    public string UrlPublica { get; set; }
    //public List<TipoDocumento> Tipos { get; set; } = [];
    //public string Title { get; set; }
  }

  public class TipoDocumento
  {
    public int KnownTipoDocumento { get; set; }
    public string Name { get; set; }
  }

  public class MedicaoIntegralizacao
  {
    public DateTime Data { get; set; }
    public int CotasEmitidas { get; set; }
    public int CotasIntegralizadas { get; set; }
    public int CotasResgatadas { get; set; }
    public int CotasDisponiveis { get; set; }
  }

}

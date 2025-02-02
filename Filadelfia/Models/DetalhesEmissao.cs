using FiladelfiaFunction.Akrual;
using FiladelfiaFunction.Akrual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Filadelfia.Models
{
  public class DetalhesEmissao
  {


    [JsonProperty("_ID")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("cct_status")]
    public string CctStatus { get; set; } = string.Empty;

    [JsonProperty("cct_single_post_id")]
    public string CctSinglePostId { get; set; } = string.Empty;

    [JsonProperty("cct_author_id")]
    public string CctAuthorId { get; set; } = string.Empty;

    [JsonProperty("cct_created")]
    public string CctCreated { get; set; } = string.Empty;

    [JsonProperty("cct_modified")]
    public string CctModified { get; set; } = string.Empty;

    [JsonProperty("dataultimopagamento")]
    public string DataUltimoPagamento { get; set; } = string.Empty;

    [JsonProperty("jurosultimopagamento")]
    public string JurosUltimoPagamento { get; set; } = string.Empty;

    [JsonProperty("amortultimopagamento")]
    public string AmortUltimoPagamento { get; set; } = string.Empty;

    [JsonProperty("amexultimopagamento")]
    public string AmexUltimoPagamento { get; set; } = string.Empty;

    [JsonProperty("dataproximopagamento")]
    public string DataProximoPagamento { get; set; } = string.Empty;

    [JsonProperty("jurosproximopagamento")]
    public string JurosProximoPagamento { get; set; } = string.Empty;

    [JsonProperty("amortproximopagamento")]
    public string AmortProximoPagamento { get; set; } = string.Empty;

    [JsonProperty("amexproximopagamento")]
    public string AmexProximoPagamento { get; set; } = string.Empty;

    [JsonProperty("durationemanos")]
    public string DurationEmAnos { get; set; } = string.Empty;

    [JsonProperty("ratings")]
    public string Ratings { get; set; } = string.Empty;

    [JsonProperty("naturezalastro")]
    public string NaturezaLastro { get; set; } = string.Empty;

    [JsonProperty("camaradeliquidacoes")]
    public string CamaraDeLiquidacoes { get; set; } = string.Empty;

    [JsonProperty("privatedocs")]
    public string PrivateDocs { get; set; } = string.Empty;

    [JsonProperty("nomeserie")]
    public string NomeSerie { get; set; } = string.Empty;

    [JsonProperty("codigocetip")]
    public string CodigoCetip { get; set; } = string.Empty;

    [JsonProperty("numeroemissao")]
    public string NumeroEmissao { get; set; } = string.Empty;

    [JsonProperty("tipoemissao")]
    public string TipoEmissao { get; set; } = string.Empty;

    [JsonProperty("emissor")]
    public string Emissor { get; set; } = string.Empty;

    [JsonProperty("agentefiduciario")]
    public string AgenteFiduciario { get; set; } = string.Empty;

    [JsonProperty("numeroserie")]
    public string NumeroSerie { get; set; } = string.Empty;

    [JsonProperty("valorglobalemissao")]
    public string ValorGlobalEmissao { get; set; } = string.Empty;

    [JsonProperty("dataemissao")]
    public string DataEmissao { get; set; } = string.Empty;

    [JsonProperty("datavencimento")]
    public string DataVencimento { get; set; } = string.Empty;

    [JsonProperty("dataliquidacao")]
    public string DataLiquidacao { get; set; } = string.Empty;

    [JsonProperty("indicecorrecao")]
    public string IndiceCorrecao { get; set; } = string.Empty;

    [JsonProperty("percentualindice")]
    public string PercentualIndice { get; set; } = string.Empty;

    [JsonProperty("spread")]
    public string Spread { get; set; } = string.Empty;

    [JsonProperty("remuneracao")]
    public string Remuneracao { get; set; } = string.Empty;

    [JsonProperty("codigoisin")]
    public string CodigoIsin { get; set; } = string.Empty;

    [JsonProperty("periodopagamentoamort")]
    public string PeriodoPagamentoAmort { get; set; } = string.Empty;

    [JsonProperty("periodopagamentojuros")]
    public string PeriodoPagamentoJuros { get; set; } = string.Empty;

    [JsonProperty("quantidade")]
    public string Quantidade { get; set; } = string.Empty;

    [JsonProperty("puemissao")]
    public string PuEmissao { get; set; } = string.Empty;

    [JsonProperty("serieid")]
    public string SerieId { get; set; } = string.Empty;

    [JsonProperty("nomefantasia")]
    public string NomeFantasia { get; set; } = string.Empty;

    [JsonProperty("issimulada")]
    public string IsSimulada { get; set; } = string.Empty;

    [JsonProperty("isativo")]
    public string IsAtivo { get; set; } = string.Empty;

    [JsonProperty("isregimefiduciario")]
    public string IsRegimeFiduciario { get; set; } = string.Empty;

    [JsonProperty("tipodeoferta")]
    public string TipoDeOferta { get; set; } = string.Empty;

    [JsonProperty("nomeemissao")]
    public string NomeEmissao { get; set; } = string.Empty;

    [JsonProperty("cedentes")]
    public string Cedentes { get; set; } = string.Empty;

    [JsonProperty("cedentesdocumentos")]
    public string CedentesDocumentos { get; set; } = string.Empty;

    [JsonProperty("devedores")]
    public string Devedores { get; set; } = string.Empty;

    [JsonProperty("tiposuboordinacao")]
    public string TipoSuboordinacao { get; set; } = string.Empty;

    [JsonProperty("escriturador")]
    public string Escriturador { get; set; } = string.Empty;

    [JsonProperty("coordenadorlider")]
    public string CoordenadorLider { get; set; } = string.Empty;

    [JsonProperty("numerodecotas")]
    public string NumeroDeCotas { get; set; } = string.Empty;

    //[JsonProperty("documents")]
    //public List<Document> Documents { get; set; } = [];

    [JsonProperty("medicaointegralizacaocotas")]
    public string MedicaoIntegralizacaoCotas { get; set; } = string.Empty;

    [JsonProperty("cct_slug")]
    public string CctSlug { get; set; } = string.Empty;


    public List<Pu> PuData { get; set; } = [];


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

    }

  }

}

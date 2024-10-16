using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Filadelfia.Models
{
    public class Emissao
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }


    public class Meta
    {
        [JsonProperty("serieid")]
        public string SerieId { get; set; }         

        [JsonProperty("nomefantasia")]
        public string NomeFantasia { get; set; }   
        [JsonProperty("codigoin")]
        public string CodigoISIN { get; set; }        

        [JsonProperty("codigocetip")]
        public string CodigoCETIP { get; set; }      

        [JsonProperty("dataultimopagamento")]
        public string DataUltimoPagamento { get; set; }  
        public string DataLiquidacao { get; set; } 

        [JsonProperty("jurosultimopagamento")]
        public string JurosUltimoPagamento { get; set; }   

        [JsonProperty("dataproximopagamento")]
        public string DataProximoPagamento { get; set; }   

        [JsonProperty("jurosproximopagamento")]
        public string JurosProximoPagamento { get; set; }  

        [JsonProperty("tipoemissao")]
        public string TipoEmissao { get; set; }      

        [JsonProperty("numeroemissao")]
        public string NumeroEmissao { get; set; }     

        [JsonProperty("tiposubordinacao")]
        public string TipoSuboordinacao { get; set; }  

        [JsonProperty("agentefiduciario")]
        public string AgenciaFiduciaria { get; set; }  

        [JsonProperty("numeroserie")]
        public string NumeroSerie { get; set; }      

        [JsonProperty("dataemissao")]
        public string DataEmissao { get; set; }      

        [JsonProperty("datavencimento")]
        public string DataVencimento { get; set; }    

        [JsonProperty("quantidade")]
        public string Quantidade { get; set; }       

        [JsonProperty("puemissao")]
        public string PUEmissao { get; set; } 

        [JsonProperty("valorglobalemissao")]
        public string ValorGlobalEmissao { get; set; } 

        [JsonProperty("periodopagamentojuros")]
        public string PeriodoPagamentoJuros { get; set; }   

        [JsonProperty("periodopagamentoamort")]
        public string PeriodoPagamentoAmort { get; set; }   

        [JsonProperty("remuneracao")]
        public string Remuneracao { get; set; }      

        [JsonProperty("tipodeoferta")]
        public string TipoDeOferta { get; set; }      

        [JsonProperty("medicaointegralizacaocotas")]
        public string MedicaoIntegralizacaoCotas { get; set; }  

        [JsonProperty("cedentes")]
        public string Cedentes { get; set; }         

        [JsonProperty("escriturador")]
        public string Escriturador { get; set; }     

        [JsonProperty("coordenadorlider")]
        public string CoordenadorLider { get; set; }  

        [JsonProperty("naturezalastro")]
        public string NaturezaLastro { get; set; }   

        public string AmortUltimoPagamento { get; set; }

        public string AmexUltimoPagamento { get; set; }

        public string AmexProximoPagamento { get; set; }

        public string Devedores { get; set; }

        public string CedentesDocumentos { get; set; }

        public string AmortProximoPagamento { get; set; }

        public string DurationEmAnos { get; set; }
        public string Ratings { get; set; }

        public string CamaraDeLiquidacoes { get; set; }

        public string NomeSerie { get; set; }
        public string Emissor { get; set; }

        public string AgenteFiduciario { get; set; }

        public string AgentIndiceeFiduciario { get; set; }

        public string IndiceCorrecao { get; set; }

        public string PrivateDocs { get; set; }

        public string PercentualIndice { get; set; }

        public string NumeroDeCotas { get; set; }

        public string Documents { get; set; }

        public string IsSimulada { get; set; }

        public string IsAtivo { get; set; }

        public string IsRegimeFiduciario { get; set; }

        public string NomeEmissao { get; set; }

        public string Spread { get; set; }

    }




}

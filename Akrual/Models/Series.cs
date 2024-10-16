using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Akrun.Models
{
    public class Series
    {
        public string DataUltimoPagamento { get; set; } 
        public string JurosUltimoPagamento { get; set; } 
        public string AmortUltimoPagamento { get; set; }
        public string AmexUltimoPagamento { get; set; }
        public string DataProximoPagamento { get; set; }
        public string JurosProximoPagamento { get; set; }
        public string AmortProximoPagamento { get; set; }
        public string AmexProximoPagamento { get; set; }
        public string DurationEmAnos { get; set; } 
        public List<string> Ratings { get; set; } = new List<string>(); 
        public string NaturezaLastro { get; set; }
        public List<string> CamaraDeLiquidacoes { get; set; } = new List<string>(); 
        public string PrivateDocs { get; set; } 
        public string NomeSerie { get; set; }
        public string CodigoCETIP { get; set; } 
        public string NumeroEmissao { get; set; }
        public string TipoEmissao { get; set; }
        public string Emissor { get; set; } 
        public string AgenteFiduciario { get; set; } 
        public string NumeroSerie { get; set; }
        public string ValorGlobalEmissao { get; set; }
        public string DataEmissao { get; set; }
        public string DataVencimento { get; set; }
        public string DataLiquidacao { get; set; } 
        public string IndiceCorrecao { get; set; }
        public string PercentualIndice { get; set; }
        public string Spread { get; set; }
        public string Remuneracao { get; set; }
        public string CodigoISIN { get; set; } 
        public string PeriodoPagamentoAmort { get; set; }
        public string PeriodoPagamentoJuros { get; set; }
        public string Quantidade { get; set; }
        public string PUEmissao { get; set; }
        public string SerieId { get; set; }
        public string NomeFantasia { get; set; }
        public string IsSimulada { get; set; }
        public string IsAtivo { get; set; }
        public string IsRegimeFiduciario { get; set; }
        public string TipoDeOferta { get; set; }
        public string NomeEmissao { get; set; } 
        public string Cedentes { get; set; } 
        public string CedentesDocumentos { get; set; } 
        public string Devedores { get; set; } 
        public string TipoSuboordinacao { get; set; } 
        public string Escriturador { get; set; } 
        public string CoordenadorLider { get; set; } 
        public string NumeroDeCotas { get; set; }
        public string Documents { get; set; } 
        public string MedicaoIntegralizacaoCotas { get; set; }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Filadelfia.Models
{
    public class Emissao
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public string SerieId { get; set; }          // "serieid"
        public string NomeFantasia { get; set; }     // "nomefantasia"
        public string CodigoIn { get; set; }         // "codigoin"
        public string CodigoCetip { get; set; }      // "codigocetip"
        public string DataUltimoPagamento { get; set; } // "dataultimopagamento"
        public string JurosUltimoPagamento { get; set; } // "jurosultimopagamento"
        public string DataProximoPagamento { get; set; } // "dataproximopagamento"
        public string JurosProximoPagamento { get; set; } // "jurosproximopagamento"
        public string TipoEmissao { get; set; }      // "tipoemissao"
        public string NumeroEmissao { get; set; }    // "numeroemissao"
        public string TipoSubordinacao { get; set; } // "tiposubordinacao"
        public string AgenciaFiduciaria { get; set; } // "agentefiduciario"
        public string NumeroSerie { get; set; }      // "numeroserie"
        public string DataEmissao { get; set; }      // "dataemissao"
        public string DataVencimento { get; set; }    // "datavencimento"
        public string Quantidade { get; set; }       // "quantidade"
        public string PuEmissao { get; set; }        // "puemissao"
        public string ValorGlobalEmissao { get; set; } // "valorglobalemissao"
        public string PeriodoPagamentoJuros { get; set; } // "periodopagamentojuros"
        public string PeriodoPagamentoAmort { get; set; } // "periodopagamentoamort"
        public string Remuneracao { get; set; }      // "remuneracao"
        public string TipoDeOferta { get; set; }     // "tipodeoferta"
        public string MedicaoIntegralizacaoCotas { get; set; } // "medicaointegralizacaocotas"
        public string Cedentes { get; set; }         // "cedentes"
        public string Escriturador { get; set; }     // "escriturador"
        public string CoordenadorLider { get; set; } // "coordenadorlider"
        public string NaturezaLastro { get; set; }   // "naturezalastro"
    }

}

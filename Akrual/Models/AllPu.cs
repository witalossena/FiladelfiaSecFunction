using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Akrual.Models
{
  public class AllPus
  {
    public DateTime Data { get; set; }
    public double PU { get; set; }
    public string NomeSerie { get; set; }
    public string NomeFantasia { get; set; }
    public int NumeroSerie { get; set; }
    public string TipoEmissao { get; set; }
    public int NumeroEmissao { get; set; }
    public string CodigoISIN { get; set; }
    public string CodigoCETIP { get; set; }
    public string Remuneracao { get; set; }
    public bool IsSimulada { get; set; }
    public bool IsAtivo { get; set; }
    public string SerieId { get; set; }
  }
}

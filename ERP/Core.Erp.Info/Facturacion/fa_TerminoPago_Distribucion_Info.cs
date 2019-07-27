using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
  public class fa_TerminoPago_Distribucion_Info
    {
        public string IdTerminoPago { get; set; }
        public int Secuencia { get; set; }
        public int Num_Dias_Vcto { get; set; }
        public double Por_distribucion { get; set; }
    }
}

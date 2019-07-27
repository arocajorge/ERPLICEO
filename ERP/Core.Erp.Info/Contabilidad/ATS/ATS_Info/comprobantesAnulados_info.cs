using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad.ATS.ATS_Info
{
   public class comprobantesAnulados_info
    {
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public int Secuencia { get; set; }
        public string tipoComprobante { get; set; }
        public string Establecimiento { get; set; }
        public string puntoEmision { get; set; }
        public string secuencialInicio { get; set; }
        public string secuencialFin { get; set; }
        public string Autorizacion { get; set; }
    }
}

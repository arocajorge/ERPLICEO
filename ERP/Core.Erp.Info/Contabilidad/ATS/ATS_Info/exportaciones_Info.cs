using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad.ATS.ATS_Info
{
   public class exportaciones_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public int Secuencia { get; set; }
        public string tpIdClienteEx { get; set; }
        public string idClienteEx { get; set; }
        public string parteRel { get; set; }
        public string tipoRegi { get; set; }
        public string paisEfecPagoGen { get; set; }
        public string paisEfecExp { get; set; }
        public string exportacionDe { get; set; }
        public string tipoComprobante { get; set; }
        public Nullable<System.DateTime> fechaEmbarque { get; set; }
        public Nullable<decimal> valorFOB { get; set; }
        public Nullable<decimal> valorFOBComprobante { get; set; }
        public string establecimiento { get; set; }
        public string puntoEmision { get; set; }
        public string secuencial { get; set; }
        public string autorizacion { get; set; }
        public Nullable<System.DateTime> fechaEmision { get; set; }
        public string denoExpCli { get; set; }
    }
}

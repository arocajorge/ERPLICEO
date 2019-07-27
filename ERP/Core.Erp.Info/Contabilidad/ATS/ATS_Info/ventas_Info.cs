using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Contabilidad.ATS.ATS_Info
{
   public class ventas_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public int Secuencia { get; set; }
        public string tpIdCliente { get; set; }
        public string idCliente { get; set; }
        public string parteRelVtas { get; set; }
        public string tipoCliente { get; set; }
        public string DenoCli { get; set; }
        public string tipoComprobante { get; set; }
        public string tipoEmision { get; set; }
        public int numeroComprobantes { get; set; }
        public decimal baseNoGraIva { get; set; }
        public decimal baseImponible { get; set; }
        public decimal baseImpGrav { get; set; }
        public decimal montoIva { get; set; }
        public decimal montoIce { get; set; }
        public decimal valorRetIva { get; set; }
        public decimal valorRetRenta { get; set; }
        public string formaPago { get; set; }
        public string codEstab { get; set; }
        public decimal ventasEstab { get; set; }
        public decimal ivaComp { get; set; }
    }
}

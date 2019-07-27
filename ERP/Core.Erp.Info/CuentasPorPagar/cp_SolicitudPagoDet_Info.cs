using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_SolicitudPagoDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdSolicitud { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_cxp { get; set; }
        public int IdTipoCbte_cxp { get; set; }
        public decimal IdCbteCble_cxp { get; set; }
        public string TipoDocumento { get; set; }
        public double ValorAPagar { get; set; }

        //campos que no existen en la tabla

        public DateTime Fecha { get; set; }
        public cp_proveedor_Info info_proveedor { get; set; }
    }
}

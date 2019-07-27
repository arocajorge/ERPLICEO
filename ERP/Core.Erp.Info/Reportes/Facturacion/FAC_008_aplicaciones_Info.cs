using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_008_aplicaciones_Info
    {
        public int IdEmpresa_nt { get; set; }
        public int IdSucursal_nt { get; set; }
        public int IdBodega_nt { get; set; }
        public decimal IdNota_nt { get; set; }
        public int secuencia { get; set; }
        public decimal IdCbteVta_fac_nd_doc_mod { get; set; }
        public double Valor_Aplicado { get; set; }
        public string NumFactura { get; set; }
        public Nullable<System.DateTime> vt_fecha { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_factura_det_x_in_Ing_Egr_Inven_det_Info
    {
        public int IdEmpresa_fa { get; set; }
        public int IdSucursal_fa { get; set; }
        public int IdBodega_fa { get; set; }
        public decimal IdCbteVta_fa { get; set; }
        public int Secuencia_fa { get; set; }
        public int IdEmpresa_eg { get; set; }
        public int IdSucursal_eg { get; set; }
        public int IdMovi_inven_tipo_eg { get; set; }
        public decimal IdNumMovi_eg { get; set; }
        public int Secuencia_eg { get; set; }
        public string Observacion { get; set; }
    }
}

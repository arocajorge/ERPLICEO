using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
    public class INV_015_Info
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
        public string pr_descripcion { get; set; }
        public double CantidadFac { get; set; }
        public double vt_PrecioFinal { get; set; }
        public double TotalFac { get; set; }
        public Nullable<double> CantidadInv { get; set; }
        public double CostoUni { get; set; }
        public Nullable<double> TotalCosto { get; set; }
        public double Utilidad { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public int IdLinea { get; set; }
        public int IdGrupo { get; set; }
        public int IdSubGrupo { get; set; }
        public string ca_Categoria { get; set; }
        public string nom_linea { get; set; }
        public string nom_grupo { get; set; }
        public string nom_subgrupo { get; set; }
        public string Su_Descripcion { get; set; }
        public string vt_NumFactura { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public decimal IdProducto { get; set; }
    }
}

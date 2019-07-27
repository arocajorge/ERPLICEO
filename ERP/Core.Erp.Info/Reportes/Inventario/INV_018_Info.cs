using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
    public class INV_018_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAjuste { get; set; }
        public int Secuencia { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public Nullable<int> IdMovi_inven_tipo_ing { get; set; }
        public Nullable<int> IdMovi_inven_tipo_egr { get; set; }
        public Nullable<decimal> IdNumMovi_ing { get; set; }
        public Nullable<decimal> IdNumMovi_egr { get; set; }
        public string IdCatalogo_Estado { get; set; }
        public bool Estado { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string Su_Descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public string tm_descripcion_ing { get; set; }
        public string tm_descripcion_egr { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public double StockSistema { get; set; }
        public double StockFisico { get; set; }
        public double Ajuste { get; set; }
        public double Costo { get; set; }
        public string pr_descripcion { get; set; }
        public string NomUnidadMedida { get; set; }
        public string ca_Categoria { get; set; }
        public string nom_linea { get; set; }
        public double Total { get; set; }
        public string NombreEstado { get; set; }
    }
}

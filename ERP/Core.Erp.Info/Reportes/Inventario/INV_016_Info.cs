using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
    public class INV_016_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdProducto { get; set; }
        public string IdUsuario { get; set; }
        public double SaldoInicial { get; set; }
        public double CantidadIngresada { get; set; }
        public double CantidadVendida { get; set; }
        public double CostoPromedio { get; set; }
        public double CostoTotal { get; set; }
        public double Stock { get; set; }
        public double PrecioVenta { get; set; }
        public string Su_Descripcion { get; set; }
        public string pr_descripcion { get; set; }
        public string ca_Categoria { get; set; }
        public string nom_linea { get; set; }
        public string nom_grupo { get; set; }
        public string nom_subgrupo { get; set; }
    }
}

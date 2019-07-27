using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
   public class INV_004_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public string NomProducto { get; set; }
        public string NomPresentacion { get; set; }
        public string NomSucursal { get; set; }
        public string NomBodega { get; set; }
        public string NomTipo { get; set; }
        public string NomMarca { get; set; }
        public Nullable<int> IdMarca { get; set; }
        public double Stock_minimo { get; set; }
        public double StockActual { get; set; }
    }
}

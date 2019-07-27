using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
   public class INV_010_Info
    {
        public int IdEmpresa { get; set; }
        public string IdUsuario { get; set; }
        public int IdAnio { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public string IdCategoria { get; set; }
        public int IdLinea { get; set; }
        public int IdGrupo { get; set; }
        public int IdSubGrupo { get; set; }
        public int IdMarca { get; set; }
        public double Enero { get; set; }
        public double Febrero { get; set; }
        public double Marzo { get; set; }
        public double Abril { get; set; }
        public double Mayo { get; set; }
        public double Junio { get; set; }
        public double Julio { get; set; }
        public double Agosto { get; set; }
        public double Septiembre { get; set; }
        public double Octubre { get; set; }
        public double Noviembre { get; set; }
        public double Diciembre { get; set; }
        public double Total { get; set; }
        public double StockActual { get; set; }
        public string ca_Categoria { get; set; }
        public string nom_linea { get; set; }
        public string nom_grupo { get; set; }
        public string nom_subgrupo { get; set; }
        public string NomMarca { get; set; }
        public string IdPresentacion { get; set; }
        public string nom_presentacion { get; set; }
    }
}

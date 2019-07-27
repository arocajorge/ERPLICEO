using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
    public class INV_003_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public double Stock { get; set; }
        public double Costo_promedio { get; set; }
        public double Costo_total { get; set; }
        public string Su_Descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public string pr_codigo { get; set; }
        public string pr_descripcion { get; set; }
        public string lote_num_lote { get; set; }
        public Nullable<System.DateTime> lote_fecha_vcto { get; set; }
        public string IdCategoria { get; set; }
        public string ca_Categoria { get; set; }
        public int? IdLinea { get; set; }
        public string nom_linea { get; set; }
        public int? IdGrupo { get; set; }
        public string nom_grupo { get; set; }
        public int? IdSubgrupo { get; set; }
        public string nom_subgrupo { get; set; }
        public string IdPresentacion { get; set; }
        public string nom_presentacion { get; set; }
        public int IdMarca { get; set; }
        public string NomMarca { get; set; }
    }
}

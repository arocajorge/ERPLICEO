using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Inventario
{
  public  class INV_002_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdMovi_inven_tipo { get; set; }
        public decimal IdNumMovi { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public string pr_codigo { get; set; }
        public string Su_Descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public string IdUnidadMedida_sinConversion { get; set; }
        public string Descripcion { get; set; }
        public Nullable<double> dm_cantidad_sinConversion { get; set; }
        public double mv_costo_sinConversion { get; set; }
        public string cm_observacion { get; set; }
        public string CodMoviInven { get; set; }
        public System.DateTime cm_fecha { get; set; }
        public string Estado { get; set; }
        public int IdMotivo_Inv { get; set; }
        public string Desc_mov_inv { get; set; }
        public string lote_num_lote { get; set; }
        public Nullable<System.DateTime> lote_fecha_vcto { get; set; }
        public string nom_presentacion { get; set; }
        public string signo { get; set; }
        public string tm_descripcion { get; set; }
        public string NomUsuario { get; set; }
        public Nullable<decimal> IdResponsable { get; set; }
        public string pe_nombreCompleto { get; set; }
    }
}

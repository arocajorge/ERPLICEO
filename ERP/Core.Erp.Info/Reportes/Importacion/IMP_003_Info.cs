using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Importacion
{
    public class IMP_003_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdOrdenCompra_ext { get; set; }
        public int Secuencia { get; set; }
        public Nullable<decimal> IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public string IdPresentacion { get; set; }
        public string nom_presentacion { get; set; }
        public int IdMarca { get; set; }
        public string NomMarca { get; set; }
        public decimal IdProveedor { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string IdPais_embarque { get; set; }
        public string NomPais { get; set; }
        public string oe_observacion { get; set; }
        public double od_cantidad_recepcion { get; set; }
        public double od_costo_final { get; set; }
        public double od_total_fob { get; set; }
        public double od_costo_bodega { get; set; }
        public double od_costo_total { get; set; }
        public double od_por_descuento { get; set; }
        public double od_costo { get; set; }
        public System.DateTime oe_fecha { get; set; }
    }
}

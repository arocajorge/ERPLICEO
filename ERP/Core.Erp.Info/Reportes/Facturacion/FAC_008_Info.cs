using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Facturacion
{
   public class FAC_008_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public string nom_presentacion { get; set; }
        public string lote_num_lote { get; set; }
        public Nullable<System.DateTime> lote_fecha_vcto { get; set; }
        public double sc_cantidad { get; set; }
        public double sc_Precio { get; set; }
        public double sc_descUni { get; set; }
        public double sc_PordescUni { get; set; }
        public double sc_precioFinal { get; set; }
        public double DescTotal { get; set; }
        public double sc_subtotal { get; set; }
        public double sc_subtotalIVA { get; set; }
        public double sc_subtotal0 { get; set; }
        public double sc_iva { get; set; }
        public double vt_por_iva { get; set; }
        public string Nombres { get; set; }
        public string Su_Descripcion { get; set; }
        public string NumNota_Impresa { get; set; }
        public System.DateTime no_fecha { get; set; }
        public System.DateTime no_fecha_venc { get; set; }
        public string CreDeb { get; set; }
        public string sc_observacion { get; set; }
        public double sc_total { get; set; }
        public string No_Descripcion { get; set; }
        public string nomReporte { get; set; }
    }
}

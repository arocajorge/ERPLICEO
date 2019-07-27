using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
   public class fa_guia_remision_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdGuiaRemision { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public double gi_cantidad { get; set; }
        public string gi_detallexItems { get; set; }
        public double gi_precio { get; set; }
        public double gi_por_desc { get; set; }
        public double gi_descuentoUni { get; set; }
        public double gi_PrecioFinal { get; set; }
        public double gi_Subtotal { get; set; }
        public string IdCod_Impuesto { get; set; }
        public double gi_por_iva { get; set; }
        public double gi_Iva { get; set; }
        public double gi_Total { get; set; }

        #region Campos que no existen en la tabla        
        public string pr_descripcion { get; set; }

        #endregion Campos que no estan en la tabla
        public Nullable<decimal> IdCbteVta { get; set; }
        public Nullable<int> Secuencia_fact { get; set; }        
        public Nullable<int> IdEmpresa_fact { get; set; }
        public Nullable<int> IdSucursal_fact { get; set; }
        public Nullable<int> IdBodega_fact { get; set; }
        //public string ca_Categoria { get; set; }
        //public Nullable<System.DateTime> lote_fecha_fab { get; set; }
        //public string lote_num_lote { get; set; }
        //public Nullable<System.DateTime> lote_fecha_vcto { get; set; }
        //public string nom_presentacion { get; set; }
    }
}

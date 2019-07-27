using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Importacion
{
   public class imp_ordencompra_ext_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdOrdenCompra_ext { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = "Seleccione producto")]
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        [Required(ErrorMessage = "Ingrese cantida")]
        public double od_cantidad { get; set; }
        [Required(ErrorMessage = "Ingrese precio unitario")]
        public double od_costo { get; set; }
        public double od_por_descuento { get; set; }
        public double od_descuento { get; set; }
        public double od_costo_final { get; set; }
        public double od_subtotal { get; set; }
        public double od_cantidad_recepcion { get; set; }
        public double od_costo_convertido { get; set; }
        public double? od_total_fob { get; set; }
        public double od_factor_costo { get; set; }
        public double od_costo_bodega { get; set; }
        public double od_costo_total { get; set; }

        #region campos de vistas
        public string pr_descripcion { get; set; }
        public Nullable<System.DateTime> lote_fecha_vcto { get; set; }
        public string lote_num_lote { get; set; }
        #endregion
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Facturacion
{
    public class fa_proforma_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdProforma { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = "El campo cantidad es obligatorio")]
        [Range(1,int.MaxValue,ErrorMessage ="El campo producto es obligatorio")]
        public decimal IdProducto { get; set; }
        [Required(ErrorMessage = "El campo cantidad es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "El campo cantidad es obligatorio")]
        public double pd_cantidad { get; set; }
        [Required(ErrorMessage = "El campo precio es obligatorio")]
        public double pd_precio { get; set; }
        public double pd_por_descuento_uni { get; set; }
        public double pd_descuento_uni { get; set; }
        public double pd_precio_final { get; set; }
        public double pd_subtotal { get; set; }
        public string IdCod_Impuesto { get; set; }
        public double pd_por_iva { get; set; }
        public double pd_iva { get; set; }
        public double pd_total { get; set; }
        public bool anulado { get; set; }

        #region Campos que no existen en la tabla
        public string pr_descripcion { get; set; }
        public DateTime? lote_fecha_vcto { get; set; }
        public string lote_num_lote { get; set; }
        public string nom_presentacion { get; set; }
        #endregion
    }
}

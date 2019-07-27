using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Ing_Egr_Inven_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdMovi_inven_tipo { get; set; }
        public decimal IdNumMovi { get; set; }
        public int Secuencia { get; set; }
        public int IdBodega { get; set; }
        [Required(ErrorMessage = ("El campo producto es obligatorio"))]
        public decimal IdProducto { get; set; }
        [Required(ErrorMessage = ("El campo cantidad es obligatorio"))]
        public double dm_cantidad { get; set; }
        public string dm_observacion { get; set; }
        public double mv_costo { get; set; }
        public string IdUnidadMedida { get; set; }
        public Nullable<int> IdEmpresa_oc { get; set; }
        public Nullable<int> IdSucursal_oc { get; set; }
        public Nullable<decimal> IdOrdenCompra { get; set; }
        public Nullable<int> Secuencia_oc { get; set; }
        public Nullable<int> IdEmpresa_inv { get; set; }
        public Nullable<int> IdSucursal_inv { get; set; }
        public Nullable<int> IdBodega_inv { get; set; }
        public Nullable<int> IdMovi_inven_tipo_inv { get; set; }
        public Nullable<decimal> IdNumMovi_inv { get; set; }
        public Nullable<int> secuencia_inv { get; set; }
        public double dm_cantidad_sinConversion { get; set; }
        public string IdUnidadMedida_sinConversion { get; set; }
        //[Required(ErrorMessage = ("El campo costo es obligatorio"))]
        public double mv_costo_sinConversion { get; set; }
        public Nullable<int> IdMotivo_Inv { get; set; }


        #region Campos que no existen en la tabla
        public string pr_descripcion { get; set; }
        public DateTime? lote_fecha_vcto { get; set; }
        public string lote_num_lote { get; set; }
        public string nom_presentacion { get; set; }

        public double Saldo { get; set; }
        #endregion

        #region Campos de factura        
        public fa_factura_det_x_in_Ing_Egr_Inven_det_Info RelacionDetalleFactura { get; set; }
        public bool se_distribuye { get; set; }
        public string tp_ManejaInven { get; set; }
        public double CantidadAnterior { get; set; }
        #endregion
    }
}

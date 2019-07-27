using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Importacion
{
  public  class imp_ordencompra_ext_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdOrdenCompra_ext { get; set; }
        [Required(ErrorMessage = "Seleccione el proveedor")]
        public decimal IdProveedor { get; set; }
        public string IdPais_origen { get; set; }
        [Required(ErrorMessage = "Seleccione páis embarque")]
        public string IdPais_embarque { get; set; }
        public string IdCiudad_destino { get; set; }
        public int IdCatalogo_via { get; set; }
        public int IdCatalogo_forma_pago { get; set; }
        public System.DateTime oe_fecha { get; set; }
        public Nullable<System.DateTime> oe_fecha_llegada_est { get; set; }
        public Nullable<System.DateTime> oe_fecha_embarque_est { get; set; }
        public Nullable<System.DateTime> oe_fecha_desaduanizacion_est { get; set; }
        public string IdCtaCble_importacion { get; set; }
        public string oe_observacion { get; set; }
        public string oe_codigo { get; set; }
        public bool estado { get; set; }
        public string IdUsuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public string IdUsuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        public string IdUsuario_anulacion { get; set; }
        public Nullable<System.DateTime> fecha_anulacion { get; set; }
        public Nullable<decimal> IdLiquidacion { get; set; }
        public Nullable<System.DateTime> oe_fecha_llegada { get; set; }
        public Nullable<System.DateTime> oe_fecha_embarque { get; set; }
        public Nullable<System.DateTime> oe_fecha_desaduanizacion { get; set; }
        public Nullable<int> IdMoneda_origen { get; set; }
        public Nullable<int> IdMoneda_destino { get; set; }
        public Nullable<int> IdEmpresa_inv { get; set; }
        public Nullable<int> IdSucursal_inv { get; set; }
        public Nullable<int> IdBodega_inv { get; set; }

        public Nullable<int> IdMovi_inven_tipo_inv { get; set; }
        public Nullable<decimal> IdNumMovi_inv { get; set; }
        public Nullable<int> IdEmpresa_ct { get; set; }
        public Nullable<int> IdTipoCbte_ct { get; set; }
        public Nullable<decimal> IdCbteCble_ct { get; set; }

        public List<imp_ordencompra_ext_det_Info> lst_detalle { get; set; }
        public List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> lst_gastos_asignados { get; set; }
        public List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> lst_gastos_por_asignar { get; set; }
        public ct_cbtecble_Info info_comrobante { get; set; }
        public List<ct_cbtecble_det_Info> lst_comprobante { get; set; }


        #region campos de vistas
        public decimal IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public Nullable<double> cantidad_x_recibir { get; set; }
        public Nullable<double> cantidad_global { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        #endregion

    }
}

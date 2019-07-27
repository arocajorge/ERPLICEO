using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Importacion
{
   public class imp_liquidacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdLiquidacion { get; set; }
        public decimal IdOrdenCompra_ext { get; set; }
        public string li_num_documento { get; set; }
        public string li_codigo { get; set; }
        public string li_num_DAU { get; set; }
        public System.DateTime li_fecha { get; set; }
        public string li_observacion { get; set; }
        public double li_factor_conversion { get; set; }
        public double li_total_fob { get; set; }
        public double li_total_gastos { get; set; }
        public double li_total_bodega { get; set; }
        public double li_factor_costo { get; set; }
        public bool estado { get; set; }
        public bool cerrado { get; set; }
        public Nullable<int> IdEmpresa_inv { get; set; }
        public Nullable<int> IdSucursal_inv { get; set; }
        public Nullable<int> IdMovi_inven_tipo_inv { get; set; }
        public Nullable<decimal> IdNumMovi_inv { get; set; }
        public Nullable<int> IdEmpresa_ct { get; set; }
        public Nullable<int> IdTipoCbte_ct { get; set; }
        public Nullable<decimal> IdCbteCble_ct { get; set; }
        public Nullable<int> IdBodega_inv { get; set; }
        public System.DateTime oe_fecha { get; set; }
        public string IdUsuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public string IdUsuario_modificacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacion { get; set; }
        public string IdUsuario_anulacion { get; set; }
        public Nullable<System.DateTime> fecha_anulacion { get; set; }






        public Nullable<System.DateTime> oe_fecha_llegada_est { get; set; }
        public Nullable<System.DateTime> oe_fecha_embarque_est { get; set; }
        public Nullable<System.DateTime> oe_fecha_desaduanizacion_est { get; set; }
        public string IdCtaCble_importacion { get; set; }
        public string oe_observacion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }



        public List<imp_ordencompra_ext_det_Info> lst_detalle { get; set; }
        public List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> lst_gastos_asignados { get; set; }
        public ct_cbtecble_Info info_comrobante { get; set; }
        public List<ct_cbtecble_det_Info> lst_comprobante { get; set; }
    }
}

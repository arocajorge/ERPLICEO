using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_nota_DebCre_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble_Nota { get; set; }
        public int IdTipoCbte_Nota { get; set; }
        public string DebCre { get; set; }
        public string IdTipoNota { get; set; }
        [Required(ErrorMessage = "El campo proveedor es obligatorio")]
        [Range(1,int.MaxValue,ErrorMessage = "El campo proveedor es obligatorio")]
        public decimal IdProveedor { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public System.DateTime cn_fecha { get; set; }
        public Nullable<System.DateTime> Fecha_contable { get; set; }
        public System.DateTime cn_Fecha_vcto { get; set; }
        public string cn_serie1 { get; set; }
        public string cn_serie2 { get; set; }
        public string cn_Nota { get; set; }
        [Required(ErrorMessage ="El campo observación es obligatorio")]
        public string cn_observacion { get; set; }
        [Required(ErrorMessage = "El campo subtotal IVA es obligatorio")]
        public double cn_subtotal_iva { get; set; }
        [Required(ErrorMessage = "El campo subtotal 0 es obligatorio")]
        public double cn_subtotal_siniva { get; set; }
        public double cn_baseImponible { get; set; }
        public double cn_Por_iva { get; set; }
        public double cn_valoriva { get; set; }
        public double cn_Ice_base { get; set; }
        public double cn_Ice_por { get; set; }
        public double cn_Ice_valor { get; set; }
        public double cn_Serv_por { get; set; }
        public double cn_Serv_valor { get; set; }
        public decimal cn_BaseSeguro { get; set; }
        public decimal cn_total { get; set; }
        public string cn_vaCoa { get; set; }
        public string cn_Autorizacion { get; set; }
        public string cn_num_doc_modificado { get; set; }
        public Nullable<int> IdCod_ICE { get; set; }
        public string IdTipoServicio { get; set; }
        public Nullable<int> IdIden_credito { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string cn_tipoLocacion { get; set; }
        public string IdCentroCosto { get; set; }
        public string PagoLocExt { get; set; }
        public string PaisPago { get; set; }
        public string ConvenioTributacion { get; set; }
        public string PagoSujetoRetencion { get; set; }
        public Nullable<System.DateTime> fecha_autorizacion { get; set; }
        public string cod_nota { get; set; }
        public string Nombre_proveedor { get; set; }
        public cp_proveedor_Info info_proveedor { get; set; }
        public ct_cbtecble_Info info_comrobante { get; set; }
        public List<cp_orden_pago_cancelaciones_Info> lst_det_canc_op { get; set; }
        public cp_nota_DebCre_Info()
        {
            info_proveedor = new cp_proveedor_Info();
            info_comrobante = new ct_cbtecble_Info();
        }

        // campos que no existen en la tabla

        public bool ConvenioTributacion_bool { get; set; }
        public bool PagoSujetoRetencion_bool { get; set; }
        public List<ct_cbtecble_det_Info> lst_detalle_ct { get; set; }

    }
}

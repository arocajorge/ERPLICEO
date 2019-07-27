using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Contabilidad;
namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_orden_giro_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public string IdOrden_giro_Tipo { get; set; }
        [Required(ErrorMessage = "El proveedor es obligatorio")]
        public decimal IdProveedor { get; set; }
        public System.DateTime co_fechaOg { get; set; }
        [RegularExpression(@"\d{3}-\d{3}", ErrorMessage = "El formato de la serie debe ser 000-000")]
        [Required(ErrorMessage = "Serie del documento es obligatorio")]
        public string co_serie { get; set; }
        [Required(ErrorMessage = "El numero del documento es obligatorio")]
        public string co_factura { get; set; }
        public System.DateTime co_FechaFactura { get; set; }
        public Nullable<System.DateTime> co_FechaContabilizacion { get; set; }
        public System.DateTime co_FechaFactura_vct { get; set; }
        public int co_plazo { get; set; }
        [Required(ErrorMessage ="El campo observación es obligatorio")]
        public string co_observacion { get; set; }
        public double co_subtotal_iva { get; set; }
        public double co_subtotal_siniva { get; set; }
        public double co_baseImponible { get; set; }
        public double co_Por_iva { get; set; }
        public double co_valoriva { get; set; }
        public Nullable<int> IdCod_ICE { get; set; }
        public double co_total { get; set; }
        public double co_valorpagar { get; set; }
        public string co_vaCoa { get; set; }
        public Nullable<int> IdIden_credito { get; set; }
        public Nullable<int> IdCod_101 { get; set; }
        public string IdTipoServicio { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public int IdSucursal { get; set; }
        public Nullable<int> IdSucursal_cxp { get; set; }
        public string PagoLocExt { get; set; }
        public string PaisPago { get; set; }
        public string ConvenioTributacion { get; set; }
        public string PagoSujetoRetencion { get; set; }
        public Nullable<double> BseImpNoObjDeIva { get; set; }
        public Nullable<System.DateTime> fecha_autorizacion { get; set; }
        public string Num_Autorizacion { get; set; }
        public string Num_Autorizacion_Imprenta { get; set; }
        public Nullable<bool> cp_es_comprobante_electronico { get; set; }
        public string Tipodoc_a_Modificar { get; set; }
        public string estable_a_Modificar { get; set; }
        public string ptoEmi_a_Modificar { get; set; }
        public string num_docu_Modificar { get; set; }
        public string aut_doc_Modificar { get; set; }
        public Nullable<int> IdTipoMovi { get; set; }
        public ct_cbtecble_Info info_comrobante { get; set; }
        public cp_proveedor_Info info_proveedor { get; set; }
        public string Descripcion { get; set; }
        public string Tipo_Vcto { get; set; }
        public int Dias_Vencidos { get; set; }
        public double Total_Pagado { get; set; }
        public Nullable<double> Saldo_OG { get; set; }
        public int IdBodega { get; set; }
        

        #region Campos que no existen en la tabla
        public bool ConvenioTributacion_bool { get; set; }
        public bool PagoSujetoRetencion_bool { get; set; }
        public List<cp_orden_giro_det_Info> lst_det { get; set; }
        public List<cp_orden_giro_det_ing_x_oc_Info> lst_det_oc { get; set; }
        public List<cp_orden_giro_det_ing_x_os_Info> lst_det_os { get; set; }
        public string SecuencialID { get; set; }
        public string nom_tipo_Documento { get; set; }
        #endregion
        public cp_orden_giro_pagos_sri_Info info_forma_pago { get; set; }
        public cp_retencion_Info info_retencion { get; set; }
        public int TieneRetencion { get; set; }
        public cp_orden_giro_Info()

        {
            info_retencion = new cp_retencion_Info();
            info_comrobante = new ct_cbtecble_Info();
        }
    }

    public class cp_orden_giro_aprobacion_Info
    {
        public string nom_tipo_Documento { get; set; }
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public string IdOrden_giro_Tipo { get; set; }
        public decimal IdProveedor { get; set; }
        public System.DateTime co_fechaOg { get; set; }
        public string co_serie { get; set; }
        public string co_factura { get; set; }
        public System.DateTime co_FechaFactura { get; set; }
        public Nullable<System.DateTime> co_FechaContabilizacion { get; set; }
        public System.DateTime co_FechaFactura_vct { get; set; }
        public int co_plazo { get; set; }
        public string co_observacion { get; set; }
        public double co_subtotal_iva { get; set; }
        public double co_subtotal_siniva { get; set; }
        public double co_baseImponible { get; set; }
        public double co_Por_iva { get; set; }
        public double co_valoriva { get; set; }
        public Nullable<int> IdCod_ICE { get; set; }
        public double co_total { get; set; }
        public double co_valorpagar { get; set; }
        public string co_vaCoa { get; set; }
        public Nullable<int> IdIden_credito { get; set; }
        public Nullable<int> IdCod_101 { get; set; }
        public Nullable<decimal> IdTipoFlujo { get; set; }
        public string IdTipoServicio { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string PagoLocExt { get; set; }
        public string PaisPago { get; set; }
        public string ConvenioTributacion { get; set; }
        public string PagoSujetoRetencion { get; set; }
        public Nullable<double> BseImpNoObjDeIva { get; set; }
        public Nullable<System.DateTime> fecha_autorizacion { get; set; }
        public string Num_Autorizacion { get; set; }
        public string Num_Autorizacion_Imprenta { get; set; }
        public Nullable<bool> cp_es_comprobante_electronico { get; set; }
        public string Tipodoc_a_Modificar { get; set; }
        public string estable_a_Modificar { get; set; }
        public string ptoEmi_a_Modificar { get; set; }
        public string num_docu_Modificar { get; set; }
        public string aut_doc_Modificar { get; set; }
        public Nullable<int> IdTipoMovi { get; set; }
        public ct_cbtecble_Info info_comrobante { get; set; }
        public cp_proveedor_Info info_proveedor { get; set; }
        public string Descripcion { get; set; }
        public string Tipo_Vcto { get; set; }
        public int Dias_Vencidos { get; set; }
        public double Total_Pagado { get; set; }
        public Nullable<double> Saldo_OG { get; set; }
        public Nullable<int> IdBodega { get; set; }
        public cp_orden_giro_pagos_sri_Info info_forma_pago { get; set; }

        public bool seleccionado { get; set; }

        //campos que no existen en la tabla

        public bool ConvenioTributacion_bool { get; set; }
        public bool PagoSujetoRetencion_bool { get; set; }
        public List<cp_orden_giro_det_Info> lst_det { get; set; }
        public string SecuencialID { get; set; }

        public cp_retencion_Info info_retencion { get; set; }
    }
}

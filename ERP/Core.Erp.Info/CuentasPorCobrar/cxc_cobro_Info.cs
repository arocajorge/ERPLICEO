using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.CuentasPorCobrar
{
    public class cxc_cobro_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public Nullable<decimal> IdCobro_a_aplicar { get; set; }
        public string cr_Codigo { get; set; }
        public string IdCobro_tipo { get; set; }
        [Range(1,999999,ErrorMessage ="El campo cliente es obligatorio")]
        [Required(ErrorMessage = "El campo cliente es obligatorio")]
        public decimal IdCliente { get; set; }
        [Range(0.01,double.MaxValue, ErrorMessage ="El campo total cobro es obligatorio")]
        [Required(ErrorMessage ="El campo total cobro es obligatorio")]
        public double cr_TotalCobro { get; set; }
        [Required(ErrorMessage ="El campo fecha es obligatorio")]
        public System.DateTime cr_fecha { get; set; }
        public System.DateTime cr_fechaDocu { get; set; }
        public System.DateTime cr_fechaCobro { get; set; }
        public string cr_observacion { get; set; }
        public string cr_Banco { get; set; }
        public string cr_cuenta { get; set; }
        public string cr_NumDocumento { get; set; }
        public string cr_Tarjeta { get; set; }
        public string cr_propietarioCta { get; set; }
        public string cr_estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<decimal> cr_recibo { get; set; }
        public string cr_es_anticipo { get; set; }
        public Nullable<int> IdBanco { get; set; }
        [Required(ErrorMessage ="El campo caja es obligatorio")]
        public int IdCaja { get; set; }
        public string MotiAnula { get; set; }
        public Nullable<int> IdTipoNotaCredito { get; set; }

        #region Campos de auditoria
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        #endregion
        
        #region Campos que no existen en la tabla
        public string pe_nombreCompleto { get; set; }
        public string tc_descripcion { get; set; }
        public string Su_Descripcion { get; set; }
        public double cr_saldo { get; set; }
        public int IdBodega { get; set; }
        public decimal IdEntidad { get; set; }
        public double vt_Subtotal { get; set; }
        public double vt_Iva { get; set; }
        public double vt_Total { get; set; }
        public string vt_NumFactura { get; set; }        
        public decimal IdCbteVta { get; set; }
        public List<cxc_cobro_det_Info> lst_det { get; set; }
        public DateTime? vt_fech_venc { get; set; }
        public DateTime vt_fecha { get; set; }
        public string vt_tipoDoc { get; set; }
        public string nom_Motivo_tipo_cobro { get; set; }
        #endregion
    }
}

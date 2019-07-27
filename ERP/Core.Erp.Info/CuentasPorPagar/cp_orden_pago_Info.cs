using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Contabilidad;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_orden_pago_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public decimal IdOrdenPago { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]        
        public string Observacion { get; set; }
        [Required(ErrorMessage = "El campo tipo de orden de pago es obligatorio")]
        public string IdTipo_op { get; set; }
        [Required(ErrorMessage = "El campo tipo de persona es obligatorio")]
        public string IdTipo_Persona { get; set; }
        public decimal IdPersona { get; set; }
        [Required(ErrorMessage = "El campo beneficiario es obligatorio")]
        public decimal IdEntidad { get; set; }
        public System.DateTime Fecha { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo forma de pago es obligatorio")]
        public string IdFormaPago { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }

        public string ReferenciaGen { get; set; }
        #region Campos auditoria
        public string IdUsuario { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "La observación debe tener mínimo 3 caracteres y máximo 150")]
        public string MotivoAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        #endregion
        
        public Nullable<System.DateTime> Fecha_Transac { get; set; }        
        [Required(ErrorMessage = "El campo valor a pagar a cancelar es obligatorio")]
        public double Valor_a_pagar { get; set; }
        public double Valor_estimado_a_pagar_OP { get; set; }
        public double Total_cancelado_OP { get; set; }
        public double Saldo_x_Pagar_OP { get; set; }
        public List<cp_orden_pago_det_Info> detalle { get; set; }
        public string Nom_Beneficiario { get; set; }
        public Nullable<double> Total_OP { get; set; }
        public ct_cbtecble_Info info_comprobante { get; set; }
       
        public cp_orden_pago_Info()
        {
            detalle = new List<cp_orden_pago_det_Info>();
            info_comprobante = new ct_cbtecble_Info();
        }
    }

    public class cp_orden_pago_aprobacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdOrdenPago { get; set; }
        public string IdFormaPago { get; set; }
        public System.DateTime fecha_ini { get; set; }
        public System.DateTime fecha_fin { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioAprobacion { get; set; }
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "La observación debe tener máximo 500 caracteres")]
        public string MotivoAprobacion { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public DateTime FechaAprobacion { get; set; }

        public cp_orden_pago_aprobacion_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }
    }
}

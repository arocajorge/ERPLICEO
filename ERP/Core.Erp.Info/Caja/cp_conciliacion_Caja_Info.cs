using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;

namespace Core.Erp.Info.Caja
{
    public class cp_conciliacion_Caja_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        [Required(ErrorMessage = "El campo periodo es obligatorio")]
        public int IdPeriodo { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")]
        public System.DateTime Fecha_ini { get; set; }
        [Required(ErrorMessage = "El campo fecha fin es obligatorio")]
        public DateTime Fecha_fin { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo caja es obligatorio")]
        public int IdCaja { get; set; }
        [Required(ErrorMessage = "El campo estado es obligatorio")]
        public string IdEstadoCierre { get; set; }
        [StringLength(1000, ErrorMessage = "el campo observación debe tener máximo 1000 caracteres")]
        public string Observacion { get; set; }
        public Nullable<int> IdEmpresa_op { get; set; }
        public Nullable<decimal> IdOrdenPago_op { get; set; }
        [Required(ErrorMessage = "El campo cuenta contable es obligatorio")]
        public string IdCtaCble { get; set; }
        public double Saldo_cont_al_periodo { get; set; }
        public double Ingresos { get; set; }
        public double Total_Ing { get; set; }
        public double Total_fact_vale { get; set; }
        public double Total_fondo { get; set; }
        public double Dif_x_pagar_o_cobrar { get; set; }
        public Nullable<decimal> IdTipoFlujo { get; set; }
        public Nullable<int> IdEmpresa_mov_caj { get; set; }
        public Nullable<int> IdTipoCbte_mov_caj { get; set; }
        public Nullable<decimal> IdCbteCble_mov_caj { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<decimal> SecuenciaCaja { get; set; }

        public List<cp_conciliacion_Caja_det_Info> lst_det_fact { get; set; }
        public List<cp_conciliacion_Caja_det_x_ValeCaja_Info> lst_det_vale { get; set; }
        public List<cp_conciliacion_Caja_det_Ing_Caja_Info> lst_det_ing { get; set; }

        public DateTime FechaOP { get; set; }
        public string ObservacionOP { get; set; }
        public string IdTipoPersona { get; set; }
        public decimal IdEntidad { get; set; }
        public decimal IdPersona { get; set; }
        public string IdUsuario { get; set; }
        public List<ct_cbtecble_det_Info> lst_det_ct { get; set; }
    }
}

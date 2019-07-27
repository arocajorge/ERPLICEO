using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_cbtecble_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public string CodCbteCble { get; set; }
        public int IdPeriodo { get; set; }
        public int IdSucursal { get; set; }
        public System.DateTime cb_Fecha { get; set; }
        public double cb_Valor { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "el campo observación debe tener mínimo 1 caracter y máximo 5000")]
        public string cb_Observacion { get; set; }
        public string cb_Estado { get; set; }
        public bool EstadoBool { get; set; }

        #region Campos de auditoria
        public string IdUsuario { get; set; }
        public string IdUsuarioAnu { get; set; }
        public string cb_MotivoAnu { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> cb_FechaAnu { get; set; }
        public Nullable<System.DateTime> cb_FechaTransac { get; set; }
        public Nullable<System.DateTime> cb_FechaUltModi { get; set; }
        #endregion


        //Campos que no existen en la tabla
        public List<ct_cbtecble_det_Info> lst_ct_cbtecble_det { get; set; }
        
        public string tc_TipoCbte { get; set; }
        public string Su_Descripcion { get; set; }
    }
}

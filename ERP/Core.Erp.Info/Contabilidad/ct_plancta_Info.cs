using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_plancta_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo código cuenta contable debe tener mínimo 1 caracter y máximo 20")]
        [Required(ErrorMessage = "El campo código cuenta contable es obligatorio")]
        public string IdCtaCble { get; set; }
        [StringLength(150, MinimumLength = 1, ErrorMessage = "el campo cuenta contable debe tener mínimo 1 caracter y máximo 150")]
        [Required(ErrorMessage = "El campo cuenta contable es obligatorio")]
        public string pc_Cuenta { get; set; }
        public string IdCtaCblePadre { get; set; }
        public string pc_Naturaleza { get; set; }
        public int IdNivelCta { get; set; }
        public string IdGrupoCble { get; set; }
        public string pc_Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string pc_EsMovimiento { get; set; }
        public string pc_clave_corta { get; set; }
        

        #region Campos de auditoria
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        #endregion

        //Campos que no existen en la base
        public string pc_Cuenta_padre { get; set; }
        public bool pc_EsMovimiento_bool { get; set; }
        public int IdCtaCble_int { get; set; }
        public int Secuencia { get; set; }

    }
}

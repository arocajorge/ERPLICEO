using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Erp.Info.Contabilidad
{
    public class ct_cbtecble_tipo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }

        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 10")]
        public string CodTipoCbte { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string tc_TipoCbte { get; set; }
        public string tc_Estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = "El campo nemonico es obligatorio")]
        public string tc_Interno { get; set; }

        [Required(ErrorMessage = "El campo nemonico es obligatorio")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "el campo nemonico debe tener mínimo 1 caracter y máximo 5")]
        public string tc_Nemonico { get; set; }
        [Required(ErrorMessage = "El campo tipo comprobante anulación es obligatorio")]
        public int IdTipoCbte_Anul { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        
        //campos que existen

        public bool tc_Interno_bool { get; set; }
    }
}

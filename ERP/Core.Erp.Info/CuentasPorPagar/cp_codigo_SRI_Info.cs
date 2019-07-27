using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_codigo_SRI_Info
    {
        public int IdCodigo_SRI { get; set; }
        
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string codigoSRI { get; set; }

        [Required(ErrorMessage = "El campo código base es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código base debe tener mínimo 1 caracter y máximo 50")]
        public string co_codigoBase { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(350, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 350")]
        public string co_descripcion { get; set; }

        [Required(ErrorMessage = "El campo retención es obligatorio")]
        public double co_porRetencion { get; set; }
        public System.DateTime co_f_valides_desde { get; set; }
        public System.DateTime co_f_valides_hasta { get; set; }
        public string co_estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdTipoSRI { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotivoAnulacion { get; set; }

        // no existe
        public cp_codigo_SRI_x_CtaCble_Info info_codigo_ctacble { get; set; }
    }
}

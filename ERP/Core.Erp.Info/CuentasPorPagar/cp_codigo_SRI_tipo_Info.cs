using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
    public class cp_codigo_SRI_tipo_Info
    {
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 20")]
        public string IdTipoSRI { get; set; }        
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo descripción debe tener mínimo 0 caracteres y máximo 50")]
        public string descripcion { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
    }
}

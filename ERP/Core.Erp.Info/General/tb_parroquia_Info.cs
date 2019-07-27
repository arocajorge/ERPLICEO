using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_parroquia_Info
    {
        public string IdParroquia { get; set; }
        [Required(ErrorMessage = " el campo código es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string cod_parroquia { get; set; }
        [Required(ErrorMessage = " el campo descripción es obligatorio")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 150")]
        public string nom_parroquia { get; set; }
        public bool estado { get; set; }
        [Required(ErrorMessage = " el campo ciudad es obligatorio")]
        public string IdCiudad_Canton { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnula { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }


        //campos que no existen en la tabla
        public string IdPais { get; set; }
        public string IdProvincia { get; set; }

    }
}

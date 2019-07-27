using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
   public class tb_visor_video_Info
    {
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string Cod_video { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracter y máximo 500")]
        public string Nombre_video { get; set; }
        public byte[] video { get; set; }

        public bool Estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaTransaccion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string IdUsuarioModifica { get; set; }
        public string IdUsuarioAnulacion { get; set; }
    }
}

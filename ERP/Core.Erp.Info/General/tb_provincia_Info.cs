using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_provincia_Info
    {
        [Key]
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 25")]
        public string IdProvincia { get; set; }
        public string Cod_Provincia { get; set; }
        [Required(ErrorMessage ="El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo descripción debe tener mínimo 1 caracter y máximo 50")]
        public string Descripcion_Prov { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        [Required(ErrorMessage = "El campo pais es obligatorio")]
        public string IdPais { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnula { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        [Required(ErrorMessage ="El campo región es obligatorio")]
        public string Cod_Region { get; set; }

        public tb_pais_Info info_pais { get; set; }
        public tb_provincia_Info()
        {
            info_pais = new tb_pais_Info();
        }
    }
}

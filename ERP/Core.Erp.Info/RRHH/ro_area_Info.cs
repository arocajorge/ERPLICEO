using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_area_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo división es obligatorio")]
        public int IdDivision { get; set; }
        public int IdArea { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La descripción debe tener mínimo 4 caracteres y máximo 50")]
        public string Descripcion { get; set; }
        public string Division { get; set; }

        public bool EstadoBool { get; set; }
        public string estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotiAnula { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_catalogo_Info
    {
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El campo código debe tener mínimo 4 caracteres y máximo 10")]
        [Required(ErrorMessage = "El campo Código reporte es obligatorio")]
        public string CodCatalogo { get; set; }
        public int IdCatalogo { get; set; }
        public int IdTipoCatalogo { get; set; }
        [StringLength(250, MinimumLength = 4, ErrorMessage = "El campo descripción debe tener mínimo 4 caracteres y máximo 250")]
        [Required(ErrorMessage = "El campo Código reporte es obligatorio")]
        public string ca_descripcion { get; set; }
        public string ca_estado { get; set; }
        public bool EstadoBool { get; set; }
        public int ca_orden { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotivoAnulacion { get; set; }

    }
}

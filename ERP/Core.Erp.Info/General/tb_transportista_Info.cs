using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_transportista_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdTransportista { get; set; }
        [Required]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracter y máximo 500")]
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transaccion { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }

        [Required(ErrorMessage = "El campo placa es obligatorio")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo placa debe tener mínimo 1 caracter y máximo 20")]
        public string Placa { get; set; }
    }
}

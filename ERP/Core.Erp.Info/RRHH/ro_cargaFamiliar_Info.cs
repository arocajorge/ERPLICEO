using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_cargaFamiliar_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCargaFamiliar { get; set; }
        public decimal IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo cédula es obligatorio")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "La descripción debe tener mínimo 10 caracteres y máximo 50")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El campo tipo de sexo es obligatorio")]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "El campo parentezco es obligatorio")]
        public string TipoFamiliar { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "El nombre debe tener mínimo 20 caracteres y máximo 200")]

        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<System.DateTime> FechaDefucion { get; set; }
        public bool capacidades_especiales { get; set; }
    }
}

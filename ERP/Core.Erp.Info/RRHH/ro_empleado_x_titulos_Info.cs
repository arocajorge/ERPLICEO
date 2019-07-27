using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_x_titulos_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo empleado es obligatorio")]
        public decimal IdEmpleado { get; set; }
        public int Secuencia { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime fecha { get; set; }
        [Required(ErrorMessage = "El campo institución es obligatorio")]
        public string IdInstitucion { get; set; }
        [Required(ErrorMessage = "El campo título es obligatorio")]
        public string IdTitulo { get; set; }
        public string Observacion { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotivoAnulacion { get; set; }
        public string estado { get; set; }


        public string em_codigo { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string institucion { get; set; }
        public string titulo { get; set; }

    }
}

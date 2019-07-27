using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_persona_Info
    {
        public decimal IdPersona { get; set; }
        [StringLength(20, MinimumLength = 0, ErrorMessage = "el campo código debe tener máximo 20 caracteres")]
        public string CodPersona { get; set; }
        [Required(ErrorMessage ="El campo naturaleza es requerido")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "el campo naturaleza debe tener mínimo 1 caracter y máximo 25 caracteres")]
        public string pe_Naturaleza { get; set; }
        [Required(ErrorMessage = "El campo nombre completo es requerido")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "el campo nombre completo debe tener mínimo 1 caracter y máximo 200 caracteres")]
        public string pe_nombreCompleto { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "el campo razón social debe tener máximo 150 caracteres")]
        public string pe_razonSocial { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo apellidos debe tener máximo 100 caracteres")]
        public string pe_apellido { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo nombres debe tener máximo 100 caracteres")]
        public string pe_nombre { get; set; }
        public string IdTipoDocumento { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo número documento debe tener mínimo 1 caracter y máximo 50 caracteres")]
        [Required(ErrorMessage = "El campo número documento es requerido")]
        public string pe_cedulaRuc { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "el campo dirección debe tener máximo 150 caracteres")]
        public string pe_direccion { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo teléfono de casa debe tener máximo 50 caracteres")]
        public string pe_telfono_Contacto { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo celular debe tener máximo 50 caracteres")]
        public string pe_celular { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo correo debe tener máximo 100 caracteres")]
        public string pe_correo { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo fax debe tener máximo 50 caracteres")]
        public string pe_sexo { get; set; }
        public string IdEstadoCivil { get; set; }        
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string pe_estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<System.DateTime> pe_fechaCreacion { get; set; }
        public Nullable<System.DateTime> pe_fechaModificacion { get; set; }
        public string pe_UltUsuarioModi { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "el campo correo 2 debe tener máximo 100 caracteres")]
        public string IdTipoCta_acreditacion_cat { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo número de cuenta debe tener máximo 50 caracteres")]
        public string num_cta_acreditacion { get; set; }
        public Nullable<int> IdBanco_acreditacion { get; set; }

        //Campos que no existen en la base
        public decimal IdEntidad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Core.Erp.Info.General
{
    public class tb_empresa_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage ="El campo código es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string codigo { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "el campo nombre debe tener mínimo 1 caracter y máximo 300")]
        public string em_nombre { get; set; }
        [Required(ErrorMessage = "El campo razón social es obligatorio")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "el campo razón social debe tener mínimo 1 caracter y máximo 300")]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "El campo nombre comercial es obligatorio")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "el campo nombre comercial debe tener mínimo 1 caracter y máximo 300")]
        public string NombreComercial { get; set; }
        [Required(ErrorMessage = "El campo # contribuyente especial es obligatorio")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "el campo # contribuyente debe tener mínimo 1 caracter y máximo 5")]
        public string ContribuyenteEspecial { get; set; }
        [Required(ErrorMessage = "El campo RUC es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo RUC debe tener mínimo 1 caracter y máximo 50")]
        public string em_ruc { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo nombre gerente debe tener mínimo 0 caracter y máximo 50")]
        public string em_gerente { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "el campo nombre contador debe tener mínimo 0 caracter y máximo 150")]
        public string em_contador { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo RUC contador debe tener mínimo 0 caracter y máximo 50")]
        public string em_rucContador { get; set; }
        [StringLength(200, MinimumLength = 0, ErrorMessage = "el campo teléfonos debe tener mínimo 0 caracter y máximo 200")]
        public string em_telefonos { get; set; }
        [Required(ErrorMessage = "El campo dirección es obligatorio")]
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "el campo dirección debe tener mínimo 0 caracter y máximo 1000")]
        public string em_direccion { get; set; }
        public byte[] em_logo { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo fecha inicio contable es obligatorio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime em_fechaInicioContable { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> em_fechaInicioActividad { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "el campo código entidad debe tener mínimo 0 caracter y máximo 50")]
        public string cod_entidad_dinardap { get; set; }
        [StringLength(300, MinimumLength = 0, ErrorMessage = "el campo email debe tener mínimo 0 caracter y máximo 300")]
        public string em_Email { get; set; }

        //Campo para la vista
        //public bool ObligadoAllevarConta_bool { get; set; }
        public bool EstadoBool { get; set; }
        public Image em_logo_imagen { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Erp.Info.SeguridadAcceso
{
    public class seg_usuario_Info
    {
        public decimal IdTransaccionSession { get; set; }
        [Key]
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public string IdUsuario { get; set; }
        public string Contrasena { get; set; }
        public string estado { get; set; }
        public bool EstadoBool { get; set; }
        public Nullable<System.DateTime> Fecha_Transaccion { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public string Nombre { get; set; }
        public bool ExigirDirectivaContrasenia { get; set; }
        public bool CambiarContraseniaSgtSesion { get; set; }
        public bool es_super_admin { get; set; }
        public string contrasena_admin { get; set; }
        public Nullable<int> IdMenu { get; set; }
        public string IPMaquina { get; set; }
        public string IPImpresora { get; set; }
        public List<seg_Usuario_x_Empresa_Info> lst_usuario_x_empresa { get; set; }
        public List<seg_usuario_x_tb_sucursal_Info> lst_usuario_x_sucursal { get; set; }
    }
}

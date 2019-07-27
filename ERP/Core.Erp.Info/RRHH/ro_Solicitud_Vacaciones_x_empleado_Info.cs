using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_Solicitud_Vacaciones_x_empleado_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo empleado es obligatorio")]
        public decimal IdEmpleado { get; set; }
        public int IdSolicitud { get; set; }
        public int IdVacacion { get; set; }
        [Required(ErrorMessage = "El campo empleado autoriza es obligatorio")]

        public decimal IdEmpleado_aprue { get; set; }
        public Nullable<decimal> IdEmpleado_remp { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public System.DateTime Fecha { get; set; }
        public int AnioServicio { get; set; }
        public int Dias_q_Corresponde { get; set; }
        public int Dias_a_disfrutar { get; set; }
        public int Dias_pendiente { get; set; }
        public System.DateTime Anio_Desde { get; set; }
        public System.DateTime Anio_Hasta { get; set; }
        public System.DateTime Fecha_Desde { get; set; }
        public System.DateTime Fecha_Hasta { get; set; }
        public System.DateTime Fecha_Retorno { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El campo descripción debe tener mínimo 4 caracteres y máximo 250")]
        public string Observacion { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuario_Anu { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string MotivoAnulacion { get; set; }
        public string ip { get; set; }
        public string nom_pc { get; set; }
        public bool Gozadas_Pgadas { get; set; }
        public bool Canceladas { get; set; }

        public string pe_cedulaRuc { get; set; }
        public string em_codigo { get; set; }
        public string pe_nombre_completo { get; set; }
        public Nullable<int> IdLiquidacion { get; set; }
        public string Estado_liquidacion { get; set; }
        public List<ro_historico_vacaciones_x_empleado_Info> lst_vacaciones { get; set; }
        public  ro_Solicitud_Vacaciones_x_empleado_Info()
        {
            lst_vacaciones = new List<ro_historico_vacaciones_x_empleado_Info>();

        }
    }
}

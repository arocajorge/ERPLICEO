using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_nomina_x_horas_extras_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdHorasExtras { get; set; }
        [Required(ErrorMessage = "El campo nómina es obligatorio")]
        public int IdNomina_Tipo { get; set; }
        [Required(ErrorMessage = "El campo nómna tipo es obligatorio")]
        public int IdNomina_TipoLiqui { get; set; }
        [Required(ErrorMessage = "El campo periodo es obligatorio")]
        public int IdPeriodo { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotivoAnulacion { get; set; }
        public string observacion { get; set; }



        public string Descripcion { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public List<ro_nomina_x_horas_extras_det_Info> lst_nomina_horas_extras { get; set; }
        public  ro_nomina_x_horas_extras_Info()
        {
            lst_nomina_horas_extras = new List<ro_nomina_x_horas_extras_det_Info>();
        }
    }
}

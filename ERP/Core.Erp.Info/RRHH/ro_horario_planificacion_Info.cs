using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_horario_planificacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdPlanificacion { get; set; }
        public string Observacion { get; set; }
        public int IdNomina { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
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
        public int? IdCargo { get; set; }

        public int? IdSucursal { get; set; }

        public int? IdDivision { get; set; }
        public int? IdArea { get; set; }
        public int? IdDepartamento { get; set; }
        public int IdHorario { get; set; }

        public List<ro_horario_planificacion_det_Info> lst_planificacion_det { get; set; }

        public ro_horario_planificacion_Info()
        {
            lst_planificacion_det = new List<ro_horario_planificacion_det_Info>();
        }

        public decimal IdEmpleado { get; set; }
    }
}

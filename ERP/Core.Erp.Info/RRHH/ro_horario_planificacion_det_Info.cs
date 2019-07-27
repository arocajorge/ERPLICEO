using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public class ro_horario_planificacion_det_Info
    {
        public int IdEmpresa { get; set; }
        public int? IdCalendario { get; set; }
        public decimal IdEmpleado { get; set; }
        public decimal IdPlanificacion { get; set; }
        public decimal? IdHorario { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }


        public string pe_cedulaRuc { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string ca_descripcion { get; set; }
        public string ar_descripcion { get; set; }
        public string di_descripcion { get; set; }
        public string de_descripcion { get; set; }
        public string Su_Descripcion { get; set; }
        public DateTime? fecha { get; set; }
        public int Secuencia { get; set; }

       
    }
}

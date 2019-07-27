using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
   public class ROL_011_Info
    {
        public int IdEmpresa { get; set; }
        public int IdHorasExtras { get; set; }
        public int IdNominaTipo { get; set; }
        public int IdNominaTipoLiqui { get; set; }
        public int IdPeriodo { get; set; }
        public decimal IdEmpleado { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<System.TimeSpan> time_entrada1 { get; set; }
        public Nullable<System.TimeSpan> time_entrada2 { get; set; }
        public Nullable<System.TimeSpan> time_salida1 { get; set; }
        public Nullable<System.TimeSpan> time_salida2 { get; set; }
        public double hora_extra25 { get; set; }
        public double hora_extra50 { get; set; }
        public double hora_extra100 { get; set; }
        public double Valor25 { get; set; }
        public double Valor50 { get; set; }
        public double Valor100 { get; set; }
        public double Sueldo_base { get; set; }
        public double hora_atraso { get; set; }
        public double hora_temprano { get; set; }
        public double hora_trabajada { get; set; }
        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public string ca_descripcion { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_nombreCompleto { get; set; }
    }
}

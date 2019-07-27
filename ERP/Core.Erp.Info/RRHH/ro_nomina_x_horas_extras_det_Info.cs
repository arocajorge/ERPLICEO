using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_nomina_x_horas_extras_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdHorasExtras { get; set; }
        public decimal IdEmpleado { get; set; }
        public int IdCalendario { get; set; }
        public decimal IdTurno { get; set; }
        public decimal IdHorario { get; set; }
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
        public bool es_HorasExtrasAutorizadas { get; set; }
        public int IdSucursal { get; set; }



        public string Horario { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string tu_descripcion { get; set; }
        public string ca_descripcion { get; set; }
        public string pe_cedulaRuc { get; set; }

        public int Secuencia { get; set; }
    }
}

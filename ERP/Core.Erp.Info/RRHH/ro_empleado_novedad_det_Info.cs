using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_novedad_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdNovedad { get; set; }
        public decimal IdEmpleado { get; set; }
        public int Secuencia { get; set; }
        public Nullable<int> IdNomina_tipo { get; set; }
        public Nullable<int> IdNomina_Tipo_Liq { get; set; }
        public string IdRol { get; set; }
        public string IdRubro { get; set; }
        public System.DateTime FechaPago { get; set; }
        public double Valor { get; set; }
        public string EstadoCobro { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public string IdCalendario { get; set; }
        public Nullable<double> Num_Horas { get; set; }
        public Nullable<int> IdPeriodo { get; set; }
        public Nullable<double> CantidadHoras { get; set; }


        public string rub_tipo { get; set; }
        public string ru_descripcion { get; set; }

    }
}

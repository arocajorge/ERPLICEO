using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_022_Info
    {
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public int IdNomina_TipoLiqui { get; set; }
        public Nullable<int> IdDivision { get; set; }
        public Nullable<int> IdArea { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdNomina_Tipo { get; set; }
        public Nullable<int> IdPeriodo { get; set; }
        public string Descripcion { get; set; }
        public string ru_descripcion { get; set; }
        public string empleado { get; set; }
        public string ca_descripcion { get; set; }
        public string ru_tipo { get; set; }
        public Nullable<int> ru_orden { get; set; }
        public Nullable<double> Valor { get; set; }

        public string NomNomina { get; set; }
        public string NomNominaTipo { get; set; }
        public string Su_Descripcion { get; set; }
        public Nullable<System.DateTime> FechaIni { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public string NomDivision { get; set; }
        public string NomArea { get; set; }
        public string IdRubro { get; set; }

    }
}

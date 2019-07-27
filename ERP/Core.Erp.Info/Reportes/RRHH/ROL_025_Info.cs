using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_025_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public decimal IdPersona { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_sexo { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public string ca_descripcion { get; set; }
        public System.DateTime pe_FechaIni { get; set; }
        public Nullable<int> IdNomina { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public decimal IdRol { get; set; }
        public int IdPeriodo { get; set; }
        public string IdRubro { get; set; }
        public string ru_descripcion { get; set; }
        public double Valor { get; set; }
        public Nullable<int> dias { get; set; }
        public Nullable<long> IdRow { get; set; }
        public string Division { get; set; }
        public string Area { get; set; }
    }
}

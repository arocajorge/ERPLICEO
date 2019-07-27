using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_024_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRol { get; set; }
        public decimal IdEmpleado { get; set; }
        public int IdNominaTipoLiqui { get; set; }
        public int IdNominaTipo { get; set; }
        public string NomNomina { get; set; }
        public string NomNominaTipo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public int IdPeriodo { get; set; }
        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public double Valor { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public string pe_cedulaRuc { get; set; }
        public double Dias { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_020_Info
    {
        public Nullable<long> Secuancia { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int IdNomina { get; set; }
        public int IdNominaTipo { get; set; }
        public int IdPeriodo { get; set; }
        public int IdCuentaBancaria { get; set; }
        public int IdProceso { get; set; }
        public double Valor { get; set; }
        public string em_tipoCta { get; set; }
        public string em_NumCta { get; set; }
        public string Nombres { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string em_codigo { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public string Area { get; set; }
        public string Division { get; set; }
    }
}

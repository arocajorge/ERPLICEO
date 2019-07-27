using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_004_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public double UtilidadDerechoIndividual { get; set; }
        public double UtilidadCargaFamiliar { get; set; }
        public double LimiteDistribucionUtilidad { get; set; }
        public double DiasTrabajados { get; set; }
        public double CargasFamiliares { get; set; }
        public double ValorIndividual { get; set; }
        public double ValorCargaFamiliar { get; set; }
        public double ValorExedenteIESS { get; set; }
        public double ValorTotal { get; set; }
        public decimal IdEmpleado { get; set; }
        public string Nombres { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string ca_descripcion { get; set; }
        public string em_codigo { get; set; }
        public int IdUtilidad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
    public class ROL_003_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public decimal IdPersona { get; set; }
        public decimal IdNovedad { get; set; }
        public System.DateTime FechaPago { get; set; }
        public double Valor { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string ca_descripcion { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string DescripcionProcesoNomina { get; set; }
        public string Observacion { get; set; }
        public string Descripcion { get; set; }
        public string ru_descripcion { get; set; }
        public string EstadoCobro { get; set; }
        public double TotalValor { get; set; }
    }
}

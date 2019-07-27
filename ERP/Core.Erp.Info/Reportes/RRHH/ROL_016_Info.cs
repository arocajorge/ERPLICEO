using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.RRHH
{
  public  class ROL_016_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdActaFiniquito { get; set; }
        public decimal IdEmpleado { get; set; }
        public string IdCausaTerminacion { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public System.DateTime FechaSalida { get; set; }
        public double UltimaRemuneracion { get; set; }
        public string Observacion { get; set; }
        public double Ingresos { get; set; }
        public double Egresos { get; set; }
        public double Liquido { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string ca_descripcion { get; set; }
        public string Contrato { get; set; }
        public int IdTipoCatalogo { get; set; }
    }
}

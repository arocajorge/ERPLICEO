using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_EmpleadoNovedadCargaMasiva_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdCarga { get; set; }
        public int Secuancia { get; set; }
        public int IdEmpresa_nov { get; set; }
        public decimal IdNovedad { get; set; }
        public string Observacion { get; set; }
        public double Valor { get; set; }
        public decimal IdEmpleado { get; set; }
        public string em_codigo { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public Nullable<double> CantidadHoras { get; set; }

    }
}

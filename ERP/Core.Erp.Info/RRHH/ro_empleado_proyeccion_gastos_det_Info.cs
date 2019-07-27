using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_empleado_proyeccion_gastos_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdTransaccion { get; set; }
        public int Secuencia { get; set; }
        public string IdTipoGasto { get; set; }
        public double Valor { get; set; }
        public string Observacion { get; set; }
    }
}

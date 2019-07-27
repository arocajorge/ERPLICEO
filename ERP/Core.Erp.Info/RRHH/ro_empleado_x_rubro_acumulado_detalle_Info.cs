using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_empleado_x_rubro_acumulado_detalle_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public string IdRubro { get; set; }
        public int Secuencia { get; set; }
        public string IdRubroContabilizacion { get; set; }
        public int IdJornada { get; set; }

        #region Campos que no existen en la tabla
        public string Descripcion { get; set; }
        public string ru_descripcion { get; set; }
        #endregion
    }
}

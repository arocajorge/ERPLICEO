using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
    public class ro_rol_detalle_x_rubro_acumulado_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRol { get; set; }
        public decimal IdEmpleado { get; set; }
        public string IdRubro { get; set; }
        public double Valor { get; set; }
        public string Estado { get; set; }
        public Nullable<int> IdSucursal { get; set; }

        #region Campos que no estan en la tabla
        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public string Empleado { get; set; }
        public int IdNominaTipo { get; set; }
        public double ValorAjustado { get; set; }
        public int Secuencial { get; set; }
        #endregion

    }
}

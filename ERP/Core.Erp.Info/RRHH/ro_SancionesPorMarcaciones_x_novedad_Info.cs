using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public class ro_SancionesPorMarcaciones_x_novedad_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAjuste { get; set; }
        public int Secuencia { get; set; }
        public decimal IdNovedad { get; set; }
        public int IdEmpresa_nov { get; set; }
        public decimal IdEmpleado { get; set; }
        public int IdNomina_Tipo { get; set; }
        public int IdNomina_TipoLiqui { get; set; }
        public double Valor { get; set; }
        public System.DateTime FechaPago { get; set; }
        public string em_codigo { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_nombreCompleto { get; set; }

        public int IdSucursal { get; set; }
        public Nullable<double> Sueldo { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info
    {
        public int IdEmpresa { get; set; }
        public int IdNomina { get; set; }
        public int IdNominaTipo { get; set; }
        public string IdCtaCble { get; set; }
        public string IdCtaCble_sueldo { get; set; }
        public string Observacion { get; set; }
        public Nullable<decimal> IdTipoFlujo { get; set; }
        public int Secuencia { get; set; }

        public string Descripcion { get; set; }
        public string DescripcionProcesoNomina { get; set; }


        public string pc_Cuenta { get; set; }
        public string IdString { get; set; }
    }
}

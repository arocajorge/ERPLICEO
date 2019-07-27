using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_periodo_x_ro_Nomina_TipoLiqui_Info
    {

        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdNomina_Tipo { get; set; }
        public int IdNomina_TipoLiqui { get; set; }
        public int IdPeriodo { get; set; }
        public string Cerrado { get; set; }
        public string Procesado { get; set; }
        public string Contabilizado { get; set; }



        public System.DateTime pe_FechaIni { get; set; }
        public System.DateTime pe_FechaFin { get; set; }
        public string descripcion { get; set; }
        public bool seleccionado { get; set; }
        public bool esta_base { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_009_resumen_Info
    {
        public string Cod_Sri { get; set; }
        public string descripcion { get; set; }
        public string Tipo_Retencion { get; set; }
        public double Base_Ret { get; set; }
        public double Total_Ret { get; set; }
    }
}
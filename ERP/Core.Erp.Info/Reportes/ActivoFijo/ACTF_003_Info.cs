using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.ActivoFijo
{
   public class ACTF_003_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRetiroActivo { get; set; }
        public string NumComprobante { get; set; }
        public int IdActivoFijo { get; set; }
        public string Af_Nombre { get; set; }
        public double ValorActivo { get; set; }
        public double Valor_Tot_Bajas { get; set; }
        public double Valor_Tot_Mejora { get; set; }
        public double Valor_Depre_Acu { get; set; }
        public double Valor_Neto { get; set; }
        public System.DateTime Fecha_Retiro { get; set; }
        public string Estado { get; set; }
        public string Concepto_Retiro { get; set; }
        public string IdCtaCble { get; set; }
        public double dc_Valor { get; set; }
        public double dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
        public string pc_Cuenta { get; set; }
    }
}

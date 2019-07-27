using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.ActivoFijo
{
   public class ACTF_005_Info
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public Nullable<int> IdActivoFijoTipo { get; set; }
        public string nom_tipo { get; set; }
        public string nom_categoria { get; set; }
        public Nullable<int> IdCategoriaAF { get; set; }
        public string CodActivoFijo { get; set; }
        public string Af_Nombre { get; set; }
        public string Estado_Proceso { get; set; }
        public string nom_estado_proceso { get; set; }
        public System.DateTime Af_fecha_compra { get; set; }
        public double Af_costo_compra { get; set; }
        public double valor_mejora { get; set; }
        public double valor_baja { get; set; }
        public double valor_depreciacion { get; set; }
        public double valor_venta { get; set; }
        public double saldo { get; set; }
    }
}

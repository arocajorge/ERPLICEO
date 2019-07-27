using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.ActivoFijo
{
    public class ACTF_004_resumen_Info
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijoTipo { get; set; }
        public string IdUsuario { get; set; }
        public string Af_Descripcion { get; set; }
        public double Af_costo_compra { get; set; }
        public double Valor_Depreciacion { get; set; }
        public double Valor_ult_depreciacion { get; set; }
        public double Costo_neto { get; set; }
    }
}

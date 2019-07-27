using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.ActivoFijo
{
   public class ACTF_004_detalle_Info
    {
        public int IdEmpresa { get; set; }
        public int IdActivoFijo { get; set; }
        public string IdUsuario { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public string CodActivoFijo { get; set; }
        public string Af_Nombre { get; set; }
        public int IdActivoFijoTipo { get; set; }
        public string tipo_AF { get; set; }
        public int IdCategoria_Af { get; set; }
        public string Categoria_AF { get; set; }
        public double Af_costo_compra { get; set; }
        public double Af_Depreciacion_acum { get; set; }
        public double Costo_actual { get; set; }
        public double valor_ult_depreciacion { get; set; }
        public System.DateTime Af_fecha_compra { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_retencion_det_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdRetencion { get; set; }
        public int Idsecuencia { get; set; }
        public string re_tipoRet { get; set; }
        public double? re_baseRetencion { get; set; }
        public int IdCodigo_SRI { get; set; }
        public string re_Codigo_impuesto { get; set; }
        public double? re_Porcen_retencion { get; set; }
        public double? re_valor_retencion { get; set; }
        public string IdCtacble { get; set; }
    }
}

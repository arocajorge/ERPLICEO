using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_orden_pago_formapago_Info
    {
        public string IdFormaPago { get; set; }
        public string descripcion { get; set; }
        public string IdTipoTransaccion { get; set; }
        public string CodModulo { get; set; }
        public Nullable<int> IdTipoMovi_caj { get; set; }

    }
}

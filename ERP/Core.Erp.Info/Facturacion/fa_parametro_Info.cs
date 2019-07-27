using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
    public class fa_parametro_Info
    {
        public int IdEmpresa { get; set; }
        public int IdMovi_inven_tipo_Factura { get; set; }
        public int IdTipoCbteCble_Factura { get; set; }
        public int IdTipoCbteCble_NC { get; set; }
        public int IdTipoCbteCble_ND { get; set; }
        public int NumeroDeItemFact { get; set; }
        public int IdCaja_Default_Factura { get; set; }
        public string IdCtaCble_IVA { get; set; }
        public string IdCtaCble_SubTotal_Vtas_x_Default { get; set; }
        public string IdCtaCble_CXC_Vtas_x_Default { get; set; }
        public bool pa_Contabiliza_descuento { get; set; }
        public string pa_IdCtaCble_descuento { get; set; }
        public int NumeroDeItemProforma { get; set; }
        public string clave_desbloqueo_precios { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public Nullable<decimal> IdClienteConsumidorFinal { get; set; }
        public Nullable<double> MontoMaximoConsumidorFinal { get; set; }
    }
}

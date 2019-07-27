using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_002_cancelaciones_Info
    {
        public int mba_IdEmpresa { get; set; }
        public decimal mba_IdCbteCble { get; set; }
        public int mba_IdTipocbte { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdBodega_Cbte { get; set; }
        public Nullable<decimal> IdCbte_vta_nota { get; set; }
        public string dc_TipoDocumento { get; set; }
        public string Referencia { get; set; }
        public string Observacion { get; set; }
        public Nullable<double> monto { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_007_Info
    {
        public long IdRow { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble { get; set; }
        public int IdTipocbte { get; set; }
        public System.DateTime cb_FechaCheque { get; set; }
        public string cb_Cheque { get; set; }
        public string pe_nombreCompleto { get; set; }
        public double cb_Valor { get; set; }
        public string ca_descripcion { get; set; }
        public System.DateTime cb_Fecha { get; set; }
        public string Nombre { get; set; }
        public string cb_Observacion { get; set; }
        public Nullable<decimal> IdPersona_Girado_a { get; set; }
        public int IdBanco { get; set; }
        public string ba_descripcion { get; set; }
        public string Estado { get; set; }
        public string IdCatalogo { get; set; }
    }
}

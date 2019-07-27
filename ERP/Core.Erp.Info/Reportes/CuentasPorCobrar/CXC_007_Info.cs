using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorCobrar
{
    public class CXC_007_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdMotivo { get; set; }
        public string DescripcionMotivo { get; set; }
        public double Valor { get; set; }
        public string Su_Descripcion { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Lote { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> IdEmpresa_ct { get; set; }
        public Nullable<int> IdTipoCbte_ct { get; set; }
        public Nullable<decimal> IdCbteCble_ct { get; set; }
        public string ba_descripcion { get; set; }
        public string NombreUsuario { get; set; }
    }
}

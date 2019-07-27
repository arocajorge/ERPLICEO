using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_016_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdProveedor { get; set; }
        public string IdUsuario { get; set; }
        public string Su_Descripcion { get; set; }
        public string pe_CedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public double SaldoInicial { get; set; }
        public double Compra { get; set; }
        public double Retenciones { get; set; }
        public double Pagos { get; set; }
        public double Saldo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Facturacion
{
   public class fa_cliente_x_fa_Vendedor_x_sucursal_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdCliente { get; set; }
        public int IdSucursal { get; set; }
        public int IdVendedor { get; set; }
        public string observacion { get; set; }
    }
}

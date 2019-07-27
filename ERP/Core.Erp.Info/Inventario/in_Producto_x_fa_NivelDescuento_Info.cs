using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Producto_x_fa_NivelDescuento_Info
    {
        public decimal IdTransaccionSession { get; set; }

        public int IdEmpresa { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public int IdNivel { get; set; }
        public double Porcentaje { get; set; }

        public string Descripcion { get; set; }
    }
}

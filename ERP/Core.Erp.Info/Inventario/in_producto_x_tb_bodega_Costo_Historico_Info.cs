using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
   public class in_producto_x_tb_bodega_Costo_Historico_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public int IdFecha { get; set; }
        public int Secuencia { get; set; }
        public System.DateTime fecha { get; set; }
        public double costo { get; set; }
        public double Stock_a_la_fecha { get; set; }
        public string Observacion { get; set; }
        public Nullable<System.DateTime> fecha_trans { get; set; }

        #region Campos de recosteo por sucursal
        public string nom_producto { get; set; }
        public string cod_producto { get; set; }
        #endregion
    }
}

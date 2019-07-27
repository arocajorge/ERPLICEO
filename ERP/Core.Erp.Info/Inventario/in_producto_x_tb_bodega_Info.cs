using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
   public class in_producto_x_tb_bodega_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = ("el campo sucursal es obligatorio"))]
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = ("el campo bodega es obligatorio"))]

        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public string IdCtaCble_Inven { get; set; }
        public string IdCtaCble_Costo { get; set; }
        public string IdCtaCble_Gasto_x_cxp { get; set; }
        public string IdCtaCble_Vta { get; set; }
        public bool seleccionado { get; set; }
        [Required(ErrorMessage = ("el campo stock minimo es obligatorio"))]
        public double Stock_minimo { get; set; }
        public int Secuencia { get; set; }

        #region Campos que no existen
        public string Su_Descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public bool Eliminar { get; set; }
        public string IdString { get; set; }//PK PARA PRODUCTO POR BODEGA
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_movi_inven_tipo_x_tb_bodega_Info
    {
        public int IdEmpresa { get; set; }
        public int IdMovi_inven_tipo { get; set; }
        public int IdSucucursal { get; set; }
        public int IdBodega { get; set; }
        public string IdCtaCble { get; set; }

        //Campos que no existen en la tabla
        public string Su_Descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public bool seleccionado { get; set; }
    }
}

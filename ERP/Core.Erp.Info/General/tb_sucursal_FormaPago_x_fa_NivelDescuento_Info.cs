using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_sucursal_FormaPago_x_fa_NivelDescuento_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int Secuencia { get; set; }
        public string IdCatalogo { get; set; }
        public int IdNivel { get; set; }

        //#region Campos que no existen en la tabla
        //public List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> ListaNivelDescuento { get; set; }

        //public string Su_Descripcion { get; set; }
        //public string Nombre { get; set; }
        //#endregion
    }
}

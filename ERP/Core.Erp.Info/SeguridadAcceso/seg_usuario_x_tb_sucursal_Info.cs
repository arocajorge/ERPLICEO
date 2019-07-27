using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.SeguridadAcceso
{
    public class seg_usuario_x_tb_sucursal_Info
    {
        public string IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Observacion { get; set; }
        #region Campos que no existen en la tabla
        public string Su_Descripcion { get; set; }
        public string em_nombre { get; set; }
        public string IdString { get; set; }
        public int Secuencia { get; set; }
        public int var_IdEmpresa { get; set; }
        #endregion
    }
}

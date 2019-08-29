using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Caja
{
    public class caj_Caja_x_seg_usuario_Info
    {
        public int IdEmpresa { get; set; }
        public int IdCaja { get; set; }
        public int Secuencia { get; set; }
        public string IdUsuario { get; set; }

        #region Campor que no existen en la tabla
        public string Nombre { get; set; }
        #endregion
    }
}

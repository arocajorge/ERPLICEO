using Core.Erp.Data.SeguridadAcceso;
using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.SeguridadAcceso
{
    public class seg_usuario_x_tb_sucursal_Bus
    {
        seg_usuario_x_tb_sucursal_Data odata = new seg_usuario_x_tb_sucursal_Data();
        public List<seg_usuario_x_tb_sucursal_Info> GetList(string IdUsuario)
        {
            try
            {
                return odata.GetList(IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

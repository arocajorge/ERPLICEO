using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.General
{
    public class tb_sis_reporte_x_seg_usuario_Bus
    {
        tb_sis_reporte_x_seg_usuario_Data odata = new tb_sis_reporte_x_seg_usuario_Data();
    
        public List<tb_sis_reporte_x_seg_usuario_Info> get_list(int IdEmpresa, string IdUsuario, bool MostrarNoAsignados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdUsuario, MostrarNoAsignados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.eliminarDB(IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<tb_sis_reporte_x_seg_usuario_Info> Lista, int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.guardarDB(Lista, IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

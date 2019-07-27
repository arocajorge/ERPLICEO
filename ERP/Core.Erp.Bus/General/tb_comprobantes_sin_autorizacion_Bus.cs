using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.General
{
   public class tb_comprobantes_sin_autorizacion_Bus
    {
        tb_comprobantes_sin_autorizacion_Data odata = new tb_comprobantes_sin_autorizacion_Data();
        public List<tb_comprobantes_sin_autorizacion_Info> get_list(int IdEmpresa, string Tipo_doc, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal)
        {
            try
            {
                return odata.get_list(IdEmpresa, Tipo_doc, Fecha_ini, Fecha_fin, IdSucursal);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarEstado(tb_comprobantes_sin_autorizacion_Info info)
        {
            try
            {
                return odata.modificarEstado(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

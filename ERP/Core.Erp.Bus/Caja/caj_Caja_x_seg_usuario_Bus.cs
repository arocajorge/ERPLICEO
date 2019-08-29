using Core.Erp.Data.Caja;
using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Caja
{
    public class caj_Caja_x_seg_usuario_Bus
    {
        caj_Caja_x_seg_usuario_Data odata = new caj_Caja_x_seg_usuario_Data();
        public List<caj_Caja_x_seg_usuario_Info> get_list(int IdEmpresa, int IdCaja)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCaja);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public caj_Caja_x_seg_usuario_Info get_info(int IdEmpresa, int IdCaja, int Secuencia)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCaja, Secuencia);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(caj_Caja_x_seg_usuario_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

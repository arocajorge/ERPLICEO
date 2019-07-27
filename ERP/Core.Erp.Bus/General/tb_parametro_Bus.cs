using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.General
{
    public class tb_parametro_Bus
    {
        tb_parametro_Data odata = new tb_parametro_Data();
        public tb_parametro_Info GetInfo(int IdEmpresa)
        {
            try
            {
                return odata.GetInfo(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(tb_parametro_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

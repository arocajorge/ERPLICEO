using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_028_Bus
    {
        ROL_028_Data odata = new ROL_028_Data();
        public List<ROL_028_Info> GetList(int IdEmpresa, int Id_Rdep)
        {
            try
            {
                return odata.get_list(IdEmpresa, Id_Rdep);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_029_Bus
    {
        ROL_029_Data odata = new ROL_029_Data();
        public List<ROL_029_Info> get_list(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAjuste);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ROL_029_Info> get_list_detalle_oi(int IdEmpresa, decimal IdAjuste, int IdEmpleado)
        {
            try
            {
                return odata.get_list_detalle_oi(IdEmpresa, IdAjuste, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

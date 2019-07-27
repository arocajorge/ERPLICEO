using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_HorasProfesores_det_Bus
    {
        ro_HorasProfesores_det_Data odata = new ro_HorasProfesores_det_Data();
        public List<ro_HorasProfesores_det_Info> get_list(int IdEmpresa, decimal IdCarga)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCarga);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

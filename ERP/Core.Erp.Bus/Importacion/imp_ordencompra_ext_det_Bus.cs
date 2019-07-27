using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Importacion;
using Core.Erp.Info.Importacion;

namespace Core.Erp.Bus.Importacion
{
  public  class imp_ordencompra_ext_det_Bus
    {
        imp_ordencompra_ext_det_Data odata = new imp_ordencompra_ext_det_Data();

        public List<imp_ordencompra_ext_det_Info> get_list(int IdEmpresa, decimal IdOrdencompraext)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdOrdencompraext);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

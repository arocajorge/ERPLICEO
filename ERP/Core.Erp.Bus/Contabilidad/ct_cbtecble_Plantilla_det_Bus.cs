using Core.Erp.Data.Contabilidad;
using Core.Erp.Info;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_cbtecble_Plantilla_det_Bus
    {
        ct_cbtecble_Plantilla_det_Data odata_det = new ct_cbtecble_Plantilla_det_Data();

        public List<ct_cbtecble_Plantilla_det_Info> GetList(int IdEmpresa, decimal IdPlantilla)
        {
            try
            {
                return odata_det.get_list(IdEmpresa, IdPlantilla);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

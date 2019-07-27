using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_TipoFlujo_PlantillaDet_Bus
    {
        ba_TipoFlujo_PlantillaDet_Data odata_det = new ba_TipoFlujo_PlantillaDet_Data();

        public List<ba_TipoFlujo_PlantillaDet_Info> GetList(int IdEmpresa, decimal IdPlantilla)
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

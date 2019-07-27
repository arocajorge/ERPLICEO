using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Inventario
{
    public class in_ConsignacionDet_Bus
    {
        in_ConsignacionDet_Data odata_det = new in_ConsignacionDet_Data();

        public List<in_ConsignacionDet_Info> GetList(int IdEmpresa, int IdConsignacion)
        {
            try
            {
                return odata_det.GetList(IdEmpresa, IdConsignacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

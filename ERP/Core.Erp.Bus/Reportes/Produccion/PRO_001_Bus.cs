using Core.Erp.Data.Reportes.Produccion;
using Core.Erp.Info.Reportes.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Produccion
{
    public class PRO_001_Bus
    {
        PRO_001_Data odata = new PRO_001_Data();
        public List<PRO_001_Info> GetList(int IdEmpresa, decimal IdFabricacion)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdFabricacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

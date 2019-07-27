using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Inventario
{
   public class INV_018_Bus
    {
        INV_018_Data odata = new INV_018_Data();
        public List<INV_018_Info> GetList(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAjuste);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

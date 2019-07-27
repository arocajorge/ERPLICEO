using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus
{
    public class in_AjusteDet_Bus
    {
        in_AjusteDet_Data oData_det = new in_AjusteDet_Data();

        public List<in_AjusteDet_Info> GetList(int IdEmpresa, int IdAjuste)
        {
            try
            {
                return oData_det.GetList(IdEmpresa, IdAjuste);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

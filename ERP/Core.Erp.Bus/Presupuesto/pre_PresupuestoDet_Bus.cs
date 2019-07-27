using Core.Erp.Data.Presupuesto;
using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Presupuesto
{
    public class pre_PresupuestoDet_Bus
    {
        pre_PresupuestoDet_Data oData_det = new pre_PresupuestoDet_Data();

        public List<pre_PresupuestoDet_Info> GetList(int IdEmpresa, int IdPresupuesto)
        {
            try
            {
                return oData_det.GetList(IdEmpresa, IdPresupuesto);
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}

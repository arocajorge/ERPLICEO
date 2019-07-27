using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
 public class ba_Conciliacion_det_Bus
    {
        ba_Conciliacion_det_Data odata = new ba_Conciliacion_det_Data();
        public List<ba_Conciliacion_det_Info> GetList(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdConciliacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

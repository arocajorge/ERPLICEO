using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_AjusteImpuestoRentaDet_Bus
    {
        ro_AjusteImpuestoRentaDet_Data odata = new ro_AjusteImpuestoRentaDet_Data();
        public List<ro_AjusteImpuestoRentaDet_Info> GetList(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAjuste);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_AjusteImpuestoRentaDet_Info get_info(int IdEmpresa, int IdAjuste, int Secuencia)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdAjuste, Secuencia);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

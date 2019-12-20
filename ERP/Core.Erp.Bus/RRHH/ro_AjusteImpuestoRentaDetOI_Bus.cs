using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_AjusteImpuestoRentaDetOI_Bus
    {
        ro_AjusteImpuestoRentaDetOI_Data odata = new ro_AjusteImpuestoRentaDetOI_Data();
        public List<ro_AjusteImpuestoRentaDetOI_Info> GetList(int IdEmpresa, decimal IdAjuste, decimal IdEmpleado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAjuste, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                return odata.GuardarBD(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

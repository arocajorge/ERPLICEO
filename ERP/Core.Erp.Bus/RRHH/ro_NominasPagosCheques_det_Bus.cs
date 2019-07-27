using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
  public  class ro_NominasPagosCheques_det_Bus
    {
        ro_NominasPagosCheques_det_Data odata = new ro_NominasPagosCheques_det_Data();
        public List<ro_NominasPagosCheques_det_Info> get_list(int IdEmpresa, decimal IdTransaccion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTransaccion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_NominasPagosCheques_det_Info> get_list(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiqui, int IdPeriodo, string TipoCuenta)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNominaTipo, IdNominaTipoLiqui, IdPeriodo, TipoCuenta);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_AjusteImpuestoRenta_Bus
    {
        ro_AjusteImpuestoRenta_Data odata = new ro_AjusteImpuestoRenta_Data();
        public List<ro_AjusteImpuestoRenta_Info> get_list(int IdEmpresa, int IdAnio, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, estado);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ro_AjusteImpuestoRenta_Info get_info(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdAjuste);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ProcesarDB(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                return odata.procesarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

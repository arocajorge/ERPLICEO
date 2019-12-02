using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_EmpleadoAnticipoBeneficio_Bus
    {
        ro_EmpleadoAnticipoBeneficio_Data odata = new ro_EmpleadoAnticipoBeneficio_Data();
        public List<ro_EmpleadoAnticipoBeneficio_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaIni, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_EmpleadoAnticipoBeneficio_Info GetInfo(int IdEmpresa, decimal IdAnticipo)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdAnticipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(ro_EmpleadoAnticipoBeneficio_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(ro_EmpleadoAnticipoBeneficio_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(ro_EmpleadoAnticipoBeneficio_Info info)
        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

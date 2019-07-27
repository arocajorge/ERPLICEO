using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_HorasProfesores_Bus
    {
        ro_HorasProfesores_Data odata = new ro_HorasProfesores_Data();
        public List<ro_HorasProfesores_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, FechaInicio, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(ro_HorasProfesores_Info info)
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

        public bool AnularDB(ro_HorasProfesores_Info info)
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


        public ro_HorasProfesores_Info get_info(int IdEmpresa, decimal IdCarga)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCarga);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

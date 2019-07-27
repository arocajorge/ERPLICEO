using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_SancionesPorMarcaciones_Bus
    {
        ro_SancionesPorMarcaciones_Data odata = new ro_SancionesPorMarcaciones_Data();
        public bool guardarDB(ro_SancionesPorMarcaciones_Info info)
        {
            try
            {

                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_SancionesPorMarcaciones_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, FechaInicio,FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_SancionesPorMarcaciones_Info info)
        {
            try
            {

                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_SancionesPorMarcaciones_Info info)
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


        public ro_SancionesPorMarcaciones_Info get_info(int IdEmpresa, int IdAjuste)
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
    }
}

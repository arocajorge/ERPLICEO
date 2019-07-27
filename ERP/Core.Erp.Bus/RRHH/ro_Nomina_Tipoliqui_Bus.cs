using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_Nomina_Tipoliquiliqui_Bus
    {
        ro_Nomina_Tipoliqui_Data odata = new ro_Nomina_Tipoliqui_Data();
        public List<ro_Nomina_Tipoliqui_Info> get_list(int IdEmpresa, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, estado);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_Nomina_Tipoliqui_Info> get_list(int IdEmpresa, int IdNomina)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNomina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_Nomina_Tipoliqui_Info get_info(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiq)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdNominaTipo, IdNominaTipoLiq);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_Nomina_Tipoliqui_Info info)
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

        public bool modificarDB(ro_Nomina_Tipoliqui_Info info)
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

        public bool anularDB(ro_Nomina_Tipoliqui_Info info)
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

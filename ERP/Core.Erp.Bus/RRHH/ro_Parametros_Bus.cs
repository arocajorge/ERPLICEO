using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_Parametros_Bus
    {
        ro_Parametros_Data odata = new ro_Parametros_Data();
        ro_Config_Param_contable_Bus bus_parametros = new ro_Config_Param_contable_Bus();
        ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Bus bus_nomina_sueldo = new ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Bus();
      
        public ro_Parametros_Info get_info(int IdEmpresa)
        {
            try
            {
                ro_Parametros_Info info = null;
                info= odata.get_info(IdEmpresa);
                info.lst_cta_x_rubros = bus_parametros.get_list(IdEmpresa);
                info.lst_cta_x_sueldo_pagar = bus_nomina_sueldo.get_list(IdEmpresa);
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_Parametros_Info info)
        {
            try
            {
                if (odata.si_existe(info.IdEmpresa))
                     odata.modificarDB(info);
                else
                     odata.guardarDB(info);
                //bus_parametros.anularDB(info.IdEmpresa);
                //bus_parametros.guardarDB(info.lst_cta_x_rubros);
                //bus_nomina_sueldo.anularDB(info.IdEmpresa);
                //info.lst_cta_x_sueldo_pagar.ForEach(v=>v.IdEmpresa=info.IdEmpresa);
                //bus_nomina_sueldo.guardarDB(info.lst_cta_x_sueldo_pagar);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_Parametros_Info info)
        {
            try
            {
                odata.modificarDB(info);
                //bus_parametros.anularDB(info.IdEmpresa);
                //bus_parametros.guardarDB(info.lst_cta_x_rubros);
                //bus_nomina_sueldo.anularDB(info.IdEmpresa);
                //info.lst_cta_x_sueldo_pagar.ForEach(v => v.IdEmpresa = info.IdEmpresa);
                //bus_nomina_sueldo.guardarDB(info.lst_cta_x_sueldo_pagar);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

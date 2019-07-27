using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_periodo_x_ro_Nomina_TipoLiqui_Bus
    {
        ro_periodo_x_ro_Nomina_TipoLiqui_Data odata = new ro_periodo_x_ro_Nomina_TipoLiqui_Data();
        public List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> get_list(int IdEmpresa,int IdNominaTipo, int IdNominaTipoLiq)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNominaTipo,IdNominaTipoLiq);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> get_list_peridos(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiq)
        {
            try
            {
                return odata.get_list_peridos(IdEmpresa, IdNominaTipo, IdNominaTipoLiq);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> get_list_utimo_periodo_aprocesar(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiq)
        {
            try
            {
                return odata.get_list_utimo_periodo_aprocesar(IdEmpresa, IdNominaTipo, IdNominaTipoLiq);
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        public ro_periodo_x_ro_Nomina_TipoLiqui_Info get_info(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiq, int IdPeriodo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdNominaTipo, IdNominaTipoLiq, IdPeriodo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> info, int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiq)
        {
            try
            {
                return odata.guardarDB(info, IdEmpresa,IdNominaTipo,IdNominaTipoLiq);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_periodo_x_ro_Nomina_TipoLiqui_Info info)
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


        public bool anularDB(ro_periodo_x_ro_Nomina_TipoLiqui_Info info)
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

        public int get_siguinte_periodo_a_procesar(int IdEmpresa, int IdNomina_Tipo, int IdNomina_TipoLiqui)
        {
            try
            {
                return odata.get_siguinte_periodo_a_procesar(IdEmpresa,IdNomina_Tipo,IdNomina_TipoLiqui);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

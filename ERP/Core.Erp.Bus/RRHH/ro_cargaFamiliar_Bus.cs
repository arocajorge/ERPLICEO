using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public  class ro_cargaFamiliar_Bus
    {
         ro_cargaFamiliar_Data odata = new  ro_cargaFamiliar_Data();
        public List< ro_cargaFamiliar_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_cargaFamiliar_Info> get_list(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_cargaFamiliar_Info get_info(int IdEmpresa, int IdEmpleado, int IdCarga)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdEmpleado, IdCarga);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB( ro_cargaFamiliar_Info info)
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

        public bool modificarDB( ro_cargaFamiliar_Info info)
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

        public bool anularDB( ro_cargaFamiliar_Info info)
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

using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_plancta_nivel_Bus
    {
        ct_plancta_nivel_Data odata = new ct_plancta_nivel_Data();

        public List<ct_plancta_nivel_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public ct_plancta_nivel_Info get_info(int IdEmpresa, int IdNivelCta)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdNivelCta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_plancta_nivel_Info info)
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

        public bool modificarDB(ct_plancta_nivel_Info info)
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

        public bool anularDB(ct_plancta_nivel_Info info)
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

        public bool validar_existe_nivel(int IdEmpresa, int IdNivelCta)
        {
            try
            {
                return odata.validar_existe_nivel(IdEmpresa, IdNivelCta);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

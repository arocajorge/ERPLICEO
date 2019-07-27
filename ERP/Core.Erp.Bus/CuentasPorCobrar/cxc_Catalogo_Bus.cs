using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_Catalogo_Bus
    {
        cxc_Catalogo_Data odata = new cxc_Catalogo_Data();
    
        public List<cxc_Catalogo_Info> get_list(string IdCatalogo_tipo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdCatalogo_tipo, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_Catalogo_Info get_info(string IdCatalogo_tipo, string IdCatalogo)
        {
            try
            {
                return odata.get_info(IdCatalogo_tipo, IdCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_IdCatalogo(string IdCatalogo)
        {
            try
            {
                return odata.validar_existe_IdCatalogo(IdCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cxc_Catalogo_Info info)
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

        public bool modificarDB(cxc_Catalogo_Info info)
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
        
        public bool anularDB(cxc_Catalogo_Info info)
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

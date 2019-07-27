using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_CatalogoTipo_Bus
    {
        cxc_CatalogoTipo_Data odata = new cxc_CatalogoTipo_Data();
    
        public List<cxc_CatalogoTipo_Info> get_list()
        {
            try
            {
                return odata.get_list();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public cxc_CatalogoTipo_Info get_info(string IdCatalogo_tipo)
        {
            try
            {
                return odata.get_info(IdCatalogo_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cxc_CatalogoTipo_Info info)
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
        public bool modificarDB(cxc_CatalogoTipo_Info info)
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
        public bool validar_existe_IdCatalogotipo(string IdCatalogo_tipo)
        {
            try
            {
                return odata.validar_existe_IdCatalogotipo(IdCatalogo_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        }
    }

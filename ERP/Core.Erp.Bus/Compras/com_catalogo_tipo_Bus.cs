using Core.Erp.Data.Compras;
using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Compras
{
    public class com_catalogo_tipo_Bus
    {
        com_catalogo_tipo_Data odata = new com_catalogo_tipo_Data();
        public List<com_catalogo_tipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public com_catalogo_tipo_Info get_info(string IdCatalogocompra_tipo)
        {
            try
            {
                return odata.get_info(IdCatalogocompra_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_IdCatalogotipo(string IdCatalogocompra_tipo)
        {
            try
            {
                return odata.validar_existe_IdCatalogotipo(IdCatalogocompra_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(com_catalogo_tipo_Info info)
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
        public bool modificarDB(com_catalogo_tipo_Info info)
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
        public bool anularDB(com_catalogo_tipo_Info info)
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

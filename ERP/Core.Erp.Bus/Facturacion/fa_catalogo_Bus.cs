using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
  public  class fa_catalogo_Bus
    {
        fa_catalogo_Data odata = new fa_catalogo_Data();
        public List<fa_catalogo_Info> get_list(int IdCatalogo_tipo, bool mostrar_anulados)
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

        public fa_catalogo_Info get_info(string IdCatalogo)
        {
            try
            {
                return odata.get_info(IdCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_catalogo_Info info)
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
        public bool modificarDB(fa_catalogo_Info info)
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
        public bool anularDB(fa_catalogo_Info info)
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
    }
}

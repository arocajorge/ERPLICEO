using Core.Erp.Data.Importacion;
using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Importacion
{
    public class imp_catalogo_Bus
    {
        imp_catalogo_Data odata = new imp_catalogo_Data();
    
        public List<imp_catalogo_Info> get_list(int IdCatalogo_tipo)
        {
            try
            {
                return odata.get_list(IdCatalogo_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_catalogo_Info get_info(int IdCatalogo_tipo, int IdCatalogo)
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

        public bool guardarDB(imp_catalogo_Info info)
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
        public bool modificarDB(imp_catalogo_Info info)
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
        public bool anularDB(imp_catalogo_Info info)
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

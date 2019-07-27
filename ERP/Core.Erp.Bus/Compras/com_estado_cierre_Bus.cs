using Core.Erp.Data.Compras;
using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Compras
{
    public class com_estado_cierre_Bus
    {
        com_estado_cierre_Data odata = new com_estado_cierre_Data();
        public List<com_estado_cierre_Info> get_list(bool mostrar_anulados)
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

        public com_estado_cierre_Info get_info(string IdEstado_cierre)
        {
            try
            {
                return odata.get_info(IdEstado_cierre);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_IdEstado(string IdEstado_cierre)
        {
            try
            {
                return odata.validar_existe_IdEstado(IdEstado_cierre);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(com_estado_cierre_Info info)
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

        public bool modificarDB(com_estado_cierre_Info info)
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

        public bool anularDB(com_estado_cierre_Info info)
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

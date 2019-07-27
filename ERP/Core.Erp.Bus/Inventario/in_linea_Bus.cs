using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_linea_Bus
    {
        in_linea_Data odata = new in_linea_Data();
        public List<in_linea_Info> get_list(int IdEmpresa, string IdCategoria, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCategoria, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_linea_Info get_info(int IdEmpresa, string IdCategoria, int IdLinea)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCategoria, IdLinea);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_linea_Info info)
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
        public bool modificarDB(in_linea_Info info)
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
        public bool anularDB(in_linea_Info info)
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

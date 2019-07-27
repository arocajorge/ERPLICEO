using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_ProductoTipo_Bus
    {
        in_ProductoTipo_Data odata = new in_ProductoTipo_Data();

        public List<in_ProductoTipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public in_ProductoTipo_Info get_info(int IdEmpresa, int IdProductoTipo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdProductoTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_ProductoTipo_Info info)
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

        public bool modificarDB(in_ProductoTipo_Info info)
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

        public bool anularDB(in_ProductoTipo_Info info)
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

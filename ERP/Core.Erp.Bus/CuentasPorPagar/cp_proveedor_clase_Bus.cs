using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorPagar
{

    public class cp_proveedor_clase_Bus
    { 
        cp_proveedor_clase_Data odata = new cp_proveedor_clase_Data();

        public List<cp_proveedor_clase_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cp_proveedor_clase_Info get_info(int IdEmpresa, int IdClaseProveedor)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdClaseProveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cp_proveedor_clase_Info info)
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

        public bool modificarDB(cp_proveedor_clase_Info info)
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

        public bool anularDB(cp_proveedor_clase_Info info)
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

        public int get_id(int IdEmpresa)
        {
            try
            {
                try
                {
                    return odata.get_id(IdEmpresa);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

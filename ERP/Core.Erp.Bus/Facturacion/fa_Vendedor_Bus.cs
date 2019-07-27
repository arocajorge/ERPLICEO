using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_Vendedor_Bus
    {
        fa_Vendedor_Data odata = new fa_Vendedor_Data();
    
        public List<fa_Vendedor_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public fa_Vendedor_Info get_info(int IdEmpresa, int IdVendedor)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdVendedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_Vendedor_Info info)
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
        public bool modificarDB(fa_Vendedor_Info info)
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
        public bool anularDB(fa_Vendedor_Info info)
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

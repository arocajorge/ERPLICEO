using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public  class fa_cliente_contactos_Bus
    {
        fa_cliente_contactos_Data odata = new fa_cliente_contactos_Data();
    
        public List<fa_cliente_contactos_Info> get_list(int IdEmpresa, decimal IdCliente)
        {
            try
            {
                return odata.get_list( IdEmpresa, IdCliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_cliente_contactos_Info get_info(int IdEmpresa, decimal IdCliente, int IdContacto)
        {
            try
            {
                return odata.get_info(IdEmpresa , IdCliente, IdContacto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_cliente_contactos_Info info)
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
    }
}

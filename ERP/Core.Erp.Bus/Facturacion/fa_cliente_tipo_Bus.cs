using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_cliente_tipo_Bus
    {
        fa_cliente_tipo_Data odata = new fa_cliente_tipo_Data();
     
        public List<fa_cliente_tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public fa_cliente_tipo_Info get_info(int IdEmpresa, int Idtipo_cliente)
        {
            try
            {
                return odata.get_info(IdEmpresa, Idtipo_cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_cliente_tipo_Info info)
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
        public bool modificarDB(fa_cliente_tipo_Info info)
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
        public bool anularDB(fa_cliente_tipo_Info info)
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

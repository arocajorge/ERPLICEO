using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_PuntoVta_Bus
    {
        fa_PuntoVta_Data odata = new fa_PuntoVta_Data();

        public List<fa_PuntoVta_Info> get_list(int IdEmpresa, int IdSucursal, string CodDocumentoTipo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, CodDocumentoTipo, mostrar_anulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_PuntoVta_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, mostrar_anulados);
            }
            catch (Exception)
            {
                throw;
            }
        }
            public fa_PuntoVta_Info get_info(int IdEmpresa, int IdSucursal, int IdPuntoVta)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_PuntoVta_Info info)
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
        public bool modificarDB(fa_PuntoVta_Info info)
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
        public bool anularDB(fa_PuntoVta_Info info)
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

        public List<fa_PuntoVta_Info> get_list_x_tipo_doc(int IdEmpresa, int IdSucursal, string codDocumentoTipo)
        {
            try
            {
                return odata.get_list_x_tipo_doc(IdEmpresa, IdSucursal, codDocumentoTipo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

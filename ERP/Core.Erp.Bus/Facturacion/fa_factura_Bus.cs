using Core.Erp.Bus.General;
using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_factura_Bus
    {
        fa_factura_Data odata = new fa_factura_Data();
        public List<fa_factura_consulta_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, Fecha_ini,Fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<fa_factura_Info> get_list_fac_sin_guia(int IdEmpresa, decimal IdCliente)
        {
            try
            {
                return odata.get_list_fac_sin_guia(IdEmpresa, IdCliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_factura_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool factura_existe(int IdEmpresa, string Serie1, string Serie2, string NumFactura)
        {
            try
            {
                return odata.factura_existe(IdEmpresa, Serie1, Serie2, NumFactura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_factura_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_factura_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarEstadoImpresion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, bool estado_impresion)
        {
            try
            {
                return odata.modificarEstadoImpresion(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, estado_impresion);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(fa_factura_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_factura_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(fa_factura_Info info)
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

        public bool ValidarCarteraVencida(int IdEmpresa, decimal IdCliente, ref string mensaje)
        {
            try
            {
                return odata.ValidarCarteraVencida(IdEmpresa, IdCliente, ref mensaje);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool MostrarCuotasRpt(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                return odata.MostrarCuotasRpt(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Contabilizar(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string NombreContacto)
        {
            try
            {
                return odata.Contabilizar(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, NombreContacto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarDocumentoAnulacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string vt_tipoDoc, ref string mensaje)
        {
            try
            {
                return odata.ValidarDocumentoAnulacion(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, vt_tipoDoc, ref mensaje);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarEstadoAutorizacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                return odata.modificarEstadoAutorizacion(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            }
            catch (Exception)
            {

                throw;
            }
        }
        }
}

using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Banco
{
    public class ba_Banco_Cuenta_Bus
    {
        ba_Banco_Cuenta_Data odata = new ba_Banco_Cuenta_Data();

        public List<ba_Banco_Cuenta_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
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
        public bool GuardarDisenioDB(int IdEmpresa, int IdBanco, byte[] Disenio)
        {
            try
            {
                return odata.GuardarDisenioDB(IdEmpresa, IdBanco, Disenio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidarSaldoCuenta(int IdEmpresa, string IdCtaCble, double Valor)
        {
            try
            {
                return odata.ValidarSaldoCuenta(IdEmpresa, IdCtaCble, Valor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_Banco_Cuenta_Info get_info(int IdEmpresa, int idBanco)
        {
            try
            {
                return odata.get_info(IdEmpresa, idBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_Banco_Cuenta_Info info)
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
        public bool modificarDB(ba_Banco_Cuenta_Info info)
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

        public bool anularDB(ba_Banco_Cuenta_Info info)
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

        public bool GuardarDbImportacion(List<ba_Banco_Cuenta_Info> Lista_Banco)
        {
            try
            {
                return odata.GuardarDbImportacion(Lista_Banco);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

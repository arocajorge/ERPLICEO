using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_catalogo_Bus
    {
        ro_catalogo_Data odata = new ro_catalogo_Data();
        public List<ro_catalogo_Info> get_list( bool estado)
        {
            try
            {
                return odata.get_list(estado);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_catalogo_Info> get_list_x_tipo(int IdTipoCatalogo)
        {
            try
            {
                return odata.get_list_x_tipo(IdTipoCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_catalogo_Info> get_list_x_tipo(int IdTipoCatalogo, bool MostrarAnulados)
        {
            try
            {
                return odata.get_list_x_tipo(IdTipoCatalogo, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_catalogo_Info get_info(int IdEmpresa, int IdCargo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCargo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_catalogo_Info get_info(int IdEmpresa, string CodCatalogo)
        {
            try
            {
                return odata.get_info(IdEmpresa, CodCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_catalogo_Info info)
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

        public bool modificarDB(ro_catalogo_Info info)
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

        public bool anularDB(ro_catalogo_Info info)
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
        public bool si_existe_codigo(string CodCatalogo)
        {
            try
            {
                return odata.si_existe_codigo(CodCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

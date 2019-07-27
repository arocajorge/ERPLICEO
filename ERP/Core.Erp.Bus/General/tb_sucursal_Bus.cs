using Core.Erp.Data.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_sucursal_Bus
    {
        tb_sucursal_Data odata = new tb_sucursal_Data();
        public List<tb_sucursal_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa,mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_sucursal_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa);
        }

        public tb_sucursal_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            return odata.get_info_bajo_demanda( IdEmpresa, args);
        }

        public tb_sucursal_Info get_info(int IdEmpresa, int IdSucursal)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_sucursal_Info info)
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

        public bool modificarDB(tb_sucursal_Info info)
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

        public bool anularDB(tb_sucursal_Info info)
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
        public tb_sucursal_Info GetInfo(int IdEmpresa, string CodigoEstablecimiento)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, CodigoEstablecimiento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_sucursal_Info> GetList(int IdEmpresa, string IdUsuario, bool MostrarTodos)

        {
            try
            {
                return odata.GetList(IdEmpresa, IdUsuario, MostrarTodos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_sucursal_Info> GetListSinEmpresa(bool mostrar_anulados)

        {
            try
            {
                return odata.GetListSinEmpresa(mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

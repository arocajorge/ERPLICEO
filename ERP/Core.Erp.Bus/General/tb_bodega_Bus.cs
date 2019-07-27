using Core.Erp.Data.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_bodega_Bus
    {
        tb_bodega_Data odata = new tb_bodega_Data();

        public List<tb_bodega_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
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

        public List<tb_bodega_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public tb_bodega_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdBodega);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(tb_bodega_Info info)
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

        public bool modificarDB(tb_bodega_Info info)
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

        public bool anularDB(tb_bodega_Info info)
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





        public List<tb_bodega_Info> get_list_demanda(int IdEmpresa, int skip, int take, string filter, bool MostrarAnulados, int IdSucursal)
        {
            try
            {
                return odata.get_list_demanda(IdEmpresa, skip, take, filter, MostrarAnulados, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_bodega_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool estado, int IdSucursal)

        {
            try
            {
                return odata.get_list_bajo_demanda(args, IdEmpresa, estado, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_bodega_Info get_info_demanda(int IdEmpresa, int IdBodega, int IdSucursal)

        {
            try
            {
                return odata.get_info_demanda(IdEmpresa, IdBodega, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_bodega_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, int IdSucursal)

        {
            try
            {
                return odata.get_info_bajo_demanda(args, IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

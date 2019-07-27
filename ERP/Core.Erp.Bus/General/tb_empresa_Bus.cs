using Core.Erp.Data.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_empresa_Bus        
    {
        tb_empresa_Data odata = new tb_empresa_Data();

        public List<tb_empresa_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_empresa_Info get_info(int IdEmpresa)
        {
            try
            {
                return odata.get_info(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_empresa_Info info)
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

        public bool modificarDB(tb_empresa_Info info)
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
        public bool anularDB(tb_empresa_Info info)
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
        public bool GuardarDbImportacion(List<tb_empresa_Info> Lista_Empresa, List<tb_sucursal_Info> Lista_Sucursal, List<tb_bodega_Info> Lista_Bodega)
        {
            try
            {
                return odata.GuardarDbImportacion(Lista_Empresa, Lista_Sucursal, Lista_Bodega);
            }   
            catch (Exception)
            {

                throw;
            }
        }

        public tb_empresa_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            try
            {
                return odata.get_info_bajo_demanda(args);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<tb_empresa_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            try
            {
                return odata.get_list_bajo_demanda(args);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

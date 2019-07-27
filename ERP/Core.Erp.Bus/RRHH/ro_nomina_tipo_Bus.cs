using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using DevExpress.Web;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_nomina_tipo_Bus
    {
        ro_nomina_tipo_Data odata = new ro_nomina_tipo_Data();
        public List<ro_nomina_tipo_Info> get_list(int IdEmpresa, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, estado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_nomina_tipo_Info get_info(int IdEmpresa, int IdNominaTipo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdNominaTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_nomina_tipo_Info info)
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

        public bool modificarDB(ro_nomina_tipo_Info info)
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

        public bool anularDB(ro_nomina_tipo_Info info)
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

        public List<ro_nomina_tipo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa);
        }

        public ro_nomina_tipo_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            return odata.get_info_bajo_demanda(IdEmpresa, args);
        }
    }
}

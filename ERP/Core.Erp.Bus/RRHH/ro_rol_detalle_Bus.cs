using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public  class ro_rol_detalle_Bus
    {
        ro_rol_detalle_Data odata = new ro_rol_detalle_Data();
        public List<ro_rol_detalle_Info> Get_lst_detalle_contabilizar(int idEmpresa, int idNominaTipo, int idNominaTipoLiqui, int idPeriodo, int IdRl,  bool es_provision)
        {
            try
            {
                return odata.Get_lst_detalle_contabilizar(idEmpresa,idNominaTipo,idNominaTipoLiqui,idPeriodo,IdRl, es_provision);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_rol_detalle_Info> get_list_nomina_sin_sueldo_percibir(int idEmpresa, decimal IdRol)
        {
            try
            {
                return odata.get_list_nomina_sin_sueldo_percibir(idEmpresa, IdRol);
            }
            catch (Exception)
            {

                throw;
            }
        }

   
        public List<ro_rol_detalle_Info> get_list_ajustar_anticipo(int idEmpresa, int IdSucursal, int idNominaTipo, int idNominaTipoLiqui, int idPeriodo)
        {
            try
            {
                return odata.get_list_ajustar_anticipo(idEmpresa,IdSucursal, idNominaTipo, idNominaTipoLiqui, idPeriodo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ajustar_anticipo(List<ro_rol_detalle_Info> lista)
        {
            try
            {
                return odata.ajustar_anticipo(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

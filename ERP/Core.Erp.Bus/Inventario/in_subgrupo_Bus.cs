using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_subgrupo_Bus
    {
        in_subgrupo_Data odata = new in_subgrupo_Data();
        public List<in_subgrupo_Info> get_list(int IdEmpresa, string IdCategoria, int IdLinea, int IdGrupo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCategoria, IdLinea, IdGrupo, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public in_subgrupo_Info get_info(int IdEmpresa, string IdCategoria, int IdLinea, int IdGrupo, int IdSubgrupo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCategoria, IdLinea, IdGrupo, IdSubgrupo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_subgrupo_Info info)
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
        public bool modificarDB(in_subgrupo_Info info)
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
        public bool anularDB(in_subgrupo_Info info)
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

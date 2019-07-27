using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_grupo_Bus
    {
        in_grupo_Data odata = new in_grupo_Data();
        public List<in_grupo_Info> get_list(int IdEmpresa, string IdCategoria, int IdLinea, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCategoria, IdLinea, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_grupo_Info get_info(int IdEmpresa, string IdCategoria, int IdLinea, int IdGrupo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCategoria, IdLinea, IdGrupo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_grupo_Info info)
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

        public bool modificarDB(in_grupo_Info info)
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

        public bool anularDB(in_grupo_Info info)
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

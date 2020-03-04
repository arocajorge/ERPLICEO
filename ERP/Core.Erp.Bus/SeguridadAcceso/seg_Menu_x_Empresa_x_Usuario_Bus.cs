using Core.Erp.Data.SeguridadAcceso;
using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.SeguridadAcceso
{
    public class seg_Menu_x_Empresa_x_Usuario_Bus
    {
        seg_Menu_x_Empresa_x_Usuario_Data odata = new seg_Menu_x_Empresa_x_Usuario_Data();

        public List<seg_Menu_x_Empresa_x_Usuario_Info> get_list(int IdEmpresa, string IdUsuario, bool MostrarTodo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdUsuario, MostrarTodo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<seg_Menu_x_Empresa_x_Usuario_Info> get_list(int IdEmpresa, string IdUsuario, int IdMenuPadre)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdUsuario, IdMenuPadre);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.eliminarDB(IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(List<seg_Menu_x_Empresa_x_Usuario_Info> Lista, int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.guardarDB(Lista, IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

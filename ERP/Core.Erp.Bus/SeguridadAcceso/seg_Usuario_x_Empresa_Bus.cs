using Core.Erp.Data.SeguridadAcceso;
using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.SeguridadAcceso
{
    public class seg_Usuario_x_Empresa_Bus
    {
        seg_Usuario_x_Empresa_Data odata = new seg_Usuario_x_Empresa_Data();

        public List<seg_Usuario_x_Empresa_Info> get_list(string IdUsuario)
        {
            try
            {
                return odata.get_list(IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<seg_Usuario_x_Empresa_Info> Lista)
        {
            try
            {
                return odata.guardarDB(Lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(string IdUsuario)
        {
            try
            {
                return odata.eliminarDB(IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

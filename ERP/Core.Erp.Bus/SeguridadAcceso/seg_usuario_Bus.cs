using Core.Erp.Data.SeguridadAcceso;
using Core.Erp.Info.Helps;
using Core.Erp.Info.SeguridadAcceso;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.SeguridadAcceso
{
    public class seg_usuario_Bus
    {
        seg_usuario_Data odata = new seg_usuario_Data();

        public List<seg_usuario_Info> get_list(bool mostrar_anulados)
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

        public bool modificarDB(string IdUsuario, string old_Contrasena, string new_Contrasena)
        {
            try
            {
                old_Contrasena = cl_funciones.convertir_string_MD5Hash(old_Contrasena == null ? "" : old_Contrasena);
                new_Contrasena = cl_funciones.convertir_string_MD5Hash(new_Contrasena == null ? "" : new_Contrasena);
                return odata.modificarDB(IdUsuario, old_Contrasena, new_Contrasena);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public seg_usuario_Info validar_login(string IdUsuario, string contrasena)
        {
            try
            {
                contrasena = cl_funciones.convertir_string_MD5Hash(contrasena == null ? "" : contrasena);
                return odata.validar_login(IdUsuario, contrasena);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_usuario(string IdUsuario)
        {
            try
            {                
                return odata.validar_existe_usuario(IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public seg_usuario_Info get_info(string IdUsuario)
        {
            try
            {
                return odata.get_info(IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(seg_usuario_Info info)
        {
            try
            {
                info.Contrasena = "1234";
                info.Contrasena = cl_funciones.convertir_string_MD5Hash(info.Contrasena);
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(seg_usuario_Info info)
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

        public bool anularDB(seg_usuario_Info info)
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

        public bool ResetearContrasenia(string IdUsuario, string Contrasena)
        {
            try
            {
                Contrasena = cl_funciones.convertir_string_MD5Hash(Contrasena == null ? "" : Contrasena);
                return odata.ResetearContrasenia(IdUsuario, Contrasena);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<seg_usuario_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
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
        public seg_usuario_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
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
    }
}

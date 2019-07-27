using Core.Erp.Data.SeguridadAcceso;
using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.SeguridadAcceso
{
    public class seg_Menu_Bus
    {
        seg_Menu_Data odata = new seg_Menu_Data();

        public List<seg_Menu_Info> get_list(bool mostrar_anulados)
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

        public List<seg_Menu_Info> get_list_combo(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list_combo(mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public seg_Menu_Info get_info(int IdMenu)
        {
            try
            {
                return odata.get_info(IdMenu);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(seg_Menu_Info info)
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

        public bool modificarDB(seg_Menu_Info info)
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

        public bool anularDB(seg_Menu_Info info)
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

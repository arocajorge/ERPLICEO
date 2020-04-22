using Core.Web.Data.General;
using Core.Web.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Web.Bus.General
{
    public class tb_profesion_Bus
    {
        tb_profesion_Data odata = new tb_profesion_Data();
        public List<tb_profesion_Info> GetList(bool MostrarAnulados)
        {
            try
            {
                return odata.getList(MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_profesion_Info GetInfo(int IdProfesion)
        {
            try
            {
                return odata.getInfo(IdProfesion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(tb_profesion_Info info)
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

        public bool ModificarDB(tb_profesion_Info info)
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

        public bool AnularDB(tb_profesion_Info info)
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

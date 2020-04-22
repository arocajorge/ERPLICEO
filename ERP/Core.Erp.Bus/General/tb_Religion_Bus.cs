using Core.Web.Data.General;
using Core.Web.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Web.Bus.General
{
    public class tb_Religion_Bus
    {
        tb_Religion_Data odata = new tb_Religion_Data();
        public List<tb_Religion_Info> GetList(bool MostrarAnulados)
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

        public tb_Religion_Info GetInfo(int IdReligion)
        {
            try
            {
                return odata.getInfo(IdReligion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(tb_Religion_Info info)
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

        public bool ModificarDB(tb_Religion_Info info)
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

        public bool AnularDB(tb_Religion_Info info)
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

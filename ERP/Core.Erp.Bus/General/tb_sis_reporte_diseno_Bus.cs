using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.General;
using Core.Erp.Data.General;
namespace Core.Erp.Bus.General
{
    class tb_sis_reporte_diseno_Bus
    {
        tb_sis_reporte_diseno_Data odata = new tb_sis_reporte_diseno_Data();
        public List<tb_sis_reporte_diseno_Info> get_list(bool mostrar_anulados)
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
        public tb_sis_reporte_diseno_Info get_info(string IdPais)
        {
            try
            {
                return odata.get_info(IdPais);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_sis_reporte_diseno_Info info)
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
        public bool modificarDB(tb_sis_reporte_diseno_Info info)
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
        public bool anularDB(tb_sis_reporte_diseno_Info info)
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

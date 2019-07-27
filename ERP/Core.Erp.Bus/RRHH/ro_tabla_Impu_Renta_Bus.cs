using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_tabla_Impu_Renta_Bus
    {
        ro_tabla_Impu_Renta_Data odata = new ro_tabla_Impu_Renta_Data();
        public List<ro_tabla_Impu_Renta_Info> get_list()
        {
            try
            {
                return odata.get_list();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_tabla_Impu_Renta_Info get_info(int IdEmpresa, int IdDivision)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdDivision);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_tabla_Impu_Renta_Info info)
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

        public bool modificarDB(ro_tabla_Impu_Renta_Info info)
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

        public bool anularDB(ro_tabla_Impu_Renta_Info info)
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

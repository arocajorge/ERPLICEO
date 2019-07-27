using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_permiso_x_empleado_Bus
    {
        ro_permiso_x_empleado_Data odata = new ro_permiso_x_empleado_Data();
        public List<ro_permiso_x_empleado_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_permiso_x_empleado_Info get_info(int IdEmpresa, decimal IdPermiso)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdPermiso);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_permiso_x_empleado_Info info)
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

        public bool modificarDB(ro_permiso_x_empleado_Info info)
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

        public bool anularDB(ro_permiso_x_empleado_Info info)
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

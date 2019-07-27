using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_tipo_prestamo_Bus
    {
        ro_tipo_prestamo_Data odata = new ro_tipo_prestamo_Data();
        public List<ro_tipo_prestamo_Info> get_list(int IdEmpresa, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, estado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_tipo_prestamo_Info get_info(int IdEmpresa, int IdtipoPrestamo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdtipoPrestamo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_tipo_prestamo_Info info)
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

        public bool modificarDB(ro_tipo_prestamo_Info info)
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

        public bool anularDB(ro_tipo_prestamo_Info info)
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

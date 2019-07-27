using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public  class ro_participacion_utilidad_Bus
    {
        ro_participacion_utilidad_Data odata = new ro_participacion_utilidad_Data();
        ro_participacion_utilidad_empleado_Data odataDetalle = new ro_participacion_utilidad_empleado_Data();
        ro_participacion_utilidad_Info info_new = new ro_participacion_utilidad_Info();
        public List<ro_participacion_utilidad_Info> get_list(int IdEmpresa, bool estado)
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

        public ro_participacion_utilidad_Info get_info(int IdEmpresa, int IdUtilidad)
        {
            try
            {
                ro_participacion_utilidad_Info info = new ro_participacion_utilidad_Info();
                info= odata.get_info(IdEmpresa, IdUtilidad);
                info.detalle = odataDetalle.get_list(info.IdEmpresa, info.IdUtilidad);
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_participacion_utilidad_Info info)
        {
            try
            { int IdUtilidad = 0;

                info_new = (odata.get_info(info.IdEmpresa, info.IdUtilidad));
                if (info_new == null)
                {
                    if (odata.guardarDB(info, ref IdUtilidad))
                    {
                        odataDetalle.anularDB(info.IdEmpresa, info.IdUtilidad);
                        info.detalle.ForEach(v => v.IdUtilidad = IdUtilidad);
                        return odataDetalle.guardarDB(info.detalle);
                    }
                }
                else
                {
                    info.IdUtilidad = info_new.IdUtilidad;
                    if (odata.modificarDB(info))
                    {
                        odataDetalle.anularDB(info.IdEmpresa, info_new.IdUtilidad);
                        info.detalle.ForEach(v => v.IdUtilidad = info_new.IdUtilidad);
                        return odataDetalle.guardarDB(info.detalle);
                    }

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_participacion_utilidad_Info info)
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

        public bool anularDB(ro_participacion_utilidad_Info info)
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

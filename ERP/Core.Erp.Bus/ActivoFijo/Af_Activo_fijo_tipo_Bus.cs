using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Activo_fijo_tipo_Bus
    {
        Af_Activo_fijo_tipo_Data odata = new Af_Activo_fijo_tipo_Data();
    
        public List<Af_Activo_fijo_tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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

        public Af_Activo_fijo_tipo_Info get_info(int IdEmpresa, int IdActivoFijoTipo)
            {
            try
            {
                return odata.get_info(IdEmpresa, IdActivoFijoTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Activo_fijo_tipo_Info info)
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

        public bool modificarDB(Af_Activo_fijo_tipo_Info info)
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

        public bool anularDB(Af_Activo_fijo_tipo_Info info)
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

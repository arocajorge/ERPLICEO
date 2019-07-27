using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_movi_inven_tipo_Bus
    {
        in_movi_inven_tipo_Data odata = new in_movi_inven_tipo_Data();

        public List<in_movi_inven_tipo_Info> get_list(int IdEmpresa, string Signo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa,Signo, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_movi_inven_tipo_Info get_info(int IdEmpresa, int IdMovi_inven_tipo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdMovi_inven_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_movi_inven_tipo_Info info)
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
        public bool modificarDB(in_movi_inven_tipo_Info info)
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
        public bool anularDB(in_movi_inven_tipo_Info info)
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

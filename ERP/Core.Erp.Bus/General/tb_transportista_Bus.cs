using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_transportista_Bus
    {
        tb_transportista_Data odata = new tb_transportista_Data();

        public List<tb_transportista_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_transportista_Info get_info(int IdEmpresa, decimal IdTransportista)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdTransportista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_transportista_Info info)
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

        public bool modificarDB(tb_transportista_Info info)
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

        public bool anularDB(tb_transportista_Info info)
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

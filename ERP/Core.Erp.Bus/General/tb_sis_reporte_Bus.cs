using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_sis_reporte_Bus
    {
        tb_sis_reporte_Data odata = new tb_sis_reporte_Data();

        public List<tb_sis_reporte_Info> get_list( )
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

        public tb_sis_reporte_Info get_info(string CodReporte)
        {
            try
            {
                return odata.get_info(CodReporte);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodReporte(string CodReporte)
        {
            try
            {
                return odata.validar_existe_CodReporte(CodReporte);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string get_id(string CodModulo)
        {
            try
            {
                return odata.get_id(CodModulo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_sis_reporte_Info info)
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

        public bool modificarDB(tb_sis_reporte_Info info)
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

    }
}

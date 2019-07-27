using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;

namespace Core.Erp.Bus.General
{
    public class tb_sis_reporte_x_tb_empresa_Bus
    {
        tb_sis_reporte_x_tb_empresa_Data odata = new tb_sis_reporte_x_tb_empresa_Data();

        public tb_sis_reporte_x_tb_empresa_Info GetInfo(int IdEmpresa, string CodReporte)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, CodReporte);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(tb_sis_reporte_x_tb_empresa_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

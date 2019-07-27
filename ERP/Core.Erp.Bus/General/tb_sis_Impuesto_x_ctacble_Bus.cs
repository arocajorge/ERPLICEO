using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;

namespace Core.Erp.Bus.General
{
    public class tb_sis_Impuesto_x_ctacble_Bus
    {
        tb_sis_Impuesto_x_ctacble_Data odata = new tb_sis_Impuesto_x_ctacble_Data();

        public tb_sis_Impuesto_x_ctacble_Info get_info(string IdCod_Impuesto, int IdEmpresa)
        {
            try
            {
                return odata.get_info(IdCod_Impuesto, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_sis_Impuesto_x_ctacble_Info info)
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

        public bool eliminarDB(string IdCod_Impuesto, int IdEmpresa)
        {
            try
            {
                return odata.eliminarDB(IdCod_Impuesto, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

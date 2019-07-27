using Core.Erp.Data.Caja;
using Core.Erp.Info.Caja;
using System;

namespace Core.Erp.Bus.Caja
{
    public class caj_Caja_Movimiento_det_Bus
    {
        caj_Caja_Movimiento_det_Data odata = new caj_Caja_Movimiento_det_Data();

        public caj_Caja_Movimiento_det_Info get_info(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public bool guardarDB(caj_Caja_Movimiento_det_Info info)
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

        public bool eliminarDB(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.eliminarDB(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_anio_fiscal_x_cuenta_utilidad_Bus
    {
        ct_anio_fiscal_x_cuenta_utilidad_Data odata = new ct_anio_fiscal_x_cuenta_utilidad_Data();
        public ct_anio_fiscal_x_cuenta_utilidad_Info get_info(int IdEmpresa, int IdanioFiscal)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdanioFiscal);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ct_anio_fiscal_x_cuenta_utilidad_Info info)
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
        public bool eliminarDB(int IdEmpresa, int IdanioFiscal)
        {
            try
            {
                return odata.eliminarDB(IdEmpresa, IdanioFiscal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

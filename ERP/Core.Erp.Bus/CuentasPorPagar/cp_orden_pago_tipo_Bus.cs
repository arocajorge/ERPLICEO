using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_orden_pago_tipo_Bus
    {
        cp_orden_pago_tipo_Data oData = new cp_orden_pago_tipo_Data();

        public List<cp_orden_pago_tipo_Info> get_list()
        {
            try
            {
                return oData.get_list();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(cp_orden_pago_tipo_Info info)
        {
            try
            {
                if (!oData.si_existe(info))
                    return oData.guardarDB(info);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cp_orden_pago_tipo_Info info)
        {
            try
            {
                return oData.modificarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool anularDB(cp_orden_pago_tipo_Info info)
        {
            try
            {
                return oData.anularDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool si_existe(cp_orden_pago_tipo_Info info)
        {
            try
            {
                return oData.si_existe(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

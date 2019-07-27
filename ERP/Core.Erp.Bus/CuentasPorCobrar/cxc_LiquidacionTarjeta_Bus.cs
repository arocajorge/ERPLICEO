using Core.Erp.Bus.General;
using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_Bus
    {
        cxc_LiquidacionTarjeta_Data odata = new cxc_LiquidacionTarjeta_Data();
        public List<cxc_LiquidacionTarjeta_Info> GetList(int IdEmpresa, int IdSucursal, bool MostrarAnulado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, MostrarAnulado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cxc_LiquidacionTarjeta_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_LiquidacionTarjeta_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool modificarDB(cxc_LiquidacionTarjeta_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_LiquidacionTarjeta_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool anularDB(cxc_LiquidacionTarjeta_Info info)
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

        public cxc_LiquidacionTarjeta_Info GetInfo(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {
                cxc_LiquidacionTarjeta_Info info = new cxc_LiquidacionTarjeta_Info();
                info = odata.get_info(IdEmpresa, IdSucursal, IdLiquidacion);

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

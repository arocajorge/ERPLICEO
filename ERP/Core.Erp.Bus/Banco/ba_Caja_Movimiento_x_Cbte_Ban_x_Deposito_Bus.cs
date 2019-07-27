using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Bus
    {
        ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Data odata = new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Data();

        public List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> get_list_x_depositar(int IdEmpresa, int IdSucursal)
        {
            try
            {
                return odata.get_list_x_depositar(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_Banco_Cuenta_x_tb_sucursal_Bus
    {
        ba_Banco_Cuenta_x_tb_sucursal_Data odata = new ba_Banco_Cuenta_x_tb_sucursal_Data();
        public List<ba_Banco_Cuenta_x_tb_sucursal_Info> GetList(int IdEmpresa, int IdBanco)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_Banco_Cuenta_x_tb_sucursal_Info> GetListSuc(int IdEmpresa, int IdSucursal)

        {
            try
            {
                return odata.GetListSuc(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

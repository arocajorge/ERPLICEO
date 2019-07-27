using Core.Erp.Data;
using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.General
{
    public class tb_sucursal_FormaPago_x_fa_NivelDescuento_Bus
    {
        tb_sucursal_FormaPago_x_fa_NivelDescuento_Data odata_detalle = new tb_sucursal_FormaPago_x_fa_NivelDescuento_Data();

        public List<tb_sucursal_FormaPago_x_fa_NivelDescuento_Info> GetList(int IdEmpresa, int IdSucursal)
        {
            try
            {
                return odata_detalle.get_list(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_sucursal_FormaPago_x_fa_NivelDescuento_Info GetInfo(int IdEmpresa, int IdSucursal, string IdCatalogo)
        {
            try
            {
                tb_sucursal_FormaPago_x_fa_NivelDescuento_Info info = new tb_sucursal_FormaPago_x_fa_NivelDescuento_Info();

                info = odata_detalle.get_info(IdEmpresa, IdSucursal, IdCatalogo);

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_TipoNota_x_Empresa_x_Sucursal_Bus
    {
        fa_TipoNota_x_Empresa_x_Sucursal_Data odata = new fa_TipoNota_x_Empresa_x_Sucursal_Data();
        public List<fa_TipoNota_x_Empresa_x_Sucursal_Info> get_list(int IdEmpresa, int IdTipoNota)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoNota);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_TipoNota_x_Empresa_x_Sucursal_Info get_info(int IdEmpresa, int IdTipoNota, int IdSucursal)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdTipoNota,IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

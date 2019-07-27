using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Inventario
{
    public class in_devolucion_inven_det_Bus
    {
        in_devolucion_inven_det_Data odata = new in_devolucion_inven_det_Data();
        public List<in_devolucion_inven_det_Info> get_list_x_movimiento(int IdEmpresa, int IdSucursal, int IdMoviInven_tipo, decimal IdNumMovi)
        {
            try
            {
                return odata.get_list_x_movimiento(IdEmpresa, IdSucursal, IdMoviInven_tipo, IdNumMovi);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<in_devolucion_inven_det_Info> get_list(int IdEmpresa, decimal IdDev_Inven)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdDev_Inven);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

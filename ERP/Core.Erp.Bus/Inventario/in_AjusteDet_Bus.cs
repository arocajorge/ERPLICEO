using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Inventario
{
    public class in_AjusteDet_Bus
    {
        in_AjusteDet_Data odata = new in_AjusteDet_Data();
        public List<in_AjusteDet_Info> get_list(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAjuste);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_AjusteDet_Info> get_list_cargar_detalle(DateTime Fecha, int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0)
        {
            try
            {
                return odata.get_list_cargar_detalle(IdEmpresa, IdSucursal, IdBodega, Fecha);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

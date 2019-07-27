using Core.Erp.Data.Reportes.Importacion;
using Core.Erp.Info.Reportes.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Importacion
{
   public class IMP_002_Bus
    {
        IMP_002_Data odata = new IMP_002_Data();

        IMP_002_gastos_Data odata_gastos = new IMP_002_gastos_Data();
        List<IMP_002_gastos_Info> lst_gastos_asignados = new List<IMP_002_gastos_Info>();

        public List<IMP_002_Info> get_list(int IdEmpresa, int IdOrdenCompra_ext, ref List<IMP_002_gastos_Info> gastos)
        {
            try
            {
                List<IMP_002_Info> lst_detalle = new List<IMP_002_Info>();
                lst_detalle= odata.get_list(IdEmpresa, IdOrdenCompra_ext);
                double costo_incurridos = 0;
                double valor_compra = 0;

                lst_gastos_asignados = odata_gastos.get_list(IdEmpresa, IdOrdenCompra_ext);
                gastos = lst_gastos_asignados;
                if (lst_gastos_asignados != null)
                    costo_incurridos = lst_gastos_asignados.Sum(v => v.dc_Valor);
                if (lst_gastos_asignados != null)
                    valor_compra = Convert.ToDouble(lst_detalle.Sum(v => v.od_total_fob));
                foreach (var item in lst_detalle)
                {
                    item.od_factor_costo = (costo_incurridos + valor_compra) / valor_compra;
                    item.od_costo_bodega = item.od_costo * item.od_factor_costo;
                    item.od_costo_total = item.od_costo_bodega * item.od_cantidad_recepcion;
                    item.od_costo_bodega = item.od_costo_total / item.od_cantidad_recepcion;
                }
                return lst_detalle;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

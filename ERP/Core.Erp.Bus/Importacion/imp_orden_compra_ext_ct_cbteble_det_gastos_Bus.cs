using Core.Erp.Data.Importacion;
using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Importacion
{
   public class imp_orden_compra_ext_ct_cbteble_det_gastos_Bus
    {
        imp_orden_compra_ext_ct_cbteble_det_gastos_Data odata = new imp_orden_compra_ext_ct_cbteble_det_gastos_Data();
        public List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> get_list_gastos_no_asignados(int IdEmpresa, string IdCtaCble)
        {
            try
            {
                return odata.get_list_gastos_no_asignados(IdEmpresa, IdCtaCble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> get_list_gastos_asignados(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                return odata.get_list_gastos_asignados(IdEmpresa, IdOrdenCompra_ext);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(imp_ordencompra_ext_Info info)
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

    }
}

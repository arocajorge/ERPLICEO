using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_006_Bus
    {
        FAC_006_Data odata = new FAC_006_Data();
    
        public List<FAC_006_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdProforma, bool formato_hoja_membretada, bool mostrar_imagen)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdProforma, formato_hoja_membretada, mostrar_imagen);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

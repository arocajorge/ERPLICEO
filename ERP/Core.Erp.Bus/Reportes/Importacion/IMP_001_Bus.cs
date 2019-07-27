using Core.Erp.Data.Reportes.Importacion;
using Core.Erp.Info.Reportes.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Importacion
{
    public class IMP_001_Bus
    {
        IMP_001_Data odata = new IMP_001_Data();
    
        public List<IMP_001_Info> get_list(int IdEmpresa, int IdOrdenCompra_ext)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdOrdenCompra_ext);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

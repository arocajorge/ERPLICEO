using Core.Erp.Data.Reportes.Importacion;
using Core.Erp.Info.Reportes.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Importacion
{
    public class IMP_003_Bus
    {
        IMP_003_Data odata = new IMP_003_Data();
        public List<IMP_003_Info> get_list(int IdEmpresa, string IdPais_embarque, decimal IdProveedor, decimal IdProducto, int IdMarca, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdPais_embarque, IdProveedor, IdProducto, IdMarca, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

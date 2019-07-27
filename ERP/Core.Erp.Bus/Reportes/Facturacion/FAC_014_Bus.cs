using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_014_Bus
    {
        FAC_014_Data odata = new FAC_014_Data();
        public List<FAC_014_Info> GetList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.GetList(IdEmpresa, fecha_ini, fecha_fin);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

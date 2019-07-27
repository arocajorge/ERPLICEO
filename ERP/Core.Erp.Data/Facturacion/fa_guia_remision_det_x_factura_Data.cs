using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
   public class fa_guia_remision_det_x_factura_Data
    {
        public bool eliminar(int IdEmpresa, decimal IdGuiaRemision)
        {
            try
            {
                using (Entities_importacion context = new Entities_importacion())
                {
                    string sql = "delete fa_guia_remision_det_x_factura where IdEmpresa_guia='" + IdEmpresa + "' and IdGuiaRemision_guia='" + IdGuiaRemision + "'";
                    context.Database.ExecuteSqlCommand(sql);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

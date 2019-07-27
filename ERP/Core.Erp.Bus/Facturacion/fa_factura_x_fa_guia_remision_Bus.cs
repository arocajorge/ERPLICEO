using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
  public  class fa_factura_x_fa_guia_remision_Bus
    {
        fa_factura_x_fa_guia_remision_Data odata = new fa_factura_x_fa_guia_remision_Data();
        public List<fa_factura_x_fa_guia_remision_Info> get_list(int IdEmpresa,decimal gi_IdGuiaRemision)
        {
            try
            {
                return odata.get_list(IdEmpresa, gi_IdGuiaRemision);
            }
            catch (Exception)
            {

                throw;
            }
        }

        }
    }

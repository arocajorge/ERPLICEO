using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_TerminoPago_Distribucion_Bus
    {
        fa_TerminoPago_Distribucion_Data odata = new fa_TerminoPago_Distribucion_Data();
    
        public List<fa_TerminoPago_Distribucion_Info> get_list(string IdTerminoPago)
        {
            try
            {
                return odata.get_list(IdTerminoPago);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

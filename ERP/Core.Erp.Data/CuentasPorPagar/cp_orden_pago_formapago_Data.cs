using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_orden_pago_formapago_Data
    {
        public List<cp_orden_pago_formapago_Info> get_list()
        {
            try
            {
                List<cp_orden_pago_formapago_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                        Lista = (from q in Context.cp_orden_pago_formapago
                                 select new cp_orden_pago_formapago_Info
                                 {
                                    IdFormaPago=q.IdFormaPago,
                                    descripcion=q.descripcion,
                                    CodModulo=q.CodModulo,
                                     IdTipoMovi_caj=q.IdTipoMovi_caj,
                                      IdTipoTransaccion=q.IdTipoTransaccion
                                 }).ToList();
                  
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

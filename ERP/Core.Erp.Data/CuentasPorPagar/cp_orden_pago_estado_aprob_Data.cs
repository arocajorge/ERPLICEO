using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_orden_pago_estado_aprob_Data
    {

        public List<cp_orden_pago_estado_aprob_Info> get_list()
        {
            try
            {
                List<cp_orden_pago_estado_aprob_Info> Lst = new List<cp_orden_pago_estado_aprob_Info>();
                Entities_cuentas_por_pagar oEnti = new Entities_cuentas_por_pagar();
                Lst = (from q in oEnti.cp_orden_pago_estado_aprob
                       select new cp_orden_pago_estado_aprob_Info
                       {
                           IdEstadoAprobacion = q.IdEstadoAprobacion,
                           Descripcion = q.Descripcion
                       }).ToList();

                return Lst;

                
    }
            catch (Exception)
            {
                throw;

            }
        }

    }
}

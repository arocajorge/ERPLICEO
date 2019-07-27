using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_pais_sri_Data
    {
        public List<cp_pais_sri_Info> get_list()
        {
            try
            {
                List<cp_pais_sri_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.cp_pais_sri
                             select new cp_pais_sri_Info
                             {
                                 Codigo = q.Codigo,
                                 Pais = q.Pais
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

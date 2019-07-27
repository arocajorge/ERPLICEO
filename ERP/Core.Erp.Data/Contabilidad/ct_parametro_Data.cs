using Core.Erp.Info.Contabilidad;
using System;
using System.Linq;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_parametro_Data
    {
       public ct_parametro_Info get_info(int IdEmpresa)
        {
            try
            {
                ct_parametro_Info info = new ct_parametro_Info();
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_parametro Entity = Context.ct_parametro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new ct_parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbte_AsientoCierre_Anual = Entity.IdTipoCbte_AsientoCierre_Anual,
                        IdTipoCbte_SaldoInicial = Entity.IdTipoCbte_SaldoInicial,
                        P_Se_Muestra_Todas_las_ctas_en_combos = Entity.P_Se_Muestra_Todas_las_ctas_en_combos,
                        DiasTransaccionesAFuturo = Entity.DiasTransaccionesAFuturo
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_parametro_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_parametro Entity = Context.ct_parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new ct_parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdTipoCbte_SaldoInicial = info.IdTipoCbte_SaldoInicial,
                            IdTipoCbte_AsientoCierre_Anual = info.IdTipoCbte_AsientoCierre_Anual,
                            P_Se_Muestra_Todas_las_ctas_en_combos = info.P_Se_Muestra_Todas_las_ctas_en_combos,
                            DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo
                        };
                    Context.ct_parametro.Add(Entity);
                    }
                    else
                    {
                        Entity.IdTipoCbte_SaldoInicial = info.IdTipoCbte_SaldoInicial;
                        Entity.IdTipoCbte_AsientoCierre_Anual = info.IdTipoCbte_AsientoCierre_Anual;
                        Entity.P_Se_Muestra_Todas_las_ctas_en_combos = info.P_Se_Muestra_Todas_las_ctas_en_combos;
                        Entity.DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo;
                    }
                    Context.SaveChanges();
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

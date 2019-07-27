using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
    public class Af_Parametros_Data
    {
        public Af_Parametros_Info get_info(int IdEmpresa)
        {
            try
            {
                Af_Parametros_Info info = new Af_Parametros_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Parametros Entity = Context.Af_Parametros.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new Af_Parametros_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdTipoCbteBaja =Entity.IdTipoCbteBaja,
                        IdTipoCbteMejora = Entity.IdTipoCbteMejora,
                        IdTipoCbteRetiro = Entity.IdTipoCbteRetiro,
                        IdTipoCbteVenta =Entity.IdTipoCbteVenta,
                        DiasTransaccionesAFuturo = Entity.DiasTransaccionesAFuturo,
                        ContabilizaDepreciacionPorActivo = Entity.ContabilizaDepreciacionPorActivo
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Parametros_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Parametros Entity = Context.Af_Parametros.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new Af_Parametros
                        {

                            IdEmpresa = info.IdEmpresa,
                            IdTipoCbte = info.IdTipoCbte,
                            IdTipoCbteBaja = info.IdTipoCbteBaja,
                            IdTipoCbteMejora = info.IdTipoCbteMejora,
                            IdTipoCbteRetiro = info.IdTipoCbteRetiro,
                            IdTipoCbteVenta = info.IdTipoCbteVenta,
                            DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo,
                            ContabilizaDepreciacionPorActivo = info.ContabilizaDepreciacionPorActivo

                        };
                        Context.Af_Parametros.Add(Entity);
                    }
                    else
                    {
                        Entity.IdTipoCbte = info.IdTipoCbte;
                        Entity.IdTipoCbteBaja = info.IdTipoCbteBaja;
                        Entity.IdTipoCbteMejora = info.IdTipoCbteMejora;
                        Entity.IdTipoCbteRetiro = info.IdTipoCbteRetiro;
                        Entity.IdTipoCbteVenta = info.IdTipoCbteVenta;
                        Entity.DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo;
                        Entity.ContabilizaDepreciacionPorActivo = info.ContabilizaDepreciacionPorActivo;

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

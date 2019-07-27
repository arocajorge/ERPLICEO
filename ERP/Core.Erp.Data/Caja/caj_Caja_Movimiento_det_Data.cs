using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Caja
{
    public class caj_Caja_Movimiento_det_Data
    {
        public caj_Caja_Movimiento_det_Info get_info(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)

        {
            try
            {
                caj_Caja_Movimiento_det_Info info = new caj_Caja_Movimiento_det_Info();
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_Movimiento_det Entity = Context.caj_Caja_Movimiento_det.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTipocbte == IdTipoCbte && q.IdCbteCble == IdCbteCble);
                    if (Entity == null) return null;
                    info = new caj_Caja_Movimiento_det_Info
                    {

                        IdEmpresa = Entity.IdEmpresa,
                        IdTipocbte = Entity.IdTipocbte,
                        IdCbteCble = Entity.IdCbteCble,
                        IdCobro_tipo = Entity.IdCobro_tipo,
                        cr_Valor = Entity.cr_Valor,
                        Secuencia = Entity.Secuencia
                    };
                }
                return info; 

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(caj_Caja_Movimiento_det_Info info)
        {
            try
            {
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_Movimiento_det Entity = new caj_Caja_Movimiento_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCbteCble = info.IdCbteCble,
                        IdTipocbte = info.IdTipocbte,
                        IdCobro_tipo = info.IdCobro_tipo,
                        cr_Valor = info.cr_Valor,
                        Secuencia = info.Secuencia
                    };
                    Context.caj_Caja_Movimiento_det.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                using (Entities_caja Context = new Entities_caja())
                {
                    Context.Database.ExecuteSqlCommand("Delete caj_Caja_Movimiento_det where IdEmpresa = '"+IdEmpresa+"'and IdTipoCbte = '" + IdTipoCbte + "' and IdCbteCble = " + IdCbteCble);
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

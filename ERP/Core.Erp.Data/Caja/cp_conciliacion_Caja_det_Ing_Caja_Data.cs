using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Caja
{
    public class cp_conciliacion_Caja_det_Ing_Caja_Data
    {
        public List<cp_conciliacion_Caja_det_Ing_Caja_Info> get_list(int IdEmpresa, decimal IdConciliacion_caja)
        {
            try
            {
                List<cp_conciliacion_Caja_det_Ing_Caja_Info> Lista;

                using (Entities_caja Context = new Entities_caja())
                {
                    Lista = (from q in Context.cp_conciliacion_Caja_det_Ing_Caja
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion_Caja == IdConciliacion_caja
                             select new cp_conciliacion_Caja_det_Ing_Caja_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion_Caja = q.IdConciliacion_Caja,
                                 secuencia = q.secuencia,
                                 IdEmpresa_movcaj = q.IdEmpresa_movcaj,
                                 IdTipocbte_movcaj = q.IdTipocbte_movcaj,
                                 IdCbteCble_movcaj = q.IdCbteCble_movcaj,
                                 valor_aplicado = q.valor_aplicado,
                                 valor_disponible = q.valor_disponible
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cp_conciliacion_Caja_det_Ing_Caja_Info> get_list_ingresos_x_conciliar(int IdEmpresa, DateTime Fecha_fin, int IdCaja)
        {
            try
            {
                Fecha_fin = Fecha_fin.Date;
                List<cp_conciliacion_Caja_det_Ing_Caja_Info> Lista;

                using (Entities_caja Context = new Entities_caja())
                {
                    Lista = (from q in Context.vwcaj_Caja_Movimiento_x_Conciliar
                             where q.IdEmpresa == IdEmpresa
                             && q.cm_fecha <= Fecha_fin
                             && q.IdCaja == IdCaja
                             && q.Saldo > 0
                             select new cp_conciliacion_Caja_det_Ing_Caja_Info
                             {
                                 IdEmpresa_movcaj = q.IdEmpresa,
                                 IdTipocbte_movcaj = q.IdTipocbte,
                                 IdCbteCble_movcaj = q.IdCbteCble,
                                 cm_observacion = q.cm_observacion,
                                 cm_fecha = q.cm_fecha,
                                 Total_movi = q.Total_movi,
                                 Total_aplicado = q.Total_aplicado,
                                 valor_disponible = q.Saldo,
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

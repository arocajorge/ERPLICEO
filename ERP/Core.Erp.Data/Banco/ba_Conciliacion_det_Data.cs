using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
   public class ba_Conciliacion_det_Data
    {
        public List<ba_Conciliacion_det_Info> GetList(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                int Secuencia = 1;
                List<ba_Conciliacion_det_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = Context.ba_Conciliacion_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).Select(q => new ba_Conciliacion_det_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdConciliacion = q.IdConciliacion,
                        Fecha = q.Fecha,
                        Observacion =  q.Observacion,
                        Referencia = q.Referencia,
                        Secuencia = q.Secuencia,
                        Seleccionado = q.Seleccionado,
                        tipo_IngEgr = q.tipo_IngEgr,
                        Valor = q.Valor,
                        IdTipocbte = q.IdTipocbte,
                    }).ToList();
                }
                Lista.ForEach(v => { v.Secuencia = Secuencia++; });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

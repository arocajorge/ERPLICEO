using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
   public class ba_archivo_transferencia_x_ba_tipo_flujo_Data
    {
        public List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = Context.ba_archivo_transferencia_x_ba_tipo_flujo.Include("ba_TipoFlujo").Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new ba_archivo_transferencia_x_ba_tipo_flujo_Info
                    {
                        IdEmpresa = q.IdEmpresa, 
                        IdArchivo = q.IdArchivo,
                        IdTipoFlujo = q.IdTipoFlujo,
                        Porcentaje = q.Porcentaje,
                        Secuencia = q.Secuencia,
                        Valor = q.Valor,
                        Descricion = q.ba_TipoFlujo.Descricion
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

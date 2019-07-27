using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.ActivoFijo
{
    public class ACTF_005_Data
    {
        public List<ACTF_005_Info> get_list(int IdEmpresa, int IdActivoFijoTipo, int IdCategoriaAF, DateTime fecha_corte, string Estado_Proceso)
        {
            try
            {
                int IdActivoFijoTipo_ini = IdActivoFijoTipo;
                int IdActivoFijoTipo_fin = IdActivoFijoTipo == 0 ? 9999 : IdActivoFijoTipo;

                int IdCategoriaAF_ini = IdCategoriaAF;
                int IdCategoriaAF_fin = IdCategoriaAF == 0 ? 9999 : IdCategoriaAF;

                fecha_corte = fecha_corte.Date;
                List<ACTF_005_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPACTF_005(IdEmpresa, IdActivoFijoTipo_ini, IdActivoFijoTipo_fin, IdCategoriaAF_ini, IdCategoriaAF_fin, fecha_corte, Estado_Proceso)
                             select new ACTF_005_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActivoFijo = q.IdActivoFijo,
                                 IdActivoFijoTipo = q.IdActivoFijoTipo,
                                 nom_tipo = q.nom_tipo,
                                 nom_categoria = q.nom_categoria,
                                 IdCategoriaAF = q.IdCategoriaAF,
                                 CodActivoFijo = q.CodActivoFijo,
                                 Af_Nombre = q.Af_Nombre,
                                 Estado_Proceso = q.Estado_Proceso,
                                 nom_estado_proceso = q.nom_estado_proceso,
                                 Af_fecha_compra = q.Af_fecha_compra,
                                 Af_costo_compra = q.Af_costo_compra,
                                 valor_mejora = q.valor_mejora,
                                 valor_baja = q.valor_baja,
                                 valor_depreciacion = q.valor_depreciacion,
                                 valor_venta = q.valor_venta,
                                 saldo = q.saldo

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

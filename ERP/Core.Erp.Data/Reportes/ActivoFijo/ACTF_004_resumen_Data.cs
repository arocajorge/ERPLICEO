using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.ActivoFijo
{
    public class ACTF_004_resumen_Data
    {
        public List<ACTF_004_resumen_Info> get_list(int IdEmpresa,int IdActivoFijoTipo, int IdCategoriaAF, DateTime fecha_corte, string Estado_Proceso, string IdUsuario)
        {
            try
            {
                int IdActivoFijoTipo_ini = IdActivoFijoTipo;
                int IdActivoFijoTipo_fin = IdActivoFijoTipo == 0 ? 9999 : IdActivoFijoTipo;

                int IdCategoriaAF_ini = IdCategoriaAF;
                int IdCategoriaAF_fin = IdCategoriaAF == 0 ? 9999 : IdCategoriaAF;

                fecha_corte = fecha_corte.Date;
                List<ACTF_004_resumen_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPACTF_004_resumen(IdEmpresa, fecha_corte, IdUsuario,IdActivoFijoTipo_ini, IdActivoFijoTipo_fin, IdCategoriaAF_ini, IdCategoriaAF_fin, Estado_Proceso)
                             select new ACTF_004_resumen_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActivoFijoTipo = q.IdActivoFijoTipo,
                                 IdUsuario = q.IdUsuario,
                                 Af_Descripcion = q.Af_Descripcion,
                                 Af_costo_compra = q.Af_costo_compra,
                                 Valor_Depreciacion = q.Valor_Depreciacion,
                                 Valor_ult_depreciacion = q.Valor_ult_depreciacion,
                                 Costo_neto = q.Costo_neto
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}

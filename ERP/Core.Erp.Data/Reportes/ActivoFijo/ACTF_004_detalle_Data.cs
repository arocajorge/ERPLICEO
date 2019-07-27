using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.ActivoFijo
{
  public  class ACTF_004_detalle_Data
    {
        public List<ACTF_004_detalle_Info> get_list(int IdEmpresa, int IdActivoFijoTipo, int IdCategoriaAF, DateTime fecha_corte, string Estado_Proceso, string IdUsuario)
        {
            try
            {
                int IdActivoFijoTipo_ini = IdActivoFijoTipo;
                int IdActivoFijoTipo_fin = IdActivoFijoTipo == 0 ? 9999 : IdActivoFijoTipo;

                int IdCategoriaAF_ini = IdCategoriaAF;
                int IdCategoriaAF_fin = IdCategoriaAF == 0 ? 9999 : IdCategoriaAF;

                fecha_corte = fecha_corte.Date;
                List<ACTF_004_detalle_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPACTF_004_detalle(IdEmpresa, fecha_corte, IdUsuario, IdActivoFijoTipo_ini, IdActivoFijoTipo_fin, IdCategoriaAF_ini, IdCategoriaAF_fin, Estado_Proceso)
                             select new ACTF_004_detalle_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActivoFijo = q.IdActivoFijo,
                                 IdUsuario = q.IdUsuario,
                                 IdSucursal = q.IdSucursal,
                                 Su_Descripcion = q.Su_Descripcion,
                                 CodActivoFijo = q.CodActivoFijo,
                                 Af_Nombre = q.Af_Nombre,
                                 IdActivoFijoTipo = q.IdActivoFijoTipo,
                                 tipo_AF = q.tipo_AF,
                                 IdCategoria_Af = q.IdCategoria_Af,
                                 Categoria_AF = q.Categoria_AF,
                                 Af_costo_compra = q.Af_costo_compra,
                                 Af_Depreciacion_acum = q.Af_Depreciacion_acum,
                                 Costo_actual = q.Costo_actual,
                                 valor_ult_depreciacion = q.valor_ult_depreciacion,
                                 Af_fecha_compra = q.Af_fecha_compra
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

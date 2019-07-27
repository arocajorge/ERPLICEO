using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
    public class Af_Depreciacion_Det_Data
    {
        public List<Af_Depreciacion_Det_Info> get_list(int IdEmpresa, decimal IdDepreciacion)
        {
            try
            {
                List<Af_Depreciacion_Det_Info> lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    lista = (from q in Context.Af_Depreciacion_Det
                             join a in Context.Af_Activo_fijo
                             on new { q.IdEmpresa, q.IdActivoFijo} equals new { a.IdEmpresa, a.IdActivoFijo}
                             join t in Context.Af_Activo_fijo_tipo
                             on new { a.IdEmpresa, a.IdActivoFijoTipo} equals new { t.IdEmpresa, t.IdActivoFijoTipo}
                             join c in Context.Af_Activo_fijo_Categoria
                             on new { a.IdEmpresa, a.IdCategoriaAF} equals new { c.IdEmpresa, c.IdCategoriaAF}
                             where q.IdEmpresa == IdEmpresa
                             && q.IdDepreciacion == IdDepreciacion
                             select new Af_Depreciacion_Det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActivoFijo = q.IdActivoFijo,
                                 IdDepreciacion = q.IdDepreciacion,
                                 Concepto = q.Concepto,
                                 Porc_Depreciacion = q.Porc_Depreciacion,
                                 Secuencia = q.Secuencia,
                                 Valor_Compra = q.Valor_Compra,
                                 Valor_Depreciacion = q.Valor_Depreciacion,
                                 Valor_Depre_Acum = q.Valor_Depre_Acum,
                                 Valor_Salvamento = q.Valor_Salvamento,
                                 Vida_Util = q.Vida_Util,
                                 nom_categoria = c.Descripcion,
                                 nom_tipo = t.Af_Descripcion,
                                 Af_Nombre = a.Af_Nombre
                             }).ToList();
                }
                return lista; 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Af_Depreciacion_Det_Info> get_list_a_depreciar(int IdEmpresa, int IdPeriodo, string IdUsuario)
        {
            try
            {
                List<Af_Depreciacion_Det_Info> Lista;

                DateTime Fecha_ini = DateTime.Now;
                DateTime fecha_fin = DateTime.Now;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_periodo e_periodo = Context.ct_periodo.Where(q => q.IdPeriodo == IdPeriodo && q.IdEmpresa == IdEmpresa).FirstOrDefault();
                    if (e_periodo == null)
                        return new List<Af_Depreciacion_Det_Info>();
                    Fecha_ini = e_periodo.pe_FechaIni.Date;
                    fecha_fin = e_periodo.pe_FechaFin.Date;
                }
                
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Lista = (from q in Context.spACTF_activos_a_depreciar(IdEmpresa, Fecha_ini, fecha_fin, IdUsuario)
                             select new Af_Depreciacion_Det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActivoFijo = q.IdActivoFijo,
                                 Af_Nombre = q.Af_Nombre,
                                 CodActivoFijo = q.CodActivoFijo,
                                 nom_tipo = q.nom_tipo,
                                 nom_categoria = q.nom_categoria,
                                 Valor_Depreciacion = q.Af_valor_depreciacion,
                                 Valor_Depre_Acum = q.Af_depreciacion_acum,
                                 Valor_Compra = q.Af_costo_compra,
                                 IdCtaCble_Activo = q.IdCtaCble_Activo,
                                 IdCtaCble_Dep_Acum = q.IdCtaCble_Dep_Acum,
                                 IdCtaCble_Gastos_Depre = q.IdCtaCble_Gastos_Depre
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

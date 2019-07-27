using Core.Erp.Data.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_Conciliacion_Data
    {
        public List<ba_Conciliacion_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                List<ba_Conciliacion_Info> Lista;

                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = (from q in Context.vwba_Conciliacion
                             where q.IdEmpresa == IdEmpresa
                             && Fecha_ini <= q.co_Fecha
                             && q.co_Fecha <= Fecha_fin
                             orderby q.IdConciliacion descending
                             select new ba_Conciliacion_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion = q.IdConciliacion,
                                 IdBanco = q.IdBanco,
                                 IdPeriodo = q.IdPeriodo,
                                 co_Fecha = q.co_Fecha,
                                 IdEstado_Concil_Cat = q.IdEstado_Concil_Cat,
                                 co_SaldoContable_MesAnt = q.co_SaldoContable_MesAnt,
                                 co_totalIng = q.co_totalIng,
                                 co_totalEgr = q.co_totalEgr,
                                 co_SaldoContable_MesAct = q.co_SaldoContable_MesAct,
                                 co_SaldoBanco_EstCta = q.co_SaldoBanco_EstCta,
                                 co_SaldoBanco_anterior = q.co_SaldoBanco_anterior,
                                 Estado = q.Estado,
                                 co_Observacion = q.co_Observacion,
                                 ba_descripcion = q.ba_descripcion,
                                 IdCtaCble = q.IdCtaCble,
                                 Periodo = q.Periodo,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ba_Conciliacion_Info get_info(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                ba_Conciliacion_Info info;

                using (Entities_banco Context = new Entities_banco())
                {
                    var Entity = Context.ba_Conciliacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new ba_Conciliacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdConciliacion = Entity.IdConciliacion,
                        IdBanco = Entity.IdBanco,
                        IdPeriodo = Entity.IdPeriodo,
                        co_Fecha = Entity.co_Fecha,
                        IdEstado_Concil_Cat = Entity.IdEstado_Concil_Cat,
                        co_SaldoContable_MesAnt = Entity.co_SaldoContable_MesAnt,
                        co_totalIng = Entity.co_totalIng,
                        co_totalEgr = Entity.co_totalEgr,
                        co_SaldoContable_MesAct = Entity.co_SaldoContable_MesAct,
                        co_SaldoBanco_EstCta = Entity.co_SaldoBanco_EstCta,
                        co_SaldoBanco_anterior = Entity.co_SaldoBanco_anterior,
                        Estado = Entity.Estado,
                        co_Observacion = Entity.co_Observacion,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Conciliacion
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdConciliacion) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_Conciliacion_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var id = get_id(info.IdEmpresa);
                    Context.ba_Conciliacion.Add(new ba_Conciliacion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdConciliacion = info.IdConciliacion = id,
                        IdBanco = info.IdBanco,
                        IdPeriodo = info.IdPeriodo,
                        co_Fecha = info.co_Fecha,
                        IdEstado_Concil_Cat = info.IdEstado_Concil_Cat,
                        co_SaldoContable_MesAnt = info.co_SaldoContable_MesAnt,
                        co_totalIng = info.co_totalIng,
                        co_totalEgr = info.co_totalEgr,
                        co_SaldoContable_MesAct = info.co_SaldoContable_MesAct,
                        co_SaldoBanco_EstCta = info.co_SaldoBanco_EstCta,
                        co_SaldoBanco_anterior = info.co_SaldoBanco_anterior,
                        co_Observacion = info.co_Observacion,
                        Estado = info.Estado = "A",
                    });
                    int secuencia = 1;
                    foreach (var item in info.lst_det)
                    {
                        Context.ba_Conciliacion_det_IngEgr.Add(new ba_Conciliacion_det_IngEgr
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdConciliacion = info.IdConciliacion,
                            Secuencia = secuencia++,
                            tipo_IngEgr = item.tipo_IngEgr,
                            IdTipocbte = item.IdTipocbte,
                            IdCbteCble = item.IdCbteCble,                            
                            SecuenciaCbteCble = item.SecuenciaCbteCble,
                            Estado = "A",
                            @checked = item.seleccionado
                        });
                        if (info.IdEstado_Concil_Cat == "CONCILIADO")
                        {
                            var cbte = Context.ba_Cbte_Ban.Where(q => q.IdEmpresa == info.IdEmpresa && item.IdTipocbte == q.IdTipocbte && q.IdCbteCble == item.IdCbteCble).FirstOrDefault();
                            if (cbte != null)
                                cbte.IdEstado_cheque_cat = "ESTCBCOB";
                        }
                    }
                    if(info.List_detalle.Count()>0)
                    {
                        foreach (var item in info.List_detalle)
                        {
                            Context.ba_Conciliacion_det.Add(new ba_Conciliacion_det
                            {
                                IdEmpresa = info.IdEmpresa, 
                                IdConciliacion = info.IdConciliacion,
                                IdTipocbte = item.IdTipocbte,
                                Fecha = item.Fecha,
                                Observacion = item.Observacion,
                                Referencia = item.Referencia,
                                Secuencia = secuencia++,
                                Seleccionado = item.Seleccionado,
                                tipo_IngEgr = item.tipo_IngEgr,
                                Valor = item.Valor
                            });
                        }
                    }
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Conciliacion_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(ba_Conciliacion_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var Entity = Context.ba_Conciliacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).FirstOrDefault();
                    if (Entity == null) return false;
                    Entity.co_Fecha = info.co_Fecha;
                    Entity.IdEstado_Concil_Cat = info.IdEstado_Concil_Cat;
                    Entity.co_SaldoContable_MesAnt = info.co_SaldoContable_MesAnt;
                    Entity.co_totalIng = info.co_totalIng;
                    Entity.co_totalEgr = info.co_totalEgr;
                    Entity.co_SaldoContable_MesAct = info.co_SaldoContable_MesAct;
                    Entity.co_SaldoBanco_EstCta = info.co_SaldoBanco_EstCta;
                    Entity.co_SaldoBanco_anterior = info.co_SaldoBanco_anterior;
                    Entity.co_Observacion = info.co_Observacion;

                    var lst = Context.ba_Conciliacion_det_IngEgr.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion);
                    Context.ba_Conciliacion_det_IngEgr.RemoveRange(lst);

                    int secuencia = 1;
                    foreach (var item in info.lst_det)
                    {
                        Context.ba_Conciliacion_det_IngEgr.Add(new ba_Conciliacion_det_IngEgr
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdConciliacion = info.IdConciliacion,
                            Secuencia = secuencia++,
                            tipo_IngEgr = item.tipo_IngEgr,
                            IdTipocbte = item.IdTipocbte,
                            IdCbteCble = item.IdCbteCble,
                            SecuenciaCbteCble = item.SecuenciaCbteCble,
                            Estado = "A",
                            @checked = item.seleccionado
                        });
                        if (info.IdEstado_Concil_Cat == "CONCILIADO")
                        {
                            var cbte = Context.ba_Cbte_Ban.Where(q => q.IdEmpresa == info.IdEmpresa && item.IdTipocbte == q.IdTipocbte && q.IdCbteCble == item.IdCbteCble).FirstOrDefault();
                            if (cbte != null)
                                cbte.IdEstado_cheque_cat = "ESTCBCOB";
                        }
                    }
                    var lstd = Context.ba_Conciliacion_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion);
                    Context.ba_Conciliacion_det.RemoveRange(lstd);

                    if (info.List_detalle.Count() > 0)
                    {
                        foreach (var item in info.List_detalle)
                        {
                            Context.ba_Conciliacion_det.Add(new ba_Conciliacion_det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdConciliacion = info.IdConciliacion,
                                IdTipocbte = item.IdTipocbte,
                                Fecha = item.Fecha,
                                Observacion = item.Observacion,
                                Referencia = item.Referencia,
                                Secuencia = item.Secuencia,
                                Seleccionado = item.Seleccionado,
                                tipo_IngEgr = item.tipo_IngEgr,
                                Valor = item.Valor
                            });
                        }
                    }
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_Conciliacion_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(ba_Conciliacion_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var Entity = Context.ba_Conciliacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).FirstOrDefault();
                    if (Entity == null) return false;
                    Entity.Estado = "I";

                    var lst = Context.ba_Conciliacion_det_IngEgr.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConciliacion == info.IdConciliacion).ToList();
                    Context.ba_Conciliacion_det_IngEgr.RemoveRange(lst);

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ExisteConciliacion(int IdEmpresa, int IdPeriodo, int IdBanco)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Conciliacion
                              where q.IdEmpresa == IdEmpresa
                              && q.IdPeriodo == IdPeriodo
                              && q.IdBanco == IdBanco
                              && q.Estado == "A"
                              select q;

                    if (lst.Count() > 0)
                        return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

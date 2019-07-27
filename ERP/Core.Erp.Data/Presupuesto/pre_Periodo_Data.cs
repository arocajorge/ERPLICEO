using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Presupuesto
{
    public class pre_Periodo_Data
    {
        public List<pre_Periodo_Info> GetList(int IdEmpresa, bool MostrarAnulado, bool MostarCerrado)
        {
            try
            {
                List<pre_Periodo_Info> Lista = new List<pre_Periodo_Info>();

                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    if (MostrarAnulado == false)
                    {
                        if (MostarCerrado == false)
                        {
                            Lista = db.pre_PresupuestoPeriodo.Where(q => q.Estado == true && q.IdEmpresa == IdEmpresa && q.EstadoCierre == false).Select(q => new pre_Periodo_Info
                            {
                                IdPeriodo = q.IdPeriodo,
                                IdEmpresa = q.IdEmpresa,
                                DescripcionPeriodo = q.DescripciónPeriodo,
                                Observacion = q.Observacion,
                                FechaInicio = q.FechaInicio,
                                FechaFin = q.FechaFin,
                                EstadoCierre = q.EstadoCierre,
                                Estado = q.Estado
                            }).ToList();
                        }
                        else
                        {
                            Lista = db.pre_PresupuestoPeriodo.Where(q => q.Estado == true && q.IdEmpresa == IdEmpresa && q.EstadoCierre == true).Select(q => new pre_Periodo_Info
                            {
                                IdPeriodo = q.IdPeriodo,
                                IdEmpresa = q.IdEmpresa,
                                DescripcionPeriodo = q.DescripciónPeriodo,
                                Observacion = q.Observacion,
                                FechaInicio = q.FechaInicio,
                                FechaFin = q.FechaFin,
                                EstadoCierre = q.EstadoCierre,
                                Estado = q.Estado
                            }).ToList();
                        }                        
                    }
                    else
                    {
                        if (MostarCerrado == false)
                        {
                            Lista = db.pre_PresupuestoPeriodo.Where(q => q.IdEmpresa == IdEmpresa && q.EstadoCierre == false).Select(q => new pre_Periodo_Info
                            {
                                IdPeriodo = q.IdPeriodo,
                                IdEmpresa = q.IdEmpresa,
                                DescripcionPeriodo = q.DescripciónPeriodo,
                                Observacion = q.Observacion,
                                FechaInicio = q.FechaInicio,
                                FechaFin = q.FechaFin,
                                EstadoCierre = q.EstadoCierre,
                                Estado = q.Estado
                            }).ToList();
                        }
                        else
                        {
                            Lista = db.pre_PresupuestoPeriodo.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new pre_Periodo_Info
                            {
                                IdPeriodo = q.IdPeriodo,
                                IdEmpresa = q.IdEmpresa,
                                DescripcionPeriodo = q.DescripciónPeriodo,
                                Observacion = q.Observacion,
                                FechaInicio = q.FechaInicio,
                                FechaFin = q.FechaFin,
                                EstadoCierre = q.EstadoCierre,
                                Estado = q.Estado
                            }).ToList();
                        }
                        
                    }
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public pre_Periodo_Info GetInfo(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                pre_Periodo_Info info = new pre_Periodo_Info();

                using (Entities_presupuesto Context = new Entities_presupuesto())
                {
                    pre_PresupuestoPeriodo Entity = Context.pre_PresupuestoPeriodo.Where(q => q.IdPeriodo == IdPeriodo && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new pre_Periodo_Info
                    {
                        IdPeriodo = Entity.IdPeriodo,
                        IdEmpresa = Entity.IdEmpresa,
                        DescripcionPeriodo = Entity.DescripciónPeriodo,
                        Observacion = Entity.Observacion,
                        FechaInicio = Entity.FechaInicio,
                        FechaFin = Entity.FechaFin,
                        EstadoCierre = Entity.EstadoCierre,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public pre_Periodo_Info GetInfo_UltimoPeriodoAbierto(int IdEmpresa)
        {
            try
            {
                pre_Periodo_Info info = new pre_Periodo_Info();

                using (Entities_presupuesto Context = new Entities_presupuesto())
                {
                    pre_PresupuestoPeriodo Entity = Context.pre_PresupuestoPeriodo.Where(q => q.IdEmpresa == IdEmpresa && q.EstadoCierre == false && q.Estado == true).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new pre_Periodo_Info
                    {
                        IdPeriodo = Entity.IdPeriodo,
                        IdEmpresa = Entity.IdEmpresa,
                        DescripcionPeriodo = Entity.DescripciónPeriodo,
                        Observacion = Entity.Observacion,
                        FechaInicio = Entity.FechaInicio,
                        FechaFin = Entity.FechaFin,
                        EstadoCierre = Entity.EstadoCierre,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int get_id(int IdEmpresa)
        {

            try
            {
                int ID = 1;
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    var Lista = db.pre_PresupuestoPeriodo.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdPeriodo);

                    if (Lista.Count() > 0)
                        ID = Convert.ToInt32(Lista.Max() + 1);
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarBD(pre_Periodo_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    db.pre_PresupuestoPeriodo.Add(new pre_PresupuestoPeriodo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPeriodo = info.IdPeriodo = get_id(info.IdEmpresa),
                        DescripciónPeriodo = info.DescripcionPeriodo,
                        Observacion = info.Observacion,
                        FechaInicio = info.FechaInicio,
                        FechaFin = info.FechaFin,
                        EstadoCierre = info.EstadoCierre,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarBD(pre_Periodo_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_PresupuestoPeriodo entity = db.pre_PresupuestoPeriodo.Where(q => q.IdPeriodo == info.IdPeriodo && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.DescripciónPeriodo = info.DescripcionPeriodo;
                    entity.Observacion = info.Observacion;
                    entity.FechaInicio = info.FechaInicio;
                    entity.FechaFin = info.FechaFin;
                    entity.EstadoCierre = info.EstadoCierre;
                    entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    entity.FechaModificacion = DateTime.Now;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(pre_Periodo_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_PresupuestoPeriodo entity = db.pre_PresupuestoPeriodo.Where(q => q.IdPeriodo == info.IdPeriodo && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }                    

                    var ListaPresupuestos = db.vwpre_PresupuestoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == entity.IdPeriodo && q.Estado == true).ToList();

                    if (ListaPresupuestos == null || ListaPresupuestos.Count == 0)
                    {
                        entity.Estado = false;
                        entity.EstadoCierre = false;
                        entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                        entity.FechaAnulacion = DateTime.Now;
                        entity.MotivoAnulacion = info.MotivoAnulacion;

                        db.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
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

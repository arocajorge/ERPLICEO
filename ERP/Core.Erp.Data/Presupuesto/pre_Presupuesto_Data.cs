using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Presupuesto
{
    public class pre_Presupuesto_Data
    {
        public List<pre_Presupuesto_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdPeriodo, bool MostrarAnulados)
        {
            try
            {
                List<pre_Presupuesto_Info> Lista;

                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                decimal IdPeriodoIncio = IdPeriodo;
                decimal IdPeriodoFin = IdPeriodo == 0 ? 9999 : IdPeriodo;

                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    if (MostrarAnulados == false)
                    {
                        Lista = (from q in db.vwpre_Presupuesto
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal >= IdSucursalInicio && q.IdSucursal <=IdSucursalFin
                                 && q.IdPeriodo >= IdPeriodoIncio && q.IdPeriodo <= IdPeriodoFin
                                 && q.Estado == true
                                 select new pre_Presupuesto_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPresupuesto = q.IdPresupuesto,                            
                            IdSucursal = q.IdSucursal,
                            Su_Descripcion = q.Su_Descripcion,
                            IdPeriodo = q.IdPeriodo,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin,
                            Observacion = q.Observacion,
                            Estado = q.Estado,
                            MontoSolicitado = q.MontoSolicitado,
                            MontoAprobado = q.MontoAprobado,
                            DescripcionPeriodo = q.DescripciónPeriodo,
                            IdUsuarioAprobacion = q.IdUsuarioAprobacion

                        }).ToList();
                    }
                    else
                    {
                        Lista = (from q in db.vwpre_Presupuesto
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal >= IdSucursalInicio && q.IdSucursal <= IdSucursalFin
                                 && q.IdPeriodo >= IdPeriodoIncio && q.IdPeriodo <= IdPeriodoFin                                 
                                 select new pre_Presupuesto_Info
                                 {
                            IdEmpresa = q.IdEmpresa,
                            IdPresupuesto = q.IdPresupuesto,
                            IdSucursal = q.IdSucursal,
                            Su_Descripcion = q.Su_Descripcion,
                            IdPeriodo = q.IdPeriodo,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin,
                            Observacion = q.Observacion,
                            Estado = q.Estado,
                            MontoSolicitado = q.MontoSolicitado,
                            MontoAprobado = q.MontoAprobado,
                            DescripcionPeriodo = q.DescripciónPeriodo,
                            IdUsuarioAprobacion = q.IdUsuarioAprobacion

                                 }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public pre_Presupuesto_Info get_info(int IdEmpresa, int IdPresupuesto)
        {
            try
            {
                pre_Presupuesto_Info info = new pre_Presupuesto_Info();
                using (Entities_presupuesto Context = new Entities_presupuesto())
                {
                    pre_Presupuesto Entity = Context.pre_Presupuesto.Where(q => q.IdPresupuesto == IdPresupuesto && q.IdEmpresa == IdEmpresa).FirstOrDefault();                    

                    if (Entity == null) return null;
                    info = new pre_Presupuesto_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPresupuesto = Entity.IdPresupuesto,
                        IdSucursal = Entity.IdSucursal,
                        IdPeriodo = Entity.IdPeriodo,
                        Observacion = Entity.Observacion,
                        MontoAprobado = Entity.MontoAprobado,
                        MontoSolicitado = Entity.MontoSolicitado,
                        Estado = Entity.Estado,
                        MotivoAnulacion = Entity.MotivoAnulacion,
                        MotivoAprobacion = Entity.MotivoAprobacion
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
                    var Lista = db.pre_Presupuesto.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdPresupuesto);

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
        public bool GuardarBD(pre_Presupuesto_Info info)
        {
            try
            {
                double monto_solicitado = info.ListaPresupuestoDet.Sum(v => v.Monto);
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    db.pre_Presupuesto.Add(new pre_Presupuesto
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPresupuesto = info.IdPresupuesto = get_id(info.IdEmpresa),
                        IdSucursal = info.IdSucursal,
                        IdPeriodo = info.IdPeriodo,
                        Observacion =  info.Observacion,
                        MontoSolicitado = monto_solicitado,
                        MontoAprobado = info.MontoAprobado,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });

                    if (info.ListaPresupuestoDet != null)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.ListaPresupuestoDet)
                        {
                            pre_Rubro EntityRubro = db.pre_Rubro.Where(q => q.IdRubro == item.IdRubro && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                            db.pre_PresupuestoDet.Add(new pre_PresupuestoDet
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPresupuesto = info.IdPresupuesto,
                                Secuencia = Secuencia++,
                                IdRubro = item.IdRubro,
                                IdCtaCble = EntityRubro.IdCtaCble,
                                Monto = item.Monto
                            });

                        }
                    }
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarBD(pre_Presupuesto_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {                    
                    pre_Presupuesto entity = db.pre_Presupuesto.Where(q => q.IdPresupuesto == info.IdPresupuesto && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    double monto_solicitado = info.ListaPresupuestoDet.Sum(v => v.Monto);

                    entity.IdSucursal = info.IdSucursal;
                    entity.IdPeriodo = info.IdPeriodo;
                    entity.Observacion = info.Observacion;
                    entity.MontoSolicitado = monto_solicitado;
                    entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    entity.FechaModificacion = DateTime.Now;

                    entity.IdUsuarioAprobacion = null;                    

                    var lst_PresupuestoDet = db.pre_PresupuestoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPresupuesto == info.IdPresupuesto).ToList();
                    db.pre_PresupuestoDet.RemoveRange(lst_PresupuestoDet);
                    
                    if (info.ListaPresupuestoDet != null)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.ListaPresupuestoDet)
                        {
                            pre_Rubro EntityRubro = db.pre_Rubro.Where(q => q.IdRubro == item.IdRubro && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                            db.pre_PresupuestoDet.Add(new pre_PresupuestoDet
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPresupuesto = info.IdPresupuesto,
                                Secuencia = Secuencia++,
                                IdRubro = item.IdRubro,
                                IdCtaCble = EntityRubro.IdCtaCble,
                                Monto = item.Monto
                            });
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(pre_Presupuesto_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_Presupuesto entity = db.pre_Presupuesto.Where(q => q.IdPresupuesto == info.IdPresupuesto && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Estado = false;
                    entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    entity.FechaAnulacion = DateTime.Now;
                    entity.MotivoAnulacion = info.MotivoAnulacion;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AprobarBD(pre_Presupuesto_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_Presupuesto entity = db.pre_Presupuesto.Where(q => q.IdPresupuesto == info.IdPresupuesto && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.IdUsuarioAprobacion = info.IdUsuarioAprobacion;
                    entity.FechaAprobacion = DateTime.Now;
                    entity.MontoAprobado = info.MontoAprobado;
                    entity.MotivoAprobacion = info.MotivoAprobacion;

                    db.SaveChanges();
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

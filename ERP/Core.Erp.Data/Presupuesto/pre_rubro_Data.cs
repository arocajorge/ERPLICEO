using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Presupuesto
{
    public class pre_rubro_Data
    {
        public List<pre_rubro_Info> GetList(int IdEmpresa, bool MostrarAnulado)
        {
            try
            {
                List<pre_rubro_Info> Lista = new List<pre_rubro_Info>();

                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    if (MostrarAnulado == false)
                    {
                        Lista = db.vwpre_Rubro.Where(q => q.Estado == true && q.IdEmpresa == IdEmpresa).Select(q => new pre_rubro_Info
                        {
                            IdRubro = q.IdRubro,
                            IdEmpresa = q.IdEmpresa,
                            IdRubroTipo = q.IdRubroTipo,
                            Descripcion = q.Descripcion,
                            IdCtaCble = q.IdCtaCble,
                            pc_Cuenta = q.pc_Cuenta,
                            Descripcion_RubroTipo = q.Descripcion_RubroTipo,
                            Estado = q.Estado
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.vwpre_Rubro.Where(q=> q.IdEmpresa == IdEmpresa).Select(q => new pre_rubro_Info
                        {
                            IdRubro = q.IdRubro,
                            IdEmpresa = q.IdEmpresa,
                            IdRubroTipo = q.IdRubroTipo,
                            Descripcion = q.Descripcion,
                            IdCtaCble = q.IdCtaCble,
                            pc_Cuenta = q.pc_Cuenta,
                            Descripcion_RubroTipo = q.Descripcion_RubroTipo,
                            Estado = q.Estado
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

        public pre_rubro_Info GetInfo(int IdEmpresa, int IdRubro)
        {
            try
            {
                pre_rubro_Info info = new pre_rubro_Info();

                using (Entities_presupuesto Context = new Entities_presupuesto())
                {
                    pre_Rubro Entity = Context.pre_Rubro.Where(q => q.IdRubro == IdRubro && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new pre_rubro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRubro = Entity.IdRubro,
                        IdRubroTipo = Entity.IdRubroTipo,
                        Descripcion = Entity.Descripcion,
                        IdCtaCble = Entity.IdCtaCble,
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
                    var Lista = db.pre_Rubro.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdRubro);

                    if (Lista.Count() > 0)
                        ID = Lista.Max() + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarBD(pre_rubro_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    db.pre_Rubro.Add(new pre_Rubro
                    {            
                        IdEmpresa = info.IdEmpresa,            
                        IdRubro = info.IdRubro = get_id(info.IdEmpresa),
                        IdRubroTipo = info.IdRubroTipo,
                        Descripcion = info.Descripcion,
                        IdCtaCble = info.IdCtaCble,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuario,
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

        public bool ModificarBD(pre_rubro_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_Rubro entity = db.pre_Rubro.Where(q => q.IdRubro == info.IdRubro && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Descripcion = info.Descripcion;
                    entity.IdRubroTipo = info.IdRubroTipo;
                    entity.IdCtaCble = info.IdCtaCble;
                    entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    entity.FechaModificacion = DateTime.Now;

                    db.SaveChanges();

                    var ListaPresupuestos = db.vwpre_PresupuestoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdRubro == info.IdRubro && q.EstadoCierre == false).ToList();

                    foreach (var item in ListaPresupuestos)
                    {
                        pre_PresupuestoDet EntityRubro = db.pre_PresupuestoDet.Where(q => q.IdRubro == item.IdRubro && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                        EntityRubro.IdCtaCble = info.IdCtaCble;
                        db.SaveChanges();
                    }                    
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularBD(pre_rubro_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_Rubro entity = db.pre_Rubro.Where(q => q.IdRubro == info.IdRubro && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

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
    }
}

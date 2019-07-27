using Core.Erp.Info.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Presupuesto
{
    public class pre_RubroTipo_Data
    {
        public List<pre_RubroTipo_Info> GetList(int IdEmpresa, bool MostrarAnulado)
        {
            try
            {
                List<pre_RubroTipo_Info> Lista = new List<pre_RubroTipo_Info>();

                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    if (MostrarAnulado == false)
                    {
                        Lista = db.pre_RubroTipo.Where(q => q.Estado == true && q.IdEmpresa == IdEmpresa).Select(q => new pre_RubroTipo_Info
                        {
                            IdRubroTipo = q.IdRubroTipo,
                            IdEmpresa = q.IdEmpresa,
                            Descripcion = q.Descripcion,
                            Signo = q.Signo,
                            Orden = q.Orden,
                            Estado = q.Estado
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.pre_RubroTipo.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new pre_RubroTipo_Info
                        {
                            IdRubroTipo = q.IdRubroTipo,
                            IdEmpresa = q.IdEmpresa,
                            Descripcion = q.Descripcion,
                            Signo = q.Signo,
                            Orden = q.Orden,
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

        public pre_RubroTipo_Info GetInfo(int IdEmpresa, int IdRubroTipo)
        {
            try
            {
                pre_RubroTipo_Info info = new pre_RubroTipo_Info();

                using (Entities_presupuesto Context = new Entities_presupuesto())
                {
                    pre_RubroTipo Entity = Context.pre_RubroTipo.Where(q => q.IdRubroTipo == IdRubroTipo && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new pre_RubroTipo_Info
                    {
                        IdRubroTipo = Entity.IdRubroTipo,
                        IdEmpresa = Entity.IdEmpresa,
                        Descripcion = Entity.Descripcion,
                        Signo = Entity.Signo,
                        Orden = Entity.Orden,
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
                    var Lista = db.pre_RubroTipo.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdRubroTipo);

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
        public bool GuardarBD(pre_RubroTipo_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    db.pre_RubroTipo.Add(new pre_RubroTipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdRubroTipo = info.IdRubroTipo = get_id(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        Signo = info.Signo,
                        Orden = info.Orden,
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

        public bool ModificarBD(pre_RubroTipo_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_RubroTipo entity = db.pre_RubroTipo.Where(q => q.IdRubroTipo == info.IdRubroTipo && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Descripcion = info.Descripcion;
                    entity.Signo = info.Signo;
                    entity.Orden = info.Orden;
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

        public bool AnularBD(pre_RubroTipo_Info info)
        {
            try
            {
                using (Entities_presupuesto db = new Entities_presupuesto())
                {
                    pre_RubroTipo entity = db.pre_RubroTipo.Where(q => q.IdRubroTipo == info.IdRubroTipo && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

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

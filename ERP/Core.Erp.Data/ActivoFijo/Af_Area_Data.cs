using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
  public  class Af_Area_Data
    {
        public List<Af_Area_Info> GetList(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<Af_Area_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if (mostrar_anulados)
                        Lista = Context.Af_Area.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new Af_Area_Info
                        {

                            IdEmpresa = q.IdEmpresa, 
                            Descripcion = q.Descripcion,
                            Estado = q.Estado,
                            IdArea = q.IdArea,
                            
                        }).ToList();
                    else
                        Lista = Context.Af_Area.Where(q => 
                        q.IdEmpresa == IdEmpresa 
                        && q.Estado == true).Select(q => new Af_Area_Info
                        {

                            IdEmpresa = q.IdEmpresa,
                            Descripcion = q.Descripcion,
                            Estado = q.Estado,
                            IdArea = q.IdArea,

                        }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Af_Area_Info GetInfo (int IdEmpresa, decimal IdArea)
        {
            try
            {
                Af_Area_Info info = new Af_Area_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Area Entity = Context.Af_Area.Where(q => q.IdEmpresa == IdEmpresa && q.IdArea == IdArea).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new Af_Area_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Descripcion = Entity.Descripcion,
                        Estado = Entity.Estado,
                        IdArea = Entity.IdArea
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal GetId(int IdEmpresa)
        {
            try
            {
                decimal Id = 1;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    var lst = from q in Context.Af_Area
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.IdArea) + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(Af_Area_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Context.Af_Area.Add(new Af_Area
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdArea = info.IdArea = GetId(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB( Af_Area_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Area Entity = Context.Af_Area.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArea == info.IdArea).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(Af_Area_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Area Entity = Context.Af_Area.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArea == info.IdArea).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = false;
                    Entity.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();
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

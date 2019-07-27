using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
   public class Af_Departamento_Data
    {
        public List<Af_Departamento_Info> GetList(int IdEmpresa,decimal IdArea, bool mostrar_anulados)
        {
            try
            {
                List<Af_Departamento_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if(mostrar_anulados==true)
                    Lista = Context.Af_Departamento.Where(q => q.IdEmpresa == IdEmpresa && q.IdArea == IdArea).Select(q => new Af_Departamento_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdDepartamento = q.IdDepartamento,
                        Descripcion = q.Descripcion,
                        Estado = q.Estado,
                        IdArea = q.IdArea
                    }).ToList();
                    else
                        Lista = Context.Af_Departamento.Where(q => q.IdEmpresa == IdEmpresa && q.IdArea == IdArea && q.Estado == true ).Select(q => new Af_Departamento_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdDepartamento = q.IdDepartamento,
                            Descripcion = q.Descripcion,
                            Estado = q.Estado,
                            IdArea = q.IdArea
                        }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Af_Departamento_Info GetInfo(int IdEmpresa, decimal IdArea, decimal IdDepartamento)
        {
            try
            {
                Af_Departamento_Info info = new Af_Departamento_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Departamento Entity = Context.Af_Departamento.Where(q => q.IdEmpresa == IdEmpresa && q.IdArea == IdArea && q.IdDepartamento == IdDepartamento).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new Af_Departamento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdDepartamento = Entity.IdDepartamento,
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
                    var lst = Context.Af_Departamento.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdDepartamento);
                    if (lst.Count() > 0)
                        Id = lst.Max() + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDB(Af_Departamento_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Context.Af_Departamento.Add(new Af_Departamento
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdArea = info.IdArea,
                        IdDepartamento = info.IdDepartamento=GetId(info.IdEmpresa),
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
        public bool ModificarDB(Af_Departamento_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Departamento Entity = Context.Af_Departamento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDepartamento == info.IdDepartamento).FirstOrDefault();
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
        public bool AnularDB(Af_Departamento_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Departamento Entity = Context.Af_Departamento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDepartamento == info.IdDepartamento).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
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

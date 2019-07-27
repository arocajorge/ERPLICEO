using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
    public class Af_Activo_fijo_Categoria_Data
    {
        public List<Af_Activo_fijo_Categoria_Info> get_list(int IdEmpresa, int IdActivoFijoTipo, bool mostrar_Anulados)
        {
            try
            {
                List<Af_Activo_fijo_Categoria_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if (mostrar_Anulados)
                        Lista = (from q in Context.Af_Activo_fijo_Categoria
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdActivoFijoTipo == IdActivoFijoTipo
                                 select new Af_Activo_fijo_Categoria_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     CodCategoriaAF = q.CodCategoriaAF,
                                     cod_tipo = q.cod_tipo,
                                     Descripcion = q.Descripcion,
                                     IdActivoFijoTipo = q.IdActivoFijoTipo,
                                     IdCategoriaAF = q.IdCategoriaAF,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.Af_Activo_fijo_Categoria
                                  where q.IdEmpresa == IdEmpresa
                                  && q.IdActivoFijoTipo == IdActivoFijoTipo
                                  && q.Estado == "A"
                                  select new Af_Activo_fijo_Categoria_Info
                                  {
                                      IdEmpresa = q.IdEmpresa,
                                      CodCategoriaAF = q.CodCategoriaAF,
                                      cod_tipo = q.cod_tipo,
                                      Descripcion = q.Descripcion,
                                      IdActivoFijoTipo = q.IdActivoFijoTipo,
                                      IdCategoriaAF = q.IdCategoriaAF,
                                      Estado = q.Estado,

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

        public Af_Activo_fijo_Categoria_Info get_info(int IdEmpresa, int IdCategoriaAF)
        {
            try
            {
                Af_Activo_fijo_Categoria_Info info = new Af_Activo_fijo_Categoria_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_Categoria Entity = Context.Af_Activo_fijo_Categoria.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCategoriaAF == IdCategoriaAF);
                    if (Entity == null) return null;
                    info = new Af_Activo_fijo_Categoria_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        CodCategoriaAF = Entity.CodCategoriaAF,
                        cod_tipo = Entity.cod_tipo,
                        Descripcion = Entity.Descripcion,
                        IdActivoFijoTipo = Entity.IdActivoFijoTipo,
                        IdCategoriaAF = Entity.IdCategoriaAF,
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
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    var lst = from q in Context.Af_Activo_fijo_Categoria
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCategoriaAF) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Activo_fijo_Categoria_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_Categoria Entity = new Af_Activo_fijo_Categoria
                    {
                        IdEmpresa = info.IdEmpresa,
                        CodCategoriaAF = info.CodCategoriaAF,
                        cod_tipo = info.cod_tipo,
                        Descripcion = info.Descripcion,
                        IdActivoFijoTipo = info.IdActivoFijoTipo,
                        IdCategoriaAF = info.IdCategoriaAF=get_id(info.IdEmpresa),
                        Estado = info.Estado="A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now

                    };
                    Context.Af_Activo_fijo_Categoria.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(Af_Activo_fijo_Categoria_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_Categoria Entity = Context.Af_Activo_fijo_Categoria.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCategoriaAF == info.IdCategoriaAF);
                    if (Entity == null) return false;

                    Entity.CodCategoriaAF = info.CodCategoriaAF;
                    Entity.cod_tipo = info.cod_tipo;
                    Entity.Descripcion = info.Descripcion;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(Af_Activo_fijo_Categoria_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_Categoria Entity = Context.Af_Activo_fijo_Categoria.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCategoriaAF == info.IdCategoriaAF);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
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

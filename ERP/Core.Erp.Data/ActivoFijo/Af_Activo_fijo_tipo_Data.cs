using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
   public class Af_Activo_fijo_tipo_Data
    {
        public List<Af_Activo_fijo_tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<Af_Activo_fijo_tipo_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.Af_Activo_fijo_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 select new Af_Activo_fijo_tipo_Info
                                 {
                                     IdActivoFijoTipo = q.IdActivoFijoTipo,
                                     IdEmpresa = q.IdEmpresa,
                                     Af_Descripcion = q.Af_Descripcion,
                                     CodActivoFijo = q.CodActivoFijo,
                                     Estado = q.Estado,
                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.Af_Activo_fijo_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new Af_Activo_fijo_tipo_Info
                                 {
                                     IdActivoFijoTipo = q.IdActivoFijoTipo,
                                     IdEmpresa = q.IdEmpresa,
                                     Af_Descripcion = q.Af_Descripcion,
                                     CodActivoFijo = q.CodActivoFijo,
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

        public Af_Activo_fijo_tipo_Info get_info(int IdEmpresa, int IdActivoFijoTipo)
        {
            try
            {
                Af_Activo_fijo_tipo_Info info = new Af_Activo_fijo_tipo_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_tipo Entity = Context.Af_Activo_fijo_tipo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdActivoFijoTipo == IdActivoFijoTipo);
                    if (Entity == null) return null;
                    info = new Af_Activo_fijo_tipo_Info
                    {
                        IdActivoFijoTipo = Entity.IdActivoFijoTipo,
                        IdEmpresa = Entity.IdEmpresa,
                        Af_anio_depreciacion = Entity.Af_anio_depreciacion,
                        Af_Descripcion = Entity.Af_Descripcion,
                        Af_Porcentaje_depre = Entity.Af_Porcentaje_depre,
                        CodActivoFijo = Entity.CodActivoFijo,
                        Estado = Entity.Estado,
                        IdCtaCble_Activo = Entity.IdCtaCble_Activo,
                        IdCtaCble_Dep_Acum = Entity.IdCtaCble_Dep_Acum,
                        IdCtaCble_Gastos_Depre = Entity.IdCtaCble_Gastos_Depre,
                        Se_Deprecia = Entity.Se_Deprecia==Convert.ToBoolean(Entity.Se_Deprecia),
                        IdCtaCble_Baja = Entity.IdCtaCble_Baja,
                        IdCtaCble_CostoVenta = Entity.IdCtaCble_CostoVenta,
                        IdCtaCble_Mejora = Entity.IdCtaCble_Mejora,
                        IdCtaCble_Retiro = Entity.IdCtaCble_Retiro
                        
                        
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
                    var lst = from q in Context.Af_Activo_fijo_tipo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)

                        ID = lst.Max(q => q.IdActivoFijoTipo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Activo_fijo_tipo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_tipo Entity = new Af_Activo_fijo_tipo
                    {
                        IdActivoFijoTipo = info.IdActivoFijoTipo = get_id(info.IdEmpresa),
                        IdEmpresa = info.IdEmpresa,
                        Af_anio_depreciacion = info.Af_anio_depreciacion,
                        Af_Descripcion = info.Af_Descripcion,
                        Af_Porcentaje_depre = info.Af_Porcentaje_depre,
                        CodActivoFijo = info.CodActivoFijo,
                        Estado = info.Estado="A",
                        IdCtaCble_Activo = info.IdCtaCble_Activo,
                        IdCtaCble_Dep_Acum = info.IdCtaCble_Dep_Acum,
                        IdCtaCble_Gastos_Depre = info.IdCtaCble_Gastos_Depre,
                        Se_Deprecia = info.Se_Deprecia == Convert.ToBoolean(info.Se_Deprecia),
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now,
                        IdCtaCble_Baja = info.IdCtaCble_Baja,
                        IdCtaCble_CostoVenta = info.IdCtaCble_CostoVenta,
                        IdCtaCble_Mejora = info.IdCtaCble_Mejora,
                        IdCtaCble_Retiro = info.IdCtaCble_Retiro
                    };
                    Context.Af_Activo_fijo_tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(Af_Activo_fijo_tipo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_tipo Entity = Context.Af_Activo_fijo_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdActivoFijoTipo == info.IdActivoFijoTipo);
                    if (Entity == null) return false;
                    Entity.Af_anio_depreciacion = info.Af_anio_depreciacion;
                    Entity.Af_Descripcion = info.Af_Descripcion;
                    Entity.Af_Porcentaje_depre = info.Af_Porcentaje_depre;
                    Entity.CodActivoFijo = info.CodActivoFijo;
                    Entity.IdCtaCble_Activo = info.IdCtaCble_Activo;
                    Entity.IdCtaCble_Dep_Acum = info.IdCtaCble_Dep_Acum;
                    Entity.IdCtaCble_Gastos_Depre = info.IdCtaCble_Gastos_Depre;
                    Entity.Se_Deprecia = info.Se_Deprecia == Convert.ToBoolean(info.Se_Deprecia);
                    Entity.IdCtaCble_Baja = info.IdCtaCble_Baja;
                    Entity.IdCtaCble_CostoVenta = info.IdCtaCble_CostoVenta;
                    Entity.IdCtaCble_Mejora = info.IdCtaCble_Mejora;
                    Entity.IdCtaCble_Retiro = info.IdCtaCble_Retiro;


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

        public bool anularDB(Af_Activo_fijo_tipo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Activo_fijo_tipo Entity = Context.Af_Activo_fijo_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdActivoFijoTipo == info.IdActivoFijoTipo);
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

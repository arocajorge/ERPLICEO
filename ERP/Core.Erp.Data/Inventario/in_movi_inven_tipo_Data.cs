using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_movi_inven_tipo_Data
    {
        public List<in_movi_inven_tipo_Info> get_list(int IdEmpresa,string Signo, bool mostrar_anulados)
        {
            try
            {
                List<in_movi_inven_tipo_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_movi_inven_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.cm_tipo_movi.Contains(Signo)
                                 select new in_movi_inven_tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                     tm_descripcion = q.tm_descripcion,
                                     cm_descripcionCorta = q.cm_descripcionCorta,
                                     cm_tipo_movi = q.cm_tipo_movi,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_movi_inven_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 && q.cm_tipo_movi.Contains(Signo)
                                 select new in_movi_inven_tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                     tm_descripcion = q.tm_descripcion,
                                     cm_descripcionCorta = q.cm_descripcionCorta,
                                     cm_tipo_movi = q.cm_tipo_movi,
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
        public in_movi_inven_tipo_Info get_info(int IdEmpresa, int IdMovi_inven_tipo)
        {
            try
            {
                in_movi_inven_tipo_Info info = new in_movi_inven_tipo_Info();

                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_movi_inven_tipo Entity = Context.in_movi_inven_tipo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdMovi_inven_tipo == IdMovi_inven_tipo);
                    if (Entity == null) return null;
                    info = new in_movi_inven_tipo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMovi_inven_tipo = Entity.IdMovi_inven_tipo,
                        Codigo = Entity.Codigo,
                        tm_descripcion = Entity.tm_descripcion,
                        cm_tipo_movi = Entity.cm_tipo_movi,
                        cm_descripcionCorta = Entity.cm_descripcionCorta,
                        Estado = Entity.Estado,
                        IdTipoCbte = Entity.IdTipoCbte,
                        Genera_Diario_Contable = Entity.Genera_Diario_Contable,
                        IdCatalogoAprobacion = Entity.IdCatalogoAprobacion
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_movi_inven_tipo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdMovi_inven_tipo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_movi_inven_tipo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_movi_inven_tipo Entity = new in_movi_inven_tipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMovi_inven_tipo = info.IdMovi_inven_tipo = get_id(info.IdEmpresa),
                        Codigo = info.Codigo,
                        tm_descripcion = info.tm_descripcion,
                        cm_tipo_movi = info.cm_tipo_movi,
                        cm_descripcionCorta = info.cm_descripcionCorta,
                        Estado = info.Estado = "A",
                        IdTipoCbte = info.IdTipoCbte,
                        Genera_Diario_Contable = info.Genera_Diario_Contable,
                        IdCatalogoAprobacion = info.IdCatalogoAprobacion,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.in_movi_inven_tipo.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(in_movi_inven_tipo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_movi_inven_tipo Entity = Context.in_movi_inven_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo);
                    if (Entity == null) return false;
                    Entity.Codigo = info.Codigo;
                    Entity.tm_descripcion = info.tm_descripcion;
                    Entity.cm_tipo_movi = info.cm_tipo_movi;
                    Entity.cm_descripcionCorta = info.cm_descripcionCorta;
                    Entity.IdTipoCbte = info.IdTipoCbte;
                    Entity.Genera_Diario_Contable = info.Genera_Diario_Contable;
                    Entity.IdCatalogoAprobacion = info.IdCatalogoAprobacion;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(in_movi_inven_tipo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_movi_inven_tipo Entity = Context.in_movi_inven_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo);
                    if (Entity == null) return false;
                    Entity.Estado = "I";
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

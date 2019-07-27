using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.SeguridadAcceso
{
    public class seg_Menu_Data
    {
        public List<seg_Menu_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<seg_Menu_Info> Lista;

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.seg_Menu
                                 select new seg_Menu_Info
                                 {
                                     IdMenu = q.IdMenu,
                                     IdMenuPadre = q.IdMenuPadre,
                                     DescripcionMenu = q.DescripcionMenu,
                                     nivel = q.nivel,
                                     PosicionMenu = q.PosicionMenu,                                     
                                     Habilitado = q.Habilitado,
                                     es_desktop = q.es_desktop,
                                     es_web = q.es_web
                                 }).ToList();
                    else
                        Lista = (from q in Context.seg_Menu
                                 where q.Habilitado == true
                                 select new seg_Menu_Info
                                 {
                                     IdMenu = q.IdMenu,
                                     IdMenuPadre = q.IdMenuPadre,
                                     DescripcionMenu = q.DescripcionMenu,
                                     nivel = q.nivel,
                                     PosicionMenu = q.PosicionMenu,
                                     Habilitado = q.Habilitado,
                                     es_desktop = q.es_desktop,
                                     es_web = q.es_web
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<seg_Menu_Info> get_list_combo(bool mostrar_anulados)
        {
            try
            {
                List<seg_Menu_Info> Lista;

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.seg_Menu
                                 join m in Context.seg_Menu
                                 on q.IdMenuPadre equals m.IdMenu into temp_padre
                                 from m in temp_padre.DefaultIfEmpty()
                                 join padre in Context.seg_Menu
                                 on m.IdMenuPadre equals padre.IdMenu into temp_padre_padre
                                 from padre in temp_padre_padre.DefaultIfEmpty()
                                 select new seg_Menu_Info
                                 {
                                     IdMenu = q.IdMenu,
                                     IdMenuPadre = q.IdMenuPadre,
                                     DescripcionMenu = q.DescripcionMenu,
                                     nivel = q.nivel,
                                     PosicionMenu = q.PosicionMenu,
                                     Habilitado = q.Habilitado,
                                     DescripcionMenu_combo = (padre == null ? "" : (padre.IdMenuPadre + " " + padre.DescripcionMenu + " - ")) + (m == null ? "" : (m.IdMenuPadre + " " + m.DescripcionMenu + " - ")) + q.IdMenuPadre + " " + q.DescripcionMenu

                                 }).ToList();
                    else
                        Lista = (from q in Context.seg_Menu
                                 join m in Context.seg_Menu
                                 on q.IdMenuPadre equals m.IdMenu into temp_padre
                                 from m in temp_padre.DefaultIfEmpty()
                                 join padre in Context.seg_Menu
                                 on m.IdMenuPadre equals padre.IdMenu into temp_padre_padre
                                 from padre in temp_padre_padre.DefaultIfEmpty()
                                 where q.Habilitado == true
                                 select new seg_Menu_Info
                                 {
                                     IdMenu = q.IdMenu,
                                     IdMenuPadre = q.IdMenuPadre,
                                     DescripcionMenu = q.DescripcionMenu,
                                     nivel = q.nivel,
                                     PosicionMenu = q.PosicionMenu,
                                     Habilitado = q.Habilitado,
                                     DescripcionMenu_combo = (padre == null ? "" : (padre.IdMenuPadre + " " + padre.DescripcionMenu + " - ")) + (m == null ? "" : (m.IdMenuPadre + " " + m.DescripcionMenu + " - ")) + q.IdMenuPadre + " " + q.DescripcionMenu
                                 }).ToList();
                }

                return Lista.OrderBy(q => q.DescripcionMenu_combo).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public seg_Menu_Info get_info(int IdMenu)
        {
            try
            {
                seg_Menu_Info info = new seg_Menu_Info();

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    seg_Menu Entity = Context.seg_Menu.FirstOrDefault(q => q.IdMenu == IdMenu);
                    if (Entity == null)
                        return null;
                    info = new seg_Menu_Info
                    {
                        IdMenu = Entity.IdMenu,
                        IdMenuPadre = Entity.IdMenuPadre,
                        DescripcionMenu = Entity.DescripcionMenu,
                        PosicionMenu = Entity.PosicionMenu,
                        Tiene_FormularioAsociado = Entity.Tiene_FormularioAsociado,
                        web_nom_Area = Entity.web_nom_Area,
                        web_nom_Controller = Entity.web_nom_Controller,
                        web_nom_Action = Entity.web_nom_Action,
                        nivel = Entity.nivel,
                        es_desktop = Entity.es_desktop,
                        es_web = Entity.es_web
                        };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id()
        {
            try
            {
                int ID = 1;

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    var lst = from q in Context.seg_Menu
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdMenu) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(seg_Menu_Info info)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    seg_Menu Entity = new seg_Menu
                    {
                        IdMenu = get_id(),
                        IdMenuPadre = info.IdMenuPadre,
                        DescripcionMenu = info.DescripcionMenu,
                        PosicionMenu = info.PosicionMenu,
                        Habilitado = info.Habilitado = true,
                        Tiene_FormularioAsociado = info.Tiene_FormularioAsociado,
                        nivel = 1,
                        web_nom_Area = info.web_nom_Area,
                        web_nom_Controller = info.web_nom_Controller == null ? "" : info.web_nom_Controller,
                        web_nom_Action = info.web_nom_Action,
                        es_web = info.es_web,
                        es_desktop = info.es_desktop
                    };
                    Context.seg_Menu.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(seg_Menu_Info info)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    seg_Menu Entity = Context.seg_Menu.FirstOrDefault(q => q.IdMenu == info.IdMenu);
                    if (Entity == null) return false;
                    Entity.IdMenuPadre = info.IdMenuPadre;
                    Entity.DescripcionMenu = info.DescripcionMenu;
                    Entity.PosicionMenu = info.PosicionMenu;
                    Entity.Tiene_FormularioAsociado = info.Tiene_FormularioAsociado;
                    Entity.web_nom_Controller = info.web_nom_Controller == null ? "" : info.web_nom_Controller;
                    Entity.web_nom_Area = info.web_nom_Area;
                    Entity.web_nom_Action = info.web_nom_Action;
                    Entity.es_web = info.es_web;
                    Entity.es_desktop = info.es_desktop;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(seg_Menu_Info info)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    seg_Menu Entity = Context.seg_Menu.FirstOrDefault(q => q.IdMenu == info.IdMenu);
                    if (Entity == null) return false;
                    Entity.Habilitado = info.Habilitado = false;
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

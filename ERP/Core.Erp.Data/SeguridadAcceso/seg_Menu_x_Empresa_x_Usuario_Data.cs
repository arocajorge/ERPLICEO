using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.SeguridadAcceso
{
    public class seg_Menu_x_Empresa_x_Usuario_Data
    {
        public List<seg_Menu_x_Empresa_x_Usuario_Info> get_list(int IdEmpresa, string IdUsuario)
        {
            try
            {
                List<seg_Menu_x_Empresa_x_Usuario_Info> Lista;

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Lista = (from m in Context.seg_Menu
                             join me in Context.seg_Menu_x_Empresa
                             on m.IdMenu equals me.IdMenu
                             join meu in Context.seg_Menu_x_Empresa_x_Usuario
                             on new { me.IdEmpresa, me.IdMenu } equals new { meu.IdEmpresa, meu.IdMenu }
                             where m.Habilitado == true && meu.IdEmpresa == IdEmpresa
                             && meu.IdUsuario == IdUsuario
                             select new seg_Menu_x_Empresa_x_Usuario_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = meu.IdEmpresa,
                                 IdUsuario = meu.IdUsuario,
                                 IdMenu = meu.IdMenu,
                                 Lectura = meu.Lectura,
                                 Escritura = meu.Escritura,
                                 Eliminacion = meu.Eliminacion,
                                 info_menu = new seg_Menu_Info
                                 {
                                     IdMenu = m.IdMenu,
                                     DescripcionMenu = m.DescripcionMenu,
                                     IdMenuPadre = m.IdMenuPadre,
                                     PosicionMenu = m.PosicionMenu
                                 }
                             }).ToList();

                    Lista.AddRange((from q in Context.seg_Menu
                                    join me in Context.seg_Menu_x_Empresa
                                    on q.IdMenu equals me.IdMenu
                                    where q.Habilitado == true && me.IdEmpresa  == IdEmpresa
                                    && !Context.seg_Menu_x_Empresa_x_Usuario.Any(meu => meu.IdMenu == q.IdMenu && meu.IdEmpresa == IdEmpresa && meu.IdUsuario == IdUsuario)
                                    select new seg_Menu_x_Empresa_x_Usuario_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdUsuario = IdUsuario,
                                        IdMenu = q.IdMenu,
                                        info_menu = new seg_Menu_Info
                                        {
                                            IdMenu = q.IdMenu,
                                            DescripcionMenu = q.DescripcionMenu,
                                            IdMenuPadre = q.IdMenuPadre,
                                            PosicionMenu = q.PosicionMenu
                                        }

                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<seg_Menu_x_Empresa_x_Usuario_Info> get_list(int IdEmpresa, string IdUsuario, int IdMenuPadre)
        {
            try
            {
                List<seg_Menu_x_Empresa_x_Usuario_Info> Lista;

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Lista = (from m in Context.seg_Menu
                             join me in Context.seg_Menu_x_Empresa
                             on m.IdMenu equals me.IdMenu
                             join meu in Context.seg_Menu_x_Empresa_x_Usuario
                             on new { me.IdEmpresa, me.IdMenu } equals new { meu.IdEmpresa, meu.IdMenu }
                             where m.Habilitado == true && meu.IdEmpresa == IdEmpresa
                             && meu.IdUsuario == IdUsuario && m.IdMenuPadre == IdMenuPadre
                             && m.es_web == true
                             orderby m.PosicionMenu
                             select new seg_Menu_x_Empresa_x_Usuario_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = meu.IdEmpresa,
                                 IdUsuario = meu.IdUsuario,
                                 IdMenu = meu.IdMenu,
                                 Lectura = meu.Lectura,
                                 Escritura = meu.Escritura,
                                 Eliminacion = meu.Eliminacion,
                                 info_menu = new seg_Menu_Info
                                 {
                                     IdMenu = m.IdMenu,
                                     DescripcionMenu = m.DescripcionMenu,
                                     IdMenuPadre = m.IdMenuPadre,
                                     PosicionMenu = m.PosicionMenu,
                                     web_nom_Action = m.web_nom_Action,
                                     web_nom_Area = m.web_nom_Area,
                                     web_nom_Controller = m.web_nom_Controller
                                 }
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, string IdUsuario)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Context.Database.ExecuteSqlCommand("DELETE seg_Menu_x_Empresa_x_Usuario WHERE IdEmpresa = "+IdEmpresa+" AND IdUsuario = '"+IdUsuario+"'");
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<seg_Menu_x_Empresa_x_Usuario_Info> Lista, int IdEmpresa, string IdUsuario)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    foreach (var item in Lista)
                    {
                        seg_Menu_x_Empresa_x_Usuario Entity = new Data.seg_Menu_x_Empresa_x_Usuario
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdUsuario = item.IdUsuario,
                            IdMenu = item.IdMenu,
                            Lectura = item.Lectura,
                            Escritura = item.Escritura,
                            Eliminacion = item.Eliminacion
                        };
                        Context.seg_Menu_x_Empresa_x_Usuario.Add(Entity);
                    }

                    Context.SaveChanges();
                    string sql = "exec spseg_corregir_menu '" + IdEmpresa + "','"+ IdUsuario + "'";
                    Context.Database.ExecuteSqlCommand(sql);

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

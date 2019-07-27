using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.SeguridadAcceso
{
    public class seg_Menu_x_Empresa_Data
    {
        public List<seg_Menu_x_Empresa_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<seg_Menu_x_Empresa_Info> Lista;

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Lista = (from q in Context.seg_Menu_x_Empresa
                             join m in Context.seg_Menu
                             on q.IdMenu equals m.IdMenu
                             where q.IdEmpresa == IdEmpresa
                             && m.Habilitado == true
                             select new seg_Menu_x_Empresa_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdMenu = q.IdMenu,

                                 info_menu = new seg_Menu_Info
                                 {
                                     IdMenu = m.IdMenu,
                                     DescripcionMenu = m.DescripcionMenu,
                                     IdMenuPadre = m.IdMenuPadre,
                                     PosicionMenu = m.PosicionMenu
                                 }
                             }).ToList();

                    Lista.AddRange((from q in Context.seg_Menu
                                   where !Context.seg_Menu_x_Empresa.Any(me => me.IdMenu == q.IdMenu && me.IdEmpresa == IdEmpresa)
                                   && q.Habilitado == true
                                   select new seg_Menu_x_Empresa_Info
                                   {
                                       seleccionado = false,
                                       IdEmpresa = IdEmpresa,
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

        public bool eliminarDB(int IdEmpresa)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Context.Database.ExecuteSqlCommand("DELETE seg_Menu_x_Empresa WHERE IdEmpresa = "+IdEmpresa);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<seg_Menu_x_Empresa_Info> Lista)
        {
            try
            {
                int IdEmpresa = 0;
                if (Lista.Count() > 0)
                    IdEmpresa = Lista.FirstOrDefault().IdEmpresa;
                IdEmpresa = Lista.FirstOrDefault().IdEmpresa;

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    foreach (var item in Lista)
                    {
                        seg_Menu_x_Empresa Entity = new seg_Menu_x_Empresa
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdMenu = item.IdMenu,                            
                        };
                        Context.seg_Menu_x_Empresa.Add(Entity);
                    }
                    Context.SaveChanges();

                    string sql = "exec spseg_corregir_menu '" + IdEmpresa + "','" + "" + "'";
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

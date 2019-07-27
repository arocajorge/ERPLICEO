using Core.Erp.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.SeguridadAcceso
{
    public class seg_Usuario_x_Empresa_Data
    {
        public List<seg_Usuario_x_Empresa_Info> get_list(string IdUsuario)
        {
            try
            {
                List<seg_Usuario_x_Empresa_Info> Lista;                

                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Lista = (from q in Context.vwseg_Usuario_x_Empresa
                             where q.IdUsuario == IdUsuario
                             select new seg_Usuario_x_Empresa_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdUsuario = q.IdUsuario,
                                 em_nombre = q.em_nombre
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<seg_Usuario_x_Empresa_Info> Lista)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    foreach (var item in Lista)
                    {
                        seg_Usuario_x_Empresa Entity = new seg_Usuario_x_Empresa
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdUsuario = item.IdUsuario
                        };
                        Context.seg_Usuario_x_Empresa.Add(Entity);
                    }
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(string IdUsuario)
        {
            try
            {
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Context.Database.ExecuteSqlCommand("delete seg_Usuario_x_Empresa where IdUsuario = '"+IdUsuario+"'");
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

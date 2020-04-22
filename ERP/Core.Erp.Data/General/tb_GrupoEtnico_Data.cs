using Core.Erp.Data;
using Core.Web.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_GrupoEtnico_Data
    {
        public List<tb_GrupoEtnico_Info> getList(bool MostrarAnulados)
        {
            try
            {
                List<tb_GrupoEtnico_Info> Lista = new List<tb_GrupoEtnico_Info>();

                using (Entities_general odata = new Entities_general())
                {
                    var lst = odata.tb_GrupoEtnico.Where(q => q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new tb_GrupoEtnico_Info
                        {
                            IdGrupoEtnico = q.IdGrupoEtnico,
                            NomGrupoEtnico = q.NomGrupoEtnico,
                            Estado = q.Estado
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_GrupoEtnico_Info getInfo(int IdGrupoEtnico)
        {
            try
            {
                tb_GrupoEtnico_Info info;

                using (Entities_general db = new Entities_general())
                {
                    var Entity = db.tb_GrupoEtnico.Where(q => q.IdGrupoEtnico == IdGrupoEtnico).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new tb_GrupoEtnico_Info
                    {
                        IdGrupoEtnico = Entity.IdGrupoEtnico,
                        NomGrupoEtnico = Entity.NomGrupoEtnico,
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

        public int getId()
        {
            try
            {
                int ID = 1;

                using (Entities_general Context = new Entities_general())
                {
                    var cont = Context.tb_GrupoEtnico.Count();
                    if (cont > 0)
                        ID = Context.tb_GrupoEtnico.Max(q => q.IdGrupoEtnico) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_GrupoEtnico_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_GrupoEtnico Entity = new tb_GrupoEtnico
                    {
                        IdGrupoEtnico = info.IdGrupoEtnico = getId(),
                        NomGrupoEtnico = info.NomGrupoEtnico,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.tb_GrupoEtnico.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_GrupoEtnico_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_GrupoEtnico Entity = Context.tb_GrupoEtnico.FirstOrDefault(q => q.IdGrupoEtnico == info.IdGrupoEtnico);
                    if (Entity == null)
                        return false;

                    Entity.NomGrupoEtnico = info.NomGrupoEtnico;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_GrupoEtnico_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_GrupoEtnico Entity = Context.tb_GrupoEtnico.FirstOrDefault(q => q.IdGrupoEtnico == info.IdGrupoEtnico);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
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

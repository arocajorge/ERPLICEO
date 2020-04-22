using Core.Erp.Data;
using Core.Web.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Web.Data.General
{
    public class tb_profesion_Data
    {
        public List<tb_profesion_Info> getList(bool MostrarAnulados)
        {
            try
            {
                List<tb_profesion_Info> Lista = new List<tb_profesion_Info>();

                using (Entities_general odata = new Entities_general())
                {
                    var lst = odata.tb_profesion.Where(q=> q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new tb_profesion_Info
                        {
                            IdProfesion = q.IdProfesion,
                            Descripcion = q.Descripcion,
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

        public tb_profesion_Info getInfo(int IdProfesion)
        {
            try
            {
                tb_profesion_Info info;

                using (Entities_general db = new Entities_general())
                {
                    var Entity = db.tb_profesion.Where(q => q.IdProfesion == IdProfesion).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new tb_profesion_Info
                    {
                        IdProfesion = Entity.IdProfesion,
                        Descripcion = Entity.Descripcion,
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
                    var cont = Context.tb_profesion.Count();
                    if (cont > 0)
                        ID = Context.tb_profesion.Max(q => q.IdProfesion) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_profesion_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_profesion Entity = new tb_profesion
                    {
                        IdProfesion = info.IdProfesion = getId(),
                        Descripcion = info.Descripcion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.tb_profesion.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_profesion_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_profesion Entity = Context.tb_profesion.FirstOrDefault(q => q.IdProfesion == info.IdProfesion);
                    if (Entity == null)
                        return false;

                    Entity.Descripcion = info.Descripcion;
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

        public bool anularDB(tb_profesion_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_profesion Entity = Context.tb_profesion.FirstOrDefault(q => q.IdProfesion == info.IdProfesion);
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

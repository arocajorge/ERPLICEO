using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_modulo_Data
    {
        public List<tb_modulo_Info> get_list()
        {
            try
            {
                List<tb_modulo_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    Lista = (from q in Context.tb_modulo
                             select new tb_modulo_Info
                             {
                                 CodModulo = q.CodModulo,
                                 Descripcion = q.Descripcion,
                                 Nom_Carpeta = q.Nom_Carpeta

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_modulo_Info get_info(string CodModulo)
        {
            try
            {
                tb_modulo_Info info = new tb_modulo_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_modulo Entity = Context.tb_modulo.FirstOrDefault(q => q.CodModulo == CodModulo);
                    if (Entity == null) return null;
                    info = new tb_modulo_Info
                    {
                        CodModulo = Entity.CodModulo,
                        Descripcion = Entity.Descripcion,
                        Nom_Carpeta = Entity.Nom_Carpeta,
                        Se_Cierra = Entity.Se_Cierra == null ? false : Convert.ToBoolean(Entity.Se_Cierra)
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(tb_modulo_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_modulo Entity = new tb_modulo
                    {
                        CodModulo = info.CodModulo,
                        Descripcion = info.Descripcion,
                        Nom_Carpeta = info.Nom_Carpeta,
                        Se_Cierra = info.Se_Cierra

                    };
                    Context.tb_modulo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(tb_modulo_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_modulo Entity = Context.tb_modulo.FirstOrDefault(q => q.CodModulo == info.CodModulo);
                    if (Entity == null) return false;

                    Entity.CodModulo = info.CodModulo;
                    Entity.Descripcion = info.Descripcion;
                    Entity.Nom_Carpeta = info.Nom_Carpeta;
                    Entity.Se_Cierra = info.Se_Cierra;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodModulo(string CodModulo)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_modulo
                              where CodModulo == q.CodModulo
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

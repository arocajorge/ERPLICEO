using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_Catalogo_Data
    {
        public List<tb_Catalogo_Info> get_list(int IdTipoCatalogo, bool mostrar_anulados)
        {
            try
            {
                List<tb_Catalogo_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.tb_Catalogo
                             where q.IdTipoCatalogo == IdTipoCatalogo
                             select new tb_Catalogo_Info
                             {
                                 IdCatalogo =q.IdCatalogo,
                                 IdTipoCatalogo = q.IdTipoCatalogo,
                                 ca_descripcion = q.ca_descripcion,
                                 ca_estado = q.ca_estado,
                                 ca_orden = q.ca_orden,
                                 CodCatalogo = q.CodCatalogo,

                                 EstadoBool = q.ca_estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_Catalogo
                                 where q.ca_estado == "A"
                                 && q.IdTipoCatalogo == IdTipoCatalogo
                                 select new tb_Catalogo_Info
                                 {
                                     IdCatalogo = q.IdCatalogo,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     ca_estado = q.ca_estado,
                                     ca_orden = q.ca_orden,
                                     CodCatalogo = q.CodCatalogo,

                                     EstadoBool = q.ca_estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_Catalogo_Info get_info(string CodCatalogo)
        {
            try
            {
                tb_Catalogo_Info info = new tb_Catalogo_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_Catalogo Entity = Context.tb_Catalogo.FirstOrDefault(q => q.CodCatalogo == CodCatalogo);
                    if (Entity == null) return null;
                    info = new tb_Catalogo_Info
                    {
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        CodCatalogo = Entity.CodCatalogo,
                        ca_descripcion = Entity.ca_descripcion,
                        ca_orden = Entity.ca_orden
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(tb_Catalogo_Info info)
        {
            try
            {
                //Inicializo base de datos
                using (Entities_general Context = new Entities_general())
                {
                    //Creas un objeto de tipo tabla
                    tb_Catalogo Entity = new tb_Catalogo
                    {
                        CodCatalogo = info.CodCatalogo,
                        IdCatalogo = info.IdCatalogo = get_id(),
                        ca_descripcion = info.ca_descripcion,
                        ca_estado = info.ca_estado = "A",
                        ca_orden = info.ca_orden,
                        IdTipoCatalogo = info.IdTipoCatalogo
                    };
                    //Agregamos objeto de tipo tabla a la base de datos
                    Context.tb_Catalogo.Add(Entity);
                    //Guardamos cambios en la base de datos
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(tb_Catalogo_Info info)
        {
            try
            {
                //Inicializo base de datos
                using (Entities_general Context = new Entities_general())
                {
                    //Creo objeto de tipo tabla y lo lleno con la info de la base
                    tb_Catalogo Entity = Context.tb_Catalogo.FirstOrDefault(q => q.CodCatalogo == info.CodCatalogo);
                    //Valido si encontre registro
                    if (Entity == null) return false;//Si no encuentro nada retorno false
                    //Caso contrario paso los cambios del info al entity
                    Entity.ca_descripcion = info.ca_descripcion;
                    Entity.ca_orden = info.ca_orden;
                    //Grabo cambios
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(tb_Catalogo_Info info)
        {
            try
            {
                //inicializar base
                using (Entities_general Context = new Entities_general())
                {
                    //crear objeto tipotabla con la info de la base
                    tb_Catalogo Entity = Context.tb_Catalogo.FirstOrDefault(q => q.CodCatalogo == info.CodCatalogo);
                    //validar si se encontro el regist
                    if (Entity == null) return false;
                    //pasar los cambios
                    Entity.ca_estado = info.ca_estado = "I";
                    //graba cambios
                    Context.SaveChanges();
                }
                return true;
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
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_Catalogo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q=>q.IdCatalogo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodCatalogo(string CodCatalogo)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_Catalogo
                              where CodCatalogo == q.CodCatalogo
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

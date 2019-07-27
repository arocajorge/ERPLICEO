using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_provincia_Data
    {
        public List<tb_provincia_Info> get_list(string IdPais, bool mostrar_anulados)
        {
            try
            {
                List<tb_provincia_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if(mostrar_anulados == true)
                    Lista = (from q in Context.tb_provincia
                             join p in Context.tb_pais
                             on q.IdPais equals p.IdPais
                             where q.IdPais == IdPais
                             select new tb_provincia_Info
                             {
                                 IdProvincia = q.IdProvincia,
                                 Cod_Provincia = q.Cod_Provincia,
                                 Descripcion_Prov = q.Descripcion_Prov,
                                 Estado = q.Estado,
                                 info_pais = new tb_pais_Info
                                 {
                                     Nombre = p.Nombre
                                 },

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_provincia
                                 join p in Context.tb_pais
                                 on q.IdPais equals p.IdPais
                                 where q.IdPais== IdPais
                                 && q.Estado == "A"
                                 select new tb_provincia_Info
                                 {
                                     IdProvincia = q.IdProvincia,
                                     Cod_Provincia = q.Cod_Provincia,
                                     Descripcion_Prov = q.Descripcion_Prov,
                                     Estado = q.Estado,
                                     info_pais = new tb_pais_Info
                                     {
                                         Nombre = p.Nombre
                                     },

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
        
        private string get_id()
        {
            try
            {
                int ID = 1;
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.vwtb_provincia
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdProvincia) + 1;
                }
                return ID.ToString("0000");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_provincia_Info get_info(string IdProvincia)
        {
            try
            {
                tb_provincia_Info info = new tb_provincia_Info();

                using (Entities_general Context = new Entities_general())
                {
                    tb_provincia Entity = Context.tb_provincia.FirstOrDefault(q => q.IdProvincia == IdProvincia);
                    if (Entity == null) return null;
                    info = new tb_provincia_Info
                    {
                        IdProvincia = Entity.IdProvincia,
                        Cod_Provincia = Entity.Cod_Provincia,
                        Descripcion_Prov = Entity.Descripcion_Prov,
                        Estado = Entity.Estado,
                        Cod_Region = Entity.Cod_Region,
                        IdPais = Entity.IdPais
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_provincia_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_provincia Entity = new tb_provincia
                    {
                        IdProvincia = info.IdProvincia=get_id(),
                        Cod_Provincia = info.Cod_Provincia,
                        Descripcion_Prov = info.Descripcion_Prov,                        
                        IdPais = info.IdPais,
                        Cod_Region = info.Cod_Region,
                        Estado = info.Estado = "A",

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac,
                    };
                    Context.tb_provincia.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_provincia_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_provincia Entity = Context.tb_provincia.FirstOrDefault(q => q.IdProvincia == info.IdProvincia);
                    if (Entity == null) return false;

                    Entity.Cod_Provincia = info.Cod_Provincia;
                    Entity.Descripcion_Prov = info.Descripcion_Prov;
                    Entity.IdPais = info.IdPais;
                    Entity.Cod_Region = info.Cod_Region;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_provincia_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_provincia Entity = Context.tb_provincia.FirstOrDefault(q =>q.IdProvincia == info.IdProvincia);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu;
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

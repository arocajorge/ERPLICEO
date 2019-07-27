using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_pais_Data
    {
        public List<tb_pais_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<tb_pais_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.tb_pais
                             select new tb_pais_Info
                             {
                                 CodPais = q.CodPais,
                                 IdPais = q.IdPais,
                                 Nombre = q.Nombre,
                                 Nacionalidad = q.Nacionalidad,
                                 estado = q.estado,

                                 EstadoBool = q.estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_pais
                                 where q.estado == "A"
                                 select new tb_pais_Info
                                 {
                                     CodPais = q.CodPais,
                                     IdPais = q.IdPais,
                                     Nombre = q.Nombre,
                                     Nacionalidad = q.Nacionalidad,
                                     estado = q.estado,

                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string get_id()
        {
            try
            {
                string ID = "";
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.vwtb_pais
                              select q;

                    if (lst.Count() > 0)
                    {
                        ID = (Convert.ToInt32(lst.Max(q => q.IdPais)) + 1).ToString("00000");
                    }
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_pais_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_pais Entity = new tb_pais
                    {
                        IdPais = info.IdPais = get_id(),
                        CodPais = info.CodPais,
                        Nombre = info.Nombre,
                        Nacionalidad = info.Nacionalidad,
                        estado = info.estado = "A"
                    };
                    Context.tb_pais.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_pais_Info get_info(string IdPais)
        {
            try
            {
                tb_pais_Info info = new tb_pais_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_pais Entity = Context.tb_pais.FirstOrDefault(q => q.IdPais == IdPais);
                    if (Entity == null) return null;

                    info = new tb_pais_Info
                    {
                        IdPais = Entity.IdPais,
                        CodPais = Entity.CodPais,
                        Nombre = Entity.Nombre,
                        Nacionalidad = Entity.Nacionalidad,
                        estado = Entity.estado

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_pais_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_pais Entity = Context.tb_pais.FirstOrDefault(q => q.IdPais == info.IdPais);
                    if (Entity == null)
                        return false;
                    Entity.CodPais = info.CodPais;
                    Entity.Nombre = info.Nombre;
                    Entity.Nacionalidad = info.Nacionalidad;
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_pais_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_pais Entity = Context.tb_pais.FirstOrDefault(q => q.IdPais == info.IdPais);
                    if (Entity == null)
                        return false;
                    Entity.estado = info.estado = "I";
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

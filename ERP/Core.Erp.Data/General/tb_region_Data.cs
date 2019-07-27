using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_region_Data
    {
        public List<tb_region_Info> get_list(string IdPais, bool mostrar_anulados)
        {
            try
            {
                List<tb_region_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.tb_region
                             where q.IdPais == IdPais
                             select new tb_region_Info
                             {
                                 Cod_Region = q.Cod_Region,
                                 Nom_region = q.Nom_region,
                                 codigo = q.codigo,
                                 estado = q.estado,
                                 IdPais = q.IdPais
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_region
                                 where q.IdPais == IdPais
                                 && q.estado == true
                                 select new tb_region_Info
                                 {
                                     Cod_Region = q.Cod_Region,
                                     Nom_region = q.Nom_region,
                                     codigo = q.codigo,
                                     estado = q.estado,
                                     IdPais = q.IdPais
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
                string ID = "00001";

                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_region
                              select q;

                    if (lst.Count() > 0)
                        ID = (Convert.ToInt32(lst.Max(q => q.Cod_Region))+1).ToString("00000");
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_region_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_region Entity = new tb_region
                    {
                        Cod_Region = info.Cod_Region = get_id(),
                        Nom_region = info.Nom_region,
                        codigo = info.codigo,
                        estado = info.estado = true,
                        IdPais = info.IdPais
                    };
                    Context.tb_region.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_region_Info get_info(string CodRegion)
        {
            try
            {
                tb_region_Info info = new tb_region_Info();

                using (Entities_general Context = new Entities_general())
                {
                    tb_region Entity = Context.tb_region.FirstOrDefault(q => q.Cod_Region == CodRegion);
                    if (Entity == null) return null;

                    info = new tb_region_Info
                    {
                        Cod_Region = Entity.Cod_Region,
                        codigo = Entity.codigo,
                        Nom_region = Entity.Nom_region,
                        estado = Entity.estado,
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

        public bool modificarDB(tb_region_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_region Entity = Context.tb_region.FirstOrDefault(q =>  q.Cod_Region == info.Cod_Region);
                    if (Entity == null)
                        return false;
                    Entity.codigo = info.codigo;
                    Entity.Nom_region = info.Nom_region;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_region_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_region Entity = Context.tb_region.FirstOrDefault(q =>  q.Cod_Region == info.Cod_Region);
                    if (Entity == null)
                        return false;
                    Entity.estado = info.estado = false;
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

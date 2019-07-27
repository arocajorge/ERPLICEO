using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_parroquia_Data
    {
        public List<tb_parroquia_Info> get_list(string IdCiudad, bool mostrar_anulados)
        {
            try
            {
                List<tb_parroquia_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                    {
                        if(!string.IsNullOrEmpty(IdCiudad))
                        Lista = (from q in Context.tb_parroquia
                                 where q.IdCiudad_Canton == IdCiudad
                                 select new tb_parroquia_Info
                                 {
                                     IdCiudad_Canton = q.IdCiudad_Canton,
                                     IdParroquia = q.IdParroquia,
                                     cod_parroquia = q.cod_parroquia,
                                     nom_parroquia = q.nom_parroquia,
                                     estado = q.estado
                                 }).ToList();
                        else
                            Lista = (from q in Context.tb_parroquia
                                     select new tb_parroquia_Info
                                     {
                                         IdCiudad_Canton = q.IdCiudad_Canton,
                                         IdParroquia = q.IdParroquia,
                                         cod_parroquia = q.cod_parroquia,
                                         nom_parroquia = q.nom_parroquia,
                                         estado = q.estado
                                     }).ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(IdCiudad))
                            Lista = (from q in Context.tb_parroquia
                                 where q.IdCiudad_Canton == IdCiudad
                                 && q.estado == true
                                 select new tb_parroquia_Info
                                 {
                                     IdCiudad_Canton = q.IdCiudad_Canton,
                                     IdParroquia = q.IdParroquia,
                                     cod_parroquia = q.cod_parroquia,
                                     nom_parroquia = q.nom_parroquia,
                                     estado = q.estado,
                                 }).ToList();
                        else
                            Lista = (from q in Context.tb_parroquia
                                     where q.estado == true
                                     select new tb_parroquia_Info
                                     {
                                         IdCiudad_Canton = q.IdCiudad_Canton,
                                         IdParroquia = q.IdParroquia,
                                         cod_parroquia = q.cod_parroquia,
                                         nom_parroquia = q.nom_parroquia,
                                         estado = q.estado,
                                     }).ToList();
                    }
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
                    var lst = from q in Context.vwtb_parroquia
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdParroquia) +1;
                }

                return ID.ToString("0000");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_parroquia_Info get_info( string IdParroquia)
        {
            try
            {
                tb_parroquia_Info info = new tb_parroquia_Info();

                using (Entities_general Context = new Entities_general())
                {
                    tb_parroquia Entity = Context.tb_parroquia.FirstOrDefault(q => q.IdParroquia == IdParroquia);
                    if (Entity == null) return null;
                    info = new tb_parroquia_Info
                    {
                        IdCiudad_Canton = Entity.IdCiudad_Canton,
                        IdParroquia = Entity.IdParroquia,
                        cod_parroquia = Entity.cod_parroquia,
                        nom_parroquia = Entity.nom_parroquia,
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

        public bool guardarDB(tb_parroquia_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_parroquia Entity = new tb_parroquia
                    {
                        IdCiudad_Canton = info.IdCiudad_Canton,
                        IdParroquia = info.IdParroquia=get_id(),
                        cod_parroquia = info.cod_parroquia,
                        nom_parroquia = info.nom_parroquia,
                        estado = true,
                        Fecha_Transac = info.Fecha_Transac,
                        IdUsuario=info.IdUsuario
                    };
                    Context.tb_parroquia.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_parroquia_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_parroquia Entity = Context.tb_parroquia.FirstOrDefault(q => q.IdParroquia == info.IdParroquia);
                    if (Entity == null) return false;

                    Entity.cod_parroquia = info.cod_parroquia;
                    Entity.nom_parroquia = info.nom_parroquia;
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

        public bool anularDB(tb_parroquia_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_parroquia Entity = Context.tb_parroquia.FirstOrDefault(q =>q.IdParroquia == info.IdParroquia);
                    if (Entity == null) return false;
                    Entity.estado = info.estado = false;

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

using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Importacion
{
   public class imp_partida_arancelaria_Data
    {
        public List<imp_partida_arancelaria_Info> get_list(bool mostrar_Anulados)
        {
            try
            {
                List<imp_partida_arancelaria_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    if(mostrar_Anulados)
                    Lista = (from q in Context.imp_partida_arancelaria
                             select new imp_partida_arancelaria_Info
                             {
                                 IdArancel = q.IdArancel,
                                 CodigoPartidaArancelaria = q.CodigoPartidaArancelaria,
                                 Descripcion = q.Descripcion,
                                 Estado = q.Estado,
                                 Observacion = q.Observacion,
                                 TarifaArancelaria = q.TarifaArancelaria
                             }).ToList();
                    else
                        Lista = (from q in Context.imp_partida_arancelaria
                                 select new imp_partida_arancelaria_Info
                                 {
                                     IdArancel = q.IdArancel,
                                     CodigoPartidaArancelaria = q.CodigoPartidaArancelaria,
                                     Descripcion = q.Descripcion,
                                     Estado = true,
                                     Observacion = q.Observacion,
                                     TarifaArancelaria = q.TarifaArancelaria
                                 }).ToList();

                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_partida_arancelaria_Info get_info(decimal IdArancel)
        {
            try
            {
                imp_partida_arancelaria_Info info = new imp_partida_arancelaria_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_partida_arancelaria Entity = Context.imp_partida_arancelaria.Where(q => q.IdArancel == IdArancel).FirstOrDefault();
                    if (Entity == null) return null;

                    info = new imp_partida_arancelaria_Info
                    {
                        IdArancel = Entity.IdArancel,
                        CodigoPartidaArancelaria = Entity.CodigoPartidaArancelaria,
                        Descripcion = Entity.Descripcion,
                        Estado = Entity.Estado,
                        Observacion = Entity.Observacion,
                        TarifaArancelaria = Entity.TarifaArancelaria
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal get_id()
        {
            try
            {
                decimal ID = 1;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    var lst = from q in Context.imp_partida_arancelaria
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdArancel) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_si_existe_codigo(string CodigoPartidaArancelaria)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    var lst = from q in Context.imp_partida_arancelaria
                              where q.CodigoPartidaArancelaria == CodigoPartidaArancelaria
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

        public bool guardarDB(imp_partida_arancelaria_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_partida_arancelaria Entity = new imp_partida_arancelaria
                    {
                        IdArancel = info.IdArancel=get_id(),
                        CodigoPartidaArancelaria = info.CodigoPartidaArancelaria,
                        Descripcion = info.Descripcion,
                        Estado = true,
                        Observacion = info.Observacion,
                        TarifaArancelaria = info.TarifaArancelaria
                    };
                    Context.imp_partida_arancelaria.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(imp_partida_arancelaria_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_partida_arancelaria Entity = Context.imp_partida_arancelaria.Where(q => q.IdArancel == info.IdArancel).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.CodigoPartidaArancelaria = info.CodigoPartidaArancelaria;
                    Entity.Descripcion = info.Descripcion;
                    Entity.Observacion = info.Observacion;
                    Entity.TarifaArancelaria = info.TarifaArancelaria;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(imp_partida_arancelaria_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_partida_arancelaria Entity = Context.imp_partida_arancelaria.Where(q => q.IdArancel == info.IdArancel).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = false;

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

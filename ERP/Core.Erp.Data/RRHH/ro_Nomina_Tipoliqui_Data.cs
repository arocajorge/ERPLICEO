using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;

namespace Core.Erp.Data.RRHH
{
   public class ro_Nomina_Tipoliqui_Data
    {

        public List<ro_Nomina_Tipoliqui_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_Nomina_Tipoliqui_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_Nomina_Tipoliqui
                                 join p in Context.ro_Nomina_Tipo
                                 on q.IdNomina_Tipo equals p.IdNomina_Tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdEmpresa==p.IdEmpresa
                                 select new ro_Nomina_Tipoliqui_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNomina_Tipo = q.IdNomina_Tipo,
                                     IdNomina_TipoLiqui = q.IdNomina_TipoLiqui,
                                     DescripcionProcesoNomina=q.DescripcionProcesoNomina,
                                     Estado = q.Estado,
                                     Descripcion=p.Descripcion,

                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_Nomina_Tipoliqui
                                 join p in Context.ro_Nomina_Tipo
                                 on q.IdNomina_Tipo equals p.IdNomina_Tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdEmpresa == p.IdEmpresa
                                 && q.Estado == "A"
                                 select new ro_Nomina_Tipoliqui_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNomina_Tipo = q.IdNomina_Tipo,
                                     IdNomina_TipoLiqui = q.IdNomina_TipoLiqui,
                                     DescripcionProcesoNomina = q.DescripcionProcesoNomina,
                                     Descripcion = p.Descripcion,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }
                Lista.ForEach(q => q.IdString = q.IdNomina_Tipo.ToString("000") + q.IdNomina_TipoLiqui.ToString("000"));

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_Nomina_Tipoliqui_Info> get_list(int IdEmpresa, int IdNominaTipo)
        {
            try
            {
                List<ro_Nomina_Tipoliqui_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.ro_Nomina_Tipoliqui
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdNomina_Tipo== IdNominaTipo
                                 select new ro_Nomina_Tipoliqui_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNomina_Tipo = q.IdNomina_Tipo,
                                     IdNomina_TipoLiqui = q.IdNomina_TipoLiqui,
                                     DescripcionProcesoNomina = q.DescripcionProcesoNomina,
                                     Estado = q.Estado
                                 }).ToList();
                   
                }
                Lista.ForEach(q => q.IdString = q.IdNomina_Tipo.ToString("000") + q.IdNomina_TipoLiqui.ToString("000"));

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_Nomina_Tipoliqui_Info get_info(int IdEmpresa, int IdNomina_Tipo, int IdNominaTipoLiq)
        {
            try
            {
                ro_Nomina_Tipoliqui_Info info = new ro_Nomina_Tipoliqui_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipoliqui Entity = Context.ro_Nomina_Tipoliqui.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdNomina_Tipo==IdNomina_Tipo && q.IdNomina_TipoLiqui == IdNominaTipoLiq);
                    if (Entity == null) return null;

                    info = new ro_Nomina_Tipoliqui_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNomina_Tipo = Entity.IdNomina_Tipo,
                        IdNomina_TipoLiqui = Entity.IdNomina_TipoLiqui,
                        DescripcionProcesoNomina = Entity.DescripcionProcesoNomina,
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

        public int get_id(int IdEmpresa, int IdNominaTipo)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_Nomina_Tipoliqui
                              where q.IdEmpresa == IdEmpresa
                              && q.IdNomina_Tipo==IdNominaTipo
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdNomina_TipoLiqui) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_Nomina_Tipoliqui_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipoliqui Entity = new ro_Nomina_Tipoliqui
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNomina_Tipo = info.IdNomina_Tipo,
                        IdNomina_TipoLiqui = get_id(info.IdEmpresa,info.IdNomina_Tipo),
                        DescripcionProcesoNomina = info.DescripcionProcesoNomina,
                        Estado  = "A",
                        IdUsuario = info.IdUsuario,
                        FechaTransac = DateTime.Now
                    };
                    Context.ro_Nomina_Tipoliqui.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_Nomina_Tipoliqui_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipoliqui Entity = Context.ro_Nomina_Tipoliqui.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNomina_Tipo == info.IdNomina_Tipo && q.IdNomina_TipoLiqui == info.IdNomina_TipoLiqui);
                    if (Entity == null)
                        return false;
                    Entity.DescripcionProcesoNomina = info.DescripcionProcesoNomina;
                    Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                    Entity.FechaTransac = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_Nomina_Tipoliqui_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipoliqui Entity = Context.ro_Nomina_Tipoliqui.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNomina_Tipo == info.IdNomina_Tipo && q.IdNomina_TipoLiqui == info.IdNomina_TipoLiqui);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioAnu = info.IdUsuarioAnu;
                    Entity.FechaAnu = DateTime.Now;
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

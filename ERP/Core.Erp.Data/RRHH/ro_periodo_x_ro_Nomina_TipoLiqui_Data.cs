using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
  public  class ro_periodo_x_ro_Nomina_TipoLiqui_Data
    {

        public List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> get_list(int IdEmpresa, int IdNominTipo, int IdNominaTipo_liq)
        {
            try
            {
                List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> Lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_periodo
                             join r in Context.ro_periodo_x_ro_Nomina_TipoLiqui
                             on new { q.IdEmpresa , q.IdPeriodo} equals new { r.IdEmpresa, r.IdPeriodo }
                             where q.IdEmpresa == IdEmpresa
                             && r.IdNomina_Tipo == IdNominTipo
                             && r.IdNomina_TipoLiqui==IdNominaTipo_liq
                             select new ro_periodo_x_ro_Nomina_TipoLiqui_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPeriodo=q.IdPeriodo,
                                  IdNomina_Tipo=r.IdNomina_Tipo,
                                  IdNomina_TipoLiqui=r.IdNomina_TipoLiqui,
                                  Procesado=r.Procesado,
                                  Cerrado=r.Cerrado,
                                  Contabilizado=r.Contabilizado,
                                  pe_FechaFin=q.pe_FechaFin,
                                  pe_FechaIni=q.pe_FechaIni,
                                  seleccionado = true,
                                  esta_base=true
                                  
                             }).ToList();

                    
                        Lista.AddRange((from q in Context.ro_periodo
                                        where !Context.ro_periodo_x_ro_Nomina_TipoLiqui.Any(meu => meu.IdEmpresa == q.IdEmpresa && meu.IdPeriodo==q.IdPeriodo&& meu.IdNomina_Tipo == IdNominTipo && meu.IdNomina_TipoLiqui == IdNominaTipo_liq)
                                        && q.pe_estado == "A"
                                        && q.IdEmpresa==IdEmpresa
                                        select new ro_periodo_x_ro_Nomina_TipoLiqui_Info
                                        {
                                            IdEmpresa = IdEmpresa,
                                            IdPeriodo = q.IdPeriodo,
                                            pe_FechaFin = q.pe_FechaFin,
                                            pe_FechaIni = q.pe_FechaIni,
                                            seleccionado = false
                                        }).ToList());
                    
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> get_list_peridos(int IdEmpresa, int IdNominTipo, int IdNominaTipo_liq)
        {
            try
            {
                List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> Lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_periodo
                             join r in Context.ro_periodo_x_ro_Nomina_TipoLiqui
                             on new { q.IdEmpresa, q.IdPeriodo } equals new { r.IdEmpresa, r.IdPeriodo }
                             where q.IdEmpresa == IdEmpresa
                             && r.IdNomina_Tipo == IdNominTipo
                             && r.IdNomina_TipoLiqui == IdNominaTipo_liq
                             select new ro_periodo_x_ro_Nomina_TipoLiqui_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPeriodo = q.IdPeriodo,
                                 IdNomina_Tipo = r.IdNomina_Tipo,
                                 IdNomina_TipoLiqui = r.IdNomina_TipoLiqui,
                                 Procesado = r.Procesado,
                                 Cerrado = r.Cerrado,
                                 Contabilizado = r.Contabilizado,
                                 pe_FechaFin = q.pe_FechaFin,
                                 pe_FechaIni = q.pe_FechaIni,
                                 seleccionado = true,
                                 esta_base = true

                             }).ToList();


                  

                }

                Lista.ForEach(v => v.descripcion = v.pe_FechaIni.ToString().Substring(0, 10) + " " + v.pe_FechaFin.ToString().Substring(0, 10)

                    );
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }




        public List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> get_list_utimo_periodo_aprocesar(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiq)
        {
            try
            {
                List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> Lista;


                int SiguientePeriodo = 0;
              
                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    SiguientePeriodo = Context.ro_periodo_x_ro_Nomina_TipoLiqui.Where(c => c.Procesado =="N").Min(c => c.IdPeriodo);
                    Lista = (from q in Context.ro_periodo_x_ro_Nomina_TipoLiqui
                             join p in Context.ro_periodo
                             on q.IdPeriodo equals p.IdPeriodo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdNomina_Tipo == IdNominaTipo
                             && q.IdNomina_TipoLiqui == IdNominaTipoLiq
                             && q.IdEmpresa == p.IdEmpresa
                             && q.Procesado=="N"
                             && q.IdPeriodo== SiguientePeriodo
                             select new ro_periodo_x_ro_Nomina_TipoLiqui_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdNomina_Tipo = q.IdNomina_Tipo,
                                 IdNomina_TipoLiqui = q.IdNomina_TipoLiqui,
                                 IdPeriodo = q.IdPeriodo,
                                 Contabilizado = q.Contabilizado,
                                 Cerrado = q.Cerrado,
                                 Procesado = q.Procesado,
                                 pe_FechaIni = p.pe_FechaIni,
                                 pe_FechaFin = p.pe_FechaFin
                             }).ToList();
                }
                Lista.ForEach(v => v.descripcion = v.pe_FechaIni.ToString("dd/MM/yyyy").Substring(0, 10) + "                  al                  " + v.pe_FechaFin.ToString("dd/MM/yyyy").Substring(0, 10));

                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_periodo_x_ro_Nomina_TipoLiqui_Info get_info(int IdEmpresa, int IdNomina_Tipo, int IdNominaTipoLiq, int IdPeriodo)
        {
            try
            {
                ro_periodo_x_ro_Nomina_TipoLiqui_Info info = new ro_periodo_x_ro_Nomina_TipoLiqui_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_periodo_x_ro_Nomina_TipoLiqui Entity = Context.ro_periodo_x_ro_Nomina_TipoLiqui.FirstOrDefault(q => q.IdEmpresa == IdEmpresa 
                    && q.IdNomina_Tipo == IdNomina_Tipo 
                    && q.IdNomina_TipoLiqui == IdNominaTipoLiq
                    && q.IdPeriodo==info.IdPeriodo);
                    if (Entity == null) return null;

                    info = new ro_periodo_x_ro_Nomina_TipoLiqui_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNomina_Tipo = Entity.IdNomina_Tipo,
                        IdNomina_TipoLiqui = Entity.IdNomina_TipoLiqui,
                        Procesado = Entity.Procesado,
                        Cerrado = Entity.Cerrado,
                        Contabilizado=Entity.Contabilizado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lista, int IdEmpresa, int IdNomina_Tipo, int IdNomina_TipoLiqui)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst_periodos = Context.ro_periodo_x_ro_Nomina_TipoLiqui.Where(v => v.IdEmpresa == IdEmpresa
                      && v.IdNomina_Tipo == IdNomina_Tipo && v.IdNomina_TipoLiqui == IdNomina_TipoLiqui
                      && v.Procesado == "N");
                      Context.ro_periodo_x_ro_Nomina_TipoLiqui.RemoveRange(lst_periodos);

                    foreach (var item in lista)
                    {
                        ro_periodo_x_ro_Nomina_TipoLiqui Entity = new ro_periodo_x_ro_Nomina_TipoLiqui
                        {
                            IdEmpresa = IdEmpresa,
                            IdNomina_Tipo = IdNomina_Tipo,
                            IdNomina_TipoLiqui = IdNomina_TipoLiqui,
                            IdPeriodo=item.IdPeriodo,
                            Procesado = "N",
                            Cerrado = "N",
                            Contabilizado = "N"
                        };
                        if(item.Procesado!="S" )
                        Context.ro_periodo_x_ro_Nomina_TipoLiqui.Add(Entity);
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool guardarDB(ro_periodo_x_ro_Nomina_TipoLiqui_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_periodo_x_ro_Nomina_TipoLiqui Entity = new ro_periodo_x_ro_Nomina_TipoLiqui
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNomina_Tipo = info.IdNomina_Tipo,
                        IdNomina_TipoLiqui = info.IdNomina_TipoLiqui,
                        Procesado = "N",
                        Cerrado = "N",
                        Contabilizado = "N"
                    };
                    Context.ro_periodo_x_ro_Nomina_TipoLiqui.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_periodo_x_ro_Nomina_TipoLiqui_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_periodo_x_ro_Nomina_TipoLiqui Entity = Context.ro_periodo_x_ro_Nomina_TipoLiqui.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdNomina_Tipo == info.IdNomina_Tipo 
                    && q.IdNomina_TipoLiqui == info.IdNomina_TipoLiqui 
                    && q.IdPeriodo==info.IdPeriodo);
                    if (Entity == null)
                        return false;
                    Context.ro_periodo_x_ro_Nomina_TipoLiqui.Remove(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int get_siguinte_periodo_a_procesar(int IdEmpresa, int IdNomina_Tipo,int IdNomina_TipoLiqui)
        {
            try
            {
                int IdPeriodo = 0;


                using (Entities_rrhh contet=new Entities_rrhh())
                {
                    var lst = from q in contet.ro_periodo_x_ro_Nomina_TipoLiqui
                              where q.IdEmpresa == IdEmpresa
                              && q.IdNomina_Tipo==IdNomina_Tipo
                              && q.IdNomina_TipoLiqui==IdNomina_TipoLiqui
                              && q.Procesado=="N"
                              select q;

                    if (lst.Count() > 0)
                        IdPeriodo = lst.Min(q => q.IdPeriodo);
                }
                return IdPeriodo;
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}

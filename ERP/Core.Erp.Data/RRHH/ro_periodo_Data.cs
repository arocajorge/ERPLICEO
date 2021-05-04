﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_periodo_Data
    {
        public int ffin, anio, mes;
        public List<ro_periodo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_periodo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_periodo
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_periodo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdPeriodo = q.IdPeriodo,
                                     pe_FechaIni = q.pe_FechaIni,
                                     pe_FechaFin = q.pe_FechaFin,
                                     Carga_Todos_Empl=q.Carga_Todos_Empleados,
                                     Cod_region=q.Cod_region,
                                     pe_mes=q.pe_mes,
                                     pe_anio=q.pe_anio,
                                     pe_estado=q.pe_estado,

                                     EstadoBool = q.pe_estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_periodo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.pe_estado == "A"
                                 select new ro_periodo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdPeriodo = q.IdPeriodo,
                                     pe_FechaIni = q.pe_FechaIni,
                                     pe_FechaFin = q.pe_FechaFin,
                                     Carga_Todos_Empl = q.Carga_Todos_Empleados,
                                     Cod_region = q.Cod_region,
                                     pe_mes = q.pe_mes,
                                     pe_anio = q.pe_anio,
                                     pe_estado = q.pe_estado,

                                     EstadoBool = q.pe_estado == "A" ? true : false
                                 }).ToList();
                }
                foreach (var item in Lista)
                {
                   item.Carga_Todos_Empleados = item.Carga_Todos_Empl == null ? false : Convert.ToBoolean(item.Carga_Todos_Empl);

                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_periodo_Info get_info(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                ro_periodo_Info info = new ro_periodo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_periodo Entity = Context.ro_periodo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPeriodo == IdPeriodo);
                    if (Entity == null) return null;

                    info = new ro_periodo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPeriodo = Entity.IdPeriodo,
                        pe_FechaIni = Entity.pe_FechaIni,
                        pe_FechaFin = Entity.pe_FechaFin,
                        Carga_Todos_Empleados = (Entity.Carga_Todos_Empleados)==null?false:Convert.ToBoolean(Entity.Carga_Todos_Empleados),
                        Cod_region = Entity.Cod_region,
                        pe_mes = Entity.pe_mes,
                        pe_anio = Entity.pe_anio,
                        pe_estado = Entity.pe_estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                string result;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_periodo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    // if (lst.Count() > 0)
                    //   ID = lst.Max(q => q.IdPeriodo) + 1;
                    result = "";
                    if ((ffin) == 15)
                    {
                        result = anio + digitos(mes) + "01";
                        ID = Convert.ToInt32(result);// quincenas
                    }
                    if ((ffin) != 15)
                    {
                        result = anio + digitos(mes);
                        ID = Convert.ToInt32(result);
                        var lst2 = from q in Context.ro_periodo where q.IdEmpresa == IdEmpresa && q.IdPeriodo == ID select q;
                        if (lst2.Count()<1 ) // null
                        {
                            ID = Convert.ToInt32(ID);// fin de mes
                        }
                        else
                        {
                            ID = lst.Max(q => q.IdPeriodo) + 1; // Bonos y demas Casos.
                        }

                    }

                    return ID;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string digitos( int numero)
        {
            string resultado="";
            switch (numero)
            {
                case 1: resultado = "01"; break;
                case 2: resultado = "02"; break;
                case 3: resultado = "03"; break;
                case 4: resultado = "04"; break;
                case 5: resultado = "05"; break;
                case 6: resultado = "06"; break;
                case 7: resultado = "07"; break;
                case 8: resultado = "08"; break;
                case 9: resultado = "09"; break;
                case 10: resultado = "10"; break;
                case 11: resultado = "11"; break;
                case 12: resultado = "12"; break;
            }
            return resultado;
        }
        public bool guardarDB(ro_periodo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ffin = info.pe_FechaFin.Date.Day;
                    anio = Convert.ToInt32(info.pe_anio);
                    mes = Convert.ToInt32(info.pe_mes);
                    ro_periodo Entity = new ro_periodo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPeriodo = get_id(info.IdEmpresa),
                        pe_FechaIni = info.pe_FechaIni.Date,
                        pe_FechaFin = info.pe_FechaFin.Date,
                        pe_anio = info.pe_anio,
                        pe_mes = info.pe_mes,
                        Carga_Todos_Empleados = info.Carga_Todos_Empleados,
                        Cod_region = info.Cod_region,
                        IdUsuario = info.IdUsuario,
                        pe_estado = "A",
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_periodo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardar_periodos_generadosDB(ro_periodo_Info info)
        {
            try
            {
                if (si_existe_periodo(info.IdEmpresa, info.IdPeriodo))
                    return false;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_periodo Entity = new ro_periodo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPeriodo = info.IdPeriodo,
                        pe_FechaIni = info.pe_FechaIni.Date,
                        pe_FechaFin = info.pe_FechaFin.Date,
                        Carga_Todos_Empleados = info.Carga_Todos_Empleados,
                        pe_anio=info.pe_anio,
                        pe_mes=info.pe_mes,
                        Cod_region = info.Cod_region,
                        IdUsuario=info.IdUsuario,
                        pe_estado = "A",
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_periodo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_periodo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_periodo Entity = Context.ro_periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == info.IdPeriodo);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.IdPeriodo = info.IdPeriodo;
                    Entity.pe_FechaIni = info.pe_FechaIni.Date;
                    Entity.pe_FechaFin = info.pe_FechaFin.Date;
                    Entity.Carga_Todos_Empleados = info.Carga_Todos_Empleados;
                    Entity.Cod_region = info.Cod_region;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_periodo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_periodo Entity = Context.ro_periodo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == info.IdPeriodo);
                    if (Entity == null)
                        return false;
                    Entity.pe_estado  = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.FechaHoraAnul  = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool si_existe_periodo(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_periodo
                              where q.IdEmpresa == IdEmpresa
                              && q.IdPeriodo==IdPeriodo
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

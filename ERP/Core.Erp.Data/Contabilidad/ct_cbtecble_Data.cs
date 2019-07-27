using Core.Erp.Data.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_cbtecble_Data
    {
        public List<ct_cbtecble_Info> get_list(int IdEmpresa,int IdSucursal, bool mostrar_anulados, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 999999 : IdSucursal;
                List<ct_cbtecble_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.vwct_cbtecble
                             where q.IdEmpresa == IdEmpresa
                             && fecha_ini <= q.cb_Fecha && q.cb_Fecha <= fecha_fin
                             && IdSucursal_ini <= q.IdSucursal && q.IdSucursal <= IdSucursal_fin
                             orderby q.cb_Fecha descending
                             select new ct_cbtecble_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 cb_Estado = q.cb_Estado,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_Observacion = q.cb_Observacion,
                                 cb_Valor = q.cb_Valor,
                                 CodCbteCble = q.CodCbteCble,
                                 IdCbteCble = q.IdCbteCble,
                                 IdTipoCbte = q.IdTipoCbte,
                                 tc_TipoCbte = q.tc_TipoCbte,
                                 Su_Descripcion = q.Su_Descripcion,
                                 EstadoBool = q.cb_Estado == "A" ? true : false
                             }).ToList();

                    else
                        Lista = (from q in Context.vwct_cbtecble
                                 where q.IdEmpresa == IdEmpresa
                                 && fecha_ini <= q.cb_Fecha && q.cb_Fecha <= fecha_fin
                                 && IdSucursal_ini <= q.IdSucursal && q.IdSucursal <= IdSucursal_fin
                                 && q.cb_Estado == "A"
                                 orderby q.cb_Fecha descending
                                 select new ct_cbtecble_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     cb_Estado = q.cb_Estado,
                                     cb_Fecha = q.cb_Fecha,                                     
                                     cb_Observacion = q.cb_Observacion,
                                     cb_Valor = q.cb_Valor,
                                     CodCbteCble = q.CodCbteCble,
                                     IdCbteCble = q.IdCbteCble,
                                     Su_Descripcion = q.Su_Descripcion,
                                     IdTipoCbte = q.IdTipoCbte,
                                     tc_TipoCbte = q.tc_TipoCbte,

                                     EstadoBool = q.cb_Estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_cbtecble_Info get_info(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
            {
            try
            {
                ct_cbtecble_Info info = new ct_cbtecble_Info();
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble Entity = Context.ct_cbtecble.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte == IdTipoCbte &&q.IdCbteCble == IdCbteCble);
                    if (Entity == null) return null;
                    info = new ct_cbtecble_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        cb_Estado = Entity.cb_Estado,
                        cb_Fecha = Entity.cb_Fecha,
                        IdSucursal = Entity.IdSucursal,
                        cb_Observacion = Entity.cb_Observacion,
                        cb_Valor = Entity.cb_Valor,
                        CodCbteCble = Entity.CodCbteCble,
                        IdCbteCble = Entity.IdCbteCble,
                        IdPeriodo = Entity.IdPeriodo,
                        IdTipoCbte = Entity.IdTipoCbte
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal get_id(int IdEmpresa, int IdTipoCbte)
        {
            try
            {
                decimal ID = 1;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_cbtecble
                              where q.IdEmpresa == IdEmpresa
                              && q.IdTipoCbte == IdTipoCbte
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCbteCble) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_cbtecble_Info info)
        {
            try
            {
                info.IdPeriodo = Convert.ToInt32(info.cb_Fecha.Year.ToString() + info.cb_Fecha.Month.ToString("00"));
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var periodo = Context.ct_periodo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPeriodo == info.IdPeriodo).FirstOrDefault();
                    if (periodo == null)
                        return false;

                    ct_cbtecble Entity = new ct_cbtecble
                    {

                        IdEmpresa = info.IdEmpresa,
                        cb_Estado = info.cb_Estado="A",
                        cb_Fecha = info.cb_Fecha.Date,
                        IdSucursal = info.IdSucursal,
                        cb_Observacion = info.cb_Observacion,
                        cb_Valor = info.lst_ct_cbtecble_det.Where(q=> q.dc_Valor > 0).Sum(q=>q.dc_Valor),
                        CodCbteCble = info.CodCbteCble,
                        IdCbteCble = info.IdCbteCble=get_id(info.IdEmpresa, info.IdTipoCbte),
                        IdPeriodo = info.IdPeriodo = Convert.ToInt32(info.cb_Fecha.Year.ToString()+ info.cb_Fecha.Month.ToString("00")),
                        IdTipoCbte = info.IdTipoCbte,
                        
                        IdUsuario = info.IdUsuario,
                        cb_FechaTransac = DateTime.Now
                    };
                    Context.ct_cbtecble.Add(Entity);
                    int secuencia = 1;
                    foreach (var item in info.lst_ct_cbtecble_det)
                    {
                            ct_cbtecble_det Entity_det = new ct_cbtecble_det
                            {
                                IdEmpresa = Entity.IdEmpresa,
                                IdCbteCble = Entity.IdCbteCble,
                                IdTipoCbte = Entity.IdTipoCbte,                                
                                dc_Observacion = item.dc_Observacion,
                                dc_Valor = item.dc_Valor,
                                IdCtaCble = item.IdCtaCble,                                
                                secuencia = secuencia++,
                                dc_para_conciliar = item.dc_para_conciliar,
                                IdCentroCosto = item.IdCentroCosto,
                                IdPunto_cargo = item.IdPunto_cargo,
                                IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                            };
                            Context.ct_cbtecble_det.Add(Entity_det);                        
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ct_cbtecble_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(ct_cbtecble_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble Entity = Context.ct_cbtecble.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipoCbte && q.IdCbteCble == info.IdCbteCble);
                    if (Entity == null) return false;

                    
                    Entity.cb_Fecha = info.cb_Fecha.Date;
                    Entity.IdSucursal = info.IdSucursal;
                    Entity.cb_Observacion = info.cb_Observacion;
                    Entity.cb_Valor = info.lst_ct_cbtecble_det.Where(q => q.dc_Valor > 0).Sum(q => q.dc_Valor);
                    Entity.CodCbteCble = info.CodCbteCble;
                    Entity.IdPeriodo = info.IdPeriodo = Convert.ToInt32(info.cb_Fecha.ToString("yyyyMM"));

                    Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                    Entity.cb_FechaUltModi = DateTime.Now;

                    Context.Database.ExecuteSqlCommand("DElETE ct_cbtecble_det WHERE IdEmpresa = " + info.IdEmpresa + " and IdTipoCbte = " + info.IdTipoCbte + " and IdCbteCble = " + info.IdCbteCble + "");

                    int secuencia = 1;
                    foreach (var item in info.lst_ct_cbtecble_det)
                    {
                        ct_cbtecble_det Entity_det = new ct_cbtecble_det
                        {
                            IdEmpresa = Entity.IdEmpresa,
                            IdCbteCble = Entity.IdCbteCble,
                            IdTipoCbte = Entity.IdTipoCbte,
                            dc_Observacion = item.dc_Observacion,
                            dc_Valor = item.dc_Valor,
                            IdCtaCble = item.IdCtaCble,
                            secuencia = secuencia++,
                            dc_para_conciliar = item.dc_para_conciliar,
                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_cargo = item.IdPunto_cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                        };
                        Context.ct_cbtecble_det.Add(Entity_det);
                    }
                    Context.SaveChanges();
                };
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ct_cbtecble_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        
        /// <param name="info">Debe ir llenos los PK y el IdUsuarioUltAnu</param>
        /// <returns></returns>
        public bool anularDB(ct_cbtecble_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble Entity = Context.ct_cbtecble.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipoCbte && q.IdCbteCble == info.IdCbteCble);
                    if (Entity == null) return false;

                    //Si ya esta anulado no volverlo a anular
                    if (Entity.cb_Estado == "I")
                        return true;

                    #region Comprobante reverso
                    ct_cbtecble_tipo e_tipo = Context.ct_cbtecble_tipo.Where(q=>q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipoCbte).FirstOrDefault();
                    if (e_tipo == null) return false;

                    #region Cabecera
                    ct_cbtecble Entity_reverso = new ct_cbtecble
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbte = e_tipo.IdTipoCbte_Anul,
                        IdCbteCble = get_id(Entity.IdEmpresa, e_tipo.IdTipoCbte_Anul),
                        IdSucursal = Entity.IdSucursal,
                        cb_Estado = Entity.cb_Estado = "A",
                        cb_Fecha = DateTime.Now.Date,
                        cb_Observacion = "**REVERSO DE DIARIO tipo: "+ Entity.IdTipoCbte.ToString()+ " #cbte: "+ Entity.IdCbteCble.ToString()+"** "+ Entity.cb_Observacion,
                        cb_Valor = Entity.cb_Valor,
                        CodCbteCble = "ANU"+ Entity.CodCbteCble,
                        IdPeriodo = Entity.IdPeriodo = Convert.ToInt32(DateTime.Now.ToString("yyyyMM")),

                        IdUsuario = info.IdUsuarioAnu,
                        cb_FechaTransac = DateTime.Now
                    };
                    Context.ct_cbtecble.Add(Entity_reverso);
                    #endregion

                    #region Detalle
                    var det = Context.ct_cbtecble_det.Where(q => q.IdEmpresa == Entity.IdEmpresa && q.IdTipoCbte == Entity.IdTipoCbte && q.IdCbteCble == Entity.IdCbteCble).ToList();
                    foreach (var item in det)
                    {
                        ct_cbtecble_det Entity_reverso_det = new ct_cbtecble_det
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTipoCbte = Entity_reverso.IdTipoCbte,
                            IdCbteCble = Entity_reverso.IdCbteCble,
                            secuencia = item.secuencia,
                            IdCtaCble = item.IdCtaCble,
                            dc_Observacion = "**REVERSO DE DIARIO tipo: " + Entity.IdTipoCbte.ToString() + " #cbte: " + Entity.IdCbteCble.ToString() + "** " + item.dc_Observacion,
                            dc_Valor = item.dc_Valor * -1,
                            dc_para_conciliar = false,
                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_cargo = item.IdPunto_cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                        };
                        Context.ct_cbtecble_det.Add(Entity_reverso_det);
                    }
                    #endregion

                    #endregion

                    #region Tabla intermedia
                    ct_cbtecble_Reversado Entity_int = new ct_cbtecble_Reversado
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdCbteCble = Entity.IdCbteCble,
                        IdEmpresa_Anu = Entity_reverso.IdEmpresa,
                        IdTipoCbte_Anu = Entity_reverso.IdTipoCbte,
                        IdCbteCble_Anu = Entity_reverso.IdCbteCble,

                    };
                    Context.ct_cbtecble_Reversado.Add(Entity_int);
                    #endregion

                    Entity.cb_MotivoAnu = info.cb_MotivoAnu;
                    Entity.cb_Estado = info.cb_Estado = "I";
                    Entity.IdUsuarioAnu = info.IdUsuarioAnu;
                    Entity.cb_FechaAnu = DateTime.Now;
                    Entity.cb_Observacion = "REVERSADO CON EL DIARIO tipo: " + Entity_reverso.IdTipoCbte.ToString() + " #cbte: " + Entity_reverso.IdCbteCble.ToString() + "** " + Entity.cb_Observacion;

                    Context.SaveChanges();
                };
                return true;
            }
            catch (Exception )
            {

                throw;
            }
        }

        public ct_cbtecble_Info armar_info(List<ct_cbtecble_det_Info> lista, int IdEmpresa, int IdSucursal, int IdTipoCbte, decimal IdCbteCble, string Observacion, DateTime Fecha)
        {
            try
            {
                ct_cbtecble_Info info = new ct_cbtecble_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdTipoCbte = IdTipoCbte,
                    IdCbteCble = IdCbteCble,
                    IdSucursal = IdSucursal,
                    cb_Observacion = Observacion,
                    cb_Fecha = Fecha,
                    cb_Valor = lista.Where(q=>q.dc_Valor > 0).Sum(q=>q.dc_Valor)
                };
                info.lst_ct_cbtecble_det = lista;
                info.lst_ct_cbtecble_det.ForEach(q => { q.IdEmpresa = IdEmpresa; q.IdTipoCbte = IdTipoCbte; q.IdCbteCble = IdCbteCble; });
                
                return info;
            }
            catch (Exception)
            {

                throw;
            }            
        }        
    }
}

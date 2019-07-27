using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_cobro_tipo_Data
    {
        public List<cxc_cobro_tipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<cxc_cobro_tipo_Info> Lista;
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.cxc_cobro_tipo
                                 select new cxc_cobro_tipo_Info
                                 {
                                     IdCobro_tipo = q.IdCobro_tipo,
                                     tc_abreviatura = q.tc_abreviatura,
                                     tc_descripcion = q.tc_descripcion,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.cxc_cobro_tipo
                                 where q.Estado == "A"
                                 select new cxc_cobro_tipo_Info
                                 {
                                     IdCobro_tipo = q.IdCobro_tipo,
                                     tc_abreviatura = q.tc_abreviatura,
                                     tc_descripcion = q.tc_descripcion,
                                     Estado = q.Estado,

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
        public List<cxc_cobro_tipo_Info> get_list_retenciones(bool mostrar_anulados)
        {
            try
            {
                List<cxc_cobro_tipo_Info> Lista;
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.cxc_cobro_tipo
                                 where q.IdMotivo_tipo_cobro == "RET"
                                 select new cxc_cobro_tipo_Info
                                 {
                                     IdCobro_tipo = q.IdCobro_tipo,
                                     tc_abreviatura = q.tc_abreviatura,
                                     tc_descripcion = q.tc_descripcion,
                                     Estado = q.Estado
                                 }).ToList();
                    else
                        Lista = (from q in Context.cxc_cobro_tipo
                                 where q.Estado == "A"
                                 && q.IdMotivo_tipo_cobro == "RET"
                                 select new cxc_cobro_tipo_Info
                                 {
                                     IdCobro_tipo = q.IdCobro_tipo,
                                     tc_abreviatura = q.tc_abreviatura,
                                     tc_descripcion = q.tc_descripcion,
                                     Estado = q.Estado
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public cxc_cobro_tipo_Info get_info(string IdCobro_tipo)
        {
            try
            {
                cxc_cobro_tipo_Info info = new cxc_cobro_tipo_Info();
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_cobro_tipo Entity = Context.cxc_cobro_tipo.FirstOrDefault(q => q.IdCobro_tipo == IdCobro_tipo);
                    if (Entity == null) return null;
                    info = new cxc_cobro_tipo_Info
                    {
                        IdCobro_tipo = Entity.IdCobro_tipo,
                        ESRetenFTE_bool = Entity.ESRetenFTE == "S" ? true : false,
                        ESRetenIVA_bool = Entity.ESRetenIVA == "S" ? true : false,
                        ESRetenFTE = Entity.ESRetenFTE,
                        ESRetenIVA = Entity.ESRetenIVA,
                        IdMotivo_tipo_cobro = Entity.IdMotivo_tipo_cobro,
                        tc_abreviatura = Entity.tc_abreviatura,
                        tc_descripcion = Entity.tc_descripcion,
                        tc_Tomar_Cta_Cble_De = Entity.tc_Tomar_Cta_Cble_De,
                        Estado = Entity.Estado,
                        PorcentajeRet = Entity.PorcentajeRet,
                        EsTarjetaCredito = Entity.EsTarjetaCredito,
                        SeDeposita = Entity.SeDeposita,
                        PorcentajeDescuento = Entity.PorcentajeDescuento
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool validar_existe_IdCobro_tipo(string IdCobro_tipo)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var lst = from q in Context.cxc_cobro_tipo
                              where q.IdCobro_tipo == IdCobro_tipo
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
        //== true ? "S" : "N"
        public bool guardarDB(cxc_cobro_tipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_cobro_tipo Entity = new cxc_cobro_tipo
                    {
                        IdCobro_tipo = info.IdCobro_tipo,
                        ESRetenFTE = info.ESRetenFTE_bool == true ? "S" : "N",
                        ESRetenIVA = info.ESRetenIVA_bool == true ? "S" : "N",
                        IdMotivo_tipo_cobro = info.IdMotivo_tipo_cobro,
                        tc_abreviatura = info.tc_abreviatura,
                        tc_descripcion = info.tc_descripcion,
                        tc_Tomar_Cta_Cble_De = info.tc_Tomar_Cta_Cble_De,
                        Estado = info.Estado = "A",
                        PorcentajeRet = info.PorcentajeRet,
                        EsTarjetaCredito = info.EsTarjetaCredito,
                        SeDeposita = info.SeDeposita,
                        PorcentajeDescuento = info.PorcentajeDescuento,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.cxc_cobro_tipo.Add(Entity);
                    foreach (var item in info.Lst_tipo_param_det)
                    {
                        cxc_cobro_tipo_Param_conta_x_sucursal det = new cxc_cobro_tipo_Param_conta_x_sucursal
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCtaCble = item.IdCtaCble,
                            IdCobro_tipo = info.IdCobro_tipo                            
                        };
                        Context.cxc_cobro_tipo_Param_conta_x_sucursal.Add(det);
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
        public bool modificarDB(cxc_cobro_tipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_cobro_tipo Entity = Context.cxc_cobro_tipo.FirstOrDefault(q => q.IdCobro_tipo == info.IdCobro_tipo);
                    if (Entity == null) return false;

                    Entity.ESRetenFTE = info.ESRetenFTE_bool == true ? "S" : "N";
                    Entity.ESRetenIVA = info.ESRetenIVA_bool == true ? "S" : "N";
                    Entity.tc_abreviatura = info.tc_abreviatura;
                    Entity.tc_descripcion = info.tc_descripcion;
                    Entity.tc_Tomar_Cta_Cble_De = info.tc_Tomar_Cta_Cble_De;
                    Entity.PorcentajeRet = info.PorcentajeRet;
                    Entity.EsTarjetaCredito = info.EsTarjetaCredito;
                    Entity.SeDeposita = info.SeDeposita;
                    Entity.PorcentajeDescuento = info.PorcentajeDescuento;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod;

                    var lst = Context.cxc_cobro_tipo_Param_conta_x_sucursal.Where(q => q.IdCobro_tipo == info.IdCobro_tipo && q.IdEmpresa == info.IdEmpresa).ToList();
                    foreach (var item in lst)
                    {
                        Context.cxc_cobro_tipo_Param_conta_x_sucursal.Remove(item);
                    }
                    foreach (var item in info.Lst_tipo_param_det)
                    {
                        cxc_cobro_tipo_Param_conta_x_sucursal det = new cxc_cobro_tipo_Param_conta_x_sucursal
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCtaCble = item.IdCtaCble,
                            IdCobro_tipo = info.IdCobro_tipo
                        };
                        Context.cxc_cobro_tipo_Param_conta_x_sucursal.Add(det);
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
        public bool anularDB(cxc_cobro_tipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_cobro_tipo Entity = Context.cxc_cobro_tipo.FirstOrDefault(q => q.IdCobro_tipo == info.IdCobro_tipo);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";

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

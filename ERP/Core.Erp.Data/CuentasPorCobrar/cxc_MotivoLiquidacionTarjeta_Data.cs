using Core.Erp.Data.General;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorCobrar
{
   public class cxc_MotivoLiquidacionTarjeta_Data
    {
        public List<cxc_MotivoLiquidacionTarjeta_Info> GetList(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<cxc_MotivoLiquidacionTarjeta_Info> Lista;
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    if(mostrar_anulados)
                    {
                        Lista = Context.cxc_MotivoLiquidacionTarjeta.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new cxc_MotivoLiquidacionTarjeta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMotivo = q.IdMotivo,
                            Descripcion = q.Descripcion,
                            ESRetenFTE = q.ESRetenFTE,
                            ESRetenIVA = q.ESRetenIVA,
                            Estado = q.Estado,
                            Porcentaje = q.Porcentaje
                        }).ToList();
                    }
                    else
                    {
                        Lista = Context.cxc_MotivoLiquidacionTarjeta.Where(
                         q => q.IdEmpresa == IdEmpresa
                         && q.Estado == true).Select(q => new cxc_MotivoLiquidacionTarjeta_Info
                         {
                             IdEmpresa = q.IdEmpresa,
                             IdMotivo = q.IdMotivo,
                             Descripcion = q.Descripcion,
                             ESRetenFTE = q.ESRetenFTE,
                             ESRetenIVA = q.ESRetenIVA,
                             Estado = q.Estado

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

        public cxc_MotivoLiquidacionTarjeta_Info GEtInfo(int IdEmpresa, decimal IdMotivo)
        {
            try
            {
                cxc_MotivoLiquidacionTarjeta_Info info = new cxc_MotivoLiquidacionTarjeta_Info();
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_MotivoLiquidacionTarjeta Entity = Context.cxc_MotivoLiquidacionTarjeta.Where(q => q.IdEmpresa == IdEmpresa && q.IdMotivo == IdMotivo).FirstOrDefault();
                    if (Entity == null) return null;

                    info = new cxc_MotivoLiquidacionTarjeta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMotivo = Entity.IdMotivo,
                        Descripcion = Entity.Descripcion,
                        ESRetenFTE = Entity.ESRetenFTE,
                        ESRetenIVA = Entity.ESRetenIVA,
                        Estado = Entity.Estado,
                        Porcentaje = Entity.Porcentaje,


                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal GetId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var lst = from q in Context.cxc_MotivoLiquidacionTarjeta
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdMotivo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(cxc_MotivoLiquidacionTarjeta_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    Context.cxc_MotivoLiquidacionTarjeta.Add(new cxc_MotivoLiquidacionTarjeta
                    {

                        IdEmpresa = info.IdEmpresa,
                        IdMotivo = info.IdMotivo=GetId(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        ESRetenFTE = info.ESRetenFTE,
                        ESRetenIVA = info.ESRetenIVA,
                        Porcentaje = info.Porcentaje,
                        Estado = true,

                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });
                    if(info.Lst_det.Count()>0)
                    {
                        foreach (var item in info.Lst_det)
                        {
                            Context.cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.Add(new cxc_MotivoLiquidacionTarjeta_x_tb_sucursal
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMotivo = info.IdMotivo,
                                IdCtaCble = item.IdCtaCble,
                                IdSucursal = item.IdSucursal,
                                Secuencia = item.Secuencia
                            });
                        }
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_MotivoLiquidacionTarjeta_Data", Metodo = "GuardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        } 

        public bool ModificarDB(cxc_MotivoLiquidacionTarjeta_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_MotivoLiquidacionTarjeta Entity = Context.cxc_MotivoLiquidacionTarjeta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMotivo == info.IdMotivo).FirstOrDefault();
                    if (Entity == null) return false;
                    Entity.Descripcion = info.Descripcion;
                    Entity.ESRetenFTE = info.ESRetenFTE;
                    Entity.ESRetenIVA = info.ESRetenIVA;
                    Entity.Porcentaje = info.Porcentaje;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var lst_det = Context.cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMotivo == info.IdMotivo).ToList();
                    Context.cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.RemoveRange(lst_det);
                    if (info.Lst_det.Count() > 0)
                    {
                        foreach (var item in info.Lst_det)
                        {
                            Context.cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.Add(new cxc_MotivoLiquidacionTarjeta_x_tb_sucursal
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMotivo = info.IdMotivo,
                                IdCtaCble = item.IdCtaCble,
                                IdSucursal = item.IdSucursal,
                                Secuencia = item.Secuencia
                            });
                        }
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_MotivoLiquidacionTarjeta_Data", Metodo = "ModificarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }
        public bool AnularDB(cxc_MotivoLiquidacionTarjeta_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_MotivoLiquidacionTarjeta Entity = Context.cxc_MotivoLiquidacionTarjeta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMotivo == info.IdMotivo).FirstOrDefault();
                    if (Entity == null) return false;
                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;
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

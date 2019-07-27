using Core.Erp.Data.General;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
    public class Af_Retiro_Activo_Data
    {
        public List<Af_Retiro_Activo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<Af_Retiro_Activo_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.Af_Retiro_Activo
                                 where q.IdEmpresa == IdEmpresa
                                 select new Af_Retiro_Activo_Info
                                 {
                                     IdEmpresa  = q.IdEmpresa,
                                     Cod_Ret_Activo = q.Cod_Ret_Activo,
                                     Concepto_Retiro = q.Concepto_Retiro,
                                     Estado = q.Estado,
                                     IdActivoFijo = q.IdActivoFijo,
                                     IdCbteCble = q.IdCbteCble,
                                     IdEmpresa_ct = q.IdEmpresa_ct,
                                     IdTipoCbte = q.IdTipoCbte,
                                     IdRetiroActivo = q.IdRetiroActivo,
                                     Fecha_Retiro = q.Fecha_Retiro,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();

                    else
                        Lista = (from q in Context.Af_Retiro_Activo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new Af_Retiro_Activo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Cod_Ret_Activo = q.Cod_Ret_Activo,
                                     Concepto_Retiro = q.Concepto_Retiro,
                                     Estado = q.Estado,
                                     IdActivoFijo = q.IdActivoFijo,
                                     IdCbteCble = q.IdCbteCble,
                                     IdEmpresa_ct = q.IdEmpresa_ct,
                                     IdTipoCbte = q.IdTipoCbte,
                                     IdRetiroActivo = q.IdRetiroActivo,
                                     Fecha_Retiro = q.Fecha_Retiro,

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

        public Af_Retiro_Activo_Info get_info(int IdEmpresa, decimal IdRetiroActivo)
        {
            try
            {
                Af_Retiro_Activo_Info info = new Af_Retiro_Activo_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Retiro_Activo Entity = Context.Af_Retiro_Activo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdRetiroActivo == IdRetiroActivo);
                    if (Entity == null) return null;
                    info = new Af_Retiro_Activo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Cod_Ret_Activo = Entity.Cod_Ret_Activo,
                        Concepto_Retiro = Entity.Concepto_Retiro,
                        Estado = Entity.Estado,
                        Fecha_Retiro = Entity.Fecha_Retiro.Date,
                        IdActivoFijo = Entity.IdActivoFijo,
                        IdCbteCble = Entity.IdCbteCble,
                        IdEmpresa_ct = Entity.IdEmpresa_ct,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdRetiroActivo = Entity.IdRetiroActivo,
                        NumComprobante = Entity.NumComprobante,
                        ValorActivo = Entity.ValorActivo,
                        Valor_Depre_Acu = Entity.Valor_Depre_Acu,
                        Valor_Neto = Entity.Valor_Neto,
                        Valor_Tot_Bajas = Entity.Valor_Tot_Bajas,
                        Valor_Tot_Mejora = Entity.Valor_Tot_Mejora

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    var lst = from q in Context.Af_Retiro_Activo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.LongCount() > 0)
                        ID = lst.Max(q => q.IdRetiroActivo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Retiro_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Retiro_Activo Entity = new Af_Retiro_Activo
                    {

                        IdEmpresa = info.IdEmpresa,
                        Cod_Ret_Activo = info.Cod_Ret_Activo,
                        Concepto_Retiro = info.Concepto_Retiro,
                        Estado = info.Estado="A",
                        Fecha_Retiro = info.Fecha_Retiro.Date,
                        IdActivoFijo = info.IdActivoFijo,
                        IdCbteCble = info.IdCbteCble,
                        IdEmpresa_ct = info.IdEmpresa_ct,
                        IdTipoCbte = info.IdTipoCbte,
                        IdRetiroActivo = info.IdRetiroActivo=get_id(info.IdEmpresa),
                        NumComprobante = info.NumComprobante,
                        ValorActivo = info.ValorActivo,
                        Valor_Depre_Acu = info.Valor_Depre_Acu,
                        Valor_Neto = info.Valor_Neto,
                        Valor_Tot_Bajas = info.Valor_Tot_Bajas,
                        Valor_Tot_Mejora = info.Valor_Tot_Mejora,
                        
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.Af_Retiro_Activo.Add(Entity);

                    Af_Activo_fijo Entity_A = Context.Af_Activo_fijo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdActivoFijo == info.IdActivoFijo).FirstOrDefault();
                    if (Entity_A == null) return false;
                    Entity_A.Estado_Proceso = "TIP_ESTADO_AF_RETIRO";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Retiro_Activo_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(Af_Retiro_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Retiro_Activo Entity = Context.Af_Retiro_Activo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRetiroActivo == info.IdRetiroActivo);
                    if (Entity == null) return false;


                    Entity.Concepto_Retiro = info.Concepto_Retiro;
                    Entity.Fecha_Retiro = info.Fecha_Retiro.Date;
                    Entity.IdCbteCble = info.IdCbteCble;
                    Entity.IdEmpresa_ct = info.IdEmpresa_ct;
                    Entity.IdTipoCbte = info.IdTipoCbte;
                    Entity.NumComprobante = info.NumComprobante;
                    Entity.ValorActivo = info.ValorActivo;
                    Entity.Valor_Depre_Acu = info.Valor_Depre_Acu;
                    Entity.Valor_Neto = info.Valor_Neto;
                    Entity.Valor_Tot_Bajas = info.Valor_Tot_Bajas;
                    Entity.Valor_Tot_Mejora = info.Valor_Tot_Mejora;
                    Entity.Cod_Ret_Activo = info.Cod_Ret_Activo;
                        
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Retiro_Activo_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(Af_Retiro_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Retiro_Activo Entity = Context.Af_Retiro_Activo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRetiroActivo == info.IdRetiroActivo);
                    if (Entity == null) return false;


                    Entity.Estado = info.Estado="I";

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
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

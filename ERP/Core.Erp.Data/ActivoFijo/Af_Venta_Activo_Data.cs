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
    public class Af_Venta_Activo_Data
    {
        public List<Af_Venta_Activo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<Af_Venta_Activo_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.Af_Venta_Activo
                                 where q.IdEmpresa == IdEmpresa
                                 select new Af_Venta_Activo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Cod_VtaActivo = q.Cod_VtaActivo,
                                     Concepto_Vta = q.Concepto_Vta,
                                     Estado = q.Estado,
                                     IdVtaActivo = q.IdVtaActivo,
                                     Fecha_Venta = q.Fecha_Venta,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.Af_Venta_Activo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new Af_Venta_Activo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Cod_VtaActivo = q.Cod_VtaActivo,
                                     Concepto_Vta = q.Concepto_Vta,
                                     Estado = q.Estado,
                                     IdVtaActivo = q.IdVtaActivo,
                                     Fecha_Venta = q.Fecha_Venta,

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

        public Af_Venta_Activo_Info get_info(int IdEmpresa, decimal IdVtaActivo)
        {
            try
            {
                Af_Venta_Activo_Info info = new Af_Venta_Activo_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Venta_Activo Entity = Context.Af_Venta_Activo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdVtaActivo == IdVtaActivo);
                    if (Entity == null) return null;
                    info = new Af_Venta_Activo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Cod_VtaActivo = Entity.Cod_VtaActivo,
                        Concepto_Vta = Entity.Concepto_Vta,
                        Estado = Entity.Estado,
                        Fecha_Venta = Entity.Fecha_Venta,
                        IdActivoFijo = Entity.IdActivoFijo,
                        IdCbteCble = Entity.IdCbteCble,
                        IdEmpresa_ct = Entity.IdEmpresa_ct,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdVtaActivo = Entity.IdVtaActivo,
                        NumComprobante = Entity.NumComprobante,
                        ValorActivo = Entity.ValorActivo,
                        Valor_Depre_Acu = Entity.Valor_Depre_Acu,
                        Valor_Neto = Entity.Valor_Neto,
                        Valor_Perdi_Gana = Entity.Valor_Perdi_Gana,
                        Valor_Tot_Bajas = Entity.Valor_Tot_Bajas,
                        Valor_Tot_Mejora = Entity.Valor_Tot_Mejora,
                        Valor_Venta = Entity.Valor_Venta
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
                    var lst = from q in Context.Af_Venta_Activo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdVtaActivo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Venta_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Venta_Activo Entity = new Af_Venta_Activo
                    {
                        IdEmpresa = info.IdEmpresa,
                        Cod_VtaActivo = info.Cod_VtaActivo,
                        Concepto_Vta = info.Concepto_Vta,
                        Estado = info.Estado="A",
                        Fecha_Venta = info.Fecha_Venta.Date,
                        IdActivoFijo = info.IdActivoFijo,
                        IdCbteCble = info.IdCbteCble,
                        IdEmpresa_ct = info.IdEmpresa_ct,
                        IdTipoCbte = info.IdTipoCbte,
                        IdVtaActivo = info.IdVtaActivo=get_id(info.IdEmpresa),
                        NumComprobante = info.NumComprobante,
                        ValorActivo = info.ValorActivo,
                        Valor_Depre_Acu = info.Valor_Depre_Acu,
                        Valor_Neto = info.Valor_Neto,
                        Valor_Perdi_Gana = info.Valor_Perdi_Gana,
                        Valor_Tot_Bajas = info.Valor_Tot_Bajas,
                        Valor_Tot_Mejora = info.Valor_Tot_Mejora,
                        Valor_Venta = info.Valor_Venta,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.Af_Venta_Activo.Add(Entity);

                    Af_Activo_fijo Entity_A = Context.Af_Activo_fijo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdActivoFijo == info.IdActivoFijo).FirstOrDefault();
                    if (Entity_A == null) return false;
                    Entity_A.Estado_Proceso = "TIP_ESTADO_AF_VENTA";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Venta_Activo_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(Af_Venta_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Venta_Activo Entity = Context.Af_Venta_Activo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdVtaActivo == info.IdVtaActivo);
                    if (Entity == null) return false;


                    Entity.Cod_VtaActivo = info.Cod_VtaActivo;
                    Entity.Concepto_Vta = info.Concepto_Vta;
                    Entity.Fecha_Venta = info.Fecha_Venta.Date;
                    Entity.IdCbteCble = info.IdCbteCble;
                    Entity.IdEmpresa_ct = info.IdEmpresa_ct;
                    Entity.IdTipoCbte = info.IdTipoCbte;
                    Entity.NumComprobante = info.NumComprobante;
                    Entity.ValorActivo = info.ValorActivo;
                    Entity.Valor_Depre_Acu = info.Valor_Depre_Acu;
                    Entity.Valor_Neto = info.Valor_Neto;
                    Entity.Valor_Perdi_Gana = info.Valor_Perdi_Gana;
                    Entity.Valor_Tot_Bajas = info.Valor_Tot_Bajas;
                    Entity.Valor_Tot_Mejora = info.Valor_Tot_Mejora;
                    Entity.Valor_Venta = info.Valor_Venta;


                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Venta_Activo_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(Af_Venta_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Venta_Activo Entity = Context.Af_Venta_Activo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdVtaActivo == info.IdVtaActivo);
                    if (Entity == null) return false;


                    Entity.Estado = info.Estado="I";


                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
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

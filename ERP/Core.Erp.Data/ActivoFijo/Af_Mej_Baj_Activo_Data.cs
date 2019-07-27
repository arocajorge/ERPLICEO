using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Helps;
using Core.Erp.Data.General;
using Core.Erp.Info.General;

namespace Core.Erp.Data.ActivoFijo
{
   public class Af_Mej_Baj_Activo_Data
    {
        public List<Af_Mej_Baj_Activo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<Af_Mej_Baj_Activo_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.Af_Mej_Baj_Activo
                                 where q.IdEmpresa == IdEmpresa
                                 select new Af_Mej_Baj_Activo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Cod_Mej_Baj_Activo= q.Cod_Mej_Baj_Activo,
                                     Estado = q.Estado,
                                     Id_Mejora_Baja_Activo = q.Id_Mejora_Baja_Activo,
                                     Id_Tipo = q.Id_Tipo,
                                     Fecha_MejBaj = q.Fecha_MejBaj,
                                     Motivo = q.Motivo,

                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    else
                    Lista = (from q in Context.Af_Mej_Baj_Activo
                             where q.IdEmpresa == IdEmpresa
                             && q.Estado == "A"
                             select new Af_Mej_Baj_Activo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 Cod_Mej_Baj_Activo = q.Cod_Mej_Baj_Activo,
                                 Estado = q.Estado,
                                 Id_Mejora_Baja_Activo = q.Id_Mejora_Baja_Activo,
                                 Id_Tipo = q.Id_Tipo,
                                 Fecha_MejBaj = q.Fecha_MejBaj,
                                 Motivo = q.Motivo,

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

        public Af_Mej_Baj_Activo_Info get_info(int IdEmpresa, decimal Id_Mejora_Baja_Activo)
        {
            try
            {
                Af_Mej_Baj_Activo_Info info = new Af_Mej_Baj_Activo_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Mej_Baj_Activo Entity = Context.Af_Mej_Baj_Activo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.Id_Mejora_Baja_Activo == Id_Mejora_Baja_Activo);
                    if (Entity == null) return null;
                    info = new Af_Mej_Baj_Activo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdActivoFijo = Entity.IdActivoFijo,
                        IdCbteCble = Entity.IdCbteCble,
                        IdEmpresa_ct = Entity.IdEmpresa_ct,
                        IdTipoCbte = Entity.IdTipoCbte,
                        Id_Mejora_Baja_Activo = Entity.Id_Mejora_Baja_Activo,
                        Id_Tipo = Entity.Id_Tipo,
                        Cod_Mej_Baj_Activo = Entity.Cod_Mej_Baj_Activo,
                        Compr_Mej_Baj = Entity.Compr_Mej_Baj,
                        DescripcionTecnica = Entity.DescripcionTecnica,
                        Estado = Entity.Estado,
                        Fecha_MejBaj = Entity.Fecha_MejBaj.Date,
                        ValorActivo = Entity.ValorActivo,
                        Valor_Depre_Acu = Entity.Valor_Depre_Acu,
                        Valor_Mej_Baj_Activo = Math.Abs(Entity.Valor_Mej_Baj_Activo),
                        Valor_Neto = Entity.Valor_Neto,
                        Valor_Tot_Bajas = Entity.Valor_Tot_Bajas,
                        Valor_Tot_Mejora = Entity.Valor_Tot_Mejora,
                        Motivo = Entity.Motivo
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal get_id(int IdEmpresa, string Id_Tipo)
        {
            try
            {
                decimal ID = 1;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    var lst = from q in Context.Af_Mej_Baj_Activo
                              where q.IdEmpresa == IdEmpresa
                              && q.Id_Tipo == Id_Tipo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q=>q.Id_Mejora_Baja_Activo)+1;

                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(Af_Mej_Baj_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Mej_Baj_Activo Entity = new Af_Mej_Baj_Activo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpresa_ct = info.IdEmpresa_ct,
                        IdTipoCbte = info.IdTipoCbte,
                        IdCbteCble = info.IdCbteCble,
                        Cod_Mej_Baj_Activo = info.Cod_Mej_Baj_Activo,
                        Compr_Mej_Baj = info.Compr_Mej_Baj,
                        DescripcionTecnica = info.DescripcionTecnica,
                        Estado = info.Estado = "A",
                        IdActivoFijo = info.IdActivoFijo,
                        Id_Mejora_Baja_Activo = info.Id_Mejora_Baja_Activo = get_id(info.IdEmpresa, info.Id_Tipo),
                        Id_Tipo = info.Id_Tipo,
                        ValorActivo = info.ValorActivo,
                        Valor_Depre_Acu = info.Valor_Depre_Acu,
                        Valor_Mej_Baj_Activo = info.Id_Tipo == cl_enumeradores.eTipoMejBajAF.Mejo_Acti.ToString() ? Math.Abs(info.Valor_Mej_Baj_Activo) : Math.Abs(info.Valor_Mej_Baj_Activo) * -1,
                        Valor_Neto = info.Valor_Neto,
                        Valor_Tot_Bajas = info.Valor_Tot_Bajas,
                        Valor_Tot_Mejora = info.Valor_Tot_Mejora,
                        Motivo = info.Motivo,
                        Fecha_MejBaj = info.Fecha_MejBaj.Date,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.Af_Mej_Baj_Activo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Mej_Baj_Activo_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarDB(Af_Mej_Baj_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Mej_Baj_Activo Entity = Context.Af_Mej_Baj_Activo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.Id_Mejora_Baja_Activo == info.Id_Mejora_Baja_Activo);
                    if (Entity == null) return false;

                    Entity.Cod_Mej_Baj_Activo = info.Cod_Mej_Baj_Activo;
                    Entity.Compr_Mej_Baj = info.Compr_Mej_Baj;
                    Entity.DescripcionTecnica = info.DescripcionTecnica;
                    Entity.ValorActivo = info.ValorActivo;
                    Entity.Valor_Depre_Acu = info.Valor_Depre_Acu;
                    Entity.Valor_Mej_Baj_Activo = info.Id_Tipo == cl_enumeradores.eTipoMejBajAF.Mejo_Acti.ToString() ? Math.Abs(info.Valor_Mej_Baj_Activo) : Math.Abs(info.Valor_Mej_Baj_Activo) * -1;
                    Entity.Valor_Neto = info.Valor_Neto;
                    Entity.Valor_Tot_Bajas = info.Valor_Tot_Bajas;
                    Entity.Valor_Tot_Mejora = info.Valor_Tot_Mejora;
                    Entity.Motivo = info.Motivo;
                    Entity.Fecha_MejBaj = info.Fecha_MejBaj.Date;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "Af_Mej_Baj_Activo_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(Af_Mej_Baj_Activo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Mej_Baj_Activo Entity = Context.Af_Mej_Baj_Activo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.Id_Mejora_Baja_Activo == info.Id_Mejora_Baja_Activo);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado = "I";

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

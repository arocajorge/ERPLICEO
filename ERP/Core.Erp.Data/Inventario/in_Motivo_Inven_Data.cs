using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_Motivo_Inven_Data
    {
        public List<in_Motivo_Inven_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<in_Motivo_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_Motivo_Inven
                                 where q.IdEmpresa == IdEmpresa
                                 select new in_Motivo_Inven_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMotivo_Inv = q.IdMotivo_Inv,
                                     Cod_Motivo_Inv = q.Cod_Motivo_Inv,
                                     Desc_mov_inv = q.Desc_mov_inv,
                                     estado = q.estado,
                                     Genera_Movi_Inven = q.Genera_Movi_Inven,
                                     Tipo_Ing_Egr = q.Tipo_Ing_Egr,
                                     IdCtaCble = q.IdCtaCble,
                                     EstadoBool = q.estado == "A" ? true : false
                                     
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_Motivo_Inven
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado == "A"
                                 select new in_Motivo_Inven_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMotivo_Inv = q.IdMotivo_Inv,
                                     Cod_Motivo_Inv = q.Cod_Motivo_Inv,
                                     Desc_mov_inv = q.Desc_mov_inv,
                                     estado = q.estado,
                                     Genera_Movi_Inven = q.Genera_Movi_Inven,
                                     Tipo_Ing_Egr = q.Tipo_Ing_Egr,
                                     IdCtaCble = q.IdCtaCble,
                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<in_Motivo_Inven_Info> get_list(int IdEmpresa,string tipo, bool mostrar_anulados)
        {
            try
            {
                List<in_Motivo_Inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_Motivo_Inven
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Tipo_Ing_Egr == tipo
                                 select new in_Motivo_Inven_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMotivo_Inv = q.IdMotivo_Inv,
                                     Cod_Motivo_Inv = q.Cod_Motivo_Inv,
                                     Desc_mov_inv = q.Desc_mov_inv,
                                     estado = q.estado,
                                     Genera_Movi_Inven = q.Genera_Movi_Inven,
                                     Tipo_Ing_Egr = q.Tipo_Ing_Egr,

                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_Motivo_Inven
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado == "A"
                                 && q.Tipo_Ing_Egr == tipo
                                 select new in_Motivo_Inven_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdMotivo_Inv = q.IdMotivo_Inv,
                                     Cod_Motivo_Inv = q.Cod_Motivo_Inv,
                                     Desc_mov_inv = q.Desc_mov_inv,
                                     estado = q.estado,
                                     Genera_Movi_Inven = q.Genera_Movi_Inven,
                                     Tipo_Ing_Egr = q.Tipo_Ing_Egr,

                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public in_Motivo_Inven_Info get_info(int IdEmpresa, int IdMotivo_Inv)
        {
            try
            {
                in_Motivo_Inven_Info info = new in_Motivo_Inven_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Motivo_Inven Entity = Context.in_Motivo_Inven.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdMotivo_Inv == IdMotivo_Inv);
                    if (Entity == null) return null;

                    info = new in_Motivo_Inven_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMotivo_Inv = Entity.IdMotivo_Inv,
                        Cod_Motivo_Inv = Entity.Cod_Motivo_Inv,
                        Desc_mov_inv = Entity.Desc_mov_inv,
                        estado = Entity.estado,
                        Genera_Movi_Inven_bool = Entity.Genera_Movi_Inven == "S" ? true : false,
                        Tipo_Ing_Egr = Entity.Tipo_Ing_Egr,
                        IdCtaCble = Entity.IdCtaCble
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_Motivo_Inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Motivo_Inven Entity = new in_Motivo_Inven
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMotivo_Inv = info.IdMotivo_Inv=get_id(info.IdEmpresa),
                        Cod_Motivo_Inv = info.Cod_Motivo_Inv,
                        Desc_mov_inv = info.Desc_mov_inv,
                        estado = info.estado="A",
                        Genera_Movi_Inven = info.Genera_Movi_Inven_bool == true ? "S" : "N",
                        Tipo_Ing_Egr = info.Tipo_Ing_Egr,
                        IdCtaCble = info.IdCtaCble,
                        Fecha_Transac = DateTime.Now

                    };
                    Context.in_Motivo_Inven.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(in_Motivo_Inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Motivo_Inven Entity = Context.in_Motivo_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMotivo_Inv == info.IdMotivo_Inv);
                    if (Entity == null) return false;

                    Entity.Cod_Motivo_Inv = info.Cod_Motivo_Inv;
                    Entity.Desc_mov_inv = info.Desc_mov_inv;
                    Entity.Tipo_Ing_Egr = info.Tipo_Ing_Egr;
                    Entity.Genera_Movi_Inven = info.Genera_Movi_Inven_bool == true ? "S" : "N";
                    Entity.IdCtaCble = info.IdCtaCble;

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

        public bool anularDB(in_Motivo_Inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Motivo_Inven Entity = Context.in_Motivo_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMotivo_Inv == info.IdMotivo_Inv);
                    if (Entity == null) return false;

                    Entity.estado = Entity.estado="I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.FechaHoraAnul = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id(int IdEmpresa)
        {

            try
            {
                int ID = 1;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_Motivo_Inven
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdMotivo_Inv) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id_movimiento(int IdEmpresa, string signo)
        {
            try
            {
                int ID = 0;

                using (Entities_inventario db = new Entities_inventario())
                {
                    var motivo = db.in_Motivo_Inven.Where(q => q.IdEmpresa == IdEmpresa && q.Tipo_Ing_Egr == (signo == "+" ? "ING" : "EGR") && q.Genera_Movi_Inven == "S").FirstOrDefault();
                    if (motivo == null)
                        return ID;
                    else
                        ID = motivo.IdMotivo_Inv;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

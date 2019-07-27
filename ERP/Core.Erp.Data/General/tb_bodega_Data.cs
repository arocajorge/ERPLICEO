using Core.Erp.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_bodega_Data
    {
        #region Funciones

        public List<tb_bodega_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                List<tb_bodega_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.tb_bodega
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 select new tb_bodega_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     cod_bodega = q.cod_bodega,
                                     bo_Descripcion = q.bo_Descripcion,
                                     cod_punto_emision = q.cod_punto_emision,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.tb_bodega
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.Estado == "A"
                                 select new tb_bodega_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     cod_bodega = q.cod_bodega,
                                     bo_Descripcion = q.bo_Descripcion,
                                     cod_punto_emision = q.cod_punto_emision,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }
                Lista.ForEach(q => q.IdString = q.IdSucursal.ToString("000") + q.IdBodega.ToString("000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_bodega_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<tb_bodega_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.tb_bodega
                                 where q.IdEmpresa == IdEmpresa
                                 select new tb_bodega_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     cod_bodega = q.cod_bodega,
                                     bo_Descripcion = q.bo_Descripcion,
                                     cod_punto_emision = q.cod_punto_emision,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.tb_bodega
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new tb_bodega_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     cod_bodega = q.cod_bodega,
                                     bo_Descripcion = q.bo_Descripcion,
                                     cod_punto_emision = q.cod_punto_emision,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }
                Lista.ForEach(q => q.IdString = q.IdSucursal.ToString("000") + q.IdBodega.ToString("000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_bodega_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                tb_bodega_Info info = new tb_bodega_Info();

                using (Entities_general Context = new Entities_general())
                {
                    tb_bodega Entity = Context.tb_bodega.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega);
                    if (Entity == null) return null;
                    info = new tb_bodega_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        cod_bodega = Entity.cod_bodega,
                        bo_Descripcion = Entity.bo_Descripcion,
                        cod_punto_emision = Entity.cod_punto_emision,
                        bo_EsBodega_bool = Entity.bo_EsBodega == "S" ? true : false,
                        bo_manejaFacturacion_bool = Entity.bo_manejaFacturacion == "S" ? true : false,
                        Estado = Entity.Estado,
                        IdCtaCtble_Costo = Entity.IdCtaCtble_Costo,
                        IdCtaCtble_Inve = Entity.IdCtaCtble_Inve
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id(int IdEmpresa, int IdSucursal)
        {
            try
            {
                int ID = 1;

                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_bodega
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == IdSucursal
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdBodega) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_bodega_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_bodega Entity = new tb_bodega
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega = get_id(info.IdEmpresa, info.IdSucursal),
                        cod_bodega = info.cod_bodega,
                        cod_punto_emision = info.cod_punto_emision,
                        bo_Descripcion = info.bo_Descripcion,
                        bo_EsBodega = info.bo_EsBodega_bool == true ? "S" : "N",
                        bo_manejaFacturacion = info.bo_manejaFacturacion_bool == true ? "S" : "N",
                        IdCtaCtble_Costo = info.IdCtaCtble_Costo,
                        IdCtaCtble_Inve = info.IdCtaCtble_Inve,
                        IdEstadoAproba_x_Ing_Egr_Inven = info.IdEstadoAproba_x_Ing_Egr_Inven,
                        Estado = info.Estado = "A",
                    };
                    Context.tb_bodega.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_bodega_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_bodega Entity = Context.tb_bodega.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega);
                    if (Entity == null) return false;
                    Entity.cod_bodega = info.cod_bodega;
                    Entity.cod_punto_emision = info.cod_punto_emision;
                    Entity.bo_Descripcion = info.bo_Descripcion;
                    Entity.bo_EsBodega = info.bo_EsBodega_bool == true ? "S" : "N";
                    Entity.bo_manejaFacturacion = info.bo_manejaFacturacion_bool == true ? "S" : "N";
                    Entity.IdCtaCtble_Costo = info.IdCtaCtble_Costo;
                    Entity.IdCtaCtble_Inve = info.IdCtaCtble_Inve;
                    Entity.IdEstadoAproba_x_Ing_Egr_Inven = info.IdEstadoAproba_x_Ing_Egr_Inven;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_bodega_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_bodega Entity = Context.tb_bodega.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = "I";
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion



        public List<tb_bodega_Info> get_list_demanda(int IdEmpresa, int skip, int take, string filter, bool MostrarAnulados, int IdSucursal)
        {
            try
            {
                List<tb_bodega_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if (MostrarAnulados)
                        Lista = (from q in Context.tb_bodega
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && (q.IdBodega.ToString() + " " + q.bo_Descripcion).Contains(filter)
                                 select new tb_bodega_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     bo_Descripcion = q.bo_Descripcion,
                                     Estado = q.Estado,
                                     EstadoBool = q.Estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdBodega)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                    else
                        Lista = (from q in Context.tb_bodega
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.Estado == "A"
                                 && (q.IdBodega.ToString() + " " + q.bo_Descripcion).Contains(filter)
                                 select new tb_bodega_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     bo_Descripcion = q.bo_Descripcion,
                                     Estado = q.Estado,
                                     EstadoBool = q.Estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdBodega)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tb_bodega_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool estado, int IdSucursal)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<tb_bodega_Info> Lista = new List<tb_bodega_Info>();
            Lista = get_list_demanda(IdEmpresa, skip, take, args.Filter, estado, IdSucursal);
            return Lista;
        }


        public tb_bodega_Info get_info_demanda(int IdEmpresa, int IdBodega, int IdSucursal)
        {
            try
            {
                tb_bodega_Info info = new tb_bodega_Info();

                using (Entities_general Context = new Entities_general())
                {
                    tb_bodega Entity = Context.tb_bodega.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdBodega == IdBodega);
                    if (Entity == null) return null;

                    info = new tb_bodega_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        bo_Descripcion = Entity.bo_Descripcion,
                        Estado = Entity.Estado,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_bodega_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, int IdSucursal)
        {
            return get_info(IdEmpresa, args.Value == null ? 0 : (int)args.Value, IdSucursal);
        }

    }
}

using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_movi_inven_tipo_x_tb_bodega_Data
    {
        public List<in_movi_inven_tipo_x_tb_bodega_Info> get_list(int IdEmpresa, int IdMovi_inven_tipo)
        {
            try
            {
                List<in_movi_inven_tipo_x_tb_bodega_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.in_movi_inven_tipo_x_tb_bodega
                             where q.IdEmpresa == IdEmpresa && q.IdMovi_inven_tipo == IdMovi_inven_tipo
                             select new in_movi_inven_tipo_x_tb_bodega_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                 IdSucucursal = q.IdSucucursal,
                                 IdBodega = q.IdBodega,
                                 IdCtaCble = q.IdCtaCble
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

       public bool guardarDB(List<in_movi_inven_tipo_x_tb_bodega_Info> Lista)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    foreach (var item in Lista)
                    {
                        in_movi_inven_tipo_x_tb_bodega Entity = new Data.in_movi_inven_tipo_x_tb_bodega
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdMovi_inven_tipo = item.IdMovi_inven_tipo,
                            IdSucucursal = item.IdSucucursal,
                            IdBodega = item.IdBodega,
                            IdCtaCble = item.IdCtaCble
                        };
                        Context.in_movi_inven_tipo_x_tb_bodega.Add(Entity);
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

        public bool eliminarDB(int IdEmpresa, int IdMovi_inven_tipo)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Context.Database.ExecuteSqlCommand("DELETE in_movi_inven_tipo_x_tb_bodega WHERE IdEmpresa = "+IdEmpresa+ " AND IdMovi_inven_tipo = "+ IdMovi_inven_tipo);
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

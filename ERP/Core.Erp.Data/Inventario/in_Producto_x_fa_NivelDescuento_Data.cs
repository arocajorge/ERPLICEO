using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_Producto_x_fa_NivelDescuento_Data
    {
        public List<in_Producto_x_fa_NivelDescuento_Info> get_list_nuevo(int IdEmpresa)
        {            
            List<in_Producto_x_fa_NivelDescuento_Info> lista = null;
            try
            {
                int secuencia = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {                    
                    lista = (from q in Context.fa_NivelDescuento
                             where q.IdEmpresa == IdEmpresa && q.Estado == true
                             select new in_Producto_x_fa_NivelDescuento_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 Descripcion = q.Descripcion,
                                 IdNivel = q.IdNivel,
                                 Porcentaje = q.Porcentaje,
                             }).ToList();

                }
                lista.ForEach(v => v.Secuencia = secuencia++);
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<in_Producto_x_fa_NivelDescuento_Info> get_list(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                List<in_Producto_x_fa_NivelDescuento_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.in_Producto_x_fa_NivelDescuento
                             where q.IdEmpresa == IdEmpresa
                             && q.IdProducto == IdProducto
                             select new in_Producto_x_fa_NivelDescuento_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdProducto = q.IdProducto,
                                 Secuencia = q.Secuencia,
                                 IdNivel = q.IdNivel,
                                 Porcentaje = q.Porcentaje

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_Producto_x_fa_NivelDescuento_Info GetInfo(int IdEmpresa, decimal IdProducto, int IdNivel)
        {
            try
            {
                in_Producto_x_fa_NivelDescuento_Info info;
                using (Entities_inventario db = new Entities_inventario())
                {
                    info = db.in_Producto_x_fa_NivelDescuento.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdProducto == IdProducto && q.IdNivel == IdNivel).Select(q=> new in_Producto_x_fa_NivelDescuento_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdNivel = q.IdNivel,
                        IdProducto = q.IdProducto,
                        Porcentaje = q.Porcentaje
                    }).FirstOrDefault();
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(List<in_Producto_x_fa_NivelDescuento_Info> Lista)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var secuencia = 1;
                    foreach (var item in Lista)
                    {
                        in_Producto_x_fa_NivelDescuento Entity = new in_Producto_x_fa_NivelDescuento
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdProducto = item.IdProducto,
                            IdNivel = item.IdNivel,
                            Secuencia = secuencia++,
                            Porcentaje = item.Porcentaje,
                        };
                        Context.in_Producto_x_fa_NivelDescuento.Add(Entity);
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

        public bool eliminarDB(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Context.Database.ExecuteSqlCommand("DELETE in_Producto_x_fa_NivelDescuento WHERE IdEmpresa = " + IdEmpresa + " AND IdProducto = " + IdProducto);
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

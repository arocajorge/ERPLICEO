using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Reportes.Inventario
{
    public class INV_013_Data
    {
        public List<INV_013_Info> get_list(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                List<INV_013_Info> Lista;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = Context.in_Producto.Where(q=>q.IdEmpresa == IdEmpresa && q.IdProducto == IdProducto).Select(q=> new INV_013_Info{
                        IdEmpresa = q.IdEmpresa,
                        IdProducto = q.IdProducto,
                        pr_codigo = q.pr_codigo,
                        pr_descripcion = q.pr_descripcion,
                        nom_presentacion = q.in_presentacion.nom_presentacion,
                        pr_codigo_barra = q.pr_codigo_barra
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
   public class INV_003_Data
    {
        public List<INV_003_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, string IdCategoria, int IdLinea, int IdGrupo, int IdSubgrupo, DateTime fecha_corte, bool mostrar_stock_0, int IdMarca)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 999999 : IdSucursal;
                int IdBodega_ini = IdBodega;
                int IdBodega_fin = IdBodega == 0 ? 999999 : IdBodega;
                decimal IdProducto_ini = IdProducto;
                decimal IdProducto_fin = IdProducto == 0 ? 999999999 : IdProducto;
                int IdMarca_ini = IdMarca;
                int IdMarca_fin = IdMarca == 0 ? 9999999 : IdMarca;

                List<INV_003_Info> Lista=null;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    
                    Lista = (from q in Context.SPINV_003(IdEmpresa, IdSucursal_ini, IdSucursal_fin, IdBodega_ini, IdBodega_fin, IdProducto_ini, IdProducto_fin, IdCategoria, IdLinea, IdGrupo, IdSubgrupo, fecha_corte, mostrar_stock_0,IdMarca_ini,IdMarca_fin)
                             select new INV_003_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdProducto = q.IdProducto,
                                 Stock = q.Stock,
                                 Costo_promedio = q.Costo_promedio,
                                 Costo_total = q.Costo_total,
                                 Su_Descripcion = q.Su_Descripcion,
                                 bo_Descripcion = q.bo_Descripcion,
                                 pr_codigo = q.pr_codigo,
                                 pr_descripcion = q.pr_descripcion,
                                 lote_num_lote = q.lote_num_lote,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 IdCategoria = q.IdCategoria,
                                 ca_Categoria = q.ca_Categoria,
                                 IdLinea = q.IdLinea,
                                 nom_linea = q.nom_linea,
                                 IdGrupo = q.IdGrupo,
                                 nom_grupo = q.nom_grupo,
                                 IdSubgrupo = q.IdSubgrupo,
                                 nom_subgrupo = q.nom_subgrupo,
                                 IdPresentacion = q.IdPresentacion,
                                 nom_presentacion = q.nom_presentacion,
                                 IdMarca = q.IdMarca,
                                 NomMarca = q.NomMarca
                                 
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

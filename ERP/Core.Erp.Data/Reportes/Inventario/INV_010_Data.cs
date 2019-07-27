using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
    public class INV_010_Data
    {
        public List<INV_010_Info> get_list(int IdEmpresa, decimal IdProducto, string IdCategoria, int IdLinea, int IdGrupo, int IdSubGrupo, int IdMarca, string IdUsuario, DateTime fechaIni, DateTime fechaFin, bool mostrarSinMovimiento)

        {
            try
            {
                decimal IdProductoIni = IdProducto;
                decimal IdProductoFin = IdProducto == 0 ? 9999 : IdProducto;


                int IdMarcaIni = IdMarca;
                int IdMarcaFin = IdMarca == 0 ? 9999 : IdMarca;

                List<INV_010_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPINV_010(IdEmpresa, IdProductoIni, IdProductoFin, IdCategoria, IdLinea, IdGrupo, IdSubGrupo, IdUsuario, IdMarcaIni, IdMarcaFin, fechaIni, fechaFin, mostrarSinMovimiento)
                             select new INV_010_Info
                             { 
                                 IdEmpresa = q.IdEmpresa,
                                 IdProducto = q.IdProducto,
                                 IdCategoria = q.IdCategoria,
                                 IdLinea = q.IdLinea,
                                 IdGrupo = q.IdGrupo,
                                 IdSubGrupo = q.IdSubGrupo,
                                 IdMarca = q.IdMarca,
                                 IdUsuario = q.IdUsuario,
                                 ca_Categoria = q.ca_Categoria,
                                 nom_linea = q.nom_linea,
                                 nom_grupo = q.nom_grupo,
                                 nom_subgrupo = q.nom_subgrupo,
                                 NomMarca = q.NomMarca,
                                 nom_presentacion = q.nom_presentacion,
                                 IdPresentacion = q.IdPresentacion,
                                 StockActual = q.StockActual,
                                 Total = q.Total,
                                 Enero = q.Enero,
                                 Febrero = q.Febrero,
                                 Marzo = q.Marzo,
                                 Abril = q.Abril,
                                 Mayo = q.Mayo,
                                 Junio = q.Junio,
                                 Julio = q.Julio,
                                 Agosto = q.Agosto,
                                 Septiembre = q.Septiembre,
                                 Octubre = q.Octubre,
                                 Noviembre = q.Noviembre,
                                 Diciembre = q.Diciembre,
                                 IdAnio = q.IdAnio,
                                 pr_descripcion = q.pr_descripcion
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

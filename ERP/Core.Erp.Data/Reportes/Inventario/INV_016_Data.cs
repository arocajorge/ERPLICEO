using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
   public class INV_016_Data
    {
        public List<INV_016_Info> GetList(int IdEmpresa, int IdSucursal, int IdCategoria, int IdLinea,  int IdGrupo,  int IdSubGrupo, DateTime fecha_ini, DateTime fecha_fin, bool noMostrarSinVenta, string IdUsuario)
        {
            try
            {
                
                List<INV_016_Info> Lista;


                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;


                int IdCategoriaIni = IdCategoria;
                int IdCategoriaFin = IdCategoria == 0 ? 9999 : IdCategoria;


                int IdLineaIni = IdLinea;
                int IdLineaFin = IdLinea == 0 ? 9999 : IdLinea;


                int IdGrupoIni = IdGrupo;
                int IdGrupoFin = IdGrupo == 0 ? 9999 : IdGrupo;


                int IdSubGrupoIni = IdSubGrupo;
                int IdSubGrupoFin = IdSubGrupo == 0 ? 9999 : IdSubGrupo;

                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPINV_016(IdEmpresa, IdSucursalIni, IdSucursalFin, IdCategoriaIni, IdCategoriaFin, IdLineaIni, IdLineaFin, IdGrupoIni, IdGrupoFin,IdSubGrupoIni, IdSubGrupoFin, fecha_ini, fecha_fin, noMostrarSinVenta, IdUsuario).Select(q => new INV_016_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdUsuario = q.IdUsuario,
                        CantidadIngresada = q.CantidadIngresada,
                        CantidadVendida = q.CantidadVendida,
                        ca_Categoria = q.ca_Categoria,
                        CostoPromedio = q.CostoPromedio,
                        CostoTotal = q.CostoTotal,
                        IdProducto = q.IdProducto,
                        IdSucursal = q.IdSucursal,
                        nom_grupo = q.nom_grupo,
                        nom_linea = q.nom_linea,
                        nom_subgrupo = q.nom_subgrupo,
                        PrecioVenta = q.PrecioVenta,
                        pr_descripcion = q.pr_descripcion,
                        SaldoInicial = q.SaldoInicial,
                        Stock = q.Stock,
                        Su_Descripcion = q.Su_Descripcion
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

using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
    public class INV_005_Data
    {
        public List<INV_005_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, DateTime fecha_ini, DateTime fecha_fin, string IdUsuario, bool no_mostrar_valores_en_0, bool mostrar_detallado, decimal IdProductoPadre)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdBodega_ini = IdBodega;
                int IdBodega_fin = IdBodega == 0 ? 9999 : IdBodega;

                decimal IdProducto_ini = IdProducto;
                decimal IdProducto_fin = IdProducto == 0 ? 999999 : IdProducto;

                decimal IdProductoPadre_ini = IdProductoPadre;
                decimal IdProductoPadre_fin = IdProductoPadre == 0 ? 99999 : IdProductoPadre;

                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                List<INV_005_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPINV_005(IdEmpresa, IdSucursal_ini, IdSucursal_fin, IdBodega_ini, IdBodega_fin, IdProducto_ini
                             , IdProducto_fin, fecha_ini, fecha_fin, IdUsuario, no_mostrar_valores_en_0, mostrar_detallado, IdProductoPadre_ini, IdProductoPadre_fin)
                             select new INV_005_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                 IdNumMovi = q.IdNumMovi,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 IdProductoPadre = q.IdProductoPadre,
                                 Saldo_ini_cant = q.Saldo_ini_cant,
                                 Cost_prom_ini = q.Cost_prom_ini,
                                 Saldo_ini_cost = q.Saldo_ini_cost,
                                 cant_ing = q.cant_ing,
                                 cost_ing = q.cost_ing,
                                 total_ing = q.total_ing,
                                 cant_egr = q.cant_egr,
                                 cost_egr = q.cost_egr,
                                 total_egr = q.total_egr,
                                 Saldo_cant = q.Saldo_cant,
                                 Saldo_cost_prom = q.Saldo_cost_prom,
                                 Saldo_cost = q.Saldo_cost,
                                 Saldo_fin_cant = q.Saldo_fin_cant,
                                 Cost_prom_fin = q.Cost_prom_fin,
                                 Saldo_fin_cost = q.Saldo_fin_cost,
                                 IdUsuario = q.IdUsuario,
                                 dm_observacion = q.dm_observacion,
                                 cm_fecha = q.cm_fecha,
                                 tipo_movi = q.tipo_movi,
                                 cod_bodega = q.cod_bodega,
                                 nom_bodega = q.nom_bodega,
                                 cod_sucursal = q.cod_sucursal,
                                 nom_sucursal = q.nom_sucursal,
                                 IdEmpresa_oc = q.IdEmpresa_oc,
                                 IdSucursal_oc = q.IdSucursal_oc,
                                 IdOrdenCompra = q.IdOrdenCompra,
                                 num_factura = q.num_factura,
                                 nom_proveedor = q.nom_proveedor,
                                 pr_codigo = q.pr_codigo,
                                 pr_descripcion = q.pr_descripcion,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 nom_unidad_consumo = q.nom_unidad_consumo,
                                 cod_unidad_consumo = q.cod_unidad_consumo
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

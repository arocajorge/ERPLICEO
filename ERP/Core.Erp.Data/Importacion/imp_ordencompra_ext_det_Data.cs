using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Importacion;
namespace Core.Erp.Data.Importacion
{
   public class imp_ordencompra_ext_det_Data
    {
        public List<imp_ordencompra_ext_det_Info> get_list(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                List<imp_ordencompra_ext_det_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.vwimp_orden_compra_ext_det
                             where q.IdEmpresa==IdEmpresa
                             && q.IdOrdenCompra_ext==IdOrdenCompra_ext
                             select new imp_ordencompra_ext_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 od_cantidad = q.od_cantidad,
                                 od_costo = q.od_costo,
                                 od_por_descuento = q.od_por_descuento,
                                 od_descuento = q.od_descuento,
                                 od_costo_final = q.od_costo_final,
                                 od_subtotal = q.od_subtotal,
                                 od_cantidad_recepcion = q.od_cantidad_recepcion,
                                 od_costo_convertido = q.od_costo_convertido,
                                 od_total_fob = q.od_total_fob,
                                 od_factor_costo = q.od_factor_costo,
                                 od_costo_bodega = q.od_costo_bodega,
                                 od_costo_total = q.od_costo_total,
                                 pr_descripcion=q.pr_descripcion,
                                 lote_fecha_vcto=q.lote_fecha_vcto,
                                 lote_num_lote=q.lote_num_lote
                                 

                             }).ToList();
                }
                foreach (var item in Lista)
                {
                    string fecha = "";
                    if (item.lote_num_lote == null)
                        item.lote_num_lote = "";
                    if (item.lote_fecha_vcto != null)
                        fecha=item.lote_fecha_vcto.ToString().Substring(0,10);
                    item.pr_descripcion = item.pr_descripcion + " " + (item.lote_num_lote) +" "+fecha;
                }
                return Lista;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<imp_ordencompra_ext_det_Info> get_list_recepcion(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                List<imp_ordencompra_ext_det_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.vwimp_orden_compra_ext_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdOrdenCompra_ext == IdOrdenCompra_ext
                             select new imp_ordencompra_ext_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 od_cantidad = q.od_cantidad,
                                 od_costo = q.od_costo,
                                 od_por_descuento = q.od_por_descuento,
                                 od_descuento = q.od_descuento,
                                 od_costo_final = q.od_costo_final,
                                 od_subtotal = q.od_subtotal,
                                 od_cantidad_recepcion = q.od_cantidad,
                                 od_costo_convertido = q.od_costo_convertido,
                                 od_total_fob = q.od_total_fob,
                                 od_factor_costo = q.od_factor_costo,
                                 od_costo_bodega = q.od_costo_bodega,
                                 od_costo_total = q.od_costo_total,
                                 pr_descripcion = q.pr_descripcion,


                             }).ToList();
                }
                foreach (var item in Lista)
                {
                    string fecha = "";
                    if (item.lote_num_lote == null)
                        item.lote_num_lote = "";
                    if (item.lote_fecha_vcto != null)
                        fecha = item.lote_fecha_vcto.ToString().Substring(0, 10);
                    item.pr_descripcion = item.pr_descripcion + " " + (item.lote_num_lote) + " " + fecha;
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminar(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                using (Entities_importacion context=new Entities_importacion())
                {
                    string sql = "delete imp_orden_compra_ext_det where IdEmpresa='" + IdEmpresa+ "' and IdOrdenCompra_ext='"+ IdOrdenCompra_ext + "'";
                    context.Database.ExecuteSqlCommand(sql);
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

using Core.Erp.Info.Reportes.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Importacion
{
   
        public class IMP_002_Data
        {
            public List<IMP_002_Info> get_list(int IdEmpresa, int IdOrdenCompra_ext)
            {
                try
                {
                    List<IMP_002_Info> Lista;
                    using (Entities_reportes Context = new Entities_reportes())
                    {
                        Lista = (from q in Context.VWIMP_002
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdOrdenCompra_ext == IdOrdenCompra_ext
                                 select new IMP_002_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                     IdProveedor = q.IdProveedor,
                                     oe_fecha = q.oe_fecha,
                                     oe_fecha_embarque_est = q.oe_fecha_embarque_est,
                                     oe_fecha_llegada_est = q.oe_fecha_llegada_est,
                                     oe_observacion = q.oe_observacion,
                                     pr_codigo = q.pr_codigo,
                                     pr_descripcion = q.pr_descripcion,
                                     od_cantidad = q.od_cantidad,
                                     od_costo = q.od_costo,
                                     od_costo_final = q.od_costo_final,
                                     od_descuento = q.od_descuento,
                                     od_por_descuento = q.od_por_descuento,
                                     od_subtotal = q.od_subtotal,
                                     FormaPago = q.FormaPago,
                                     Paisembarque = q.Paisembarque,
                                     PaisOrigen=q.PaisOrigen,
                                     ViaEmbarque = q.ViaEmbarque,
                                     pe_nombreCompleto = q.pe_nombreCompleto,
                                     od_total_fob = q.od_total_fob,
                                     IdUnidadMedida=q.IdUnidadMedida,
                                     Descripcion_Ciudad=q.Descripcion_Ciudad,
                                     od_cantidad_recepcion=q.od_cantidad_recepcion,
                                     oe_fecha_llegada=q.oe_fecha_llegada,
                                     oe_fecha_embarque=q.oe_fecha_embarque

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

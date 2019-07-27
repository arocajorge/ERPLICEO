using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Compras.Base;

namespace Core.Erp.Data.Compras
{
   public class com_ordencompra_local_det_Data
    {
        public List<com_ordencompra_local_det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdOrdenCompra)
        {
            try
            {
                List<com_ordencompra_local_det_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    Lista = (from q in Context.vwcom_ordencompra_local_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdOrdenCompra == IdOrdenCompra
                             select new com_ordencompra_local_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra = q.IdOrdenCompra,
                                 IdSucursal = q.IdSucursal,
                                 IdProducto = q.IdProducto,
                                 IdCod_Impuesto = q.IdCod_Impuesto,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 do_Cantidad = q.do_Cantidad,
                                 do_descuento = q.do_descuento,
                                 do_iva = q.do_iva,
                                 do_observacion = q.do_observacion,
                                 do_porc_des = q.do_porc_des,
                                 do_precioCompra = q.do_precioCompra,
                                 do_precioFinal = q.do_precioFinal,
                                 do_subtotal = q.do_subtotal,
                                 do_total = q.do_total,
                                 Por_Iva = q.Por_Iva,
                                 Secuencia = q.Secuencia,
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

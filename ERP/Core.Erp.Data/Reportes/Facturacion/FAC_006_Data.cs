using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_006_Data
    {
        public List<FAC_006_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdProforma, bool formato_hoja_membretada, bool mostrar_imagen)
        {
            try
            {
                List<FAC_006_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWFAC_006
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdProforma == IdProforma
                             select new FAC_006_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                                 Su_Descripcion = q.Su_Descripcion,
                                 Su_Direccion = q.Su_Direccion,
                                 Su_Telefonos = q.Su_Telefonos,
                                 IdCliente = q.IdCliente,
                                 nombre_cliente = q.nombre_cliente,
                                 ced_ruc_cliente = q.ced_ruc_cliente,
                                 direccion_cliente = q.direccion_cliente,
                                 celular_cliente = q.celular_cliente,
                                 telefono_cliente = q.telefono_cliente,
                                 IdProforma = q.IdProforma,
                                 Secuencia = q.Secuencia,
                                 nom_TerminoPago = q.nom_TerminoPago,
                                 pf_plazo = q.pf_plazo,
                                 pf_codigo = q.pf_codigo,
                                 pf_fecha = q.pf_fecha,
                                 estado = q.estado,
                                 Codigo = q.Codigo,
                                 Ve_Vendedor = q.Ve_Vendedor,
                                 pd_cantidad = q.pd_cantidad,
                                 pd_precio = q.pd_precio,
                                 pd_descuento_uni = q.pd_descuento_uni,
                                 pd_por_descuento_uni = q.pd_por_descuento_uni,
                                 pd_precio_final = q.pd_precio_final,
                                 pd_subtotal = q.pd_subtotal,
                                 pd_iva = q.pd_iva,
                                 pd_por_iva = q.pd_por_iva,
                                 pd_total = q.pd_total,
                                 pr_dias_entrega = q.pr_dias_entrega,
                                 IdProducto = q.IdProducto,
                                 pr_observacion = q.pr_observacion,
                                 pf_observacion = q.pf_observacion,
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

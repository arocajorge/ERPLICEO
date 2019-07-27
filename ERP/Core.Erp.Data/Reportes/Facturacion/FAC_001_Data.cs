using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_001_Data
    {
         public List<FAC_001_Info> get_list(int IdEmpresa, int IdSucursal, int IdVendedor, decimal IdCliente, decimal IdProducto, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_anulados)
        {
            try
            {

                fecha_fin = Convert.ToDateTime(fecha_fin.Date.ToShortDateString());
                fecha_ini = Convert.ToDateTime(fecha_ini.Date.ToShortDateString());

                int IdSucursal_Ini = IdSucursal;
                int IdSucursal_Fin = IdSucursal == 0 ? 99999 : IdSucursal;

                int IdVendedor_Ini = IdVendedor;
                int IdVendedor_Fin = IdVendedor == 0 ? 99999 : IdVendedor;

                decimal IdCliente_Ini = IdCliente;
                decimal IdCliente_Fin = IdCliente == 0 ? 999999 : IdCliente;
                
                decimal IdProducto_Ini = IdProducto;
                decimal IdProducto_Fin = IdProducto == 0 ? 99999 : IdProducto;
                
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                List<FAC_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWFAC_001
                             where q.IdEmpresa ==IdEmpresa
                             && IdSucursal_Ini <= q.IdSucursal && q.IdSucursal <= IdSucursal_Fin
                             && IdVendedor_Ini <= q.IdVendedor && q.IdVendedor <= IdVendedor_Fin
                             && IdCliente_Ini <= q.IdCliente && q.IdCliente <= IdCliente_Fin
                             && IdProducto_Ini <= q.IdProducto && q.IdProducto <= IdProducto_Fin
                             && fecha_ini <= q.vt_fecha && q.vt_fecha <= fecha_fin
                             select new FAC_001_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 IdProducto_padre = q.IdProducto_padre,
                                 vt_NumFactura = q.vt_NumFactura,
                                 IdCliente = q.IdCliente,
                                 IdVendedor = q.IdVendedor,
                                 Ve_Vendedor = q.Ve_Vendedor,
                                 NombreCliente = q.NombreCliente,
                                 pr_descripcion = q.pr_descripcion,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 lote_num_lote = q.lote_num_lote,
                                 vt_cantidad = q.vt_cantidad,
                                 vt_DesctTotal = q.vt_DesctTotal,
                                 vt_iva = q.vt_iva,
                                 vt_PorDescUnitario = q.vt_PorDescUnitario,
                                 vt_Precio = q.vt_Precio,
                                 vt_PrecioFinal = q.vt_PrecioFinal,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_total = q.vt_total, 
                                 Estado = q.Estado,
                                 Su_Descripcion = q.Su_Descripcion,
                                 vt_fecha = q.vt_fecha
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

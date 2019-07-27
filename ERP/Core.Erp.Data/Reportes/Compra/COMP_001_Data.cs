using Core.Erp.Info.Reportes.Compra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes
{
    public class COMP_001_Data
    {
        public List<COMP_001_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdOrdenCompra)
        {
            try
            {
                List<COMP_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWCOMP_001.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdSucursal == IdSucursal
                    && q.IdOrdenCompra == IdOrdenCompra
                    ).Select(q => new COMP_001_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        DescuentoTotal = q.DescuentoTotal,
                        DireccionProveedor = q.DireccionProveedor,
                        do_Cantidad = q.do_Cantidad,
                        do_descuento = q.do_descuento,
                        do_iva = q.do_iva,
                        do_porc_des = q.do_porc_des,
                        do_precioCompra = q.do_precioCompra,
                        do_precioFinal = q.do_precioFinal,
                        do_subtotal = q.do_subtotal,
                        do_total = q.do_total,
                        Estado = q.Estado,
                        IdOrdenCompra = q.IdOrdenCompra,  
                        SecuenciaTipo = q.SecuenciaTipo,
                        Tipo = q.Tipo,                      
                        IdProducto = q.IdProducto,
                        IdProveedor = q.IdProveedor,
                        IdSucursal = q.IdSucursal,
                        NombreComprador = q.NombreComprador,
                        NombreProducto = q.NombreProducto,
                        NombreProveedor = q.NombreProveedor,
                        NombreTerminoPago = q.NombreTerminoPago,
                        NomUnidadMedida = q.NomUnidadMedida,
                        oc_fecha = q.oc_fecha,
                        oc_observacion = q.oc_observacion,
                        oc_plazo = q.oc_plazo,
                        Por_Iva = q.Por_Iva,
                        RucProveedor = q.RucProveedor,
                        Secuencia = q.Secuencia,
                        Subtotal0 = q.Subtotal0,
                        SubtotalIVA = q.SubtotalIVA,
                        Su_Descripcion = q.Su_Descripcion,
                        TelefonosProveedor = q.TelefonosProveedor
                        
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

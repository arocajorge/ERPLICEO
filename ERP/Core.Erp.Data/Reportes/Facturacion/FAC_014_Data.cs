using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_014_Data
    {
        public List<FAC_014_Info> GetList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<FAC_014_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWFAC_014.Where(q=>q.IdEmpresa == IdEmpresa
                    && fecha_ini <= q.Fecha_Emi_Fact
                    && q.Fecha_Emi_Fact <= fecha_fin
                    ).Select(q => new FAC_014_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdComprobante = q.IdComprobante,
                        IdEstado_cbte = q.IdEstado_cbte,
                        IdTipoDocumento = q.IdTipoDocumento,
                        Cantidad = q.Cantidad,
                        Cedula_Ruc = q.Cedula_Ruc,
                        Evento = q.Evento,
                        Factura = q.Factura,
                        FechaAutorizacion = q.FechaAutorizacion,
                        Fecha_Emi_Fact = q.Fecha_Emi_Fact,
                        Fecha_transaccion = q.Fecha_transaccion,
                        Iva = q.Iva,
                        Nom_Contribuyente = q.Nom_Contribuyente,
                        Numero_Autorizacion = q.Numero_Autorizacion,
                        Subtotal = q.Subtotal,
                        Total = q.Total,
                        ValorUnitario = q.ValorUnitario,
                        Nombre_Evento = q.Nombre_Evento,
                        Observacion = q.Observacion
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

using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
   public class FAC_018_Data
    {
        public List<FAC_018_Info> GetList(int IdEmpresa, decimal IdCliente, int IdTipoNota, DateTime fecha_ini, DateTime fecha_fin, string CreDeb, bool mostrar_anulados)
        {
            try
            {
                
                decimal IdClienteIni = IdCliente;
                decimal IdClienteFin = IdCliente == 0 ? 99999999 : IdCliente;
                List<FAC_018_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    if(mostrar_anulados)
                    {
                            Lista = Context.VWFAC_018.Where(q => q.IdEmpresa == IdEmpresa
                            && IdClienteIni <= q.IdCliente
                            && q.IdCliente <= IdClienteFin
                            && q.IdTipoNota == IdTipoNota
                            && fecha_ini <= q.no_fecha
                            && q.no_fecha <= fecha_fin
                            && q.CreDeb == CreDeb
                            ).Select(q => new FAC_018_Info
                            {
                                IdEmpresa = q.IdEmpresa,
                                Estado = q.Estado,
                                IdBodega = q.IdBodega,
                                IdCliente = q.IdCliente,
                                IdNota = q.IdNota,
                                IdSucursal = q.IdSucursal,
                                IdTipoNota = q.IdTipoNota,
                                NomEstado = q.NomEstado,
                                No_Descripcion = q.No_Descripcion,
                                no_fecha = q.no_fecha,
                                NumDocumentoAplica = q.NumDocumentoAplica,
                                NumDocumentoReemplazo = q.NumDocumentoReemplazo,
                                NumNota = q.NumNota,
                                Orden = q.Orden,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                Subtotal0 = q.Subtotal0,
                                SubtotalIVA = q.SubtotalIVA,
                                Su_Descripcion = q.Su_Descripcion,
                                Total = q.Total,
                                ValorAplicado = q.ValorAplicado,
                                ValorIva = q.ValorIva,
                                CreDeb = q.CreDeb
                            }).ToList();
                     
                    }
                    else
                    {
                            Lista = Context.VWFAC_018.Where(q => q.IdEmpresa == IdEmpresa
                           && IdClienteIni <= q.IdCliente
                           && q.IdCliente <= IdClienteFin
                           && q.IdTipoNota == IdTipoNota
                           && fecha_ini <= q.no_fecha
                           && q.no_fecha <= fecha_fin
                           && q.Estado == "A"
                           ).Select(q => new FAC_018_Info
                           {
                               IdEmpresa = q.IdEmpresa,
                               Estado = q.Estado,
                               IdBodega = q.IdBodega,
                               IdCliente = q.IdCliente,
                               IdNota = q.IdNota,
                               IdSucursal = q.IdSucursal,
                               IdTipoNota = q.IdTipoNota,
                               NomEstado = q.NomEstado,
                               No_Descripcion = q.No_Descripcion,
                               no_fecha = q.no_fecha,
                               NumDocumentoAplica = q.NumDocumentoAplica,
                               NumDocumentoReemplazo = q.NumDocumentoReemplazo,
                               NumNota = q.NumNota,
                               Orden = q.Orden,
                               pe_cedulaRuc = q.pe_cedulaRuc,
                               pe_nombreCompleto = q.pe_nombreCompleto,
                               Subtotal0 = q.Subtotal0,
                               SubtotalIVA = q.SubtotalIVA,
                               Su_Descripcion = q.Su_Descripcion,
                               Total = q.Total,
                               ValorAplicado = q.ValorAplicado,
                               ValorIva = q.ValorIva,
                               CreDeb = q.CreDeb
                           }).ToList();
                    }                    
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

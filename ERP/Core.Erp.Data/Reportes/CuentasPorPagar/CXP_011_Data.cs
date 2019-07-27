using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_011_Data
    {
        public List<CXP_011_Info> GetList(int IdEmpresa, decimal IdSolicitud)
        {
            try
            {
                List<CXP_011_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWCXP_011.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdSolicitud == IdSolicitud).Select(q => new CXP_011_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdProveedor = q.IdProveedor,
                        IdSolicitud = q.IdSolicitud,
                        IdSucursal = q.IdSucursal,
                        Concepto = q.Concepto,
                        Estado = q.Estado,
                        Fecha = q.Fecha,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Solicitante = q.Solicitante,
                        Su_Descripcion = q.Su_Descripcion,
                        Valor = q.Valor,
                        Nombre = q.Nombre,
                        GiradoA = q.GiradoA
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

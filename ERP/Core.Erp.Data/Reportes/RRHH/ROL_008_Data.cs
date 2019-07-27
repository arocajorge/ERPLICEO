using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_008_Data
    {
        public List<ROL_008_Info> get_list(int IdEmpresa, decimal IdPrestamo)
        {
            try
            {
                List<ROL_008_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_008
                             where q.IdEmpresa == IdEmpresa
                             && q.IdPrestamo == IdPrestamo
                             select new ROL_008_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPrestamo = q.IdPrestamo,
                                 CedulaRuc = q.CedulaRuc,
                                 IdEmpleado = q.IdEmpleado,
                                 Fecha = q.Fecha,
                                 MontoSol = q.MontoSol,
                                 NumCuota = q.NumCuota,
                                 Observacion = q.Observacion,
                                 NumCuotas = q.NumCuotas,
                                 SaldoInicial = q.SaldoInicial,
                                 TotalCuota = q.TotalCuota,
                                 Saldo = q.Saldo,
                                 FechaPago = q.FechaPago,
                                 EstadoPago = q.EstadoPago,
                                 ObservacionCuota = q.ObservacionCuota,
                                 RubroDescripcion = q.RubroDescripcion,
                                 CodigoEmpleado = q.CodigoEmpleado,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 descuento_mensual = q.descuento_mensual,
                                 descuento_men_quin = q.descuento_men_quin,
                                 descuento_quincena = q.descuento_quincena,
                                 Fecha_PriPago = q.Fecha_PriPago,
                                 Fecha_Transac = q.Fecha_Transac,
                                 TotalCobrado = q.TotalCobrado
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

using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_PrestamoMasivo_Det_Data
    {
        public List<ro_PrestamoMasivo_Det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCarga)
        {
            try
            {
                List<ro_PrestamoMasivo_Det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_PrestamoMasivo_Det
                             where q.IdEmpresa == IdEmpresa
                                    && q.IdSucursal == IdSucursal
                                   && q.IdCarga == IdCarga
                             select new ro_PrestamoMasivo_Det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCarga = q.IdCarga,
                                 IdSucursal = q.IdSucursal,
                                 Secuencia = q.Secuencia,
                                 IdPrestamo = q.IdPrestamo,
                                 IdEmpleado = q.IdEmpleado,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdRubro = q.IdRubro,
                                 ru_descripcion = q.ru_descripcion,
                                 Monto = q.Monto,
                                 NumCuotas = q.NumCuotas
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

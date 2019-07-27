using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
  public  class BAN_012_Data
    {
        public List<BAN_012_Info> GetList(int IdEmpresa,  int IdSucursal, DateTime fechaIni, DateTime fechaFin, bool mostrarSaldo0, string IdUsuario)
        {
            try
            {
                List<BAN_012_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPBAN_012(IdEmpresa, IdSucursal, fechaIni, fechaFin, mostrarSaldo0, IdUsuario).Select(q => new BAN_012_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdUsuario = q.IdUsuario,
                        ba_descripcion = q.ba_descripcion,
                        Egresos = q.Egresos,
                        IdTipoFlujo = q.IdTipoFlujo,
                        Ingresos = q.Ingresos,
                        nom_tipo_flujo = q.nom_tipo_flujo,
                        SaldoFinal = q.SaldoFinal,
                        SaldoFinalBanco = q.SaldoFinalBanco,
                        SaldoInicial = q.SaldoInicial,
                        IdBanco = q.IdBanco
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

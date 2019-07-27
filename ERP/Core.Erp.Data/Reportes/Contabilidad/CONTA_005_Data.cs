using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Contabilidad
{
    public class CONTA_005_Data
    {
        public List<CONTA_005_Info> GetList(int IdEmpresa, int IdPunto_cargo_grupo, string IdUsuario, DateTime fechaIni, DateTime fechaFin, bool mostrarSaldo0)
        {
            try
            {
                List<CONTA_005_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPCONTA_005(IdEmpresa, IdPunto_cargo_grupo, IdUsuario, fechaIni, fechaFin, mostrarSaldo0).Select(q => new CONTA_005_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdUsuario = q.IdUsuario,
                        Creditos = q.Creditos,
                        Debitos = q.Debitos,
                        IdPunto_cargo = q.IdPunto_cargo,
                        IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                        nom_punto_cargo = q.nom_punto_cargo,
                        nom_punto_cargo_grupo = q.nom_punto_cargo_grupo,
                        SaldoAnterior = q.SaldoAnterior,
                        SaldoFinal = q.SaldoFinal
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

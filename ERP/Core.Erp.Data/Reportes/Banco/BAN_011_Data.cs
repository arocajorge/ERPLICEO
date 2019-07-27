using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_011_Data
    {
        public List<BAN_011_Info> GetList(int IdEmpresa, string IdUsuario, int IdSucursal, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                List<BAN_011_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPBAN_011(IdEmpresa, IdUsuario, IdSucursal, fechaIni, fechaFin).Select(q => new BAN_011_Info
                    {
                        IdUsuario =q.IdUsuario,
                        Descripcion = q.Descripcion,
                        Egreso =q.Egreso,
                        IdBanco = q.IdBanco,
                        IdCtaCble = q.IdCtaCble,
                        Ingreso = q.Ingreso,
                        SaldoAnterior = q.SaldoAnterior,
                        SaldoFinal = q.SaldoFinal,
                        Su_Descripcion = q.Su_Descripcion 
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

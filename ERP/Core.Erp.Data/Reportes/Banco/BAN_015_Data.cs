using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_015_Data
    {
        public List<BAN_015_Info> GetList(string IdUsuario, string Tipo, DateTime FechaIni, DateTime FechaFin, bool MostrarSaldo0)
        {
            try
            {
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                List<BAN_015_Info> Lista = new List<BAN_015_Info>();

                using (Entities_reportes db = new Entities_reportes())
                {
                    var lst = db.SPBAN_015(IdUsuario, FechaIni, FechaFin, Tipo, MostrarSaldo0);
                    foreach (var item in lst)
                    {
                        Lista.Add(new BAN_015_Info
                        {
                            IdUsuario = item.IdUsuario,
                            Reporte = item.Reporte,
                            Secuencia = item.Secuencia,
                            Tipo = item.Tipo,
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            SaldoE1 = item.SaldoE1,
                            SaldoE2 = item.SaldoE2,
                            SaldoE3 = item.SaldoE3,
                            SaldoE4 = item.SaldoE4,
                            Total = item.Total
                        });
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

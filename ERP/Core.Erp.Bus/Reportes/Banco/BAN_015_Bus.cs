using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_015_Bus
    {
        BAN_015_Data odata = new BAN_015_Data();

        public List<BAN_015_Info> GetList(string IdUsuario, string Tipo, DateTime FechaIni, DateTime FechaFin, bool MostrarSaldo0)
        {
            try
            {
                return odata.GetList(IdUsuario, Tipo, FechaIni, FechaFin, MostrarSaldo0);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

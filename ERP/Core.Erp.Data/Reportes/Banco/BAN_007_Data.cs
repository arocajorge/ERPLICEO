using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_007_Data
    {
        public List<BAN_007_Info> get_list(int IdEmpresa, int IdBanco, decimal IdPersona, DateTime fecha_ini, DateTime fecha_fin, string Estado)
        {
            try
            {

                int IdBanco_ini = IdBanco;
                int IdBanco_fin = IdBanco == 0 ? 9999 : IdBanco;
                decimal IdPersona_ini = IdPersona;
                decimal IdPersona_fin = IdPersona == 0 ? 99999 : IdPersona;
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;
                List<BAN_007_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())

                    Lista = (from q in Context.VWBAN_007
                             where q.IdEmpresa == IdEmpresa
                             && IdBanco_ini <= q.IdBanco && q.IdBanco <= IdBanco_fin
                             && IdPersona_ini <= q.IdPersona_Girado_a && q.IdPersona_Girado_a <= IdPersona_fin
                             && q.cb_Fecha >= fecha_ini
                             && q.cb_Fecha <= fecha_fin
                             && q.IdCatalogo.Contains(Estado)
                             select new BAN_007_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipocbte = q.IdTipocbte,
                                 IdCbteCble = q.IdCbteCble,
                                 cb_Cheque = q.cb_Cheque,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_FechaCheque = q.cb_FechaCheque,
                                 cb_Observacion = q.cb_Observacion,
                                 cb_Valor = q.cb_Valor,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 ca_descripcion = q.ca_descripcion,
                                 ba_descripcion = q.ba_descripcion,
                                 Estado = q.Estado,
                                 IdBanco = q.IdBanco,
                                 IdCatalogo = q.IdCatalogo,
                                 IdPersona_Girado_a = q.IdPersona_Girado_a,
                                 IdRow = q.IdRow,
                                 Nombre = q.Nombre
                             }).ToList();
            
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

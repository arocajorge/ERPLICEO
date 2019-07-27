using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_008_Data
    {
        public List<BAN_008_Info> GetList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin , int IdBanco)
        {
            try
            {
                List<BAN_008_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPBAN_008(IdEmpresa, fecha_ini, fecha_fin, IdBanco).Select(q => new BAN_008_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdBanco = q.IdBanco,
                        IdCbteCble = q.IdCbteCble,
                        IdSucursal = q.IdSucursal,
                        IdTipoCbte = q.IdTipoCbte,
                        ba_descripcion = q.ba_descripcion,
                        cb_Cheque = q.cb_Cheque,
                        cb_Fecha = q.cb_Fecha,
                        cb_giradoA = q.cb_giradoA,
                        cb_Observacion = q.cb_Observacion,
                        Estado = q.Estado,
                        Orden = q.Orden,
                        Referencia = q.Referencia,
                        SaldoInicial = q.SaldoInicial,
                        secuencia = q.secuencia,
                        Su_Descripcion = q.Su_Descripcion,
                        Tipo = q.Tipo,
                        Valor = q.Valor,
                        ValorAbsoluto = q.ValorAbsoluto,
                        tc_TipoCbte = q.tc_TipoCbte,
                        Flujo = q.Flujo,
                        MotivoNota = q.MotivoNota,
                        TipoAgrupacion = q.TipoAgrupacion,
                        OrdenRegistros = q.OrdenRegistros
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

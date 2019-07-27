using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
  public  class BAN_010_Data
    {
        public List<BAN_010_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                List<BAN_010_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWBAN_010.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new BAN_010_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdArchivo = q.IdArchivo,
                        ba_descripcion = q.ba_descripcion,
                        cb_Fecha = q.cb_Fecha,
                        CodigoLegalBanco = q.CodigoLegalBanco,
                        Contabilizado = q.Contabilizado,
                        Estado = q.Estado,
                        Fecha_proceso = q.Fecha_proceso,
                        IdBanco_acreditacion = q.IdBanco_acreditacion,
                        IdEmpresa_OP = q.IdEmpresa_OP,
                        IdEntidad = q.IdEntidad,
                        IdOrdenPago = q.IdOrdenPago,
                        IdPersona = q.IdPersona,
                        IdTipoCta_acreditacion_cat = q.IdTipoCta_acreditacion_cat,
                        IdTipoDocumento = q.IdTipoDocumento,
                        IdTipo_Persona = q.IdTipo_Persona,
                        num_cta_acreditacion = q.num_cta_acreditacion,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        pr_correo = q.pr_correo,
                        pr_direccion = q.pr_direccion,
                        Referencia = q.Referencia,
                        Secuencia = q.Secuencia,
                        Secuencial_reg_x_proceso = q.Secuencial_reg_x_proceso,
                        Secuencia_OP = q.Secuencia_OP,
                        Valor = q.Valor,
                        Fecha= q.Fecha,
                        NombreProceso= q.NombreProceso,
                        NomCuenta = q.NomCuenta,
                        Observacion = q.Observacion

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

using Core.Erp.Info.Banco;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
   public class ba_Archivo_Transferencia_Det_Data
    {
        public List<ba_Archivo_Transferencia_Det_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                List<ba_Archivo_Transferencia_Det_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = Context.vwba_Archivo_Transferencia_Det.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new ba_Archivo_Transferencia_Det_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdArchivo = q.IdArchivo,
                        Estado = q.Estado,
                        IdEmpresa_OP = q.IdEmpresa_OP,
                        IdOrdenPago = q.IdOrdenPago,
                        Secuencia = q.Secuencia,
                        Secuencial_reg_x_proceso = q.Secuencial_reg_x_proceso,
                        Secuencia_OP = q.Secuencia_OP,
                        Valor = q.Valor,
                        Nom_Beneficiario = q.pe_nombreCompleto,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        pr_correo = q.pr_correo,
                        pr_direccion = q.pr_direccion,
                        num_cta_acreditacion = q.num_cta_acreditacion,
                        IdBanco_acreditacion=q.IdBanco_acreditacion,
                        IdTipoCta_acreditacion_cat =q.IdTipoCta_acreditacion_cat,
                        IdTipoDocumento = q.IdTipoDocumento,
                        CodigoLegalBanco = q.CodigoLegalBanco,
                        Referencia = q.Referencia,

                        IdTipoPersona = q.IdTipo_Persona,
                        IdEntidad = q.IdEntidad,
                        IdPersona = q.IdPersona,
                        Fecha_Factura = q.cb_Fecha,
                        IdCtaCble = q.IdCtaCble_Acreedora,
                        pc_Cuenta = q.pc_Cuenta,

                        IdCbteCble_cxp = q.IdCbteCble_cxp,
                        IdEmpresa_cxp = q.IdEmpresa_cxp,
                        IdTipoCbte_cxp = q.IdTipoCbte_cxp
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_Archivo_Transferencia_Det_Info> get_list_con_saldo(int IdEmpresa, decimal IdPersona, string IdTipo_Persona, decimal IdEntidad, string IdEstado_Aprobacion, string IdUsuario, int IdSucursal, bool mostrar_saldo_0)
        {
            try
            {
                decimal IdPersona_ini = IdPersona;
                decimal IdPersona_fin = IdPersona == 0 ? 99999 : IdPersona;

                decimal IdEntidad_ini = IdEntidad;
                decimal IdEntidad_fin = IdEntidad == 0 ? 99999 : IdEntidad;

                List<ba_Archivo_Transferencia_Det_Info> Lista;
                Entities_banco db = new Entities_banco();
                var banco = db.ba_parametros.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.spcp_Get_Data_orden_pago_con_cancelacion_data(IdEmpresa, IdPersona_ini, IdPersona_fin, IdTipo_Persona, IdEntidad_ini, IdEntidad_fin, IdEstado_Aprobacion, IdUsuario, IdSucursal, mostrar_saldo_0,banco.ValidarSoloCuentasArchivo)
                             select new ba_Archivo_Transferencia_Det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 Secuencia = q.Secuencia_OP,
                                 Secuencia_OP = q.Secuencia_OP,
                                 IdOrdenPago = q.IdOrdenPago,
                                 Nom_Beneficiario =  q.Nom_Beneficiario,
                                 Valor = q.Saldo_x_Pagar_OP,
                                 Referencia = q.Referencia2,
                                 IdTipoPersona = q.IdTipoPersona,
                                 Fecha_Factura = q.Fecha_Fa_Prov,
                                 Observacion = q.Referencia,
                                 IdEntidad = q.IdEntidad,
                                 IdPersona = q.IdPersona,
                                 IdEmpresa_cxp = q.IdEmpresa_cxp,
                                 IdTipoCbte_cxp = q.IdTipoCbte_cxp,
                                 IdCbteCble_cxp = q.IdCbteCble_cxp
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal GetIdSecuencial( int IdEmpresa, int IdBanco, int IdProceso_bancario)
        {
            try
            {
                decimal Id = 1;

                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Archivo_Transferencia_Det
                              join t in Context.ba_Archivo_Transferencia
                              on new { q.IdEmpresa } equals new { t.IdEmpresa }
                              where q.IdEmpresa == IdEmpresa
                              && t.IdBanco == IdBanco
                              && t.IdProceso_bancario == IdProceso_bancario
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.Secuencial_reg_x_proceso) +1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

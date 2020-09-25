using Core.Erp.Info.Banco;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                List<ba_Archivo_Transferencia_Det_Info> Lista = new List<ba_Archivo_Transferencia_Det_Info>();
                using (SqlConnection connection = new SqlConnection(ConexionERP.GetConnectionString()))
                {
                    connection.Open();
                    string query = "SELECT        d.IdEmpresa, d.IdArchivo, d.Secuencia, d.IdEmpresa_OP, d.IdOrdenPago, d.Secuencia_OP, d.Estado, d.Valor, d.Secuencial_reg_x_proceso, "
                                + " CASE WHEN cp_orden_pago.IdTipo_op <> 'FACT_PROVEE' THEN ISNULL(cp_proveedor_detalle.IdTipoCta_acreditacion_cat, cp_proveedor.IdTipoCta_acreditacion_cat) ELSE ISNULL(pd.IdTipoCta_acreditacion_cat,"
                                + " pog.IdTipoCta_acreditacion_cat) END AS IdTipoCta_acreditacion_cat, CASE WHEN cp_orden_pago.IdTipo_op <> 'FACT_PROVEE' THEN ISNULL(cp_proveedor_detalle.num_cta_acreditacion, cp_proveedor.num_cta_acreditacion) "
                                + " ELSE ISNULL(pd.num_cta_acreditacion, pog.num_cta_acreditacion) END AS num_cta_acreditacion, CASE WHEN cp_orden_pago.IdTipo_op <> 'FACT_PROVEE' THEN ISNULL(cp_proveedor_detalle.IdBanco_acreditacion,"
                                + " cp_proveedor.IdBanco_acreditacion) ELSE ISNULL(pd.IdBanco_acreditacion, pog.IdBanco_acreditacion) END AS IdBanco_acreditacion, dbo.cp_proveedor.pr_direccion, "
                                + " CASE WHEN cp_orden_pago.IdTipo_op <> 'FACT_PROVEE' THEN ISNULL(cp_proveedor_detalle.pr_correo, cp_proveedor.pr_correo) ELSE ISNULL(pd.pr_correo, pog.pr_correo) END AS pr_correo, "
                                + " dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, CASE WHEN cp_orden_pago.IdTipo_op <> 'FACT_PROVEE' THEN ISNULL(cp_proveedor_detalle.NombreAdicional, tb_persona.pe_nombreCompleto) "
                                + " ELSE ISNULL(pd.NombreAdicional, eog.pe_nombreCompleto) END AS pe_nombreCompleto, dbo.tb_banco.CodigoLegal AS CodigoLegalBanco, d.Referencia, dbo.cp_orden_pago.IdTipo_Persona, dbo.cp_orden_pago.IdPersona, "
                                + " dbo.cp_orden_pago.IdEntidad, dbo.ct_cbtecble.cb_Fecha, ct.IdCtaCble_Acreedora, pc.IdCtaCble + ' - ' + pc.pc_Cuenta AS pc_Cuenta, dbo.cp_orden_pago_det.IdEmpresa_cxp, dbo.cp_orden_pago_det.IdTipoCbte_cxp, "
                                + " dbo.cp_orden_pago_det.IdCbteCble_cxp, dbo.cp_proveedor.pr_telefonos"
                                + " FROM            dbo.ba_Archivo_Transferencia INNER JOIN"
                                + " dbo.ba_Archivo_Transferencia_Det AS d INNER JOIN"
                                + " dbo.cp_orden_pago_det ON d.IdEmpresa_OP = dbo.cp_orden_pago_det.IdEmpresa AND d.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago AND d.Secuencia_OP = dbo.cp_orden_pago_det.Secuencia INNER JOIN"
                                + " dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN"
                                + " dbo.tb_persona INNER JOIN"
                                + " dbo.cp_proveedor ON dbo.tb_persona.IdPersona = dbo.cp_proveedor.IdPersona ON dbo.cp_orden_pago.IdPersona = dbo.cp_proveedor.IdPersona AND dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor AND"
                                + " dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa ON dbo.ba_Archivo_Transferencia.IdEmpresa = d.IdEmpresa AND dbo.ba_Archivo_Transferencia.IdArchivo = d.IdArchivo LEFT OUTER JOIN"
                                + " dbo.cp_orden_giro AS og ON dbo.cp_orden_pago_det.IdEmpresa_cxp = og.IdEmpresa AND dbo.cp_orden_pago_det.IdTipoCbte_cxp = og.IdTipoCbte_Ogiro AND"
                                + " dbo.cp_orden_pago_det.IdCbteCble_cxp = og.IdCbteCble_Ogiro LEFT OUTER JOIN"
                                + " dbo.cp_proveedor_detalle AS pd ON pd.IdEmpresa = og.IdEmpresa AND pd.IdProveedor = og.IdProveedor AND pd.Secuencia = og.SecuenciaProveedor INNER JOIN"
                                + " dbo.ct_cbtecble ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.ct_cbtecble.IdTipoCbte AND"
                                + " dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.ct_cbtecble.IdCbteCble LEFT OUTER JOIN"
                                + " dbo.cp_proveedor AS pog ON og.IdEmpresa = pog.IdEmpresa AND og.IdProveedor = pog.IdProveedor LEFT OUTER JOIN"
                                + " dbo.cp_proveedor_detalle ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor_detalle.IdEmpresa AND dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor_detalle.IdProveedor AND"
                                + " dbo.cp_orden_pago.SecuenciaProveedor = dbo.cp_proveedor_detalle.Secuencia LEFT OUTER JOIN"
                                + " dbo.vwct_cbtecble_con_ctacble_acreedora AS ct ON ct.IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa AND ct.IdTipoCbte = dbo.cp_orden_pago_det.IdTipoCbte_cxp AND"
                                + " ct.IdCbteCble = dbo.cp_orden_pago_det.IdCbteCble_cxp LEFT OUTER JOIN"
                                + " dbo.ct_plancta AS pc ON ct.IdEmpresa = pc.IdEmpresa AND ct.IdCtaCble_Acreedora = pc.IdCtaCble AND dbo.cp_orden_pago.IdTipo_Persona = 'PROVEE' LEFT OUTER JOIN"
                                + " dbo.tb_persona AS eog ON pog.IdPersona = eog.IdPersona INNER JOIN"
                                + " dbo.tb_banco ON dbo.tb_banco.IdBanco = CASE WHEN cp_orden_pago.IdTipo_op <> 'FACT_PROVEE' THEN ISNULL(cp_proveedor_detalle.IdBanco_acreditacion, cp_proveedor.IdBanco_acreditacion) "
                                + " ELSE ISNULL(pd.IdBanco_acreditacion, pog.IdBanco_acreditacion) END"
                                + " WHERE d.IdEmpresa = " + IdEmpresa.ToString() + " AND D.IdArchivo = " + IdArchivo.ToString();

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ba_Archivo_Transferencia_Det_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdArchivo = Convert.ToDecimal(reader["IdArchivo"]),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            IdEmpresa_OP = Convert.ToInt32(reader["IdEmpresa_OP"]),
                            IdOrdenPago = Convert.ToDecimal(reader["IdOrdenPago"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            Secuencial_reg_x_proceso = Convert.ToDecimal(reader["Secuencial_reg_x_proceso"]),
                            Secuencia_OP = Convert.ToInt32(reader["Secuencia_OP"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            Nom_Beneficiario = Convert.ToString(reader["pe_nombreCompleto"]),
                            pe_cedulaRuc = Convert.ToString(reader["pe_cedulaRuc"]),
                            pr_correo = Convert.ToString(reader["pr_correo"]),
                            pr_direccion = Convert.ToString(reader["pr_direccion"]),
                            pr_telefonos = Convert.ToString(reader["pr_telefonos"]),
                            num_cta_acreditacion = Convert.ToString(reader["num_cta_acreditacion"]),
                            IdBanco_acreditacion = Convert.ToInt32(reader["IdBanco_acreditacion"]),
                            IdTipoCta_acreditacion_cat = Convert.ToString(reader["IdTipoCta_acreditacion_cat"]),
                            IdTipoDocumento = Convert.ToString(reader["IdTipoDocumento"]),
                            CodigoLegalBanco = Convert.ToString(reader["CodigoLegalBanco"]),
                            Referencia = Convert.ToString(reader["Referencia"]),

                            IdTipoPersona = Convert.ToString(reader["IdTipo_Persona"]),
                            IdEntidad = Convert.ToDecimal(reader["IdEntidad"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            Fecha_Factura = Convert.ToDateTime(reader["cb_Fecha"]),
                            IdCtaCble = Convert.ToString(reader["IdCtaCble_Acreedora"]),
                            pc_Cuenta = Convert.ToString(reader["pc_Cuenta"]),

                            IdCbteCble_cxp = Convert.ToDecimal(reader["IdCbteCble_cxp"]),
                            IdEmpresa_cxp = Convert.ToInt32(reader["IdEmpresa_cxp"]),
                            IdTipoCbte_cxp = Convert.ToInt32(reader["IdTipoCbte_cxp"])
                        });
                    }
                    reader.Close();
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

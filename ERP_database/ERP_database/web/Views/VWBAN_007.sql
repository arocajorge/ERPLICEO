CREATE VIEW [web].[VWBAN_007]
AS
SELECT ISNULL(ROW_NUMBER() OVER (ORDER BY dbo.ba_Cbte_Ban.IdEmpresa), 0) AS IdRow, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, GETDATE() cb_FechaCheque, 
dbo.ba_Cbte_Ban.cb_Cheque, dbo.tb_persona.pe_nombreCompleto, dbo.ba_Cbte_Ban.cb_Valor, dbo.ba_Catalogo.ca_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, dbo.seg_usuario.Nombre, dbo.ba_Cbte_Ban.cb_Observacion, 
dbo.ba_Cbte_Ban.IdPersona_Girado_a, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, ba_Cbte_Ban.Estado, dbo.ba_Catalogo.IdCatalogo
FROM     dbo.ba_Cbte_Ban INNER JOIN
                  dbo.tb_persona ON dbo.ba_Cbte_Ban.IdPersona_Girado_a = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.ba_Catalogo ON dbo.ba_Cbte_Ban.IdEstado_cheque_cat = dbo.ba_Catalogo.IdCatalogo INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_Cbte_Ban.IdBanco = dbo.ba_Banco_Cuenta.IdBanco LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.ba_Cbte_Ban.IdUsuario = dbo.seg_usuario.IdUsuario
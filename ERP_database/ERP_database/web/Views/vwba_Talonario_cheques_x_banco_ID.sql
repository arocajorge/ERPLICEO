CREATE VIEW [web].[vwba_Talonario_cheques_x_banco_ID]
AS
SELECT IdEmpresa, IdBanco, CAST(Num_cheque AS numeric) AS Num_cheque
FROM     dbo.ba_Talonario_cheques_x_banco
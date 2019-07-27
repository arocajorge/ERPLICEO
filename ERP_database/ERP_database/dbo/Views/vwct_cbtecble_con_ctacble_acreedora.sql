CREATE VIEW [dbo].[vwct_cbtecble_con_ctacble_acreedora]
AS
SELECT IdEmpresa, IdTipoCbte, IdCbteCble, MAX(IdCtaCble_Acreedora) AS IdCtaCble_Acreedora
FROM     (        SELECT ct.IdEmpresa, CT.IdTipoCbte, ct.IdCbteCble, ct.IdCtaCble IdCtaCble_Acreedora
                  FROM     dbo.ct_cbtecble_det AS ct
                  WHERE  (ct.dc_Valor < 0)) AS Querry
GROUP BY IdEmpresa, IdTipoCbte, IdCbteCble
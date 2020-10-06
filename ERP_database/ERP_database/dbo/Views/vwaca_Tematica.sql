create view vwaca_Tematica as
SELECT t.IdEmpresa, t.IdTematica, t.IdCampoAccion, t.NombreTematica, t.OrdenTematica, t.Estado, c.NombreCampoAccion, c.OrdenCampoAccion
FROM     dbo.aca_Tematica AS t INNER JOIN
  dbo.aca_CampoAccion AS c ON t.IdEmpresa = c.IdEmpresa AND t.IdCampoAccion = c.IdCampoAccion
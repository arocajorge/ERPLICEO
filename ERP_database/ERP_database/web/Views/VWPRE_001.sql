
CREATE view [web].[VWPRE_001] as
SELECT        d.IdEmpresa, d.IdPresupuesto, d.Secuencia, c.IdSucursal, su.Su_Descripcion, c.IdPeriodo, pe.DescripciónPeriodo,  d.IdRubro, ru.Descripcion AS DescripcionRubro, d.IdCtaCble, 
                         pc.pc_Cuenta, d.Monto, c.Estado, c.MontoSolicitado, c.MontoAprobado, c.Observacion, c.MotivoAnulacion, c.IdUsuarioAprobacion, c.FechaAprobacion
FROM            dbo.pre_Presupuesto AS c INNER JOIN
                         dbo.pre_PresupuestoDet AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdPresupuesto = d.IdPresupuesto INNER JOIN
                         dbo.pre_PresupuestoPeriodo AS pe ON c.IdEmpresa = pe.IdEmpresa AND c.IdPeriodo = pe.IdPeriodo INNER JOIN
                         dbo.tb_sucursal AS su ON c.IdEmpresa = su.IdEmpresa AND c.IdSucursal = su.IdSucursal INNER JOIN
                         dbo.pre_Rubro AS ru ON d.IdEmpresa = ru.IdEmpresa AND d.IdRubro = ru.IdRubro LEFT OUTER JOIN
                         dbo.ct_plancta AS pc ON d.IdCtaCble = pc.IdCtaCble AND d.IdEmpresa = pc.IdEmpresa
GO



GO



GO



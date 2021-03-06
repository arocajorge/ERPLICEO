﻿
CREATE PROCEDURE [Academico].[SPACA_ContabilizacionCobros]
(
@IdEmpresa int,
@FechaIni date,
@FechaFin date
)
AS
SELECT a.IdEmpresa, a.IdSucursal, a.IdCobro, c.pe_nombreCompleto, a.IdCobro_tipo, rel.ct_IdEmpresa, rel.ct_IdTipoCbte, rel.ct_IdCbteCble, a.cr_TotalCobro TotalModulo, SUM(isnull(CT.dc_Valor,0)) TotalContabilidad,
dbo.BankersRounding(a.cr_TotalCobro - SUM(isnull(CT.dc_Valor,0)),2) as Saldo, A.cr_fecha, A.cr_ObservacionPantalla
FROM     ct_cbtecble_det AS ct with (nolock) left JOIN
                  cxc_cobro_x_ct_cbtecble AS rel with (nolock) ON ct.IdEmpresa = rel.ct_IdEmpresa AND ct.IdTipoCbte = rel.ct_IdTipoCbte AND ct.IdCbteCble = rel.ct_IdCbteCble RIGHT OUTER JOIN
                  cxc_cobro AS a with (nolock) INNER JOIN
                  aca_Alumno AS b with (nolock) ON a.IdEmpresa = b.IdEmpresa AND a.IdAlumno = b.IdAlumno INNER JOIN
                  tb_persona AS c with (nolock) ON b.IdPersona = c.IdPersona ON rel.cbr_IdEmpresa = a.IdEmpresa AND rel.cbr_IdSucursal = a.IdSucursal AND rel.cbr_IdCobro = a.IdCobro
WHERE  (a.IdEmpresa = @IdEmpresa) AND (a.IdCobro_tipo NOT IN ('NTCR', 'NTDB')) AND ISNULL(CT.dc_Valor,0.02) > 0.01 and a.cr_fecha between @FechaIni and @FechaFin and a.cr_estado = 'A'
GROUP BY a.IdEmpresa, a.IdSucursal, a.IdCobro, c.pe_nombreCompleto, a.IdCobro_tipo, rel.ct_IdEmpresa, rel.ct_IdTipoCbte, rel.ct_IdCbteCble, a.cr_TotalCobro,A.cr_fecha, A.cr_ObservacionPantalla
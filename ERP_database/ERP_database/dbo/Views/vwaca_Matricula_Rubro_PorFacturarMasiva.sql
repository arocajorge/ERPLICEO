CREATE VIEW [dbo].[vwaca_Matricula_Rubro_PorFacturarMasiva]
AS
SELECT     a.IdEmpresa, a.IdMatricula, a.IdAnio, a.IdPeriodo, a.IdPlantilla, a.IdRubro, a.IdProducto, a.Subtotal, a.IdCod_Impuesto_Iva, a.Porcentaje, a.ValorIVA, a.Total, 
                      CASE WHEN j.AplicaProntoPago = 1 AND d .FechaProntoPago > CAST(getdate() AS date) THEN CAST(k.Subtotal AS float) 
                      - (CASE WHEN l.AplicaParaTodo = 1 THEN (CASE WHEN l.TipoDescuento = '$' THEN CAST(l.Valor AS FLOAT) ELSE ROUND(CAST(k.Subtotal AS float) * (l.Valor / 100), 2) 
                      END) ELSE (CASE WHEN k.TipoDescuento_descuentoDet = '$' THEN CAST(k.Valor_descuentoDet AS FLOAT) ELSE ROUND(CAST(k.Subtotal AS float) 
                      * (k.Valor_descuentoDet / 100), 2) END) END) ELSE CAST(k.Subtotal AS float) + CAST(k.ValorIVA AS FLOAT) END + k.ValorIVA AS ValorProntoPago, 
                      c.NomRubro + CASE WHEN c.NumeroCuotas > 1 THEN + ' ' + CAST(e.Secuencia AS varchar) + '/' + CAST(c.NumeroCuotas AS varchar) 
                      ELSE '' END + ' ' + f.smes + ' ' + CAST(YEAR(d.FechaDesde) AS varchar) AS Observacion, h.IdAlumno, d.FechaDesde, d.FechaProntoPago, b.IdTerminoPago, 
                      i.IdCliente, m.Codigo, n.pe_nombreCompleto AS Alumno, H.IdEmpresa_rol, h.IdEmpleado
FROM         dbo.aca_Matricula_Rubro AS a INNER JOIN
                      dbo.aca_MecanismoDePago AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdMecanismo = b.IdMecanismo LEFT OUTER JOIN
                      dbo.aca_AnioLectivo_Rubro AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdRubro = c.IdRubro AND a.IdRubro = c.IdRubro LEFT OUTER JOIN
                      dbo.aca_AnioLectivo_Periodo AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdAnio = d.IdAnio AND a.IdPeriodo = d.IdPeriodo LEFT OUTER JOIN
                      dbo.aca_AnioLectivo_Rubro_Periodo AS e ON a.IdEmpresa = e.IdEmpresa AND a.IdRubro = e.IdRubro AND a.IdPeriodo = e.IdPeriodo AND 
                      a.IdAnio = e.IdAnio LEFT OUTER JOIN
                      dbo.tb_mes AS f ON d.IdMes = f.idMes LEFT OUTER JOIN
                      dbo.aca_AnioLectivo AS g ON a.IdEmpresa = g.IdEmpresa AND a.IdAnio = g.IdAnio INNER JOIN
                      dbo.aca_Matricula AS h ON a.IdEmpresa = h.IdEmpresa AND a.IdMatricula = h.IdMatricula LEFT OUTER JOIN
                          (SELECT     IdEmpresa, IdPersona, MAX(IdCliente) AS IdCliente
                            FROM          dbo.fa_cliente
                            GROUP BY IdEmpresa, IdPersona) AS i ON h.IdPersonaF = i.IdPersona AND h.IdEmpresa = i.IdEmpresa LEFT OUTER JOIN
                      dbo.aca_AnioLectivo_Rubro AS j ON j.IdEmpresa = a.IdEmpresa AND j.IdAnio = a.IdAnio AND j.IdRubro = a.IdRubro LEFT OUTER JOIN
                      dbo.aca_Plantilla_Rubro AS k ON a.IdEmpresa = k.IdEmpresa AND a.IdAnio = k.IdAnio AND a.IdPlantilla = k.IdPlantilla AND a.IdRubro = k.IdRubro LEFT OUTER JOIN
                      dbo.aca_Plantilla AS l ON l.IdEmpresa = k.IdEmpresa AND l.IdAnio = k.IdAnio AND l.IdPlantilla = k.IdPlantilla LEFT OUTER JOIN
                      dbo.aca_Alumno AS m ON h.IdEmpresa = m.IdEmpresa AND h.IdAlumno = m.IdAlumno LEFT OUTER JOIN
                      dbo.tb_persona AS n ON m.IdPersona = n.IdPersona
WHERE     (a.FechaFacturacion IS NULL) AND (NOT EXISTS
                          (SELECT     IdEmpresa
                            FROM          dbo.aca_AlumnoRetiro AS ret
                            WHERE      (IdEmpresa = a.IdEmpresa) AND (IdMatricula = a.IdMatricula) AND (Estado = 1)))
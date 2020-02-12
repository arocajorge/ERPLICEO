﻿--EXEC Academico.SPCXC_004 1,'admin','2020/2/2'
CREATE PROCEDURE Academico.SPCXC_004
(
@IdEmpresa int,
@IdUsuario varchar(200),
@FechaCorte date
)
AS
PRINT 'BORRAR TABLA CXC_004'
DELETE [Academico].[cxc_SPCXC_004] where IdUsuario = @IdUsuario

PRINT 'INSERT DATA INICIAL'
BEGIN 
	INSERT INTO [Academico].[cxc_SPCXC_004]
			   ([IdEmpresa]
			   ,[IdAlumno]
			   ,[IdAnio]
			   ,[IdUsuario]
			   ,[NomAnio]
			   ,[CodigoAlumno]
			   ,[NombreAlumno]
			   ,[IdJornada]
			   ,[NombreJornada]
			   ,[SaldoDeudor]
			   ,[SaldoAcreedor]
			   ,[SaldoFinal])
	select a.IdEmpresa, a.IdAlumno, 0, @IdUsuario, 'NINGUNO', a.Codigo, b.pe_nombreCompleto, 0 IdJornada, 'NINGUNO' NomJornada, 0, 0, 0
	from aca_Alumno as a inner join 
	tb_persona as b on a.IdPersona = b.IdPersona 
	UNION ALL
	select a.IdEmpresa, a.IdAlumno, c.IdAnio, @IdUsuario, cast(year(FechaDesde) as varchar) ,a.Codigo, b.pe_nombreCompleto, c.IdJornada, e.NomJornada, 0, 0, 0
	from aca_Alumno as a inner join 
	tb_persona as b on a.IdPersona = b.IdPersona INNER JOIN
	ACA_matricula as c on a.IdEMpresa = c.idempresa and a.idalumno = c.idalumno inner join
	aca_anioLectivo as d on c.IdEmpresa = d.IdEmpresa and c.IdAnio = D.IdAnio inner join
	aca_AnioLectivo_NivelAcademico_Jornada as e on e.IdEmpresa = c.IdEmpresa and  e.IdAnio = c.IdAnio  and e.IdSede = c.IdSede and e.IdNivel = c.IdNivel and e.IdJornada = c.IdJornada
	
END

PRINT 'UPDATE DE SALDO DEUDOR'
BEGIN 

update [Academico].[cxc_SPCXC_004] set SaldoDeudor = A.SaldoDeudor
FROM(
	SELECT S.IdEmpresa, S.IdAlumno, s.IdAnio, SUM(S.SaldoDeudor) SaldoDeudor FROM(
		select a.IdEmpresa, a.IdAlumno, sum(b.Total)Total, ISNULL(SUM(C.dc_ValorPago),0)dc_ValorPago, sum(b.Total) - ISNULL(SUM(C.dc_ValorPago),0) SaldoDeudor, b.IdAnio
		from fa_factura as a inner join 
		fa_factura_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega= b.IdBodega and a.IdCbteVta = b.idcbtevta LEFT JOIN
		(
			SELECT a2.IdEmpresa, a2.IdSucursal, a2.IdBodega_Cbte, a2.IdCbte_vta_nota, a2.dc_TipoDocumento, sum(a2.dc_ValorPago) dc_ValorPago
			FROM cxc_cobro AS a1 inner join 
			cxc_cobro_det as a2 on a1.IdEmpresa = a2.IdEmpresa and a1.IdSucursal = a2.IdSucursal and a1.IdCobro = a2.IdCobro
			where a1.IdEmpresa = @IdEmpresa and a1.cr_fecha <= @FechaCorte and a1.cr_estado = 'A' AND A2.dc_TipoDocumento = 'FACT'
			group by a2.IdEmpresa, a2.IdSucursal, a2.IdBodega_Cbte, a2.IdCbte_vta_nota, a2.dc_TipoDocumento
		) as c ON A.IdEmpresa = C.IdEmpresa AND A.IdSucursal = C.IdSucursal AND A.IdBodega = C.IdBodega_Cbte AND A.IdCbteVta = C.IdCbte_vta_nota
		where a.Estado = 'A' and a.IdEmpresa = @IdEmpresa and a.vt_fecha <= @FechaCorte
		group by a.IdEmpresa, a.IdAlumno, b.IdAnio

		UNION ALL

		select a.IdEmpresa, a.IdAlumno, sum(b.Total)Total, ISNULL(SUM(C.dc_ValorPago),0)dc_ValorPago, sum(b.Total) - ISNULL(SUM(C.dc_ValorPago),0) SaldoDeudor, 0
		from fa_notaCreDeb as a inner join 
		fa_notaCreDeb_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega= b.IdBodega and a.IdNota = b.IdNota LEFT JOIN
		(
			SELECT a2.IdEmpresa, a2.IdSucursal, a2.IdBodega_Cbte, a2.IdCbte_vta_nota, a2.dc_TipoDocumento, sum(a2.dc_ValorPago) dc_ValorPago
			FROM cxc_cobro AS a1 inner join 
			cxc_cobro_det as a2 on a1.IdEmpresa = a2.IdEmpresa and a1.IdSucursal = a2.IdSucursal and a1.IdCobro = a2.IdCobro
			where a1.IdEmpresa = @IdEmpresa and a1.cr_fecha <= @FechaCorte and a1.cr_estado = 'A' AND A2.dc_TipoDocumento = 'NTDB'
			group by a2.IdEmpresa, a2.IdSucursal, a2.IdBodega_Cbte, a2.IdCbte_vta_nota, a2.dc_TipoDocumento
		) as c ON A.IdEmpresa = C.IdEmpresa AND A.IdSucursal = C.IdSucursal AND A.IdBodega = C.IdBodega_Cbte AND A.IdNota = C.IdCbte_vta_nota
		where a.Estado = 'A' and a.IdEmpresa = @IdEmpresa and a.no_fecha <= @FechaCorte AND A.CreDeb = 'D'
		group by a.IdEmpresa, a.IdAlumno
		) S
		GROUP BY S.IdEmpresa, S.IdAlumno, s.IdAnio
	) A
WHERE [Academico].[cxc_SPCXC_004].IdEmpresa = A.IdEmpresa
AND [Academico].[cxc_SPCXC_004].IdAlumno = A.IdAlumno
AND [Academico].[cxc_SPCXC_004].IdAnio = A.IdAnio
END

select * from [Academico].[cxc_SPCXC_004] where IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario
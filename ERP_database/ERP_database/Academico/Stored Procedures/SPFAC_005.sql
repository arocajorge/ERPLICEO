--exec Academico.SPFAC_005 1,'01/01/2020', '29/02/2020','C','SRI'
CREATE PROCEDURE Academico.SPFAC_005
(
@IdEmpresa int,
@FechaDesde date,
@FechaHasta date,
@CreDeb varchar(1),
@Naturaleza varchar(3)
)
AS
BEGIN

select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.no_fecha, a.CreDeb, a.NaturalezaNota, a.Estado, case when a.NaturalezaNota = 'SRI' THEN a.NumNota_Impresa else a.CodNota end as NumeroNota,
B.SubtotalConDscto, B.ValorIVA, B.Total, isnull(h.Valor_Aplicado,0) Valor_Aplicado, isnull(k.Valor_Aplicado,0) as Cruce, B.Total - isnull(k.Valor_Aplicado,0) - isnull(h.Valor_Aplicado,0) Saldo, d.pe_nombreCompleto as NombreCliente, f.pe_nombreCompleto as NombreAlumno, a.IdTipoNota, g.No_Descripcion
from fa_notaCreDeb as a inner join
fa_notaCreDeb_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdNota = b.IdNota INNER JOIN
fa_cliente AS c on a.IdEmpresa = c.IdEmpresa and a.IdCliente = c.IdCliente INNER JOIN
tb_persona AS d on c.IdPersona = d.IdPersona inner join
aca_Alumno as e on a.IdEmpresa = e.IdEmpresa and a.IdAlumno = e.IdAlumno inner join
tb_persona as f on e.IdPersona = f.IdPersona left join 
fa_TipoNota as g on g.IdEmpresa = a.IdEmpresa and g.IdTipoNota = a.IdTipoNota left join
(
	select a1.IdEmpresa, a1.IdSucursal, a1.IdBodega, a1.IdNota, sum(a2.Valor_Aplicado) Valor_Aplicado
	from fa_notaCreDeb as a1 inner join 
	fa_notaCreDeb_x_fa_factura_NotaDeb as a2 on a1.IdEmpresa = a2.IdEmpresa_nt and a1.IdSucursal = a2.IdSucursal_nt and a1.IdBodega = a2.IdBodega_nt and a1.IdNota = a2.IdNota_nt left join
		(
			select x.IdEmpresa, x.IdSucursal, x.IdBodega, x.IdNota, y.secuencia_nt
			from cxc_ConciliacionNotaCredito as x inner join 
			cxc_ConciliacionNotaCreditoDet as y on x.IdEmpresa = y.IdEmpresa and x.IdConciliacion = y.IdConciliacion
			where x.IdEmpresa = @IdEmpresa and x.Fecha between @FechaDesde and @FechaHasta and x.Estado=1
		) as j on a2.IdEmpresa_nt = j.IdEmpresa and a2.IdSucursal_nt = j.IdSucursal and a2.IdBodega_nt = j.IdBodega and a2.IdNota_nt = j.IdNota and a2.secuencia = j.secuencia_nt
	where a1.IdEmpresa = @IdEmpresa and a1.no_fecha between @FechaDesde and @FechaHasta and a1.CreDeb = @CreDeb and a1.NaturalezaNota like ('%'+isnull(@Naturaleza,'')+'%') AND A1.Estado = 'A'
	and cast(a2.fecha_cruce as date) <= @FechaHasta and j.secuencia_nt is null
	group by a1.IdEmpresa, a1.IdSucursal, a1.IdBodega, a1.IdNota
) AS h on a.IdEmpresa = h.IdEmpresa and a.IdSucursal = h.IdSucursal and a.IdBodega = h.IdBodega and a.IdNota = h.IdNota left join
(
	select a1.IdEmpresa, a1.IdSucursal, a1.IdBodega, a1.IdNota, sum(a2.Valor_Aplicado) Valor_Aplicado
	from fa_notaCreDeb as a1 inner join 
	fa_notaCreDeb_x_fa_factura_NotaDeb as a2 on a1.IdEmpresa = a2.IdEmpresa_nt and a1.IdSucursal = a2.IdSucursal_nt and a1.IdBodega = a2.IdBodega_nt and a1.IdNota = a2.IdNota_nt left join
		(
			select x.IdEmpresa, x.IdSucursal, x.IdBodega, x.IdNota, y.secuencia_nt
			from cxc_ConciliacionNotaCredito as x inner join 
			cxc_ConciliacionNotaCreditoDet as y on x.IdEmpresa = y.IdEmpresa and x.IdConciliacion = y.IdConciliacion
			where x.IdEmpresa = @IdEmpresa and x.Fecha between @FechaDesde and @FechaHasta and x.Estado= 1
		) as j on a2.IdEmpresa_nt = j.IdEmpresa and a2.IdSucursal_nt = j.IdSucursal and a2.IdBodega_nt = j.IdBodega and a2.IdNota_nt = j.IdNota and a2.secuencia = j.secuencia_nt
	where a1.IdEmpresa = @IdEmpresa and a1.no_fecha between @FechaDesde and @FechaHasta and a1.CreDeb = @CreDeb and a1.NaturalezaNota like ('%'+isnull(@Naturaleza,'')+'%') AND A1.Estado = 'A'
	and cast(a2.fecha_cruce as date) <= @FechaHasta and j.secuencia_nt is not null
	group by a1.IdEmpresa, a1.IdSucursal, a1.IdBodega, a1.IdNota
) AS k on a.IdEmpresa = k.IdEmpresa and a.IdSucursal = k.IdSucursal and a.IdBodega = k.IdBodega and a.IdNota = k.IdNota 



where a.IdEmpresa = @IdEmpresa and a.no_fecha between @FechaDesde and @FechaHasta and a.CreDeb = @CreDeb and ISNULL(a.NaturalezaNota,'INT') like ('%'+isnull(@Naturaleza,'')+'%') AND A.Estado = 'A'

END
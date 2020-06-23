--exec Academico.SPCXC_010 1,4642,'2020/01/01','2020/05/31'
CREATE PROCEDURE Academico.SPCXC_010
(
@IdEmpresa int,
@IdAlumno numeric,
@FechaIni date,
@FechaFin date
)
AS

DECLARE @SaldoInicial float

SELECT @SaldoInicial = sum(Valor) FROM Academico.VWCXC_010 where IdEmpresa = @IdEmpresa and IdAlumno = @IdAlumno and vt_fecha < @FechaIni

select IdEmpresa,Tipo,IdCbteVta,Orden,vt_fecha,vt_Observacion,Referencia,Anio,IdMes,smes,@SaldoInicial SaldoInicial, Debe,Haber,Valor, ISNULL(@SaldoInicial,0) + sum(Valor) over(order by vt_fecha, Orden, IdCbteVta),
pe_nombreCompleto,CodigoAlumno,IdAlumno 
from Academico.VWCXC_010 where IdEmpresa = @IdEmpresa AND IdAlumno = @IdAlumno AND vt_fecha BETWEEN @FechaIni AND @FechaFin
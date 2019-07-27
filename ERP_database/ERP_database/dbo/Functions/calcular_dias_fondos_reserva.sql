 CREATE function [dbo].[calcular_dias_fondos_reserva]
 (
  @Fecha_inicio date,
  @Fecha_fin date,
  @fecha_ingreso date,
  @em_status varchar(10),
  @Fecha_salida date,
  @FechaInicioAcum date
 )
 returns int 
 as
 begin 

   declare @dias int,
    @Mes int
	if(DATEPART(day,@Fecha_fin) = 31)
		set @Fecha_fin=DATEADD (DAY , -1 , @Fecha_fin )
	if(datepart(MONTH, @Fecha_fin)=2 and datepart(DAY, @Fecha_fin)=28)
		set @Fecha_fin=DATEADD (DAY , 2 , @Fecha_fin )
	if(datepart(MONTH, @Fecha_fin)=2 and datepart(DAY, @Fecha_fin)=29)
		set @Fecha_fin=DATEADD (DAY , 1 , @Fecha_fin )

				--EMPLEADO ENTRA Y SALE EN EL MISMO MES
	SET @dias = CASE WHEN (@FechaInicioAcum BETWEEN @Fecha_inicio AND @Fecha_fin) AND (@Fecha_salida BETWEEN @Fecha_inicio AND @Fecha_fin) 
					THEN DATEDIFF(DAY,@FechaInicioAcum,@Fecha_salida)+1

				--EMPLEADO ENTRA DURANTE EL MES Y SIGUE ACUMULANDO
				WHEN (@FechaInicioAcum BETWEEN @Fecha_inicio AND @Fecha_fin) AND (@Fecha_salida NOT BETWEEN @Fecha_inicio AND @Fecha_fin)
					THEN DATEDIFF(DAY,@FechaInicioAcum,@Fecha_fin)+1

				--EMPLEADO COMIENZA A ACUMULAR ANTES DE PERIODO Y SIGUE TRABAJANDO
				WHEN (@FechaInicioAcum <= @Fecha_inicio) AND (@Fecha_salida >= @Fecha_fin)
					THEN DATEDIFF(DAY,@Fecha_inicio,@Fecha_fin)+1

				--EMPLEADO COMIENZA A ACUMULAR ANTES DE PERIODO Y SALE DURANTE EL PERIODO
				WHEN (@FechaInicioAcum <= @Fecha_inicio) AND (@Fecha_salida <= @Fecha_fin)
					--THEN DATEDIFF(DAY,@Fecha_inicio,@Fecha_salida)+1
					THEN 30
					END
   return @dias
 end;
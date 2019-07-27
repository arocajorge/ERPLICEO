--[web].[SPCONTA_006] 2,2019,0,'admin',6,0,'ER'
CREATE PROCEDURE [web].[SPCONTA_006]
(
@IdEmpresa int,
@IdAnio int,
@MostrarSaldo0 bit,
@IdUsuario varchar(50),
@IdNivel int,
@MostrarAcumulado bit,
@Balance varchar(2)
)
AS
DELETE [web].[ct_CONTA_006] WHERE IdUsuario = @IdUsuario

INSERT INTO [web].[ct_CONTA_006]
           ([IdUsuario]
           ,[IdEmpresa]
           ,[IdCtaCble]
           ,[pc_Cuenta]
           ,[IdCtaCblePadre]
           ,[EsCtaUtilidad]
           ,[IdNivelCta]
           ,[IdGrupoCble]
           ,[gc_GrupoCble]
           ,[gc_estado_financiero]
           ,[gc_Orden]
           ,[EsCuentaMovimiento]
           ,[Naturaleza]
           ,[Enero]
           ,[Febrero]
           ,[Marzo]
           ,[Abril]
           ,[Mayo]
           ,[Junio]
           ,[Julio]
           ,[Agosto]
           ,[Septiembre]
           ,[Octubre]
           ,[Noviembre]
           ,[Diciembre]
           ,[Total])

SELECT @IdUsuario, 
ct_plancta.IdEmpresa, 
ct_plancta.IdCtaCble, 
ct_plancta.pc_Cuenta, 
ct_plancta.IdCtaCblePadre, 
CASE WHEN ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble = ct_plancta.IdCtaCble THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS EsCtaUtilidad, 
ct_plancta.IdNivelCta,
ct_plancta.IdGrupoCble, 
ct_grupocble.gc_GrupoCble, 
ct_grupocble.gc_estado_financiero, 
ct_grupocble.gc_Orden,
0,ct_plancta.pc_Naturaleza,0,0,0,0,0,0,0,0,0,0,0,0,0
FROM            ct_anio_fiscal_x_cuenta_utilidad RIGHT OUTER JOIN
        ct_plancta ON ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa = ct_plancta.IdEmpresa AND ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble = ct_plancta.IdCtaCble LEFT OUTER JOIN
        ct_grupocble ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble
WHERE        (ISNULL(ct_anio_fiscal_x_cuenta_utilidad.IdanioFiscal, @IdAnio) = @IdAnio) AND ct_plancta.IdEmpresa = @IdEmpresa

UPDATE [web].[ct_CONTA_006] SET 
ENERO = A.Enero, 
Febrero = A.Febrero, 
Marzo = A.Marzo, 
Abril = A.Abril, 
Mayo = A.Mayo, 
Junio = A.Junio, 
Julio = A.Julio,
Agosto = A.Agosto, 
Septiembre = A.Septiembre, 
Octubre = A.Octubre, 
Noviembre = A.Noviembre, 
Diciembre = A.Diciembre,
Total = A.Enero + A.Febrero + A.Marzo + A.Abril + A.Mayo + A.Junio + A.Julio + A.Agosto + A.Septiembre + A.Octubre + A.Noviembre + A.Diciembre
from(
select G.IdEmpresa, G.IdCtaCble, SUM(G.Enero)Enero, SUM(G.Febrero)Febrero, SUM(G.Marzo)Marzo, SUM(G.Abril)Abril, SUM(G.Mayo)Mayo, SUM(G.Junio)Junio, SUM(G.Julio)Julio, SUM(G.Agosto)Agosto,
SUM(G.Septiembre)Septiembre, SUM(G.Octubre)Octubre, SUM(G.Noviembre)Noviembre, SUM(G.Diciembre)Diciembre
from(
SELECT D.IdEmpresa, D.IdCtaCble, 

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,1,31) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 1 then D.dc_Valor else 0 end as Enero,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,2,28) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 2 then D.dc_Valor else 0 end as Febrero,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,3,31) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 3 then D.dc_Valor else 0 end as Marzo,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,4,30) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 4 then D.dc_Valor else 0 end as Abril,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,5,31) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 5 then D.dc_Valor else 0 end as Mayo,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,6,30) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 6 then D.dc_Valor else 0 end as Junio,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,7,31) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 7 then D.dc_Valor else 0 end as Julio,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,8,31) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 8 then D.dc_Valor else 0 end as Agosto,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,9,30) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 9 then D.dc_Valor else 0 end as Septiembre,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,10,31) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 10 then D.dc_Valor else 0 end as Octubre,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,11,30) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 11 then D.dc_Valor else 0 end as Noviembre,

CASE WHEN @MostrarAcumulado = 1 THEN 
CASE WHEN C.cb_Fecha <= DATEFROMPARTS(@IdAnio,12,31) THEN D.dc_Valor ELSE 0 END
WHEN MONTH(C.cb_Fecha) = 12 then D.dc_Valor else 0 end as Diciembre

FROM ct_cbtecble AS C INNER JOIN 
ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble INNER JOIN 
WEB.tb_FiltroReportes AS F ON C.IdEmpresa = F.IdEmpresa AND C.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario 
WHERE C.IdEmpresa = @IdEmpresa AND YEAR(C.cb_Fecha) between case when @MostrarAcumulado = 1 then 0 else @IdAnio end and @IdAnio
)G 
GROUP BY G.IdEmpresa, G.IdCtaCble

)A WHERE [web].[ct_CONTA_006].IdEmpresa = A.IdEmpresa
AND [web].[ct_CONTA_006].IdCtaCble = A.IdCtaCble

BEGIN --CUENTA UTILIDAD
UPDATE [web].[ct_CONTA_006] SET ENERO = A.Enero, 
Febrero = A.Febrero, 
Marzo = A.Marzo, 
Abril = A.Abril, 
Mayo = A.Mayo, 
Junio = A.Junio, 
Julio = A.Julio,
Agosto = A.Agosto, 
Septiembre = A.Septiembre, 
Octubre = A.Octubre, 
Noviembre = A.Noviembre, 
Diciembre = A.Diciembre,
Total = A.Enero + A.Febrero + A.Marzo + A.Abril + A.Mayo + A.Junio + A.Julio + A.Agosto + A.Septiembre + A.Octubre + A.Noviembre + A.Diciembre
FROM(
SELECT IdEmpresa, SUM(Enero)Enero,SUM(Febrero)Febrero, SUM(Marzo)Marzo,SUM(Abril)Abril, SUM(Mayo)Mayo,SUM(Junio)Junio, SUM(Julio)Julio, SUM(Agosto)Agosto, SUM(Septiembre)Septiembre,
SUM(Octubre)Octubre, SUM(Noviembre)Noviembre, SUM(Diciembre)Diciembre
FROM [web].[ct_CONTA_006]
WHERE IdEmpresa = @IdEmpresa AND IdUsuario = @IdUsuario AND gc_estado_financiero = 'ER'
GROUP BY IdEmpresa
) A WHERE [web].[ct_CONTA_006].IdEmpresa = A.IdEmpresa
AND[web].[ct_CONTA_006].EsCtaUtilidad = 1
END



BEGIN --SUMATORIA ASCENDENTE

DECLARE @Contador int

select @Contador = max(IdNivelCta) 
from web.[ct_CONTA_006]
where IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa
	WHILE @Contador > 0
	BEGIN

		UPDATE web.[ct_CONTA_006] 
		SET ENERO = A.Enero, 
		Febrero = A.Febrero, 
		Marzo = A.Marzo, 
		Abril = A.Abril, 
		Mayo = a.Mayo, 
		Junio = A.Junio, 
		Julio = A.Julio,
		Agosto = A.Agosto, 
		Septiembre = A.Septiembre, 
		Octubre = A.Octubre, 
		Noviembre = A.Noviembre, 
		Diciembre = A.Diciembre,
		Total = A.Total
		FROM(
		SELECT        IdEmpresa, IdCtaCblePadre
		   ,SUM(Enero)Enero
           ,SUM(Febrero) Febrero
           ,SUM(Marzo)Marzo
           ,SUM(Abril)Abril
           ,SUM(Mayo)Mayo
           ,SUM(Junio)Junio
           ,SUM(Julio)Julio
           ,SUM(Agosto)Agosto
           ,SUM(Septiembre)Septiembre
		   ,SUM(Octubre)Octubre
		   ,SUM(Noviembre)Noviembre
		   ,SUM(Diciembre)Diciembre
		   ,SUM(Total)Total
		FROM            web.[ct_CONTA_006]
		where web.[ct_CONTA_006].IdEmpresa = @IdEmpresa
		and web.[ct_CONTA_006].IdUsuario = @IdUsuario
		GROUP BY IdEmpresa, IdCtaCblePadre
		
		) A where web.[ct_CONTA_006].IdEmpresa = a.IdEmpresa
		and web.[ct_CONTA_006].IdCtaCble = a.IdCtaCblePadre
		and web.[ct_CONTA_006].IdUsuario = @IdUsuario
		and web.[ct_CONTA_006].IdEmpresa = @IdEmpresa

		SET @Contador = @Contador - 1
	END

END

UPDATE [web].[ct_CONTA_006] SET 
ENERO = iif(Naturaleza = 'C', Enero * -1, Enero), 
Febrero = iif(Naturaleza = 'C', Febrero * -1, Febrero), 
Marzo = iif(Naturaleza = 'C', Marzo * -1, Marzo), 
Abril = iif(Naturaleza = 'C', Abril * -1, Abril), 
Mayo = iif(Naturaleza = 'C', Mayo * -1, Mayo), 
Junio = iif(Naturaleza = 'C', Junio * -1, Junio), 
Julio = iif(Naturaleza = 'C', Julio * -1, Julio),
Agosto = iif(Naturaleza = 'C', Agosto * -1, Agosto), 
Septiembre = iif(Naturaleza = 'C', Septiembre * -1, Septiembre), 
Octubre = iif(Naturaleza = 'C', Octubre * -1, Octubre), 
Noviembre = iif(Naturaleza = 'C', Noviembre * -1, Noviembre), 
Diciembre = iif(Naturaleza = 'C', Diciembre * -1, Diciembre),
Total = iif(Naturaleza = 'C', Total * -1, Total)
where IdUsuario = @IdUsuario

update [web].[ct_CONTA_006] set EsCuentaMovimiento = 1 
where IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa
and not exists(
select * from [web].[ct_CONTA_006] as f
where f.IdEmpresa = [web].[ct_CONTA_006].IdEmpresa
and f.IdCtaCblePadre = [web].[ct_CONTA_006].IdCtaCble
and f.IdEmpresa = @IdEmpresa
and f.IdUsuario = @IdUsuario
)

IF(@MostrarSaldo0 = 0)
BEGIN
	DELETE [web].[ct_CONTA_006]
	WHERE Enero = 0 AND Febrero = 0 AND Marzo = 0 AND Abril = 0 AND Mayo = 0 AND Junio = 0 AND Julio = 0 AND Agosto = 0 AND Septiembre = 0 AND Octubre= 0 AND Noviembre = 0 AND Diciembre= 0 AND Total = 0
END

select * from [web].[ct_CONTA_006] 
where gc_estado_financiero LIKE '%'+@Balance+'%'
and [IdNivelCta] <= @IdNivel AND IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa
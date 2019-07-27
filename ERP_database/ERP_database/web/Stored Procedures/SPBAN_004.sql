
--exec [web].[SPBAN_004] 1,2,13
CREATE proc [web].[SPBAN_004]
(
 @IdEmpresa int
,@IdBanco int
,@IdConciliacion numeric
)
as

declare @IdCtaCble varchar(20)
declare @IdPeriodo int
declare @SaldoInicial float
declare @SaldoFin float
declare @i_FechaIni datetime
declare @i_FechaFin datetime
declare @TotalRegistros as numeric
declare @Beneficiario varchar(1000)
declare @TotalConciliado float
declare @EstadoConciliado varchar(20)
declare @SaldoContable float
declare @w_TEgr float
declare @w_TEgr_ANU float
declare @w_TIng float
declare @w_TIng_ANU float
declare @o_nomBanco varchar(200)
declare @o_ba_Num_Cuenta varchar(200)
declare @TotalConciliadoContable float
declare @TotalConciliadoNoContable float

delete web.ba_SPBAN_004

BEGIN --OBTENGO DATOS PARA EL REPORTE EN CASO DE QUE NO EXISTAN REGISTROS NO CONCILIADOS
BEGIN --OBTENGO SALDO CONTABLE ANTERIOR Y ESTADO DE CONCILIACION

SELECT @IdBanco = IdBanco FROM ba_Conciliacion WHERE IdEmpresa = @IdEmpresa
AND IdConciliacion = @IdConciliacion

SELECT @EstadoConciliado =  C.ca_descripcion,
@SaldoContable = A.co_SaldoBanco_EstCta
FROM ba_Conciliacion A, ba_Catalogo C
where A.IdEmpresa = @IdEmpresa
and A.IdConciliacion = @IdConciliacion 
and A.IdBanco = @IdBanco 
and C.IdCatalogo = A.IdEstado_Concil_Cat
END

BEGIN --OBTENGO NOMBRE DEL BANCO

SELECT @o_nomBanco = A.ba_descripcion, 
        @IdCtaCble= A.IdCtaCble, @o_ba_Num_Cuenta = A.ba_Num_Cuenta
FROM            ba_Banco_Cuenta A
where A.IdEmpresa=@IdEmpresa 
and A.IdBanco=@IdBanco
END

BEGIN --OBTENGO IDPERIODO

select @IdPeriodo= A.IdPeriodo
from ba_Conciliacion A
where A.IdEmpresa=@IdEmpresa
and A.IdConciliacion=@IdConciliacion
END
END

BEGIN --OBTENGO FECHA INICIAL Y FINAL DEL PERIODO

select @i_FechaIni=A.pe_FechaIni ,@i_FechaFin=A.pe_FechaFin
from ct_periodo A
where A.IdEmpresa=@IdEmpresa
and A.IdPeriodo=@IdPeriodo
END

BEGIN --CALCULO SALDO INICIAL

SELECT     @SaldoInicial=isnull( SUM(B.dc_Valor) ,0) 
FROM ct_cbtecble AS A ,ct_cbtecble_det B where 
    A.IdEmpresa = B.IdEmpresa 
AND A.IdTipoCbte = B.IdTipoCbte 
AND A.IdCbteCble = B.IdCbteCble 
and A.IdEmpresa = @IdEmpresa
and B.IdCtaCble=@IdCtaCble
AND A.cb_Fecha <= @i_FechaFin
GROUP BY A.IdEmpresa, B.IdCtaCble
/*
DECLARE @RE_NoSeleccionado float
select @RE_NoSeleccionado = SUM(A.Valor)
from(
select isnull(case when tipo_IngEgr = '-' then Valor*-1 else Valor end,0) Valor
from ba_Conciliacion_det b
where b.IdEmpresa = @IdEmpresa
and b.IdConciliacion = @IdConciliacion
and b.Seleccionado = 0
) A

set @RE_NoSeleccionado = isnull(@RE_NoSeleccionado,0)*/
set @SaldoInicial = round(isnull(@SaldoInicial,0),2) --+ isnull(@RE_NoSeleccionado,0),2)

END

BEGIN --CALCULO EGRESOS NO CONCILIADOS
SELECT @w_TEgr=  ISNULL(A.Valor,0)
from(
SELECT       ISNULL(SUM(D.dc_valor),0) Valor
FROM            ct_cbtecble_det AS D INNER JOIN
                         ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor < 0) 
AND  C.cb_Fecha <= @i_FechaFin
AND C.cb_Estado = 'A'     
    AND C.cb_Observacion NOT LIKE '%**REVERS%' AND SUBSTRING(C.cb_Observacion, 1, 2) != '**' 
    AND ISNULL(D.dc_para_conciliar,0) = 1
    AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE ba_Conciliacion.IdEmpresa = @IdEmpresa and
            ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia and ba_Conciliacion_det_IngEgr.checked = 0
            and ba_Conciliacion_det_IngEgr.IdConciliacion = @IdConciliacion)
/*UNION ALL

SELECT ISNULL(SUM(B.Valor)*-1,0) FROM ba_Conciliacion_det B
WHERE B.IdEmpresa = @IdEmpresa
AND B.IdConciliacion  = @IdConciliacion
AND B.tipo_IngEgr = '-' AND B.Seleccionado = 0*/
			)A
END

BEGIN --CALCULO EGRESOS NO CONCILIADOS QUE ESTAN ANULADOS
SELECT       @w_TEgr_ANU=  ISNULL(SUM(D.dc_valor),0)
FROM            ct_cbtecble_det AS D INNER JOIN
                         ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor < 0) 
AND  C.cb_Fecha <= @i_FechaFin
AND C.cb_Estado = 'I'     
    --AND C.cb_Observacion NOT LIKE '%**REVERS%' AND SUBSTRING(C.cb_Observacion, 1, 2) != '**' 
    AND ISNULL(D.dc_para_conciliar,0) = 1
    AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE ba_Conciliacion.IdEmpresa = @IdEmpresa and
            ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia and ba_Conciliacion_det_IngEgr.checked = 0
			and ba_Conciliacion_det_IngEgr.IdConciliacion = @IdConciliacion)
END

BEGIN --CALCULO INGRESOS NO CONCILIADOS
SELECT       @w_TIng = ISNULL(SUM(D.dc_valor),0)
FROM            ct_cbtecble_det AS D INNER JOIN
                         ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor > 0) 
AND C.cb_Fecha <= @i_FechaFin AND C.cb_Estado = 'A' 
AND C.cb_Observacion NOT LIKE '%**REVERS%' AND SUBSTRING(C.cb_Observacion, 1, 2) != '**'
AND ISNULL(D.dc_para_conciliar,0) = 1
AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE ba_Conciliacion.IdEmpresa = @IdEmpresa and
            ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia and ba_Conciliacion_det_IngEgr.checked = 0
            and ba_Conciliacion_det_IngEgr.IdConciliacion = @IdConciliacion)
END

BEGIN --CALCULO INGRESOS NO CONCILIADOS QUE PUEDEN ESTAR ANULADOS
SELECT       @w_TIng_ANU = ISNULL(SUM(D.dc_valor),0)
FROM            ct_cbtecble_det AS D INNER JOIN
                         ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor > 0) 
AND C.cb_Fecha <= @i_FechaFin AND C.cb_Estado = 'I' 
AND ISNULL(D.dc_para_conciliar,0) = 1
AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE ba_Conciliacion.IdEmpresa = @IdEmpresa and
            ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia and ba_Conciliacion_det_IngEgr.checked = 0
            and ba_Conciliacion_det_IngEgr.IdConciliacion = @IdConciliacion)
            and exists(
                SELECT        rev.IdEmpresa, rev.IdTipoCbte, rev.IdCbteCble, ct_cbtecble.cb_Fecha
                FROM            ct_cbtecble_Reversado AS rev INNER JOIN
                            ct_cbtecble ON rev.IdEmpresa_Anu = ct_cbtecble.IdEmpresa AND rev.IdTipoCbte_Anu = ct_cbtecble.IdTipoCbte AND rev.IdCbteCble_Anu = ct_cbtecble.IdCbteCble
                where ct_cbtecble.cb_Fecha <= @i_FechaIni
                and rev.IdEmpresa = c.IdEmpresa
                and rev.IdTipoCbte = c.IdTipoCbte
                and rev.IdCbteCble = c.IdCbteCble
                )
				/*
select @RE_NoSeleccionado = SUM(A.Valor)
from(
select isnull(case when tipo_IngEgr = '-' then Valor*-1 else Valor end,0) Valor
from ba_Conciliacion_det b
where b.IdEmpresa = @IdEmpresa
and b.IdConciliacion = @IdConciliacion
and b.Seleccionado = 1
) A
set @RE_NoSeleccionado = isnull(@RE_NoSeleccionado,0)
*/
END

set @SaldoFin=ISNULL(@w_TIng+@w_TEgr_ANU+@w_TIng_ANU+@w_TEgr/*+@RE_NoSeleccionado*/,0)
set @SaldoInicial=ISNULL(@SaldoInicial,0)

BEGIN --INSERTO INGRESOS NO CONCILIADOS 
INSERT INTO web.ba_SPBAN_004
           ([IdEmpresa]
           ,[IdConciliacion]
           ,[IdTipoCbte]
           ,[IdCbteCble]
           ,[secuencia])

SELECT       @IdEmpresa,@IdConciliacion,C.IdTipoCbte,C.IdCbteCble,D.secuencia
FROM            ct_cbtecble_det AS D INNER JOIN
ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor > 0) 
AND  C.cb_Fecha <= @i_FechaFin AND C.cb_Estado = 'A' 
AND C.cb_Observacion NOT LIKE '%**REVERS%' AND SUBSTRING(C.cb_Observacion, 1, 2) != '**'
AND ISNULL(D.dc_para_conciliar,0) = 1
AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE             ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia AND ba_Conciliacion.IdEmpresa = @IdEmpresa and ba_Conciliacion_det_IngEgr.checked = 0
            and ba_Conciliacion_det_IngEgr.IdConciliacion = @IdConciliacion)        
END

BEGIN--INSERTO INGRESOS ANULADOS QUE PUEDEN ESTAR EN ESTA CONCILIACION
INSERT INTO web.ba_SPBAN_004
           ([IdEmpresa]
           ,[IdConciliacion]
           ,[IdTipoCbte]
           ,[IdCbteCble]
           ,[secuencia])

SELECT       @IdEmpresa,@IdConciliacion,C.IdTipoCbte,C.IdCbteCble,D.secuencia
FROM            ct_cbtecble_det AS D INNER JOIN
ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor > 0) 
AND  C.cb_Fecha <= @i_FechaFin AND C.cb_Estado = 'I' 
AND ISNULL(D.dc_para_conciliar,0) = 1
AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE             ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia AND ba_Conciliacion.IdEmpresa = @IdEmpresa and ba_Conciliacion_det_IngEgr.checked = 0)
            and exists(
                SELECT        rev.IdEmpresa, rev.IdTipoCbte, rev.IdCbteCble, ct_cbtecble.cb_Fecha
                FROM            ct_cbtecble_Reversado AS rev INNER JOIN
                            ct_cbtecble ON rev.IdEmpresa_Anu = ct_cbtecble.IdEmpresa AND rev.IdTipoCbte_Anu = ct_cbtecble.IdTipoCbte AND rev.IdCbteCble_Anu = ct_cbtecble.IdCbteCble
                where ct_cbtecble.cb_Fecha <= @i_FechaIni
                and rev.IdEmpresa = c.IdEmpresa
                and rev.IdTipoCbte = c.IdTipoCbte
                and rev.IdCbteCble = c.IdCbteCble
                )
END

BEGIN --INSERTO EGRESOS NO CONCILIADOS
INSERT INTO web.ba_SPBAN_004
           ([IdEmpresa]
           ,[IdConciliacion]
           ,[IdTipoCbte]
           ,[IdCbteCble]
           ,[secuencia])

SELECT       @IdEmpresa,@IdConciliacion,C.IdTipoCbte,C.IdCbteCble,D.secuencia
FROM            ct_cbtecble_det AS D INNER JOIN
ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor < 0) 
AND C.cb_Fecha <= @i_FechaFin AND C.cb_Estado = 'A' 
AND C.cb_Observacion NOT LIKE '%**REVERS%' AND SUBSTRING(C.cb_Observacion, 1, 2) != '**'
AND ISNULL(D.dc_para_conciliar,0) = 1
AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia and ba_Conciliacion.IdEmpresa = @IdEmpresa and ba_Conciliacion_det_IngEgr.checked = 0
            and ba_Conciliacion_det_IngEgr.IdConciliacion = @IdConciliacion)
            
END


BEGIN --INSERTO EGRESOS NO CONCILIADOS QUE ESTEN ANULADOS
INSERT INTO web.ba_SPBAN_004
           ([IdEmpresa]
           ,[IdConciliacion]
           ,[IdTipoCbte]
           ,[IdCbteCble]
           ,[secuencia])

SELECT       @IdEmpresa,@IdConciliacion,C.IdTipoCbte,C.IdCbteCble,D.secuencia
FROM            ct_cbtecble_det AS D INNER JOIN
ct_cbtecble AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND D.IdCbteCble = C.IdCbteCble
WHERE        (C.IdEmpresa = @IdEmpresa) AND (D.IdCtaCble = @IdCtaCble) AND (D.dc_Valor < 0) 
AND C.cb_Fecha <= @i_FechaFin AND C.cb_Estado = 'I' 
AND ISNULL(D.dc_para_conciliar,0) = 1
AND EXISTS
            (
            SELECT   ba_Conciliacion.IdEmpresa
            FROM            ba_Conciliacion_det_IngEgr INNER JOIN
            ba_Conciliacion ON ba_Conciliacion_det_IngEgr.IdEmpresa = ba_Conciliacion.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdConciliacion = ba_Conciliacion.IdConciliacion INNER JOIN
            ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
            WHERE ba_Conciliacion.IdPeriodo <= @IdPeriodo and ba_Conciliacion_det_IngEgr.IdEmpresa = D.IdEmpresa AND 
            ba_Conciliacion_det_IngEgr.IdTipocbte = D.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = D.IdCbteCble AND 
            ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = D.secuencia and ba_Conciliacion.IdEmpresa = @IdEmpresa and ba_Conciliacion_det_IngEgr.checked = 0
			and ba_Conciliacion_det_IngEgr.IdConciliacion = @IdConciliacion)
            and exists(
                SELECT        rev.IdEmpresa, rev.IdTipoCbte, rev.IdCbteCble, ct_cbtecble.cb_Fecha
                FROM            ct_cbtecble_Reversado AS rev INNER JOIN
                            ct_cbtecble ON rev.IdEmpresa_Anu = ct_cbtecble.IdEmpresa AND rev.IdTipoCbte_Anu = ct_cbtecble.IdTipoCbte AND rev.IdCbteCble_Anu = ct_cbtecble.IdCbteCble
                where ct_cbtecble.cb_Fecha <= @i_FechaIni
                and rev.IdEmpresa = c.IdEmpresa
                and rev.IdTipoCbte = c.IdTipoCbte
                and rev.IdCbteCble = c.IdCbteCble
                )
END

BEGIN --VALIDO QUE EXISTAN REGISTROS NO CONCILIADOS PARA MOSTRAR EN REPORTE
SELECT  @TotalRegistros= isnull(count(*),0) FROM    web.ba_SPBAN_004 where IdEmpresa = @IdEmpresa
END

BEGIN --CALCULO EGRESOS CONCILIADOS
SELECT     @w_TEgr=   ISNULL(SUM(ct_cbtecble_det.dc_Valor) ,0)
FROM            ba_Conciliacion AS A INNER JOIN
                         ba_Conciliacion_det_IngEgr ON A.IdEmpresa = ba_Conciliacion_det_IngEgr.IdEmpresa AND A.IdConciliacion = ba_Conciliacion_det_IngEgr.IdConciliacion INNER JOIN
                         ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
                         ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
                         ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
WHERE        (A.IdEmpresa = @IdEmpresa) AND (A.IdConciliacion = @IdConciliacion) AND (A.IdBanco = @IdBanco) AND (ba_Conciliacion_det_IngEgr.checked = 1)  AND (ct_cbtecble_det.dc_Valor < 0)
END

BEGIN --CALCULO INGRESOS CONCILIADOS
SELECT     @w_TIng=   ISNULL(SUM(ct_cbtecble_det.dc_Valor) ,0)
FROM            ba_Conciliacion AS A INNER JOIN
                         ba_Conciliacion_det_IngEgr ON A.IdEmpresa = ba_Conciliacion_det_IngEgr.IdEmpresa AND A.IdConciliacion = ba_Conciliacion_det_IngEgr.IdConciliacion INNER JOIN
                         ct_cbtecble_det ON ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
                         ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
                         ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia
WHERE        (A.IdEmpresa = @IdEmpresa) AND (A.IdConciliacion = @IdConciliacion) AND (A.IdBanco = @IdBanco) AND (ba_Conciliacion_det_IngEgr.checked = 1)  AND (ct_cbtecble_det.dc_Valor > 0)
END

set @TotalConciliado = ISNULL(@w_TIng,0)  + ISNULL(@w_TEgr,0)
select @TotalConciliadoNoContable = sum(CASE WHEN tipo_IngEgr = '+' THEN valor ELSE valor *-1 END) from ba_conciliacion_det where idempresa = @idempresa and idconciliacion = @idconciliacion and Seleccionado = 1

if (@TotalRegistros>0)
begin
SELECT        A.IdEmpresa, A.IdConciliacion, A.IdBanco, A.IdPeriodo, dbo.ba_Banco_Cuenta.ba_descripcion AS nom_banco, dbo.ba_Banco_Cuenta.ba_Num_Cuenta, 
                         dbo.ba_Banco_Cuenta.IdCtaCble, CAST(dbo.ba_Cbte_Ban.cb_Fecha AS date) AS Fecha, dbo.ct_cbtecble_tipo.CodTipoCbte, 
                         dbo.ct_cbtecble_tipo.tc_TipoCbte AS Tipo_Cbte,web.ba_SPBAN_004.IdCbteCble, web.ba_SPBAN_004.IdTipoCbte,web.ba_SPBAN_004.secuencia AS SecuenciaCbte, 
                         isnull(dbo.ct_cbtecble_det.dc_Valor,0) AS Valor, dbo.ct_cbtecble_det.dc_Observacion AS Observacion, 
                         dbo.ba_Cbte_Ban.cb_Cheque AS Cheque, ISNULL(@SaldoInicial, 0) AS SaldoInicial, ISNULL(@SaldoFin, 0) AS SaldoFinal, RTRIM(dbo.ct_cbtecble_tipo.tc_TipoCbte) 
                         + 'S GIRADOS Y NO COBRADOS' AS Titulo_grupo, CASE WHEN ISNULL(ba_Cbte_Ban.cb_Cheque, '') <> '' THEN rtrim(ct_cbtecble_tipo.CodTipoCbte) 
                         + '#:' + ba_Cbte_Ban.cb_Cheque + ' cbte:' + rtrim(CAST(web.ba_SPBAN_004.IdCbteCble AS varchar(20))) ELSE rtrim(ct_cbtecble_tipo.CodTipoCbte) 
                         + '#: ' + rtrim(CAST(web.ba_SPBAN_004.IdCbteCble AS varchar(20))) END AS referencia, dbo.tb_empresa.em_ruc AS ruc_empresa, 
                         dbo.tb_empresa.em_nombre AS nom_empresa, A.co_SaldoBanco_EstCta AS SaldoBanco_EstCta, A.IdEstado_Concil_Cat AS Estado_Conciliacion, 
                         case when dbo.ba_Cbte_Ban.Estado = 'I' THEN '**ANULADO** ' ELSE '' END +
						 CASE WHEN dbo.ba_Cbte_Ban.cb_giradoA IS NULL THEN dbo.ba_Cbte_Ban.cb_Observacion ELSE 
                         dbo.ba_Cbte_Ban.cb_giradoA END AS GiradoA,
						 
						  dbo.ba_TipoFlujo.IdTipoFlujo, dbo.ba_TipoFlujo.Descricion AS nom_tipo_flujo, ISNULL(@TotalConciliado, 0) 
                         AS Total_Conciliado, @i_FechaIni AS FechaIni, @i_FechaFin AS FechaFin, isnull(@TotalConciliadoNoContable,0) TotalConciliadoNoContable, a.co_SaldoBanco_anterior
FROM            ba_TipoFlujo RIGHT OUTER JOIN
                         web.ba_SPBAN_004 INNER JOIN
                         ba_Conciliacion AS A INNER JOIN
                         ba_Banco_Cuenta ON A.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND A.IdBanco = ba_Banco_Cuenta.IdBanco INNER JOIN
                         tb_empresa ON A.IdEmpresa = tb_empresa.IdEmpresa ON web.ba_SPBAN_004.IdConciliacion = A.IdConciliacion AND web.ba_SPBAN_004.IdEmpresa = A.IdEmpresa LEFT OUTER JOIN
                         ba_Cbte_Ban ON web.ba_SPBAN_004.IdEmpresa = ba_Cbte_Ban.IdEmpresa AND web.ba_SPBAN_004.IdTipoCbte = ba_Cbte_Ban.IdTipocbte AND web.ba_SPBAN_004.IdCbteCble = ba_Cbte_Ban.IdCbteCble LEFT OUTER JOIN
                         ct_cbtecble_tipo INNER JOIN
                         ct_cbtecble_det ON ct_cbtecble_tipo.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble_tipo.IdEmpresa = ct_cbtecble_det.IdEmpresa ON web.ba_SPBAN_004.IdEmpresa = ct_cbtecble_det.IdEmpresa AND 
                         web.ba_SPBAN_004.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND web.ba_SPBAN_004.IdCbteCble = ct_cbtecble_det.IdCbteCble AND web.ba_SPBAN_004.secuencia = ct_cbtecble_det.secuencia ON 
                         ba_TipoFlujo.IdEmpresa = ba_Cbte_Ban.IdEmpresa AND ba_TipoFlujo.IdTipoFlujo = ba_Cbte_Ban.IdTipoFlujo
where web.ba_SPBAN_004.IdEmpresa = @IdEmpresa AND isnull(ct_cbtecble_det.dc_para_conciliar,0) = 1
UNION ALL
SELECT d.IdEmpresa,d.IdConciliacion,c.IdBanco,c.IdPeriodo, b.ba_descripcion, b.ba_Num_Cuenta, b.IdCtaCble, d.Fecha, t.CodTipoCbteBan, ti.tc_TipoCbte, d.Secuencia, d.IdTipocbte, d.Secuencia, case WHEN d.tipo_IngEgr = '+' THEN d.Valor ELSE d.Valor *-1 END Valor,d.Observacion, d.Referencia,@SaldoInicial, @SaldoFin,
'REGISTROS ADICIONALES ', d.Referencia, e.em_ruc, e.em_nombre, c.co_SaldoBanco_EstCta, c.IdEstado_Concil_Cat, d.Observacion,null,null,@TotalConciliado,@i_FechaIni AS FechaIni, @i_FechaFin AS FechaFin, isnull(@TotalConciliadoNoContable,0) TotalConciliadoNoContable, c.co_SaldoBanco_anterior
FROM ba_Conciliacion as c inner join 
ba_Conciliacion_det as d on c.IdEmpresa = d.IdEmpresa and c.IdConciliacion = d.IdConciliacion inner join
ba_Banco_Cuenta as b on b.IdEmpresa = c.IdEmpresa and b.IdBanco = c.IdBanco inner join 
ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo as t on t.IdEmpresa = d.IdEmpresa and t.IdTipoCbteCble = d.IdTipoCbte inner join
ct_cbtecble_tipo as ti on ti.IdEmpresa = t.IdEmpresa and ti.IdTipoCbte = t.IdTipoCbteCble inner join
tb_empresa as e on e.IdEmpresa = c.IdEmpresa
where c.IdEmpresa = @IdEmpresa and c.IdConciliacion = @IdConciliacion and d.Seleccionado = 1

end 
else
begin

            SELECT                
            A.IdEmpresa                    ,cast(@IdConciliacion as numeric) IdConciliacion    ,@IdBanco IdBanco        ,@IdPeriodo IdPeriodo        ,@o_nomBanco nom_banco        ,@o_ba_Num_Cuenta ba_Num_Cuenta    
            ,@IdCtaCble IdCtaCble        ,cast(@i_FechaIni as date ) Fecha                ,'' CodTipoCbte         ,'NO HAY REGISTRO' Tipo_Cbte                ,0 Secuencia        ,cast(0  as numeric) as IdCbteCble  
            ,0 IdTipocbte                ,0 SecuenciaCbte                ,cast(0 as float) Valor                ,'NO HAY REGISTRO' Observacion                ,''    Cheque    
            ,ISNULL(@SaldoInicial,0) SaldoInicial    ,ISNULL(@SaldoFin,0) SaldoFinal            ,''Titulo_grupo            ,'NO HAY REGISTRO' referencia                ,A.em_ruc ruc_empresa    ,A.em_nombre nom_empresa
            ,ISNULL(@SaldoContable,0) SaldoBanco_EstCta        ,@EstadoConciliado Estado_Conciliacion ,'' GiradoA
            ,null IdTipoFlujo            ,null AS nom_tipo_flujo            , ISNULL(@TotalConciliado,0) Total_Conciliado
            ,cast(@i_FechaIni as date) as FechaIni,cast(@i_FechaFin as date )as FechaFin
            from tb_empresa A
            where A.IdEmpresa=@IdEmpresa 
end

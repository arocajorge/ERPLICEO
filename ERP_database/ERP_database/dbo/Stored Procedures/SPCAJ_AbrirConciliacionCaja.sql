--EXEC [dbo].[SPCAJ_AbrirConciliacionCaja] 1, 76
CREATE PROCEDURE [dbo].[SPCAJ_AbrirConciliacionCaja]
(
@IdEmpresa int,
@IdConciliacion numeric
)
AS
PRINT 'INSERTO PK DE OP'
BEGIN -- INSERTO PK DE OP

	CREATE TABLE xxx_EliminarOP_caja
	(
	IdEmpresa int,
	IdOrdenPago int
	)

	INSERT INTO xxx_EliminarOP_caja
	SELECT IdEmpresa_OP, IdOrdenPago_OP 
	FROM cp_conciliacion_Caja_det 
	WHERE IdEmpresa = @IdEmpresa AND IdConciliacion_Caja = @IdConciliacion
END

PRINT 'INSERTO PK DE NC'
BEGIN --INSERTO PK DE NC
	CREATE TABLE xxx_EliminarNC
	(
	IdEmpresa int,
	IdTipoCbte int,
	IdCbteCble numeric(18,0)
	)

	insert into xxx_EliminarNC
	SELECT IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago 
	FROM cp_orden_pago_cancelaciones
		WHERE EXISTS(
		SELECT * FROM xxx_EliminarOP_caja F
		WHERE cp_orden_pago_cancelaciones.IdEmpresa_op = F.IdEmpresa
		AND cp_orden_pago_cancelaciones.IdOrdenPago_op = F.IdOrdenPago
		)
END
PRINT 'ACTUALIZO CAMPOS DE OP EN DETALLE'
BEGIN -- ACTUALIZO CAMPOS DE OP EN DETALLE
	UPDATE cp_conciliacion_Caja_det SET IdEmpresa_OP = NULL, IdOrdenPago_OP = NULL
	WHERE IdEmpresa = @IdEmpresa AND IdConciliacion_Caja = @IdConciliacion
END

PRINT 'ELIMINO CANCELACIONES Y OP'
BEGIN -- ELIMINO CANCELACIONES Y OP
	DELETE cp_orden_pago_cancelaciones
	WHERE EXISTS(
	SELECT * FROM xxx_EliminarOP_caja F
	WHERE cp_orden_pago_cancelaciones.IdEmpresa_op = F.IdEmpresa
	AND cp_orden_pago_cancelaciones.IdOrdenPago_op = F.IdOrdenPago
	)

	DELETE cp_orden_pago_det
	WHERE EXISTS(
	SELECT * FROM xxx_EliminarOP_caja F
	WHERE cp_orden_pago_det.IdEmpresa = F.IdEmpresa
	AND cp_orden_pago_det.IdOrdenPago = F.IdOrdenPago
	)

	DELETE cp_orden_pago
	WHERE EXISTS(
	SELECT * FROM xxx_EliminarOP_caja F
	WHERE cp_orden_pago.IdEmpresa = F.IdEmpresa
	AND cp_orden_pago.IdOrdenPago = F.IdOrdenPago
	)
END

PRINT 'ELIMINO NC'
BEGIN -- ELIMINO NC
	DELETE cp_nota_DebCre 
	WHERE EXISTS(
	SELECT * FROM xxx_EliminarNC f
	where cp_nota_DebCre.IdEmpresa = f.IdEmpresa
	and cp_nota_DebCre.IdTipoCbte_Nota = f.IdTipoCbte
	and cp_nota_DebCre.IdCbteCble_Nota = f.IdCbteCble
	)

	DELETE ct_cbtecble_det
	WHERE EXISTS(
	SELECT * FROM xxx_EliminarNC f
	where ct_cbtecble_det.IdEmpresa = f.IdEmpresa
	and ct_cbtecble_det.IdTipoCbte = f.IdTipoCbte
	and ct_cbtecble_det.IdCbteCble = f.IdCbteCble
	)

	DELETE ct_cbtecble
	WHERE EXISTS(
	SELECT * FROM xxx_EliminarNC f
	where ct_cbtecble.IdEmpresa = f.IdEmpresa
	and ct_cbtecble.IdTipoCbte = f.IdTipoCbte
	and ct_cbtecble.IdCbteCble = f.IdCbteCble
	)
END

PRINT 'OBTENGO ID DE INGRESO DE CAJA Y DE OP'
BEGIN -- OBTENGO ID DE INGRESO DE CAJA Y DE OP
	DECLARE @IdOrdenPago numeric
	DECLARE @IdTipoCbte int
	DECLARE @IdCbteCble numeric(18,0)

	SELECT @IdOrdenPago = IdOrdenPago_op, 
	@IdTipoCbte = IdTipoCbte_mov_caj, 
	@IdCbteCble = IdCbteCble_mov_caj
	FROM cp_conciliacion_Caja 
	WHERE IdEmpresa = @IdEmpresa and IdConciliacion_Caja = @IdConciliacion

	UPDATE cp_conciliacion_Caja 
	SET IdEmpresa_op = NULL, IdOrdenPago_op = null, 
	IdEmpresa_mov_caj = null, IdTipoCbte_mov_caj = null, IdCbteCble_mov_caj = null,
	IdEstadoCierre = 'EST_CIE_ABI'
	WHERE IdEmpresa = @IdEmpresa and IdConciliacion_Caja = @IdConciliacion

	DELETE cp_conciliacion_Caja_ValesNoConciliados
	WHERE IdEmpresa = @IdEmpresa and IdConciliacion_Caja = @IdConciliacion

	DELETE [dbo].[cp_conciliacion_Caja_det_Ing_Caja]
	WHERE IdEmpresa = @IdEmpresa and IdConciliacion_Caja = @IdConciliacion
END

PRINT 'OBTENGO PK DE DIARIO DE OP'
BEGIN -- OBTENGO PK DE DIARIO DE OP
	DECLARE @IdTipoCbte_op int
	DECLARE @IdCbteCble_op numeric

	select @IdTipoCbte_op = IdTipoCbte_cxp, 
	@IdCbteCble_op = IdCbteCble_cxp 
	from cp_orden_pago_det
	where IdEmpresa = @IdEmpresa and IdOrdenPago = @IdOrdenPago
END

PRINT 'ELIMINO REPOSICION DE CAJA Y OP'
BEGIN -- ELIMINO REPOSICION DE CAJA Y OP
	DELETE cp_orden_pago_det where IdEmpresa = @IdEmpresa and IdOrdenPago = @IdOrdenPago
	DELETE cp_orden_pago where IdEmpresa = @IdEmpresa and IdOrdenPago = @IdOrdenPago
	DELETE ct_cbtecble_det where IdEmpresa = @IdEmpresa and IdTipoCbte = @IdTipoCbte_op and IdCbteCble = @IdCbteCble_op
	DELETE ct_cbtecble where IdEmpresa = @IdEmpresa and IdTipoCbte = @IdTipoCbte_op and IdCbteCble = @IdCbteCble_op

	DELETE caj_Caja_Movimiento_det where IdEmpresa = @IdEmpresa and IdTipocbte = @IdTipoCbte and IdCbteCble = @IdCbteCble
	DELETE caj_Caja_Movimiento where IdEmpresa = @IdEmpresa and IdTipocbte = @IdTipoCbte and IdCbteCble = @IdCbteCble
	DELETE ct_cbtecble_det where IdEmpresa = @IdEmpresa and IdTipocbte = @IdTipoCbte and IdCbteCble = @IdCbteCble
	DELETE ct_cbtecble where IdEmpresa = @IdEmpresa and IdTipocbte = @IdTipoCbte and IdCbteCble = @IdCbteCble

END

DROP TABLE xxx_EliminarOP_caja
DROP TABLE xxx_EliminarNC
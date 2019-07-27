
CREATE PROCEDURE spsys_fa_reversar_anulacion_factura
(
@IdEmpresa int, @IdSucursal int, @IdBodega int, @IdCbteVta numeric
)
AS
DECLARE @i_Contador int, @i_IdEmpresa_ct int, @i_IdTipoCbte_ct int, @i_IdCbteCble_ct numeric, @i_TieneDiario bit,
@i_IdEmpresa_anu int, @i_IdTipoCbte_anu int, @i_IdCbteCble_anu numeric,
@i_IdEmpresa_inv int, @i_IdSucursal_inv int, @i_IdBodega_inv int, @i_IdMovi_inven_tipo_inv int, @i_IdNumMovi numeric, @i_TieneMovi bit

select @i_Contador = COUNT(*) from fa_factura where IdEmpresa = @IdEmpresa and IdSucursal = @IdSucursal and IdBodega = @IdBodega and IdCbteVta = @IdCbteVta and Estado = 'I'
SET @i_Contador = isnull(@i_contador,0)
IF(@i_Contador = 0)
BEGIN
	SELECT 'LA FACTURA INGRESADA NO SE ENCUENTRA ANULADA'
	RETURN
END

SELECT @i_IdEmpresa_ct = ct_IdEmpresa, @i_IdTipoCbte_ct = ct_IdTipoCbte, @i_IdCbteCble_ct = ct_IdCbteCble 
FROM fa_factura_x_ct_cbtecble where vt_IdEmpresa = @IdEmpresa and vt_IdSucursal = @IdSucursal and vt_IdBodega = @IdBodega and vt_IdCbteVta = @IdCbteVta

set @i_IdEmpresa_ct = isnull(@i_IdEmpresa_ct,0)
IF(@i_IdEmpresa_ct = 0)
BEGIN
	SELECT 'LA FACTURA NO TIENE DIARIO CONTABLE RELACIONADO'
END
ELSE
	SET @i_TieneDiario = 1

IF(@i_TieneDiario = 1)
BEGIN
	SELECT @i_IdEmpresa_anu = IdEmpresa_Anu, @i_IdTipoCbte_anu = IdTipoCbte_Anu, @i_IdCbteCble_anu = IdCbteCble_Anu 
	FROM ct_cbtecble_Reversado WHERE IdEmpresa = @i_IdEmpresa_ct and IdTipoCbte = @i_IdTipoCbte_ct and IdCbteCble = @i_IdCbteCble_ct

	DELETE ct_cbtecble_Reversado where IdEmpresa_Anu = @i_IdEmpresa_anu and IdTipoCbte_Anu = @i_IdTipoCbte_anu and IdCbteCble_Anu = @i_IdCbteCble_anu
	DELETE ct_cbtecble_det where IdEmpresa = @i_IdEmpresa_anu and IdTipoCbte = @i_IdTipoCbte_anu and IdCbteCble = @i_IdCbteCble_anu
	DELETE ct_cbtecble where IdEmpresa = @i_IdEmpresa_anu and IdTipoCbte = @i_IdTipoCbte_anu and IdCbteCble = @i_IdCbteCble_anu
	UPDATE ct_cbtecble set cb_Estado = 'A' where IdEmpresa = @i_IdEmpresa_ct AND IdTipoCbte = @i_IdTipoCbte_ct AND IdCbteCble = @i_IdCbteCble_ct
END

SELECT @i_IdEmpresa_inv = IdEmpresa_in_eg_x_inv, @i_IdSucursal_inv = IdSucursal_in_eg_x_inv, @i_IdMovi_inven_tipo_inv = IdMovi_inven_tipo_in_eg_x_inv, @i_IdNumMovi = IdNumMovi_in_eg_x_inv
FROM fa_factura_x_in_Ing_Egr_Inven where IdEmpresa_fa = @IdEmpresa and IdSucursal_fa = @IdSucursal and IdBodega_fa = @IdBodega and IdCbteVta_fa = @IdCbteVta

SET @i_IdEmpresa_inv = ISNULL(@i_IdEmpresa_inv,0)
IF(@i_IdEmpresa_inv = 0)
BEGIN
	SELECT 'LA FACTURA NO TIENE MOVIMIENTO DE INVENTARIO RELACIONADO'
END
ELSE
	SET @i_TieneMovi = 1

IF(@i_TieneMovi = 1)
BEGIN
	UPDATE in_Ing_Egr_Inven SET Estado = 'A' WHERE IdEmpresa = @i_IdEmpresa_inv AND IdSucursal = @i_IdSucursal_inv AND IdMovi_inven_tipo = @i_IdMovi_inven_tipo_inv AND IdNumMovi = @i_IdNumMovi
	EXEC spINV_aprobacion_ing_egr @i_IdEmpresa_inv, @i_IdSucursal_inv, @i_IdBodega_inv, @i_IdMovi_inven_tipo_inv, @i_IdNumMovi 
END

update fa_factura SET Estado = 'A' WHERE IdEmpresa = @IdEmpresa and IdSucursal = @IdSucursal and IdBodega = @IdBodega and IdCbteVta = @IdCbteVta
CREATE PROCEDURE SPFAC_EliminarCobroEfectivo
(
@IdEmpresa int, @IdSucursal int, @IdBodega int, @IdCbteVta numeric
)
AS
DECLARE @IdCobro numeric
select @IdCobro = IdCobro from fa_factura_x_cxc_cobro where IdEmpresa = @IdEmpresa
and IdSucursal = @IdSucursal and IdBodega = @IdBodega and IdCbteVta = @IdCbteVta

DECLARE @IdTipoCbte int, @IdCbteCble numeric

SELECT @IdTipoCbte = ct_IdTipoCbte, @IdCbteCble = ct_IdCbteCble 
FROM cxc_cobro_x_ct_cbtecble
where cbr_IdEmpresa = @IdEmpresa
and cbr_IdSucursal = @IdSucursal
and cbr_IdCobro = @IdCobro

DELETE fa_factura_x_cxc_cobro where IdEmpresa = @IdEmpresa and IdSucursal = @IdSucursal and IdBodega = @IdBodega and IdCbteVta = @IdCbteVta
DELETE cxc_cobro_x_ct_cbtecble where cbr_IdEmpresa = @IdEmpresa and cbr_IdSucursal = @IdSucursal and cbr_IdCobro = @IdCobro 
DELETE caj_Caja_Movimiento_det where IdEmpresa = @IdEmpresa and IdTipoCbte = @IdTipoCbte AND IdCbteCble = @IdCbteCble
DELETE caj_Caja_Movimiento where IdEmpresa = @IdEmpresa and IdTipoCbte = @IdTipoCbte AND IdCbteCble = @IdCbteCble
DELETE ct_cbtecble_det where IdEmpresa = @IdEmpresa and IdTipoCbte = @IdTipoCbte AND IdCbteCble = @IdCbteCble
DELETE ct_cbtecble where IdEmpresa = @IdEmpresa and IdTipoCbte = @IdTipoCbte AND IdCbteCble = @IdCbteCble
DELETE cxc_cobro_det where IdEmpresa = @IdEmpresa and IdSucursal = @IdSucursal and IdCobro = @IdCobro
DELETE cxc_cobro where IdEmpresa = @IdEmpresa and IdSucursal = @IdSucursal and IdCobro = @IdCobro
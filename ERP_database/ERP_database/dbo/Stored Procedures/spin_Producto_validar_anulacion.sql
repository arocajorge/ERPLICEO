CREATE PROCEDURE spin_Producto_validar_anulacion
(
@IdEmpresa int,
@IdProducto decimal
)
AS
DECLARE @cont int
--VALIDO DETALLE FACTURA
SELECT @cont = count(*) FROM fa_factura_det where IdEmpresa = @IdEmpresa and IdProducto = @IdProducto
IF(@cont > 0)
	SELECT 'PRODUCTO CON ID# '+CAST(@IdProducto as varchar(20))+' UTILIZADO EN FACTURA, NO SE PUEDE ANULAR'
ELSE
BEGIN --VALIDO DETALLE DE MOVI INVEN
	SELECT @cont = count(*) FROM in_movi_inve_detalle where IdEmpresa = @IdEmpresa and IdProducto = @IdProducto
	IF(@cont > 0)
		SELECT 'PRODUCTO CON ID# '+CAST(@IdProducto as varchar(20))+' UTILIZADO EN MOVIMIENTO DE INVENTARIO, NO SE PUEDE ANULAR'
		ELSE
			BEGIN --VALIDO DETALLE DE ING EGR
				SELECT @cont = count(*) FROM in_Ing_Egr_Inven_det where IdEmpresa = @IdEmpresa and IdProducto = @IdProducto
				IF(@cont > 0)
					SELECT 'PRODUCTO CON ID# '+CAST(@IdProducto as varchar(20))+' UTILIZADO EN MOVIMIENTO DE INVENTARIO, NO SE PUEDE ANULAR'
					ELSE
						BEGIN --VALIDO PROFORMA
							SELECT @cont = count(*) FROM fa_proforma_det where IdEmpresa = @IdEmpresa and IdProducto = @IdProducto
							IF(@cont > 0)
								SELECT 'PRODUCTO CON ID# '+CAST(@IdProducto as varchar(20))+' UTILIZADO EN PROFORMA, NO SE PUEDE ANULAR'
							ELSE
							BEGIN --VALIDO QUE LOS LOTES NO EXISTAN EN ING EGR
								SELECT @cont = COUNT(*) FROM in_Ing_Egr_Inven_det F
								WHERE EXISTS(
								SELECT * FROM in_Producto P WHERE P.IdEmpresa = @IdEmpresa and P.IdProducto_padre = @IdProducto
								AND F.IdEmpresa = P.IdEmpresa AND F.IdProducto = P.IdProducto
								)
								IF(@cont > 0)
								SELECT 'PRODUCTO CON ID# '+CAST(@IdProducto as varchar(20))+' TIENE PRODUCTOS LOTE QUE SON UTILIZADOS EN MOVIMIENTOS DE INVENTARIO, NO SE PUEDE ANULAR'
								ELSE
									BEGIN --VALIDO QUE LOS LOTEN NO EXISTAN EN MOVI_INVEN
										SELECT @cont = COUNT(*) FROM in_movi_inve_detalle F
										WHERE EXISTS(
										SELECT * FROM in_Producto P WHERE P.IdEmpresa = @IdEmpresa and P.IdProducto_padre = @IdProducto
										AND F.IdEmpresa = P.IdEmpresa AND F.IdProducto = P.IdProducto
										)
										IF(@cont > 0)
										SELECT 'PRODUCTO CON ID# '+CAST(@IdProducto as varchar(20))+' TIENE PRODUCTOS LOTE QUE SON UTILIZADOS EN MOVIMIENTOS DE INVENTARIO, NO SE PUEDE ANULAR'
										ELSE
											BEGIN
												SELECT 'OK'
											END
									END
							END
							
						END
				
			END
END

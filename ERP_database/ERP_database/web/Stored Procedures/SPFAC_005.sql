--exec WEB.SPFAC_005 1,1,99999,1,99999,'2018/08/01','2018/08/31',0,0
CREATE  PROCEDURE [web].[SPFAC_005]
(
@IdEmpresa int,
@IdSucursal_ini int,
@IdSucursal_fin int,
@IdCliente_ini numeric,
@IdCliente_fin numeric,
@Fecha_ini datetime,
@fecha_fin datetime
)
AS

SELECT IdEmpresa, IdSucursal, IdCliente, NomCliente, TipoDocumento, EsExportacion, Su_Descripcion, Su_CodigoEstablecimiento, SUM(SubtotalIVASinDscto) AS SubtotalIVASinDscto, SUM(SubtotalSinIVASinDscto) AS SubtotalSinIVASinDscto, 
                  SUM(SubtotalSinDscto) AS SubtotalSinDscto, SUM(Descuento) AS Descuento, SUM(SubtotalIVAConDscto) AS SubtotalIVAConDscto, SUM(SubtotalSinIVAConDscto) AS SubtotalSinIVAConDscto, SUM(SubtotalConDscto) 
                  AS SubtotalConDscto, SUM(ValorIVA) AS ValorIVA, SUM(Total) AS Total, COUNT(IdCliente) Cantidad
FROM     web.VWFAC_005 AS VWFAC_005_1
where IdEmpresa = @IdEmpresa and IdSucursal between @IdSucursal_ini and @IdSucursal_fin and IdCliente between @IdCliente_ini and @IdCliente_fin and vt_fecha between @Fecha_ini and @fecha_fin
GROUP BY IdEmpresa, IdSucursal, IdCliente, NomCliente, TipoDocumento, EsExportacion, Su_Descripcion, Su_CodigoEstablecimiento
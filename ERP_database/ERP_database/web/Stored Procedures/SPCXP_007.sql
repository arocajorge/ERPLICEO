-- EXEC [web].[SPCXP_007] 1,'23/05/2017','30/05/2017',1
CREATE PROCEDURE [web].[SPCXP_007]
(
@IdEmpresa int,
@Fecha_ini datetime,
@Fecha_fin datetime,
@Mostrar_agrupado bit,
@IdSucursalIni int,
@IdSucursalFin int
)
as
begin
/*
DECLARE @IdTipoCbte INT 
DECLARE @IdCbteCble numeric
SET @IdTipoCbte = 7
set @IdCbteCble = 12

set @IdEmpresa = 1 
set @Fecha_ini = '01/04/2017' 
set @Fecha_fin = '30/04/2017'
set @Mostrar_agrupado = 1
*/
SELECT isnull(ROW_NUMBER() over(order by A.IdEmpresa),0) as IdRow, A.* FROM(
SELECT [IdEmpresa]			,[IdTipoCbte_Ogiro]		,[IdCbteCble_Ogiro]      ,[IdOrden_giro_Tipo]		,[Codigo]				,[Descripcion]
      ,[IdProveedor]		,[pr_nombre]			,[pe_cedulaRuc]			,[serie_fact]			 ,[num_factura]				,[co_FechaFactura]      ,[subtotal_iva]
      ,[subtotal_sin_iva]   ,[valor_iva]			,[NAutorizacion]		,[serie_ret]			 ,[NumRetencion]			,[re_baseRetencion]     ,[re_Porcen_retencion]
      ,[re_valor_retencion] ,[re_Codigo_impuesto]   ,[RIVA_0]				,[RIVA_10]				 ,[RIVA_20]					,[RIVA_30]				,[RIVA_70]      ,[RIVA_100]
      ,[RTF_0]				,[RTF_0_1]				,[RTF_1]				,[RTF_2]				 ,[RTF_8]					,[RTF_10]				,[RTF_100]      ,[Documento]      
	  ,[descripcion_cod_sri],[re_tipoRet]			,[Num_Autorizacion_OG]  ,[IdSucursal]            ,[Su_Descripcion]
  FROM web.VWCXP_007
   where IdEmpresa = @IdEmpresa and co_FechaFactura between @Fecha_ini and @Fecha_fin and @Mostrar_agrupado = 1 and IdSucursal between @IdSucursalIni and @IdSucursalFin
   --and IdTipoCbte_Ogiro = @IdTipoCbte and IdCbteCble_Ogiro = @IdCbteCble
UNION ALL
SELECT [IdEmpresa]			,[IdTipoCbte_Ogiro]		,[IdCbteCble_Ogiro]      ,[IdOrden_giro_Tipo]		,[Codigo]				,[Descripcion]
      ,[IdProveedor]		,[pr_nombre]			,[pe_cedulaRuc]			,[serie_fact]			 ,[num_factura]				,[co_FechaFactura]      ,[subtotal_iva]
      ,[subtotal_sin_iva]   ,[valor_iva]		,[NAutorizacion]		,[serie_ret]			 ,[NumRetencion]			,0 AS [re_baseRetencion]     ,0 AS [re_Porcen_retencion]
      ,0 [re_valor_retencion] ,NULL [re_Codigo_impuesto]   ,SUM([RIVA_0])				,SUM([RIVA_10])				 ,SUM([RIVA_20])					,SUM([RIVA_30])				,SUM([RIVA_70])      ,SUM([RIVA_100])
      ,SUM([RTF_0])				,SUM([RTF_0_1])				,SUM([RTF_1])				,SUM([RTF_2])				 ,SUM([RTF_8])					,SUM([RTF_10])				,SUM([RTF_100])      ,[Documento]      
	  ,NULL [descripcion_cod_sri], NULL[re_tipoRet]			,[Num_Autorizacion_OG]      ,[IdSucursal]            ,[Su_Descripcion]
  FROM web.VWCXP_007
  where IdEmpresa = @IdEmpresa and co_FechaFactura between @Fecha_ini and @Fecha_fin and @Mostrar_agrupado = 0 and IdSucursal between @IdSucursalIni and @IdSucursalFin
  --and IdTipoCbte_Ogiro = @IdTipoCbte and IdCbteCble_Ogiro = @IdCbteCble
  GROUP BY [IdEmpresa]			,[IdTipoCbte_Ogiro]		,[IdCbteCble_Ogiro]      ,[IdOrden_giro_Tipo]		,[Codigo]				,[Descripcion]
      ,[IdProveedor]		,[pr_nombre]			,[pe_cedulaRuc]			,[serie_fact]			 ,[num_factura]				,[co_FechaFactura]      ,[subtotal_iva]
      ,[subtotal_sin_iva],   [valor_iva], [NAutorizacion]		,[serie_ret]			 ,[NumRetencion]			
      ,[Documento]      
	  ,[Num_Autorizacion_OG] ,[IdSucursal]            ,[Su_Descripcion]) A
   end
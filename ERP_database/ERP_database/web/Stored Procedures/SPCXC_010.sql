
CREATE PROCEDURE [web].[SPCXC_010]
	@IdEmpresa as int,
	@SucursalIni as int,
	@SucursalFin as int,	
	@IdClienteIni int,
	@IdClienteFin int,
	@IdTipoClienteIni as numeric,
	@IdTipoClienteFin as numeric,
	@fechaCorte as datetime,
	@MostrarSoloCarteraVencida bit
AS
BEGIN
	SET NOCOUNT ON;


select      Facturas_y_notas_deb.IdEmpresa ,Facturas_y_notas_deb.IdSucursal,Facturas_y_notas_deb.IdBodega,Facturas_y_notas_deb.IdCliente,Facturas_y_notas_deb.Codigo,Facturas_y_notas_deb.IdCbteVta,Facturas_y_notas_deb.CodCbteVta,Facturas_y_notas_deb.vt_tipoDoc,Facturas_y_notas_deb.vt_serie1,Facturas_y_notas_deb.vt_serie2,
            Facturas_y_notas_deb.vt_NumFactura,Facturas_y_notas_deb.Su_Descripcion,LTRIM(Facturas_y_notas_deb.pe_nombreCompleto) AS pe_nombreCompleto,Facturas_y_notas_deb.pe_cedulaRuc,
			Facturas_y_notas_deb.Valor_Original as Valor_Original,
			isnull(Cobros_x_fac.dc_ValorPago,0) as Total_Pagado,
            IIF( DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte)<=0, Facturas_y_notas_deb.Valor_Original - isnull(Cobros_x_fac.dc_ValorPago,0) ,0) Valor_x_Vencer,
			IIF( DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte)>0, Facturas_y_notas_deb.Valor_Original - isnull(Cobros_x_fac.dc_ValorPago,0) ,0) Valor_vencido,

			IIF( DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte)>=1 and  DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte )<=30 ,  Facturas_y_notas_deb.Valor_Original -( isnull( Cobros_x_fac.dc_ValorPago,0)) ,0) Vencer_30_Dias,
			IIF( DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte )>30 and  DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte )<=60 ,   Facturas_y_notas_deb.Valor_Original - ( isnull( Cobros_x_fac.dc_ValorPago,0)) ,0) Vencer_60_Dias,
			IIF( DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte )>60 and  DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte )<=90 ,  Facturas_y_notas_deb.Valor_Original -( isnull( Cobros_x_fac.dc_ValorPago,0)) ,0) Vencer_90_Dias,
			IIF( DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte )>90,  Facturas_y_notas_deb.Valor_Original - isnull( Cobros_x_fac.dc_ValorPago,0),0) Mayor_a_90Dias
			,Facturas_y_notas_deb.vt_fech_venc,Facturas_y_notas_deb.vt_fecha,Facturas_y_notas_deb.Idtipo_cliente,DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte ) Dias_Vencidos,
			ISNULL(cast( Facturas_y_notas_deb.Valor_Original-isnull( Cobros_x_fac.dc_ValorPago,0) as numeric(10,2)),0) Saldo,Facturas_y_notas_deb.pe_telefonoOfic, vt_Observacion, vt_plazo, NomContacto, TelefonoContacto,
			Descripcion_tip_cliente--, Idtipo_cliente
			
 from 
(
	
			SELECT              F.IdEmpresa, F.IdSucursal, F.IdBodega, dbo.fa_cliente.IdCliente, dbo.fa_cliente.Codigo, F.IdCbteVta, 
								F.CodCbteVta,F.vt_tipoDoc,F.vt_serie1,F.vt_serie2,F.vt_NumFactura, 
								dbo.tb_sucursal.Su_Descripcion, LTRIM(dbo.tb_persona.pe_nombreCompleto) + '/'+ cast( fa_cliente.IdCliente as varchar(20)) as pe_nombreCompleto, dbo.tb_persona.pe_cedulaRuc, 
								FD.Total Valor_Original,F.vt_fech_venc,
								F.vt_fecha,dbo.fa_cliente.Idtipo_cliente, '' +'/'+ dbo.tb_persona.pe_telfono_Contacto as pe_telefonoOfic,
								F.vt_Observacion,F.vt_plazo, p.pe_nombreCompleto as NomContacto, con.Telefono + '/' + con.Celular TelefonoContacto, t.Descripcion_tip_cliente
			FROM            fa_factura AS F INNER JOIN
                         fa_factura_resumen AS FD ON F.IdEmpresa = FD.IdEmpresa AND F.IdSucursal = FD.IdSucursal AND F.IdBodega = FD.IdBodega AND F.IdCbteVta = FD.IdCbteVta INNER JOIN
                         fa_cliente ON F.IdEmpresa = fa_cliente.IdEmpresa AND F.IdCliente = fa_cliente.IdCliente INNER JOIN
                         tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona INNER JOIN
                         tb_sucursal ON F.IdEmpresa = tb_sucursal.IdEmpresa AND F.IdSucursal = tb_sucursal.IdSucursal 
						 inner join tb_persona as p on fa_cliente.IdPersona = p.IdPersona
						 inner join fa_cliente_contactos as con on fa_cliente.IdEmpresa = con.IdEmpresa and fa_cliente.IdCliente = con.IdCliente
						 inner join fa_cliente_tipo AS T on fa_cliente.IdEmpresa = T.IdEmpresa and fa_cliente.Idtipo_cliente = t.Idtipo_cliente
			WHERE				F.IdEmpresa = @IdEmpresa AND F.vt_fecha <= @fechaCorte and F.Estado='A' 
			and fa_cliente.Idtipo_cliente between @IdTipoClienteIni and @IdTipoClienteFin
			AND f.IdCliente between @IdClienteIni and @IdClienteFin
			AND f.IdSucursal between @SucursalIni and @SucursalFin

			
-- *******************************************************************************************************************************************
-- notas de debito
union 

SELECT			dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_cliente.IdCliente, dbo.fa_cliente.Codigo, 
				dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CodNota,
			case 
				when  dbo.fa_notaCreDeb.CodDocumentoTipo is null then 'NTDB'
				 else  dbo.fa_notaCreDeb.CodDocumentoTipo end as CodDocumentoTipo
				, dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, 
				isnull( dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_notaCreDeb.CodNota)+'/'+cast(fa_notaCreDeb.IdNota as varchar(10)), 
				
				dbo.tb_sucursal.Su_Descripcion, 
				LTRIM(dbo.tb_persona.pe_nombreCompleto) + '/'+ cast( fa_cliente.IdCliente as varchar(20)) , dbo.tb_persona.pe_cedulaRuc, 
				dbo.fa_notaCreDeb_det.sc_total, dbo.fa_notaCreDeb.no_fecha_venc,dbo.fa_notaCreDeb.no_fecha, dbo.fa_cliente.Idtipo_cliente,
				dbo.tb_persona.pe_telfono_Contacto as pe_telefonoOfic, fa_notaCreDeb.sc_observacion,
				DATEDIFF(DAY,dbo.fa_notaCreDeb.no_fecha,dbo.fa_notaCreDeb.no_fecha_venc), tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_telfono_Contacto,
				t.Descripcion_tip_cliente
FROM            fa_notaCreDeb INNER JOIN
                fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND 
                fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota INNER JOIN
                fa_cliente ON fa_notaCreDeb.IdEmpresa = fa_cliente.IdEmpresa AND fa_notaCreDeb.IdCliente = fa_cliente.IdCliente INNER JOIN
                tb_sucursal ON fa_notaCreDeb.IdEmpresa = tb_sucursal.IdEmpresa AND fa_notaCreDeb.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona INNER JOIN
				fa_cliente_tipo AS T on fa_cliente.IdEmpresa = T.IdEmpresa and fa_cliente.Idtipo_cliente = t.Idtipo_cliente
where           dbo.fa_notaCreDeb.IdEmpresa = @IdEmpresa and dbo.fa_notaCreDeb.CreDeb='D' and fa_notaCreDeb.no_fecha <= @fechaCorte
				and dbo.fa_notaCreDeb.Estado='A' 
				and fa_cliente.Idtipo_cliente between @IdTipoClienteIni and @IdTipoClienteFin
				AND fa_notaCreDeb.IdCliente between @IdClienteIni and @IdClienteFin 
				AND fa_notaCreDeb.IdSucursal between @SucursalIni and @SucursalFin
				AND NOT EXISTS(
				  SELECT        *
                               FROM            fa_notaCreDeb_x_fa_factura_NotaDeb Cruce
                               WHERE        Cruce.IdEmpresa_nt = fa_notaCreDeb.IdEmpresa AND Cruce.IdSucursal_nt = fa_notaCreDeb.IdSucursal AND Cruce.IdBodega_nt = fa_notaCreDeb.IdBodega AND Cruce.IdNota_nt = fa_notaCreDeb.IdNota							  
				)
GROUP BY		dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_cliente.IdCliente, dbo.fa_cliente.Codigo, 
				dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CodNota, dbo.fa_notaCreDeb.CodDocumentoTipo, dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, 
				dbo.fa_notaCreDeb.NumNota_Impresa, dbo.tb_sucursal.Su_Descripcion, 
				LTRIM(dbo.tb_persona.pe_nombreCompleto) + '/'+ cast( fa_cliente.IdCliente as varchar(20)) , dbo.tb_persona.pe_cedulaRuc, 
				dbo.fa_notaCreDeb_det.sc_total, dbo.fa_notaCreDeb.no_fecha_venc,dbo.fa_notaCreDeb.no_fecha,dbo.fa_cliente.Idtipo_cliente,
				'' +'/'+ dbo.tb_persona.pe_telfono_Contacto, fa_notaCreDeb.sc_observacion, tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_telfono_Contacto,
				t.Descripcion_tip_cliente

) as  Facturas_y_notas_deb left join
(

SELECT A.IdEmpresa, A.IdSucursal, A.dc_TipoDocumento, A.IdBodega_Cbte, A.IdCbte_vta_nota, SUM(dc_ValorPago) dc_ValorPago
FROM(
		SELECT d.IdEmpresa, d.IdSucursal, d.dc_TipoDocumento, d.IdBodega_Cbte, d.IdCbte_vta_nota, 
		case when C.IdCobro_tipo <> 'TARJ' then
		d.dc_ValorPago
		when C.IdCobro_tipo = 'TARJ' AND ISNULL(L.Fecha, DATEADD(DAY,1,@fechaCorte)) <= @fechaCorte then
		d.dc_ValorPago
		else 0 end as dc_ValorPago
		FROM     cxc_LiquidacionTarjeta AS l INNER JOIN
		cxc_LiquidacionTarjeta_x_cxc_cobro AS ld ON l.IdEmpresa = ld.IdEmpresa AND l.IdSucursal = ld.IdSucursal AND l.IdLiquidacion = ld.IdLiquidacion RIGHT OUTER JOIN
		cxc_cobro AS c INNER JOIN
		cxc_cobro_det AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdSucursal = d.IdSucursal AND c.IdCobro = d.IdCobro ON ld.IdEmpresa = c.IdEmpresa AND ld.IdSucursal = c.IdSucursal AND ld.IdCobro = c.IdCobro
		WHERE					 c.IdEmpresa = @IdEmpresa and cast(c.cr_fechaCobro as date)<= @fechaCorte and c.cr_estado='A'
) A
GROUP BY A.IdEmpresa, A.IdSucursal, A.dc_TipoDocumento, A.IdBodega_Cbte, A.IdCbte_vta_nota
/*

		SELECT                   dbo.cxc_cobro.IdEmpresa, dbo.cxc_cobro.IdSucursal,  dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, 
								 dbo.cxc_cobro_det.IdCbte_vta_nota, 
								 
								 sum(dbo.cxc_cobro_det.dc_ValorPago) as dc_ValorPago 


		FROM                     dbo.cxc_cobro INNER JOIN
								 dbo.cxc_cobro_det ON dbo.cxc_cobro.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.cxc_cobro.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
								 dbo.cxc_cobro.IdCobro = dbo.cxc_cobro_det.IdCobro 

								 


		WHERE					 dbo.cxc_cobro.IdEmpresa = @IdEmpresa and cast(dbo.cxc_cobro.cr_fechaCobro as date)<= @fechaCorte and dbo.cxc_cobro.cr_estado='A'
		
		GROUP BY                 dbo.cxc_cobro.IdEmpresa, dbo.cxc_cobro.IdSucursal,  dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, 
								 dbo.cxc_cobro_det.IdCbte_vta_nota
*/	

) as Cobros_x_fac
on Facturas_y_notas_deb.IdEmpresa=Cobros_x_fac.IdEmpresa
and Facturas_y_notas_deb.IdSucursal=Cobros_x_fac.IdSucursal
and Facturas_y_notas_deb.IdBodega=Cobros_x_fac.IdBodega_Cbte
and Facturas_y_notas_deb.IdCbteVta=Cobros_x_fac.IdCbte_vta_nota
and Facturas_y_notas_deb.vt_tipoDoc=Cobros_x_fac.dc_TipoDocumento
where 
    Facturas_y_notas_deb.IdEmpresa = @IdEmpresa 
	and round(Facturas_y_notas_deb.Valor_Original,2) - round(isnull(Cobros_x_fac.dc_ValorPago,0),2) > 0
	and iif(@MostrarSoloCarteraVencida = 1,DATEDIFF( day,Facturas_y_notas_deb.vt_fech_venc,@fechaCorte), 0) >= 0

END
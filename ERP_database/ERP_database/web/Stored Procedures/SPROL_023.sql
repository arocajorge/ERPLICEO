--exec [web].[SPROL_023] 2,8,8,1,2,201906,1,99999,1,99999,1,99999
CREATE PROCEDURE [web].[SPROL_023]
(
@IdEmpresa int,
@IdSucursalIni int,
@IdSucursalFin int,
@IdNomina int,
@IdNominaTipoLiqui int,
@IdPeriodo int,
@IdDivisionIni int,
@IdDivisionFin int,
@IdAreaIni int,
@IdAreaFin int,
@IdDepartamentoIni int,
@IdDepartamentoFin int
)
AS


--declare
--@IdEmpresa int,
--@IdSucursalIni int,
--@IdSucursalFin int,
--@IdNomina int,
--@IdNominaTipoLiqui int,
--@IdPeriodo int,
--@IdDivisionIni int,
--@IdDivisionFin int,
--@IdAreaIni int,
--@IdAreaFin int,
--@IdDepartamentoIni int,
--@IdDepartamentoFin int


--set @IdEmpresa =1
--set @IdSucursalIni =1
--set @IdSucursalFin =99
--set @IdNomina =1
--set @IdNominaTipoLiqui =2
--set @IdPeriodo =201902
--set @IdDivisionIni =1
--set @IdDivisionFin =99
--set @IdAreaIni= 0
--set @IdAreaFin =99
--set @IdDepartamentoIni =0
--set @IdDepartamentoFin= 99


DECLARE 
@ValorFR float

SELECT IdEmpresa, IdRol, IdEmpleado, IdDivision, IdArea, IdDepartamento, IdSucursal,IdNominaTipo,  Descripcion, IdNominaTipoLiqui,DescripcionProcesoNomina,IdPeriodo, pe_nombreCompleto, NombreDivision, NombreArea, NombreDepartamento, Su_Descripcion, SUELDO,ANTICIPO, DECIMOC, DECIMOT, FRESERVA, IESS, PRESTAMO,SOBRET,
 TOTALE, TOTALI, OTROEGR,  OTROING,  DIASTRABAJADOS, NETO,NombreCargo, JORNADA, UBUCACION, Porcentaje,
 IESS_TOTAL, FRESERVA_TOTAL, ROW_NUMBER() OVER(PARTITION BY IdEmpresa, IdRol, IdEmpleado ORDER BY IdEmpresa, IdRol, IdEmpleado) As Fila
  FROM(
--******************************************************************************************************************************************************************
--*******************************************************************+PROFESORES************************************************************************************
SELECT a.IdEmpresa, A.IdRol, A.IdEmpleado, A.IdDivision, A.IdArea, A.IdDepartamento, A.IdSucursal,IdNominaTipo,  A.Descripcion,  A.IdNominaTipoLiqui,DescripcionProcesoNomina,  A.IdPeriodo, A.pe_nombreCompleto, A.NombreDivision, A.NombreArea, A.NombreDepartamento, A.Su_Descripcion,  ( A.SUELDO)*(B.Porcentaje/100) SUELDO, case when B.CargaGasto=1 then  A.ANTICIPO else 0 end ANTICIPO,  (A.DECIMOC)*(B.Porcentaje/100) DECIMOC, ( A.DECIMOT)*(B.Porcentaje/100) DECIMOT, ( A.FRESERVA)*(B.Porcentaje/100) FRESERVA, ( A.IESS)*(B.Porcentaje/100) IESS, case when B.CargaGasto=1 then  A.PRESTAMO else 0 end PRESTAMO,( A.SOBRET)*(B.Porcentaje/100) SOBRET,
 case when B.CargaGasto=1 then A.TOTALE else 0 end TOTALE, ( A.TOTALI)*(B.Porcentaje/100)TOTALI, case when B.CargaGasto=1 then  A.OTROEGR else 0 end OTROEGR,  (A.OTROING)*(B.Porcentaje/100)OTROING,  A.DIASTRABAJADOS, case when B.CargaGasto=1 then  ((A.TOTALI)*(B.Porcentaje/100)) - a.TOTALE else ( A.TOTALI)*(B.Porcentaje/100) end NETO,NombreCargo, A.JORNADA, B.Descripcion AS UBUCACION, B.Porcentaje,
 IESS_TOTAL, FRESERVA_TOTAL
 FROM (

SELECT IdEmpresa, IdRol,IdEmpleado,IdDivision,IdArea,IdDepartamento,IdSucursal,IdNominaTipo, Descripcion, IdNominaTipoLiqui,DescripcionProcesoNomina, IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea,NombreDepartamento,Su_Descripcion, SUM(SUELDO) SUELDO, SUM(ANTICIPO) ANTICIPO, SUM(DECIMOC) DECIMOC, SUM(DECIMOT) DECIMOT, SUM(FRESERVA)FRESERVA,SUM(IESS)IESS, SUM(PRESTAMO)PRESTAMO,SUM(SOBRET) SOBRET,
SUM(TOTALE)TOTALE, SUM(TOTALI)TOTALI,SUM(OTROEGR)OTROEGR, SUM(OTROING) OTROING, SUM(DIASTRABAJADOS) DIASTRABAJADOS, (SUM(TOTALI) - SUM(TOTALE)) NETO,NombreCargo, JORNADA
FROM (
		SELECT ro_rol_detalle.IdEmpresa, ro_rol_detalle.IdRol, ro_rol_detalle.IdEmpleado, ro_empleado.IdDivision, ro_empleado.IdArea, ro_empleado.IdDepartamento, ro_rol.IdSucursal, ro_rol.IdNominaTipo,ro_Nomina_Tipo.Descripcion, ro_rol.IdNominaTipoLiqui, ro_Nomina_Tipoliqui.DescripcionProcesoNomina, tb_sucursal.Su_Descripcion,
		ro_rol.IdPeriodo, tb_persona.pe_nombreCompleto, ro_Division.Descripcion AS NombreDivision, ro_area.Descripcion AS NombreArea, ro_Departamento.de_descripcion AS NombreDepartamento,
		ro_cargo.ca_descripcion as NombreCargo,
		ro_rubro_tipo.ru_tipo,ro_catalogo.ca_descripcion as NombreRubro,
		CASE WHEN ro_catalogo.CodCatalogo = 'SUELDO' THEN  VALOR  ELSE 0 END AS SUELDO,
		CASE WHEN ro_catalogo.CodCatalogo = 'ANTICIPO' THEN VALOR ELSE 0 END AS ANTICIPO,
		CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOC' THEN  VALOR ELSE 0 END AS DECIMOC,
		CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOT' THEN  VALOR ELSE 0 END AS DECIMOT,
	    case when ( select SUM( d.Valor) from ro_rol_detalle d where  d.IdEmpresa=ro_rol_detalle.IdEmpresa AND d.IdRol=ro_rol.IdRol and ro_rol_detalle.IdEmpleado=d.IdEmpleado and d.IdRubro=ro_rubros_calculados.IdRubro_fondo_reserva AND d.Valor>0) is not null then  CASE WHEN ro_rubro_tipo.rub_aplica_IESS=1 THEN  VALOR*(0.0833)  ELSE 0 END ELSE 0 END AS FRESERVA,
		CASE WHEN ro_contrato.IdNomina=1 THEN CASE WHEN ro_rubro_tipo.rub_aplica_IESS = 1 THEN  VALOR*(ro_rol.PorAportePersonal) ELSE 0 END ELSE 0 END AS IESS,
		CASE WHEN ro_catalogo.CodCatalogo = 'PRESTAMO' THEN VALOR ELSE 0 END AS PRESTAMO,
		CASE WHEN ro_catalogo.CodCatalogo = 'SOBRET' THEN  VALOR ELSE 0 END AS SOBRET,
		CASE WHEN ro_rubro_tipo.ru_tipo = 'E' THEN VALOR ELSE 0 END AS TOTALE,
		CASE WHEN ro_rubro_tipo.ru_tipo = 'I' THEN VALOR ELSE 0 END AS TOTALI,
		CASE WHEN ro_rubro_tipo.ru_tipo = 'E' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROEGR,
		CASE WHEN ro_rubro_tipo.ru_tipo = 'I' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROING,
		CASE WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_dias_trabajados then VALOR ELSE 0 END AS DIASTRABAJADOS,				  
		CASE WHEN ro_rubro_tipo.IdRubro <> ro_rubros_calculados.IdRubro_horas_vespertina AND ro_rubro_tipo.IdRubro<>ro_rubros_calculados.IdRubro_primaria_vespertina  then 'HORAS MATUTINA' WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_horas_vespertina THEN'HORAS VESPERTINA'   WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_primaria_vespertina THEN'PRIMARIA VESP.'  END AS JORNADA
				 
		FROM            dbo.tb_persona INNER JOIN
		dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
		dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento RIGHT OUTER JOIN
		dbo.ro_rol INNER JOIN
		dbo.ro_rol_detalle ON dbo.ro_rol.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND dbo.ro_rol.IdRol = dbo.ro_rol_detalle.IdRol INNER JOIN
		dbo.ro_rubro_tipo ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_rol_detalle.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
		dbo.ro_rubros_calculados ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa ON dbo.ro_empleado.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND 
		dbo.ro_empleado.IdEmpleado = dbo.ro_rol_detalle.IdEmpleado LEFT OUTER JOIN
		dbo.ro_Nomina_Tipo INNER JOIN
		dbo.ro_Nomina_Tipoliqui ON dbo.ro_Nomina_Tipo.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo ON 
		dbo.ro_rol.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
		dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui LEFT OUTER JOIN
		dbo.ro_area INNER JOIN
		dbo.ro_Division ON dbo.ro_area.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_area.IdDivision = dbo.ro_Division.IdDivision ON dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa AND 
		dbo.ro_empleado.IdDivision = dbo.ro_area.IdDivision AND dbo.ro_empleado.IdArea = dbo.ro_area.IdArea LEFT OUTER JOIN
		dbo.ro_catalogo ON dbo.ro_rubro_tipo.rub_GrupoResumen = dbo.ro_catalogo.CodCatalogo INNER JOIN
		dbo.tb_sucursal ON dbo.tb_sucursal.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.ro_rol.IdSucursal INNER JOIN
		dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
		dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado and dbo.ro_contrato.EstadoContrato <> 'ECT_LIQ'
		--METE EL WHERE AQUIIIIIIIIIIIIIIIIIIIIIIIIII :*

		where ro_rol_detalle.IdEmpresa=@IdEmpresa
		and ro_rol_detalle.IdSucursal>=@IdSucursalIni
		and ro_rol_detalle.IdSucursal<=@IdSucursalFin

		and ro_rol.IdNominaTipo=@IdNomina
		and ro_rol.IdNominaTipoLiqui=@IdNominaTipoLiqui
		and ro_rol.IdPeriodo=@IdPeriodo

				  
		and ro_empleado.IdDivision>=@IdDivisionIni
		and ro_empleado.IdDivision<=@IdDivisionFin

		and ro_empleado.IdArea>=@IdAreaIni
		and ro_empleado.IdArea<=@IdAreaFin

		and ro_empleado.IdDepartamento>=@IdDepartamentoIni
		and ro_empleado.IdDepartamento<=@IdDepartamentoFin

		and  ro_empleado.Tiene_ingresos_compartidos=1
		AND ro_empleado.Pago_por_horas=1
		) A
		GROUP BY IdEmpresa, IdRol,IdEmpleado,IdDivision,IdArea,Descripcion,DescripcionProcesoNomina,IdDepartamento,IdSucursal,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea,NombreDepartamento,Su_Descripcion,NombreCargo, JORNADA
) A
INNER JOIN 
(
SELECT        emp_x_are_xrol.IdEmpresa, emp_x_are_xrol.IdRol, emp_x_are_xrol.Secuencia, emp_x_are_xrol.IdEmpleado, emp_x_are_xrol.IDividion, emp_x_are_xrol.IdArea, emp_x_are_xrol.Porcentaje, area.Descripcion, 
ro_empleado_x_division_x_area.CargaGasto
FROM            ro_empleado_division_area_x_rol AS emp_x_are_xrol INNER JOIN
ro_area AS area ON emp_x_are_xrol.IdEmpresa = area.IdEmpresa AND emp_x_are_xrol.IDividion = area.IdDivision AND emp_x_are_xrol.IdArea = area.IdArea INNER JOIN
ro_empleado_x_division_x_area ON area.IdEmpresa = ro_empleado_x_division_x_area.IdEmpresa AND area.IdDivision = ro_empleado_x_division_x_area.IDividion AND 
area.IdArea = ro_empleado_x_division_x_area.IdArea AND emp_x_are_xrol.IdEmpleado = ro_empleado_x_division_x_area.IdEmpleado
--WHERE        (emp_x_are_xrol.IdEmpleado = 130)
) B ON 
A.IdEmpresa=B.IdEmpresa
AND A.IdRol=B.IdRol
AND A.IdEmpleado=B.IdEmpleado
		LEFT JOIN(
			SELECT G.IdEmpresa, G.IdRol, G.IdEmpleado, SUM(G.IESS_TOTAL)IESS_TOTAL, SUM(G.FRESERVA_TOTAL) FRESERVA_TOTAL
			FROM (
			SELECT D.IdEmpresa, D.IdRol, D.IdEmpleado,
			CASE WHEN CA.CodCatalogo = 'IESS' THEN D.Valor ELSE 0 END AS IESS_TOTAL,
			CASE WHEN CA.CodCatalogo = 'FRESERVA' THEN D.Valor ELSE 0 END AS FRESERVA_TOTAL
			FROM ro_rol_detalle D INNER JOIN ro_rol C
			ON C.IdEmpresa = D.IdEmpresa AND C.IdRol = D.IdRol
			INNER JOIN ro_rubro_tipo AS R
			ON R.IdEmpresa = D.IdEmpresa AND R.IdRubro = D.IdRubro
			INNER JOIN dbo.ro_catalogo CA ON R.rub_GrupoResumen = CA.CodCatalogo
			WHERE D.IdEmpresa = @IdEmpresa		
			and D.IdSucursal>=@IdSucursalIni
			and D.IdSucursal<=@IdSucursalFin
			and C.IdNominaTipo=@IdNomina
			and C.IdNominaTipoLiqui=@IdNominaTipoLiqui
			and C.IdPeriodo=@IdPeriodo 
			)G
			GROUP BY G.IdEmpresa, G.IdRol, G.IdEmpleado
		) T ON A.IdEmpresa = T.IdEmpresa AND A.IdRol = T.IdRol AND A.IdEmpleado = T.IdEmpleado

UNION ALL

			
			
			
			
				  
SELECT A.IdEmpresa, A.IdRol,A.IdEmpleado,IdDivision,IdArea,IdDepartamento,IdSucursal,IdNominaTipo, Descripcion, IdNominaTipoLiqui,DescripcionProcesoNomina, IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea, NombreDepartamento,Su_Descripcion, SUM(SUELDO) SUELDO, SUM(ANTICIPO) ANTICIPO, SUM(DECIMOC) DECIMOC, SUM(DECIMOT) DECIMOT, SUM(FRESERVA)FRESERVA,SUM(IESS)IESS, SUM(PRESTAMO)PRESTAMO,SUM(SOBRET) SOBRET, 
SUM(TOTALE)TOTALE, SUM(TOTALI)TOTALI,SUM(OTROEGR)OTROEGR, SUM(OTROING) OTROING, SUM(DIASTRABAJADOS) DIASTRABAJADOS, (SUM(TOTALI) - SUM(TOTALE)) NETO,NombreCargo, JORNADA, '' UBICACION, 0 PORCENTAJE,
IESS_TOTAL, FRESERVA_TOTAL
FROM (
SELECT ro_rol_detalle.IdEmpresa, ro_rol_detalle.IdRol, ro_rol_detalle.IdEmpleado, ro_empleado.IdDivision, ro_empleado.IdArea, ro_empleado.IdDepartamento, ro_rol.IdSucursal, ro_rol.IdNominaTipo,ro_Nomina_Tipo.Descripcion, ro_rol.IdNominaTipoLiqui, ro_Nomina_Tipoliqui.DescripcionProcesoNomina, tb_sucursal.Su_Descripcion,
ro_rol.IdPeriodo, tb_persona.pe_nombreCompleto, ro_Division.Descripcion AS NombreDivision, ro_area.Descripcion AS NombreArea, ro_Departamento.de_descripcion AS NombreDepartamento,
ro_cargo.ca_descripcion as NombreCargo,
				  ro_rubro_tipo.ru_tipo,ro_catalogo.ca_descripcion as NombreRubro,
				  CASE WHEN ro_catalogo.CodCatalogo = 'SUELDO' THEN VALOR ELSE 0 END AS SUELDO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'ANTICIPO' THEN VALOR ELSE 0 END AS ANTICIPO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOC' THEN VALOR ELSE 0 END AS DECIMOC,
				  CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOT' THEN VALOR ELSE 0 END AS DECIMOT,
				
				   case when ( select SUM( d.Valor) from ro_rol_detalle d where  d.IdEmpresa=ro_rol_detalle.IdEmpresa AND d.IdRol=ro_rol.IdRol and ro_rol_detalle.IdEmpleado=d.IdEmpleado and d.IdRubro=ro_rubros_calculados.IdRubro_fondo_reserva AND d.Valor>0) is not null then  CASE WHEN ro_rubro_tipo.rub_aplica_IESS=1 THEN  VALOR*(0.0833)  ELSE 0 END ELSE 0 END AS FRESERVA,
				  CASE WHEN ro_contrato.IdNomina=1 THEN CASE WHEN ro_rubro_tipo.rub_aplica_IESS = 1 THEN  VALOR*(ro_rol.PorAportePersonal) ELSE 0 END ELSE 0 END AS IESS,
				  CASE WHEN ro_catalogo.CodCatalogo = 'PRESTAMO' THEN VALOR ELSE 0 END AS PRESTAMO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'SOBRET' THEN VALOR ELSE 0 END AS SOBRET,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'E' THEN VALOR ELSE 0 END AS TOTALE,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'I' THEN VALOR ELSE 0 END AS TOTALI,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'E' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROEGR,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'I' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROING,
				  CASE WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_dias_trabajados then VALOR ELSE 0 END AS DIASTRABAJADOS,				  
				 case when J.CONT > 1 then
				 (CASE WHEN ro_rubro_tipo.IdRubro <> ro_rubros_calculados.IdRubro_horas_vespertina AND ro_rubro_tipo.IdRubro<>ro_rubros_calculados.IdRubro_primaria_vespertina  
				 then 'HORAS MATUTINA' WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_horas_vespertina 
				 THEN'HORAS VESPERTINA' 
				 WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_primaria_vespertina THEN'PRIMARIA VESP.'  END )
				 ELSE J.NomJornada END
				 AS JORNADA						 
FROM            dbo.tb_persona INNER JOIN
                         dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento RIGHT OUTER JOIN
                         dbo.ro_rol INNER JOIN
                         dbo.ro_rol_detalle ON dbo.ro_rol.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND dbo.ro_rol.IdRol = dbo.ro_rol_detalle.IdRol INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_rol_detalle.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa ON dbo.ro_empleado.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_rol_detalle.IdEmpleado LEFT OUTER JOIN
                         dbo.ro_Nomina_Tipo INNER JOIN
                         dbo.ro_Nomina_Tipoliqui ON dbo.ro_Nomina_Tipo.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo ON 
                         dbo.ro_rol.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui LEFT OUTER JOIN
                         dbo.ro_area INNER JOIN
                         dbo.ro_Division ON dbo.ro_area.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_area.IdDivision = dbo.ro_Division.IdDivision ON dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa AND 
                         dbo.ro_empleado.IdDivision = dbo.ro_area.IdDivision AND dbo.ro_empleado.IdArea = dbo.ro_area.IdArea LEFT OUTER JOIN
                         dbo.ro_catalogo ON dbo.ro_rubro_tipo.rub_GrupoResumen = dbo.ro_catalogo.CodCatalogo INNER JOIN
                         dbo.tb_sucursal ON dbo.tb_sucursal.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.ro_rol.IdSucursal INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado and dbo.ro_contrato.EstadoContrato <> 'ECT_LIQ'
						 LEFT JOIN (
						 SELECT X.IdEmpresa, IdEmpleado, MAX(Y.Descripcion) NomJornada, COUNT(*) CONT FROM ro_empleado_x_jornada AS X
						 INNER JOIN ro_jornada AS Y ON X.IDEMPRESA = Y.IdEmpresa AND X.IdJornada = Y.IdJornada
						 WHERE X.IdEmpresa = @IdEmpresa
						 GROUP BY X.IdEmpresa, IdEmpleado
						 ) AS J ON ro_empleado.IdEmpresa = J.IdEmpresa AND ro_empleado.IdEmpleado = J.IdEmpleado
				  

				  where ro_rol_detalle.IdEmpresa=@IdEmpresa
				  and ro_rol_detalle.IdSucursal>=@IdSucursalIni
				  and ro_rol_detalle.IdSucursal<=@IdSucursalFin

				  and ro_rol.IdNominaTipo=@IdNomina
				  and ro_rol.IdNominaTipoLiqui=@IdNominaTipoLiqui
				  and ro_rol.IdPeriodo=@IdPeriodo

				  
				  and ro_empleado.IdDivision>=@IdDivisionIni
				  and ro_empleado.IdDivision<=@IdDivisionFin

				  and ro_empleado.IdArea>=@IdAreaIni
				  and ro_empleado.IdArea<=@IdAreaFin

				  and ro_empleado.IdDepartamento>=@IdDepartamentoIni
				  and ro_empleado.IdDepartamento<=@IdDepartamentoFin
				  
				  and  ro_empleado.Tiene_ingresos_compartidos=0
				  AND ro_empleado.Pago_por_horas=1

				  and ro_contrato.IdNomina = @IdNomina
				  ) A

				  LEFT JOIN(
					SELECT G.IdEmpresa, G.IdRol, G.IdEmpleado, SUM(G.IESS_TOTAL)IESS_TOTAL, SUM(G.FRESERVA_TOTAL) FRESERVA_TOTAL
					FROM (
					SELECT D.IdEmpresa, D.IdRol, D.IdEmpleado,
					CASE WHEN CA.CodCatalogo = 'IESS' THEN D.Valor ELSE 0 END AS IESS_TOTAL,
					CASE WHEN CA.CodCatalogo = 'FRESERVA' THEN D.Valor ELSE 0 END AS FRESERVA_TOTAL
					FROM ro_rol_detalle D INNER JOIN ro_rol C
					ON C.IdEmpresa = D.IdEmpresa AND C.IdRol = D.IdRol
					INNER JOIN ro_rubro_tipo AS R
					ON R.IdEmpresa = D.IdEmpresa AND R.IdRubro = D.IdRubro
					INNER JOIN dbo.ro_catalogo CA ON R.rub_GrupoResumen = CA.CodCatalogo
					WHERE D.IdEmpresa = @IdEmpresa		
					and D.IdSucursal>=@IdSucursalIni
					and D.IdSucursal<=@IdSucursalFin
					and C.IdNominaTipo=@IdNomina
					and C.IdNominaTipoLiqui=@IdNominaTipoLiqui
					and C.IdPeriodo=@IdPeriodo 
					)G
					GROUP BY G.IdEmpresa, G.IdRol, G.IdEmpleado
				) T ON A.IdEmpresa = T.IdEmpresa AND A.IdRol = T.IdRol AND A.IdEmpleado = T.IdEmpleado

				  GROUP BY A.IdEmpresa, A.IdRol,A.IdEmpleado,IdDivision,IdArea,Descripcion,DescripcionProcesoNomina,IdDepartamento,IdSucursal,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea,NombreDepartamento,Su_Descripcion,NombreCargo, JORNADA,IESS_TOTAL, FRESERVA_TOTAL


--******************************************************************************************************************************************************************
--*******************************************************************+PERSONAL QUE NO SE LES PAGA POR HORAS ADM.****************************************************
UNION ALL





SELECT a.IdEmpresa, A.IdRol, A.IdEmpleado, A.IdDivision, A.IdArea, A.IdDepartamento, A.IdSucursal,IdNominaTipo,  A.Descripcion,  A.IdNominaTipoLiqui,DescripcionProcesoNomina,  A.IdPeriodo, A.pe_nombreCompleto, A.NombreDivision, A.NombreArea, A.NombreDepartamento, A.Su_Descripcion,  ( A.SUELDO)*(B.Porcentaje/100) SUELDO, case when B.CargaGasto=1 then  A.ANTICIPO else 0 end ANTICIPO,  (A.DECIMOC)*(B.Porcentaje/100) DECIMOC, ( A.DECIMOT)*(B.Porcentaje/100) DECIMOT, ( A.FRESERVA)*(B.Porcentaje/100) FRESERVA, ( A.IESS)*(B.Porcentaje/100) IESS, case when B.CargaGasto=1 then  A.PRESTAMO else 0 end PRESTAMO,( A.SOBRET)*(B.Porcentaje/100) SOBRET,
 case when B.CargaGasto=1 then A.TOTALE else 0 end TOTALE, ( A.TOTALI)*(B.Porcentaje/100)TOTALI, case when B.CargaGasto=1 then  A.OTROEGR else 0 end OTROEGR,  (A.OTROING)*(B.Porcentaje/100)OTROING,  A.DIASTRABAJADOS, case when B.CargaGasto=1 then  ((A.TOTALI)*(B.Porcentaje/100)) - a.TOTALE else ( A.TOTALI)*(B.Porcentaje/100) end NETO,NombreCargo, A.JORNADA, B.Descripcion AS UBUCACION, B.Porcentaje,
 T.IESS_TOTAL, T.FRESERVA_TOTAL

 FROM (

SELECT IdEmpresa, IdRol,IdEmpleado,IdDivision,IdArea,IdDepartamento,IdSucursal,IdNominaTipo, Descripcion, IdNominaTipoLiqui,DescripcionProcesoNomina, IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea,NombreDepartamento,Su_Descripcion, SUM(SUELDO) SUELDO, SUM(ANTICIPO) ANTICIPO, SUM(DECIMOC) DECIMOC, SUM(DECIMOT) DECIMOT, SUM(FRESERVA)FRESERVA,SUM(IESS)IESS, SUM(PRESTAMO)PRESTAMO,SUM(SOBRET) SOBRET,
SUM(TOTALE)TOTALE, SUM(TOTALI)TOTALI,SUM(OTROEGR)OTROEGR, SUM(OTROING) OTROING, SUM(DIASTRABAJADOS) DIASTRABAJADOS, (SUM(TOTALI) - SUM(TOTALE)) NETO,NombreCargo, JORNADA
FROM (
SELECT ro_rol_detalle.IdEmpresa, ro_rol_detalle.IdRol, ro_rol_detalle.IdEmpleado, ro_empleado.IdDivision, ro_empleado.IdArea, ro_empleado.IdDepartamento, ro_rol.IdSucursal, ro_rol.IdNominaTipo,ro_Nomina_Tipo.Descripcion, ro_rol.IdNominaTipoLiqui, ro_Nomina_Tipoliqui.DescripcionProcesoNomina, tb_sucursal.Su_Descripcion,
ro_rol.IdPeriodo, tb_persona.pe_nombreCompleto, ro_Division.Descripcion AS NombreDivision, ro_area.Descripcion AS NombreArea, ro_Departamento.de_descripcion AS NombreDepartamento,
ro_cargo.ca_descripcion as NombreCargo,
				  ro_rubro_tipo.ru_tipo,ro_catalogo.ca_descripcion as NombreRubro,
				  CASE WHEN ro_catalogo.CodCatalogo = 'SUELDO' THEN  VALOR  ELSE 0 END AS SUELDO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'ANTICIPO' THEN VALOR ELSE 0 END AS ANTICIPO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOC' THEN  VALOR ELSE 0 END AS DECIMOC,
				  CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOT' THEN  VALOR ELSE 0 END AS DECIMOT,
				  case when ( select SUM( d.Valor) from ro_rol_detalle d where  d.IdEmpresa=ro_rol_detalle.IdEmpresa AND d.IdRol=ro_rol.IdRol and ro_rol_detalle.IdEmpleado=d.IdEmpleado and d.IdRubro=ro_rubros_calculados.IdRubro_fondo_reserva AND d.Valor>0) is not null then  CASE WHEN ro_rubro_tipo.rub_aplica_IESS=1 THEN  VALOR*(0.0833)  ELSE 0 END ELSE 0 END AS FRESERVA,
				  CASE WHEN ro_contrato.IdNomina=1 THEN CASE WHEN ro_rubro_tipo.rub_aplica_IESS = 1 THEN  VALOR*(ro_rol.PorAportePersonal) ELSE 0 END ELSE 0 END AS IESS,
				  CASE WHEN ro_catalogo.CodCatalogo = 'PRESTAMO' THEN VALOR ELSE 0 END AS PRESTAMO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'SOBRET' THEN  VALOR ELSE 0 END AS SOBRET,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'E' THEN VALOR ELSE 0 END AS TOTALE,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'I' THEN VALOR ELSE 0 END AS TOTALI,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'E' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROEGR,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'I' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROING,
				  CASE WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_dias_trabajados then VALOR ELSE 0 END AS DIASTRABAJADOS,				  
				  ' ' JORNADA
				 
FROM            dbo.tb_persona INNER JOIN
                         dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento RIGHT OUTER JOIN
                         dbo.ro_rol INNER JOIN
                         dbo.ro_rol_detalle ON dbo.ro_rol.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND dbo.ro_rol.IdRol = dbo.ro_rol_detalle.IdRol INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_rol_detalle.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa ON dbo.ro_empleado.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_rol_detalle.IdEmpleado LEFT OUTER JOIN
                         dbo.ro_Nomina_Tipo INNER JOIN
                         dbo.ro_Nomina_Tipoliqui ON dbo.ro_Nomina_Tipo.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo ON 
                         dbo.ro_rol.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui LEFT OUTER JOIN
                         dbo.ro_area INNER JOIN
                         dbo.ro_Division ON dbo.ro_area.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_area.IdDivision = dbo.ro_Division.IdDivision ON dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa AND 
                         dbo.ro_empleado.IdDivision = dbo.ro_area.IdDivision AND dbo.ro_empleado.IdArea = dbo.ro_area.IdArea LEFT OUTER JOIN
                         dbo.ro_catalogo ON dbo.ro_rubro_tipo.rub_GrupoResumen = dbo.ro_catalogo.CodCatalogo INNER JOIN
                         dbo.tb_sucursal ON dbo.tb_sucursal.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.ro_rol.IdSucursal INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado and dbo.ro_contrato.EstadoContrato <> 'ECT_LIQ'


				  
				  where ro_rol_detalle.IdEmpresa=@IdEmpresa
				  and ro_rol_detalle.IdSucursal>=@IdSucursalIni
				  and ro_rol_detalle.IdSucursal<=@IdSucursalFin
				  
				  and ro_rol.IdNominaTipo=@IdNomina
				  and ro_rol.IdNominaTipoLiqui=@IdNominaTipoLiqui
				  and ro_rol.IdPeriodo=@IdPeriodo

				  
				  and ro_empleado.IdDivision>=@IdDivisionIni
				  and ro_empleado.IdDivision<=@IdDivisionFin

				  and ro_empleado.IdArea>=@IdAreaIni
				  and ro_empleado.IdArea<=@IdAreaFin

				  and ro_empleado.IdDepartamento>=@IdDepartamentoIni
				  and ro_empleado.IdDepartamento<=@IdDepartamentoFin

				  and  ro_empleado.Tiene_ingresos_compartidos=1
				  AND ro_empleado.Pago_por_horas=0
				  ) A
				  GROUP BY IdEmpresa, IdRol,IdEmpleado,IdDivision,IdArea,Descripcion,DescripcionProcesoNomina,IdDepartamento,IdSucursal,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea,NombreDepartamento,Su_Descripcion,NombreCargo, JORNADA
				  ) A
				  INNER JOIN 

				  (
					SELECT        emp_x_are_xrol.IdEmpresa, emp_x_are_xrol.IdRol, emp_x_are_xrol.Secuencia, emp_x_are_xrol.IdEmpleado, emp_x_are_xrol.IDividion, emp_x_are_xrol.IdArea, emp_x_are_xrol.Porcentaje, area.Descripcion, 
                         ro_empleado_x_division_x_area.CargaGasto
FROM            ro_empleado_division_area_x_rol AS emp_x_are_xrol INNER JOIN
                         ro_area AS area ON emp_x_are_xrol.IdEmpresa = area.IdEmpresa AND emp_x_are_xrol.IDividion = area.IdDivision AND emp_x_are_xrol.IdArea = area.IdArea INNER JOIN
                         ro_empleado_x_division_x_area ON area.IdEmpresa = ro_empleado_x_division_x_area.IdEmpresa AND area.IdDivision = ro_empleado_x_division_x_area.IDividion AND 
                         area.IdArea = ro_empleado_x_division_x_area.IdArea AND emp_x_are_xrol.IdEmpleado = ro_empleado_x_division_x_area.IdEmpleado
--WHERE        (emp_x_are_xrol.IdEmpleado = 130)
				  ) B ON 
				  A.IdEmpresa=B.IdEmpresa
				  AND A.IdRol=B.IdRol
				  AND A.IdEmpleado=B.IdEmpleado
				  LEFT JOIN(
					SELECT G.IdEmpresa, G.IdRol, G.IdEmpleado, SUM(G.IESS_TOTAL)IESS_TOTAL, SUM(G.FRESERVA_TOTAL) FRESERVA_TOTAL
					FROM (
					SELECT D.IdEmpresa, D.IdRol, D.IdEmpleado,
					CASE WHEN CA.CodCatalogo = 'IESS' THEN D.Valor ELSE 0 END AS IESS_TOTAL,
					CASE WHEN CA.CodCatalogo = 'FRESERVA' THEN D.Valor ELSE 0 END AS FRESERVA_TOTAL
					FROM ro_rol_detalle D INNER JOIN ro_rol C
					ON C.IdEmpresa = D.IdEmpresa AND C.IdRol = D.IdRol
					INNER JOIN ro_rubro_tipo AS R
					ON R.IdEmpresa = D.IdEmpresa AND R.IdRubro = D.IdRubro
					INNER JOIN dbo.ro_catalogo CA ON R.rub_GrupoResumen = CA.CodCatalogo
					WHERE D.IdEmpresa = @IdEmpresa		
					and D.IdSucursal>=@IdSucursalIni
					and D.IdSucursal<=@IdSucursalFin
					and C.IdNominaTipo=@IdNomina
					and C.IdNominaTipoLiqui=@IdNominaTipoLiqui
					and C.IdPeriodo=@IdPeriodo 
					)G
					GROUP BY G.IdEmpresa, G.IdRol, G.IdEmpleado
				) T ON A.IdEmpresa = T.IdEmpresa AND A.IdRol = T.IdRol AND A.IdEmpleado = T.IdEmpleado

				  UNION ALL

			
			
			
				  
SELECT A.IdEmpresa, A.IdRol,A.IdEmpleado,IdDivision,IdArea,IdDepartamento,IdSucursal,IdNominaTipo, Descripcion, IdNominaTipoLiqui,DescripcionProcesoNomina, IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea, NombreDepartamento,Su_Descripcion, SUM(SUELDO) SUELDO, SUM(ANTICIPO) ANTICIPO, SUM(DECIMOC) DECIMOC, SUM(DECIMOT) DECIMOT, SUM(FRESERVA)FRESERVA,SUM(IESS)IESS, SUM(PRESTAMO)PRESTAMO,SUM(SOBRET) SOBRET, 
SUM(TOTALE)TOTALE, SUM(TOTALI)TOTALI,SUM(OTROEGR)OTROEGR, SUM(OTROING) OTROING, SUM(DIASTRABAJADOS) DIASTRABAJADOS, (SUM(TOTALI) - SUM(TOTALE)) NETO,NombreCargo, JORNADA, '' UBICACION, 0 PORCENTAJE,
IESS_TOTAL, FRESERVA_TOTAL
FROM (
SELECT ro_rol_detalle.IdEmpresa, ro_rol_detalle.IdRol, ro_rol_detalle.IdEmpleado, ro_empleado.IdDivision, ro_empleado.IdArea, ro_empleado.IdDepartamento, ro_rol.IdSucursal, ro_rol.IdNominaTipo,ro_Nomina_Tipo.Descripcion, ro_rol.IdNominaTipoLiqui, ro_Nomina_Tipoliqui.DescripcionProcesoNomina, tb_sucursal.Su_Descripcion,
ro_rol.IdPeriodo, tb_persona.pe_nombreCompleto, ro_Division.Descripcion AS NombreDivision, ro_area.Descripcion AS NombreArea, ro_Departamento.de_descripcion AS NombreDepartamento,
ro_cargo.ca_descripcion as NombreCargo,
				  ro_rubro_tipo.ru_tipo,ro_catalogo.ca_descripcion as NombreRubro,
				  CASE WHEN ro_catalogo.CodCatalogo = 'SUELDO' THEN VALOR ELSE 0 END AS SUELDO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'ANTICIPO' THEN VALOR ELSE 0 END AS ANTICIPO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOC' THEN VALOR ELSE 0 END AS DECIMOC,
				  CASE WHEN ro_catalogo.CodCatalogo = 'DECIMOT' THEN VALOR ELSE 0 END AS DECIMOT,
				case when  (select SUM( d.Valor) from ro_rol_detalle d where  d.IdEmpresa=ro_rol_detalle.IdEmpresa AND d.IdRol=ro_rol.IdRol and ro_rol_detalle.IdEmpleado=d.IdEmpleado and d.IdRubro=ro_rubros_calculados.IdRubro_fondo_reserva and d.Valor>0) is not null then  CASE WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_fondo_reserva /*ro_rubro_tipo.rub_aplica_IESS=1*/ THEN  VALOR/**(0.0833)*/  ELSE 0 END ELSE 0 END AS FRESERVA,
				  --case when ( select SUM( d.Valor) from ro_rol_detalle d where  d.IdEmpresa=ro_rol_detalle.IdEmpresa AND d.IdRol=ro_rol.IdRol and ro_rol_detalle.IdEmpleado=d.IdEmpleado and d.IdRubro=ro_rubros_calculados.IdRubro_fondo_reserva and d.Valor>0) is not null then  CASE WHEN ro_rubro_tipo.rub_aplica_IESS=1 THEN  VALOR*(0.0833)  ELSE 0 END ELSE 0 END AS FRESERVA,
				  CASE WHEN ro_contrato.IdNomina=1 THEN CASE WHEN ro_rubro_tipo.rub_aplica_IESS = 1 THEN  VALOR*(ro_rol.PorAportePersonal) ELSE 0 END ELSE 0 END AS IESS,
				  CASE WHEN ro_catalogo.CodCatalogo = 'PRESTAMO' THEN VALOR ELSE 0 END AS PRESTAMO,
				  CASE WHEN ro_catalogo.CodCatalogo = 'SOBRET' THEN VALOR ELSE 0 END AS SOBRET,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'E' THEN VALOR ELSE 0 END AS TOTALE,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'I' THEN VALOR ELSE 0 END AS TOTALI,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'E' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROEGR,
				  CASE WHEN ro_rubro_tipo.ru_tipo = 'I' AND ro_rubro_tipo.rub_GrupoResumen is null THEN VALOR ELSE 0 END AS OTROING,
				  CASE WHEN ro_rubro_tipo.IdRubro = ro_rubros_calculados.IdRubro_dias_trabajados then VALOR ELSE 0 END AS DIASTRABAJADOS,				  
				 ' ' JORNADA
				 
FROM            dbo.tb_persona INNER JOIN
                         dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento RIGHT OUTER JOIN
                         dbo.ro_rol INNER JOIN
                         dbo.ro_rol_detalle ON dbo.ro_rol.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND dbo.ro_rol.IdRol = dbo.ro_rol_detalle.IdRol INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_rol_detalle.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa ON dbo.ro_empleado.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_rol_detalle.IdEmpleado LEFT OUTER JOIN
                         dbo.ro_Nomina_Tipo INNER JOIN
                         dbo.ro_Nomina_Tipoliqui ON dbo.ro_Nomina_Tipo.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo ON 
                         dbo.ro_rol.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui LEFT OUTER JOIN
                         dbo.ro_area INNER JOIN
                         dbo.ro_Division ON dbo.ro_area.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_area.IdDivision = dbo.ro_Division.IdDivision ON dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa AND 
                         dbo.ro_empleado.IdDivision = dbo.ro_area.IdDivision AND dbo.ro_empleado.IdArea = dbo.ro_area.IdArea LEFT OUTER JOIN
                         dbo.ro_catalogo ON dbo.ro_rubro_tipo.rub_GrupoResumen = dbo.ro_catalogo.CodCatalogo INNER JOIN
                         dbo.tb_sucursal ON dbo.tb_sucursal.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.ro_rol.IdSucursal INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado  and dbo.ro_contrato.EstadoContrato <> 'ECT_LIQ'
				  --METE EL WHERE AQUIIIIIIIIIIIIIIIIIIIIIIIIII :*

				  where ro_rol_detalle.IdEmpresa=@IdEmpresa
				  and ro_rol_detalle.IdSucursal>=@IdSucursalIni
				  and ro_rol_detalle.IdSucursal<=@IdSucursalFin

				  and ro_rol.IdNominaTipo=@IdNomina
				  and ro_rol.IdNominaTipoLiqui=@IdNominaTipoLiqui
				  and ro_rol.IdPeriodo=@IdPeriodo

				  
				  and ro_empleado.IdDivision>=@IdDivisionIni
				  and ro_empleado.IdDivision<=@IdDivisionFin

				  and ro_empleado.IdArea>=@IdAreaIni
				  and ro_empleado.IdArea<=@IdAreaFin

				  and ro_empleado.IdDepartamento>=@IdDepartamentoIni
				  and ro_empleado.IdDepartamento<=@IdDepartamentoFin
				  AND ro_empleado.Pago_por_horas=0
				  
				  and  ro_empleado.Tiene_ingresos_compartidos=0
				  ) A
				  LEFT JOIN(
					SELECT G.IdEmpresa, G.IdRol, G.IdEmpleado, SUM(G.IESS_TOTAL)IESS_TOTAL, SUM(G.FRESERVA_TOTAL) FRESERVA_TOTAL
					FROM (
					SELECT D.IdEmpresa, D.IdRol, D.IdEmpleado,
					CASE WHEN CA.CodCatalogo = 'IESS' THEN D.Valor ELSE 0 END AS IESS_TOTAL,
					CASE WHEN CA.CodCatalogo = 'FRESERVA' THEN D.Valor ELSE 0 END AS FRESERVA_TOTAL
					FROM ro_rol_detalle D INNER JOIN ro_rol C
					ON C.IdEmpresa = D.IdEmpresa AND C.IdRol = D.IdRol
					INNER JOIN ro_rubro_tipo AS R
					ON R.IdEmpresa = D.IdEmpresa AND R.IdRubro = D.IdRubro
					INNER JOIN dbo.ro_catalogo CA ON R.rub_GrupoResumen = CA.CodCatalogo
					WHERE D.IdEmpresa = @IdEmpresa		
					and D.IdSucursal>=@IdSucursalIni
					and D.IdSucursal<=@IdSucursalFin
					and C.IdNominaTipo=@IdNomina
					and C.IdNominaTipoLiqui=@IdNominaTipoLiqui
					and C.IdPeriodo=@IdPeriodo 
					)G
					GROUP BY G.IdEmpresa, G.IdRol, G.IdEmpleado
				) T ON A.IdEmpresa = T.IdEmpresa AND A.IdRol = T.IdRol AND A.IdEmpleado = T.IdEmpleado
				  GROUP BY A.IdEmpresa, A.IdRol,A.IdEmpleado,IdDivision,IdArea,Descripcion,DescripcionProcesoNomina,IdDepartamento,IdSucursal,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,pe_nombreCompleto,NombreDivision,NombreArea,NombreDepartamento,Su_Descripcion,NombreCargo, JORNADA, T.IESS_TOTAL, T.FRESERVA_TOTAL 


)F
order by IdEmpresa, IdRol, IdEmpleado
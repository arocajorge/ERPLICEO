CREATE VIEW dbo.vwRo_rol_detalle_saldo_por_pagar
AS
SELECT        rol_1.IdEmpresa, rol_1.IdRol, rol_1.IdNominaTipo, rol_1.IdNominaTipoLiqui, rol_1.IdPeriodo, rol_1.IdEmpleado, rol_1.IdRubro, rol_1.ValorGanado, rol_1.IdSucursal, rol_1.em_codigo, rol_1.pe_apellido, rol_1.pe_nombre, 
                         rol_1.pe_nombreCompleto, rol_1.pe_cedulaRuc, ISNULL(pago.Valor, 0) AS ValorCancelado, rol_1.ValorGanado - ISNULL(pago.Valor, 0) AS Saldo, rol_1.em_NumCta, rol_1.em_tipoCta, rol_1.IdPersona, rol_1.IdCtaCble_Emplea, 
                         rol_1.IdCtaCble_x_pagar_empleado
FROM            (SELECT        rol.IdEmpresa, rol.IdRol, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol.IdPeriodo, rol_det.IdEmpleado, rol_det.IdRubro, rol_det.Valor AS ValorGanado, rol_det.IdSucursal, emp.em_codigo, persona.pe_apellido, 
                                                    persona.pe_nombre, persona.pe_nombreCompleto, persona.pe_cedulaRuc, emp.em_NumCta, emp.em_tipoCta, persona.IdPersona, emp.IdCtaCble_Emplea, emp.IdCtaCble_x_pagar_empleado
                          FROM            dbo.ro_rol AS rol INNER JOIN
                                                    dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                                                    dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                                                    dbo.tb_persona AS persona ON emp.IdPersona = persona.IdPersona AND rol_det.IdRubro = 24) AS rol_1 LEFT OUTER JOIN
                             (SELECT        archivo.IdEmpresa, archivo.IdNomina, archivo.IdNominaTipo, archivo.IdPeriodo, archivo_det.IdSucursal, archivo_det.IdEmpleado, SUM(archivo_det.Valor) AS Valor
                               FROM            dbo.ro_archivos_bancos_generacion AS archivo INNER JOIN
                                                         dbo.ro_archivos_bancos_generacion_x_empleado AS archivo_det ON archivo.IdEmpresa = archivo_det.IdEmpresa AND archivo.IdArchivo = archivo_det.IdArchivo AND archivo.estado = 'A'
                               GROUP BY archivo.IdEmpresa, archivo.IdNomina, archivo.IdNominaTipo, archivo.IdPeriodo, archivo_det.IdEmpleado, archivo_det.IdSucursal
                               UNION ALL
                               SELECT        nom_pa_cheq.IdEmpresa, nom_pa_cheq.IdNomina_Tipo, nom_pa_cheq.IdNomina_TipoLiqui, nom_pa_cheq.IdPeriodo, nom_pa_cheq_det.IdSucursal, nom_pa_cheq_det.IdEmpleado, SUM(nom_pa_cheq_det.Valor) 
                                                        AS Expr1
                               FROM            dbo.ro_NominasPagosCheques AS nom_pa_cheq INNER JOIN
                                                        dbo.ro_NominasPagosCheques_det AS nom_pa_cheq_det ON nom_pa_cheq.IdEmpresa = nom_pa_cheq_det.IdEmpresa AND nom_pa_cheq.IdTransaccion = nom_pa_cheq_det.IdTransaccion
                               WHERE        (nom_pa_cheq.Estado = 1)
                               GROUP BY nom_pa_cheq.IdEmpresa, nom_pa_cheq.IdNomina_Tipo, nom_pa_cheq.IdNomina_TipoLiqui, nom_pa_cheq.IdPeriodo, nom_pa_cheq_det.IdEmpleado, nom_pa_cheq_det.IdSucursal) AS pago ON 
                         pago.IdEmpresa = rol_1.IdEmpresa AND pago.IdNomina = rol_1.IdNominaTipo AND pago.IdNominaTipo = rol_1.IdNominaTipoLiqui AND pago.IdPeriodo = rol_1.IdPeriodo AND pago.IdEmpleado = rol_1.IdEmpleado AND 
                         pago.IdSucursal = rol_1.IdSucursal

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_rol_detalle_saldo_por_pagar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[11] 2[48] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pago"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 136
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rol_1"
            Begin Extent = 
               Top = 6
               Left = 486
               Bottom = 136
               Right = 730
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_rol_detalle_saldo_por_pagar';




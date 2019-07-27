CREATE VIEW dbo.vwRo_Division_Area_dep_rubro
AS
SELECT        dbo.ro_Division.IdEmpresa, dbo.ro_Division.IdDivision, dbo.ro_Division.Descripcion AS DescripcionDiv, dbo.ro_area.IdArea, dbo.ro_area.Descripcion AS DescripcionArea, dbo.ro_Departamento.IdDepartamento, 
                         dbo.ro_Departamento.de_descripcion, dbo.ro_rubro_tipo.IdRubro, dbo.ro_rubro_tipo.rub_codigo, dbo.ro_rubro_tipo.ru_descripcion, dbo.ro_rubro_tipo.ru_estado, dbo.ro_rubro_tipo.ru_tipo, dbo.ro_rubro_tipo.rub_concep, 
                         dbo.ro_rubro_tipo.rub_ctacon, dbo.ro_rubro_tipo.rub_nocontab, dbo.ro_rubro_tipo.rub_provision, ISNULL(dbo.ro_Config_Param_contable.IdCtaCble, dbo.ro_rubro_tipo.rub_ctacon) AS IdCtaCble, 
                         dbo.ro_Config_Param_contable.IdCtaCble_Haber, dbo.ro_Config_Param_contable.DebCre AS DebCre_rrhh, dbo.ro_Config_Param_contable.IdCentroCosto, dbo.ro_rubro_tipo.rub_aplica_IESS, dbo.ro_rubro_tipo.rub_grupo, 
                         ct_plancta_1.pc_Naturaleza AS DebCre, dbo.ct_plancta.IdCtaCble + ' - ' + dbo.ct_plancta.pc_Cuenta AS pc_Cuenta, ct_plancta_1.IdCtaCble + ' - ' + ct_plancta_1.pc_Cuenta AS pc_Cuenta_prov_debito, 
                         dbo.ro_rubro_tipo.rub_ContPorEmpleado, dbo.ro_rubro_tipo.se_distribuye
FROM            dbo.ro_Config_Param_contable LEFT OUTER JOIN
                         dbo.ct_plancta ON dbo.ro_Config_Param_contable.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ro_Config_Param_contable.IdEmpresa = dbo.ct_plancta.IdEmpresa AND 
                         dbo.ro_Config_Param_contable.IdCtaCble_Haber = dbo.ct_plancta.IdCtaCble LEFT OUTER JOIN
                         dbo.ct_plancta AS ct_plancta_1 ON dbo.ro_Config_Param_contable.IdCtaCble = ct_plancta_1.IdCtaCble AND dbo.ro_Config_Param_contable.IdEmpresa = ct_plancta_1.IdEmpresa FULL OUTER JOIN
                         dbo.ro_area RIGHT OUTER JOIN
                         dbo.ro_Departamento INNER JOIN
                         dbo.ro_Division INNER JOIN
                         dbo.ro_area_x_departamento ON dbo.ro_Division.IdEmpresa = dbo.ro_area_x_departamento.IdEmpresa AND dbo.ro_Division.IdDivision = dbo.ro_area_x_departamento.IdDivision ON 
                         dbo.ro_Departamento.IdEmpresa = dbo.ro_area_x_departamento.IdEmpresa AND dbo.ro_Departamento.IdDepartamento = dbo.ro_area_x_departamento.IdDepartamento LEFT OUTER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_area_x_departamento.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa ON dbo.ro_area.IdEmpresa = dbo.ro_area_x_departamento.IdEmpresa AND 
                         dbo.ro_area.IdDivision = dbo.ro_area_x_departamento.IdDivision AND dbo.ro_area.IdArea = dbo.ro_area_x_departamento.IdArea ON dbo.ro_Config_Param_contable.IdRubro = dbo.ro_rubro_tipo.IdRubro AND 
                         dbo.ro_Config_Param_contable.IdEmpresa = dbo.ro_area_x_departamento.IdEmpresa AND dbo.ro_Config_Param_contable.IdDivision = dbo.ro_area_x_departamento.IdDivision AND 
                         dbo.ro_Config_Param_contable.IdArea = dbo.ro_area_x_departamento.IdArea AND dbo.ro_Config_Param_contable.IdDepartamento = dbo.ro_area_x_departamento.IdDepartamento
WHERE        (dbo.ro_rubro_tipo.rub_nocontab = 1)

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[58] 4[34] 2[5] 3) )"
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
         Begin Table = "ro_Config_Param_contable"
            Begin Extent = 
               Top = 0
               Left = 193
               Bottom = 311
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_plancta"
            Begin Extent = 
               Top = 216
               Left = 565
               Bottom = 498
               Right = 748
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_plancta_1"
            Begin Extent = 
               Top = 122
               Left = 836
               Bottom = 410
               Right = 1019
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_area"
            Begin Extent = 
               Top = 116
               Left = 1136
               Bottom = 246
               Right = 1315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Departamento"
            Begin Extent = 
               Top = 354
               Left = 1017
               Bottom = 484
               Right = 1196
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Division"
            Begin Extent = 
               Top = 6
               Left = 909
               Bottom = 136
               Right = 1088
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_area_x_departamento"
            Begin Extent = 
               Top = 267
               Left = 1444
               Bottom = 493
               ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_Division_Area_dep_rubro';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_Division_Area_dep_rubro';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Right = 1641
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 45
               Left = 95
               Bottom = 331
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 14
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 28
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2700
         Alias = 2490
         Table = 1725
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_Division_Area_dep_rubro';


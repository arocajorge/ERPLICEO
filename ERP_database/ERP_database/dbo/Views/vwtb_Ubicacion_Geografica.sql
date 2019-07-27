CREATE VIEW [dbo].[vwtb_Ubicacion_Geografica]
AS
SELECT        'PA-' + IdPais AS Id, NULL AS IdPadre, CodPais AS Codigo, Nombre, Nacionalidad, estado AS Estado, 1 AS Nivel, IdPais AS IdPais, NULL AS IdProvincia, NULL 
                         AS IdCiudad
FROM            tb_pais
UNION
SELECT        'PR-' + pro.IdProvincia AS Id, 'PA-' + pro.IdPais AS IdPadre, pro.Cod_Provincia AS Codigo, pro.Descripcion_Prov AS Nombre, NULL AS Nacionalidad, pro.Estado, 
                         2 AS Nivel, pro.IdPais AS IdPais, pro.IdProvincia AS IdProvincia, NULL AS IdCiudad
FROM            tb_provincia pro INNER JOIN
                         tb_pais pai ON pro.IdPais = pai.IdPais
UNION
SELECT        'CI-' + ciu.IdCiudad AS Id, 'PR-' + ciu.IdProvincia AS IdPadre, ciu.Cod_Ciudad AS Codigo, ciu.Descripcion_Ciudad AS Nombre, NULL AS Nacionalidad, ciu.Estado, 
                         3 AS Nivel, pro.IdPais AS IdPais, ciu.IdProvincia AS IdProvincia, ciu.IdCiudad AS IdCiudad
FROM            tb_ciudad ciu INNER JOIN
                         tb_provincia pro ON ciu.IdProvincia = pro.IdProvincia
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_Ubicacion_Geografica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_Ubicacion_Geografica';


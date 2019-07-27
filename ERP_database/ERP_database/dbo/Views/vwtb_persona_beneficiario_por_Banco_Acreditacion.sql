CREATE VIEW [dbo].[vwtb_persona_beneficiario_por_Banco_Acreditacion]
AS
SELECT        dbo.vwtb_persona_beneficiario.IdEmpresa, dbo.vwtb_persona_beneficiario.IdBeneficiario, dbo.vwtb_persona_beneficiario.IdTipo_Persona, 
                         dbo.vwtb_persona_beneficiario.IdPersona, dbo.vwtb_persona_beneficiario.IdEntidad, dbo.vwtb_persona_beneficiario.Codigo, 
                         dbo.vwtb_persona_beneficiario.Nombre, dbo.vwtb_persona_beneficiario.pr_girar_cheque_a, dbo.vwtb_persona_beneficiario.pe_razonSocial, 
                         dbo.vwtb_persona_beneficiario.pe_cedulaRuc, dbo.vwtb_persona_beneficiario.pe_Naturaleza, dbo.vwtb_persona_beneficiario.IdCtaCble, 
                         dbo.vwtb_persona_beneficiario.IdCentroCosto, dbo.vwtb_persona_beneficiario.IdSubCentroCosto, dbo.vwtb_persona_beneficiario.IdCtaCble_Anticipo, 
                         dbo.vwtb_persona_beneficiario.IdCtaCble_Gasto, dbo.vwtb_persona_beneficiario.Estado, dbo.vwtb_persona_beneficiario.IdTipoCta_acreditacion_cat, 
                         dbo.vwtb_persona_beneficiario.num_cta_acreditacion, dbo.vwtb_persona_beneficiario.IdBanco_acreditacion, dbo.vwtb_persona_beneficiario.pe_apellido, 
                         dbo.vwtb_persona_beneficiario.pe_nombre, dbo.vwtb_persona_beneficiario.pe_nombreCompleto, dbo.vwtb_persona_beneficiario.IdTipoDocumento, 
                         dbo.vwtb_persona_beneficiario.pe_direccion, dbo.vwtb_persona_beneficiario.pe_telefonoCasa, dbo.vwtb_persona_beneficiario.pe_celular, 
                         dbo.vwtb_persona_beneficiario.pe_correo, dbo.tb_banco.CodigoLegal, dbo.tb_banco.TieneFormatoTransferencia, dbo.tb_banco.ba_descripcion
FROM            dbo.vwtb_persona_beneficiario LEFT OUTER JOIN
                         dbo.tb_banco ON dbo.vwtb_persona_beneficiario.IdBanco_acreditacion = dbo.tb_banco.IdBanco
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[8] 3) )"
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
         Begin Table = "vwtb_persona_beneficiario"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 195
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 18
         End
         Begin Table = "tb_banco"
            Begin Extent = 
               Top = 2
               Left = 390
               Bottom = 186
               Right = 624
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
      Begin ColumnWidths = 9
         Width = 284
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_persona_beneficiario_por_Banco_Acreditacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_persona_beneficiario_por_Banco_Acreditacion';


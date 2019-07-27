CREATE VIEW dbo.vwtb_persona_beneficiario
AS
SELECT        prov.IdEmpresa, 'PROVEE' + '-' + CAST(prov.IdPersona AS varchar(20)) + '-' + CAST(prov.IdProveedor AS varchar(20)) AS IdBeneficiario, 'PROVEE' AS IdTipo_Persona, prov.IdPersona, 
                         prov.IdProveedor AS IdEntidad, prov.pr_codigo AS Codigo, pers.pe_nombreCompleto AS Nombre, pers.pe_nombreCompleto pr_girar_cheque_a, pers.pe_razonSocial, pers.pe_cedulaRuc, pers.pe_Naturaleza, prov.IdCtaCble_CXP AS IdCtaCble, 
                         null AS IdCentroCosto, NULL AS IdSubCentroCosto, null IdCtaCble_Anticipo, prov.IdCtaCble_Gasto, prov.pr_estado AS Estado, prov.IdTipoCta_acreditacion_cat, prov.num_cta_acreditacion, 
                         prov.IdBanco_acreditacion, pers.pe_apellido, pers.pe_nombre, pers.pe_nombreCompleto, pers.IdTipoDocumento, pers.pe_direccion, null pe_telefonoCasa, pers.pe_celular, pers.pe_correo
FROM            tb_persona AS pers INNER JOIN
                         cp_proveedor AS prov ON pers.IdPersona = prov.IdPersona
UNION
SELECT        cli.IdEmpresa, 'CLIENTE' + '-' + CAST(pers.IdPersona AS varchar(20)) + '-' + CAST(cli.IdCliente AS varchar(20)) AS IdBeneficiario, 'CLIENTE' AS IdTipo_Persona, pers.IdPersona, cli.IdCliente, cli.Codigo, 
                         pers.pe_nombreCompleto, pers.pe_nombreCompleto AS Girado_a, pers.pe_razonSocial, pers.pe_cedulaRuc, pers.pe_Naturaleza, cli.IdCtaCble_cxc_Credito IdCtaCble_cxc, NULL AS Expr2, NULL AS Expr3, cli.IdCtaCble_cxc_Credito AS Expr4, 
                         cli.IdCtaCble_cxc_Credito AS Expr5, cli.Estado, pers.IdTipoCta_acreditacion_cat, pers.num_cta_acreditacion, pers.IdBanco_acreditacion, pers.pe_apellido, pers.pe_nombre, pers.pe_nombreCompleto AS Expr6, 
                         pers.IdTipoDocumento, pers.pe_direccion, null pe_telefonoCasa, pers.pe_celular, pers.pe_correo
FROM            tb_persona AS pers INNER JOIN
                         fa_cliente AS cli ON pers.IdPersona = cli.IdPersona
UNION
SELECT        emp.IdEmpresa, 'EMPLEA' + '-' + CAST(pers.IdPersona AS varchar(20)) + '-' + CAST(emp.IdEmpleado AS varchar(20)) AS IdBeneficiario, 'EMPLEA' AS IdTipo_Persona, pers.IdPersona, emp.IdEmpleado, 
                         emp.em_codigo, pers.pe_nombreCompleto, pers.pe_nombreCompleto AS Expr1, pers.pe_razonSocial, pers.pe_cedulaRuc, pers.pe_Naturaleza, emp.IdCtaCble_Emplea, NULL IdCentroCosto, 
                         NULL IdCentroCosto_sub_centro_costo, emp.IdCtaCble_Emplea AS Expr2, emp.IdCtaCble_Emplea AS Expr3, emp.em_estado, emp.em_tipoCta, emp.em_NumCta, emp.IdBanco, pers.pe_apellido, 
                         pers.pe_nombre, pers.pe_nombreCompleto AS Expr4, pers.IdTipoDocumento, pers.pe_direccion, null pe_telefonoCasa, pers.pe_celular, pers.pe_correo
FROM            tb_persona AS pers INNER JOIN
                         ro_empleado AS emp ON pers.IdPersona = emp.IdPersona
UNION
SELECT        em.IdEmpresa, 'PERSONA' + '-' + CAST(B.IdPersona AS varchar(20)) + '-' + CAST(B.IdPersona AS varchar(20)) AS IdBeneficiario, 'PERSONA' AS IdTipo_Persona, B.IdPersona, B.IdPersona AS Expr1, B.CodPersona, 
                         B.pe_nombreCompleto, B.pe_nombreCompleto AS Expr2, B.pe_razonSocial, B.pe_cedulaRuc, B.pe_Naturaleza, NULL AS Expr3, NULL AS Expr4, NULL AS Expr5, NULL AS Expr6, NULL AS Expr7, B.pe_estado, 
                         B.IdTipoCta_acreditacion_cat, B.num_cta_acreditacion, B.IdBanco_acreditacion, B.pe_apellido, B.pe_nombre, B.pe_nombreCompleto AS Expr8, B.IdTipoDocumento, B.pe_direccion, null pe_telefonoCasa, 
                         B.pe_celular, B.pe_correo
FROM            tb_persona AS B CROSS JOIN
                         tb_empresa AS em
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[6] 4[4] 2[72] 3) )"
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
         Top = -288
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_persona_beneficiario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_persona_beneficiario';


CREATE VIEW dbo.vwro_empleado_datos_generales
AS
SELECT        dbo.ro_empleado.IdEmpresa, dbo.ro_empleado.IdEmpleado_Supervisor, dbo.ro_empleado.IdPersona, dbo.ro_empleado.IdSucursal, dbo.ro_empleado.IdTipoEmpleado, dbo.ro_empleado.em_codigo, 
                         dbo.ro_empleado.Codigo_Biometrico, dbo.ro_empleado.em_lugarNacimiento, dbo.ro_empleado.em_CarnetIees, dbo.ro_empleado.em_cedulaMil, dbo.ro_empleado.em_fechaSalida, dbo.ro_empleado.em_fechaIngaRol, 
                         dbo.ro_empleado.em_tipoCta, dbo.ro_empleado.em_NumCta, dbo.ro_empleado.em_estado, dbo.ro_empleado.IdCodSectorial, dbo.ro_empleado.IdDepartamento, dbo.ro_empleado.IdTipoSangre, dbo.ro_empleado.IdCargo, 
                         dbo.ro_empleado.IdCtaCble_Emplea, dbo.ro_empleado.IdCiudad, dbo.ro_empleado.em_mail, dbo.ro_empleado.IdTipoLicencia, dbo.ro_empleado.IdBanco, dbo.ro_empleado.IdArea, dbo.ro_empleado.IdDivision, 
                         dbo.ro_empleado.por_discapacidad, dbo.ro_empleado.carnet_conadis, dbo.ro_empleado.talla_pant, dbo.ro_empleado.talla_camisa, dbo.ro_empleado.talla_zapato, dbo.ro_empleado.em_status, 
                         dbo.ro_empleado.IdCondicionDiscapacidadSRI, dbo.ro_empleado.IdTipoIdentDiscapacitadoSustitutoSRI, dbo.ro_empleado.IdentDiscapacitadoSustitutoSRI, dbo.ro_empleado.IdAplicaConvenioDobleImposicionSRI, 
                         dbo.ro_empleado.IdTipoResidenciaSRI, dbo.ro_empleado.IdTipoSistemaSalarioNetoSRI, dbo.ro_empleado.es_AcreditaHorasExtras, dbo.ro_empleado.IdTipoAnticipo, dbo.ro_empleado.ValorAnticipo, 
                         dbo.ro_empleado.CodigoSectorial, dbo.ro_empleado.em_AnticipoSueldo, dbo.ro_empleado.Marca_Biometrico, dbo.ro_empleado.IdHorario, dbo.tb_persona.pe_Naturaleza, dbo.tb_persona.pe_nombre, 
                         dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_direccion, dbo.tb_persona.pe_telfono_Contacto, dbo.tb_persona.pe_celular, dbo.tb_persona.pe_correo, dbo.tb_persona.pe_sexo, 
                         dbo.tb_persona.IdEstadoCivil, dbo.tb_persona.pe_fechaNacimiento, dbo.tb_sucursal.Su_Descripcion, dbo.ro_Departamento.de_descripcion, dbo.ro_cargo.ca_descripcion, dbo.ro_Division.Descripcion, 
                         dbo.ro_empleado.IdEmpleado, dbo.tb_persona.pe_apellido, dbo.ro_empleado.Tiene_ingresos_compartidos, dbo.ro_empleado.Pago_por_horas, dbo.ro_empleado.Valor_horas_vespertina, 
                         dbo.ro_empleado.Valor_horas_matutino, dbo.ro_empleado.Valor_maximo_horas_vesp, dbo.ro_empleado.Valor_maximo_horas_mat, dbo.ro_empleado.Valor_horas_brigada, dbo.ro_empleado.GozaMasDeQuinceDiasVaciones, 
                         dbo.ro_empleado.DiasVacaciones, dbo.ro_empleado.IdEmpleadoPAdre, dbo.ro_empleado.Valor_hora_adicionales, dbo.ro_empleado.Valor_hora_control_salida, dbo.ro_empleado.CodCatalogo_Ubicacion, 
                         dbo.ro_empleado.IdCtaCble_x_pagar_empleado, dbo.ro_empleado.IdSucursalContabilizacion
FROM            dbo.tb_persona INNER JOIN
                         dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo AND dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND 
                         dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento AND 
                         dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_empleado.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_empleado.IdSucursal = dbo.tb_sucursal.IdSucursal AND dbo.ro_empleado.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND 
                         dbo.ro_empleado.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                         dbo.ro_Division ON dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[77] 4[3] 2[20] 3) )"
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
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 446
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 10
               Left = 278
               Bottom = 409
               Right = 564
            End
            DisplayFlags = 280
            TopColumn = 49
         End
         Begin Table = "ro_cargo"
            Begin Extent = 
               Top = 0
               Left = 669
               Bottom = 130
               Right = 886
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Departamento"
            Begin Extent = 
               Top = 6
               Left = 987
               Bottom = 136
               Right = 1166
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 138
               Left = 961
               Bottom = 391
               Right = 1179
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Division"
            Begin Extent = 
               Top = 232
               Left = 794
               Bottom = 362
               Right = 973
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
      Begin ColumnWidths = 104
         Width = 284
         Width = 1500
  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_datos_generales';






















GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_datos_generales';














GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_datos_generales';


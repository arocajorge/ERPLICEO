create view EntidadRegulatoria.vwrdep_gastos_x_rubros_deducibles as 
SELECT        IdEmpresa, IdEmpleado, AnioFiscal, ISNULL([ALI], 0) AS GastoAlimentacion, ISNULL([EDU], 0) AS GastoEucacion, ISNULL([SA], 0) AS GastoSalud, ISNULL([VES], 0) AS GastoVestimenta, ISNULL([VIV], 0) AS GastoVivienda
FROM            (SELECT        gastos.IdEmpresa, gastos.IdEmpleado, gastos.AnioFiscal, gastos_det.IdTipoGasto, gastos_det.Valor
                          FROM            dbo.ro_empleado_proyeccion_gastos AS gastos INNER JOIN
                                                    dbo.ro_empleado_proyeccion_gastos_det AS gastos_det ON gastos.IdEmpresa = gastos_det.IdEmpresa AND gastos.IdTransaccion = gastos_det.IdTransaccion
                          GROUP BY gastos.IdEmpresa, gastos.IdEmpleado, gastos.AnioFiscal, gastos_det.IdTipoGasto, gastos_det.Valor) AS s PIVOT (sum([Valor]) FOR [IdTipoGasto] IN ([ALI], [EDU], [SA], [VES], [VIV])) AS pvt
CREATE VIEW ba_Cbte_Ban_beneficiario
AS
SELECT ba_Cbte_Ban.IdEmpresa, ba_Cbte_Ban.IdTipocbte, ba_Cbte_Ban.IdCbteCble, tb_persona.IdPersona, tb_persona.pe_nombreCompleto
FROM     cp_proveedor INNER JOIN
                  tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
                  ba_Cbte_Ban ON cp_proveedor.IdProveedor = ba_Cbte_Ban.IdEntidad AND cp_proveedor.IdEmpresa = ba_Cbte_Ban.IdEmpresa
where cb_Cheque is null
UNION
SELECT ba_Cbte_Ban.IdEmpresa, ba_Cbte_Ban.IdTipocbte, ba_Cbte_Ban.IdCbteCble, tb_persona.IdPersona, tb_persona.pe_nombreCompleto
FROM     tb_persona INNER JOIN
                  ba_Cbte_Ban ON tb_persona.IdPersona = ba_Cbte_Ban.IdPersona_Girado_a
WHERE cb_Cheque is not null
/*union
SELECT ba_Cbte_Ban.IdEmpresa, ba_Cbte_Ban.IdTipocbte, ba_Cbte_Ban.IdCbteCble, tb_persona.IdPersona, tb_persona.pe_nombreCompleto
FROM     ro_empleado INNER JOIN
                  tb_persona ON ro_empleado.IdPersona = tb_persona.IdPersona INNER JOIN
                  ba_Archivo_Transferencia_Det ON ro_empleado.IdEmpresa = ba_Archivo_Transferencia_Det.IdEmpresa AND ro_empleado.IdEmpleado = ba_Archivo_Transferencia_Det.IdEmpleado INNER JOIN
                  ba_Cbte_Ban ON ba_Archivo_Transferencia_Det.IdEmpresa_pago = ba_Cbte_Ban.IdEmpresa AND ba_Archivo_Transferencia_Det.IdTipoCbte_pago = ba_Cbte_Ban.IdTipocbte AND 
                  ba_Archivo_Transferencia_Det.IdCbteCble_pago = ba_Cbte_Ban.IdCbteCble*/
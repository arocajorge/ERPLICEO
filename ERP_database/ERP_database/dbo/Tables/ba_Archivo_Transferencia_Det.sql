CREATE TABLE [dbo].[ba_Archivo_Transferencia_Det] (
    [IdEmpresa]                INT           NOT NULL,
    [IdArchivo]                NUMERIC (18)  NOT NULL,
    [Secuencia]                INT           NOT NULL,
    [IdEmpresa_OP]             INT           NOT NULL,
    [IdOrdenPago]              NUMERIC (18)  NOT NULL,
    [Secuencia_OP]             INT           NOT NULL,
    [Estado]                   BIT           NOT NULL,
    [Valor]                    FLOAT (53)    NOT NULL,
    [Secuencial_reg_x_proceso] NUMERIC (18)  NOT NULL,
    [Referencia]               VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ba_Archivo_Transferencia_Det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdArchivo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_Archivo_Transferencia_Det_ba_Archivo_Transferencia1] FOREIGN KEY ([IdEmpresa], [IdArchivo]) REFERENCES [dbo].[ba_Archivo_Transferencia] ([IdEmpresa], [IdArchivo]),
    CONSTRAINT [FK_ba_Archivo_Transferencia_Det_cp_orden_pago_det] FOREIGN KEY ([IdEmpresa_OP], [IdOrdenPago], [Secuencia_OP]) REFERENCES [dbo].[cp_orden_pago_det] ([IdEmpresa], [IdOrdenPago], [Secuencia])
);








GO
CREATE NONCLUSTERED INDEX [IX_ba_Archivo_Transferencia_Det_1]
    ON [dbo].[ba_Archivo_Transferencia_Det]([IdEmpresa_OP] ASC, [IdOrdenPago] ASC, [Secuencia_OP] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ba_Archivo_Transferencia_Det]
    ON [dbo].[ba_Archivo_Transferencia_Det]([IdEmpresa] ASC, [IdArchivo] ASC, [Secuencia] ASC);


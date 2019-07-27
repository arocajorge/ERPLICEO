CREATE TABLE [dbo].[ba_archivo_transferencia_x_ba_tipo_flujo] (
    [IdEmpresa]   INT          NOT NULL,
    [IdArchivo]   NUMERIC (18) NOT NULL,
    [Secuencia]   INT          NOT NULL,
    [IdTipoFlujo] NUMERIC (18) NOT NULL,
    [Porcentaje]  FLOAT (53)   NOT NULL,
    [Valor]       FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_ba_archivo_transferencia_x_ba_tipo_flujo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdArchivo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_archivo_transferencia_x_ba_tipo_flujo_ba_Archivo_Transferencia] FOREIGN KEY ([IdEmpresa], [IdArchivo]) REFERENCES [dbo].[ba_Archivo_Transferencia] ([IdEmpresa], [IdArchivo]),
    CONSTRAINT [FK_ba_archivo_transferencia_x_ba_tipo_flujo_ba_TipoFlujo] FOREIGN KEY ([IdEmpresa], [IdTipoFlujo]) REFERENCES [dbo].[ba_TipoFlujo] ([IdEmpresa], [IdTipoFlujo])
);


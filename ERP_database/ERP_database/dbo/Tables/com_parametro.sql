CREATE TABLE [dbo].[com_parametro] (
    [IdEmpresa]                INT          NOT NULL,
    [IdEstadoAprobacion_OC]    VARCHAR (25) NOT NULL,
    [IdMovi_inven_tipo_OC]     INT          NOT NULL,
    [IdEstadoAnulacion_OC]     VARCHAR (25) NOT NULL,
    [IdEstado_cierre]          VARCHAR (25) NOT NULL,
    [DiasTransaccionesAFuturo] INT          NOT NULL,
    CONSTRAINT [PK_com_parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_com_parametro_com_catalogo] FOREIGN KEY ([IdEstadoAprobacion_OC]) REFERENCES [dbo].[com_catalogo] ([IdCatalogocompra]),
    CONSTRAINT [FK_com_parametro_com_catalogo1] FOREIGN KEY ([IdEstadoAnulacion_OC]) REFERENCES [dbo].[com_catalogo] ([IdCatalogocompra]),
    CONSTRAINT [FK_com_parametro_com_estado_cierre] FOREIGN KEY ([IdEstado_cierre]) REFERENCES [dbo].[com_estado_cierre] ([IdEstado_cierre]),
    CONSTRAINT [FK_com_parametro_in_movi_inven_tipo] FOREIGN KEY ([IdEmpresa], [IdMovi_inven_tipo_OC]) REFERENCES [dbo].[in_movi_inven_tipo] ([IdEmpresa], [IdMovi_inven_tipo]),
    CONSTRAINT [FK_com_parametro_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);






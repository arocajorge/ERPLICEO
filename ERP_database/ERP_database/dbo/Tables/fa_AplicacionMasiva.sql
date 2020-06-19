CREATE TABLE [dbo].[fa_AplicacionMasiva] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAplicacion]          NUMERIC (18)  NOT NULL,
    [IdSucursal]            INT           NOT NULL,
    [Fecha]                 DATETIME      NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_fa_AplicacionMasiva] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAplicacion] ASC),
    CONSTRAINT [FK_fa_AplicacionMasiva_cxc_ConciliacionNotaCredito] FOREIGN KEY ([IdEmpresa], [IdAplicacion]) REFERENCES [dbo].[cxc_ConciliacionNotaCredito] ([IdEmpresa], [IdConciliacion])
);


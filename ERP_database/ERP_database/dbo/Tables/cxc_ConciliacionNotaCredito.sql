CREATE TABLE [dbo].[cxc_ConciliacionNotaCredito] (
    [IdEmpresa]             INT           NOT NULL,
    [IdConciliacion]        NUMERIC (18)  NOT NULL,
    [IdSucursal]            INT           NOT NULL,
    [IdBodega]              INT           NOT NULL,
    [IdNota]                NUMERIC (18)  NOT NULL,
    [IdCobro]               NUMERIC (18)  NOT NULL,
    [Fecha]                 DATE          NOT NULL,
    [Valor]                 FLOAT (53)    NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [IdTipoCbte]            INT           NULL,
    [IdCbteCble]            NUMERIC (18)  NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_cxc_ConciliacionNotaCredito] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion] ASC),
    CONSTRAINT [FK_cxc_ConciliacionNotaCredito_ct_cbtecble] FOREIGN KEY ([IdEmpresa], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_cxc_ConciliacionNotaCredito_cxc_cobro] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdCobro]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro]),
    CONSTRAINT [FK_cxc_ConciliacionNotaCredito_fa_notaCreDeb] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota]) REFERENCES [dbo].[fa_notaCreDeb] ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota])
);




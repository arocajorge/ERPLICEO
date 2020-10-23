CREATE TABLE [dbo].[cxc_Convenio_Det] (
    [IdEmpresa]            INT           NOT NULL,
    [IdConvenio]           INT           NOT NULL,
    [NumCuota]             INT           NOT NULL,
    [SaldoInicial]         FLOAT (53)    NOT NULL,
    [TotalCuota]           FLOAT (53)    NOT NULL,
    [Saldo]                FLOAT (53)    NOT NULL,
    [FechaPago]            DATETIME      NOT NULL,
    [Observacion_det]      VARCHAR (MAX) NULL,
    [IdCatalogoEstadoPago] INT           NOT NULL,
    CONSTRAINT [PK_cxc_Convenio_Det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConvenio] ASC, [NumCuota] ASC),
    CONSTRAINT [FK_cxc_Convenio_Det_aca_Catalogo] FOREIGN KEY ([IdCatalogoEstadoPago]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_cxc_Convenio_Det_cxc_Convenio] FOREIGN KEY ([IdEmpresa], [IdConvenio]) REFERENCES [dbo].[cxc_Convenio] ([IdEmpresa], [IdConvenio])
);


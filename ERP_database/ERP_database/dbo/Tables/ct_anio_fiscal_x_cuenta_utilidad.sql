CREATE TABLE [dbo].[ct_anio_fiscal_x_cuenta_utilidad] (
    [IdEmpresa]       INT          NOT NULL,
    [IdanioFiscal]    INT          NOT NULL,
    [IdCtaCble]       VARCHAR (20) NULL,
    [IdCtaCbleCierre] VARCHAR (20) NULL,
    CONSTRAINT [PK_ct_anio_fiscal_x_cuenta_utilidad_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdanioFiscal] ASC),
    CONSTRAINT [FK_ct_anio_fiscal_x_cuenta_utilidad_ct_anio_fiscal] FOREIGN KEY ([IdanioFiscal]) REFERENCES [dbo].[ct_anio_fiscal] ([IdanioFiscal]),
    CONSTRAINT [FK_ct_anio_fiscal_x_cuenta_utilidad_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_ct_anio_fiscal_x_cuenta_utilidad_ct_plancta1] FOREIGN KEY ([IdanioFiscal], [IdCtaCbleCierre]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);






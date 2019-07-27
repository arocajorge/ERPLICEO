CREATE TABLE [dbo].[ct_periodo] (
    [IdEmpresa]    INT           NOT NULL,
    [IdPeriodo]    INT           NOT NULL,
    [IdanioFiscal] INT           NOT NULL,
    [pe_mes]       INT           NOT NULL,
    [pe_FechaIni]  SMALLDATETIME NOT NULL,
    [pe_FechaFin]  SMALLDATETIME NOT NULL,
    [pe_cerrado]   NVARCHAR (1)  NOT NULL,
    [pe_estado]    NVARCHAR (1)  NULL,
    CONSTRAINT [PK_ct_periodo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC),
    CONSTRAINT [FK_ct_periodo_ct_anio_fiscal] FOREIGN KEY ([IdanioFiscal]) REFERENCES [dbo].[ct_anio_fiscal] ([IdanioFiscal]),
    CONSTRAINT [FK_ct_periodo_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_ct_periodo_tb_mes] FOREIGN KEY ([pe_mes]) REFERENCES [dbo].[tb_mes] ([idMes])
);


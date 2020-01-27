CREATE TABLE [dbo].[aca_Reporte_x_tb_empresa] (
    [IdEmpresa]      INT             NOT NULL,
    [CodReporte]     VARCHAR (50)    COLLATE Modern_Spanish_CI_AS NOT NULL,
    [ReporteDisenio] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_aca_reporte_x_tb_empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [CodReporte] ASC),
    CONSTRAINT [FK_aca_Reporte_x_tb_empresa_aca_Reporte] FOREIGN KEY ([CodReporte]) REFERENCES [dbo].[aca_Reporte] ([CodReporte]),
    CONSTRAINT [FK_aca_Reporte_x_tb_empresa_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);


CREATE TABLE [web].[tb_sis_reporte_x_tb_empresa] (
    [IdEmpresa]      INT             NOT NULL,
    [CodReporte]     VARCHAR (50)    NOT NULL,
    [ReporteDisenio] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_tb_sis_reporte_x_tb_empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [CodReporte] ASC),
    CONSTRAINT [FK_tb_sis_reporte_x_tb_empresa_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_tb_sis_reporte_x_tb_empresa_tb_sis_reporte] FOREIGN KEY ([CodReporte]) REFERENCES [web].[tb_sis_reporte] ([CodReporte])
);


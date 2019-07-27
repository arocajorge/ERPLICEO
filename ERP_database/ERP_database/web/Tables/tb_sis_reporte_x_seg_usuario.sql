CREATE TABLE [web].[tb_sis_reporte_x_seg_usuario] (
    [IdUsuario]  VARCHAR (50) NOT NULL,
    [IdEmpresa]  INT          NOT NULL,
    [CodReporte] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tb_sis_reporte_x_seg_usuario] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEmpresa] ASC, [CodReporte] ASC),
    CONSTRAINT [FK_tb_sis_reporte_x_seg_usuario_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario]),
    CONSTRAINT [FK_tb_sis_reporte_x_seg_usuario_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_tb_sis_reporte_x_seg_usuario_tb_sis_reporte] FOREIGN KEY ([CodReporte]) REFERENCES [web].[tb_sis_reporte] ([CodReporte])
);


CREATE TABLE [dbo].[seg_usuario_x_tb_sis_reporte] (
    [IdUsuario]   VARCHAR (50) NOT NULL,
    [CodReporte]  VARCHAR (50) NOT NULL,
    [observacion] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_seg_usuario_x_tb_sis_reporte] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [CodReporte] ASC),
    CONSTRAINT [FK_seg_usuario_x_tb_sis_reporte_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario]),
    CONSTRAINT [FK_seg_usuario_x_tb_sis_reporte_tb_sis_reporte1] FOREIGN KEY ([CodReporte]) REFERENCES [web].[tb_sis_reporte] ([CodReporte])
);




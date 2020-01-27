CREATE TABLE [dbo].[aca_Reporte_x_seg_usuario] (
    [IdUsuario]  VARCHAR (50) NOT NULL,
    [IdEmpresa]  INT          NOT NULL,
    [CodReporte] VARCHAR (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
    CONSTRAINT [PK_aca_Reporte_x_seg_usuario] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEmpresa] ASC, [CodReporte] ASC),
    CONSTRAINT [FK_aca_Reporte_x_seg_usuario_aca_Reporte] FOREIGN KEY ([CodReporte]) REFERENCES [dbo].[aca_Reporte] ([CodReporte]),
    CONSTRAINT [FK_aca_Reporte_x_seg_usuario_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario]),
    CONSTRAINT [FK_aca_Reporte_x_seg_usuario_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);


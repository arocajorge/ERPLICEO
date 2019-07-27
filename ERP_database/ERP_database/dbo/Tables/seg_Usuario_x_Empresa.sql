CREATE TABLE [dbo].[seg_Usuario_x_Empresa] (
    [IdUsuario]   VARCHAR (50) NOT NULL,
    [IdEmpresa]   INT          NOT NULL,
    [Observacion] VARCHAR (50) NULL,
    CONSTRAINT [PK_seg_Usuario_x_Empresa] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEmpresa] ASC),
    CONSTRAINT [FK_seg_Usuario_x_Empresa_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario]),
    CONSTRAINT [FK_seg_Usuario_x_Empresa_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);


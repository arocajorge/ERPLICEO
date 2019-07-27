CREATE TABLE [dbo].[seg_Menu_x_Empresa_x_Usuario] (
    [IdEmpresa]   INT          NOT NULL,
    [IdUsuario]   VARCHAR (50) NOT NULL,
    [IdMenu]      INT          NOT NULL,
    [Lectura]     BIT          NOT NULL,
    [Escritura]   BIT          NOT NULL,
    [Eliminacion] BIT          NOT NULL,
    CONSTRAINT [PK_seg_Menu_x_Empresa_x_Usuario] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdUsuario] ASC, [IdMenu] ASC),
    CONSTRAINT [FK_seg_Menu_x_Empresa_x_Usuario_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario])
);


CREATE TABLE [dbo].[seg_usuario_x_tb_sucursal] (
    [IdUsuario]   VARCHAR (50) NOT NULL,
    [IdEmpresa]   INT          NOT NULL,
    [IdSucursal]  INT          NOT NULL,
    [Observacion] VARCHAR (10) NULL,
    CONSTRAINT [PK_seg_usuario_x_tb_sucursal] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdEmpresa] ASC, [IdSucursal] ASC),
    CONSTRAINT [FK_seg_usuario_x_tb_sucursal_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario]),
    CONSTRAINT [FK_seg_usuario_x_tb_sucursal_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);


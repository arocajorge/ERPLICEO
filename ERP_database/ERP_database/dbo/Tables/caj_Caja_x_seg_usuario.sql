CREATE TABLE [dbo].[caj_Caja_x_seg_usuario] (
    [IdEmpresa] INT          NOT NULL,
    [IdCaja]    INT          NOT NULL,
    [Secuencia] INT          NOT NULL,
    [IdUsuario] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_caj_Caja_x_seg_usuario] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCaja] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_caj_Caja_x_seg_usuario_caj_Caja] FOREIGN KEY ([IdEmpresa], [IdCaja]) REFERENCES [dbo].[caj_Caja] ([IdEmpresa], [IdCaja]),
    CONSTRAINT [FK_caj_Caja_x_seg_usuario_seg_usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[seg_usuario] ([IdUsuario])
);


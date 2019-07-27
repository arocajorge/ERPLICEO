CREATE TABLE [dbo].[tb_sucursal_FormaPago_x_fa_NivelDescuento] (
    [IdEmpresa]  INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [Secuencia]  INT          NOT NULL,
    [IdCatalogo] VARCHAR (15) NOT NULL,
    [IdNivel]    INT          NOT NULL,
    CONSTRAINT [PK_tb_sucursal_FormaPago_x_fa_NivelDescuento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_tb_sucursal_FormaPago_x_fa_NivelDescuento_fa_catalogo] FOREIGN KEY ([IdCatalogo]) REFERENCES [dbo].[fa_catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_tb_sucursal_FormaPago_x_fa_NivelDescuento_fa_NivelDescuento] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[fa_NivelDescuento] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_tb_sucursal_FormaPago_x_fa_NivelDescuento_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);


CREATE TABLE [dbo].[fa_cliente_x_fa_Vendedor_x_sucursal] (
    [IdEmpresa]   INT          NOT NULL,
    [IdCliente]   NUMERIC (18) NOT NULL,
    [IdSucursal]  INT          NOT NULL,
    [IdVendedor]  INT          NOT NULL,
    [observacion] VARCHAR (2)  NULL,
    CONSTRAINT [PK_fa_cliente_x_fa_Vendedor_x_sucursal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCliente] ASC, [IdSucursal] ASC, [IdVendedor] ASC),
    CONSTRAINT [FK_fa_cliente_x_fa_Vendedor_x_sucursal_fa_cliente] FOREIGN KEY ([IdEmpresa], [IdCliente]) REFERENCES [dbo].[fa_cliente] ([IdEmpresa], [IdCliente]),
    CONSTRAINT [FK_fa_cliente_x_fa_Vendedor_x_sucursal_fa_Vendedor] FOREIGN KEY ([IdEmpresa], [IdVendedor]) REFERENCES [dbo].[fa_Vendedor] ([IdEmpresa], [IdVendedor]),
    CONSTRAINT [FK_fa_cliente_x_fa_Vendedor_x_sucursal_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);


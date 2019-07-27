CREATE TABLE [dbo].[ba_Banco_Cuenta_x_tb_sucursal] (
    [IdEmpresa]  INT NOT NULL,
    [IdBanco]    INT NOT NULL,
    [Secuencia]  INT NOT NULL,
    [IdSucursal] INT NOT NULL,
    CONSTRAINT [PK_ba_Banco_Cuenta_x_tb_sucursal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdBanco] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_Banco_Cuenta_x_tb_sucursal_ba_Banco_Cuenta] FOREIGN KEY ([IdEmpresa], [IdBanco]) REFERENCES [dbo].[ba_Banco_Cuenta] ([IdEmpresa], [IdBanco]),
    CONSTRAINT [FK_ba_Banco_Cuenta_x_tb_sucursal_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);


CREATE TABLE [dbo].[ct_CierrePorModuloPorSucursal] (
    [IdEmpresa]  INT          NOT NULL,
    [IdCierre]   INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [CodModulo]  VARCHAR (20) NOT NULL,
    [FechaIni]   DATE         NOT NULL,
    [FechaFin]   DATE         NOT NULL,
    [Cerrado]    BIT          NOT NULL,
    CONSTRAINT [PK_ct_CierrePorModuloPorSucursal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCierre] ASC),
    CONSTRAINT [FK_ct_CierrePorModuloPorSucursal_tb_modulo] FOREIGN KEY ([CodModulo]) REFERENCES [dbo].[tb_modulo] ([CodModulo]),
    CONSTRAINT [FK_ct_CierrePorModuloPorSucursal_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);


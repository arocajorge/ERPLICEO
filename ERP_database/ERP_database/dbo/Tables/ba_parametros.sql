CREATE TABLE [dbo].[ba_parametros] (
    [IdEmpresa]                     INT          NOT NULL,
    [CiudadDefaultParaCrearCheques] VARCHAR (25) NULL,
    [DiasTransaccionesAFuturo]      INT          NOT NULL,
    [CantidadChequesAlerta]         INT          NULL,
    [PermitirSobreGiro]             BIT          NOT NULL,
    [ValidarSoloCuentasArchivo]     BIT          NOT NULL,
    [IdUsuario]                     VARCHAR (20) NULL,
    [FechaTransac]                  DATETIME     NULL,
    [IdUsuarioUltMod]               VARCHAR (20) NULL,
    [FechaUltMod]                   DATETIME     NULL,
    CONSTRAINT [PK_ba_parametros] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_ba_parametros_tb_ciudad] FOREIGN KEY ([CiudadDefaultParaCrearCheques]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_ba_parametros_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);








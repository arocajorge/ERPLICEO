CREATE TABLE [dbo].[ct_periodo_x_tb_modulo] (
    [IdEmpresa]        INT          NOT NULL,
    [IdPeriodo]        INT          NOT NULL,
    [IdModulo]         VARCHAR (20) NOT NULL,
    [Cerrado]          BIT          NOT NULL,
    [IdUsuario]        VARCHAR (50) NULL,
    [IdUsuarioUltModi] VARCHAR (50) NULL,
    [FechaTransac]     DATETIME     NULL,
    [FechaUltModi]     DATETIME     NULL,
    CONSTRAINT [PK_ct_periodo_x_tb_modulo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC, [IdModulo] ASC),
    CONSTRAINT [FK_ct_periodo_x_tb_modulo_ct_periodo] FOREIGN KEY ([IdEmpresa], [IdPeriodo]) REFERENCES [dbo].[ct_periodo] ([IdEmpresa], [IdPeriodo]),
    CONSTRAINT [FK_ct_periodo_x_tb_modulo_tb_modulo] FOREIGN KEY ([IdModulo]) REFERENCES [dbo].[tb_modulo] ([CodModulo])
);


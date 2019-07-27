CREATE TABLE [dbo].[caj_parametro] (
    [IdEmpresa]                    INT          NOT NULL,
    [IdTipoCbteCble_MoviCaja_Ing]  INT          NOT NULL,
    [IdTipoCbteCble_MoviCaja_Egr]  INT          NOT NULL,
    [IdTipo_movi_ing_x_reposicion] INT          NULL,
    [DiasTransaccionesAFuturo]     INT          NOT NULL,
    [IdUsuario]                    VARCHAR (20) NULL,
    [Fecha_Transac]                DATETIME     NULL,
    [IdUsuarioUltMod]              VARCHAR (20) NULL,
    [FechaUltMod]                  DATETIME     NULL,
    CONSTRAINT [PK_caj_parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_caj_parametro_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [IdTipoCbteCble_MoviCaja_Ing]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_caj_parametro_ct_cbtecble_tipo1] FOREIGN KEY ([IdEmpresa], [IdTipoCbteCble_MoviCaja_Egr]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_caj_parametro_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);




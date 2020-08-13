﻿CREATE TABLE [dbo].[cxc_Parametro] (
    [IdEmpresa]                            INT          NOT NULL,
    [pa_IdCaja_x_cobros_x_CXC]             INT          NOT NULL,
    [pa_IdTipoMoviCaja_x_Cobros_x_cliente] INT          NOT NULL,
    [pa_IdTipoCbteCble_CxC]                INT          NOT NULL,
    [IdTipoCbte_ConciliacionNC]            INT          NULL,
    [DiasTransaccionesAFuturo]             INT          NOT NULL,
    [IdUsuario]                            VARCHAR (20) NULL,
    [FechaTransac]                         DATETIME     NULL,
    [IdUsuarioUltMod]                      VARCHAR (20) NULL,
    [FechaUltMod]                          DATETIME     NULL,
    [IdTipoNotaProntoPago]                 INT          NULL,
    [IdTipoNotaPagoAnticipado]             INT          NULL,
    [DiasTransaccionesAPasado]             INT          NULL,
    CONSTRAINT [PK_cxc_Parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_cxc_Parametro_caj_Caja] FOREIGN KEY ([IdEmpresa], [pa_IdCaja_x_cobros_x_CXC]) REFERENCES [dbo].[caj_Caja] ([IdEmpresa], [IdCaja]),
    CONSTRAINT [FK_cxc_Parametro_caj_Caja_Movimiento_Tipo] FOREIGN KEY ([IdEmpresa], [pa_IdTipoMoviCaja_x_Cobros_x_cliente]) REFERENCES [dbo].[caj_Caja_Movimiento_Tipo] ([IdEmpresa], [IdTipoMovi]),
    CONSTRAINT [FK_cxc_Parametro_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [pa_IdTipoCbteCble_CxC]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_cxc_Parametro_ct_cbtecble_tipo1] FOREIGN KEY ([IdEmpresa], [IdTipoCbte_ConciliacionNC]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_cxc_Parametro_fa_TipoNota] FOREIGN KEY ([IdEmpresa], [IdTipoNotaProntoPago]) REFERENCES [dbo].[fa_TipoNota] ([IdEmpresa], [IdTipoNota]),
    CONSTRAINT [FK_cxc_Parametro_fa_TipoNota1] FOREIGN KEY ([IdEmpresa], [IdTipoNotaPagoAnticipado]) REFERENCES [dbo].[fa_TipoNota] ([IdEmpresa], [IdTipoNota]),
    CONSTRAINT [FK_cxc_Parametro_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);












CREATE TABLE [dbo].[cxc_cobro_x_ct_cbtecble] (
    [cbr_IdEmpresa]  INT          NOT NULL,
    [cbr_IdSucursal] INT          NOT NULL,
    [cbr_IdCobro]    NUMERIC (18) NOT NULL,
    [ct_IdEmpresa]   INT          NOT NULL,
    [ct_IdTipoCbte]  INT          NOT NULL,
    [ct_IdCbteCble]  NUMERIC (18) NOT NULL,
    [observacion]    VARCHAR (50) NULL,
    CONSTRAINT [PK_cxc_cobro_x_ct_cbtecble] PRIMARY KEY CLUSTERED ([cbr_IdEmpresa] ASC, [cbr_IdSucursal] ASC, [cbr_IdCobro] ASC, [ct_IdEmpresa] ASC, [ct_IdTipoCbte] ASC, [ct_IdCbteCble] ASC),
    CONSTRAINT [FK_cxc_cobro_x_ct_cbtecble_ct_cbtecble] FOREIGN KEY ([ct_IdEmpresa], [ct_IdTipoCbte], [ct_IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_cxc_cobro_x_ct_cbtecble_cxc_cobro] FOREIGN KEY ([cbr_IdEmpresa], [cbr_IdSucursal], [cbr_IdCobro]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro])
);


CREATE TABLE [dbo].[fa_notaCreDeb_x_ct_cbtecble] (
    [no_IdEmpresa]  INT          NOT NULL,
    [no_IdSucursal] INT          NOT NULL,
    [no_IdBodega]   INT          NOT NULL,
    [no_IdNota]     NUMERIC (18) NOT NULL,
    [ct_IdEmpresa]  INT          NOT NULL,
    [ct_IdTipoCbte] INT          NOT NULL,
    [ct_IdCbteCble] NUMERIC (18) NOT NULL,
    [observacion]   VARCHAR (50) NULL,
    CONSTRAINT [PK_fa_notaCreDeb_x_ct_cbtecble] PRIMARY KEY CLUSTERED ([no_IdEmpresa] ASC, [no_IdSucursal] ASC, [no_IdBodega] ASC, [no_IdNota] ASC, [ct_IdEmpresa] ASC, [ct_IdTipoCbte] ASC, [ct_IdCbteCble] ASC),
    CONSTRAINT [FK_fa_notaCreDeb_x_ct_cbtecble_ct_cbtecble] FOREIGN KEY ([ct_IdEmpresa], [ct_IdTipoCbte], [ct_IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_fa_notaCreDeb_x_ct_cbtecble_ct_cbtecble1] FOREIGN KEY ([ct_IdEmpresa], [ct_IdTipoCbte], [ct_IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_fa_notaCreDeb_x_ct_cbtecble_fa_notaCreDeb] FOREIGN KEY ([no_IdEmpresa], [no_IdSucursal], [no_IdBodega], [no_IdNota]) REFERENCES [dbo].[fa_notaCreDeb] ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota])
);


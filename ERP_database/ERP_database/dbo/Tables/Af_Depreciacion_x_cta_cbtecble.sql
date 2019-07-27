CREATE TABLE [dbo].[Af_Depreciacion_x_cta_cbtecble] (
    [IdEmpresa]          INT          NOT NULL,
    [IdDepreciacion]     DECIMAL (18) NOT NULL,
    [IdTipoDepreciacion] INT          NOT NULL,
    [ct_IdEmpresa]       INT          NOT NULL,
    [ct_IdTipoCbte]      INT          NOT NULL,
    [ct_IdCbteCble]      NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_Af_Depreciacion_x_cta_cbtecble] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDepreciacion] ASC, [IdTipoDepreciacion] ASC, [ct_IdEmpresa] ASC, [ct_IdTipoCbte] ASC, [ct_IdCbteCble] ASC),
    CONSTRAINT [FK_Af_Depreciacion_x_cta_cbtecble_Af_Depreciacion] FOREIGN KEY ([IdEmpresa], [IdDepreciacion]) REFERENCES [dbo].[Af_Depreciacion] ([IdEmpresa], [IdDepreciacion]),
    CONSTRAINT [FK_Af_Depreciacion_x_cta_cbtecble_ct_cbtecble] FOREIGN KEY ([ct_IdEmpresa], [ct_IdTipoCbte], [ct_IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble])
);


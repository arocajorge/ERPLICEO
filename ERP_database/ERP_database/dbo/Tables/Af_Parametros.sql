CREATE TABLE [dbo].[Af_Parametros] (
    [IdEmpresa]                        INT NOT NULL,
    [IdTipoCbte]                       INT NOT NULL,
    [IdTipoCbteMejora]                 INT NOT NULL,
    [IdTipoCbteBaja]                   INT NOT NULL,
    [IdTipoCbteVenta]                  INT NOT NULL,
    [IdTipoCbteRetiro]                 INT NOT NULL,
    [DiasTransaccionesAFuturo]         INT NOT NULL,
    [ContabilizaDepreciacionPorActivo] BIT NOT NULL,
    CONSTRAINT [PK_Af_Parametros] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_Af_Parametros_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [IdTipoCbte]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_Af_Parametros_ct_cbtecble_tipo1] FOREIGN KEY ([IdEmpresa], [IdTipoCbteMejora]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_Af_Parametros_ct_cbtecble_tipo2] FOREIGN KEY ([IdEmpresa], [IdTipoCbteBaja]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_Af_Parametros_ct_cbtecble_tipo3] FOREIGN KEY ([IdEmpresa], [IdTipoCbteVenta]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_Af_Parametros_ct_cbtecble_tipo4] FOREIGN KEY ([IdEmpresa], [IdTipoCbteRetiro]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_Af_Parametros_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);






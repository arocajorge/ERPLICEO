CREATE TABLE [dbo].[ct_parametro] (
    [IdEmpresa]                             INT NOT NULL,
    [IdTipoCbte_SaldoInicial]               INT NOT NULL,
    [IdTipoCbte_AsientoCierre_Anual]        INT NOT NULL,
    [P_Se_Muestra_Todas_las_ctas_en_combos] BIT NOT NULL,
    [DiasTransaccionesAFuturo]              INT NOT NULL,
    CONSTRAINT [PK_ct_parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_ct_parametro_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [IdTipoCbte_SaldoInicial]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_ct_parametro_ct_cbtecble_tipo1] FOREIGN KEY ([IdEmpresa], [IdTipoCbte_AsientoCierre_Anual]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte])
);




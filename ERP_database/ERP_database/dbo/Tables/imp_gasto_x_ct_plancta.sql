CREATE TABLE [dbo].[imp_gasto_x_ct_plancta] (
    [IdGasto_tipo] INT          NOT NULL,
    [IdEmpresa]    INT          NOT NULL,
    [IdCtaCble]    VARCHAR (20) NULL,
    CONSTRAINT [PK_imp_gasto_x_ct_plancta] PRIMARY KEY CLUSTERED ([IdGasto_tipo] ASC, [IdEmpresa] ASC),
    CONSTRAINT [FK_imp_gasto_x_ct_plancta_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_imp_gasto_x_ct_plancta_imp_gasto] FOREIGN KEY ([IdGasto_tipo]) REFERENCES [dbo].[imp_gasto] ([IdGasto_tipo])
);


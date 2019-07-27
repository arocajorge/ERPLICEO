CREATE TABLE [dbo].[tb_sis_Impuesto_x_ctacble] (
    [IdCod_Impuesto] VARCHAR (25) NOT NULL,
    [IdEmpresa_cta]  INT          NOT NULL,
    [IdCtaCble]      VARCHAR (20) NULL,
    [observacion]    VARCHAR (50) NULL,
    [IdCtaCble_vta]  VARCHAR (20) NULL,
    CONSTRAINT [PK_tb_sis_Impuesto_x_ctacble] PRIMARY KEY CLUSTERED ([IdCod_Impuesto] ASC, [IdEmpresa_cta] ASC),
    CONSTRAINT [FK_tb_sis_Impuesto_x_ctacble_ct_plancta] FOREIGN KEY ([IdEmpresa_cta], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_tb_sis_Impuesto_x_ctacble_tb_sis_Impuesto] FOREIGN KEY ([IdCod_Impuesto]) REFERENCES [dbo].[tb_sis_Impuesto] ([IdCod_Impuesto])
);


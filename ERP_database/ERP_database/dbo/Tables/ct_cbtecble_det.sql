CREATE TABLE [dbo].[ct_cbtecble_det] (
    [IdEmpresa]           INT           NOT NULL,
    [IdTipoCbte]          INT           NOT NULL,
    [IdCbteCble]          NUMERIC (18)  NOT NULL,
    [secuencia]           INT           NOT NULL,
    [IdCtaCble]           VARCHAR (20)  NOT NULL,
    [dc_Valor]            FLOAT (53)    NOT NULL,
    [dc_Observacion]      VARCHAR (MAX) NULL,
    [dc_para_conciliar]   BIT           NULL,
    [IdPunto_cargo_grupo] INT           NULL,
    [IdPunto_cargo]       INT           NULL,
    [IdCentroCosto]       VARCHAR (200) NULL,
    CONSTRAINT [PK_ct_cbtecble_det_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC, [secuencia] ASC),
    CONSTRAINT [FK_ct_cbtecble_det_ct_cbtecble] FOREIGN KEY ([IdEmpresa], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_ct_cbtecble_det_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCosto]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
    CONSTRAINT [FK_ct_cbtecble_det_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_ct_cbtecble_det_ct_punto_cargo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo]) REFERENCES [dbo].[ct_punto_cargo] ([IdEmpresa], [IdPunto_cargo]),
    CONSTRAINT [FK_ct_cbtecble_det_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo])
);






GO
CREATE NONCLUSTERED INDEX [IX_ct_cbtecble_det]
    ON [dbo].[ct_cbtecble_det]([IdEmpresa] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC, [secuencia] ASC, [IdCtaCble] ASC);


CREATE TABLE [dbo].[ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo] (
    [IdEmpresa]          INT          NOT NULL,
    [CodTipoCbteBan]     VARCHAR (20) NOT NULL,
    [IdTipoCbteCble]     INT          NOT NULL,
    [IdTipoCbteCble_Anu] INT          NOT NULL,
    [IdCtaCble]          VARCHAR (20) NULL,
    [Tipo_DebCred]       CHAR (1)     NULL,
    CONSTRAINT [PK_ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [CodTipoCbteBan] ASC),
    CONSTRAINT [FK_ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_ba_Cbte_Ban_tipo] FOREIGN KEY ([CodTipoCbteBan]) REFERENCES [dbo].[ba_Cbte_Ban_tipo] ([CodTipoCbteBan]),
    CONSTRAINT [FK_ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);


CREATE TABLE [dbo].[ct_cbtecble_Reversado] (
    [IdEmpresa]      INT          NOT NULL,
    [IdTipoCbte]     INT          NOT NULL,
    [IdCbteCble]     NUMERIC (18) NOT NULL,
    [IdEmpresa_Anu]  INT          NOT NULL,
    [IdTipoCbte_Anu] INT          NOT NULL,
    [IdCbteCble_Anu] NUMERIC (18) NOT NULL,
    [ip]             CHAR (1)     NULL,
    CONSTRAINT [PK_ct_cbtecble_Anulados] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC, [IdEmpresa_Anu] ASC, [IdTipoCbte_Anu] ASC, [IdCbteCble_Anu] ASC),
    CONSTRAINT [FK_ct_cbtecble_Reversado_ct_cbtecble] FOREIGN KEY ([IdEmpresa], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_ct_cbtecble_Reversado_ct_cbtecble1] FOREIGN KEY ([IdEmpresa_Anu], [IdTipoCbte_Anu], [IdCbteCble_Anu]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble])
);


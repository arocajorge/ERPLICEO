CREATE TABLE [dbo].[ct_cbtecble_tipo] (
    [IdEmpresa]       INT           NOT NULL,
    [IdTipoCbte]      INT           NOT NULL,
    [CodTipoCbte]     VARCHAR (10)  NOT NULL,
    [tc_TipoCbte]     VARCHAR (50)  CONSTRAINT [DF__ct_tipo_c__TipoC__114A936A] DEFAULT (NULL) NOT NULL,
    [tc_Estado]       CHAR (1)      CONSTRAINT [DF__ct_tipo_c__Estad__123EB7A3] DEFAULT (NULL) NOT NULL,
    [tc_Interno]      CHAR (1)      CONSTRAINT [DF__ct_tipo_c__Inter__1332DBDC] DEFAULT (NULL) NOT NULL,
    [tc_Nemonico]     NCHAR (10)    NULL,
    [IdTipoCbte_Anul] INT           NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotiAnula]       VARCHAR (100) NULL,
    CONSTRAINT [PK_ct_cbtecble_tipo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoCbte] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_ct_cbtecble_tipo]
    ON [dbo].[ct_cbtecble_tipo]([IdEmpresa] ASC, [IdTipoCbte] ASC);


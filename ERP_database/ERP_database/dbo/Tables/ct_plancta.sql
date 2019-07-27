CREATE TABLE [dbo].[ct_plancta] (
    [IdEmpresa]       INT           NOT NULL,
    [IdCtaCble]       VARCHAR (20)  NOT NULL,
    [pc_Cuenta]       VARCHAR (500) NOT NULL,
    [IdCtaCblePadre]  VARCHAR (20)  NULL,
    [pc_Naturaleza]   CHAR (1)      NOT NULL,
    [IdNivelCta]      INT           NOT NULL,
    [IdGrupoCble]     VARCHAR (5)   NOT NULL,
    [pc_Estado]       CHAR (1)      NOT NULL,
    [pc_EsMovimiento] CHAR (1)      NOT NULL,
    [pc_clave_corta]  VARCHAR (15)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuario]       VARCHAR (50)  NULL,
    [IdUsuarioUltMod] VARCHAR (50)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (50)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotivoAnulacion] VARCHAR (150) NULL,
    CONSTRAINT [PK__ct_planc__86F41E99182C9B23] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCtaCble] ASC),
    CONSTRAINT [FK_ct_plancta_ct_grupocble] FOREIGN KEY ([IdGrupoCble]) REFERENCES [dbo].[ct_grupocble] ([IdGrupoCble]),
    CONSTRAINT [FK_ct_plancta_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCblePadre]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_ct_plancta_ct_plancta_nivel] FOREIGN KEY ([IdEmpresa], [IdNivelCta]) REFERENCES [dbo].[ct_plancta_nivel] ([IdEmpresa], [IdNivelCta]),
    CONSTRAINT [FK_ct_plancta_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);






GO
CREATE NONCLUSTERED INDEX [IX_ct_plancta]
    ON [dbo].[ct_plancta]([IdEmpresa] ASC, [IdCtaCble] ASC);


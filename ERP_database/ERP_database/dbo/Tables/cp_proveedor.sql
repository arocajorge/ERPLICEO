CREATE TABLE [dbo].[cp_proveedor] (
    [IdEmpresa]                  INT           NOT NULL,
    [IdProveedor]                NUMERIC (18)  NOT NULL,
    [IdPersona]                  NUMERIC (18)  NOT NULL,
    [pr_codigo]                  VARCHAR (50)  NULL,
    [pr_contribuyenteEspecial]   CHAR (1)      NULL,
    [pr_plazo]                   INT           NOT NULL,
    [pr_estado]                  VARCHAR (1)   NULL,
    [IdCiudad]                   VARCHAR (25)  NOT NULL,
    [IdCtaCble_CXP]              VARCHAR (20)  NULL,
    [IdCtaCble_Gasto]            VARCHAR (20)  NULL,
    [IdCtaCble_Anticipo]         VARCHAR (20)  NULL,
    [IdClaseProveedor]           INT           NOT NULL,
    [MotivoAnulacion]            VARCHAR (MAX) NULL,
    [IdTipoCta_acreditacion_cat] VARCHAR (25)  NULL,
    [num_cta_acreditacion]       VARCHAR (50)  NULL,
    [IdBanco_acreditacion]       INT           NULL,
    [es_empresa_relacionada]     BIT           NOT NULL,
    [pr_telefonos]               VARCHAR (100) NULL,
    [pr_celular]                 VARCHAR (100) NULL,
    [pr_direccion]               VARCHAR (500) NULL,
    [pr_correo]                  VARCHAR (200) NULL,
    [IdUsuario]                  VARCHAR (20)  NULL,
    [Fecha_Transac]              DATETIME      NULL,
    [IdUsuarioUltMod]            VARCHAR (20)  NULL,
    [Fecha_UltMod]               DATETIME      NULL,
    [IdUsuarioUltAnu]            VARCHAR (20)  NULL,
    [Fecha_UltAnu]               DATETIME      NULL,
    CONSTRAINT [PK_cp_proveedor] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProveedor] ASC),
    CONSTRAINT [FK_cp_proveedor_cp_catalogo2] FOREIGN KEY ([IdTipoCta_acreditacion_cat]) REFERENCES [dbo].[cp_catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_cp_proveedor_cp_proveedor_clase] FOREIGN KEY ([IdEmpresa], [IdClaseProveedor]) REFERENCES [dbo].[cp_proveedor_clase] ([IdEmpresa], [IdClaseProveedor]),
    CONSTRAINT [FK_cp_proveedor_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble_CXP]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cp_proveedor_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Anticipo]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cp_proveedor_ct_plancta2] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Gasto]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cp_proveedor_tb_banco] FOREIGN KEY ([IdBanco_acreditacion]) REFERENCES [dbo].[tb_banco] ([IdBanco]),
    CONSTRAINT [FK_cp_proveedor_tb_ciudad] FOREIGN KEY ([IdCiudad]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad]),
    CONSTRAINT [FK_cp_proveedor_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_cp_proveedor_tb_persona] FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[tb_persona] ([IdPersona])
);






GO
CREATE NONCLUSTERED INDEX [IX_cp_proveedor]
    ON [dbo].[cp_proveedor]([IdEmpresa] ASC, [IdProveedor] ASC, [IdPersona] ASC);


CREATE TABLE [dbo].[cp_proveedor_detalle] (
    [IdEmpresa]                  INT           NOT NULL,
    [IdProveedor]                NUMERIC (18)  NOT NULL,
    [Secuencia]                  INT           NOT NULL,
    [NombreAdicional]            VARCHAR (MAX) NULL,
    [IdTipoCta_acreditacion_cat] VARCHAR (25)  NULL,
    [num_cta_acreditacion]       VARCHAR (50)  NULL,
    [IdBanco_acreditacion]       INT           NULL,
    [pr_correo]                  VARCHAR (MAX) NULL,
    [Estado]                     BIT           NOT NULL,
    CONSTRAINT [PK_cp_proveedor_detalle] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProveedor] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_proveedor_detalle_cp_proveedor] FOREIGN KEY ([IdEmpresa], [IdProveedor]) REFERENCES [dbo].[cp_proveedor] ([IdEmpresa], [IdProveedor]),
    CONSTRAINT [FK_cp_proveedor_detalle_tb_banco] FOREIGN KEY ([IdBanco_acreditacion]) REFERENCES [dbo].[tb_banco] ([IdBanco]),
    CONSTRAINT [FK_cp_proveedor_detalle_tb_Catalogo] FOREIGN KEY ([IdTipoCta_acreditacion_cat]) REFERENCES [dbo].[tb_Catalogo] ([CodCatalogo])
);


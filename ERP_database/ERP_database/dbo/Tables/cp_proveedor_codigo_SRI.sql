CREATE TABLE [dbo].[cp_proveedor_codigo_SRI] (
    [IdEmpresa]    INT          NOT NULL,
    [IdProveedor]  NUMERIC (18) NOT NULL,
    [IdCodigo_SRI] INT          NOT NULL,
    [observacion]  VARCHAR (50) NULL,
    CONSTRAINT [PK_cp_proveedor_codRet_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProveedor] ASC, [IdCodigo_SRI] ASC),
    CONSTRAINT [FK_cp_proveedor_codigo_SRI_cp_codigo_SRI] FOREIGN KEY ([IdCodigo_SRI]) REFERENCES [dbo].[cp_codigo_SRI] ([IdCodigo_SRI]),
    CONSTRAINT [FK_cp_proveedor_codigo_SRI_cp_proveedor] FOREIGN KEY ([IdEmpresa], [IdProveedor]) REFERENCES [dbo].[cp_proveedor] ([IdEmpresa], [IdProveedor])
);


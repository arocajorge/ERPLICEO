CREATE TABLE [dbo].[cp_codigo_SRI] (
    [IdCodigo_SRI]       INT           NOT NULL,
    [codigoSRI]          VARCHAR (50)  NOT NULL,
    [co_codigoBase]      VARCHAR (50)  NOT NULL,
    [co_descripcion]     VARCHAR (350) NOT NULL,
    [co_porRetencion]    FLOAT (53)    NOT NULL,
    [co_f_valides_desde] DATE          NOT NULL,
    [co_f_valides_hasta] DATE          NOT NULL,
    [co_estado]          VARCHAR (1)   NOT NULL,
    [IdTipoSRI]          VARCHAR (20)  NOT NULL,
    [IdUsuario]          VARCHAR (20)  NULL,
    [Fecha_Transac]      DATETIME      NULL,
    [IdUsuarioUltMod]    VARCHAR (20)  NULL,
    [Fecha_UltMod]       DATETIME      NULL,
    [IdUsuarioUltAnu]    VARCHAR (20)  NULL,
    [Fecha_UltAnu]       DATETIME      NULL,
    [nom_pc]             VARCHAR (50)  NULL,
    [ip]                 VARCHAR (50)  NULL,
    [MotivoAnulacion]    VARCHAR (200) NULL,
    CONSTRAINT [PK_cp_codigo_SRI] PRIMARY KEY CLUSTERED ([IdCodigo_SRI] ASC),
    CONSTRAINT [FK_cp_codigo_SRI_cp_codigo_SRI_tipo] FOREIGN KEY ([IdTipoSRI]) REFERENCES [dbo].[cp_codigo_SRI_tipo] ([IdTipoSRI])
);


CREATE TABLE [dbo].[pre_RubroTipo] (
    [IdEmpresa]             INT            NOT NULL,
    [IdRubroTipo]           INT            NOT NULL,
    [Descripcion]           VARCHAR (5000) NOT NULL,
    [Signo]                 VARCHAR (1)    NOT NULL,
    [Orden]                 INT            NOT NULL,
    [Estado]                BIT            NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)   NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (50)   NULL,
    [FechaModificacion]     DATETIME       NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)   NULL,
    [FechaAnulacion]        DATETIME       NULL,
    [MotivoAnulacion]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_pre_RubroTipo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRubroTipo] ASC)
);


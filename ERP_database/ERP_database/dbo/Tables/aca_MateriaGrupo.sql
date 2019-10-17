CREATE TABLE [dbo].[aca_MateriaGrupo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdMateriaGrupo]        INT           NOT NULL,
    [NomMateriaGrupo]       VARCHAR (500) NOT NULL,
    [OrdenMateriaGrupo]     INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_MateriaGrupo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMateriaGrupo] ASC)
);


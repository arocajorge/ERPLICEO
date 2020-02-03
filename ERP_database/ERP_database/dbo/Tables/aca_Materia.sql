CREATE TABLE [dbo].[aca_Materia] (
    [IdEmpresa]             INT           NOT NULL,
    [IdMateria]             INT           NOT NULL,
    [IdMateriaGrupo]        INT           NULL,
    [IdMateriaArea]         INT           NULL,
    [NomMateria]            VARCHAR (500) NOT NULL,
    [EsObligatorio]         BIT           NOT NULL,
    [OrdenMateria]          INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Materia] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMateria] ASC),
    CONSTRAINT [FK_aca_Materia_aca_MateriaArea] FOREIGN KEY ([IdEmpresa], [IdMateriaArea]) REFERENCES [dbo].[aca_MateriaArea] ([IdEmpresa], [IdMateriaArea]),
    CONSTRAINT [FK_aca_Materia_aca_MateriaGrupo] FOREIGN KEY ([IdEmpresa], [IdMateriaGrupo]) REFERENCES [dbo].[aca_MateriaGrupo] ([IdEmpresa], [IdMateriaGrupo])
);








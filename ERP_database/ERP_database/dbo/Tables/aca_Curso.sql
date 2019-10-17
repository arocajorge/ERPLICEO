CREATE TABLE [dbo].[aca_Curso] (
    [IdEmpresa]             INT           NOT NULL,
    [IdCurso]               INT           NOT NULL,
    [IdCursoAPromover]      INT           NULL,
    [NomCurso]              VARCHAR (500) NOT NULL,
    [OrdenCurso]            INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Curso] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCurso] ASC),
    CONSTRAINT [FK_aca_Curso_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCursoAPromover]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso])
);




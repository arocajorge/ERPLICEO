CREATE TABLE [dbo].[aca_MatriculaCambios] (
    [IdEmpresa]         INT           NOT NULL,
    [IdMatricula]       NUMERIC (18)  NOT NULL,
    [Secuencia]         INT           NOT NULL,
    [IdAnio]            INT           NOT NULL,
    [IdSede]            INT           NOT NULL,
    [IdNivel]           INT           NOT NULL,
    [IdJornada]         INT           NOT NULL,
    [IdCurso]           INT           NOT NULL,
    [IdParalelo]        INT           NOT NULL,
    [IdPlantilla]       INT           NOT NULL,
    [IdUsuarioCreacion] VARCHAR (200) NULL,
    [FechaCreacion]     DATETIME      NULL,
    [Observacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_MatriculaCambios] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_aca_MatriculaCambios_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);




CREATE TABLE [dbo].[aca_MatriculaCalificacionCualitativaPromedio] (
    [IdEmpresa]                      INT             NOT NULL,
    [IdMatricula]                    NUMERIC (18)    NOT NULL,
    [IdMateria]                      INT             NOT NULL,
    [IdProfesor]                     NUMERIC (18)    NULL,
    [IdCalificacionCualitativaQ1]    INT             NULL,
    [PromedioQ1]                     NUMERIC (18, 2) NULL,
    [IdCalificacionCualitativaQ2]    INT             NULL,
    [PromedioQ2]                     NUMERIC (18, 2) NULL,
    [IdCalificacionCualitativaFinal] INT             NULL,
    [PromedioFinal]                  NUMERIC (18, 2) NULL,
    [IdUsuarioCreacion]              VARCHAR (200)   NULL,
    [FechaCreacion]                  DATETIME        NULL,
    [IdUsuarioModificacion]          VARCHAR (200)   NULL,
    [FechaModificacion]              DATETIME        NULL,
    CONSTRAINT [PK_aca_MatriculaCalificacionCualitativaPromedio] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC, [IdMateria] ASC)
);


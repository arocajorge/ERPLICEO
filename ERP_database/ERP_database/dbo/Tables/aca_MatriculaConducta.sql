CREATE TABLE [dbo].[aca_MatriculaConducta] (
    [IdEmpresa]          INT             NOT NULL,
    [IdMatricula]        NUMERIC (18)    NOT NULL,
    [IdMateria]          INT             NOT NULL,
    [IdProfesor]         NUMERIC (18)    NULL,
    [CalificacionP1]     NUMERIC (18, 2) NULL,
    [CalificacionP2]     NUMERIC (18, 2) NULL,
    [CalificacionP3]     NUMERIC (18, 2) NULL,
    [PromedioQ1]         NUMERIC (18, 2) NULL,
    [ExamenQ1]           NUMERIC (18, 2) NULL,
    [PromedioFinalQ1]    NUMERIC (18, 2) NULL,
    [CalificacionP4]     NUMERIC (18, 2) NULL,
    [CalificacionP5]     NUMERIC (18, 2) NULL,
    [CalificacionP6]     NUMERIC (18, 2) NULL,
    [PromedioQ2]         NUMERIC (18, 2) NULL,
    [ExamenQ2]           NUMERIC (18, 2) NULL,
    [PromedioFinalQ2]    NUMERIC (18, 2) NULL,
    [ExamenMejoramiento] NUMERIC (18, 2) NULL,
    [ExamenSupletorio]   NUMERIC (18, 2) NULL,
    [ExamenRemedial]     NUMERIC (18, 2) NULL,
    [ExamenGracia]       NUMERIC (18, 2) NULL,
    [PromedioFinal]      NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_aca_MatriculaConducta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC, [IdMateria] ASC),
    CONSTRAINT [FK_aca_MatriculaConducta_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);






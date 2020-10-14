CREATE TABLE [dbo].[aca_MatriculaCalificacion] (
    [IdEmpresa]                 INT             NOT NULL,
    [IdMatricula]               NUMERIC (18)    NOT NULL,
    [IdMateria]                 INT             NOT NULL,
    [IdProfesor]                NUMERIC (18)    NULL,
    [CalificacionP1]            NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioP1]  INT             NULL,
    [CalificacionP2]            NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioP2]  INT             NULL,
    [CalificacionP3]            NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioP3]  INT             NULL,
    [PromedioQ1]                NUMERIC (18, 2) NULL,
    [ExamenQ1]                  NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioEQ1] INT             NULL,
    [PromedioFinalQ1]           NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioQ1]  INT             NULL,
    [CausaQ1]                   VARCHAR (MAX)   NULL,
    [ResolucionQ1]              VARCHAR (MAX)   NULL,
    [CalificacionP4]            NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioP4]  INT             NULL,
    [CalificacionP5]            NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioP5]  INT             NULL,
    [CalificacionP6]            NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioP6]  INT             NULL,
    [PromedioQ2]                NUMERIC (18, 2) NULL,
    [ExamenQ2]                  NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioEQ2] INT             NULL,
    [PromedioFinalQ2]           NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioQ2]  INT             NULL,
    [CausaQ2]                   VARCHAR (MAX)   NULL,
    [ResolucionQ2]              VARCHAR (MAX)   NULL,
    [PromedioQuimestres]        NUMERIC (18, 2) NULL,
    [ExamenMejoramiento]        NUMERIC (18, 2) NULL,
    [CampoMejoramiento]         VARCHAR (50)    NULL,
    [ExamenSupletorio]          NUMERIC (18, 2) NULL,
    [ExamenRemedial]            NUMERIC (18, 2) NULL,
    [ExamenGracia]              NUMERIC (18, 2) NULL,
    [PromedioFinal]             NUMERIC (18, 2) NULL,
    [IdEquivalenciaPromedioPF]  INT             NULL,
    CONSTRAINT [PK_aca_MatriculaCalificacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC, [IdMateria] ASC),
    CONSTRAINT [FK_aca_MatriculaCalificacion_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);














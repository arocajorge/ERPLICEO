CREATE TABLE [dbo].[aca_MatriculaConducta] (
    [IdEmpresa]       INT             NOT NULL,
    [IdMatricula]     NUMERIC (18)    NOT NULL,
    [PromedioP1]      NUMERIC (18, 2) NULL,
    [PromedioFinalP1] NUMERIC (18, 2) NULL,
    [PromedioP2]      NUMERIC (18, 2) NULL,
    [PromedioFinalP2] NUMERIC (18, 2) NULL,
    [PromedioP3]      NUMERIC (18, 2) NULL,
    [PromedioFinalP3] NUMERIC (18, 2) NULL,
    [PromedioQ1]      NUMERIC (18, 2) NULL,
    [PromedioFinalQ1] NUMERIC (18, 2) NULL,
    [PromedioP4]      NUMERIC (18, 2) NULL,
    [PromedioFinalP4] NUMERIC (18, 2) NULL,
    [PromedioP5]      NUMERIC (18, 2) NULL,
    [PromedioFinalP5] NUMERIC (18, 2) NULL,
    [PromedioP6]      NUMERIC (18, 2) NULL,
    [PromedioFinalP6] NUMERIC (18, 2) NULL,
    [PromedioQ2]      NUMERIC (18, 2) NULL,
    [PromedioFinalQ2] NUMERIC (18, 2) NULL,
    [PromedioGeneral] NUMERIC (18, 2) NULL,
    [PromedioFinal]   NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_aca_MatriculaConducta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC),
    CONSTRAINT [FK_aca_MatriculaConducta_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);










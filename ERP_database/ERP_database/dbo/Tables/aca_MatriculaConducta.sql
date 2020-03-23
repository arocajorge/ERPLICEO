CREATE TABLE [dbo].[aca_MatriculaConducta] (
    [IdEmpresa]       INT          NOT NULL,
    [IdMatricula]     NUMERIC (18) NOT NULL,
    [PromedioP1]      INT          NULL,
    [PromedioFinalP1] INT          NULL,
    [PromedioP2]      INT          NULL,
    [PromedioFinalP2] INT          NULL,
    [PromedioP3]      INT          NULL,
    [PromedioFinalP3] INT          NULL,
    [PromedioQ1]      INT          NULL,
    [PromedioFinalQ1] INT          NULL,
    [PromedioP4]      INT          NULL,
    [PromedioFinalP4] INT          NULL,
    [PromedioP5]      INT          NULL,
    [PromedioFinalP5] INT          NULL,
    [PromedioP6]      INT          NULL,
    [PromedioFinalP6] INT          NULL,
    [PromedioQ2]      INT          NULL,
    [PromedioFinalQ2] INT          NULL,
    [PromedioGeneral] INT          NULL,
    [PromedioFinal]   INT          NULL,
    CONSTRAINT [PK_aca_MatriculaConducta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC),
    CONSTRAINT [FK_aca_MatriculaConducta_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);












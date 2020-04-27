CREATE TABLE [dbo].[aca_MatriculaConducta] (
    [IdEmpresa]                INT          NOT NULL,
    [IdMatricula]              NUMERIC (18) NOT NULL,
    [SecuenciaPromedioP1]      INT          NULL,
    [PromedioP1]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalP1] INT          NULL,
    [PromedioFinalP1]          FLOAT (53)   NULL,
    [SecuenciaPromedioP2]      INT          NULL,
    [PromedioP2]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalP2] INT          NULL,
    [PromedioFinalP2]          FLOAT (53)   NULL,
    [SecuenciaPromedioP3]      INT          NULL,
    [PromedioP3]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalP3] INT          NULL,
    [PromedioFinalP3]          FLOAT (53)   NULL,
    [SecuenciaPromedioQ1]      INT          NULL,
    [PromedioQ1]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalQ1] INT          NULL,
    [PromedioFinalQ1]          FLOAT (53)   NULL,
    [SecuenciaPromedioP4]      INT          NULL,
    [PromedioP4]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalP4] INT          NULL,
    [PromedioFinalP4]          FLOAT (53)   NULL,
    [SecuenciaPromedioP5]      INT          NULL,
    [PromedioP5]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalP5] INT          NULL,
    [PromedioFinalP5]          FLOAT (53)   NULL,
    [SecuenciaPromedioP6]      INT          NULL,
    [PromedioP6]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalP6] INT          NULL,
    [PromedioFinalP6]          FLOAT (53)   NULL,
    [SecuenciaPromedioQ2]      INT          NULL,
    [PromedioQ2]               FLOAT (53)   NULL,
    [SecuenciaPromedioFinalQ2] INT          NULL,
    [PromedioFinalQ2]          FLOAT (53)   NULL,
    [SecuenciaPromedioGeneral] INT          NULL,
    [PromedioGeneral]          FLOAT (53)   NULL,
    [SecuenciaPromedioFinal]   INT          NULL,
    [PromedioFinal]            FLOAT (53)   NULL,
    CONSTRAINT [PK_aca_MatriculaConducta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC),
    CONSTRAINT [FK_aca_MatriculaConducta_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula])
);














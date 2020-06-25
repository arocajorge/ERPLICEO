CREATE TABLE [dbo].[aca_AnioLectivo_PeriodoDet] (
    [IdEmpresa] INT NOT NULL,
    [IdPeriodo] INT NOT NULL,
    [Secuencia] INT NOT NULL,
    CONSTRAINT [PK_aca_AnioLectivo_PeriodoDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC, [Secuencia] ASC)
);


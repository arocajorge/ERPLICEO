CREATE TABLE [dbo].[ro_FormulaHorasRecargo] (
    [IdEmpresa]         INT        NOT NULL,
    [Dividendo]         FLOAT (53) NOT NULL,
    [Divisor]           FLOAT (53) NOT NULL,
    [PorcentajeRecargo] FLOAT (53) NOT NULL,
    CONSTRAINT [PK_ro_FormulaHorasRecargos] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC)
);


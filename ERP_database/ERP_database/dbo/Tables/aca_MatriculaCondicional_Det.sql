CREATE TABLE [dbo].[aca_MatriculaCondicional_Det] (
    [IdEmpresa]     INT          NOT NULL,
    [IdCondicional] NUMERIC (18) NOT NULL,
    [Secuencia]     INT          NOT NULL,
    [IdParrafo]     INT          NOT NULL,
    CONSTRAINT [PK_aca_MatriculaCondicional_Det_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCondicional] ASC),
    CONSTRAINT [FK_aca_MatriculaCondicional_Det_aca_MatriculaCondicional] FOREIGN KEY ([IdEmpresa], [IdCondicional]) REFERENCES [dbo].[aca_MatriculaCondicional] ([IdEmpresa], [IdCondicional]),
    CONSTRAINT [FK_aca_MatriculaCondicional_Det_aca_MatriculaCondicionalParrafo] FOREIGN KEY ([IdEmpresa], [IdParrafo]) REFERENCES [dbo].[aca_MatriculaCondicionalParrafo] ([IdEmpresa], [IdParrafo])
);


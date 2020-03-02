CREATE TABLE [dbo].[aca_MatriculaCondicional_Det] (
    [IdEmpresa]              INT          NOT NULL,
    [IdMatriculaCondicional] NUMERIC (18) NOT NULL,
    [IdParrafo]              INT          NOT NULL,
    [Secuencia]              INT          NOT NULL,
    CONSTRAINT [PK_aca_MatriculaCondicional_Det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatriculaCondicional] ASC, [IdParrafo] ASC),
    CONSTRAINT [FK_aca_MatriculaCondicional_Det_aca_MatriculaCondicional] FOREIGN KEY ([IdEmpresa], [IdMatriculaCondicional]) REFERENCES [dbo].[aca_MatriculaCondicional] ([IdEmpresa], [IdMatriculaCondicional]),
    CONSTRAINT [FK_aca_MatriculaCondicional_Det_aca_MatriculaCondicionalParrafo] FOREIGN KEY ([IdEmpresa], [IdParrafo]) REFERENCES [dbo].[aca_MatriculaCondicionalParrafo] ([IdEmpresa], [IdParrafo])
);


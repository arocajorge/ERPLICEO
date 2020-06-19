CREATE TABLE [dbo].[fa_AplicacionMasivaDet] (
    [IdEmpresa]    INT          NOT NULL,
    [IdAplicacion] NUMERIC (18) NOT NULL,
    [Secuencia]    INT          NOT NULL,
    [IdAlumno]     NUMERIC (18) NOT NULL,
    [Saldo]        FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_fa_AplicacionMasivaDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAplicacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_AplicacionMasivaDet_fa_AplicacionMasiva] FOREIGN KEY ([IdEmpresa], [IdAplicacion]) REFERENCES [dbo].[fa_AplicacionMasiva] ([IdEmpresa], [IdAplicacion])
);


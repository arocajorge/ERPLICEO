CREATE TABLE [dbo].[ro_rubro_tipo_x_jornada] (
    [IdEmpresa]              INT          NOT NULL,
    [IdRubro]                VARCHAR (50) NOT NULL,
    [Secuencia]              INT          NOT NULL,
    [IdRubroContabilizacion] VARCHAR (50) NOT NULL,
    [IdJornada]              INT          NOT NULL,
    CONSTRAINT [PK_ro_rubro_tipo_x_jornada_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRubro] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_rubro_tipo_x_jornada_ro_jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[ro_jornada] ([IdEmpresa], [IdJornada]),
    CONSTRAINT [FK_ro_rubro_tipo_x_jornada_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rubro_tipo_x_jornada_ro_rubro_tipo1] FOREIGN KEY ([IdEmpresa], [IdRubroContabilizacion]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);


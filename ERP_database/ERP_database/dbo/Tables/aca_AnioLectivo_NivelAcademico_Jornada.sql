CREATE TABLE [dbo].[aca_AnioLectivo_NivelAcademico_Jornada] (
    [IdEmpresa]    INT           NOT NULL,
    [IdAnio]       INT           NOT NULL,
    [IdSede]       INT           NOT NULL,
    [IdNivel]      INT           NOT NULL,
    [IdJornada]    INT           NOT NULL,
    [NomJornada]   VARCHAR (500) NOT NULL,
    [OrdenJornada] INT           NOT NULL,
    CONSTRAINT [PK_aca_AnioLectivo_NivelAcademico_Jornada] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_NivelAcademico_Jornada_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivo_NivelAcademico_Jornada_aca_Jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[aca_Jornada] ([IdEmpresa], [IdJornada]),
    CONSTRAINT [FK_aca_AnioLectivo_NivelAcademico_Jornada_aca_NivelAcademico] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[aca_NivelAcademico] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_aca_AnioLectivo_NivelAcademico_Jornada_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede])
);


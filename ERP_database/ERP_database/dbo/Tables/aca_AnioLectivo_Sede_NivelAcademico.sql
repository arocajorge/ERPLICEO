CREATE TABLE [dbo].[aca_AnioLectivo_Sede_NivelAcademico] (
    [IdEmpresa]  INT           NOT NULL,
    [IdAnio]     INT           NOT NULL,
    [IdSede]     INT           NOT NULL,
    [IdNivel]    INT           NOT NULL,
    [NomSede]    VARCHAR (500) NOT NULL,
    [NomNivel]   VARCHAR (500) NOT NULL,
    [OrdenNivel] INT           NOT NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Sede_NivelAcademico] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Sede_NivelAcademico_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivo_Sede_NivelAcademico_aca_NivelAcademico] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[aca_NivelAcademico] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_aca_AnioLectivo_Sede_NivelAcademico_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede])
);




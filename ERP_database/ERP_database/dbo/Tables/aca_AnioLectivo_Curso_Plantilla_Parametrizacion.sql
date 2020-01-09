CREATE TABLE [dbo].[aca_AnioLectivo_Curso_Plantilla_Parametrizacion] (
    [IdEmpresa]      INT          NOT NULL,
    [IdAnio]         INT          NOT NULL,
    [IdSede]         INT          NOT NULL,
    [IdNivel]        INT          NOT NULL,
    [IdJornada]      INT          NOT NULL,
    [IdCurso]        INT          NOT NULL,
    [IdPlantilla]    INT          NOT NULL,
    [IdRubro]        INT          NOT NULL,
    [IdCtaCbleDebe]  VARCHAR (20) NULL,
    [IdCtaCbleHaber] VARCHAR (20) NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Curso_Plantilla_Parametrizacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC, [IdCurso] ASC, [IdPlantilla] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Plantilla_Parametrizacion_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCbleDebe]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Plantilla_Parametrizacion_ct_plancta2] FOREIGN KEY ([IdEmpresa], [IdCtaCbleHaber]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);




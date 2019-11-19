CREATE TABLE [dbo].[aca_AnioLectivo_Curso_Documento] (
    [IdEmpresa]      INT           NOT NULL,
    [IdAnio]         INT           NOT NULL,
    [IdSede]         INT           NOT NULL,
    [IdNivel]        INT           NOT NULL,
    [IdJornada]      INT           NOT NULL,
    [IdCurso]        INT           NOT NULL,
    [IdDocumento]    INT           NOT NULL,
    [NomDocumento]   VARCHAR (500) NULL,
    [OrdenDocumento] INT           NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Curso_Documento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC, [IdCurso] ASC, [IdDocumento] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Documento_aca_AnioLectivo_Jornada_Curso] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso]) REFERENCES [dbo].[aca_AnioLectivo_Jornada_Curso] ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso]),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Documento_aca_Documento] FOREIGN KEY ([IdEmpresa], [IdDocumento]) REFERENCES [dbo].[aca_Documento] ([IdEmpresa], [IdDocumento])
);


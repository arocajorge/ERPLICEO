CREATE TABLE [dbo].[aca_AnioLectivo_Tematica] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [IdTematica]            INT           NOT NULL,
    [IdCampoAccion]         INT           NOT NULL,
    [NombreCampoAccion]     VARCHAR (200) NULL,
    [NombreTematica]        VARCHAR (200) NULL,
    [OrdenCampoAccion]      INT           NULL,
    [OrdenTematica]         INT           NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Tematica] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdTematica] ASC, [IdCampoAccion] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Tematica_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivo_Tematica_aca_CampoAccion] FOREIGN KEY ([IdEmpresa], [IdCampoAccion]) REFERENCES [dbo].[aca_CampoAccion] ([IdEmpresa], [IdCampoAccion]),
    CONSTRAINT [FK_aca_AnioLectivo_Tematica_aca_Tematica] FOREIGN KEY ([IdEmpresa], [IdTematica]) REFERENCES [dbo].[aca_Tematica] ([IdEmpresa], [IdTematica])
);


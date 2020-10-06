CREATE TABLE [dbo].[aca_Tematica] (
    [IdEmpresa]             INT           NOT NULL,
    [IdTematica]            INT           NOT NULL,
    [IdCampoAccion]         INT           NOT NULL,
    [NombreTematica]        VARCHAR (200) NOT NULL,
    [OrdenTematica]         INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Tematica] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTematica] ASC),
    CONSTRAINT [FK_aca_Tematica_aca_CampoAccion] FOREIGN KEY ([IdEmpresa], [IdCampoAccion]) REFERENCES [dbo].[aca_CampoAccion] ([IdEmpresa], [IdCampoAccion])
);




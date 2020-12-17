CREATE TABLE [dbo].[aca_PlantillaTipo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdTipoPlantilla]       INT           NOT NULL,
    [NomPlantillaTipo]      VARCHAR (500) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    [EsBeca]                BIT           NULL,
    CONSTRAINT [PK_aca_PlantillaTipo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoPlantilla] ASC)
);




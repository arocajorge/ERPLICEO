CREATE TABLE [dbo].[aca_Plantilla] (
    [IdEmpresa]             INT             NOT NULL,
    [IdAnio]                INT             NOT NULL,
    [IdPlantilla]           INT             NOT NULL,
    [NomPlantilla]          VARCHAR (500)   NOT NULL,
    [Valor]                 NUMERIC (18, 2) NOT NULL,
    [Estado]                BIT             NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200)   NULL,
    [FechaCreacion]         DATETIME        NULL,
    [IdUsuarioModificacion] VARCHAR (200)   NULL,
    [FechaModificacion]     DATETIME        NULL,
    [IdUsuarioAnulacion]    VARCHAR (200)   NULL,
    [FechaAnulacion]        DATETIME        NULL,
    [MotivoAnulacion]       VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_aca_Plantilla] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdPlantilla] ASC)
);


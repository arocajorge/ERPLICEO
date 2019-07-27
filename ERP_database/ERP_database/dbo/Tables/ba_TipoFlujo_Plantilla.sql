CREATE TABLE [dbo].[ba_TipoFlujo_Plantilla] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPlantilla]           NUMERIC (18)  NOT NULL,
    [Descripcion]           VARCHAR (200) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ba_TipoFlujo_Plantilla] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPlantilla] ASC)
);




CREATE TABLE [dbo].[aca_CampoAccion] (
    [IdEmpresa]             INT           NOT NULL,
    [IdCampoAccion]         INT           NOT NULL,
    [NombreCampoAccion]     VARCHAR (200) NOT NULL,
    [OrdenCampoAccion]      INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_CampoAccion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCampoAccion] ASC)
);




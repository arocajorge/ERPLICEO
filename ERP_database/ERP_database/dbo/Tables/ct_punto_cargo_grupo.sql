CREATE TABLE [dbo].[ct_punto_cargo_grupo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPunto_cargo_grupo]   INT           NOT NULL,
    [cod_Punto_cargo_grupo] VARCHAR (50)  NOT NULL,
    [nom_punto_cargo_grupo] VARCHAR (MAX) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ct_punto_cargo_grupo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPunto_cargo_grupo] ASC)
);




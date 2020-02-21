CREATE TABLE [dbo].[aca_MatriculaCondicionalParrafo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdParrafo]             INT           NOT NULL,
    [Nombre]                VARCHAR (50)  NOT NULL,
    [Parrafo]               VARCHAR (MAX) NOT NULL,
    [Orden]                 INT           NOT NULL,
    [IdCatalogoCONDIC]      INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_MatriculaCondicionalParrafo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdParrafo] ASC),
    CONSTRAINT [FK_aca_MatriculaCondicionalParrafo_aca_Catalogo] FOREIGN KEY ([IdCatalogoCONDIC]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo])
);




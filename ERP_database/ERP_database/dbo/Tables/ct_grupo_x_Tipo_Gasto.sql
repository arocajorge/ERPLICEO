CREATE TABLE [dbo].[ct_grupo_x_Tipo_Gasto] (
    [IdEmpresa]          INT          NOT NULL,
    [IdTipo_Gasto]       INT          NOT NULL,
    [IdTipo_Gasto_Padre] INT          NULL,
    [nom_tipo_Gasto]     VARCHAR (50) NOT NULL,
    [estado]             BIT          NOT NULL,
    [nivel]              INT          NULL,
    [orden]              INT          NULL,
    CONSTRAINT [PK_ct_Grupo_x_Tipo_Gasto] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipo_Gasto] ASC),
    CONSTRAINT [FK_ct_grupo_x_Tipo_Gasto_ct_grupo_x_Tipo_Gasto] FOREIGN KEY ([IdEmpresa], [IdTipo_Gasto_Padre]) REFERENCES [dbo].[ct_grupo_x_Tipo_Gasto] ([IdEmpresa], [IdTipo_Gasto])
);


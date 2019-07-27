CREATE TABLE [dbo].[cp_proveedor_clase] (
    [IdEmpresa]                      INT           NOT NULL,
    [IdClaseProveedor]               INT           NOT NULL,
    [cod_clase_proveedor]            VARCHAR (25)  NOT NULL,
    [descripcion_clas_prove]         VARCHAR (100) NOT NULL,
    [IdCtaCble_Anticipo]             VARCHAR (20)  NULL,
    [IdCtaCble_gasto]                VARCHAR (20)  NULL,
    [IdCtaCble_CXP]                  VARCHAR (20)  NULL,
    [IdCentroCosto]                  VARCHAR (20)  NULL,
    [IdCentroCosto_sub_centro_costo] VARCHAR (20)  NULL,
    [Estado]                         CHAR (1)      NOT NULL,
    [IdUsuario]                      VARCHAR (20)  NULL,
    [IdUsuarioAnu]                   VARCHAR (20)  NULL,
    [MotivoAnu]                      VARCHAR (150) NULL,
    [IdUsuarioUltModi]               VARCHAR (20)  NULL,
    [FechaAnu]                       DATETIME      NULL,
    [FechaTransac]                   DATETIME      NULL,
    [FechaUltModi]                   DATETIME      NULL,
    CONSTRAINT [PK_cp_proveedor_clase] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdClaseProveedor] ASC)
);


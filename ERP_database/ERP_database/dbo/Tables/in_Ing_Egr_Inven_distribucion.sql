CREATE TABLE [dbo].[in_Ing_Egr_Inven_distribucion] (
    [IdEmpresa]              INT          NOT NULL,
    [IdSucursal]             INT          NOT NULL,
    [IdMovi_inven_tipo]      INT          NOT NULL,
    [IdNumMovi]              NUMERIC (18) NOT NULL,
    [secuencia_distribucion] INT          NOT NULL,
    [IdEmpresa_dis]          INT          NOT NULL,
    [IdSucursal_dis]         INT          NOT NULL,
    [IdMovi_inven_tipo_dis]  INT          NOT NULL,
    [IdNumMovi_dis]          NUMERIC (18) NOT NULL,
    [estado]                 BIT          NOT NULL,
    [signo]                  VARCHAR (1)  NOT NULL,
    CONSTRAINT [PK_in_Ing_Egr_Inven_distribucion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdMovi_inven_tipo] ASC, [IdNumMovi] ASC, [secuencia_distribucion] ASC),
    CONSTRAINT [FK_in_Ing_Egr_Inven_distribucion_in_Ing_Egr_Inven] FOREIGN KEY ([IdEmpresa_dis], [IdSucursal_dis], [IdMovi_inven_tipo_dis], [IdNumMovi_dis]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi]),
    CONSTRAINT [FK_in_Ing_Egr_Inven_distribucion_in_Ing_Egr_Inven1] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi])
);


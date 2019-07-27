CREATE TABLE [dbo].[tb_bodega] (
    [IdEmpresa]                      INT           NOT NULL,
    [IdSucursal]                     INT           NOT NULL,
    [IdBodega]                       INT           NOT NULL,
    [cod_bodega]                     VARCHAR (50)  NULL,
    [bo_Descripcion]                 VARCHAR (100) NOT NULL,
    [cod_punto_emision]              CHAR (3)      NULL,
    [bo_manejaFacturacion]           NCHAR (1)     NULL,
    [bo_EsBodega]                    NCHAR (1)     NULL,
    [IdUsuario]                      VARCHAR (20)  NULL,
    [Fecha_Transac]                  DATETIME      NULL,
    [IdUsuarioUltMod]                VARCHAR (20)  NULL,
    [Fecha_UltMod]                   DATETIME      NULL,
    [IdUsuarioUltAnu]                VARCHAR (20)  NULL,
    [Fecha_UltAnu]                   DATETIME      NULL,
    [nom_pc]                         VARCHAR (50)  NULL,
    [ip]                             VARCHAR (25)  NULL,
    [Estado]                         CHAR (1)      NOT NULL,
    [IdEstadoAproba_x_Ing_Egr_Inven] VARCHAR (15)  NULL,
    [IdCentroCosto]                  VARCHAR (20)  NULL,
    [IdCtaCtble_Inve]                VARCHAR (20)  NULL,
    [IdCtaCtble_Costo]               VARCHAR (20)  NULL,
    CONSTRAINT [PK_tb_bodega] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC),
    CONSTRAINT [FK_tb_bodega_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCtble_Inve]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_tb_bodega_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCtble_Costo]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_tb_bodega_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);



